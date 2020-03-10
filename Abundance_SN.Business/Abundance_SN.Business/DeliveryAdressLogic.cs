using System;

using Abundance_SN.Model.Entity;
using Abundance_SN.Model.Model;
using Abundance_SN.Model.Translator;

namespace Abundance_SN.Business
{
    public class DeliveryAdressLogic : BusinessBaseLogic<DeliveryAddress,DELIVERY_ADDRESS>
    {
        public DeliveryAdressLogic()
        {
            translator = new DeliveryAddressTranslator();
        }
    }
}
