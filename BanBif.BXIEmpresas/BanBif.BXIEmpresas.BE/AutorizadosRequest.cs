using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BanBif.BXIEmpresas.BE
{
    public class AutorizadosRequest
    {
        public string nombre { get; set; }
        public int tipodocumento { get; set; }
        public string documento { get; set; }
        public string telefono { get; set; }

    }
}
