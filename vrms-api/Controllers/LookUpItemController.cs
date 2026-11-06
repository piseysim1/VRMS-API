using Microsoft.AspNetCore.Mvc;
using vrms_api.Logics;
using vrms_api.Models;
using static vrms_api.Models.ShareModel;

namespace vrms_api.Controllers
{
    public class LookUpItemController(IServiceProvider provider) : ShareController<LookUpItemLogic>(provider)
    {
        [HttpGet("{id}")]
        public async Task<ViewResult<LookUpItemViewModel>> FindAsync(long id)
        {
            return await _logic.FindAsync(id);
        }

        [HttpPost()]
        public async Task<ViewResult<LookUpItemViewModel>> PostAsync(LookUpItemAddModel model)
        {
            return await _logic.AddAsync(model);
        }

        [HttpPut("{id}")]
        public async Task<ViewResult<LookUpItemViewModel>> EditAsync(LookUpItemEditModel model)
        {
            return await _logic.EditAsync(model);
        }
        [HttpDelete("{id}")]
        public async Task<ViewResult<LookUpItemViewModel>> RemoveAsync(RemoveModel model)
        {
            return await  _logic.RemoveModelAsync(model);
        }

    }
}
