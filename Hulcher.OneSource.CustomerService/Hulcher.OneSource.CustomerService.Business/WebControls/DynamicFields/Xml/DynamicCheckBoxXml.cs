using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Hulcher.OneSource.CustomerService.Business.WebControls.DynamicFields.Base;

namespace Hulcher.OneSource.CustomerService.Business.WebControls.DynamicFields.Xml
{
    [Serializable()]
    public class DynamicCheckBoxXml : DynamicControls
    {
        public DynamicCheckBoxXml()
        {

        }

        public DynamicCheckBoxXml(string name,bool check, string text, string css, string style,DynamicLabel label, bool visible)
        {
            Text = text;
            Check = check;
            Css = css;
            Style = style;
            Label = label;
            this.Name = name;
            Visible = visible;
        }

        private bool _check;
        private string _text;

        public bool Check
        {
            get
            {
                return _check;
            }
            set
            {
                _check = value;
            }
        }

        public string Text
        {
            get
            {
                return _text;
            }
            set
            {
                _text = value;
            }
        }
    }
}
