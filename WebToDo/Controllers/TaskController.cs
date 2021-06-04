using Core.Dto.TasksDto;
using Core.Interfaces;
using Core.Interfaces.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebToDo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskController : ControllerBase
    {
        ITaskService taskSvc { get; set; }
        IMyAuthorizationServiceSingelton myAuthorizationSvc { get; set; }

        public TaskController(
            ITaskService taskSvc,
            IMyAuthorizationServiceSingelton myAuthorizationSvc
            )
        {
            this.taskSvc = taskSvc;
            this.myAuthorizationSvc = myAuthorizationSvc;
        }

        [HttpGet]
        [Route("get-all-tasks")]
        public async Task<IActionResult> GetAllTasks()
        {
            return Ok(this.taskSvc.GetAllTasks(this.myAuthorizationSvc.UserIdAuthenticated));
        }

        [HttpPost]
        [Route("create-task")]
        public async Task<IActionResult> CreateTask(CreateTaskDto task)
        {
            var result = await this.taskSvc.AddTaskAsync(task);

            if (result != null)
                return Ok(result);

            return BadRequest();
        }

        [HttpPut]
        [Route("{taskId}/update-status")]
        public async Task<IActionResult> UpdateStatus(long taskId, bool status)
        {
            await this.taskSvc.UpdateStatus(taskId, status);

            return Ok();
        }

        [HttpPut]
        [Route("{taskId}/update-favorite")]
        public async Task<IActionResult> UpdateFavorite(long taskId, bool favorite)
        {
            await this.taskSvc.UpdateFavorite(taskId, favorite);

            return Ok();
        }

        [HttpPut]
        [Route("{taskId}/update-name")]
        public async Task<IActionResult> UpdateName(long taskId, string name)
        {
            await this.taskSvc.UpdateName(taskId, name);

            return Ok();
        }

        [HttpDelete]
        [Route("{taskId}/delete")]
        public async Task<IActionResult> Delete(long taskId)
        {
            if (await this.taskSvc.Delete(taskId))
                return Ok();
            else
                return BadRequest();
        }

        [HttpDelete]
        [Route("delete")]
        public async Task<IActionResult> Delete([FromBody]List<long> taskIds)
        {
            if (await this.taskSvc.Delete(taskIds))
                return Ok();
            else
                return BadRequest();
        }
    }
}
