using BanBif.BXIEmpresas.BE;
using BanBif.BXIEmpresas.DA;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BanBif.BXIEmpresas.BL
{
    public class EmpresaModBL
    {
        public RegistrarEmpresaResponse Registrar(RegistrarEmpresaRequest request)
        {
            var response = new RegistrarEmpresaResponse();
            var da = new Panel_BXIEmpresaMOD_DA();

            response.data = da.Registrar(request);

            if (response.data != null && response.data.codigoregistro > 0)
            {
                response.result = true;
            }
            else
            {
                response.result = false;
            }

            return response;
        }

        public ActualizarEmpresaResponse Actualizar(ActualizarEmpresaRequest request)
        {
            var da = new Panel_BXIEmpresaMOD_DA();
            return da.Actualizar(request);
        }

        public ActualizarEmpresaResponse ActualizarDatoEntrega(ActualizarEmpresaRequest request)
        {
            var da = new Panel_BXIEmpresaMOD_DA();
            return da.ActualizarDatoEntrega(request);
        }

        public ObtenerRegistroResponse ObtenerRegistro(ObtenerRegistroRequest request)
        {
            var da = new Panel_BXIEmpresaMOD_DA();
            return da.ObtenerRegistro(request);
        }

        public ValidacionTokenResponse VerificarToken(ValidacionTokenRequest request)
        {
            var da = new Panel_BXIEmpresaMOD_DA();
            return da.VerificarToken(request);
        }
        public ObtenerRegistroResponse ObtenerHome(ObtenerRegistroRequest request) {
            var da = new Panel_BXIEmpresaMOD_DA();
            return da.ObtenerHome(request);
        }

        public ObtenerRegistroResponse ObtenerToken(ObtenerRegistroRequest request) {
            var da = new Panel_BXIEmpresaMOD_DA();
            return da.ObtenerToken(request);
        }

        public ObtenerDatoEntregaResponse ObtenerDatoEntrega(ObtenerDatoEntregaRequest request) {
            var da = new Panel_BXIEmpresaMOD_DA();
            return da.ObtenerDatoEntrega(request);
        }

    }
}
