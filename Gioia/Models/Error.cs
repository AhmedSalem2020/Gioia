﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Gioia.Models
{
    public class Error
    {
        public string Message { get; set; }
        public Dictionary<string, List<string>> ModelState { get; set; }
    }
}