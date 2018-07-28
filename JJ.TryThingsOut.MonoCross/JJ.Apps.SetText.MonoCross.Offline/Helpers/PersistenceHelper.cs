using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using JJ.Framework.Persistence;
using JJ.Models.SetText.Persistence.Repositories;
using JJ.Models.SetText.Persistence.RepositoryInterfaces;

namespace JJ.Apps.SetText.MonoCross.Helpers
{
    internal static class PersistenceHelper
    {
        // TODO: Do I even have a config file? I doubt it...

        public static TRepositoryInterface CreateRepository<TRepositoryInterface>(IContext context)
        {
            return RepositoryFactory.CreateRepositoryFromConfiguration<TRepositoryInterface>(context);
        }

        public static IContext CreateContext()
        {
            PersistenceConfiguration persistenceConfiguration = PersistenceConfigurationHelper.GetPersistenceConfiguration();
            return ContextFactory.CreateContextFromConfiguration();
        }
    }
}