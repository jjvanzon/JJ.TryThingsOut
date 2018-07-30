using Microsoft.VisualStudio.TestTools.UnitTesting;
using Puzzle.NPersist.Framework;
using TryNPersist.Model;


namespace TryNPersist.Tests
{
    [TestClass]
    public class TryNPersistTests
    {
        [TestMethod]
        public void Test_TryNPersist_Create()
        {
            //Create a context object
            using (IContext context = GetContext())
            {
                //Ask the context to create a new employee
                var entity = (Thing)context.CreateObject(typeof(Thing));

                entity.Name = "Thing1";

                context.Commit();

                string id = entity.Id.ToString();
            }
        }

        private IContext GetContext()
        {
            //Create a context object passing the assembly
            //containing the domain model to the constructor
            var mapName = "TryNPersist.Model.TryNPersist.npersist";
            IContext context = new Context(typeof(Thing).Assembly, mapName);

            //Set the connection string to the database
            context.SetConnectionString(
                @"Data Source=.\SQLEXPRESS;Initial Catalog=TryNPersistDB_DEV;User ID=development;Password=development;Persist Security Info=True");

            //return the new context
            return context;
        }

        //private IContext GetContext2()
        //{
        //    IDomainMap domainMap = DomainMap.Load(

        //    //Create a context object passing the assembly
        //    //containing the domain model to the constructor
        //    string mapName = "TryNPersist.Model.TryNPersist.npersist";
        //    IContext context = new Context(typeof(Thing).Assembly, mapName);

        //    //Set the connection string to the database
        //    context.SetConnectionString(@"Data Source=.\SQLEXPRESS;Initial Catalog=TryNPersistDB_DEV;User ID=development;Password=development;Persist Security Info=True");

        //    //return the new context
        //    return context;
        //}
    }
}