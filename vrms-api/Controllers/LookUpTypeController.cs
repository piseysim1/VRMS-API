using Microsoft.AspNetCore.Mvc;
using vrms_api.Logics;
using vrms_api.Models;
using static vrms_api.Models.ShareModel;

namespace vrms_api.Controllers
{
    public class LookUpTypeController(IServiceProvider provider): ShareController<LookUpTypeLogic>(provider)
    {
        [HttpGet("{id}")]
        public async Task<LookUpTypeViewModel> FindAsync(long id)
        {
            return await _logic.FindAsync(id);
        }

        [HttpPost()]
        public async Task<LookUpTypeViewModel> PostAsync(LookUpTypeAddModel model)
        {
            return await _logic.AddAsync(model);
        }

        [HttpPut("{id}")]
        public async Task<LookUpTypeViewModel> EditAsync(LookUpTypeEditModel model)
        {
            return await _logic.EditAsync(model);
        }
        [HttpDelete("{id}")]
        public async Task<LookUpTypeViewModel> RemoveAsync(RemoveModel model)
        {
            return await _logic.RemoveModelAsync(model);
        }
    }
}
