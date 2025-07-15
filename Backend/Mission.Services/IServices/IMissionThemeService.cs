using Mission.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mission.Services.IServices
{
    public interface IMissionThemeService
    {
        Task<bool> AddMissionTheme(MissionThemeModel model);
        Task<List<MissionThemeModel>> GetAllMissionThemes();
        Task<MissionThemeModel> GetMissionThemesById(int id);
        Task<bool> UpdateMissionTheme(MissionThemeModel model);
    }
}
