using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.OData;
using Microsoft.Azure.Mobile.Server;
using UtahOpenSource.Backend.Models;
using UtahOpenSource.DataObjects;
using UtahOpenSource.Backend.Identity;
using UtahOpenSource.Backend.Helpers;

namespace UtahOpenSource.Backend.Controllers
{
    public class FeaturedEventController : TableController<FeaturedEvent>
    {
        protected override void Initialize(HttpControllerContext controllerContext)
        {
            base.Initialize(controllerContext);
            UtahOpenSourceContext context = new UtahOpenSourceContext();
            DomainManager = new EntityDomainManager<FeaturedEvent>(context, Request, true);
        }

        [QueryableExpand("Sponsor")]
        public IQueryable<FeaturedEvent> GetAllFeaturedEvent()
        {
            return Query(); 
        }

        [QueryableExpand("Sponsor")]
        public SingleResult<FeaturedEvent> GetFeaturedEvent(string id)
        {
            return Lookup(id);
        }

        [EmployeeAuthorize]
        public Task<FeaturedEvent> PatchFeaturedEvent(string id, Delta<FeaturedEvent> patch)
        {
             return UpdateAsync(id, patch);
        }

        [EmployeeAuthorize]
        public async Task<IHttpActionResult> PostFeaturedEvent(FeaturedEvent item)
        {
            FeaturedEvent current = await InsertAsync(item);
            return CreatedAtRoute("Tables", new { id = current.Id }, current);
        }

        [EmployeeAuthorize]
        public Task DeleteFeaturedEvent(string id)
        {
             return DeleteAsync(id);
        }
    }
}
