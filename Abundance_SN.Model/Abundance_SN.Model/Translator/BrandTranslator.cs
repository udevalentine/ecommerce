using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abundance_SN.Model.Entity;
using Abundance_SN.Model.Model;

namespace Abundance_SN.Model.Translator
{
    public class BrandTranslator : TranslatorBase<Brand,BRAND>
    {
        public override Brand TranslateToModel(BRAND entity)
        {
            try
            {
                Brand model = null;
                if (entity != null)
                {
                    model = new Brand();
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

        public override BRAND TranslateToEntity(Brand model)
        {
            try
            {
                BRAND entity = null;
                if (model != null)
                {
                    entity = new BRAND();
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
