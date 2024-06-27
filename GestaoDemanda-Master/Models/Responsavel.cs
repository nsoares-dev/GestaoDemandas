using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace GestaoDemanda_Master.Models
{
    public class Responsavel
    {
        public int ResponsavelId { get; set; }
        [DisplayName("Nome")]
        public string? NomeResponsavel { get; set; }
        [DisplayName("Descrição")]
        public string? ApresentacaoResponsavel { get; set; }
        public int NivelId { get; set; }
        public Nivel? Nivel { get; set; }
        public string? FotoResponsavel { get; set; }
    }
}
