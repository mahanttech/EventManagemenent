using System;
using System.Collections.Generic;
using System.Text;

namespace Web.Api.Core.Entity
{
    public class SystemUsers
    {
        public string id { get; set; }
        public string Username { get; set; }
        public string role { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
    }
}
