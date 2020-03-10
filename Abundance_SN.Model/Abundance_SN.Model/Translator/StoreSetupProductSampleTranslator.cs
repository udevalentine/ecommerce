using System;
using Abundance_SN.Model.Entity;
using Abundance_SN.Model.Model;

namespace Abundance_SN.Model.Translator
{
    public class StoreSetupProductSampleTranslator : TranslatorBase<StoreSetupProductSample,STORE_SETUP_PRODUCTSAMPLE>
    {
        public override StoreSetupProductSample TranslateToModel(STORE_SETUP_PRODUCTSAMPLE entity)
        {
            try
            {
                StoreSetupProductSample model = null;
                if (entity != null)
                {
                    model = new StoreSetupProductSample();
                    model.Id = entity.Store_Setup_ProductSample_Id;
                    model.Name = entity.Name;
                    model.Description = entity.Description;
                }

                return model;
            }
            catch (Exception)
            {
                
                throw;
            }
        }

        public override STORE_SETUP_PRODUCTSAMPLE TranslateToEntity(StoreSetupProductSample model)
        {
            try
            {
                STORE_SETUP_PRODUCTSAMPLE entity = null;
                if (model != null)
                {
                    entity = new STORE_SETUP_PRODUCTSAMPLE();
                    entity.Store_Setup_ProductSample_Id = model.Id;
                    entity.Name = model.Name;
                    entity.Description = model.Description;
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
