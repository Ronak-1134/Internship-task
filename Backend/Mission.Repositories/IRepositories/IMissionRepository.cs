using Mission.Entities.Models.MissionsModels;

namespace Mission.Repositories.IRepositories
{
    public interface IMissionRepository
    {
        List<MissionResponseModel> MissionList();
        string AddMission(AddMissionRequestModel request);
        MissionResponseModel GetMissionById(int missionId);
        string DeleteMissionById(int missionId);

        List<MissionResponseModel> ClientMissionList(int userId);

        string ApplyMission(ApplyMissionRequestModel request);
        string ApproveMission(int id);
        string DeleteMissionApplication(int id);
        List<MissionApplicationResponseModel> MissionApplicationList();
    }
}
