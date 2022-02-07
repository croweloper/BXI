namespace BanBif.BXIEmpresas.BE
{
    public class ObtenerConfiguracionResult
    {
        public string entidad { get; set; }
        public string administrador { get; set; }
        public string dniadministrador { get; set; }
        public string nombreadministrador { get; set; }
        public string emailadministrador { get; set; }
        public string telefonoadministrador { get; set; }
        public string version { get; set; }
        public string cuentacargo { get; set; }
        public string moneda { get; set; }        
        public int tokenfisico { get; set; }       
    }
}
