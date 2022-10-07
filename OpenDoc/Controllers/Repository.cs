using OpenDoc.Models;

namespace OpenDoc.Controllers
{
    public abstract class Repository
    {
        public readonly static OpenDocEntities db = new OpenDocEntities();
    }
}