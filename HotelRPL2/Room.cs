namespace HotelRPL2
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Room")]
    public partial class Room
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Room()
        {
            ReservationRooms = new HashSet<ReservationRoom>();
        }

        public int Id { get; set; }

        public int? RoomTypeId { get; set; }

        [StringLength(50)]
        public string RoomNumber { get; set; }

        [StringLength(50)]
        public string RoomFloor { get; set; }

        [Column(TypeName = "text")]
        public string Description { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ReservationRoom> ReservationRooms { get; set; }

        public virtual RoomType RoomType { get; set; }
    }
}
