﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShope.Core.Entities.Security
{
    public class Role
    {
        public int Id { get; set; }
        public string RoleName { get; set; }
        public bool IsActive { get; set; }

    }
      
}
