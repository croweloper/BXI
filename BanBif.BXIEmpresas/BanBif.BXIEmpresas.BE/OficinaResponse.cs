using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BanBif.BXIEmpresas.BE
{
    public class OficinaResponse
    {
        public bool result { get; set; }
        public List<OficinasResult> data { get; set; }
    }
}
