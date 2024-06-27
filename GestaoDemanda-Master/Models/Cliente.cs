namespace GestaoDemanda_Master.Models
{
    public class Cliente
    {
        public int ClienteId { get; set; }
        public string? NomeCliente { get; set; }
        public string? EmailCliente { get; set; }
        public int DemandaId { get; set; }
        public Demanda? Demanda { get; set; }
        public string? FotoCliente { get; set; }
    }
}
