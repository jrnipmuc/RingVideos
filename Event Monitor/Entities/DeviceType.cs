using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Event_Monitor.Entities
{
    public enum DeviceType
    {
        [Description("Unknown")]
        Unknown = 0,

        [Description("Stickup Camera")]
        StickupCam = 1,

        [Description("Chime")]
        Chime = 2
    }


    /*
    [Table("DeviceTypes")]
    public class DeviceType : IEnumEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Description { get; set; }
    }
    */
}