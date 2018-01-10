using System.ComponentModel.DataAnnotations;

namespace FluentNHibernateIdentity.Example.Models
{
    public class ExternalLoginConfirmationViewModel
    {
        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }
}
