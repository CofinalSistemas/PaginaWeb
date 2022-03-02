using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Ingenio.PortalWebExterno.Models
{
    public class SubscribeModel
    {
        //model specific fields 
        [Required]
        [Display(Name = "Cuánto es:")]
        public string Captcha { get; set; }
    }
}