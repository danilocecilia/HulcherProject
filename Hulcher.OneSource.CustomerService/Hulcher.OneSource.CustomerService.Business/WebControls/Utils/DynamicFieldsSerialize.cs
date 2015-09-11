using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml.Serialization;
using System.Xml;
using Hulcher.OneSource.CustomerService.Business.WebControls.DynamicFields;
using Hulcher.OneSource.CustomerService.Business.WebControls.DynamicFields.Xml;

namespace Hulcher.OneSource.CustomerService.Business.WebControls.Utils
{
    public class DynamicFieldsSerialize
    {
        private String UTF8ByteArrayToString(Byte[] characters)
        {

            UTF8Encoding encoding = new UTF8Encoding();

            String constructedString = encoding.GetString(characters);

            return (constructedString.Remove(0, 1));

        }

        private Byte[] StringToUTF8ByteArray(String pXmlString)
        {

            UTF8Encoding encoding = new UTF8Encoding();

            Byte[] byteArray = encoding.GetBytes(pXmlString);

            return byteArray;

        }

        public String SerializeObject(DynamicFieldsAggregator _Object)
        {
            try
            {
                String XmlizedString = null;

                MemoryStream memoryStream = new MemoryStream();

                XmlSerializer xs = new XmlSerializer(typeof(DynamicFieldsAggregator));

                XmlTextWriter xmlTextWriter = new XmlTextWriter(memoryStream, Encoding.UTF8);

                xs.Serialize(xmlTextWriter, _Object);

                memoryStream = (MemoryStream)xmlTextWriter.BaseStream;

                XmlizedString = UTF8ByteArrayToString(memoryStream.ToArray());

                return XmlizedString;
            }
            catch
            {
                throw;
            }
        }

        public DynamicFieldsAggregator DeserializeObject(string XmlizedString)
        {
            try
            {
                XmlSerializer xs = new XmlSerializer(typeof(DynamicFieldsAggregator));

                MemoryStream memoryStream = new MemoryStream(StringToUTF8ByteArray(XmlizedString));

                XmlTextWriter xmlTextWriter = new XmlTextWriter(memoryStream, Encoding.UTF8);

                return xs.Deserialize(memoryStream) as DynamicFieldsAggregator;
            }
            catch
            {
                throw;
            }
        }
    }
}
