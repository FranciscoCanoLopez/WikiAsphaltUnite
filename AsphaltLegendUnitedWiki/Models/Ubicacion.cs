using System.ComponentModel.DataAnnotations;

namespace AsphaltLegendUnitedWiki.Models
{
    public class Ubicacion
    {
        public int Id { get; set; }

        [Required, StringLength(100)]
        public string NombreCiudad { get; set; }

        public virtual ICollection<Pista> Pistas { get; set; }
    }
}
