using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ProyectoFinal_Ecommerce.Models
{
    public class UsuarioModel
    {
        [Required]
        public int id { get; set; }

        [Required]
        [StringLength(30, ErrorMessage = "El número de caracteres de {0} debe ser al menos {2}.", MinimumLength = 2)]
        [Display(Name = "Nombre")]
        public string nombre { get; set; }

        [Required]
        [StringLength(30, ErrorMessage = "El número de caracteres de {0} debe ser al menos {2}.", MinimumLength = 2)]
        [Display(Name = "Apellido Paterno")]
        public string apellido_paterno { get; set; }

        [Required]
        [StringLength(30, ErrorMessage = "El número de caracteres de {0} debe ser al menos {2}.", MinimumLength = 2)]
        [Display(Name = "Apellido Materno")]
        public string apellido_materno { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "Correo electrónico")]
        [StringLength(50, ErrorMessage = "El número de caracteres de {0} debe ser al menos {2}.", MinimumLength = 6)]
        public string correo { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Contraseña")]
        public string pass { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Confirmar contraseña")]
        [Compare("pass", ErrorMessage = "La contraseña y la contraseña de confirmación no coinciden.")]
        public string confpass { get; set; }
        public int stat { get; set; }

        [Display(Name ="Rol")]
        public int role_id { get; set; }
        
        [Required]
        [StringLength(20, ErrorMessage = "El número de caracteres de {0} debe ser al menos {2}.", MinimumLength = 6)]
        [Display(Name = "Nombre de usuario")]
        public string username { get; set; }

    }

    public class UsuarioModelEdit
    {
        [Required]
        public int id { get; set; }

        [Required]
        [StringLength(30, ErrorMessage = "El número de caracteres de {0} debe ser al menos {2}.", MinimumLength = 2)]
        [Display(Name = "Nombre")]
        public string nombre { get; set; }

        [Required]
        [StringLength(30, ErrorMessage = "El número de caracteres de {0} debe ser al menos {2}.", MinimumLength = 2)]
        [Display(Name = "Apellido Paterno")]
        public string apellido_paterno { get; set; }

        [Required]
        [StringLength(30, ErrorMessage = "El número de caracteres de {0} debe ser al menos {2}.", MinimumLength = 2)]
        [Display(Name = "Apellido Materno")]
        public string apellido_materno { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "Correo electrónico")]
        [StringLength(50, ErrorMessage = "El número de caracteres de {0} debe ser al menos {2}.", MinimumLength = 6)]
        public string correo { get; set; }

        [Required]
        [StringLength(20, ErrorMessage = "El número de caracteres de {0} debe ser al menos {2}.", MinimumLength = 6)]
        [Display(Name = "Nombre de usuario")]
        public string username { get; set; }

        [Required]
        [Display(Name = "Rol")]
        public int role_id { get; set; }
    }

}