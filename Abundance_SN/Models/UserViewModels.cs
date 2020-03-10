using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Abundance_SN.Model.Model;

namespace Abundance_SN.Models
{
    public class UserViewModels
    {
        public List<Role> Roles { get; set; }
        public Role Role { get; set; }
    }
}