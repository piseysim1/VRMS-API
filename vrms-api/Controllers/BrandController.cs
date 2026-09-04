using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Services;
using vrms_api.Logics;
using vrms_api.Models;
using static vrms_api.Models.ShareModel;

namespace vrms_api.Controllers
{
    
    public class BrandController(IServiceProvider provider): ShareController<BrandLogic>(provider)
    {
        
        [HttpGet()]
        public async Task<SearchResult<BrandListModel , QueryParamModel>> SearchAsync (QueryParamModel param)
        {
            return await _logic.SearchAsync(param);
        }

        [HttpGet("id")]
        public async Task<IActionResult> BrandFindAsync(long id)
        {
            var result = await _logic.FindAsync(id);
            return Ok(result);
        }
        [HttpPost()]
        public async Task<IActionResult> BrandAddAsync(BrandAddModel model)
        {
            var result = await _logic.BrandAddAsync(model);
            return Ok(result);
        }
        [HttpPut("id")]
        public async Task<IActionResult> BrandEditAsync(BrandEditModel model)
        {
            var result = await _logic.BrandEditAsync(model);
            return Ok(result);
        }
        [HttpDelete("id")]
        public async Task<IActionResult> BrandRemoveAsync(RemoveModel model)
        {
            var result = await _logic.BrandRemoveAsync(model);
            return Ok(result);
        }

    }
}
