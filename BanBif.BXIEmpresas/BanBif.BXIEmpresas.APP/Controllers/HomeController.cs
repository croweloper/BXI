using BanBif.BXIEmpresas.BE;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace BanBif.BXIEmpresas.APP.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            ViewBag.App = ConfigurationManager.AppSettings.Get("UrlAPP").ToString();
            return View();
        }

        [HttpPost]
        public ActionResult BuscarProveedor(ProveedorRequest request)
        {
            string apiBaseUrl = ConfigurationManager.AppSettings.Get("UrlAPI").ToString();
            string apiUrl = apiBaseUrl + "api/Proveedor/BuscarPorRuc";
            var result = new HttpResponseMessage();
            using (var client = new HttpClient())
            {
                var jsonObject = JsonConvert.SerializeObject(request);
                var content = new StringContent(jsonObject, Encoding.UTF8, "application/json");
                System.Net.ServicePointManager.ServerCertificateValidationCallback = delegate (object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors) { return true; };
                result = client.PostAsync(apiUrl, content).Result;

                var dataObjects = result.Content.ReadAsStringAsync().Result;
                var resultado = JsonConvert.DeserializeObject<ProveedorResponse>(dataObjects);

                return Json(resultado);
            }
        }

        [HttpPost]
        public ActionResult RegistrarEmpresa(RegistrarEmpresaRequest request)
        {
            string apiBaseUrl = ConfigurationManager.AppSettings.Get("UrlAPI").ToString();
            string apiUrl = apiBaseUrl + "api/Empresa/Registrar";
            var result = new HttpResponseMessage();
            using (var client = new HttpClient())
            {
                var jsonObject = JsonConvert.SerializeObject(request);
                var content = new StringContent(jsonObject, Encoding.UTF8, "application/json");
                System.Net.ServicePointManager.ServerCertificateValidationCallback = delegate (object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors) { return true; };
                result = client.PostAsync(apiUrl, content).Result;

                var dataObjects = result.Content.ReadAsStringAsync().Result;
                var resultado = JsonConvert.DeserializeObject<RegistrarEmpresaResponse>(dataObjects);

                return Json(resultado);
            }
        }

        [HttpPost]
        public ActionResult ObtenerRepresentantes(ObtenerRegistroRequest request)
        {
            string apiBaseUrl = ConfigurationManager.AppSettings.Get("UrlAPI").ToString();
            string apiUrl = apiBaseUrl + "api/Empresa/ObtenerRepresentantes";
            var result = new HttpResponseMessage();
            using (var client = new HttpClient())
            {
                var jsonObject = JsonConvert.SerializeObject(request);
                var content = new StringContent(jsonObject, Encoding.UTF8, "application/json");
                System.Net.ServicePointManager.ServerCertificateValidationCallback = delegate (object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors) { return true; };
                result = client.PostAsync(apiUrl, content).Result;

                var dataObjects = result.Content.ReadAsStringAsync().Result;
                var resultado = JsonConvert.DeserializeObject<ObtenerRegistroResponse>(dataObjects);

                return Json(resultado);
            }
        }
    }
}