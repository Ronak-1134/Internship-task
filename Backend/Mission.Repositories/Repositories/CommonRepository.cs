using Mission.Entities.context;
using Mission.Entities.Models.CommonModel;
using Mission.Repositories.IRepositories;

namespace Mission.Repositories.Repositories
{
    public class CommonRepository(MissionDbContext cIDbContext) : ICommonRepository
    {
        private readonly MissionDbContext _cIDbContext = cIDbContext;

        public List<DropDownResponseModel> CountryList()
        {
            var countries = _cIDbContext.Country
                .OrderBy(c => c.CountryName)
                .Select(c => new DropDownResponseModel(c.Id, c.CountryName))
                .ToList();

            return countries;
        }

        public List<DropDownResponseModel> CityList(int countryId)
        {
            var cities = _cIDbContext.City
                .Where(c => c.CountryId == countryId)
                .OrderBy(c => c.CityName)
                .Select(c => new DropDownResponseModel(c.Id, c.CityName))
                .ToList();

            return cities;
        }
    }
}
