using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCoreLocalization.Code.ViewModels
{
    public class PersonModel
    {
        
        [Required(ErrorMessage = "NameRequired")]
        public string Name { get; set; }

        [Required(ErrorMessage ="EmailRequired")]
        public string Email { get; set; }
       
        [Required(ErrorMessage ="AgeRequired")]
        [Range(18,110,ErrorMessage ="AgeRange")]
        public string Age { get; set; }


        public bool ReceiveEmail { get; set; }

        public bool HasErrors { get; set; }

    }
}
