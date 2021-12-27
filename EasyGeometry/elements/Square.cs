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
/// 

namespace EasyGeometry.elements
{
    class Square :  MyFigure
    {
        protected List<int> Ribs = new List<int>(2);
        /// <summary>
        /// gets the coordinates of opposite points
        /// </summary>
        public Square(int x1 = 30, int y1 = 20, int x2 = 60, int y2 = 50)
        {
            Ribs.Add(Math.Abs(x2 - x1));
            Ribs.Add(Math.Abs(y2 - y1));

            Points.Add(new Point(x1, y1));
            Points.Add(new Point(x1 + Ribs[0], y1));
            Points.Add(new Point(x2, y2));
            Points.Add(new Point(x1, y1 + Ribs[1]));
            Create_Ellipses();
            Create_Lines();
            Create_Service_Lines();
            Name = "Square";
        }
        public override void UpdateFigure(Ellipse ellipse, double x, double y)
        {
            
        }
        protected void Update_PointsAll(double x, double y)
        {

        }
    }
}
