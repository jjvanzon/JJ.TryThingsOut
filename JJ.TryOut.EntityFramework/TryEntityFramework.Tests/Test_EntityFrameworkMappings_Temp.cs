//using System;
//using Microsoft.VisualStudio.TestTools.UnitTesting;
//using TryEntityFramework.Model;
//using TryEntityFramework.Model.EntityFrameworkMappings.Temp;

//namespace TryEntityFramework.Tests
//{
//    [TestClass]
//    public class Test_EntityFrameworkMappings_Temp
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
//        public void Test_EntityFrameworkMappings_Temp_GetObjects()
//        {
//            using (var context = new TryEntityFrameworkDBEntities("name=TryEntityFrameworkDBEntities"))
//            {
//                foreach (Thing thing in context.Things)
//                {
//                    string name = thing.Name;
//                }
//            }
//        }

//        [TestMethod]
//        public void Test_EntityFrameworkMappings_Temp_CreateObject()
//        {
//            using (var context = new TryEntityFrameworkDBEntities("name=TryEntityFrameworkDBEntities"))
//            {
//                var thing = new Thing();
//                thing.Name = "Thing";
//                context.Things.Add(thing);
//                context.SaveChanges();
//            }
//        }
//    }
//}
