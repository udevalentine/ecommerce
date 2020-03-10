using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Abundance_SN.Model.Entity;
using Abundance_SN.Model.Model;
using Abundance_SN.Model.Translator;

namespace Abundance_SN.Business
{
    public class PersonLogic : BusinessBaseLogic<Person,PERSON>
    {
        public PersonLogic()
        {
            translator = new PersonTranslator();
        }

        public void Modify(Person person)
        {
         
           try
            {
                Expression<Func<PERSON, bool>> selector = a => a.User_Id == person.User.Id;
                PERSON entity = GetEntityBy(selector);

                entity.Address = person.Address;
                entity.State_Id = person.StateId;
                if (person.OtherName != null)
                {
                    entity.OtherName = person.OtherName;
                }
                int modifiedRecordCount = Save();
                if (modifiedRecordCount <= 0)
                {
                }
               
            }
            catch (Exception ex)
            {
                
                throw ex;
            }
        
        }
        public bool CheckDuplicateEmail(string email)
        {
            try
            {
                Expression<Func<PERSON, bool>> selector = r => r.Email == email;
                Person personDetails = GetModelBy(selector);
                if (personDetails != null)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
