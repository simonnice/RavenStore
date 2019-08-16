using System;
using System.Web.Mvc;
using Raven.Client;

namespace RavenStore.Controllers
{
    public class BaseController : Controller
    {
        protected IDocumentSession DocumentSession
        {
            get
            {
                var session = HttpContext.Items[Constants.Keys.RavenDocumentSession] as IDocumentSession;
                if (session != null)
                    return session;

                throw new Exception("No session open");
            }
        }
    }
}