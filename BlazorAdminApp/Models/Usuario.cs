namespace BlazorAdminApp.Models
{
    public class Usuario
    {
        public string Id { get; set; }
        public string Nombre { get; set; }
        public string LlaveSucursal { get; set; }
        public string LlaveSucursal_Seleccionada { get; set; }
        public string NombreSucursal { get; set; }
        public string IdSession { get; set; }
        public string Logo { get; set; }
        public string Mensaje { get; set; }
        public int StatusCode { get; set; }
    }
}