﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abundance_SN.Model.Model
{
    public class UnitMeasurement
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Active { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}