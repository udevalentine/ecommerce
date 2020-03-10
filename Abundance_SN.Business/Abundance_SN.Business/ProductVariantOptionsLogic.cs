using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Abundance_SN.Model.Entity;
using Abundance_SN.Model.Model;
using Abundance_SN.Model.Translator;

namespace Abundance_SN.Business
{
    public class ProductVariantOptionsLogic: BusinessBaseLogic<ProductVariantOptions,PRODUCT_VARIANT_OPTIONS>
    {
        public ProductVariantOptionsLogic()
        {
            translator = new ProductVariantOptionsTranslator();
        }

        public bool Modify(List<ProductVariantOptions> productVariantOptionses)
        {
            try
            {
                if (productVariantOptionses.Count > 0)
                {
                    var productVariantOptions = productVariantOptionses.FirstOrDefault();
                    if (productVariantOptions != null)
                    {
                        long variantId = productVariantOptions.ProductVariant.Id;

                        Expression<Func<PRODUCT_VARIANT_OPTIONS, bool>> selector = a => a.Product_Variant_Id == variantId;
                        List<PRODUCT_VARIANT_OPTIONS> entities = GetEntitiesBy(selector);

                        for (int i = 0; i < productVariantOptionses.Count; i++)
                        {
                            entities[i].Product_Option_Name = productVariantOptionses[i].ProductOptionName;
                            entities[i].Image_Url = productVariantOptionses[i].ImageUrl;
                            entities[i].Price = productVariantOptionses[i].Price;
                        }
                        int modifiedCount = Save();
                        if (modifiedCount > 0)
                        {
                            return true;
                        }
                       
                    }

                }

            }
            catch (Exception ex)
            {
                
                throw ex;
            }
            return false;
        }
    }
}
