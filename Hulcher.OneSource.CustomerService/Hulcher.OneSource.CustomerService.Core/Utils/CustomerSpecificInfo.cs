using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.Xml.Serialization;
using System.Xml;
using System.IO;

namespace Hulcher.OneSource.CustomerService.Core.Utils
{
    [Serializable()]
    public class CustomerSpecificInfo : ISerializable
    {
        private string _Type;
        private string _Value;
        private string _Name;

        public string Type
        {
            get
            {
                return _Type;
            }
            set
            {
                _Type = value;
            }
        }

        public string Value
        {
            get
            {
                return _Value;
            }
            set
            {
                _Value = value;
            }
        }

        public string Name
        {
            get
            {
                return _Name;
            }
            set
            {
                _Name = value;
            }
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("Name", _Name);
            info.AddValue("Value", _Value);
            info.AddValue("Type", _Type);
        }

        private static String UTF8ByteArrayToString(Byte[] characters)
        {

            UTF8Encoding encoding = new UTF8Encoding();

            String constructedString = encoding.GetString(characters);

            return (constructedString);

        }

        private static Byte[] StringToUTF8ByteArray(String pXmlString)
        {

            UTF8Encoding encoding = new UTF8Encoding();

            Byte[] byteArray = encoding.GetBytes(pXmlString);

            return byteArray;

        } 

        public static String SerializeObject(List<CustomerSpecificInfo> _CustomerSpecificInfoList)
        {
            try
            {
                String XmlizedString = null;

                MemoryStream memoryStream = new MemoryStream();

                XmlSerializer xs = new XmlSerializer(typeof(List<CustomerSpecificInfo>));

                XmlTextWriter xmlTextWriter = new XmlTextWriter(memoryStream, Encoding.UTF8);

                xs.Serialize(xmlTextWriter, _CustomerSpecificInfoList);

                memoryStream = (MemoryStream)xmlTextWriter.BaseStream;

                XmlizedString = UTF8ByteArrayToString(memoryStream.ToArray());

                return XmlizedString.Replace("utf-8", "utf-16");
            }
            catch
            {
                throw;
            }
        }

        public static List<CustomerSpecificInfo> DeserializeObject(string XmlizedString)
        {
            try
            {
                XmlSerializer xs = new XmlSerializer(typeof(List<CustomerSpecificInfo>));

                MemoryStream memoryStream = new MemoryStream(StringToUTF8ByteArray(XmlizedString));

                XmlTextWriter xmlTextWriter = new XmlTextWriter(memoryStream, Encoding.UTF8);

                return xs.Deserialize(memoryStream) as List<CustomerSpecificInfo>;
            }
            catch
            {
                throw;
            }
        }
    }
}
