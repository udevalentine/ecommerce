using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abundance_SN.Model.Entity;
using Abundance_SN.Model.Model;

namespace Abundance_SN.Model.Translator
{
    public class CustomerOrderTranslator : TranslatorBase<CustomerOrder,CUSTOMER_ORDER>
    {
        private DeliveryServiceTranslator deliveryServiceTranslator;
        private CustomerTranslator customerTranslator;

        public CustomerOrderTranslator()
        {
            deliveryServiceTranslator = new DeliveryServiceTranslator();
            customerTranslator = new CustomerTranslator();
        }
        public override CustomerOrder TranslateToModel(CUSTOMER_ORDER entity)
        {
            try
            {
                CustomerOrder model = null;
                if (entity != null)
                {
                    model = new CustomerOrder();
                    model.Id = entity.Customer_Order_Id;
                    model.CartHash = entity.Cart_Hash;
                    model.Amount = entity.Amount;
                    model.CreatedAt = entity.Created_At;
                    model.DeliveryService = deliveryServiceTranslator.Translate(entity.DELIVERY_SERVICE);
                    model.Customer = customerTranslator.Translate(entity.CUSTOMER);
                    model.CustomerDeliveryAddress = entity.Customer_Delivery_Address;
                    model.CustomerOrderNumber = entity.Customer_Order_Number;
                }
                return model;
            }
            catch (Exception ex)
            {
                
                throw;
            }
        }

        public override CUSTOMER_ORDER TranslateToEntity(CustomerOrder model)
        {
            try
            {
                CUSTOMER_ORDER entity = null;
                if (model != null)
                {
                    entity = new CUSTOMER_ORDER();
                    entity.Customer_Order_Id = model.Customer.Id;
                    entity.Cart_Hash = model.CartHash;
                    entity.Amount = model.Amount;
                    entity.Created_At = model.CreatedAt;
                    if (model.DeliveryService != null)
                    {
                        entity.Delivery_Service_Id = model.DeliveryService.Id;
                    }
                    entity.Customer_Id = model.Customer.Id;
                    entity.Customer_Delivery_Address = model.CustomerDeliveryAddress;
                    entity.Customer_Order_Number = model.CustomerOrderNumber;
                }
                return entity;
            }
            catch (DbEntityValidationException e)
            {
                foreach (var ve in e.EntityValidationErrors)
                {

                } 
                throw;
            }
        }
    }
  
}
