using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Mission.Entities.Models;
using Mission.Services.IServices;

namespace Mission.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MissionThemeController(IMissionThemeService missionThemeService) : ControllerBase
    {
        private readonly IMissionThemeService _missionThemeService = missionThemeService;
        [HttpPost]
        [Route("AddMissionTheme")]
        public async Task<IActionResult> AddMissionTheme(MissionThemeModel model)
        {
            try
            {
                var res = await _missionThemeService.AddMissionTheme(model);
                if (res == true)
                {
                    return Ok(new ResponseResult() { Data = "Add Mission theme.", Result = ResponseStatus.Success, Message = "" });
                }
                else
                {
                    return Ok(new ResponseResult() { Data = null, Result = ResponseStatus.Error, Message = "Theme already exist" });
                }
            }
            catch (Exception ex)
            {
                return BadRequest(new ResponseResult() { Data = null, Result = ResponseStatus.Error, Message = "failed to add theme" });
            }
        }

        [HttpGet]
        [Route("GetMissionThemeList")]
        public async Task<IActionResult> GetAllMissionTheme()
        {
            try
            {
                var res = await _missionThemeService.GetAllMissionThemes();
                  return Ok(new ResponseResult() { Data = res, Result = ResponseStatus.Success, Message = "" });
            }
            catch (Exception ex)
            {
                return BadRequest(new ResponseResult() { Data = null, Result = ResponseStatus.Error, Message = "failed to get theme" });
            }
        }

        [HttpGet]
        [Route("GetMissionThemeById/{id}")]
        public async Task<IActionResult> GetMissionThemeById(int id)
        {
            var res = await _missionThemeService.GetMissionThemesById(id);
            if(res == null)
                return NotFound(new ResponseResult() { Data="Not found", Result= ResponseStatus.Error,Message=""});
            return Ok(new ResponseResult() { Data = res, Result = ResponseStatus.Success, Message = "" });
        }

        [HttpPost]
        [Route("UpdateMissionTheme")]
        public async Task<IActionResult> UpdateMissionTheme(MissionThemeModel model)
        {
            var res = await _missionThemeService.UpdateMissionTheme(model);
            if(!res)
                return NotFound(new ResponseResult() { Data = "Not found", Result = ResponseStatus.Error, Message = "" });
            return Ok(new ResponseResult() { Data = res, Result = ResponseStatus.Success, Message = "updated successfully" });
        }

    }
}
