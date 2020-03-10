using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Abundance_SN.Model.Entity;
using Abundance_SN.Model.Model;

namespace Abundance_SN.Model.Translator
{
    public class RolesTranslator : TranslatorBase<Role,ROLE>
    {
        public override Role TranslateToModel(ROLE entity)
        {
            try
            {
                Role model = null;
                if (entity != null)
                {
                    model = new Role();
                    model.Id = entity.Id;
                    model.Name = entity.Name;
                    model.Active = entity.Active;
                }
                return model;

            }
            catch (Exception ex)
            {
                
                throw ex;
            }
        }

        public override ROLE TranslateToEntity(Role model)
        {
            try
            {
                ROLE entity = null;
                if (model != null)
                {
                    entity = new ROLE();
                    entity.Id = model.Id;
                    entity.Name = model.Name;
                    entity.Active = model.Active;
                    

                }
                return entity;
            }
            catch (Exception ex)
            {
                
                throw ex;
            }
        }
    }
}
