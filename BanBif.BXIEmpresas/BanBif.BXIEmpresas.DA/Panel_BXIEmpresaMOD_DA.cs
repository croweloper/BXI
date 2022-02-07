using BanBif.BXIEmpresas.BE;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BanBif.BXIEmpresas.DA
{
    public class Panel_BXIEmpresaMOD_DA
    {
        public RegistrarEmpresaResult Registrar(RegistrarEmpresaRequest request)
        {
            using (var db = new panelEntities())
            {
                try
                {
                    var codigoregistro = 0;
                    if (request.codigoregistro == 0)
                    {
                        var nuevo = new BPI_EMPRESAS_MOD_registro
                        {
                            ruc = request.ruc,
                            razon_social = request.razonsocial,
                            telefono = request.telefono,
                            direccion = request.direccion,
                            ip = request.ip,
                            f_registro = DateTime.Now,
                            status = 0
                        };

                        db.BPI_EMPRESAS_MOD_registro.Add(nuevo);
                        db.SaveChanges();
                        codigoregistro = nuevo.idregistro;
                    }
                    else {
                        var registro = db.BPI_EMPRESAS_MOD_registro.Where(p => p.idregistro == request.codigoregistro).FirstOrDefault();
                        registro.ruc = request.ruc;
                        registro.razon_social = request.razonsocial;
                        registro.telefono = request.telefono;
                        registro.direccion = request.direccion;

                        db.SaveChanges();

                        /*ELIMINAR REPRESENTANTES*/
                        var representantes = db.BPI_EMPRESAS_MOD_representante.Where(p => p.idregistro == request.codigoregistro).ToList();
                        db.BPI_EMPRESAS_MOD_representante.RemoveRange(representantes);
                        db.SaveChanges();

                        codigoregistro = request.codigoregistro;
                    }
                    

                    if (request.Representante != null)
                    {
                        foreach (var item in request.Representante)
                        {
                            var nuevorep = new BPI_EMPRESAS_MOD_representante
                            {
                                idregistro = codigoregistro,
                                nombre = item.nombre,
                                tipo_documento = item.tipodocumento,
                                documento = item.documento,
                                f_registro = DateTime.Now,
                                status = 0
                            };

                            db.BPI_EMPRESAS_MOD_representante.Add(nuevorep);
                            db.SaveChanges();
                        }
                    }

                    return new RegistrarEmpresaResult { codigoregistro = codigoregistro };
                }
                catch (Exception ex)
                {
                    return new RegistrarEmpresaResult();
                }

            }
        }

        public ActualizarEmpresaResponse Actualizar(ActualizarEmpresaRequest request)
        {
            using (var db = new panelEntities())
            {
                var resultado = new ActualizarEmpresaResult();
                var dbEmpresa = db.BPI_EMPRESAS_MOD_registro.Where(p => p.idregistro == request.codigoregistro).FirstOrDefault();

                if (dbEmpresa != null)
                {
                    dbEmpresa.token_fisico = request.tokenfisico;
                    dbEmpresa.cuenta_cargo = request.cuentacargo;
                    dbEmpresa.moneda_cargo = request.monedacargo;
                    dbEmpresa.status = request.estado;

                    /*ELIMINAR TOKENS*/
                    var tokenseliminar = db.BPI_EMPRESAS_MOD_token.Where(p => p.idregistro == request.codigoregistro).ToList();
                    db.BPI_EMPRESAS_MOD_token.RemoveRange(tokenseliminar);
                    db.SaveChanges();

                    /*INSERTAR TOKEN ADMIN*/
                    db.BPI_EMPRESAS_MOD_token.Add(new BPI_EMPRESAS_MOD_token
                    {
                        idregistro = dbEmpresa.idregistro,
                        tipo = 0,
                        token_usuario = request.tokenadmin,
                        f_registro = DateTime.Now
                    });

                    /*INSERTAR TOKEN*/
                    if (request.reposicion != null) {
                        foreach (var item in request.reposicion.Where(p => p.estado == 1))
                        {
                            db.BPI_EMPRESAS_MOD_token.Add(new BPI_EMPRESAS_MOD_token
                            {
                                idregistro = dbEmpresa.idregistro,
                                tipo = 1,
                                token_usuario = item.token,
                                f_registro = DateTime.Now
                            });
                        }
                    }

                    if (request.nuevaclave != null) {
                        foreach (var item in request.nuevaclave.Where(p => p.estado == 1))
                        {
                            db.BPI_EMPRESAS_MOD_token.Add(new BPI_EMPRESAS_MOD_token
                            {
                                idregistro = dbEmpresa.idregistro,
                                tipo = 2,
                                token_usuario = item.token,
                                f_registro = DateTime.Now
                            });
                        }
                    }



                    if (request.anulacion != null)
                    {
                        foreach (var item in request.anulacion.Where(p => p.estado == 1))
                        {
                            db.BPI_EMPRESAS_MOD_token.Add(new BPI_EMPRESAS_MOD_token
                            {
                                idregistro = dbEmpresa.idregistro,
                                tipo = 3,
                                token_usuario = item.token,
                                f_registro = DateTime.Now
                            });
                        }
                    }
                    

                    db.SaveChanges();
                    resultado.codigoregistro = dbEmpresa.idregistro;
                    return new ActualizarEmpresaResponse { result = true, data = resultado };
                }
                else
                {
                    return new ActualizarEmpresaResponse { result = false };
                }
            }
        }

        public ActualizarEmpresaResponse ActualizarDatoEntrega(ActualizarEmpresaRequest request)
        {
            using (var db = new panelEntities())
            {
                var resultado = new ActualizarEmpresaResult();
                var dbEmpresa = db.BPI_EMPRESAS_MOD_registro.Where(p => p.idregistro == request.codigoregistro).FirstOrDefault();

                if (dbEmpresa != null)
                {
                    dbEmpresa.codoficina = request.codigooficina.ToString();
                    dbEmpresa.dni_administrador = request.dniadministrador;
                    dbEmpresa.nombre_administrador = request.nombreadministrador;
                    dbEmpresa.email_administrador = request.emailadministrador;
                    dbEmpresa.telefono_administrador = request.telefonoadministrador;
                    dbEmpresa.token = new Random().Next(10000000, 99999999).ToString();
                    db.SaveChanges();

                    /*ELIMINAR REGISTROS TOKEN*/
                    var dbToken = db.BPI_EMPRESAS_MOD_autorizados.Where(p => p.idregistro == request.codigoregistro);

                    if (dbToken != null)
                    {
                        db.BPI_EMPRESAS_MOD_autorizados.RemoveRange(dbToken);
                        db.SaveChanges();
                    }

                    /*AGREGAR NUEVOS AUTORIZADOS*/
                    foreach (var item in request.autorizados)
                    {
                        db.BPI_EMPRESAS_MOD_autorizados.Add(new BPI_EMPRESAS_MOD_autorizados
                        {
                            idregistro = dbEmpresa.idregistro,
                            nombre = item.nombre,
                            tipo_documento = item.tipodocumento,
                            documento = item.documento,
                            f_registro = DateTime.Now,
                            status = 0
                        });
                        db.SaveChanges();
                    }

                    resultado.codigoregistro = dbEmpresa.idregistro;
                    return new ActualizarEmpresaResponse { result = true, data = resultado };
                }
                else
                {
                    return new ActualizarEmpresaResponse { result = false };
                }
            }
        }

        public ObtenerRegistroResponse ObtenerRegistro(ObtenerRegistroRequest request)
        {
            using (var db = new panelEntities())
            {
                var registro = db.BPI_EMPRESAS_MOD_registro.Where(p => p.idregistro == request.codigoregistro).FirstOrDefault();
                if (registro != null)
                {
                    var contadorcosto = 0;


                    var resultado = new ObtenerRegistroResult();
                    resultado.razonsocial = registro.razon_social;
                    resultado.ruc = registro.ruc;
                    resultado.direccion = registro.direccion;
                    resultado.telefono = registro.telefono; 
                    resultado.nombreadministrador = registro.nombre_administrador;
                    resultado.emailadministrador = registro.email_administrador;
                    resultado.tokenfisico = registro.token_fisico.Value;
                    resultado.cuentacargo = registro.cuenta_cargo;
                    resultado.moneda = registro.moneda_cargo;
                    resultado.estado = registro.status.Value;
                    resultado.tokenvalidacion = registro.token;

                    contadorcosto += resultado.tokenfisico;

                    var direccion = db.SP_BPI_EMPRESAS_OFICINAS("T").Where(p => p.codoficina == registro.codoficina).FirstOrDefault();

                    if (direccion.region == "L")
                    {
                        resultado.oficinaentrega = direccion.oficina + " - " + direccion.distrito;
                    }
                    else
                    {
                        resultado.oficinaentrega = direccion.oficina + " - " + direccion.departamento;
                    }

                    resultado.direccionentrega = direccion.direccion;

                    var dbautorizados = db.BPI_EMPRESAS_MOD_autorizados.Where(p => p.idregistro == request.codigoregistro).ToList();
                    var listaAutorizados = new List<Autorizado>();
                    foreach (var item in dbautorizados)
                    {
                        listaAutorizados.Add(new Autorizado
                        {
                            autorizado = item.nombre,
                            documento = item.documento
                        });
                    }

                    resultado.autorizados = listaAutorizados;

                    var dbrepresentantes = db.BPI_EMPRESAS_MOD_representante.Where(p => p.idregistro == request.codigoregistro).ToList();
                    var listaRepresentantes = new List<Representante>();
                    foreach (var item in dbrepresentantes)
                    {
                        listaRepresentantes.Add(new Representante
                        {
                            documento = item.documento,
                            representante = item.nombre
                        });
                    }
                    resultado.representantes = listaRepresentantes;

                    /*TOKEN ADMIN*/
                    var dbAdmin = db.BPI_EMPRESAS_MOD_token.Where(p => p.tipo == 0 && p.idregistro == request.codigoregistro && p.token_usuario == "SI").ToList();
                    foreach (var item in dbAdmin)
                    {
                        resultado.tokenadmin = true;
                        contadorcosto += 1;
                    }                    


                    var dbReposicion = db.BPI_EMPRESAS_MOD_token.Where(p => p.tipo == 1 && p.idregistro == request.codigoregistro).ToList();
                    var listaReposicion = new List<TokenRequest>();
                    foreach (var item in dbReposicion) {
                        listaReposicion.Add(new TokenRequest
                        {
                            token = item.token_usuario                            
                        });
                        contadorcosto += 1;
                    }

                    resultado.reposicion = listaReposicion;

                    var dbClavenueva = db.BPI_EMPRESAS_MOD_token.Where(p => p.tipo == 2 && p.idregistro == request.codigoregistro).ToList();
                    var listaClavenueva = new List<TokenRequest>();
                    foreach (var item in dbClavenueva)
                    {
                        listaClavenueva.Add(new TokenRequest
                        {
                            token = item.token_usuario
                        });                        
                    }
                    resultado.nuevaclave = listaClavenueva;

                    var dbAnulacion = db.BPI_EMPRESAS_MOD_token.Where(p => p.tipo == 3 && p.idregistro == request.codigoregistro).ToList();
                    var listaanulacion = new List<TokenRequest>();
                    foreach (var item in dbAnulacion)
                    {
                        listaanulacion.Add(new TokenRequest
                        {
                            token = item.token_usuario
                        });
                    }

                    resultado.anulacion = listaanulacion;

                    resultado.costo = contadorcosto * 50;

                    return new ObtenerRegistroResponse { result = true, data = resultado };
                }
                else
                {
                    return new ObtenerRegistroResponse { result = false };
                }
            }
        }

        public ValidacionTokenResponse VerificarToken(ValidacionTokenRequest request)
        {
            using (var db = new panelEntities())
            {
                var registro = db.BPI_EMPRESAS_MOD_registro.Where(p => p.idregistro == request.idregistro && p.token == request.token).FirstOrDefault();
                if (registro != null)
                {
                    return new ValidacionTokenResponse { result = true };
                }
                else
                {
                    return new ValidacionTokenResponse { result = false };
                }
            }
        }

        public ObtenerRegistroResponse ObtenerHome(ObtenerRegistroRequest request)
        {
            using (var db = new panelEntities())
            {
                var registro = db.BPI_EMPRESAS_MOD_registro.Where(p => p.idregistro == request.codigoregistro).FirstOrDefault();
                if (registro != null)
                {
                    
                    var resultado = new ObtenerRegistroResult();
                    resultado.razonsocial = registro.razon_social;
                    resultado.ruc = registro.ruc;
                    resultado.direccion = registro.direccion;
                    resultado.telefono = registro.telefono;                                       

                    var dbrepresentantes = db.BPI_EMPRESAS_MOD_representante.Where(p => p.idregistro == request.codigoregistro).ToList();
                    var listaRepresentantes = new List<Representante>();
                    foreach (var item in dbrepresentantes)
                    {
                        listaRepresentantes.Add(new Representante
                        {
                            documento = item.documento,
                            representante = item.nombre,
                            tipodocumento = item.tipo_documento.Value
                        });
                    }
                    resultado.representantes = listaRepresentantes;
                   
                    return new ObtenerRegistroResponse { result = true, data = resultado };
                }
                else
                {
                    return new ObtenerRegistroResponse { result = false };
                }
            }
        }

        public ObtenerRegistroResponse ObtenerToken(ObtenerRegistroRequest request)
        {
            using (var db = new panelEntities())
            {
                var registro = db.BPI_EMPRESAS_MOD_registro.Where(p => p.idregistro == request.codigoregistro).FirstOrDefault();
                if (registro != null)
                {
                    var contadorcosto = 0;


                    var resultado = new ObtenerRegistroResult();
                   
                    resultado.tokenfisico = registro.token_fisico.Value;
                    resultado.cuentacargo = registro.cuenta_cargo;
                    resultado.moneda = registro.moneda_cargo;
                    resultado.tokenvalidacion = registro.token;

                    contadorcosto += resultado.tokenfisico;

                   

                    /*TOKEN ADMIN*/
                    var dbAdmin = db.BPI_EMPRESAS_MOD_token.Where(p => p.tipo == 0 && p.idregistro == request.codigoregistro && p.token_usuario == "SI").ToList();
                    foreach (var item in dbAdmin)
                    {
                        resultado.tokenadmin = true;
                        contadorcosto += 1;
                    }


                    var dbReposicion = db.BPI_EMPRESAS_MOD_token.Where(p => p.tipo == 1 && p.idregistro == request.codigoregistro).ToList();
                    var listaReposicion = new List<TokenRequest>();
                    foreach (var item in dbReposicion)
                    {
                        listaReposicion.Add(new TokenRequest
                        {
                            token = item.token_usuario
                        });
                        contadorcosto += 1;
                    }

                    resultado.reposicion = listaReposicion;

                    var dbClavenueva = db.BPI_EMPRESAS_MOD_token.Where(p => p.tipo == 2 && p.idregistro == request.codigoregistro).ToList();
                    var listaClavenueva = new List<TokenRequest>();
                    foreach (var item in dbClavenueva)
                    {
                        listaClavenueva.Add(new TokenRequest
                        {
                            token = item.token_usuario
                        });
                    }
                    resultado.nuevaclave = listaClavenueva;

                    var dbAnulacion = db.BPI_EMPRESAS_MOD_token.Where(p => p.tipo == 3 && p.idregistro == request.codigoregistro).ToList();
                    var listaanulacion = new List<TokenRequest>();
                    foreach (var item in dbAnulacion)
                    {
                        listaanulacion.Add(new TokenRequest
                        {
                            token = item.token_usuario
                        });
                    }

                    resultado.anulacion = listaanulacion;

                    resultado.costo = contadorcosto * 50;

                    return new ObtenerRegistroResponse { result = true, data = resultado };
                }
                else
                {
                    return new ObtenerRegistroResponse { result = false };
                }
            }
        }

        public ObtenerDatoEntregaResponse ObtenerDatoEntrega(ObtenerDatoEntregaRequest request) {
            using (var db = new panelEntities()) {
                var response = new ObtenerDatoEntregaResponse();
                var registro = db.BPI_EMPRESAS_MOD_registro.Where(p => p.idregistro == request.codigoregistro).FirstOrDefault();
                if (registro != null) {
                    response.result = true;
                    var oficinaparse = 0;
                    var resultado = new ObtenerDatoEntregaResult
                    {
                        dniadministrador = registro.dni_administrador,
                        nombreadministrador = registro.nombre_administrador,
                        emailadministrador = registro.email_administrador,
                        telefonoadministrador = registro.telefono_administrador,
                        codigooficina = int.TryParse(registro.codoficina, out oficinaparse) ? int.Parse(registro.codoficina): oficinaparse                        
                    };

                    if (resultado.codigooficina > 0) {
                        var oficina = db.SP_BPI_EMPRESAS_OFICINAS("T").ToList().Where(p => p.codoficina == registro.codoficina).FirstOrDefault();
                        resultado.region = oficina.region;
                    }
                    

                    /*AUTORIZADOS*/

                    var dbautorizados = db.BPI_EMPRESAS_MOD_autorizados.Where(p => p.idregistro == request.codigoregistro).ToList();
                    var listaAutorizados = new List<AutorizadosRequest>();
                    foreach (var item in dbautorizados)
                    {
                        listaAutorizados.Add(new AutorizadosRequest
                        {
                            nombre = item.nombre,
                            tipodocumento = item.tipo_documento.Value,
                            documento = item.documento
                        });
                    }

                    resultado.autorizados = listaAutorizados;

                    response.data = resultado;


                }

                return response;
            }
        }
    }
}
