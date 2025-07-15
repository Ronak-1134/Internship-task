using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Mission.Entities.Entities
{
    [Table("MissionApplication")] 
    public class MissionApplication : Base 
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Column("mission_id")]
        public int MissionId { get; set; }

        [Column("user_id")]
        public int UserId { get; set; }

        [Column("applied_date")]
        public DateTime AppliedDate { get; set; }

        [Column("status")]
        public bool Status { get; set; }

        [Column("sheet")]
        public int Sheet { get; set; }

        [ForeignKey(nameof(MissionId))]
        public virtual Missions Mission { get; set; } = null!;

        [ForeignKey(nameof(UserId))]
        public virtual User User { get; set; } = null!;
    }
}
