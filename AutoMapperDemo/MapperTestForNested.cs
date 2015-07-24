using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AutoMapper;

namespace AutoMapperDemo
{
    /// <summary>
    /// MapperTestForNested 的摘要描述
    /// </summary>
    [TestClass]
    public class MapperTestForNested
    {
        public MapperTestForNested()
        {
            //
            // TODO: 在此加入建構函式的程式碼
            //
        }

        private TestContext testContextInstance;

        /// <summary>
        ///取得或設定提供目前測試回合
        ///的相關資訊與功能的測試內容。
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region 其他測試屬性
        //
        // 您可以使用下列其他屬性撰寫您的測試:
        //
        // 執行該類別中第一項測試前，使用 ClassInitialize 執行程式碼
        // [ClassInitialize()]
        // public static void MyClassInitialize(TestContext testContext) { }
        //
        // 在類別中的所有測試執行後，使用 ClassCleanup 執行程式碼
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        //
        // 在執行每一項測試之前，先使用 TestInitialize 執行程式碼 
        // [TestInitialize()]
        // public void MyTestInitialize() { }
        //
        // 在執行每一項測試之後，使用 TestCleanup 執行程式碼
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //
        #endregion

        #region Test Model

        public class ModelA
        {
            public Guid Id { get; set; }

            public ModelB InnerModel { get; set; }
        }

        public class ModelB
        {
            public ModelC InnerModel { get; set; }

            public string Number { get; set; }
        }

        public class ModelC
        {
            public string Name { get; set; }
        }

        public class NewModel
        {
            public Guid Id { get; set; }
            public string InnerModelInnerModelName { get; set; }
            public InnerNewModel InnerModel { get; set; }
        }

        public class InnerNewModel
        {
            public string Number { get; set; }

            public string InnerModelName { get; set; }
        }

        #endregion

        [TestMethod]
        public void AutoMapperTestForNestedMappings()
        {
            Mapper.CreateMap<ModelA, NewModel>();
            Mapper.CreateMap<ModelB, InnerNewModel>();

            NewModel newModel;
            ModelA modelA = new ModelA
            {
                Id = Guid.NewGuid(),
                InnerModel = new ModelB
                {
                    InnerModel = new ModelC { Name = "Nested" },
                    Number = "456"
                }
            };
            newModel = Mapper.Map<ModelA, NewModel>(modelA);

            Assert.AreEqual(newModel.Id, modelA.Id);
            Assert.AreEqual(newModel.InnerModelInnerModelName, "Nested");
            Assert.AreEqual(newModel.InnerModel.InnerModelName, "Nested");
            Assert.AreEqual(newModel.InnerModel.Number, "456");
        }
    }
}
