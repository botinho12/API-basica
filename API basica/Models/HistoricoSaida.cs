namespace API_basica.Models
{
    public class HistoricoSaida
    {
        public int Id { get; set; }
        public int ProdutoId { get; set; }
        public int Quantidade { get; set; }
        public DateTime DataSaida { get; set; }

    }
}
