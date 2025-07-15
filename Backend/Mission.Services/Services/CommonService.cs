using Mission.Entities.Models.CommonModel;
using Mission.Repositories.IRepositories;
using Mission.Services.IServices;

namespace Mission.Services.Services
{
    public class CommonService(ICommonRepository commonRepository) : ICommonService
    {
        private readonly ICommonRepository _commonRepository = commonRepository;

        public List<DropDownResponseModel> CountryList()
        {
            return _commonRepository.CountryList();
        }

        public List<DropDownResponseModel> CityList(int countryId)
        {
            return _commonRepository.CityList(countryId);
        }
    }
}
