﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abundance_SN.Model.Entity;
using Abundance_SN.Model.Model;
using Abundance_SN.Model.Translator;

namespace Abundance_SN.Business
{
    public class RolesLogic : BusinessBaseLogic<Roles,ROLES>
    {
        public RolesLogic()
        {
            translator = new RolesTranslator();
        }
    }
}