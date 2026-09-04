using Microsoft.AspNetCore.Mvc;
using vrms_api.Logics;
using vrms_api.Models;
using static vrms_api.Models.ShareModel;

namespace vrms_api.Controllers
{
    public class ModelController(IServiceProvider provider) : ShareController<ModelLogic>(provider)
    {
        [HttpGet()]
        public async Task<SearchResult<ModelVehicleListModel, QueryParamModel>> SearchAsync(QueryParamModel param)
        {
            return await _logic.SearchAsync(param);
        }
        [HttpGet("id")]
        public async Task<IActionResult> ModelFindAsync(long id)
        {
            var result = await _logic.FindAsync(id);
            return Ok(result);

        }
        [HttpPost]
        public async Task<IActionResult> ModelAddAsync(ModelVehicleAddModel model)
        {
            var result = await _logic.AddModelAsync(model);
            return Ok(result);
        }
        [HttpPut("id")]
        public async Task<IActionResult> ModelEditAsync(ModelVehicleEditModel model)
        {
            var result = await _logic.EditModelAsync(model);
            return Ok(result);
        }
        [HttpDelete("id")]
        public async Task<IActionResult> ModelRemoveAsync(RemoveModel model)
        {
            var result = await _logic.RemoveModelAsync(model);
            return Ok(result);
        }
    }
   
}
