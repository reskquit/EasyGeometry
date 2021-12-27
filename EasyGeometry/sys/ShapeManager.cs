using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

using EasyGeometry.elements;


namespace EasyGeometry.sys
{
    public static class ShapeManager
    {
        private static readonly object Current_FigureLock = new object();
        //array of curent Shapes

        private static List<MyFigure> Current_Figure = new List<MyFigure>();

        public static List<MyFigure> P_CurrentFigure
        {
            get
            {
                return Current_Figure;
            }
            set
            {
                Current_Figure = value;
            }
        }

        //array of deleted  figures
        private static List<MyFigure> Deleted_Figure = new List<MyFigure>();
        
        public static void Add_ToCurent(MyFigure figure)
        {
            Current_Figure.Add(figure);
        }

        public static int Get_CountOf(string name)
        {
            int count = 0;
            for(int i = 0; i< Current_Figure.Count; i++)
            {
                if(Current_Figure[i].Name == name)
                {
                    count++;
                }
            }
            return count;
        }
        public static MyFigure GetParentFigure(Ellipse el)
        {
            string name = el.Name;
            foreach(MyFigure myFigure in Current_Figure)
            {
                foreach(Ellipse ellipse in myFigure.P_Ellipses)
                {
                    if(ellipse.Name == name)
                    {
                        return myFigure;
                    }
                }
            }
            return null;
        }
        public static MyFigure GetParentFigure(Line ln)
        {
            string name = ln.Name;
            foreach (MyFigure myFigure in Current_Figure)
            {
                foreach(Line line in myFigure.P_Lines)
                {
                    if (line.Name == name)
                    {
                        return myFigure;
                    }
                }
            }
            return null;
        }
        public static MyFigure GetParentFigure_Service(Line ln)
        {
            string name = ln.Name;
            foreach (MyFigure myFigure in Current_Figure)
            {
                foreach (Line line in myFigure.P_Service_Lines)
                {
                    if (line.Name == name)
                    {
                        return myFigure;
                    }
                }
            }
            return null;
        }
    }
}
