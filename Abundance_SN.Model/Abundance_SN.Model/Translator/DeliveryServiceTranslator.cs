using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abundance_SN.Model.Entity;
using Abundance_SN.Model.Model;

namespace Abundance_SN.Model.Translator
{
    public  class DeliveryServiceTranslator : TranslatorBase<DeliveryService,DELIVERY_SERVICE>
    {
        public override DeliveryService TranslateToModel(DELIVERY_SERVICE entity)
        {
            try
            {
                DeliveryService model = null;
                if (entity != null)
                {
                    model = new DeliveryService();
                    model.Id = entity.Delivery_Service_Id;
                    model.Name = entity.Name;
                    model.Description = entity.Description;
                    model.Activated = entity.Activated;
                    model.Price = entity.Price;
                }
                return model;
            }
            catch (Exception ex)
            {
                
                throw ex;
            }
           
        }

        public override DELIVERY_SERVICE TranslateToEntity(DeliveryService model)
        {
            try
            {
                DELIVERY_SERVICE entity = null;
                if (model != null)
                {
                    entity = new DELIVERY_SERVICE();
                    entity.Delivery_Service_Id = model.Id;
                    entity.Name = model.Name;
                    entity.Description = model.Description;
                    entity.Price = model.Price;
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
