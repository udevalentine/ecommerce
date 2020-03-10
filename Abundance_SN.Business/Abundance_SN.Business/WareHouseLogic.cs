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
    public class WareHouseLogic : BusinessBaseLogic<WareHouse,WARE_HOUSE>
    {
        public WareHouseLogic()
        {
            translator = new WareHouseTranslator();
        }

        public bool Modify(WareHouse wareHouse)
        {
       
            {
                Expression<Func<WARE_HOUSE, bool>> selector = a => a.Id == wareHouse.Id;
                WARE_HOUSE entity = GetEntityBy(selector);

                entity.Id = wareHouse.Id;
                entity.Name = wareHouse.Name;
                entity.Address = wareHouse.Address;
                entity.Active = wareHouse.Active;
                entity.Phone_Number = wareHouse.PhoneNumber;
                entity.Updated_At = wareHouse.UpdatedAt;

                int modifiedRecordCount = Save();
                if (modifiedRecordCount <= 0)
                {
                    return false;
                }

                return true;

            }
        }


    }
}
