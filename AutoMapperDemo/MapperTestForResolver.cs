using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AutoMapper;
using System.Xml.Linq;
using System.Xml;

namespace AutoMapperDemo
{
    /// <summary>
    /// MapperTestForResolvers 的摘要描述
    /// </summary>
    [TestClass]
    public class MapperTestForResolver
    {
        public MapperTestForResolver()
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
            public string FirstName { get; set; }

            public string SecondName { get; set; }
        }

        public class NewModel
        {
            public string FullName { get; set; }
        }

        public class MenuItem
        {
            public string Name { get; set; }
            public string Action { get; set; }
            public string Parameters { get; set; }
        }

        #endregion

        #region Custom ValueResolver

        public class CustomResolver : ValueResolver<object, string>
        {
            protected override string ResolveCore(object original)
            {
                if(original is OriginalModel)
                {
                    return (original as OriginalModel).FirstName + (original as OriginalModel).SecondName;
                }

                return "";
            }
        }

        public class XAttributeResolver<T> : ValueResolver<XElement, T>
        {
            public XAttributeResolver(string attributeName)
            {
                Name = attributeName;
            }

            public string Name { get; set; }

            protected override T ResolveCore(XElement source)
            {
                if (source == null)
                    return default(T);
                var attribute = source.Attribute(Name);
                if (attribute == null || String.IsNullOrEmpty(attribute.Value))
                    return default(T);

                return (T)Convert.ChangeType(attribute.Value, typeof(T));
            }
        }

        #endregion

        [TestMethod]
        public void AutoMapperTestForCustomValueResolvers()
        {

      //      Mapper.CreateMap<XElement, MenuItem>()
      //.ForMember(dest => dest.Name,
      //           opt => opt.ResolveUsing<XAttributeResolver<string>>()
      //                     .ConstructedBy(() => new XAttributeResolver<string>("name")))
      //.ForMember(dest => dest.Action,
      //           opt => opt.ResolveUsing<XAttributeResolver<string>>()
      //                     .ConstructedBy(() => new XAttributeResolver<string>("action")))
      //.ForMember(dest => dest.Parameters,
      //           opt => opt.ResolveUsing<XAttributeResolver<string>>()
      //                     .ConstructedBy(() => new XAttributeResolver<string>("parameters")));

      //      var xml = XDocument.Load("menuItem.xml");
      //      var menuItem = Mapper.Map<XDocument, MenuItem>(xml);





            //Mapper.CreateMap<OriginalModel, NewModel>().ForMember(dest => dest.FullName, opt => 
            //    opt.ResolveUsing(new CustomResolver())
            //    //opt.ResolveUsing<CustomResolver>()
            //    //opt.ResolveUsing(typeof(CustomResolver))
            //    );
            Mapper.CreateMap<OriginalModel, NewModel>()
                .ForMember(dest=>dest.FullName , opt=>opt.ResolveUsing(new CustomResolver()));


            NewModel newModel;
            OriginalModel originalModel = new OriginalModel { FirstName = "First", SecondName = "Second" };

            newModel = Mapper.Map<OriginalModel, NewModel>(originalModel, (IMappingOperationOptions p) => { });

            Assert.AreEqual(newModel.FullName, "FirstSecond");
        }
    }
}
