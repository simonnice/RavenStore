using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Raven.Client;
using Raven.Client.Document;
using Raven.Client.Indexes;
using RavenStore.Infrastructure.Raven;

namespace RavenStore
{
    public class MvcApplication : System.Web.HttpApplication
    {
        public static DocumentStore DocumentStore;


        protected void Application_Start()
        {

            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            //DocumentStore = (DocumentStore)(new DocumentStore
            //{
            //    Url = "http://localhost:23456",
            //    DefaultDatabase = "RavenStore"
            //}).Initialize();

            DocumentStore = new DocumentStore {Url = "http://localhost:23456"};
            DocumentStore.DefaultDatabase = "RavenStore";
            DocumentStore.Initialize();
           

            IndexCreation.CreateIndexes(typeof(Products_ByCategory).Assembly, DocumentStore);
        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {
            if (Context.Items[Constants.Keys.RavenDocumentSession] == null)
            {
                var session = DocumentStore.OpenSession();
                Context.Items[Constants.Keys.RavenDocumentSession] = session;
            }
        }

        protected void Application_EndRequest(object sender, EventArgs e)
        {
            var session = Context.Items[Constants.Keys.RavenDocumentSession] as IDocumentSession;
            if (session != null)
            {
                session.Dispose();
                Context.Items.Remove(Constants.Keys.RavenDocumentSession);
            }
        }
    }
}
