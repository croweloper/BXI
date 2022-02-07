using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BanBif.BXIEmpresas.BE
{
    public class ActualizarEmpresaRequest
    {
        public int codigoregistro { get; set; }
        public int version { get; set; }
        public string monedacargo { get; set; }
        public string cuentacargo { get; set; }
        public string entidad { get; set; }
        public int firmantes { get; set; }
        public int tokenfisico { get; set; }
        public int tokendigital { get; set; }
        public string tokenadmin { get; set; }
        public string dniadministrador { get; set; }
        public string nombreadministrador { get; set; }
        public string emailadministrador { get; set; }
        public string telefonoadministrador { get; set; }
        public int estado { get; set; }
        /*DATOS ENTREGA*/
        public int codigooficina { get; set; }
        public List<AutorizadosRequest> autorizados { get; set; }

        public List<TokenRequest> reposicion { get; set; }
        public List<TokenRequest> nuevaclave { get; set; }
        public List<TokenRequest> anulacion { get; set; }
    }
}
