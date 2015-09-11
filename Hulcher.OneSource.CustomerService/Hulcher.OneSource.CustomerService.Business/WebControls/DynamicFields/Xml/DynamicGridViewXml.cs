using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Hulcher.OneSource.CustomerService.Business.WebControls.DynamicFields.Base;
using System.Data;
using System.Xml.Serialization;
using System.Collections.ObjectModel;

namespace Hulcher.OneSource.CustomerService.Business.WebControls.DynamicFields.Xml
{
    [Serializable()]
    public class DynamicGridViewXml : DynamicControls
    {
        private string _text;

        public DynamicGridViewXml()
        {
        }

        public DynamicGridViewXml(string name, DynamicLabel label, string css, string style, List<string[]> dataSourceList, bool visible)
        {
            base.Name = name;
            Label = label;
            Css = css;
            Style = style;
            DataSourceList = dataSourceList;
            Visible = visible;
        }
        
        public List<string[]> DataSourceList { get; set; }

        public string Text
        {
            get
            {
                StringBuilder textBuilder = new StringBuilder();
                for (int i = 0; i < DataSourceList.Count; i++)
                {
                    for (int j = 0; j < DataSourceList[i].Count(); j++)
                    {
                        textBuilder.Append(DataSourceList[i][j]);
                        textBuilder.Append(";");
                    }
                    textBuilder.AppendLine("");
                }
                _text = textBuilder.ToString();
                return _text;
            }
            set
            {
                _text = value;
            }
        }
    }
}
