using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hulcher.OneSource.CustomerService.Core.Utils
{
    public static class StringManipulation
    {
        public static String GenerateRandomString(int length)
        {
            //Initiate objects & vars    Random random = new Random();
            String randomString = "";
            int randNumber;
            Random randObject = new Random();
            //Loop ‘length’ times to generate a random number or character
            for (int i = 0; i < length; i++)
            {
                if (randObject.Next(1, 3) == 1)
                    randNumber = randObject.Next(97, 123); //char {a-z}
                else
                    randNumber = randObject.Next(48, 58); //int {0-9}

                //append random char or digit to random string
                randomString = randomString + (char)randNumber;
            }
            //return the random string
            return randomString;
        }

        public static string TabulateStringTable(string source)
        {
            StringBuilder returnString = new StringBuilder();

            if (source.Contains("<Text>"))
            {
                string[] lines = source.Split(new string[] { "<BL>" }, StringSplitOptions.RemoveEmptyEntries);

                if (lines.Length > 0)
                {
                    returnString.Append("<TABLE border='0' cellpadding='0' cellspacing='0' width='100%'>");

                    for (int i = 0; i < lines.Length; i++)
                    {
                        returnString.Append("<TR style='width: 100%; display: inline-block;'>");

                        string[] textLine = lines[i].Split(new string[] { "<Text>" }, StringSplitOptions.None);

                        if (textLine.Length == 1)
                        {
                            if (textLine[0].Contains("<RED>"))
                            {
                                textLine[0] = textLine[0].Replace("<RED>", "");

                                returnString.Append("<TD style='text-align: right;width:30%; color: Red;height:100% ;display: inline-block;float:left'><b>");
                                returnString.Append(textLine[0]);
                                returnString.Append("&nbsp;&nbsp;");
                                returnString.Append("</b></TD>");
                            }
                            else
                            {
                                returnString.Append("<TD style='text-align: right;width:30%; height:100% ;display: inline-block;float:left'><b>");
                                returnString.Append(textLine[0]);
                                returnString.Append("&nbsp;&nbsp;");
                                returnString.Append("</b></TD>");
                            }

                            returnString.Append("<TD style='text-align: left;width:68%; height:100% ;display: inline-block;float:right'>&nbsp;");
                            returnString.Append("</TD>");
                        }
                        else
                        {
                            if (textLine[0].Contains("<RED>"))
                            {
                                textLine[0] = textLine[0].Replace("<RED>", "");

                                returnString.Append("<TD style='text-align: right;width:30%; color: Red;height:100% ;display: inline-block;float:left'><b>");
                                returnString.Append(textLine[0]);
                                returnString.Append("&nbsp;&nbsp;");
                                returnString.Append("</b></TD>");
                            }
                            else
                            {
                                returnString.Append("<TD style='text-align: right;width:30%; height:100% ;display: inline-block;float:left'><b>");
                                returnString.Append(textLine[0]);
                                returnString.Append("&nbsp;&nbsp;");
                                returnString.Append("</b></TD>");
                            }

                            if (textLine[1].Contains("<RED>"))
                            {
                                textLine[1] = textLine[1].Replace("<RED>", "");

                                returnString.Append("<TD style='text-align: left;width:68%; color: Red;height:100% ;display: inline-block;float:right'>");
                                returnString.Append(textLine[1]);
                                returnString.Append("&nbsp;&nbsp;");
                                returnString.Append("</TD>");
                            }
                            else
                            {
                                returnString.Append("<TD style='text-align: left;width:68%; height:100% ;display: inline-block;float:right'>");
                                returnString.Append(textLine[1]);
                                returnString.Append("&nbsp;&nbsp;");
                                returnString.Append("</TD>");
                            }
                        }

                        returnString.Append("</TR>");
                    }

                    returnString.Append("</TABLE>");
                }
            }
            else
            {
                returnString.Append(source);
            }

            return returnString.ToString();
        }

        public static string TabulateString(string source)
        {
            StringBuilder returnString = new StringBuilder();

            if (source.Contains("<Text>"))
            {
                string[] lines = source.Split(new string[] { "<BL>" }, StringSplitOptions.RemoveEmptyEntries);

                if (lines.Length > 0)
                {
                    returnString.Append("<div>");

                    for (int i = 0; i < lines.Length; i++)
                    {
                        returnString.Append("<div style='width: 100%; display: inline-block;'>");

                        string[] textLine = lines[i].Split(new string[] { "<Text>" }, StringSplitOptions.None);

                        if (textLine.Length == 1)
                        {
                            if (textLine[0].Contains("<RED>"))
                            {
                                textLine[0] = textLine[0].Replace("<RED>", "");

                                returnString.Append("<div style='text-align: right;width:30%; color: Red;height:100% ;display: inline-block;float:left'><b>");
                                returnString.Append(textLine[0]);
                                returnString.Append("&nbsp;&nbsp;");
                                returnString.Append("</b></div>");
                            }
                            else
                            {
                                returnString.Append("<div style='text-align: right;width:30%; height:100% ;display: inline-block;float:left'><b>");
                                returnString.Append(textLine[0]);
                                returnString.Append("&nbsp;&nbsp;");
                                returnString.Append("</b></div>");
                            }

                            returnString.Append("<div style='text-align: left;width:68%; height:100% ;display: inline-block;float:right'>");
                            returnString.Append("</div>");
                        }
                        else
                        {
                            if (textLine[0].Contains("<RED>"))
                            {
                                textLine[0] = textLine[0].Replace("<RED>", "");

                                returnString.Append("<div style='text-align: right;width:30%; color: Red;height:100% ;display: inline-block;float:left'><b>");
                                returnString.Append(textLine[0]);
                                returnString.Append("&nbsp;&nbsp;");
                                returnString.Append("</b></div>");
                            }
                            else
                            {
                                returnString.Append("<div style='text-align: right;width:30%; height:100% ;display: inline-block;float:left'><b>");
                                returnString.Append(textLine[0]);
                                returnString.Append("&nbsp;&nbsp;");
                                returnString.Append("</b></div>");
                            }

                            if (textLine[1].Contains("<RED>"))
                            {
                                textLine[1] = textLine[1].Replace("<RED>", "");

                                returnString.Append("<div style='text-align: left;width:68%; color: Red;height:100% ;display: inline-block;float:right'>");
                                returnString.Append(textLine[1]);
                                returnString.Append("&nbsp;&nbsp;");
                                returnString.Append("</div>");
                            }
                            else
                            {
                                returnString.Append("<div style='text-align: left;width:68%; height:100% ;display: inline-block;float:right'>");
                                returnString.Append(textLine[1]);
                                returnString.Append("&nbsp;&nbsp;");
                                returnString.Append("</div>");
                            }
                        }

                        returnString.Append("</div>");
                    }

                    returnString.Append("</div>");
                }
            }
            else
            {
                returnString.Append(source);
            }

            return returnString.ToString();
        }

        ///// <summary>
        ///// BASIC FUNCTION FOR CHARACTER REMOVAL OF STRING
        ///// </summary>
        ///// <param name="str"></param>
        ///// <returns></returns>
        //public static string RemoveSpecialCharacters(string str)
        //{
        //    StringBuilder sb = new StringBuilder();
        //    for (int i = 0; i < str.Length; i++)
        //    {
        //        char c = str[i];

        //        if ((c >= '0' && c <= '9') || (c >= 'A' && c <= 'Z') || (c >= 'a' && c <= 'z') | c == '.' || c == '_')
        //        {
        //            sb.Append(c);
        //        }
        //    }
        //    return sb.ToString();
        //}

        public static string RemoveSpecialCharactersForControlName(string str)
        {
            StringBuilder sb = new StringBuilder();

            for (int i = 0; i < str.Length; i++)
            {
                char c = str[i];

                if ((c >= '0' && c <= '9') || (c >= 'A' && c <= 'Z') || (c >= 'a' && c <= 'z'))
                {
                    sb.Append(c);
                }
            }

            return sb.ToString();
        }
    }
}
