using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abundance_SN.Model.Entity;
using Abundance_SN.Model.Model;

namespace Abundance_SN.Model.Translator
{
    public class InventoryTranslator : TranslatorBase<Inventory,INVENTORY>
    {
        private ProductTranslator productTranslator;
        private ProductVariantOptionsTranslator productVariantOptionsTranslator;

        public InventoryTranslator()
        {
            productTranslator = new ProductTranslator();
            productVariantOptionsTranslator = new ProductVariantOptionsTranslator();

        }
        public override Inventory TranslateToModel(INVENTORY entity)
        {
            try
            {
                Inventory model = null;
                if (entity != null)
                {
                    model = new Inventory();
                    model.Id = entity.Id;
                    model.Product = productTranslator.Translate(entity.PRODUCT);
                    model.Quantity = entity.Quantity;
                    model.ProductVariantOptions = productVariantOptionsTranslator.Translate(entity.PRODUCT_VARIANT_OPTIONS);

                }
                return model;
            }
            catch (Exception)
            {
                
                throw;
            }
        }

        public override INVENTORY TranslateToEntity(Inventory model)
        {
            try
            {
                INVENTORY entity = null;
                if (model != null)
                {
                    entity = new INVENTORY();
                    entity.Id = model.Id;
                    entity.Product_Id = model.Product.Id;
                    entity.Quantity = model.Quantity;
                    entity.Product_Variant_Option_Id = model.ProductVariantOptions.Id;
                }
                return entity;
            }
            catch (Exception)
            {
                
                throw;
            }
        }
    }
}
