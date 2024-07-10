namespace BlazorAdminApp.Models
{
    public class Bitacora
    {
        public Guid Id { get; set; }
        public string Metodo { get; set; }
        public string Parametros { get; set; }
        public string Error { get; set; }
        public string Mensaje { get; set; }
        public DateTime Fecha { get; set; }
        public int EquipoId { get; set; }
        public string Equipo { get; set; }
        public Guid UsuarioId { get; set; }
        public string Usuario { get; set; }
    }
}
