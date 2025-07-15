using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting.Internal;
using Mission.Entities.DTO;
using Mission.Entities.Entities;
using Mission.Entities.Models;
using Mission.Entities.Models.CommonModel;
using Mission.Entities.Models.MissionsModels;
using Mission.Services.IServices;
using System.Net.Http.Headers;

namespace Mission.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MissionController(IMissionService missionService, IWebHostEnvironment hostingEnvironment) : ControllerBase
    {
        private readonly IWebHostEnvironment _hostingEnvironment = hostingEnvironment;
        private readonly IMissionService _missionService = missionService;
        ResponseResult result = new ResponseResult();

        [HttpGet]
        [Route("MissionList")]
        public ResponseResult MissionList()
        {
            try
            {
                result.Data = _missionService.GetMissionList();
                result.Result = ResponseStatus.Success;
            }
            catch (Exception ex)
            {
                result.Result = ResponseStatus.Error;
                result.Message = ex.Message;
            }
            return result;
        }

        [HttpGet]
        [Route("MissionDetailsById/{id}")]
        public ResponseResult MissionDetailsById(int id)
        {
            try
            {
                result.Data = _missionService.GetMissionById(id);
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
        [Route("AddMission")]
        public ResponseResult AddMission(AddMissionRequestModel request)
        {
            try
            {
                if (request.EndDate.Date < request.StartDate.Date) { throw new Exception("Selected end date must be greater then start date"); }
                ;

                result.Data = _missionService.AddMission(request);
                result.Result = ResponseStatus.Success;
            }
            catch (Exception ex)
            {
                result.Result = ResponseStatus.Error;
                result.Message = ex.Message;
            }
            return result;
        }

        [HttpDelete]
        [Route("DeleteMission/{id}")]
        public ResponseResult DeleteMission(int id)
        {
            try
            {
                result.Data = _missionService.DeleteMission(id);
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
        [Route("UploadImage")]
        public async Task<IActionResult> UploadImage([FromForm] UploadFileRequestModel upload)
        {
            string filePath = "";
            string fullPath = "";
            List<string> fileList = new List<string>();
            var files = Request.Form.Files;
            if (files != null && files.Count > 0)
            {
                foreach (var file in files)
                {
                    string fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
                    filePath = Path.Combine("UploadMissionImage", upload.ModuleName);
                    string fileRootPath = Path.Combine(_hostingEnvironment.WebRootPath, "UploadMissionImage", upload.ModuleName);

                    if (!Directory.Exists(fileRootPath))
                    {
                        Directory.CreateDirectory(fileRootPath);
                    }

                    string name = Path.GetFileNameWithoutExtension(fileName);
                    string extension = Path.GetExtension(fileName);
                    string fullFileName = name + "_" + DateTime.Now.ToString("yyyyMMddHHmmss") + extension;
                    fullPath = Path.Combine(filePath, fullFileName);
                    string fullRootPath = Path.Combine(fileRootPath, fullFileName);
                    using (var stream = new FileStream(fullRootPath, FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
                    }
                    fileList.Add(fullPath);
                }
            }
            return Ok(new { success = true, Data = fileList });
        }

        [HttpPost]
        [Route("MissionApplicationApprove")]
        public ResponseResult MissionApplicationApprove(MissionApplicationApproveDto missionApplication)
        {
            try
            {
                result.Data = _missionService.ApproveMission(missionApplication.Id);
                result.Result = ResponseStatus.Success;
            }
            catch (Exception ex)
            {
                result.Result = ResponseStatus.Error;
                result.Message = ex.Message;
            }
            return result;
        }

        [HttpGet]
        [Route("MissionApplicationList")]
        public ResponseResult MissionApplicationList()
        {
            try
            {
                result.Data = _missionService.MissionApplicationList();
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
        [Route("MissionApplicationDelete")]
        public ResponseResult MissionApplicationDelete(MissionApplication application)
        {
            try
            {
                result.Data = _missionService.DeleteMissionApplication(application.Id);
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
