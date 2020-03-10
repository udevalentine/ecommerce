using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abundance_SN.Model.Entity;
using Abundance_SN.Model.Model;

namespace Abundance_SN.Model.Translator
{
    public class ProductImageGallaryTranslator : TranslatorBase<ProductImageGallary,PRODUCT_IMAGE_GALLARY>
    {
        private ProductTranslator productTranslator;

       public ProductImageGallaryTranslator()
        {
            productTranslator = new ProductTranslator();
        }
        public override ProductImageGallary TranslateToModel(PRODUCT_IMAGE_GALLARY entity)
        {
            try
            {
                ProductImageGallary model = null;
                if (entity != null)
                {
                    model = new ProductImageGallary();
                    model.Id = entity.Product_Image_Gallary_Id;
                    model.ImageUrl = entity.Image_Url;
                    model.Activated = (bool)entity.Activated;
                    model.Product = productTranslator.Translate(entity.PRODUCT);
                }
                return model;
            }
            catch (Exception ex)
            {
                
                throw ex;
            }
        }

        public override PRODUCT_IMAGE_GALLARY TranslateToEntity(ProductImageGallary model)
        {
            try
            {
                PRODUCT_IMAGE_GALLARY entity = null;
                if (model != null)
                {
                    entity = new PRODUCT_IMAGE_GALLARY();
                    entity.Product_Id = model.Product.Id;
                    entity.Image_Url = model.ImageUrl;
                    entity.Activated = model.Activated;

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
