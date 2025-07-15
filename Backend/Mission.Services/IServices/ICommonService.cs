
using Mission.Entities.Models.CommonModel;

namespace Mission.Services.IServices
{
    public interface ICommonService
    {
        List<DropDownResponseModel> CountryList();

        List<DropDownResponseModel> CityList(int countryId);
    }
}
