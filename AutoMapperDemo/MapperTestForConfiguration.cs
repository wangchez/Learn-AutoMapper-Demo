using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AutoMapper;

namespace AutoMapperDemo
{
    /// <summary>
    /// MapperTestForConfiguration 的摘要描述
    /// </summary>
    [TestClass]
    public class MapperTestForConfiguration
    {
        public MapperTestForConfiguration()
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

        public class OriginalModel
        {
            public Guid Id { get; set; }
            public string OriginalName { get; set; }
        }

        public class NewModel
        {
            public Guid Id { get; set; }
            public string NewName { get; set; }
        }

        #endregion

        #region Configuration

        public class CustomMappingProfile : Profile
        {
            protected override void Configure()
            {
                this.CreateMap<OriginalModel, NewModel>().ConstructUsing((OriginalModel original) => new NewModel 
                { 
                    NewName = "new"
                });
            }

            public override string ProfileName
            {
                get { return this.GetType().Name; }
            }
        }

        #endregion

        [TestMethod]
        public void AutoMapperTestForConfiguration()
        {
            Mapper.Initialize(map => 
            {
                map.AddProfile<CustomMappingProfile>();

            });

            NewModel newModel;
            OriginalModel originalModel = new OriginalModel { Id = Guid.NewGuid(), OriginalName = "Configuration"};
            newModel = Mapper.Map<OriginalModel, NewModel>(originalModel);

            Assert.AreEqual(newModel.NewName, originalModel.OriginalName);
            Assert.AreEqual(newModel.Id, originalModel.Id);
        }
    }
}
