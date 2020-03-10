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
    public  class VendorLogic : BusinessBaseLogic<Vendors, VENDOR>
    {
        public VendorLogic()
        {
            translator = new VendorsTranslator();
        }

        public bool Modify(Vendors vendors)
        {
            Expression<Func<VENDOR, bool>> selector = a => a.Id == vendors.Id;
            VENDOR entity = GetEntityBy(selector);

            entity.Id = vendors.Id;
            entity.Name = vendors.Name;
            entity.Address = vendors.Address;
            entity.Active = vendors.Active;
            entity.Phone_Number = vendors.PhoneNumber;

           
            int modifiedRecordCount = Save();
            if (modifiedRecordCount <= 0)
            {
                return false;
            }

            return true;
            
        }
    }
}
