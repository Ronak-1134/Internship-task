
using System.ComponentModel.DataAnnotations.Schema;

namespace Mission.Entities.Entities
{
    public class Missions : Base
    {
        [Column("id")]
        public int Id { get; set; }

        [Column("mission_title")]
        public string MissionTitle { get; set; }
        [Column("mission_description")]
        public string MissionDescription { get; set; }

        [Column("mission_organisation_name")]
        public string MissionOrganisationName { get; set; }

        [Column("mission_organisation_detail")]
        public string MissionOrganisationDetail { get; set; }

        [Column("country_id")]
        public int CountryId { get; set; }
        [Column("city_id")]
        public int CityId { get; set; }
        [Column("start_date")]
        public DateTime StartDate { get; set; }
        [Column("end_date")]
        public DateTime EndDate { get; set; }
        [Column("mission_type")]
        public string MissionType { get; set; }
        [Column("totalsheets")]
        public int TotalSheets { get; set; }
        [Column("mission_theme_id")]
        public int MissionThemeId { get; set; }
        [Column("mission_skill_id")]
        public string MissionSkillId { get; set; }
        [Column("mission_images")]
        public string MissionImages { get; set; }

        [ForeignKey(nameof(CityId))]
        public virtual Country Country { get; set; } = null!;

        [ForeignKey(nameof(CountryId))]
        public virtual City City { get; set; } = null!;
        [ForeignKey(nameof(MissionThemeId))]
        public virtual MissionTheme MissionTheme { get; set; } = null!;

    }
}
