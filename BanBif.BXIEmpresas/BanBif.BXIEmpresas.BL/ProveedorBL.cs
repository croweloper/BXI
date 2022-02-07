using BanBif.BXIEmpresas.BE;
using BanBif.BXIEmpresas.DA;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BanBif.BXIEmpresas.BL
{
    public class ProveedorBL
    {
        public ProveedorResponse BuscarPorRuc(ProveedorRequest request) {
            var response = new ProveedorResponse();
            var da = new Canales_ProveedorDA();

            response = da.BuscarPorRuc(request);

            return response;
        }
    }
}
