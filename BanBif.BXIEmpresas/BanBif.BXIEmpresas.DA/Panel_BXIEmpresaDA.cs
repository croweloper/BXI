using BanBif.BXIEmpresas.BE;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BanBif.BXIEmpresas.DA
{
    public class Panel_BXIEmpresaDA
    {
        public RegistrarEmpresaResult Registrar(RegistrarEmpresaRequest request) {
            using (var db = new panelEntities()) {
                try
                {
                    var codigoregistro = 0;

                    if (request.codigoregistro == 0)
                    {

                        var nuevo = new BPI_EMPRESAS_registro
                        {
                            ruc = request.ruc,
                            razon_social = request.razonsocial,
                            telefono = request.telefono,
                            direccion = request.direccion,
                            ip = request.ip,
                            f_registro = DateTime.Now,
                            status = 0
                        };

                        db.BPI_EMPRESAS_registro.Add(nuevo);
                        db.SaveChanges();

                        codigoregistro = nuevo.idregistro;
                    }
                    else {
                        var registro = db.BPI_EMPRESAS_registro.Where(p => p.idregistro == request.codigoregistro).FirstOrDefault();
                        registro.ruc = request.ruc;
                        registro.razon_social = request.razonsocial;
                        registro.telefono = request.telefono;
                        registro.direccion = request.direccion;

                        db.SaveChanges();

                        /*ELIMINAR REPRESENTANTES*/
                        var representantes = db.BPI_EMPRESAS_representante.Where(p => p.idregistro == request.codigoregistro).ToList();
                        db.BPI_EMPRESAS_representante.RemoveRange(representantes);
                        db.SaveChanges();

                        codigoregistro = request.codigoregistro;
                    }
                    
                    if (request.Representante != null) {
                        foreach (var item in request.Representante)
                        {
                            var nuevorep = new BPI_EMPRESAS_representante
                            {
                                idregistro = codigoregistro,
                                nombre = item.nombre,
                                tipo_documento = item.tipodocumento,
                                documento = item.documento,
                                f_registro = DateTime.Now,
                                status = 0
                            };

                            db.BPI_EMPRESAS_representante.Add(nuevorep);
                            db.SaveChanges();
                        }
                    }

                    return new RegistrarEmpresaResult { codigoregistro = codigoregistro };
                }
                catch (Exception ex) {
                    return new RegistrarEmpresaResult();
                }
                
            }
        }

        public ActualizarEmpresaResponse Actualizar(ActualizarEmpresaRequest request) {
            using (var db = new panelEntities()) {
                var resultado = new ActualizarEmpresaResult();
                var dbEmpresa = db.BPI_EMPRESAS_registro.Where(p => p.idregistro == request.codigoregistro).FirstOrDefault();

                if (dbEmpresa != null)
                {
                    dbEmpresa.version = request.version;
                    dbEmpresa.moneda_cargo = request.monedacargo;
                    dbEmpresa.cuenta_cargo = request.cuentacargo;
                    dbEmpresa.entidad = request.entidad;
                    dbEmpresa.firmantes = request.firmantes;
                    dbEmpresa.token_fisico = request.tokenfisico;
                    dbEmpresa.token_digital = request.tokendigital;
                    dbEmpresa.dni_administrador = request.dniadministrador;
                    dbEmpresa.nombre_administrador = request.nombreadministrador;
                    dbEmpresa.usuario_administrador = "ADMIN";
                    dbEmpresa.email_administrador = request.emailadministrador;
                    dbEmpresa.telefono_administrador = request.telefonoadministrador;

                    db.SaveChanges();

                    /*ELIMINAR REGISTROS TOKEN*/
                    var dbToken = db.BPI_EMPRESAS_token.Where(p => p.idregistro == request.codigoregistro);

                    if (dbToken != null) {
                        db.BPI_EMPRESAS_token.RemoveRange(dbToken);
                        db.SaveChanges();
                    }
                    

                    resultado.codigoregistro = dbEmpresa.idregistro;
                    return new ActualizarEmpresaResponse { result = true, data = resultado };
                }
                else {
                    return new ActualizarEmpresaResponse { result = false };
                }
            }
        }

        public ActualizarEmpresaResponse ActualizarDatoEntrega(ActualizarEmpresaRequest request)
        {
            using (var db = new panelEntities())
            {
                var resultado = new ActualizarEmpresaResult();
                var dbEmpresa = db.BPI_EMPRESAS_registro.Where(p => p.idregistro == request.codigoregistro).FirstOrDefault();

                if (dbEmpresa != null)
                {
                    dbEmpresa.codoficina = request.codigooficina.ToString();
                    dbEmpresa.token = new Random().Next(10000000, 99999999).ToString();
                    db.SaveChanges();

                    /*ELIMINAR REGISTROS TOKEN*/
                    var dbToken = db.BPI_EMPRESAS_autorizados.Where(p => p.idregistro == request.codigoregistro);

                    if (dbToken != null)
                    {
                        db.BPI_EMPRESAS_autorizados.RemoveRange(dbToken);
                        db.SaveChanges();
                    }

                    /*AGREGAR NUEVOS AUTORIZADOS*/
                    foreach (var item in request.autorizados) {
                        db.BPI_EMPRESAS_autorizados.Add(new BPI_EMPRESAS_autorizados {
                            idregistro = dbEmpresa.idregistro,
                            nombre = item.nombre,
                            tipo_documento = item.tipodocumento,
                            documento = item.documento,
                            Telefono = item.telefono,
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

        public ObtenerRegistroResponse ObtenerRegistro(ObtenerRegistroRequest request) {
            using (var db = new panelEntities()) {
                var registro = db.BPI_EMPRESAS_registro.Where(p => p.idregistro == request.codigoregistro).FirstOrDefault();

                if (registro != null)
                {
                    var resultado = new ObtenerRegistroResult();
                    resultado.razonsocial = registro.razon_social;
                    resultado.ruc = registro.ruc;
                    resultado.direccion = registro.direccion;
                    resultado.telefono = registro.telefono;
                    resultado.entidad = registro.entidad;
                    resultado.administrador = registro.usuario_administrador;
                    resultado.nombreadministrador = registro.nombre_administrador;
                    resultado.dniadministrador = registro.dni_administrador;
                    resultado.emailadministrador = registro.email_administrador;
                    resultado.telefonoadministrador = registro.telefono_administrador;
                    resultado.version = registro.version == 1 ? "Consulta" : "Full";
                    resultado.cuentacargo = registro.cuenta_cargo;
                    resultado.moneda = registro.moneda_cargo;
                    resultado.tokendigitales = registro.token_digital.Value;
                    resultado.tokenfisico = registro.token_fisico.Value;
                    resultado.tokenvalidacion = registro.token;

                    var direccion = db.SP_BPI_EMPRESAS_OFICINAS("T").Where(p => p.codoficina == registro.codoficina).FirstOrDefault();

                    if (direccion.region == "L") {
                        resultado.oficinaentrega = direccion.oficina + " - " + direccion.distrito;
                    }else
                    {
                        resultado.oficinaentrega = direccion.oficina + " - " + direccion.departamento;
                    }

                    resultado.direccionentrega = direccion.direccion;

                    var dbautorizados = db.BPI_EMPRESAS_autorizados.Where(p => p.idregistro == request.codigoregistro).ToList();
                    var listaAutorizados = new List<Autorizado>();
                    foreach (var item in dbautorizados) {
                        listaAutorizados.Add(new Autorizado {
                            autorizado = item.nombre,
                            documento = item.documento
                        });
                    }

                    resultado.autorizados = listaAutorizados;

                    var dbrepresentantes = db.BPI_EMPRESAS_representante.Where(p => p.idregistro == request.codigoregistro).ToList();
                    var listaRepresentantes = new List<Representante>();
                    foreach (var item in dbrepresentantes) {
                        listaRepresentantes.Add(new Representante {
                            documento = item.documento,
                            representante = item.nombre
                        });
                    }
                    resultado.representantes = listaRepresentantes;



                    return new ObtenerRegistroResponse { result = true, data = resultado };
                }
                else {
                    return new ObtenerRegistroResponse { result = false };
                }
            }
        }

        public ObtenerRegistroResponse ObtenerRepresentantes(ObtenerRegistroRequest request)
        {
            using (var db = new panelEntities())
            {
                var registro = db.BPI_EMPRESAS_registro.Where(p => p.idregistro == request.codigoregistro).FirstOrDefault();

                if (registro != null)
                {
                    var resultado = new ObtenerRegistroResult();
                    resultado.razonsocial = registro.razon_social;
                    resultado.ruc = registro.ruc;
                    resultado.direccion = registro.direccion;
                    resultado.telefono = registro.telefono;                                     

                    var dbrepresentantes = db.BPI_EMPRESAS_representante.Where(p => p.idregistro == request.codigoregistro).ToList();
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

        public DatosResponse ObtenerDatos(DatosRequest request) {
            using (var db = new panelEntities()) {
                var datosdb = db.BPI_EMPRESAS_dato.Where(p => p.status == request.estado).ToList();
                var listaDatos = new List<DatosResult>();

                foreach (var item in datosdb) {
                    listaDatos.Add(new DatosResult {
                        codigodato = item.iddato,
                        dato = item.dato,
                        texto = item.texto1,
                        valor = item.decimal1.HasValue ? item.decimal1.Value : 0
                    });
                }

                return new DatosResponse { result = true, data = listaDatos};
            }
        }

        public OficinaResponse ListarOficinas(OficinasRequest request) {
            using (var db = new panelEntities()) {
                var dboficinas = db.SP_BPI_EMPRESAS_OFICINAS(request.region).ToList();
                var lista = new List<OficinasResult>();
                foreach (var item in dboficinas) {
                    lista.Add(new OficinasResult {
                        codoficina = int.Parse(item.codoficina),
                        oficina = item.oficina,
                        departamento = item.departamento,
                        provincia = item.provincia,
                        distrito = item.distrito,
                        direccion = item.direccion
                    });
                }

                if (lista.Count > 0)
                {
                    return new OficinaResponse { result = true, data = lista };
                }
                else {
                    return new OficinaResponse { result = false };
                }
            }
        }

        public ValidacionTokenResponse VerificarToken(ValidacionTokenRequest request) {
            using (var db = new panelEntities()) {
                var registro = db.BPI_EMPRESAS_registro.Where(p => p.idregistro == request.idregistro && p.token == request.token).FirstOrDefault();
                if (registro != null)
                {
                    return new ValidacionTokenResponse { result = true };
                }
                else {
                    return new ValidacionTokenResponse { result = false };
                }
            }
        }

        public ObtenerConfiguracionResponse ObtenerConfiguracion(ObtenerConfiguracionRequest request)
        {
            using (var db = new panelEntities())
            {
                var registro = db.BPI_EMPRESAS_registro.Where(p => p.idregistro == request.codigoregistro).FirstOrDefault();

                if (registro != null)
                {
                    if (registro.version != null)
                    {
                        var resultado = new ObtenerConfiguracionResult();
                        resultado.entidad = registro.entidad;
                        resultado.administrador = registro.usuario_administrador;
                        resultado.nombreadministrador = registro.nombre_administrador;
                        resultado.dniadministrador = registro.dni_administrador;
                        resultado.emailadministrador = registro.email_administrador;
                        resultado.telefonoadministrador = registro.telefono_administrador;
                        resultado.version = registro.version == 1 ? "Consulta" : "Full";
                        resultado.cuentacargo = registro.cuenta_cargo;
                        resultado.moneda = registro.moneda_cargo;
                        resultado.tokenfisico = registro.token_fisico.Value;

                        return new ObtenerConfiguracionResponse { result = true, data = resultado };
                    }
                }
            
                return new ObtenerConfiguracionResponse { result = false };
                
            }
        }

        public ObtenerDatoEntregaResponse ObtenerDatoEntrega(ObtenerDatoEntregaRequest request)
        {
            using (var db = new panelEntities())
            {
                var response = new ObtenerDatoEntregaResponse();
                var registro = db.BPI_EMPRESAS_registro.Where(p => p.idregistro == request.codigoregistro).FirstOrDefault();
                if (registro != null)
                {
                    response.result = true;
                    var oficinaparse = 0;
                    var resultado = new ObtenerDatoEntregaResult
                    {
                        codigooficina = int.TryParse(registro.codoficina, out oficinaparse) ? int.Parse(registro.codoficina) : oficinaparse
                    };

                    if (resultado.codigooficina > 0)
                    {
                        var oficina = db.SP_BPI_EMPRESAS_OFICINAS("T").ToList().Where(p => p.codoficina == registro.codoficina).FirstOrDefault();
                        resultado.region = oficina.region;
                    }


                    /*AUTORIZADOS*/
                    var dbautorizados = db.BPI_EMPRESAS_autorizados.Where(p => p.idregistro == request.codigoregistro).ToList();
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
