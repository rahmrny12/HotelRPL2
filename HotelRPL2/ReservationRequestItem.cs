namespace HotelRPL2
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ReservationRequestItem")]
    public partial class ReservationRequestItem
    {
        public int Id { get; set; }

        public int? ReservationRoomId { get; set; }

        public int? ItemId { get; set; }

        public int? Qty { get; set; }

        public int? TotalPrice { get; set; }

        public virtual Item Item { get; set; }

        public virtual ReservationRoom ReservationRoom { get; set; }
    }
}
