using Microsoft.AspNetCore.Mvc;
using vrms_api.Logics;

namespace vrms_api.Controllers
{
    public class MaintenanceController(IServiceProvider provider) : ShareController<MaintenanceLogic>(provider)
    {
        [HttpPost("migrate")]
        public async Task<string> ApplyMigrations()
        {
            return await _logic.UpgradeAsync();
        }
    }
}
