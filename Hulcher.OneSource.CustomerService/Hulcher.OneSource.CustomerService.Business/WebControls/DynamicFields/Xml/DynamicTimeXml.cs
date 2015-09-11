using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Hulcher.OneSource.CustomerService.Business.WebControls.DynamicFields.Base;
using AjaxControlToolkit;

namespace Hulcher.OneSource.CustomerService.Business.WebControls.DynamicFields.Xml
{
    [Serializable()]
    public class DynamicTimeXml : DynamicControls
    {
        public DynamicTimeXml()
        {

        }

        public DynamicTimeXml(string text, string name, string mask, MaskedEditType type, bool isValidEmpty, string validationGroup, string invalidValueMessage,
            string emptyValueMessage, DynamicLabel label, string css, string style, MaskedEditInputDirection inputDirection, bool visible)
        {
            Text = text;
            Name = name;
            Mask = mask;
            MaskedType = type;
            IsValidEmpty = isValidEmpty;
            ValidationGroup = validationGroup;
            InvalidValueMessage = invalidValueMessage;
            EmptyValueMessage = emptyValueMessage;
            Label = label;
            Css = css;
            Style = style;
            InputDirection = inputDirection;
            Visible = visible;
        }

        public string Text
        {
            get;
            set;
        }

        public string Mask
        {
            get;
            set;
        }

        public MaskedEditType MaskedType
        {
            get;
            set;
        }

        public MaskedEditInputDirection InputDirection
        {
            get;
            set;
        }

        public bool IsValidEmpty
        {
            get;
            set;
        }

        public string ValidationGroup
        {
            get;
            set;
        }

        public string InvalidValueMessage
        {
            get;
            set;
        }

        public string EmptyValueMessage
        {
            get;
            set;
        }
    }
}
