namespace BlazorAdminApp.Models
{
    public class Venta
    {
        public int Cantidad { get; set; }
        public string TipoVenta { get; set; }
        public double ImporteTotal { get; set; }
        public double Saldo { get; set; }
    }

    public class Pago
    {
        public int Cantidad { get; set; }
        public double ImporteTotal { get; set; }
        public string MetodoPago { get; set; }
    }

    public class Gasto
    {
        public string Usuario { get; set; }
        public string Descripcion { get; set; }
        public double Importe { get; set; }
        public DateTime Fecha { get; set; }
    }

    public class Deposito
    {
        public string Usuario { get; set; }
        public string Descripcion { get; set; }
        public double Importe { get; set; }
        public DateTime Fecha { get; set; }
    }

    public class Indicador
    {
        public double Cantidad { get; set; }
        public double ImporteTotal { get; set; }
        public double Ganancia { get; set; }
        public string ArticuloTipo { get; set; }
    }

    public class Corte
    {
        public string Equipo { get; set; }
        public double OtraFormaPago { get; set; }
        public double EfectivoEnCaja { get; set; }
        public string Cajero { get; set; }
    }

    public class CorteCajaResponse
    {
        public int EquipoId { get; set; }
        public DateTime Desde { get; set; }
        public DateTime Hasta { get; set; }
        public List<Venta> Ventas { get; set; }
        public List<Pago> Pagos { get; set; }
        public List<Gasto> Gastos { get; set; }
        public List<Deposito> Depositos { get; set; }
        public object Anticipos { get; set; }
        public List<Indicador> Indicadores { get; set; }
        public List<object> Compras { get; set; }
        public List<Corte> Corte { get; set; }
        public bool Imprimir { get; set; }
    }

    public class CorteCajaRequest
    {
        public int EquipoId { get; set; }
        public DateTime FechaDesde { get; set; }
        public DateTime FechaHasta { get; set; }
        public string UsuarioId { get; set; }
        public string ClaveSucursal { get; set; }
    }
}
