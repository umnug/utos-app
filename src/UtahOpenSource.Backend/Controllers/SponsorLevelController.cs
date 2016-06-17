using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.OData;
using Microsoft.Azure.Mobile.Server;
using UtahOpenSource.DataObjects;
using UtahOpenSource.Backend.Models;
using UtahOpenSource.Backend.Identity;

namespace UtahOpenSource.Backend.Controllers
{
    public class SponsorLevelController : TableController<SponsorLevel>
    {
        protected override void Initialize(HttpControllerContext controllerContext)
        {
            base.Initialize(controllerContext);
            UtahOpenSourceContext context = new UtahOpenSourceContext();
            DomainManager = new EntityDomainManager<SponsorLevel>(context, Request, true);
        }

        public IQueryable<SponsorLevel> GetAllSponsorLevel()
        {
            return Query(); 
        }

        public SingleResult<SponsorLevel> GetSponsorLevel(string id)
        {
            return Lookup(id);
        }

        [EmployeeAuthorize]
        public Task<SponsorLevel> PatchSponsorLevel(string id, Delta<SponsorLevel> patch)
        {
             return UpdateAsync(id, patch);
        }


        [EmployeeAuthorize]
        public async Task<IHttpActionResult> PostSponsorLevel(SponsorLevel item)
        {
            SponsorLevel current = await InsertAsync(item);
            return CreatedAtRoute("Tables", new { id = current.Id }, current);
        }

        [EmployeeAuthorize]
        public Task DeleteSponsorLevel(string id)
        {
             return DeleteAsync(id);
        }

    }
}
