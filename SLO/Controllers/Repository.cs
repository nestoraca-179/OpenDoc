using SLO.Models;

namespace SLO.Controllers
{
    public abstract class Repository
    {
        public readonly static SLOEntities db = new SLOEntities();
    }
}