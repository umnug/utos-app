using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.OData;
using Microsoft.Azure.Mobile.Server;
using UtahOpenSource.DataObjects;
using UtahOpenSource.Backend.Helpers;
using UtahOpenSource.Backend.Models;
using UtahOpenSource.Backend.Identity;

namespace UtahOpenSource.Backend.Controllers
{
    public class SponsorController : TableController<Sponsor>
    {
        protected override void Initialize(HttpControllerContext controllerContext)
        {
            base.Initialize(controllerContext);
            UtahOpenSourceContext context = new UtahOpenSourceContext();
            DomainManager = new EntityDomainManager<Sponsor>(context, Request, true);
        }

        [QueryableExpand ("SponsorLevel")]
        public IQueryable<Sponsor> GetAllSponsor()
        {
            return Query(); 
        }

        [QueryableExpand("SponsorLevel")]
        public SingleResult<Sponsor> GetSponsor(string id)
        {
            return Lookup(id);
        }

        [EmployeeAuthorize]
        public Task<Sponsor> PatchSponsor(string id, Delta<Sponsor> patch)
        {
             return UpdateAsync(id, patch);
        }

        [EmployeeAuthorize]
        public async Task<IHttpActionResult> PostSponsor(Sponsor item)
        {
            Sponsor current = await InsertAsync(item);
            return CreatedAtRoute("Tables", new { id = current.Id }, current);
        }

        [EmployeeAuthorize]
        public Task DeleteSponsor(string id)
        {
             return DeleteAsync(id);
        }

    }
}
