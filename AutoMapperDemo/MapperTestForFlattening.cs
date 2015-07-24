using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AutoMapper;

namespace AutoMapperDemo
{
    /// <summary>
    /// MapperTestForFlattening 的摘要描述
    /// </summary>
    [TestClass]
    public class MapperTestForFlattening
    {
        public MapperTestForFlattening()
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

            public string GetNumber()
            {
                return InnerModel.InnerModel.FirstNumber + InnerModel.SecondNumber;
            }
        }

        public class ModelB
        {
            public ModelC InnerModel { get; set; }

            public string SecondNumber { get; set; }
        }

        public class ModelC
        {
            public string Name { get; set; }

            public string FirstNumber { get; set; }
        }

        public class NewModel
        {
            public Guid Id { get; set; }
            public string InnerModelInnerModelName { get; set; }
            public string Number { get; set; }
        }

        #endregion

        [TestMethod]
        public void AutoMapperTestForFlattening()
        {
            //Mapper.CreateMap<ModelA, NewModel>().ForMember(dest=> dest.Number ,opt=>opt.UseDestinationValue());

            NewModel newModel = new NewModel() { Number = "47"}, destModel = new NewModel(){Number="127"};
            NewModel new2 = newModel;
            ModelA modelA = new ModelA
            {
                Id = Guid.NewGuid(),
                InnerModel = new ModelB
                {
                    InnerModel = new ModelC { FirstNumber = "456", Name = "Flattening" },
                    SecondNumber = "123"
                }
            };
            //newModel = Mapper.Map<ModelA, NewModel>(modelA, destModel);
            var a = Mapper.Map<ModelA, NewModel>(new ModelA() { Id = Guid.NewGuid() });
            Assert.AreEqual(newModel.Id, modelA.Id);
            Assert.AreEqual(newModel.Number, "456123");
            Assert.AreEqual(newModel.InnerModelInnerModelName, "Flattening");
        }
    }
}
