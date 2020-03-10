using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abundance_SN.Model.Model
{
    public class Person
    {
        public long Id { get; set; }
        public string Surname { get; set; }
        public string FirstName { get; set; }
        public string OtherName { get; set; }

        [Display(Name = "Email")]
        [RegularExpression("^[a-zA-Z0-9_\\.-]+@([a-zA-Z0-9-]+\\.)+[a-zA-Z]{2,6}$", ErrorMessage = "Email is not valid")]
        public string Email { get; set; }
        [Required]
        [Display(Name = "Mobile Phone")]
        [RegularExpression("^0[0-9]{10}$", ErrorMessage = "Phone number is not valid")]
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public int? CountryId { get; set; }
        public string StateId { get; set; }
        public DateTime? DateRegistered { get; set; }
        public User User { get; set; }
        public string LandMark { get; set; }
        public string City { get; set; }
        public string Uuid { get; set; }
      
    }
}
