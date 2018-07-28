//using System;
//using Microsoft.VisualStudio.TestTools.UnitTesting;
//using TryEntityFramework.Model.EntityFrameworkMappings.Original;

//namespace TryEntityFramework.Tests
//{
//    [TestClass]
//    public class Test_EntityFrameworkMappings_Original
//    {
//        //[TestMethod]
//        //public void TestMethod1()
//        //{
//            //var thingModel = new ThingModel();

//            //foreach (Thing thing in thingModel.Things)
//            //{
//            //    Console.WriteLine(thing.Name);
//            //}
//        //}

//        [TestMethod]
//        public void Test_EntityFrameworkMappings_Original_GetObjects()
//        {
//            using (var context = new TryEntityFrameworkDBEntities())
//            {
//                foreach (Thing thing in context.Things)
//                {
//                    string name = thing.Name;
//                }
//            }
//        }

//        [TestMethod]
//        public void Test_EntityFrameworkMappings_Original_CreateObject()
//        {
//            using (var context = new TryEntityFrameworkDBEntities())
//            {
//                var thing = new Thing();
//                thing.Name = "Thing2";
//                context.Things.Add(thing);
//                context.SaveChanges();
//            }
//        }
//    }
//}
