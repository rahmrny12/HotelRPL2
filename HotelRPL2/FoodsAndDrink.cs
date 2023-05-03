namespace HotelRPL2
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class FoodsAndDrink
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        [Required]
        [StringLength(1)]
        public string Type { get; set; }

        public int Price { get; set; }

        [Required]
        [StringLength(50)]
        public string Photo { get; set; }

        public virtual FDCheckOut FDCheckOut { get; set; }
    }
}
