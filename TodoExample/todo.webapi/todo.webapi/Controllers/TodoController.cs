using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using todo.webapi.Data;

namespace todo.webapi.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class TodoController : Controller
    {
        private readonly ITodoRepository _todoRepo;

        public TodoController(ITodoRepository todoRepo)
        {
            _todoRepo = todoRepo;
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

    }
}
