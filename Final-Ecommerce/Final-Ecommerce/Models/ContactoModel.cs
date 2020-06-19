using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Final_Ecommerce.Models
{
    public class ContactoModel
    {
        [Required]
        public int id { get; set; }
        [Required]
        [Display(Name = "Nombre*")]
        [StringLength(30, ErrorMessage = "El número de caracteres de {0} debe ser al menos {2}.", MinimumLength = 2)]
        public string nombre { get; set; }
        [Required]
        [Display(Name = "Correo electrónico*")]
        [EmailAddress]
        public string email { get; set; }
        [Required]
        [Display(Name = "Mensaje*")]
        [StringLength(100, ErrorMessage = "El número de caracteres de {0} debe ser al menos {2}.", MinimumLength = 20)]
        public string mensaje { get; set; }
        [Display(Name = "Teléfono")]
        [StringLength(13, ErrorMessage = "El número de dígitos de {0} debe ser al menos {2}.", MinimumLength = 10)]
        public string telefono { get; set; }
    }
}