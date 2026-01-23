using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Event_Monitor.Entities
{
    [Table("Sites")]
    public class Site : IEnumEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Description { get; set; }

        public virtual ICollection<Device> Devices { get; set; } = new List<Device>();
    }
}