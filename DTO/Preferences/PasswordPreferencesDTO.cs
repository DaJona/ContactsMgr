﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.Preferences
{
    public class PasswordPreferencesDTO
    {
        public int memberId { get; set; }
        public string actualPassword { get; set; }
        public string newPassword { get; set; }
    }
}
