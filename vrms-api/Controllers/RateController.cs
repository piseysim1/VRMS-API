using vrms_api.Logics;

namespace vrms_api.Controllers
{
    public class RateController(IServiceProvider provider):ShareController<RateLogic>(provider)
    {
    }
}
