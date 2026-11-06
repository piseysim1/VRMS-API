using Microsoft.AspNetCore.Mvc;
using vrms_api.Logics;
using vrms_api.Models;
using static vrms_api.Models.ShareModel;

namespace vrms_api.Controllers
{
    public class BookingController(IServiceProvider provider): ShareController<BookingLogic>(provider)
    {
        [HttpGet()]
        public async Task<SearchResult<BookingListModel, QueryParamModel>> SearchAsync(QueryParamModel param)
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
        public async Task<IActionResult> AddAsync(BookingAddModel model)
        {
            var result = await _logic.BookingAddAsync(model);
            return Ok(result);
        }
        [HttpPut("id")]
        public async Task<IActionResult> EditAsync(BookingEditModel model)
        {
            var result = await _logic.BookingEditAsync(model);
            return Ok(result);
        }
        [HttpDelete("id")]
        public async Task<IActionResult> RemoveAsync(RemoveModel model)
        {
            var result = await _logic.BookingRemoveAsync(model);
            return Ok(result);
        }
        [HttpPut("status")]
        public async Task<IActionResult> Status(BookingStatusModel m) => Ok(await _logic.ChangeStatusAsync(m));
    }
}
