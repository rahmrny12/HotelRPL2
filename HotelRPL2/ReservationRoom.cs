namespace HotelRPL2
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ReservationRoom")]
    public partial class ReservationRoom
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ReservationRoom()
        {
            FDCheckOuts = new HashSet<FDCheckOut>();
            ReservationCheckOuts = new HashSet<ReservationCheckOut>();
            ReservationRequestItems = new HashSet<ReservationRequestItem>();
        }

        public int Id { get; set; }

        public int? ReservationId { get; set; }

        public int? RoomId { get; set; }

        [Column(TypeName = "date")]
        public DateTime? StartDateTime { get; set; }

        public int? DurationNights { get; set; }

        public int? RoomPrice { get; set; }

        public DateTime? CheckInDateTime { get; set; }

        public DateTime? CheckOutDateTime { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<FDCheckOut> FDCheckOuts { get; set; }

        public virtual Reservation Reservation { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ReservationCheckOut> ReservationCheckOuts { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ReservationRequestItem> ReservationRequestItems { get; set; }

        public virtual Room Room { get; set; }
    }
}
