using System.Configuration;
using System.Data.Entity;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TryEntityFramework.Model;
using TryEntityFramework.Model.EntityFrameworkMappings;
using TryEntityFramework.Tests.Helpers;

// ReSharper disable UnusedVariable

namespace TryEntityFramework.Tests
{
    [TestClass]
    public class Test_EntityFrameworkMappings
    {
        [TestMethod]
        public void Test_EntityFrameworkMappings_GetObjects()
            => AssertInconclusiveHelper.WithConnectionInconclusiveAssertion(
                () =>
                {
                    string connectionString = GetConnectionString_WithSpecialFormat();

                    using (DbContext context = new TryEntityFrameworkContext(connectionString))
                    {
                        foreach (Thing thing in context.Set<Thing>())
                        {
                            string name = thing.Name;
                        }
                    }
                });

        [TestMethod]
        public void Test_EntityFrameworkMappings_CreateObject()
            => AssertInconclusiveHelper.WithConnectionInconclusiveAssertion(
                () =>
                {
                    string connectionString = GetConnectionString_WithSpecialFormat();

                    using (DbContext context = new TryEntityFrameworkContext(connectionString))
                    {
                        var thing = new Thing { Name = "Thing" };
                        context.Set<Thing>().Add(thing);
                        context.SaveChanges();
                    }
                });

        private string GetConnectionString_WithSpecialFormat()
            => ConfigurationManager.ConnectionStrings["TryEntityFrameworkDBEntities"].ConnectionString;
    }
}