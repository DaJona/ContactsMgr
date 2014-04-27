using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.System
{
    public class SessionMemberInfo
    {
        public int id { get; set; }
        public string name { get; set; }
        public string lang { get; set; }
        public int timeZoneMinsOffset { get; set; }
    }
}
