using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abundance_SN.Model.Entity;
using Abundance_SN.Model.Model;

namespace Abundance_SN.Model.Translator
{
    public class DeliveryAddressTranslator : TranslatorBase<DeliveryAddress,DELIVERY_ADDRESS>
    {
        private PersonTranslator personTranslator;
        private DeliveryServiceTranslator deliveryServiceTranslator;
        private DeliveryStatusTranslator deliveryStatusTranslator;

        public DeliveryAddressTranslator()
        {
            personTranslator = new PersonTranslator();
            deliveryServiceTranslator = new DeliveryServiceTranslator();
            deliveryStatusTranslator = new DeliveryStatusTranslator();
        }
        public override DeliveryAddress TranslateToModel(DELIVERY_ADDRESS entity)
        {
            try
            {
                DeliveryAddress model = null;
                if (entity != null)
                {
                    model = new DeliveryAddress();
                    model.Id = entity.Delivery_Address_Id;
                    model.CartHash = entity.Cart_Hash;
                    model.StateId = entity.Delivery_State_Id;
                    model.Address = entity.Address;
                    model.DeliveryOption = entity.Delivery_Option;
                    model.DateCreated = entity.Date_Created;
                    model.DateDelivered = entity.Date_Delivered;
                    model.Person = personTranslator.Translate(entity.PERSON);
                    model.DeliveryService = deliveryServiceTranslator.Translate(entity.DELIVERY_SERVICE);
                    model.DeliveryStatus = deliveryStatusTranslator.Translate(entity.DELIVERY_STATUS);
                }
                return model;
            }
            catch (Exception ex)
            {
                
                throw ex;
            }
        }

        public override DELIVERY_ADDRESS TranslateToEntity(DeliveryAddress model)
        {
            try
            {
                DELIVERY_ADDRESS entity = null;
                if (model != null)
                {
                    entity = new DELIVERY_ADDRESS();
                    entity.Delivery_Address_Id = model.Id;
                    entity.Cart_Hash = model.CartHash;
                    entity.Address = model.Address;
                    entity.Delivery_State_Id = model.StateId;
                    entity.Date_Created = DateTime.Now.Date;
                    entity.Date_Delivered = model.DateDelivered;
                    entity.Person_Id = model.Person.Id;
                    entity.Delivery_Service_Id = model.DeliveryService.Id;
                    entity.Delivery_Status_Id = model.DeliveryStatus.Id;
                    entity.Delivery_Option = model.DeliveryOption;
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
