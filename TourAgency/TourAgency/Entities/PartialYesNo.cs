﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TourAgency.Entities
{
    public partial class Order
    {
        public string YesNo
        {
            get
            {
                if (IsActual == true)
                {
                    return "Да";
                }
                else
                {
                    return "Нет";
                }
            }
        }
    }
}
