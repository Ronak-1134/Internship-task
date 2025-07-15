namespace Mission.Entities.Models.MissionsModels
{
    public class MissionApplicationResponseModel
    {           
        public int Id { get; set; } 
        public int MissionId { get; set; }
        public int UserId { get; set; }
        public string UserName { get; set; } = string.Empty;
        public string MissionTitle { get; set; } = string.Empty;
        public string MissionTheme { get; set; } = string.Empty;
        public DateTime AppliedDate { get; set; }

        public int Sheets { get; set; }
        public bool Status { get; set; }
    }
}
