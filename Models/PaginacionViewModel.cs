namespace ManejoPresupuesto.Models
{
    public class PaginacionViewModel
    {
        public int Pagina { get; set; } = 1;
        public int recordsPorPagina = 5;
        private readonly int cantidadMaximaPorPaginas = 50;

        public int RecordsPorPagina
        {
            get {

                return recordsPorPagina;
            }
            set
            {
                recordsPorPagina = (value > cantidadMaximaPorPaginas) ? cantidadMaximaPorPaginas : value;
            }
        }
        public int RecordsASaltar => RecordsPorPagina * (Pagina - 1) ;
    }
}
