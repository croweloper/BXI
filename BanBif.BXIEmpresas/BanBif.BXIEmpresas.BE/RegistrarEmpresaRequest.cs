using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BanBif.BXIEmpresas.BE
{
    public class RegistrarEmpresaRequest
    {
        public int codigoregistro { get; set; }
        public string ruc { get; set; }
        public string razonsocial { get; set; }
        public string telefono { get; set; }
        public string direccion { get; set; }
        public string ip { get; set; }
        public List<RepresentanteRequest> Representante { get; set; }
    }
}
