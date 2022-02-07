using BanBif.BXIEmpresas.BE;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BanBif.BXIEmpresas.DA
{
    public class Canales_ProveedorDA
    {
        public ProveedorResponse BuscarPorRuc(ProveedorRequest request) {
            using (var db = new canalesEntities()) {
                var result = new ProveedorResponse();
                var proveedor = db.FAEL_PROVEEDORES.Where(p => p.RUC == request.ruc).FirstOrDefault();
                result.result = false;
                if (proveedor != null) {
                    result.data = new ProveedorResult { proveedor = proveedor.PROVEEDOR };
                    result.result = true;
                }

                return result;
            }
        }
    }
}
