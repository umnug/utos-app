using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.OData;
using Microsoft.Azure.Mobile.Server;
using UtahOpenSource.Backend.Models;
using UtahOpenSource.DataObjects;
using UtahOpenSource.Backend.Identity;

namespace UtahOpenSource.Backend.Controllers
{
    public class NotificationController : TableController<Notification>
    {
        protected override void Initialize(HttpControllerContext controllerContext)
        {
            base.Initialize(controllerContext);
            UtahOpenSourceContext context = new UtahOpenSourceContext();
            DomainManager = new EntityDomainManager<Notification>(context, Request, true);
        }

        public IQueryable<Notification> GetAllNotification()
        {
            return Query(); 
        }

        public SingleResult<Notification> GetNotification(string id)
        {
            return Lookup(id);
        }

        [EmployeeAuthorize]
        public Task<Notification> PatchNotification(string id, Delta<Notification> patch)
        {
             return UpdateAsync(id, patch);
        }

        [EmployeeAuthorize]
        public async Task<IHttpActionResult> PostNotification(Notification item)
        {
            Notification current = await InsertAsync(item);
            return CreatedAtRoute("Tables", new { id = current.Id }, current);
        }

        [EmployeeAuthorize]
        public Task DeleteNotification(string id)
        {
             return DeleteAsync(id);
        }
    }
}
