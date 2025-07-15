using Mission.Entities.Entities;
using Mission.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mission.Repositories.IRepositories
{
    public interface IMissionThemeRepository
    {
        Task<bool> AddMissionTheme(MissionTheme missionTheme);
        Task<List<MissionThemeModel>> GetAllMissionTheme();
        Task<MissionThemeModel> GetMissionThemeById(int id);
        Task<bool> UpdateMissionTheme(MissionTheme missionTheme);
    }
}
