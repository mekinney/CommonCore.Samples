using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Caching.Memory;
using todo.webapi.Data;
using todo.webapi.Hubs;

namespace todo.webapi.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class TodoController : Controller
    {
        private readonly ITodoRepository _todoRepo;
        private readonly IHubContext<DataNotificationHub> _hub;
        private readonly IMemoryCache _cache;

        public TodoController(ITodoRepository todoRepo, IHubContext<DataNotificationHub> hub, IMemoryCache cache)
        {
            _todoRepo = todoRepo;
            _hub = hub;
            _cache = cache;
        }

        [HttpGet]
        [Route("api/Todo/GetAll")]
        public async Task<List<Todo>> GetAll()
        {
            return await _todoRepo.GetAll();
        }


        [HttpGet]
        [Route("api/Todo/GetAllUpdatedByUser")]
        public async Task<List<Todo>> GetAllUpdatedByUser(int userId, long utcTickStamp)
        {
            return await _todoRepo.GetAllUpdatedByUser(userId, utcTickStamp);
        }

        [HttpPost]
        [Route("api/Todo/AddOrUpdate")]
        public async Task<IActionResult> AddOrUpdate([FromBody] Todo obj)
        {
            var response = await _todoRepo.AddOrUpdate(obj);

            if (response.error != null)
                return BadRequest(response.error);
            else
                return Ok(response.dict);
        }

        [HttpPost]
        [Route("api/Todo/AddOrUpdateCollection")]
        public async Task<IActionResult> AddOrUpdateCollection([FromBody] List<Todo> obj)
        {
            var response = await _todoRepo.AddOrUpdateCollection(obj);

            if (response.error != null)
                return BadRequest(response.error);
            else
                return Ok(response.dict);
        }

        [HttpGet]
        [Route("api/Todo/Delete")]
        public async Task<IActionResult> Delete(int id)
        {
            var response = await _todoRepo.Delete(id);

            if (response.error != null)
                return BadRequest(response.error);
            else
                return Ok(response.dict);
        }

        [HttpGet]
        [Route("api/Todo/TestUpdate")]
        public async Task<IActionResult> TestUpdate(int id, string text, long timeStamp, bool markedForDelete=false)
        {
            var response = await _todoRepo.GetById(id);
            if(response!=null){
                response.Description = text;
                response.UTCTickStamp = timeStamp == default(long) ? DateTime.UtcNow.Ticks : timeStamp;
                response.MarkedForDelete = markedForDelete;
            }

            var updateResponse = await _todoRepo.AddOrUpdate(response);

            if (updateResponse.error != null)
                return BadRequest(updateResponse.error);
            else
                return Ok(updateResponse.dict);
        }


        [HttpGet]
        [AllowAnonymous]
        [Route("api/Todo/NotifyUser")]
        public async Task<IActionResult> NotifyUser(int userId)
        {
            string connectionId = null;
            if (_cache.TryGetValue(userId, out connectionId))
            {
                if(!string.IsNullOrEmpty(connectionId)){
                    await this._hub.Clients.Client(connectionId).InvokeAsync("Send", "Update Occurred");
                }
            }
            return Ok();
        }

    }
}
