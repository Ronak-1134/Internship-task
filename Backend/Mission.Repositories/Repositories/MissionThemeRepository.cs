using Microsoft.EntityFrameworkCore;
using Mission.Entities.context;
using Mission.Entities.Entities;
using Mission.Entities.Models;
using Mission.Repositories.IRepositories;

namespace Mission.Repositories.Repositories
{
    public class MissionThemeRepository(MissionDbContext missionDbContext) : IMissionThemeRepository
    {
        private readonly MissionDbContext _missionDbContext = missionDbContext;

        public async Task<bool> AddMissionTheme(MissionTheme missionTheme)
        {
            bool alreadyExist = await _missionDbContext.MissionTheme.AnyAsync(m=>m.ThemeName.ToLower() == missionTheme.ThemeName.ToLower() && !m.IsDeleted);
            if (alreadyExist)
            {
                return false;
            }

            await _missionDbContext.MissionTheme.AddAsync(missionTheme);
            await _missionDbContext.SaveChangesAsync();
            return true;
        }

        public Task<List<MissionThemeModel>> GetAllMissionTheme()
        {
            return _missionDbContext.MissionTheme.Where(m=>!m.IsDeleted).Select(m=> new MissionThemeModel()
            {
                Id = m.Id,
                ThemeName = m.ThemeName,
                Status = m.Status,
            }).ToListAsync();
        }

        public Task<MissionThemeModel> GetMissionThemeById(int id)
        {
            return _missionDbContext.MissionTheme.Where(m =>m.Id == id && !m.IsDeleted).Select(m => new MissionThemeModel()
            {
                Id = m.Id,
                ThemeName = m.ThemeName,
                Status = m.Status,
            }).FirstOrDefaultAsync();
        }

        public async Task<bool> UpdateMissionTheme(MissionTheme missionTheme)
        {
            var missionthemeExist = await _missionDbContext.MissionTheme.FirstOrDefaultAsync(m=>m.Id == missionTheme.Id && !m.IsDeleted);
            if (missionthemeExist == null) { 
            return false;}

            missionthemeExist.ThemeName = missionTheme.ThemeName;
            missionthemeExist.Status = missionTheme.Status;
            missionthemeExist.ModifiedDate = DateTime.UtcNow;

            await _missionDbContext.SaveChangesAsync();
            return true;
        }
    }
}
