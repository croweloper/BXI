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

namespace BanBif.BXIEmpresas.Mod.APP.Controllers
{
    public class ResumenController : Controller
    {
        // GET: Resumen
        public ActionResult Index(string sol, string token)
        {
            var valorid = 0;
            var id = 0;
            if (int.TryParse(sol, out valorid))
            {
                id = int.Parse(sol);
            };

            if (id != 0 && token != null)
            {
                var request = new ValidacionTokenRequest
                {
                    idregistro = id,
                    token = token
                };

                string apiBaseUrl = ConfigurationManager.AppSettings.Get("UrlAPI").ToString();
                string apiUrl = apiBaseUrl + "api/EmpresaMod/VerificarToken";
                var result = new HttpResponseMessage();
                using (var client = new HttpClient())
                {
                    var jsonObject = JsonConvert.SerializeObject(request);
                    var content = new StringContent(jsonObject, Encoding.UTF8, "application/json");
                    System.Net.ServicePointManager.ServerCertificateValidationCallback = delegate (object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors) { return true; };
                    result = client.PostAsync(apiUrl, content).Result;

                    var dataObjects = result.Content.ReadAsStringAsync().Result;
                    var resultado = JsonConvert.DeserializeObject<ValidacionTokenResponse>(dataObjects);

                    if (resultado.result)
                    {
                        ViewBag.ok = 1;
                        ViewBag.sol = id;
                        ViewBag.App = ConfigurationManager.AppSettings.Get("UrlAPP").ToString();
                    }
                    else
                    {
                        ViewBag.ok = 0;
                    }
                }
            }
            else
            {
                ViewBag.ok = 0;
            }

            return View();
        }

        [HttpPost]
        public ActionResult ObtenerRegistro(ObtenerRegistroRequest request)
        {
            string apiBaseUrl = ConfigurationManager.AppSettings.Get("UrlAPI").ToString();
            string apiUrl = apiBaseUrl + "api/EmpresaMod/ObtenerRegistro";
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