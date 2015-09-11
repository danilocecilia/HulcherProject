using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Hulcher.OneSource.CustomerService.Business.ViewModel;
using Hulcher.OneSource.CustomerService.Core.ViewInterfaces;
using Hulcher.OneSource.CustomerService.Business.WebControls.Utils;

namespace Hulcher.OneSource.CustomerService.Test.ViewModel_Tests
{
    [TestClass]
    public class CallEntryViewModelTest
    {
        [TestMethod]
        public void TestTheNoteFieldFormatBuilder()
        {
            //Arrange
            string xml = "<?xml version=\"1.0\" encoding=\"utf-8\"?><DynamicFieldsAggregator xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\"><Controls><DynamicControls xsi:type=\"DynamicCountableTextBoxXml\"><Name>txtNote</Name><Label><Text>Note:</Text><Css>dynamicLabel</Css><Style /></Label><Css>input</Css><Style /><Visible>true</Visible><MaxChars>255</MaxChars><MaxCharsWarning>250</MaxCharsWarning><Text>Danilo test note</Text><IsRequired>false</IsRequired><ErrorMessage /><ValidationGroup /><TextMode>MultiLine</TextMode><Width>300</Width><Height>150</Height></DynamicControls><DynamicControls xsi:type=\"DynamicTextBoxXml\"><Name>txtDescription</Name><Label><Text>Description:</Text><Css>dynamicLabel</Css><Style /></Label><Css>input</Css><Style /><Visible>true</Visible><Text>sdsdsd</Text><IsRequired>false</IsRequired><ErrorMessage /><ValidationGroup /><MaxLength>255</MaxLength></DynamicControls><DynamicControls xsi:type=\"DynamicTextBoxXml\"><Name>txtCity</Name><Label><Text>City:</Text><Css>dynamicLabel</Css><Style /></Label><Css>input</Css><Style /><Visible>true</Visible><Text>ALONSA</Text><IsRequired>false</IsRequired><ErrorMessage /><ValidationGroup /><MaxLength>255</MaxLength></DynamicControls><DynamicControls xsi:type=\"DynamicTextBoxXml\"><Name>txtState</Name><Label><Text>State:</Text><Css>dynamicLabel</Css><Style /></Label><Css>input</Css><Style /><Visible>true</Visible><Text>MANITOBA</Text><IsRequired>false</IsRequired><ErrorMessage /><ValidationGroup /><MaxLength>255</MaxLength></DynamicControls></Controls><Extenders><Extenders xsi:type=\"AutoFillExtenderXml\"><TargetControlName>txtCity</TargetControlName><Type>JobCity</Type></Extenders><Extenders xsi:type=\"AutoFillExtenderXml\"><TargetControlName>txtState</TargetControlName><Type>JobState</Type></Extenders></Extenders></DynamicFieldsAggregator>";
            Moq.Mock<ICallEntryView> mockView = new Moq.Mock<ICallEntryView>();
            CallEntryViewModel viewModel = new CallEntryViewModel(mockView.Object);
            //Act
            Dictionary<string, string> controls = DynamicFieldsParser.GetDynamicFieldControlsProperties(xml);
            //Assert
            Assert.AreEqual(4, controls.Count);
            Assert.AreEqual("Danilo test note", controls["Note:"]);
            Assert.AreEqual("sdsdsd", controls["Description:"]);
            Assert.AreEqual("ALONSA", controls["City:"]);
            Assert.AreEqual("MANITOBA", controls["State:"]);
        }

        [TestMethod]
        public void TestFormatDataForDynamicFields()
        {
            //Arrange
            Dictionary<string, string> controls = new Dictionary<string, string>();
            controls.Add("City:", "Denton");
            controls.Add("State:", "TX");
            Moq.Mock<ICallEntryView> mockView = new Moq.Mock<ICallEntryView>();
            CallEntryViewModel viewModel = new CallEntryViewModel(mockView.Object);            
            //Act
            string formattedString = DynamicFieldsParser.FormatDynamicFieldsData(controls);
            //Assert
            Assert.AreEqual("City:<Text>Denton<BL>State:<Text>TX<BL>", formattedString);
        }
    }
}
