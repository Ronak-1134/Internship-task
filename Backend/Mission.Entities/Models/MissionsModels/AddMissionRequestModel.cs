namespace Mission.Entities.Models.MissionsModels
{
    public class AddMissionRequestModel
    {

        public string MissionTitle { get; set; }
        public string MissionDescription { get; set; }

        public string MissionOrganisationName { get; set; }

        public string MissionOrganisationDetail { get; set; }

        public int CountryId { get; set; }

        public int CityId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string MissionType { get; set; }
        public int TotalSheets { get; set; }
        public int MissionThemeId { get; set; }
        public string MissionSkillId { get; set; }
        public string MissionImages { get; set; }
    }
}
