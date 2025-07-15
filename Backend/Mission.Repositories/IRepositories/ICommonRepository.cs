using Mission.Entities.Models.CommonModel;

namespace Mission.Repositories.IRepositories
{
    public interface ICommonRepository
    {
        List<DropDownResponseModel> CountryList();

        List<DropDownResponseModel> CityList(int countryId);

    }
}
