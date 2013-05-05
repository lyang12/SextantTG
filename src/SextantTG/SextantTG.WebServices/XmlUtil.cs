﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml;
using SextantTG.Util;

namespace SextantTG.WebServices
{
    public class XmlUtil
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="xml"></param>
        /// <param name="xpath"></param>
        /// <returns></returns>
        internal static object ReadSelectXPath(XmlDocument xml, string xpath)
        {
            return xml.SelectSingleNode(xpath).InnerText.Trim();
        }


        /// <summary>
        /// 读取指定节点，并转换成字符串
        /// </summary>
        /// <param name="xml"></param>
        /// <param name="xpath"></param>
        /// <returns></returns>
        internal static string ReadSelectXPathString(XmlDocument xml, string xpath)
        {
            return xml.SelectSingleNode(xpath).InnerText.Trim();
        }

        /// <summary>
        /// 读取指定多个节点，并转换成字符串列表
        /// </summary>
        /// <param name="xml"></param>
        /// <param name="xpath"></param>
        /// <returns></returns>
        internal static List<string> ReadSelectXPathStringList(XmlDocument xml, string xpath)
        {
            List<string> list = new List<string>();
            foreach (XmlNode node in xml.SelectNodes(xpath))
            {
                list.Add(node.InnerText.Trim());
            }
            return list;
        }

        /// <summary>
        /// 读取指定节点，并转换成Int?
        /// </summary>
        /// <param name="xml"></param>
        /// <param name="xpath"></param>
        /// <returns></returns>
        internal static int? ReadSelectXPathInt(XmlDocument xml, string xpath)
        {
            return CustomTypeConverter.ToInt32Null(xml.SelectSingleNode(xpath).InnerText.Trim());
        }

        /// <summary>
        /// 读取指定节点，并转换成DateTime?
        /// </summary>
        /// <param name="xml"></param>
        /// <param name="xpath"></param>
        /// <returns></returns>
        internal static DateTime? ReadSelectXPathDateTime(XmlDocument xml, string xpath)
        {
            return CustomTypeConverter.ToDateTimeNull(xml.SelectSingleNode(xpath).InnerText.Trim());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="xml"></param>
        /// <param name="xpath"></param>
        /// <returns></returns>
        internal static float? ReadSelectXPathFloat(XmlDocument xml, string xpath)
        {
            return CustomTypeConverter.ToFloatNull(xml.SelectSingleNode(xpath).InnerText.Trim());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="xml"></param>
        /// <param name="xpath"></param>
        /// <returns></returns>
        internal static bool? ReadSelectXPathBoolean(XmlDocument xml, string xpath)
        {
            return CustomTypeConverter.ToBooleanNull(xml.SelectSingleNode(xpath).InnerText.Trim());
        }

        /// <summary>
        /// 写入一个XML节点
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="localName"></param>
        /// <param name="value"></param>
        internal static void WriteElement(XmlWriter writer, string localName, object value)
        {
            writer.WriteStartElement(localName);
            writer.WriteString(value != null ? value.ToString() : "");
            writer.WriteEndElement();
        }




    }
}