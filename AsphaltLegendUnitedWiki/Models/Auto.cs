using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AsphaltLegendUnitedWiki.Models
{
    public class Auto
    {
        public int Id { get; set; }

        [Required, StringLength(100)]
        public string Nombre { get; set; }

        public int? MarcaId { get; set; }
        [ForeignKey("MarcaId")]
        public virtual Marca Marca { get; set; }

        [Required]
        public ClaseAuto Clase { get; set; }

        public byte EstrellasBase { get; set; } = 1;

        [Required]
        public byte EstrellasMax { get; set; }

        public int? RankMin { get; set; }
        public int? RankMax { get; set; }

        public TipoManejo? TipoManejo { get; set; }

        [StringLength(255)]
        public string FuenteObtencion { get; set; }

        public virtual ICollection<MetaGuide> MetaGuides { get; set; }
    }
}
