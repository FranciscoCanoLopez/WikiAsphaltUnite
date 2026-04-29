using System.ComponentModel.DataAnnotations;

namespace AsphaltLegendUnitedWiki.Models
{
    public class Usuario
    {
        public int Id { get; set; }

        [Required, StringLength(50)]
        public string? nombre_usuario { get; set; }

        [Required, EmailAddress, StringLength(100)]
        public string? email { get; set; }

        [Required]
        public string? password_hash { get; set; }

        public RolUsuario rol { get; set; } = RolUsuario.Usuario;

        public DateTime fecha_registro { get; set; } = DateTime.Now;

        // Relación inversa con MetaGuide
        public virtual ICollection<MetaGuide>? GuiasCreadas { get; set; }    
    }
}
