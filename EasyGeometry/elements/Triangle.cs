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

///
using EasyGeometry.elements;
using EasyGeometry.sys;

namespace EasyGeometry.elements
{
    class Triangle : MyFigure 
    {
        
        //````````
        public Triangle(int x1 = 90, int y1=60, int x2=60, int y2=100, int x3=120, int y3=100)
        {
            Name = "Triangle";

            Points.Add(new Point(x1, y1));
            Points.Add(new Point(x2, y2));
            Points.Add(new Point(x3, y3));

            Create_Ellipses();

            Create_Lines();

            Create_Service_Lines();

        }
        //````````
    }
}
