﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abundance_SN.Model.Model
{
    public class Vendors
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public bool Active { get; set; }
        public DateTime? CreateedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
