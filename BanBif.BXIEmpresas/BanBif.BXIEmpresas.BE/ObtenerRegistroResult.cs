using System.Collections.Generic;

namespace BanBif.BXIEmpresas.BE
{
    public class ObtenerRegistroResult
    {
        public string ruc { get; set; }
        public string razonsocial { get; set; }
        public string direccion { get; set; }
        public string telefono { get; set; }
        public string entidad { get; set; }
        public string administrador { get; set; }
        public string dniadministrador { get; set; }
        public string nombreadministrador { get; set; }
        public string emailadministrador { get; set; }
        public string telefonoadministrador { get; set; }
        public string version { get; set; }
        public string cuentacargo { get; set; }
        public string moneda { get; set; }
        public int tokendigitales { get; set; }
        public int tokenfisico { get; set; }
        public bool tokenadmin { get; set; }
        public int costo { get; set; }
        public string direccionentrega { get; set; }
        public string oficinaentrega { get; set; }
        public int estado { get; set; }
        public string tokenvalidacion { get; set; }
        public List<Autorizado> autorizados { get; set; }
        public List<Representante> representantes { get; set; }
        public List<TokenRequest> reposicion { get; set; }
        public List<TokenRequest> nuevaclave { get; set; }
        public List<TokenRequest> anulacion { get; set; }
    }
}
