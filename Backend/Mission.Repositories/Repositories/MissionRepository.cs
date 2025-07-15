using Microsoft.EntityFrameworkCore;
using Mission.Entities.context;
using Mission.Entities.Entities;
using Mission.Entities.Models.MissionsModels;
using Mission.Repositories.IRepositories;
using System.Globalization;
using System.Numerics;

namespace Mission.Repositories.Repositories
{
    public class MissionRepository(MissionDbContext dbContext) : IMissionRepository
    {
        private readonly MissionDbContext _dbContext = dbContext;
        private const int MISSION_APPLICATION_SEAT = 1;

        public List<MissionResponseModel> ClientMissionList(int userId)
        {
            var missions = _dbContext.Missions
                            .Where(x => !x.IsDeleted)
                            .OrderBy(x => x.StartDate)
                            .Select(m => new MissionResponseModel()
                            {
                                Id = m.Id,
                                MissionTitle = m.MissionTitle,
                                MissionDescription = m.MissionDescription,
                                MissionOrganisationName = m.MissionOrganisationName,
                                MissionOrganisationDetail = m.MissionOrganisationDetail,
                                CountryId = m.CountryId,
                                CityId = m.CityId,
                                StartDate = m.StartDate,
                                EndDate = m.EndDate,
                                MissionType = m.MissionType,
                                TotalSheets = m.TotalSheets,
                                MissionThemeId = m.MissionThemeId,
                                MissionSkillId = m.MissionSkillId,
                                MissionImages = m.MissionImages,
                                CountryName = m.Country.CountryName,
                                CityName = m.City.CityName,
                                MissionThemeName = m.MissionTheme.ThemeName,
                                MissionSkillName = string.Join(",", _dbContext.MissionSkill
                                    .Where(ms => m.MissionSkillId.Contains(ms.Id.ToString()))
                                    .Select(ms => ms.SkillName)
                                    .ToList()),
                                MissionApplyStatus = _dbContext.MissionApplications.Any(ma => ma.UserId == userId && ma.MissionId == m.Id) ? "Applied" : "Apply",
                            }).ToList();

            return missions;
        }

        public List<MissionResponseModel> MissionList()
        {
            var missions = _dbContext.Missions.Where(x => !x.IsDeleted)
                .Select(x => new MissionResponseModel()
                {
                    Id = x.Id,
                    CityId = x.CityId,
                    CityName = x.City.CityName,
                    CountryId = x.CountryId,
                    CountryName = x.Country.CountryName,
                    EndDate = x.EndDate,
                    MissionDescription = x.MissionDescription,
                    MissionImages = x.MissionImages,
                    MissionOrganisationDetail = x.MissionOrganisationDetail,
                    MissionOrganisationName = x.MissionOrganisationName,
                    MissionSkillId = x.MissionSkillId,
                    MissionThemeId = x.MissionThemeId,
                    MissionThemeName = x.MissionTheme.ThemeName,
                    MissionTitle = x.MissionTitle,
                    MissionType = x.MissionType,
                    StartDate = x.StartDate,
                    TotalSheets = x.TotalSheets
                }).ToList();
            return missions;
        }

        public string AddMission(AddMissionRequestModel request)
        {

            var exists = _dbContext.Missions.Any(x => x.MissionTitle.ToLower() == request.MissionTitle.ToLower()
                                                        && x.CityId == request.CityId
                                                        && x.StartDate.Date == request.StartDate.Date
                                                        && x.EndDate.Date == request.EndDate.Date && !x.IsDeleted);
            if (exists)
            {
                throw new Exception("Mission already exist");
            }

            var mission = new Missions()
            {
                MissionDescription = request.MissionDescription,
                MissionImages = request.MissionImages,
                CityId = request.CityId,
                CountryId = request.CountryId,
                MissionOrganisationDetail = request.MissionOrganisationDetail,
                MissionOrganisationName = request.MissionOrganisationName,
                MissionSkillId = request.MissionSkillId,
                MissionThemeId = request.MissionThemeId,
                MissionTitle = request.MissionTitle,
                MissionType = request.MissionType,
                StartDate = request.StartDate,
                EndDate = request.EndDate,
                CreatedDate = DateTime.UtcNow,
                TotalSheets = request.TotalSheets,
                IsDeleted = false
            };




            _dbContext.Missions.Add(mission);
            _dbContext.SaveChanges();
            return "Mission Save Successfully";
        }

        public MissionResponseModel GetMissionById(int missionId)
        {
            
            return _dbContext.Missions
                .Where(x => x.Id == missionId && !x.IsDeleted)
                .Select(x => new MissionResponseModel
                {
                    Id = x.Id,
                    CityId = x.CityId,
                    CityName = x.City.CityName,
                    CountryId = x.CountryId,
                    CountryName = x.Country.CountryName,
                    EndDate = x.EndDate,
                    MissionDescription = x.MissionDescription,
                    MissionImages = x.MissionImages,
                    MissionOrganisationDetail = x.MissionOrganisationDetail,
                    MissionOrganisationName = x.MissionOrganisationName,
                    MissionSkillId = x.MissionSkillId,
                    MissionThemeId = x.MissionThemeId,
                    MissionThemeName = x.MissionTheme.ThemeName,
                    MissionTitle = x.MissionTitle,
                    MissionType = x.MissionType,
                    StartDate = x.StartDate,
                    TotalSheets = x.TotalSheets
                }).FirstOrDefault() ?? throw new Exception("Mission not found");
        }

        public string DeleteMissionById(int missionId)
        {
            var mission = _dbContext.Missions.Where(x => x.Id == missionId && !x.IsDeleted).ExecuteUpdate(x => x.SetProperty(p => p.IsDeleted, true));
            return "Mission deleted successfully";
        }

        public string ApplyMission(ApplyMissionRequestModel request)
        {

            var mission = _dbContext.Missions.Where(x => x.Id == request.MissionId && !x.IsDeleted).FirstOrDefault();

            if (mission == null) { throw new Exception("Mission Not Found"); }

            if (mission.TotalSheets == 0) { throw new Exception("Mission housefull"); }

            if (mission.TotalSheets < request.Sheets) { throw new Exception("Not available seats"); }

            var missionApplication = new MissionApplication()
            {
                MissionId = request.MissionId,
                UserId = request.UserId,
                AppliedDate = request.AppliedDate,
                Status = request.Status,
                Sheet = MISSION_APPLICATION_SEAT,
                CreatedDate = DateTime.UtcNow,
                IsDeleted = false
            };

            //Add new mission application
            _dbContext.MissionApplications.Add(missionApplication);
            _dbContext.SaveChanges();


            mission.TotalSheets -= MISSION_APPLICATION_SEAT;

            _dbContext.Missions.Update(mission);
            _dbContext.SaveChanges();

            return "Mission Application Successfull!";

        }

        public string ApproveMission(int id)
        {
            var exists = _dbContext.MissionApplications.Any(x => x.Id == id);

            if (!exists) { throw new Exception("Mission application not exist"); }

            var updateCount = _dbContext.MissionApplications.Where(x => x.Id == id).ExecuteUpdate(m => m.SetProperty(property => property.Status, true));

            return updateCount > 0 ? "Mission approved" : "Mission is not approved";
        }


        public List<MissionApplicationResponseModel> MissionApplicationList()
        {
            return _dbContext.MissionApplications
                .Where(m => !m.IsDeleted && !m.Mission.IsDeleted && !m.User.IsDeleted && !m.Status)
                .Select(m => new MissionApplicationResponseModel()
                {
                    Id = m.Id,
                    AppliedDate = m.AppliedDate,
                    MissionId = m.MissionId,
                    MissionTheme = m.Mission.MissionTheme.ThemeName,
                    MissionTitle = m.Mission.MissionTitle,
                    Sheets = m.Sheet,
                    Status = m.Status,
                    UserId = m.UserId,
                    UserName = $"{m.User.FirstName} {m.User.LastName}",
                }).ToList();
        }

        public string DeleteMissionApplication(int id)
        {
            var mission = _dbContext.MissionApplications.Where(x => x.Id == id && !x.IsDeleted).ExecuteUpdate(x => x.SetProperty(p => p.IsDeleted, true));
            return "Mission application deleted successfully";
        }
    }
}
