using Abundance_SN.Model.Entity;
using Abundance_SN.Model.Model;
using Abundance_SN.Model.Translator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Abundance_SN.Business
{
    public class CourierDetailLogic : BusinessBaseLogic<CourierDetail, COURIER_DETAIL>
    {
        public CourierDetailLogic()
        {
            translator = new CourierDetailTranslator();
        }
        public bool Modify(List<CourierDetail> courierDetails)
        {
            try
            {

                foreach (CourierDetail courierDetail in courierDetails)
                {
                    Expression<Func<COURIER_DETAIL, bool>> selector = a => a.Id == courierDetail.Id;
                    COURIER_DETAIL entity = GetEntityBy(selector);
                    if (entity == null && courierDetail.CourierAreaCharges != null && courierDetail.CourierAreaCharges.Id > 0 && courierDetail.CourierCharges != null && courierDetail.CourierService != null)
                    {
                        var newCourierDetail = new CourierDetail();
                        newCourierDetail.CourierAreaCharges = courierDetail.CourierAreaCharges;
                        newCourierDetail.CourierCharges = courierDetail.CourierCharges;
                        newCourierDetail.CourierService = courierDetail.CourierService;
                        newCourierDetail.State = courierDetail.State;
                        Create(newCourierDetail);
                    }
                    else
                    {
                        if (courierDetail.CourierAreaCharges != null && courierDetail.CourierAreaCharges.Id <= 0)
                        {
                            continue;
                        }
                        entity.Courier_Id = courierDetail.CourierService.Courier_Id;
                        entity.State_Id = courierDetail.State.StateId;
                        entity.Charge_Id = courierDetail.CourierCharges.Id;
                        entity.Area_Id = courierDetail.CourierAreaCharges.Id;
                        CourierDetail existingCourierDetail = GetModelsBy(x => x.Id == courierDetail.Id && x.State_Id == courierDetail.State.StateId && x.Courier_Id == courierDetail.CourierService.Courier_Id && x.Area_Id == courierDetail.CourierAreaCharges.Id && x.Charge_Id == courierDetail.CourierCharges.Id).LastOrDefault();
                        if (existingCourierDetail == null)
                        {
                            Modify(courierDetail);
                        }
                    }
                    //else
                    //{
                    //    if (courierDetail.Id > 0 && courierDetail.CourierService.Courier_Id <= 0 && courierDetail.CourierCharges.Id <= 0 && courierDetail.State.StateId == null)
                    //    {
                    //        feeDetailAuditLogic.Delete(f => f.Fee_Detail_Id == feeDetail.Id);
                    //        Delete(selector);
                    //    }
                    //    else
                    //    {
                    //        if (feeDetail.Fee != null && feeDetail.Fee.Id <= 0)
                    //        {
                    //            continue;
                    //        }
                    //        feeDetailEntity.Fee_Id = feeDetail.Fee.Id;
                    //        feeDetailEntity.Level_Id = feeDetail.Level.Id;
                    //        feeDetailEntity.Payment_Mode_Id = feeDetail.PaymentMode.Id;
                    //        //modified = Save();

                    //        FeeDetail existingFeeDetail = GetModelsBy(f => f.Department_Id == feeDetail.Department.Id && f.Fee_Id == feeDetail.Fee.Id &&
                    //                                                f.Fee_Type_Id == feeDetail.FeeType.Id && f.Level_Id == feeDetail.Level.Id && f.Payment_Mode_Id == feeDetail.PaymentMode.Id
                    //                                                && f.Programme_Id == feeDetail.Programme.Id && f.Session_Id == feeDetail.Session.Id).LastOrDefault();

                    //        if (existingFeeDetail == null)
                    //        {
                    //            Modify(feeDetail);
                    //        }
                    //    }
                    //}
                }
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return false;
        }

        public bool Modify(CourierDetail model)
        {
            try
            {
                Expression<Func<COURIER_DETAIL, bool>> selector = s => s.Id== model.Id;
                COURIER_DETAIL entity = GetEntityBy(selector);
                if (entity == null)
                {
                    throw new Exception(NoItemFound);
                }

                if (model.CourierAreaCharges != null && model.CourierAreaCharges.Id > 0)
                {
                    entity.Area_Id = model.CourierAreaCharges.Id;
                }
                if(model.CourierCharges !=null && model.CourierCharges.Id>0)
                {
                    entity.Charge_Id = model.CourierCharges.Id;

                }
                if(model.CourierService !=null && model.CourierService.Courier_Id>0)
                {
                    entity.Courier_Id = model.CourierService.Courier_Id;
                }
                if(model.State !=null && model.State.StateId !="")
                {
                    entity.State_Id = model.State.StateId;
                }
                int modifiedRecordCount = Save();
                if (modifiedRecordCount <= 0)
                {
                    return false;
                }

                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
