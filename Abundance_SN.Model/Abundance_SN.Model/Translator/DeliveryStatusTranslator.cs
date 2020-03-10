using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Configuration;
using System.Text;
using System.Threading.Tasks;
using Abundance_SN.Model.Entity;
using Abundance_SN.Model.Model;

namespace Abundance_SN.Model.Translator
{
    public class DeliveryStatusTranslator : TranslatorBase<DeliveryStatus,DELIVERY_STATUS>
    {
        public override DeliveryStatus TranslateToModel(DELIVERY_STATUS entity)
        {
            DeliveryStatus model = null;
            if (entity != null)
            {
                model = new DeliveryStatus();
                model.Id = entity.Delivery_Status_Id;
                model.Name = entity.Name;
                model.Decription = entity.Decription;
            }
            return model;
        }

        public override DELIVERY_STATUS TranslateToEntity(DeliveryStatus model)
        {
            try
            {
                DELIVERY_STATUS entity = null;
                if (model != null)
                {
                    entity = new DELIVERY_STATUS();
                    entity.Delivery_Status_Id = model.Id;
                    entity.Name = model.Name;
                    entity.Decription = model.Decription;
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
