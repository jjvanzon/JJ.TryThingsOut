using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TryEntityFramework.Model;
using TryEntityFramework.Model.EntityFrameworkMappings;
using System.Data.Entity;
using System.Configuration;
using System.Data.Entity.Infrastructure;
using System.Data.Common;
using System.Data.SqlClient;

namespace TryEntityFramework.Tests
{
    [TestClass]
    public class Test_EntityFrameworkMappings
    {
        [TestMethod]
        public void Test_EntityFrameworkMappings_GetObjects()
        {
            string connectionString = GetConnectionString_WithSpecialFormat();
            using (DbContext context = new TryEntityFrameworkContext(connectionString))
            {
                foreach (Thing thing in context.Set<Thing>())
                {
                    string name = thing.Name;
                }
            }
        }

        [TestMethod]
        public void Test_EntityFrameworkMappings_CreateObject()
        {
            string connectionString = GetConnectionString_WithSpecialFormat();
            using (DbContext context = new TryEntityFrameworkContext(connectionString))
            {
                var thing = new Thing();
                thing.Name = "Thing";
                context.Set<Thing>().Add(thing);
                context.SaveChanges();
            }
        }

        private string GetConnectionString_WithSpecialFormat()
        {
            return ConfigurationManager.ConnectionStrings["TryEntityFrameworkDBEntities"].ConnectionString;
        }
    }
}
