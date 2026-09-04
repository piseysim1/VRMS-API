using Microsoft.AspNetCore.Mvc;
using vrms_api.Logics;
using vrms_api.Models;
using static vrms_api.Models.ShareModel;

namespace vrms_api.Controllers
{
    public class LocationController(IServiceProvider provider) : ShareController<LocationLogic>(provider)
    {
        [HttpGet()]
        public async Task<SearchResult<LocationListModel, QueryParamModel>> SearchAsync(QueryParamModel param)
        {
            return await _logic.SearchAsync(param);
        }
        [HttpGet("id")]
        public async Task<IActionResult> LocationFindAsync(long id)
        {
            var result = await _logic.FindAsync(id);
            return Ok(result);

        }
        [HttpPost]
        public async Task<IActionResult> LocationAddAsync(LocationAddModel model)
        {
            var result = await _logic.AddLocationAsync(model);
            return Ok(result);
        }
        [HttpPut("id")]
        public async Task<IActionResult> LocationEditAsync(LocationEditModel model)
        {
            var result = await _logic.EditLocationAsync(model);
            return Ok(result);
        }
        [HttpDelete("id")]
        public async Task<IActionResult> LocationRemoveAsync(RemoveModel model)
        {
            var result = await _logic.RemoveLocationAsync(model);
            return Ok(result);
        }
    }
}
