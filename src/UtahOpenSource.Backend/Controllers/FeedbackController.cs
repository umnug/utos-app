﻿using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Controllers;
using Microsoft.Azure.Mobile.Server;
using UtahOpenSource.DataObjects;
using UtahOpenSource.Backend.Models;
using UtahOpenSource.Backend.Identity;
using UtahOpenSource.Backend.Helpers;
using System.Net;
using System;
using System.Web.Http.OData;

namespace UtahOpenSource.Backend.Controllers
{
    public class FeedbackController : TableController<Feedback>
    {
        protected override void Initialize(HttpControllerContext controllerContext)
        {
            base.Initialize(controllerContext);
            UtahOpenSourceContext context = new UtahOpenSourceContext();
            DomainManager = new EntityDomainManager<Feedback>(context, Request, true);
        }
        
        public IQueryable<Feedback> GetAllFeedback()
        {
            var items = Query();

            var email = EmailHelper.GetAuthenticatedUserEmail(RequestContext);

            var final = items.Where(feedback => feedback.UserId == email);

            return final;

        }


        [Authorize]
        public SingleResult<Feedback> GetFeedback(string id)
        {
            return Lookup(id);
        }

        [Authorize]
        public Task<Feedback> PatchFeedback(string id, Delta<Feedback> patch)
        {
            return UpdateAsync(id, patch);
        }

        [Authorize]
        public async Task<IHttpActionResult> PostFeedback(Feedback item)
        {
            var feedback = item;
            feedback.UserId = EmailHelper.GetAuthenticatedUserEmail(RequestContext);

            var current = await InsertAsync(feedback);
            return CreatedAtRoute("Tables", new { id = current.Id }, current);
        }
    }
}
