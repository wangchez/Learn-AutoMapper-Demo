using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AutoMapper;

namespace AutoMapperDemo
{
    /// <summary>
    /// MapperTestForConverter 的摘要描述
    /// </summary>
    [TestClass]
    public class MapperTestForConverter
    {
        public MapperTestForConverter()
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

        public class OriginTypeModel
        {
            public string NumberType { get; set; }
            public string DateTimeType { get; set; }
            public float d { get; set; }
            
        }

        public class NewTypeModel
        {
            public int NumberType { get; set; }
            public DateTime DateTimeType { get; set; }
            public decimal d { get; set; }
        }

        #endregion

        #region Custom TypeConverter

        public class DateTimeTypeConverter : ITypeConverter<string, DateTime>
        {
            public DateTime Convert(ResolutionContext context)
            {
                return System.Convert.ToDateTime(context.SourceValue);
            }
        }

        #endregion

        [TestMethod]
        public void AutoMapperTestForCustomTypeConverters()
        {         
            Mapper.CreateMap<string, int>().ConvertUsing(Convert.ToInt32);
            Mapper.CreateMap<string, DateTime>().ConvertUsing(new DateTimeTypeConverter());
            Mapper.CreateMap<string, DateTime>().ConvertUsing<DateTimeTypeConverter>();
            Mapper.CreateMap<OriginTypeModel, NewTypeModel>();

            OriginTypeModel originalTypeModel = new OriginTypeModel 
            { 
                NumberType = "123", DateTimeType = "2013/01/01", d = 123.3f
            };
            NewTypeModel newTypeModel;

            newTypeModel = Mapper.Map<OriginTypeModel, NewTypeModel>(originalTypeModel);

            Assert.AreEqual(newTypeModel.NumberType, 123);
            Assert.AreEqual(newTypeModel.DateTimeType, new DateTime(2013,1,1));
        }
    }
}
