using Microsoft.AspNetCore.Mvc;
using vrms_api.Logics;
using vrms_api.Models;
using static vrms_api.Models.ShareModel;

namespace vrms_api.Controllers
{
    public class PaymentMethodController(IServiceProvider provider) : ShareController<PaymentMethodLogic>(provider)
    {
        [HttpGet()]
        public async Task<SearchResult<PaymentMethodListModel, QueryParamModel>> SearchAsync(QueryParamModel param)
        {




            return await _logic.SearchAsync(param);
        }
        [HttpGet("id")]
        public async Task<IActionResult> PaymentMethodFindAsync(long id)
        {
            var result = await _logic.FindAsync(id);
            return Ok(result);

        }
        [HttpPost]
        public async Task<IActionResult> PaymentMethodAddAsync(PaymentMethodAddModel model)
        {
            var result = await _logic.AddPaymentMethodAsync(model);
            return Ok(result);
        }
        [HttpPut("id")]
        public async Task<IActionResult> PaymentMethodEditAsync(PaymentMethodEditModel model)
        {
            var result = await _logic.EditPaymentMethodAsync(model);
            return Ok(result);
        }
        [HttpDelete("id")]
        public async Task<IActionResult> PaymentMethodRemove(RemoveModel model)
        {
            var result = await _logic.RemovePaymentMethodAsync(model);
            return Ok(result);
        }
    }
}
