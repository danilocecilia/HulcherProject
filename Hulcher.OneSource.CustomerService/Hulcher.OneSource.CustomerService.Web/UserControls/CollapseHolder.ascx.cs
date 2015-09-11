using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;
using System.Text;
using System.ComponentModel;

namespace Hulcher.OneSource.CustomerService.Web.UserControls
{
    [ParseChildren(true, ChildrenAsProperties = true)]
    [PersistChildren(false)]
    public partial class CollapseHolder : System.Web.UI.UserControl, INamingContainer
    {
        #region [ Enums ]
        
        public enum State { Expanded, Collapsed };
        
        #endregion

        #region [ Atributes ]

        private ITemplate _Header;
        private ITemplate _Content;
        private State _InitialState;
        private string _GridViewCssClass = "";
        private string _ScriptOnExpand = "";

        #endregion

        #region [ Properties ]

        /// <summary>
        /// Defines if the content panel is going to be
        /// Style position relative
        /// </summary>
        [DefaultValue(false)]
        [PersistenceMode(PersistenceMode.Attribute)]
        public bool Relative
        {
            set
            {
                if (value)
                    trContent.Attributes.Add("style", "position: relative;");
            }
        }

        /// <summary>
        /// Initial State of the control (Collapsed Or Expanded)
        /// </summary>
        [PersistenceMode(PersistenceMode.Attribute)]
        public State InitialState
        {
            get
            {
                return _InitialState;
            }
            set
            {
                _InitialState = value;
                switch (_InitialState)
                {
                    case State.Expanded:
                        clpExtender.Collapsed = false;
                        clpExtender.ExpandedImage = ImageCollapse;
                        break;
                    case State.Collapsed:
                        clpExtender.Collapsed = true;
                        clpExtender.CollapsedImage = ImageExpand;
                        break;
                }
            }
        }

        [PersistenceMode(PersistenceMode.Attribute)]
        public string ScriptOnExpand
        {
            get
            {
                return _ScriptOnExpand;
            }
            set
            {
                _ScriptOnExpand = value;
            }
        }

        [PersistenceMode(PersistenceMode.Attribute)]
        public string GridViewCssClass
        {
            get
            {
                return _GridViewCssClass;
            }
            set
            {
                _GridViewCssClass = value;
            }
        }

        [PersistenceMode(PersistenceMode.Attribute)]
        public bool Collapsed
        {
            set
            {
                clpExtender.Collapsed = value;
            }
        }

        [PersistenceMode(PersistenceMode.Attribute)]
        public string Width
        {
            get
            {
                return dvBody.Style[HtmlTextWriterStyle.Width];
            }
            set
            {
                dvBody.Style[HtmlTextWriterStyle.Width] = value;
            }
        }

        [PersistenceMode(PersistenceMode.Attribute)]
        public string Height
        {
            get
            {
                return dvBody.Style[HtmlTextWriterStyle.Height];
            }
            set
            {
                dvBody.Style[HtmlTextWriterStyle.Height] = value;
            }
        }

        [PersistenceMode(PersistenceMode.Attribute)]
        public string Position
        {
            get
            {
                return dvBody.Style[HtmlTextWriterStyle.Position];
            }
            set
            {
                dvBody.Style[HtmlTextWriterStyle.Position] = value;
            }
        }

        /// <summary>
        /// Path to the image beeing used on collapse (Default : "Images\CollapseHolder\Collapse.gif")
        /// </summary>
        [PersistenceMode(PersistenceMode.Attribute)]
        public string ImageCollapse
        {
            get
            {
                return hfCollapse.Value;
            }
            set
            {
                hfCollapse.Value = value;
            }
        }


        /// <summary>
        /// Path to the image beeing used on Expand (Default : "Images\CollapseHolder\Expand.gif")
        /// </summary>
        [PersistenceMode(PersistenceMode.Attribute)]
        public string ImageExpand
        {
            get
            {
                return hfExpand.Value;
            }
            set
            {
                hfExpand.Value = value;
            }
        }

        /// <summary>
        /// Property that holds the placeholder for the header
        /// </summary>
        [PersistenceMode(PersistenceMode.InnerProperty), TemplateContainer(typeof(TemplateControl)), TemplateInstance(TemplateInstance.Single)]
        public ITemplate Header
        {
            get
            {
                return _Header;
            }
            set
            {
                _Header = value;
            }
        }

        /// <summary>
        /// Property that holds the placeholder for the content
        /// </summary>
        [PersistenceMode(PersistenceMode.InnerProperty), TemplateContainer(typeof(TemplateControl)), TemplateInstance(TemplateInstance.Single)]
        public ITemplate Content
        {
            get
            {
                return _Content;
            }
            set
            {
                _Content = value;
            }
        }


        /// <summary>
        /// Property to control the color of the header
        /// </summary>
        [PersistenceMode(PersistenceMode.Attribute)]
        public string HeaderColor
        {
            get
            {
                return trHeader.Style[HtmlTextWriterStyle.BackgroundColor].ToString();
            }
            set
            {
                trHeader.Style[HtmlTextWriterStyle.BackgroundColor] = value;
            }
        }

        /// <summary>
        /// Property to control the color of the header
        /// </summary>
        [PersistenceMode(PersistenceMode.Attribute)]
        public string ContentColor
        {
            get
            {
                return trContent.Style[HtmlTextWriterStyle.BackgroundColor].ToString();
            }
            set
            {
                trContent.Style[HtmlTextWriterStyle.BackgroundColor] = value;
            }
        }


        /// <summary>
        /// Property to control the text color of the header
        /// </summary>
        [CssClassProperty()]
        public string HeaderTextColor
        {
            get
            {
                return trHeader.Style[HtmlTextWriterStyle.Color].ToString();
            }
            set
            {
                trHeader.Style[HtmlTextWriterStyle.Color] = value;
            }
        }


        /// <summary>
        /// Property to control the text color of the content
        /// </summary>
        [PersistenceMode(PersistenceMode.Attribute)]
        public string ContentTextColor
        {
            get
            {
                return trContent.Style[HtmlTextWriterStyle.Color].ToString();
            }
            set
            {
                trContent.Style[HtmlTextWriterStyle.Color] = value;
            }
        }

        #endregion

        #region [ Events ]

        /// <summary>
        /// Initialize the placeholders with the value passed by the property
        /// </summary>
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            if (_Header != null)
            {
                _Header.InstantiateIn(HeaderHolder);
            }
            if (_Content != null)
            {
                _Content.InstantiateIn(ContentHolder);
            }

            if (clpExtender.Collapsed)
            {
                trContent.Style[HtmlTextWriterStyle.Display] = "";
            }

        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                clpExtender.CollapsedImage = ImageExpand;
                clpExtender.ExpandedImage = ImageCollapse;

                if (_GridViewCssClass != string.Empty)
                {
                    StringBuilder script = new StringBuilder();
                    script.Append("Sys.Application.add_load(function() {\n");
                    script.AppendFormat(" var objExtender = $find('{0}');\n", clpExtender.ClientID);
                    script.Append(" if (objExtender) {\n");
                    script.Append("     objExtender.add_collapsing(function() { \n");
                    script.Append("         $(document).find('." + _GridViewCssClass + "').css('visibility', 'hidden');\n");
                    script.Append("         $(document).find('.ScrollableGridView_Group').css('visibility', 'hidden');\n");
                    script.Append("     });\n");
                    script.Append("     objExtender.add_expanding(function() { \n");
                    script.Append("         $(document).find('." + _GridViewCssClass + "').css('visibility', 'hidden');\n");
                    script.Append("         $(document).find('.ScrollableGridView_Group').css('visibility', 'hidden');\n");
                    script.Append("     });\n");
                    script.Append("     objExtender.add_expandComplete(function() {\n");
                    script.Append("     " + ScriptOnExpand);
                    script.Append("         $(document).find('." + _GridViewCssClass + "').css('visibility', 'visible');\n");
                    script.Append("         $(document).find('.ScrollableGridView_Group').css('visibility', 'visible');\n");
                    script.Append("     });\n");
                    script.Append("     objExtender.add_collapseComplete(function() {\n");
                    script.Append("         $(document).find('." + _GridViewCssClass + "').css('visibility', 'visible');\n");
                    script.Append("         $(document).find('.ScrollableGridView_Group').css('visibility', 'visible');\n");
                    script.Append("     });\n");
                    script.Append(" }\n");
                    script.Append("})\n");

                    ScriptManager scriptManager = ScriptManager.GetCurrent(this.Page);
                    if (null == scriptManager)
                    {
                        ClientScriptManager cs = this.Page.ClientScript;
                        cs.RegisterStartupScript(GetType(), string.Format("CollapseHolder_{0}", ID), script.ToString(), true);
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), string.Format("CollapseHolder_{0}", ID), script.ToString(), true);
                    }
                }
            }

            //if (hfLastState.Value == "Expanded")
            //{
            //    trContent.Style[HtmlTextWriterStyle.Display] = "block";
            //    ImageHeader.Attributes["src"] = ImageCollapse;
            //}
            //else if (hfLastState.Value == "Collapsed")
            //{
            //    trContent.Style[HtmlTextWriterStyle.Display] = "none";
            //    ImageHeader.Attributes["src"] = ImageExpand;
            //}
        }

        #endregion
    }
}