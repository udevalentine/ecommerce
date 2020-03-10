using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abundance_SN.Model.Entity;
using Abundance_SN.Model.Model;

namespace Abundance_SN.Model.Translator
{
   public  class CategoryTranslator:TranslatorBase<Category,CATEGORY>
    {
       public override Category TranslateToModel(CATEGORY entity)
        {
            try
            {
                Category model = null;
                if (entity != null)
                {
                    model = new Category();
                    model.Id = entity.Category_Id;
                    model.Name = entity.Category_Name;
                    model.Description = entity.Category_Description;
                    model.ImageUrl = entity.Image_Url;
                }
                return model;

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

       public override CATEGORY TranslateToEntity(Category model)
        {
            try
            {
                CATEGORY entity = null;
                if (model != null)
                {
                    entity = new CATEGORY();
                    entity.Category_Id = model.Id;
                    entity.Category_Name = model.Name;
                    entity.Category_Description = model.Description;
                    entity.Image_Url = model.ImageUrl;
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
