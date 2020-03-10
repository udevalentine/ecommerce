using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abundance_SN.Model.Entity;
using Abundance_SN.Model.Model;
using Abundance_SN.Model.Translator;
using System.Linq.Expressions;

namespace Abundance_SN.Business
{
    public class CourierServiceLogic:BusinessBaseLogic<CourierService,COURIER_SERVICE>
    {
        public CourierServiceLogic()
        {
            translator = new CourierServiceTranslator();
        }
        public bool Modify(CourierService courierService)
        {
            Expression<Func<COURIER_SERVICE, bool>> selector = a => a.Courier_Id==courierService.Courier_Id;
            COURIER_SERVICE entity = GetEntityBy(selector);

            entity.Courier_Name = courierService.Courier_Name;
            entity.Address = courierService.Address;
            entity.Email = courierService.Email;
            entity.Phone_Number = courierService.Phone_Number;

            int modifiedRecordCount = Save();
            if (modifiedRecordCount <= 0)
            {
                return false;
            }

            return true;
        }
    }
}
