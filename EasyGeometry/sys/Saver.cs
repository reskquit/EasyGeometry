using System;
using System.Collections.Generic;
using System.IO;

using System.Text;
using System.Xml;
using System.Xaml;

using EasyGeometry.elements;

using Microsoft.Win32;
using static System.Net.WebRequestMethods;
using System.Windows.Shapes;

namespace EasyGeometry.sys
{
    static class Saver
    {
        private static XmlDocument createXmlDoc()
        {
            XmlDocument xDoc = new XmlDocument();
            return xDoc;
        }
        public static void saveDialog()
        {
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            saveFileDialog1.DefaultExt = ".xml";
            saveFileDialog1.Filter = "Xml files (*.xml)|*.xml";
            saveFileDialog1.FilterIndex = 2;
            saveFileDialog1.RestoreDirectory = true;
            
            if (saveFileDialog1.ShowDialog() == true)
            {
                //create new Xml doc
                XmlDocument xDoc = new XmlDocument();
                xDoc.Load("E:\\Илья\\CSharp\\EasyGeometry\\EasyGeometry\\saves\\dm.xml");
                //create new Root elem
                XmlElement xRoot = xDoc.DocumentElement;
                foreach(MyFigure figure in ShapeManager.P_CurrentFigure)
                {
                    //we will save every figure as new Xml-node
                    XmlElement figureElem = xDoc.CreateElement("MyFigure");

                    XmlAttribute figureNameAttr = xDoc.CreateAttribute("class");
                    XmlText figureNameText = xDoc.CreateTextNode(figure.Name);
                    figureNameAttr.AppendChild(figureNameText);
                    figureElem.Attributes.Append(figureNameAttr);

                    XmlElement linesListElem = xDoc.CreateElement("lines");

                    foreach (Line ln in figure.P_Lines)
                    {
                        XmlElement lineElem = xDoc.CreateElement("line");

                        XmlAttribute x1Attr = xDoc.CreateAttribute("x1");
                        XmlText x1AttrText = xDoc.CreateTextNode(Convert.ToString(ln.X1));
                        x1Attr.AppendChild(x1AttrText);
                        lineElem.Attributes.Append(x1Attr);

                        XmlAttribute y1Attr = xDoc.CreateAttribute("y1");
                        XmlText y1AttrText = xDoc.CreateTextNode(Convert.ToString(ln.Y1));
                        y1Attr.AppendChild(y1AttrText);
                        lineElem.Attributes.Append(y1Attr);

                        XmlAttribute x2Attr = xDoc.CreateAttribute("x2");
                        XmlText x2AttrText = xDoc.CreateTextNode(Convert.ToString(ln.X2));
                        x2Attr.AppendChild(x2AttrText);
                        lineElem.Attributes.Append(x2Attr);

                        XmlAttribute y2Attr = xDoc.CreateAttribute("y2");
                        XmlText y2AttrText = xDoc.CreateTextNode(Convert.ToString(ln.Y2));
                        y2Attr.AppendChild(y2AttrText);
                        lineElem.Attributes.Append(y2Attr);

                        linesListElem.AppendChild(lineElem);
                    }
                    figureElem.AppendChild(linesListElem);
                    xRoot.AppendChild(figureElem);
                }
                xDoc.Save(saveFileDialog1.FileName);
            }
        }
        static void Save_file()
        {
            List<MyFigure> save = ShapeManager.P_CurrentFigure;
        }
    }
}
