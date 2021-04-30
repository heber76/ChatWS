using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ChatWeb.Models.ViewModels
{
    public class RegisterViewModel
    {
        [Required]
        [EmailAddress]
        [StringLength(50)]
        [Display(Name="Correo Eectronico")]
        public string Email { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "Contraseña")]
        public string Password { get; set; }

        
        [Compare("Password")]
        [Display(Name="Confirmar Conatraseña")]
        public string Password2 { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name="Nombre")]
        public string Name { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "Ciudad")]
        public string City { get; set; }

        

    }
}