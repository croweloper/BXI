//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace BanBif.BXIEmpresas.DA
{
    using System;
    using System.Collections.Generic;
    
    public partial class BPI_EMPRESAS_representante
    {
        public int idrepresentante { get; set; }
        public Nullable<int> idregistro { get; set; }
        public string nombre { get; set; }
        public Nullable<int> tipo_documento { get; set; }
        public string documento { get; set; }
        public Nullable<System.DateTime> f_registro { get; set; }
        public Nullable<int> status { get; set; }
    }
}