using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abundance_SN.Model.Entity;
using Abundance_SN.Model.Model;

namespace Abundance_SN.Model.Translator
{
    public class InventoriesTranslator : TranslatorBase<Inventories,INVENTORIES>
    {
        private ProductTranslator productTranslator;

        public InventoriesTranslator()
        {
            productTranslator = new ProductTranslator();

        }
        public override Inventories TranslateToModel(INVENTORIES entity)
        {
            try
            {
                Inventories model = null;
                if (entity != null)
                {
                    model = new Inventories();
                    model.Id = entity.Id;
                    model.Product = productTranslator.Translate(entity.PRODUCT);
                    model.Quantity = entity.Quantity;
                    model.CreatedAt = entity.Created_At;
                    model.UpdatedAt = entity.Updated_At;

                }
                return model;
            }
            catch (Exception)
            {
                
                throw;
            }
        }

        public override INVENTORIES TranslateToEntity(Inventories model)
        {
            try
            {
                INVENTORIES entity = null;
                if (model != null)
                {
                    entity = new INVENTORIES();
                    entity.Id = model.Id;
                    entity.Product_Id = model.Product.Id;
                    entity.Quantity = model.Quantity;
                    entity.Created_At = model.CreatedAt;
                    entity.Updated_At = model.UpdatedAt;
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
