namespace GestaoDemanda_Master.Models
{
    public class Demanda
    {
        public int DemandaId { get; set; }
        public string? DescricaoDemanda { get; set; }
        public DateTime DataCriacao { get; set; }
        public DateTime? DataLiberacao { get; set; }
        public DateTime? DataEntrega { get; set; }
        public string? ResponsavelId { get; set; }
        public Responsavel? Responsavel { get; set; }
        public int ClienteId { get; set; }
        public Cliente? Cliente { get; set; }
        public int StatusDemandId { get; set; }
        public StatusDemanda? Status { get; set; }
        public string? LinkReferencia { get; set; }
        public int PrazoDias { get; set; }
        public double? TotalHorasTrabalhadas
        {
            get
            {
                if(DataLiberacao.HasValue && DataEntrega.HasValue)
                {
                    return (DataEntrega.Value - DataLiberacao.Value).TotalHours;
                }
                return null;
            }
        }
        public int? DiasRestantesEntrega
        {
            get
            {
                if (DataEntrega.HasValue)
                {
                    return (DataEntrega.Value - DateTime.Now).Days;
                }
                return null;
            }
        }
        public void CalcularDataEntrega()
        {
            if (DataLiberacao.HasValue)
            {
                DataEntrega = DataLiberacao.Value.AddDays(PrazoDias);
            }
        }
    }
}
