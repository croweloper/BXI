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
    public class DatosEntregaController : Controller
    {
        // GET: DatosEntrega
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ListarOficinas(OficinasRequest request)
        {
            string apiBaseUrl = ConfigurationManager.AppSettings.Get("UrlAPI").ToString();
            string apiUrl = apiBaseUrl + "api/Empresa/ListarOficinas";
            var result = new HttpResponseMessage();
            using (var client = new HttpClient())
            {
                var jsonObject = JsonConvert.SerializeObject(request);
                var content = new StringContent(jsonObject, Encoding.UTF8, "application/json");
                System.Net.ServicePointManager.ServerCertificateValidationCallback = delegate (object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors) { return true; };
                result = client.PostAsync(apiUrl, content).Result;

                var dataObjects = result.Content.ReadAsStringAsync().Result;
                var resultado = JsonConvert.DeserializeObject<OficinaResponse>(dataObjects);

                return Json(resultado);
            }
        }

        [HttpPost]
        public ActionResult ActualizarEntregaNoCorreo(ActualizarEmpresaRequest request)
        {
            string apiBaseUrl = ConfigurationManager.AppSettings.Get("UrlAPI").ToString();
            string apiUrl = apiBaseUrl + "api/EmpresaMod/ActualizarEntrega";
            var result = new HttpResponseMessage();
            using (var client = new HttpClient())
            {
                var jsonObject = JsonConvert.SerializeObject(request);
                var content = new StringContent(jsonObject, Encoding.UTF8, "application/json");
                System.Net.ServicePointManager.ServerCertificateValidationCallback = delegate (object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors) { return true; };
                result = client.PostAsync(apiUrl, content).Result;

                var dataObjects = result.Content.ReadAsStringAsync().Result;
                var resultado = JsonConvert.DeserializeObject<ActualizarEmpresaResponse>(dataObjects);

                return Json(resultado);
            }
        }

        [HttpPost]
        public ActionResult ObtenerDatoEntrega(ObtenerDatoEntregaRequest request)
        {
            string apiBaseUrl = ConfigurationManager.AppSettings.Get("UrlAPI").ToString();
            string apiUrl = apiBaseUrl + "api/EmpresaMod/ObtenerDatoEntrega";
            var result = new HttpResponseMessage();
            using (var client = new HttpClient())
            {
                var jsonObject = JsonConvert.SerializeObject(request);
                var content = new StringContent(jsonObject, Encoding.UTF8, "application/json");
                System.Net.ServicePointManager.ServerCertificateValidationCallback = delegate (object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors) { return true; };
                result = client.PostAsync(apiUrl, content).Result;

                var dataObjects = result.Content.ReadAsStringAsync().Result;
                var resultado = JsonConvert.DeserializeObject<ObtenerDatoEntregaResponse>(dataObjects);

                return Json(resultado);
            }
        }

        [HttpPost]
        public ActionResult ActualizarEntrega(ActualizarEmpresaRequest request)
        {
            string apiBaseUrl = ConfigurationManager.AppSettings.Get("UrlAPI").ToString();
            string apiUrl = apiBaseUrl + "api/EmpresaMod/ActualizarEntrega";
            var result = new HttpResponseMessage();
            using (var client = new HttpClient())
            {
                var jsonObject = JsonConvert.SerializeObject(request);
                var content = new StringContent(jsonObject, Encoding.UTF8, "application/json");
                System.Net.ServicePointManager.ServerCertificateValidationCallback = delegate (object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors) { return true; };
                result = client.PostAsync(apiUrl, content).Result;

                var dataObjects = result.Content.ReadAsStringAsync().Result;
                var resultado = JsonConvert.DeserializeObject<ActualizarEmpresaResponse>(dataObjects);

                if (resultado.result) {
                    GetToken(request.codigoregistro); 
                }

                return Json(resultado);
            }
        }

        public void GetToken(int codigoregistro) {
            string apiBaseUrl = ConfigurationManager.AppSettings.Get("UrlAPI").ToString();
            string apiUrl = apiBaseUrl + "api/EmpresaMod/ObtenerRegistro";
            var result = new HttpResponseMessage();
            using (var client = new HttpClient())
            {
                var jsonObject = JsonConvert.SerializeObject(new ObtenerRegistroRequest { codigoregistro = codigoregistro});
                var content = new StringContent(jsonObject, Encoding.UTF8, "application/json");
                System.Net.ServicePointManager.ServerCertificateValidationCallback = delegate (object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors) { return true; };
                result = client.PostAsync(apiUrl, content).Result;

                var dataObjects = result.Content.ReadAsStringAsync().Result;
                var resultado = JsonConvert.DeserializeObject<ObtenerRegistroResponse>(dataObjects);

                if (resultado.result)
                {
                    EnviarCorreo(codigoregistro, resultado.data);
                }
            }

        }

        public void EnviarCorreo(int id, ObtenerRegistroResult data)
        {

            var request = new CorreoRequest
            {
                To = data.emailadministrador,
                Bcc = ConfigurationManager.AppSettings.Get("BCC").ToString(),
                Asunto = "MODIFICACIÓN BXI BANBIF - " + data.razonsocial,
                From = "BanBif - Banca por Internet Empresas <activacionBxI@banbif.com.pe>",
                Mensaje = CuerpoHTML(id, data.tokenvalidacion)
            };

            string apiBaseUrl = ConfigurationManager.AppSettings.Get("UrlAPI").ToString();
            string apiUrl = apiBaseUrl + "api/EmpresaMod/EnviarCorreo";
            var result = new HttpResponseMessage();

            using (var client = new HttpClient())
            {
                var jsonObject = JsonConvert.SerializeObject(request);
                var content = new StringContent(jsonObject, Encoding.UTF8, "application/json");
                result = client.PostAsync(apiUrl, content).Result;

                var dataObjects = "";

                dataObjects = result.Content.ReadAsStringAsync().Result;
                var response = JsonConvert.DeserializeObject<bool>(dataObjects);
            }
        }

        public string CuerpoHTML(int id, string token)
        {

            var rutaapp = ConfigurationManager.AppSettings.Get("UrlAPP").ToString();
            var html = "";
            html += "<!DOCTYPE html>";
            html += "<html lang='es'>";
            html += "<head>";
            html += "<meta charset='utf-8'>";
            html += "<title>Documento</title>";
            html += "</head>";
            html += "<body>";
            html += "<h2>Banca por Internet Empresas - BanBif</h2>";
            html += "<hr/>";
            html += "<p><b>Bienvenido</b></p>";
            html += "<p>Podrá acceder a su Solicitud de Modificación <a href='" + rutaapp + "/resumen?sol=" + id + "&token=" + token + "'>aquí</a></p>";
            html += "<p>1.Te avisaremos cuando tus tokens y claves estén listos para que los recojas en la oficina seleccionada <br> 2.Recuerda llevar la solicitud original firmada y sellada por los representantes legales <br> 3.Únicamente se entregarán los tokens y claves a las personas autorizadas en la solicitud </p>";
            html += "<hr/>";

            html += "</body>";
            html += "</html>";
            return html;
        }
    }
}