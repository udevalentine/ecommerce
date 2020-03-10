using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.Remoting.Lifetime;
using System.Text;
using System.Threading.Tasks;
using Abundance_SN.Model.Entity;
using Abundance_SN.Model.Model;
using Abundance_SN.Model.Translator;

namespace Abundance_SN.Business
{
    public class ProductLogic : BusinessBaseLogic<Product,PRODUCT>
    {
        public ProductLogic()
        {
            translator = new ProductTranslator();
        }

        public bool Modify(Product product)
        {
            Expression<Func<PRODUCT, bool>> selector = a => a.Id == product.Id;
            PRODUCT entity = GetEntityBy(selector);

            
            entity.Name = product.Name;
            entity.Slug = product.Slug;
            entity.Additional_Information = product.AdditionalInformation;
            entity.Description = product.Description;
            entity.Price = product.Price;
            entity.Shipping = product.Shipping;
            entity.Weight = product.Weight;
            entity.Can_Be_Delivered = product.CanBedelivered;
            entity.MaxPrice = product.MaxPrice;
            if (product.ReorderLevel > 0)
            {
                entity.Reorder_Level = product.ReorderLevel;
            }
            if (product.Visits != null)
            {
                entity.Visits = product.Visits;
            }
           
            entity.Product_Type_Id = product.ProductType.Id;
            entity.Brand_Id = product.Brand.Id;
            if (product.ImageUrl != null)
            {
                entity.Image_Url = product.ImageUrl;
            }
            if (product.Discount > 0)
            {
                entity.Discount = product.Discount;
            }
            entity.sku = product.Sku;
            entity.Active = product.Active;
            int modifiedRecordCount = Save();
            if (modifiedRecordCount <= 0)
            {
                return false;
            }

            return true;
        }

        public void UpdateVisit(Product product)
        {
           try
            {
                Expression<Func<PRODUCT, bool>> selector = a => a.Id == product.Id;
                PRODUCT entity = GetEntityBy(selector);

                if (entity.Visits == null)
                {
                    entity.Visits = 1;
                }
                else
                {
                    entity.Visits += 1;
                }
                int modifiedRecordCount = Save();
            }
            catch (Exception ex)
            {
                
                throw ex;
            }
        }

        public void UpdateReorderLevel(Product product)
        {
            try
            {

                Expression<Func<PRODUCT, bool>> selector = a => a.Id == product.Id;
                PRODUCT entity = GetEntityBy(selector);

                entity.Reorder_Level += 1;
                int modifiedRecordCount = Save();
                if (modifiedRecordCount <= 0)
                {
                }
                
            }
            catch (Exception ex)
            {
                
                throw ex;
            }
        }
    }
}
