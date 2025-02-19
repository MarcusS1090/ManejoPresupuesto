namespace ManejoPresupuesto.Models
{
    public class ReporteTransaccionesDetalladas
    {
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
        public IEnumerable<TransaccionesPorFecha> TransaccionAgrupadas { get; set; }
        public decimal BalanceDepositos => TransaccionAgrupadas.Sum(x => x.BalanceDepositos);
        public decimal BalanceRetiros => TransaccionAgrupadas.Sum(x => x.BalanceRetiros);

        public decimal Total => BalanceDepositos - BalanceRetiros;

        public class TransaccionesPorFecha
        {
            public DateTime FechaTransaccion { get; set; }
            public IEnumerable<Transaccion> Transacciones { get; set; }
            public decimal BalanceDepositos => 
                Transacciones.Where(x => x.TipoOperacionId == TipoOperacion.Ingreso)
                .Sum(x => x.Monto);
            public decimal BalanceRetiros => 
                Transacciones.Where(x => x.TipoOperacionId == TipoOperacion.Gasto)
                .Sum(x => x.Monto);
        }
    }
}
