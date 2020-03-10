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
    public  class VendorsLogic : BusinessBaseLogic<Vendors, VENDORS>
    {
        public VendorsLogic()
        {
            translator = new VendorsTranslator();
        }

        public bool Modify(Vendors vendors)
        {
            Expression<Func<VENDORS, bool>> selector = a => a.Id == vendors.Id;
            VENDORS entity = GetEntityBy(selector);

            entity.Id = vendors.Id;
            entity.Name = vendors.Name;
            entity.Address = vendors.Address;
            entity.Active = vendors.Active;
            entity.Phone_Number = vendors.PhoneNumber;
            entity.Updated_At = vendors.UpdatedAt;
           
            int modifiedRecordCount = Save();
            if (modifiedRecordCount <= 0)
            {
                return false;
            }

            return true;
            
        }
    }
}
