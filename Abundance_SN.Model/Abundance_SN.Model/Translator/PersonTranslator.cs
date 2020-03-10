using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abundance_SN.Model.Entity;
using Abundance_SN.Model.Model;

namespace Abundance_SN.Model.Translator
{
    public class PersonTranslator : TranslatorBase<Person,PERSON>
    {
        private UserTranslator userTranslator;

        public PersonTranslator()
        {
            userTranslator = new UserTranslator();
        }
        public override PERSON TranslateToEntity(Person model)
        {
            try
            {
                PERSON entity = null;
                if (model != null)
                {
                    entity = new PERSON();
                    entity.Person_Id = model.Id;
                    entity.FirstName = model.FirstName;
                    entity.Surname = model.Surname;
                    entity.OtherName = model.OtherName;
                    entity.Address = model.Address;
                    entity.Phone_Number = model.PhoneNumber;
                    entity.Country_Id = model.CountryId;
                    entity.State_Id = model.StateId;
                    entity.Email = model.Email;
                    entity.Date_Registered = DateTime.UtcNow;
                    entity.User_Id = model.User.Id;
                    entity.Uuid = model.Uuid;
                }
                return entity;
            }
            catch (Exception)
            {
                
                throw;
            }
        }

        public override Person TranslateToModel(PERSON entity)
        {
            try
            {
                Person model = null;
                if (entity != null)
                {
                    model = new Person();
                    model.Id = entity.Person_Id;
                    model.FirstName = entity.FirstName;
                    model.Surname = entity.Surname;
                    model.OtherName = entity.OtherName;
                    model.Address = entity.Address;
                    model.PhoneNumber = entity.Phone_Number;
                    model.CountryId = entity.Country_Id;
                    model.StateId = entity.State_Id;
                    model.Email = entity.Email;
                    model.DateRegistered = entity.Date_Registered;
                    model.User = userTranslator.Translate(entity.USER);
                    model.Uuid = entity.Uuid;

                }
                return model;
            }
            catch (Exception)
            {
                
                throw;
            }
        }
    }
}
