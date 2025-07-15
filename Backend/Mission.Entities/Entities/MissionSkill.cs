using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Mission.Entities.Entities
{
    [Table("MissionSkill")] 
    public class MissionSkill : Base 
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Column("skill_name")]
        public string SkillName { get; set; }

        [Column("status")]
        public string Status { get; set; }
    }
}
