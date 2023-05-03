namespace HotelRPL2
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("FDCheckOut")]
    public partial class FDCheckOut
    {
        public int Id { get; set; }

        public int ReservationRoomId { get; set; }

        public int FDId { get; set; }

        public int? Qty { get; set; }

        public int? TotalPrice { get; set; }

        public int EmployeeId { get; set; }

        public virtual Employee Employee { get; set; }

        public virtual FoodsAndDrink FoodsAndDrink { get; set; }

        public virtual ReservationRoom ReservationRoom { get; set; }
    }
}
