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
    public class ReorderNotificationLogic:BusinessBaseLogic<ReorderNotification, REORDER_NOTIFICATION>
    {
        public ReorderNotificationLogic()
        {
            translator = new ReorderNotificationTranslator();
        }
        public bool Modify(ReorderNotification reorderNotification)
        {
            Expression<Func<REORDER_NOTIFICATION, bool>> selector = a => a.Id == reorderNotification.Id;
            REORDER_NOTIFICATION entity = GetEntityBy(selector);
            entity.Message = reorderNotification.Message;
            entity.NotificationType = reorderNotification.NotificationType;
            entity.Order_Number = reorderNotification.OrderNumber;
            entity.Status = reorderNotification.Status;
            entity.DateTime = reorderNotification.NotificationDate;

            int modifiedRecordCount = Save();
            if (modifiedRecordCount <= 0)
            {
                return false;
            }

            return true;
        }
    }
}