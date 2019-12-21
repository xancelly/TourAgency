using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TourAgency.Entities
{
    class AppData
    {
        public static TourAgencyEntities Context
        {
            get; set;
        } = new TourAgencyEntities();
    }
}
