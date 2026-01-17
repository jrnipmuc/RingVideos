using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Event_Monitor.Services;

namespace Event_Monitor.Entities
{
    [Table("Devices")]
    public class Device
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        /// <summary>
        /// Ring's GUID value
        /// </summary>
        [Required]
        [Column("RingId")]
        public long RingId { get; set; }

        /// <summary>
        /// Associated site ID (required, not nullable)
        /// </summary>
        [Required]
        public long SiteId { get; set; }

        [ForeignKey(nameof(SiteId))]
        public virtual Site Site { get; set; }

        /// <summary>
        /// Device type as enum value
        /// </summary>
        [Required]
        public long DeviceTypeId { get; set; }

        /// <summary>
        /// Description/Name of the device
        /// </summary>
        [Required]
        [Column("Descrption")] // Note: matches your schema typo
        public string Description { get; set; }

        /// <summary>
        /// Device ID from Ring
        /// </summary>
        [Required]
        [Column("DeviceId")]
        public string DeviceId { get; set; }

        /// <summary>
        /// Device kind/type from Ring
        /// </summary>
        [Required]
        public string Kind { get; set; }

        [NotMapped]
        public bool IsUnassociated => SiteId == 0;

        [NotMapped]
        public string SiteName => Site?.Description ?? "Unassociated";

        [NotMapped]
        public string DeviceTypeName => ((DeviceType)DeviceTypeId).GetDescription();

        public DeviceType DeviceType
        {
            get
            {
                return (DeviceType)DeviceTypeId;
            }
            set
            {
                DeviceTypeId = (long)value;
            }
        }
    }
}