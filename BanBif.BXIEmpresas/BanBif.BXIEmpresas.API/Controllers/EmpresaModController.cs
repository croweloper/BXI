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
    public class EmpresaModController : ApiController
    {
        [Route("api/EmpresaMod/Registrar")]
        [HttpPost]
        public IHttpActionResult Registrar(RegistrarEmpresaRequest request)
        {
            var oBL = new EmpresaModBL();
            var resultado = oBL.Registrar(request);
            return Json(resultado);
        }

        [Route("api/EmpresaMod/Actualizar")]
        [HttpPost]
        public IHttpActionResult Actualizar(ActualizarEmpresaRequest request)
        {
            var oBL = new EmpresaModBL();
            var resultado = oBL.Actualizar(request);
            return Json(resultado);
        }

        [Route("api/EmpresaMod/ActualizarEntrega")]
        [HttpPost]
        public IHttpActionResult ActualizarEntrega(ActualizarEmpresaRequest request)
        {
            var oBL = new EmpresaModBL();
            var resultado = oBL.ActualizarDatoEntrega(request);
            return Json(resultado);
        }

        [Route("api/EmpresaMod/ObtenerRegistro")]
        [HttpPost]
        public IHttpActionResult ObtenerRegistro(ObtenerRegistroRequest request)
        {
            var oBL = new EmpresaModBL();
            var resultado = oBL.ObtenerRegistro(request);
            return Json(resultado);
        }

        [Route("api/EmpresaMod/VerificarToken")]
        [HttpPost]
        public IHttpActionResult VerificarToken(ValidacionTokenRequest request)
        {
            var oBL = new EmpresaModBL();
            var resultado = oBL.VerificarToken(request);
            return Json(resultado);
        }

        [Route("api/EmpresaMod/EnviarCorreo")]
        [HttpPost]
        public IHttpActionResult EnviarCorreo(CorreoRequest request)
        {
            var oBL = new EmpresaBL();
            bool Resultado = oBL.EnviarCorreo(request);
            return Json(Resultado);
        }

        [Route("api/EmpresaMod/ObtenerHome")]
        [HttpPost]
        public IHttpActionResult ObtenerHome(ObtenerRegistroRequest request)
        {
            var oBL = new EmpresaModBL();
            var resultado = oBL.ObtenerHome(request);
            return Json(resultado);
        }

        [Route("api/EmpresaMod/ObtenerToken")]
        [HttpPost]
        public IHttpActionResult ObtenerToken(ObtenerRegistroRequest request)
        {
            var oBL = new EmpresaModBL();
            var resultado = oBL.ObtenerToken(request);
            return Json(resultado);
        }

        [Route("api/EmpresaMod/ObtenerDatoEntrega")]
        [HttpPost]
        public IHttpActionResult ObtenerDatoEntrega(ObtenerDatoEntregaRequest request)
        {
            var oBL = new EmpresaModBL();
            var resultado = oBL.ObtenerDatoEntrega(request);
            return Json(resultado);
        }
    }
}
