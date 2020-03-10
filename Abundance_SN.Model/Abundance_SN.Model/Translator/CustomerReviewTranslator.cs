using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abundance_SN.Model.Entity;
using Abundance_SN.Model.Model;

namespace Abundance_SN.Model.Translator
{
    public class CustomerReviewTranslator : TranslatorBase<CustomerReview,CUSTOMER_REVIEW>
    {
        private ProductTranslator productTranslator;

        public CustomerReviewTranslator()
        {
            productTranslator = new ProductTranslator();
        }
        public override CustomerReview TranslateToModel(CUSTOMER_REVIEW entity)
        {
            try
            {
                CustomerReview model = null;
                if (entity != null)
                {
                    model = new CustomerReview();
                    model.Id = entity.Customer_Review_Id;
                    model.Name = entity.Customer_Name;
                    model.Email = entity.Customer_Email;
                    model.Rating = entity.Customer_Rating;
                    model.Subject = entity.Customer_Review_Subject;
                    model.Review = entity.Customer_Review1;
                    model.Product = productTranslator.Translate(entity.PRODUCT);
                }
                return model;
            }
            catch (Exception ex)
            {
                
                throw ex; 
            }
        }

        public override CUSTOMER_REVIEW TranslateToEntity(CustomerReview model)
        {
            try
            {
                CUSTOMER_REVIEW entity = null;
                if (model != null)
                {
                    entity = new CUSTOMER_REVIEW();
                    entity.Customer_Name = model.Name;
                    entity.Customer_Email = model.Email;
                    entity.Customer_Rating = model.Rating;
                    entity.Customer_Review_Subject = model.Subject;
                    entity.Product_Id = model.Product.Id;
                    entity.Customer_Review1 = model.Review;

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
