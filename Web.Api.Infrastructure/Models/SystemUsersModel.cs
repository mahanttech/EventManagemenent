using System;
using System.Collections.Generic;
using System.Text;

namespace Web.Api.Infrastructure.Models
{
    public class SystemUsersModel
    {
        public string id { get; set; }
        public string role { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Token { get; set; }
        public string Message { get; set; }
    }
}
