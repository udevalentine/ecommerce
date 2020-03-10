using Abundance_SN.Model.Model;
using Abundance_SN.Model.Entity;
using Abundance_SN.Model.Translator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;

namespace Abundance_SN.Business
{
    public class CourierRegionLogic : BusinessBaseLogic<CourierRegion, COURIER_REGION>
    {
        public CourierRegionLogic()
        {
            translator = new CourierRegionTranslator();
        }
        public bool Modify(CourierRegion courierRegion)
        {
            Expression<Func<COURIER_REGION, bool>> selector = a => a.Region_Id == courierRegion.Region_Id;
            COURIER_REGION entity = GetEntityBy(selector);
            entity.Region_Name = courierRegion.Region_Name;

            int modifiedRecordCount = Save();
            if (modifiedRecordCount <= 0)
            {
                return false;
            }

            return true;
        }
    }
}
