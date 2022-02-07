﻿using BanBif.BXIEmpresas.BE;
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
    public class GraciasController : Controller
    {
        // GET: Gracias
        public ActionResult Index()
        {
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