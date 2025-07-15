using Microsoft.AspNetCore.Mvc;
using Mission.Entities.Models;
using Mission.Entities.Models.MissionsModels;
using Mission.Services.IServices;

namespace Mission.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientMissionController(IMissionService missionService) : ControllerBase
    {
        private readonly IMissionService _missionService = missionService;
        ResponseResult result = new ResponseResult();

        [HttpGet]
        [Route("ClientSideMissionList/{userId}")]
        public ResponseResult ClientMissionList(int userId)
        {
            try
            {
                result.Data = _missionService.ClientMissionList(userId);
                result.Result = ResponseStatus.Success;
            }
            catch (Exception ex)
            {
                result.Result = ResponseStatus.Error;
                result.Message = ex.Message;
            }
            return result;
        }


        [HttpPost]
        [Route("ApplyMission")]
        public ResponseResult ApplyMission(ApplyMissionRequestModel request)
        {
            try
            {
                result.Data = _missionService.ApplyMission(request);
                result.Result = ResponseStatus.Success;
            }
            catch (Exception ex)
            {
                result.Result = ResponseStatus.Error;
                result.Message = ex.Message;
            }
            return result;
        }
    }
}
