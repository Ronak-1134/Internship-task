using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Mission.Entities.Models;
using Mission.Services.IServices;

namespace Mission.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminUserController(IAdminUserService _adminUserService) : ControllerBase
    {
        [HttpGet]
        [Route("UserDetailList")]
        public ActionResult UserDetailList()
        {
            try
            {
                var res = _adminUserService.UserDetailsList();
                return Ok(new ResponseResult() { Data = res ,Result = ResponseStatus.Success , Message= ""});   
            }
            catch (Exception ex)
            {
                return BadRequest(new ResponseResult() { Data = null, Result = ResponseStatus.Error, Message = "fail to get user" });
            }
        }

        [HttpDelete]
        [Route("DeleteUser/{id}")]
        public ActionResult Delete(int id)
        {
            try
            {
                var res = _adminUserService.Userdelete(id);
                return Ok(new ResponseResult() { Data = res, Result = ResponseStatus.Success, Message = "" });
            }
            catch (Exception ex)
            {
                return BadRequest(new ResponseResult() { Data = null, Result = ResponseStatus.Error, Message = "fail to delete user" });
            }
        }
    }
}
