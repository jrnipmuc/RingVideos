using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Event_Monitor.Entities
{
    [Table("Settings")]
    public class Setting
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [MaxLength(500)]
        public string RootDirectory { get; set; }

        [Required]
        public int ConnectionId { get; set; }

        [ForeignKey(nameof(ConnectionId))]
        public Connection Connection { get; set; }

        [Required]
        public DateTime EffectiveStartDate { get; set; } = DateTime.Now;

        public DateTime? EffectiveEndDate { get; set; }

        [NotMapped]
        public bool IsActive => EffectiveEndDate == null;
    }
}