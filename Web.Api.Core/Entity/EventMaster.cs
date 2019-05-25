using System;
using System.Collections.Generic;
using System.Text;

namespace Web.Api.Core.Entity
{
    public partial class EventMaster
    {

        public string id { get; set; }
        public string event_Type { get; set; }
        public string name { get; set; }
        public DateTime startDateTime { get; set; }
        public DateTime endDateTime { get; set; }
        public string user_Id { get; set; }
    }
}
