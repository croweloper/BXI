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
    public class ProveedorController : ApiController
    {
        [Route("api/Proveedor/BuscarPorRuc")]
        [HttpPost]
        public IHttpActionResult BuscarPorRuc(ProveedorRequest request)
        {
            var oBL = new ProveedorBL();
            var resultado = oBL.BuscarPorRuc(request);
            return Json(resultado);
        }
    }
}
