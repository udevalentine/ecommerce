using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Abundance_SN.Model.Entity;
using Abundance_SN.Model.Model;

namespace Abundance_SN.Model.Translator
{
    public class ProductVariantTranslator : TranslatorBase<ProductVariant,PRODUCT_VARIANT>
    {
        private ProductTranslator productTranslator;

       public ProductVariantTranslator()
        {
            productTranslator = new ProductTranslator();
        }
        public override ProductVariant TranslateToModel(PRODUCT_VARIANT entity)
        {
            try
            {
                ProductVariant model = null;
                if (entity != null)
                {
                  model = new ProductVariant();
                    model.Id = entity.Product_Variant_Id;
                    model.Key1 = entity.Key_1;
                    model.Key2 = entity.Key_2;
                    model.Key3 = entity.Key_3;
                    model.Value1 = entity.Value_1;
                    model.Value2 = entity.Value_2;
                    model.Value3 = entity.Value_3;
                    model.Product = productTranslator.Translate(entity.PRODUCT);
                }
                return model;
            }
            catch (Exception ex)
            {
                
                throw ex;
            }
        }

        public override PRODUCT_VARIANT TranslateToEntity(ProductVariant model)
        {
            try
            {
                PRODUCT_VARIANT entity = null;
                if (model != null)
                {
                    entity = new PRODUCT_VARIANT();
                    entity.Key_1 = model.Key1;
                    entity.Value_1 = model.Value1;
                    if (!string.IsNullOrEmpty(model.Key2))
                    {
                        entity.Key_2 = model.Key2;
                        entity.Value_2 = model.Value2; 
                    }
                    if (!string.IsNullOrEmpty(model.Key3))
                    {
                        entity.Key_3 = model.Key3;
                        entity.Value_3 = model.Value3;
                    }
                    entity.Product_Id = model.Product.Id;
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
