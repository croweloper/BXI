using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BanBif.BXIEmpresas.BE
{
    public class DatosResponse
    {
        public bool result { get; set; }
        public List<DatosResult> data { get; set; }
    }
}
