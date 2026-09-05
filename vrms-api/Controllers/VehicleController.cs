using vrms_api.Logics;

namespace vrms_api.Controllers
{
    public class VehicleController(IServiceProvider provider):ShareController<VehicleLogic>(provider)
    {
    }
}
