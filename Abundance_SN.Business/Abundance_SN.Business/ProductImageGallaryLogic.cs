using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abundance_SN.Model.Entity;
using Abundance_SN.Model.Model;
using Abundance_SN.Model.Translator;
using System.Linq.Expressions;

namespace Abundance_SN.Business
{
    public class ProductImageGallaryLogic : BusinessBaseLogic<ProductImageGallary,PRODUCT_IMAGE_GALLARY>
    {
        public ProductImageGallaryLogic()
        {
            translator = new ProductImageGallaryTranslator();
        }
        public bool Modify(ProductImageGallary productImageGallary)
        {
            Expression<Func<PRODUCT_IMAGE_GALLARY, bool>> selector = a => a.Product_Image_Gallary_Id == productImageGallary.Id;
            PRODUCT_IMAGE_GALLARY entity = GetEntityBy(selector);
            entity.Product_Id = productImageGallary.Product.Id;
            entity.Image_Url = productImageGallary.ImageUrl;
            entity.Activated = productImageGallary.Activated;
            int modifiedRecordCount = Save();
            if (modifiedRecordCount <= 0)
            {
                return false;
            }

            return true;
        }
        public bool Modify(List<ProductImageGallary> productImageGallaries)
        {
            try
            {
                if (productImageGallaries.Count > 0)
                {
                    //var productImageGal = productImageGallaries.FirstOrDefault();
                    //if (productImageGal != null)
                    //{
                    for(int i=0; i<productImageGallaries.Count; i++)
                    {
                        //int gallaryImageId = productImageGal.Id;
                        int gallaryId = productImageGallaries[i].Id;
                        long productId = productImageGallaries[i].Product.Id;
                        Expression<Func<PRODUCT_IMAGE_GALLARY, bool>> selector = a => a.Product_Image_Gallary_Id == gallaryId && a.Product_Id== productId;
                        PRODUCT_IMAGE_GALLARY entity = GetEntityBy(selector);

                        //for (int j = 0; j < productImageGallaries.Count; j++)
                        //{
                        entity.Product_Id = productImageGallaries[i].Product.Id;
                        entity.Image_Url = productImageGallaries[i].ImageUrl;
                        entity.Activated = productImageGallaries[i].Activated;
                        //}
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
