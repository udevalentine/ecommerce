using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abundance_SN.Model.Entity;
using Abundance_SN.Model.Model;

namespace Abundance_SN.Model.Translator
{
    public  class CustomerTranslator : TranslatorBase<Customer,CUSTOMER>
    {
        public override Customer TranslateToModel(CUSTOMER entity)
        {
            try
            {
                Customer model = null;
                if (entity != null)
                {
                    model = new Customer();
                    model.Id = entity.Customer_Id;
                    model.FirstName = entity.First_Name;
                    model.LastName = entity.Last_Name;
                    model.MiddleName = entity.Middle_Name;
                    model.LandMark = entity.Land_Mark;
                    model.PhoneNumber = entity.Phone_Number;
                    model.City = entity.City;
                    model.State = new State
                    {
                        StateId = entity.State_Id.ToString()
                    };
                }
                return model;
            }
            catch (Exception ex)
            {
                
                throw;
            }
        }

        public override CUSTOMER TranslateToEntity(Customer model)
        {
            try
            {
                CUSTOMER entity = null;
                if (model != null)
                {
                    entity = new CUSTOMER();
                    entity.Customer_Id = model.Id;
                    entity.First_Name = model.FirstName;
                    entity.Last_Name = model.LastName;
                    entity.Middle_Name = model.MiddleName;
                    entity.Land_Mark = model.LandMark;
                    entity.Phone_Number = model.PhoneNumber;
                    entity.City = model.City;
                    entity.State_Id = model.State.StateId;
                }
                return entity;
            }
            catch (Exception ex)
            {
                
                throw;
            }
        }
    }
}
