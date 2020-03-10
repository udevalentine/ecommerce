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
    public class DeliveryAddressLogic : BusinessBaseLogic<DeliveryAddress,DELIVERY_ADDRESS>
    {
        public DeliveryAddressLogic()
        {
            translator = new DeliveryAddressTranslator();
        }

        public bool Modify(DeliveryAddress deliveryAddress)
        {
            try
            {
                Expression<Func<DELIVERY_ADDRESS, bool>> selector = a => a.Delivery_Address_Id == deliveryAddress.Id;
                DELIVERY_ADDRESS entity = GetEntityBy(selector);

                if (deliveryAddress.DeliveryStatus.Id > 0)
                {
                    entity.Delivery_Status_Id = deliveryAddress.DeliveryStatus.Id;
                    entity.Date_Delivered = DateTime.UtcNow;
                }
                
                int modifiedRecordCount = Save();
                if (modifiedRecordCount > 0)
                {
                    return true;
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
            return false;
        }
    }
}
