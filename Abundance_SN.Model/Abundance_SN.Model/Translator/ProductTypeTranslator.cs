using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Configuration;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using Abundance_SN.Model.Entity;
using Abundance_SN.Model.Model;

namespace Abundance_SN.Model.Translator
{
    
    public  class ProductTypeTranslator : TranslatorBase<ProductType,PRODUCT_TYPE>
    {
        private CategoryTranslator categoryTranslator;

        public ProductTypeTranslator()
        {
            categoryTranslator = new CategoryTranslator();
        }
        public override PRODUCT_TYPE TranslateToEntity(ProductType model)
        {
            try
            {
                PRODUCT_TYPE entity = null;
                if (model != null)
                {
                    entity = new PRODUCT_TYPE();
                    entity.Id = model.Id;
                    entity.Name = model.Name;
                    entity.Active = model.Active;
                    entity.Category_Id = model.Category.Id;
                }
                return entity;
            }
            catch (Exception)
            {
                
                throw;
            }
        }

        public override ProductType TranslateToModel(PRODUCT_TYPE entity)
        {
            try
            {
                ProductType model = null;
                if (entity != null)
                {
                    model = new ProductType();
                    model.Id = entity.Id;
                    model.Name = entity.Name;
                    model.Active = entity.Active;
                    model.Category = categoryTranslator.Translate(entity.CATEGORY);
                }
                return model;
            }
            catch (Exception)
            {
                
                throw;
            }
        }
    }
}
