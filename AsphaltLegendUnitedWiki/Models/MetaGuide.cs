using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AsphaltLegendUnitedWiki.Models
{
    public class MetaGuide
    {
        public int Id { get; set; }

        public int? AutoId { get; set; }
        [ForeignKey("AutoId")]
        public virtual Auto Auto { get; set; }

        public int? PistaId { get; set; }
        [ForeignKey("PistaId")]
        public virtual Pista Pista { get; set; }

        [Range(1, 5)]
        public byte RecomendacionNivel { get; set; }

        public string NotaTecnica { get; set; }

        public int? AutorId { get; set; }
        [ForeignKey("AutorId")]
        public virtual Usuario Autor { get; set; }

        public DateTime FechaActualizacion { get; set; } = DateTime.Now;
    }
}
