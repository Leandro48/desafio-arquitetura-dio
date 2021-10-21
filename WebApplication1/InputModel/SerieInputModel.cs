using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ApiCatalogoNetflix.InputModel
{
    public class SerieInputModel
    {
        [Required]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "A série deve conter entre 3 e 100 caracteres")]
        public string Nome { get; set; }
        [Required]
        [StringLength(100, MinimumLength = 1, ErrorMessage = "O nome da produtora deve conter entre 3 e 100 caracteres")]
        public string Produtora { get; set; }
        
        public double Nota { get; set; }
    }
}
