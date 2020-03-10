using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abundance_SN.Model.Entity;
using Abundance_SN.Model.Model;
using System.Linq.Expressions;
using Abundance_SN.Model.Translator;

namespace Abundance_SN.Business
{
    public class CourierAreaChargesLogic: BusinessBaseLogic<CourierAreaCharges,COURIER_AREA_CHARGES>
    {
        public CourierAreaChargesLogic()
        {
            translator = new CourierAreaChargesTranslator();
        }
        public bool Modify(CourierAreaCharges courierAreaCharges)
        {
            Expression<Func<COURIER_AREA_CHARGES, bool>> selector = a => a.Id == courierAreaCharges.Id;
            COURIER_AREA_CHARGES entity = GetEntityBy(selector);
            entity.Area = courierAreaCharges.Area;
            entity.Charger = courierAreaCharges.Charger;
            entity.Courier_Id = courierAreaCharges.CourierId;
            entity.State_Id = courierAreaCharges.State.StateId;
            
                 int modifiedRecordCount = Save();
            if (modifiedRecordCount <= 0)
            {
                return false;
            }

            return true;
        }
        public bool Modify(List<CourierDetail> courierDetails)
        {
            //int modified = 0;
            //Expression<Func<FEE_DETAIL, bool>> selector = c => c.Fee_Detail_Id == feeDetail.Id;
            //FEE_DETAIL feeDetailEntity = GetEntityBy(selector);
            //if (feeDetailEntity == null && feeDetail.Fee != null && feeDetail.Fee.Id > 0 && feeDetail.Department != null)


            foreach (CourierDetail courierDetail in courierDetails)
            {
                Expression<Func<COURIER_AREA_CHARGES, bool>> selector = a => a.Id == courierDetail.CourierAreaCharges.Id;
                COURIER_AREA_CHARGES entity = GetEntityBy(selector);
                if (entity == null && courierDetail.CourierAreaCharges != null && courierDetail.CourierAreaCharges.Id > 0 && courierDetail.CourierCharges != null && courierDetail.CourierService != null)
                {
                    entity.Area = courierDetail.CourierAreaCharges.Area;
                    entity.Charger = courierDetail.CourierCharges.Charges;
                    entity.Courier_Id = courierDetail.CourierService.Courier_Id;
                    entity.State_Id = courierDetail.State.StateId;
                }


                int modifiedRecordCount = Save();
                if (modifiedRecordCount <= 0)
                {
                    return false;
                }
            }

            return true;
        }
    }
}


//public class BrandLogic : BusinessBaseLogic<Brand, BRAND>
//{
//    public BrandLogic()
//    {
//        translator = new BrandTranslator();
//    }

//    public bool Modify(Brand brand)
//    {
//        Expression<Func<BRAND, bool>> selector = a => a.Id == brand.Id;
//        BRAND entity = GetEntityBy(selector);


//        entity.Name = brand.Name;
//        entity.Active = brand.Active;

//        int modifiedRecordCount = Save();
//        if (modifiedRecordCount <= 0)
//        {
//            return false;
//        }

//        return true;
//    }
//}