using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Ingenio.PortalWebExterno.Models
{
    public class user
    {
        public string Name { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }

        public string ConfirmPassword { get; set; }

        public string Address { get; set; }

        public string Contact { get; set; }

        public bool Terms { get; set; }
    }
}