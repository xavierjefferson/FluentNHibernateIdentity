using System.ComponentModel.DataAnnotations;

namespace FluentNHibernateIdentity.Example.Models
{
    public class ForgotPasswordViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }
}