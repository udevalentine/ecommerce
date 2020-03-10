using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abundance_SN.Model.Entity;
using Abundance_SN.Model.Model;

namespace Abundance_SN.Model.Translator
{
    public class ProductVariantOptionsTranslator : TranslatorBase<ProductVariantOptions,PRODUCT_VARIANT_OPTIONS>
    {
        private ProductVariantTranslator productVariantTranslator;

        public ProductVariantOptionsTranslator()
        {
            productVariantTranslator = new ProductVariantTranslator();
        }
        public override ProductVariantOptions TranslateToModel(PRODUCT_VARIANT_OPTIONS entity)
        {
            try
            {
                ProductVariantOptions model = null;
                if (entity != null)
                {
                    model = new ProductVariantOptions();
                    model.Id = entity.Product_Option_Id;
                    model.ProductOptionName = entity.Product_Option_Name;
                    model.Price = entity.Price;
                    model.ProductVariant = productVariantTranslator.Translate(entity.PRODUCT_VARIANT);
                    model.ImageUrl = entity.Image_Url;
                }
                return model;
            }
            catch (Exception ex)
            {
                    
                throw ex;
            }
        }

        public override PRODUCT_VARIANT_OPTIONS TranslateToEntity(ProductVariantOptions model)
        {
            try
            {
                PRODUCT_VARIANT_OPTIONS entity = null;
                if (model != null)
                {
                   entity = new PRODUCT_VARIANT_OPTIONS();
                    entity.Product_Option_Name = model.ProductOptionName;
                    entity.Product_Variant_Id = model.ProductVariant.Id;
                    entity.Price = model.Price;
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
