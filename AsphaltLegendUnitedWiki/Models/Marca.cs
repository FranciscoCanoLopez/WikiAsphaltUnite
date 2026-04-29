using System.ComponentModel.DataAnnotations;

namespace AsphaltLegendUnitedWiki.Models
{
    public class Marca
    {
        public int Id { get; set; }

        [Required, StringLength(50)]
        public string NombreMarca { get; set; }

        // Relación: Una marca tiene muchos autos
        public virtual ICollection<Auto> Autos { get; set; }
    }
}
