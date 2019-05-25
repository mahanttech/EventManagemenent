using System;
using System.Collections.Generic;
using System.Text;

namespace Web.Api.Infrastructure.Models
{
    public partial class EventMasterModel
    {
        public string id { get; set; }
        public string event_Type { get; set; }
        public string name { get; set; }
        public string startDateTime { get; set; }
        public string endDateTime { get; set; }
        public string user_Id { get; set; }
    }
}
