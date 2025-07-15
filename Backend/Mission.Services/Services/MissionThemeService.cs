using Mission.Entities.Entities;
using Mission.Entities.Models;
using Mission.Repositories.IRepositories;
using Mission.Services.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mission.Services.Services
{
    public class MissionThemeService(IMissionThemeRepository missionThemeRepository) : IMissionThemeService
    {
        private readonly IMissionThemeRepository _missionThemeRepository = missionThemeRepository;

        public Task<bool> AddMissionTheme(MissionThemeModel model)
        {
            MissionTheme missionTheme = new MissionTheme()
            {
                Id = model.Id,
                Status = model.Status,
                ThemeName = model.ThemeName,
            };
            return _missionThemeRepository.AddMissionTheme(missionTheme);
        }
        public Task<List<MissionThemeModel>> GetAllMissionThemes() { 
            return _missionThemeRepository.GetAllMissionTheme();
        }
        public Task<MissionThemeModel> GetMissionThemesById(int id)
        {
            return _missionThemeRepository.GetMissionThemeById(id);
        }
        public Task<bool> UpdateMissionTheme(MissionThemeModel model)
        {
            MissionTheme missionTheme = new MissionTheme()
            {
                Id = model.Id,
                Status = model.Status,
                ThemeName = model.ThemeName,
            };
            return _missionThemeRepository.UpdateMissionTheme(missionTheme);
        }

    }
}
