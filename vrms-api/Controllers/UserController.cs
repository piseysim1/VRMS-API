using Microsoft.AspNetCore.Mvc;
using vrms_api.Logics;
using vrms_api.Models;
using static vrms_api.Models.ShareModel;

namespace vrms_api.Controllers
{
    public class UserController(IServiceProvider provider) : ShareController<UserLogic>(provider)
    {
        [HttpGet()]
        public async Task<SearchResult<UserListModel, QueryParamModel>> SearchAsync(QueryParamModel param)
        {
            return await _logic.SearchAsync(param);
        }
        [HttpGet("id")]
        public async Task<IActionResult> FindAsync(long id)
        {
            var result = await _logic.FindAsync(id);
            return Ok(result);

        }
        [HttpPost]
        public async Task<IActionResult> UserCreateAsync(UserAddModel model)
        {
            var result = await _logic.AddAsync(model);
            return Ok(result);
        }
        [HttpPut("id")]
        public async Task<IActionResult> UserEditAsync(UserEditModel model)
        {
            var result = await _logic.EditAsync(model);
            return Ok(result);
        }
        [HttpDelete("id")]
        public async Task<IActionResult> UserRemoveAsync(RemoveModel model)
        {
            var result = await _logic.RemoveAsync(model);
            return Ok(result);
        }
    }
}
