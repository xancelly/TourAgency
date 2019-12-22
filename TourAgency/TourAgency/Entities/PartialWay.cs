using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TourAgency.Entities
{
    public partial class Way
    {
        public string WayCountry
        {
            get
            {
                return Country.Name + " - " + Country1.Name;
            }
        }
    }
}
