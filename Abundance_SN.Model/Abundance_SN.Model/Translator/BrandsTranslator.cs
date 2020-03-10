using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abundance_SN.Model.Entity;
using Abundance_SN.Model.Model;

namespace Abundance_SN.Model.Translator
{
    public class BrandsTranslator : TranslatorBase<Brands,BRANDS>
    {
        public override Brands TranslateToModel(BRANDS entity)
        {
            try
            {
                Brands model = null;
                if (entity != null)
                {
                    model = new Brands();
                    model.Id = entity.Id;
                    model.Name = entity.Name;
                    model.Description = entity.Description;
                    model.Active = entity.Active;
                    model.CreatedAt = entity.Created_At;
                    model.UpdatedAt = entity.Updated_At;
                   
                }
                return model;

            }
            catch (Exception ex)
            {
                
                throw;
            }
        }

        public override BRANDS TranslateToEntity(Brands model)
        {
            try
            {
                BRANDS entity = null;
                if (model != null)
                {
                    entity = new BRANDS();
                    entity.Id = model.Id;
                    entity.Name = model.Name;
                    entity.Description = model.Description;
                    entity.Active = model.Active;
                    entity.Created_At = model.CreatedAt;
                    entity.Updated_At = model.UpdatedAt;
                }
                return entity;
            }
            catch (Exception ex)
            {
                
                throw;
            }
        }
    }
}
