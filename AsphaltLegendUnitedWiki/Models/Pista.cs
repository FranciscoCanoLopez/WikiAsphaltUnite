using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AsphaltLegendUnitedWiki.Models
{
    public class Pista
    {
        public int Id { get; set; }

        [Required, StringLength(100)]
        public string NombrePista { get; set; }

        public int? UbicacionId { get; set; }
        [ForeignKey("UbicacionId")]
        public virtual Ubicacion Ubicacion { get; set; }

        [Column(TypeName = "decimal(5,2)")]
        public decimal Longitud { get; set; }

        [Range(1, 5)]
        public byte Dificultad { get; set; }

        public int? RecordGlobalMs { get; set; }

        public virtual ICollection<MetaGuide> MetaGuides { get; set; }
    }
}
