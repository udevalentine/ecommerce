using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abundance_SN.Model.Model
{
    public class ReorderNotification
    {
        public int Id { get; set; }
        public string Message { get; set; }
        public bool Status { get; set; }
        public DateTime NotificationDate { get; set; }
        public int NotificationType { get; set; }
        public string OrderNumber { get; set; }
    }
}
