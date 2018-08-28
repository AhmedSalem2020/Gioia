using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Gioia.Models
{
    public class moodChart
    {
        public ApplicationUser applicationUser { get; set; }
        public double happypersent { get; set; }
        public double sadpersent { get; set; }
        public double neturalpersent { get; set; }
        public double angerypersent { get; set; }
    }
}