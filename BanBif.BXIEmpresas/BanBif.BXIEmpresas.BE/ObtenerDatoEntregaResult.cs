using System.Collections.Generic;

namespace BanBif.BXIEmpresas.BE
{
    public class ObtenerDatoEntregaResult
    {
        public string dniadministrador { get; set; }
        public string nombreadministrador { get; set; }
        public string emailadministrador { get; set; }
        public string telefonoadministrador { get; set; }
        public int estado { get; set; }
        /*DATOS ENTREGA*/
        public string region { get; set; }
        public int codigooficina { get; set; }
        public string direccion { get; set; }
        public List<AutorizadosRequest> autorizados { get; set; }

    }
}
