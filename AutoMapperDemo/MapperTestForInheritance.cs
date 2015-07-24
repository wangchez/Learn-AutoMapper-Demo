using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AutoMapper;

namespace AutoMapperDemo
{
    /// <summary>
    /// MapperTestForInheritance 的摘要描述
    /// </summary>
    [TestClass]
    public class MapperTestForInheritance
    {
        public MapperTestForInheritance()
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

        public class Company { public Guid Id { get; set; } }
        public class Apple : Company 
        {
            //public Guid Id { get; set; }
            public string Brand { get; set; }
        }

        public class SubCompany 
        {
            public Guid Id { get; set; }
            public string Brand { get; set; }
            public string OCNumber { get; set; }
        }

        #endregion

        [TestMethod]
        public void AutoMapperTestForInheritanceMapping()
        {
            //Mapper.CreateMap<Company, SubCompany>().Include<Apple, SubCompany>().ForMember(dest => dest.Brand
            //    , opt=>opt.Ignore()).ForMember(dest => dest.OCNumber, opt => opt.MapFrom(o => o.CId));
            //Mapper.CreateMap<Apple, SubCompany>().ForMember(dest => dest.Id, opt => opt.Ignore());

            Mapper.CreateMap<Company, SubCompany>().Include<Apple, SubCompany>();
            Mapper.CreateMap<Apple, SubCompany>();

            SubCompany subCompany;
            Apple company = new Apple { Id = Guid.NewGuid(), Brand = "Apple"};
            subCompany = Mapper.Map<Company, SubCompany>(company);

            Assert.AreEqual(subCompany.Brand, "Apple");
            //Assert.AreEqual(subCompany.OCNumber, company.CId.ToString());
            Assert.AreNotEqual(subCompany.Id, company.Id);

            //Mapper.CreateMap<Apple, SubCompany>().ForMember(dest => dest.OCNumber, opt => opt.Ignore());
            //subCompany = Mapper.Map<Company, SubCompany>(company);

            //Assert.AreNotEqual(subCompany.OCNumber, company.CId);

            //Mapper.CreateMap<Apple, SubCompany>().ForMember(dest => dest.Id, opt => opt.MapFrom(o => o.Id));
            //subCompany = Mapper.Map<Company, SubCompany>(company);

            //Assert.AreEqual(subCompany.Id, company.Id);
        }
    }
}
