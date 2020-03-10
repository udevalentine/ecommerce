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
    public class CourierChargesLogic:BusinessBaseLogic<CourierCharges,COURIER_CHARGES>
    {
        public CourierChargesLogic()
        {
            translator = new CourierChargesTranslator();
        }
        public bool Modify(CourierCharges courierCharges)
        {
            Expression<Func<COURIER_CHARGES, bool>> selector = a => a.Id == courierCharges.Id;
            COURIER_CHARGES entity = GetEntityBy(selector);

            entity.Charges = courierCharges.Charges;

            int modifiedRecordCount = Save();
            if (modifiedRecordCount <= 0)
            {
                return false;
            }

            return true;
        }
    }
}
