//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace TourAgency.Entities
{
    using System;
    using System.Collections.Generic;
    
    public partial class Visa
    {
        public int Id { get; set; }
        public string CodeCountry { get; set; }
        public Nullable<decimal> Price { get; set; }
    
        public virtual Country Country { get; set; }
    }
}
