using BanBif.BXIEmpresas.BE;
using BanBif.BXIEmpresas.BL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace BanBif.BXIEmpresas.API.Controllers
{
    public class EmpresaController : ApiController
    {
        [Route("api/Empresa/Registrar")]
        [HttpPost]
        public IHttpActionResult Registrar(RegistrarEmpresaRequest request)
        {
            var oBL = new EmpresaBL();
            var resultado = oBL.Registrar(request);
            return Json(resultado);
        }

        [Route("api/Empresa/Actualizar")]
        [HttpPost]
        public IHttpActionResult Actualizar(ActualizarEmpresaRequest request)
        {
            var oBL = new EmpresaBL();
            var resultado = oBL.Actualizar(request);
            return Json(resultado);
        }

        [Route("api/Empresa/ActualizarEntrega")]
        [HttpPost]
        public IHttpActionResult ActualizarEntrega(ActualizarEmpresaRequest request)
        {
            var oBL = new EmpresaBL();
            var resultado = oBL.ActualizarDatoEntrega(request);
            return Json(resultado);
        }

        [Route("api/Empresa/ObtenerDatos")]
        [HttpPost]
        public IHttpActionResult ObtenerDatos(DatosRequest request)
        {
            var oBL = new EmpresaBL();
            var resultado = oBL.ObtenerDatos(request);
            return Json(resultado);
        }

        [Route("api/Empresa/ListarOficinas")]
        [HttpPost]
        public IHttpActionResult ListarOficinas(OficinasRequest request)
        {
            var oBL = new EmpresaBL();
            var resultado = oBL.ListarOficinas(request);
            return Json(resultado);
        }

        [Route("api/Empresa/ObtenerRegistro")]
        [HttpPost]
        public IHttpActionResult ObtenerRegistro(ObtenerRegistroRequest request)
        {
            var oBL = new EmpresaBL();
            var resultado = oBL.ObtenerRegistro(request);
            return Json(resultado);
        }

        [Route("api/Empresa/ObtenerRepresentantes")]
        [HttpPost]
        public IHttpActionResult ObtenerRepresentantes(ObtenerRegistroRequest request)
        {
            var oBL = new EmpresaBL();
            var resultado = oBL.ObtenerRepresentantes(request);
            return Json(resultado);
        }

        [Route("api/Empresa/VerificarToken")]
        [HttpPost]
        public IHttpActionResult VerificarToken(ValidacionTokenRequest request)
        {
            var oBL = new EmpresaBL();
            var resultado = oBL.VerificarToken(request);
            return Json(resultado);
        }

        [Route("api/Empresa/EnviarCorreo")]
        [HttpPost]
        public IHttpActionResult EnviarCorreo(CorreoRequest request)
        {
            var oBL = new EmpresaBL();
            bool Resultado = oBL.EnviarCorreo(request);
            return Json(Resultado);
        }

        [Route("api/Empresa/ObtenerConfiguracion")]
        [HttpPost]
        public IHttpActionResult ObtenerConfiguracion(ObtenerConfiguracionRequest request)
        {
            var oBL = new EmpresaBL();
            var resultado = oBL.ObtenerConfiguracion(request);
            return Json(resultado);
        }

        [Route("api/Empresa/ObtenerDatoEntrega")]
        [HttpPost]
        public IHttpActionResult ObtenerDatoEntrega(ObtenerDatoEntregaRequest request)
        {
            var oBL = new EmpresaBL();
            var resultado = oBL.ObtenerDatoEntrega(request);
            return Json(resultado);
        }
    }
}
