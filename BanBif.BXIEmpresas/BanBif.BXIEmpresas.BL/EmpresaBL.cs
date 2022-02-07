using BanBif.BXIEmpresas.BE;
using BanBif.BXIEmpresas.DA;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace BanBif.BXIEmpresas.BL
{
    public class EmpresaBL
    {
        public RegistrarEmpresaResponse Registrar(RegistrarEmpresaRequest request) {
            var response = new RegistrarEmpresaResponse();
            var da = new Panel_BXIEmpresaDA();

            response.data = da.Registrar(request);

            if (response.data != null && response.data.codigoregistro > 0)
            {
                response.result = true;
            }
            else {
                response.result = false;
            }

            return response;
        }

        public ActualizarEmpresaResponse Actualizar(ActualizarEmpresaRequest request) {
            var da = new Panel_BXIEmpresaDA();
            return  da.Actualizar(request);
        }

        public ActualizarEmpresaResponse ActualizarDatoEntrega(ActualizarEmpresaRequest request)
        {
            var da = new Panel_BXIEmpresaDA();
            return da.ActualizarDatoEntrega(request);
        }

        public DatosResponse ObtenerDatos(DatosRequest request) {
            var da = new Panel_BXIEmpresaDA();
            return da.ObtenerDatos(request);
        }

        public OficinaResponse ListarOficinas(OficinasRequest request) {
            var da = new Panel_BXIEmpresaDA();
            return da.ListarOficinas(request);
        }

        public ObtenerRegistroResponse ObtenerRegistro(ObtenerRegistroRequest request) {
            var da = new Panel_BXIEmpresaDA();
            return da.ObtenerRegistro(request);
        }

        public ObtenerRegistroResponse ObtenerRepresentantes(ObtenerRegistroRequest request)
        {
            var da = new Panel_BXIEmpresaDA();
            return da.ObtenerRepresentantes(request);
        }

        public ValidacionTokenResponse VerificarToken(ValidacionTokenRequest request) {
            var da = new Panel_BXIEmpresaDA();
            return da.VerificarToken(request);
        }

        public bool EnviarCorreo(CorreoRequest request)
        {

            //string apiUrl = "https://wsinnovaapp.bif1innova10.com/api/Correo/EnviarCorreo";
            string apiUrl = "http://10.200.101.89:8084/api/Correo/EnviarCorreo";
            var result = new HttpResponseMessage();

            using (var client = new HttpClient())
            {
                System.Net.ServicePointManager.ServerCertificateValidationCallback = delegate (object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors) { return true; };
                var jsonObject = JsonConvert.SerializeObject(request);
                var content = new StringContent(jsonObject, Encoding.UTF8, "application/json");
                result = client.PostAsync(apiUrl, content).Result;

                var dataObjects = "";

                dataObjects = result.Content.ReadAsStringAsync().Result;
                var response = JsonConvert.DeserializeObject<CorreoResponse>(dataObjects);
                return response.Enviado;
            }
        }

        public ObtenerConfiguracionResponse ObtenerConfiguracion(ObtenerConfiguracionRequest request) {
            var da = new Panel_BXIEmpresaDA();
            return da.ObtenerConfiguracion(request);
        }

        public ObtenerDatoEntregaResponse ObtenerDatoEntrega(ObtenerDatoEntregaRequest request)
        {
            var da = new Panel_BXIEmpresaDA();
            return da.ObtenerDatoEntrega(request);
        }

    }
}
