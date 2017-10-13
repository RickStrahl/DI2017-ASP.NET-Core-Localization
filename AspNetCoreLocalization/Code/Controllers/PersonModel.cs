using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCoreLocalization.Code.Controllers
{
    public class PersonModel
    {
        
        [Required(ErrorMessage = "Name Required")]
        public string Name { get; set; }

        [Required(ErrorMessage ="EmailRequired")]
        public string Email { get; set; }
       
        [Required(ErrorMessage ="AgeRequired")]
        [Range(18,110,ErrorMessage ="Age must be between 18 and 110")]
        public string Age { get; set; }


        public bool ReceiveEmail { get; set; }

    }
}
