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
using EasyGeometry.sys;


namespace EasyGeometry.elements
{
    public class MyFigure
    {
        /// <summary>
        /// MouseMove="Any_MouseMove"
        /// MouseLeftButtonUp="Ellipse_MouseLeftButtonUp"
        /// MouseLeftButtonDown="Ellipse_MouseLeftButtonDown"
        /// </summary>

        //блокировщик для Ellips
        private readonly object EllipseLock = new object();

        private readonly object PointLock = new object();

        private readonly object LineLock = new object();


        //every figure is an array of a Points
        protected List<Point> Points = new List<Point>();

        //every printed figure is an array of Elipses
        protected List<Ellipse> Ellipses = new List<Ellipse>();

        //every printed figure is an array of Lines
        protected List<Line> Lines = new List<Line>();

        //Service Lines array to manage the user activity
        protected List<Line> Service_Lines = new List<Line>();


        public List<Ellipse> P_Ellipses
        {
            get
            {
                return Ellipses;
            }
            set
            {
                Ellipses = value;
            }
        }

        public List<Line> P_Lines
        {
            get
            {
                return Lines;
            }
            set
            {
                Lines = value;
            }
        }

        public List<Line> P_Service_Lines
        {
            get
            {
                return Service_Lines;
            }
            set
            {
                Service_Lines = value;
            }
        }

        public string Name { get; protected set; } = "MyFigure";
        /// <summary>
        /// возвращает текущий набор линий синхронизированный с Point - лист
        /// </summary>
        /// <returns></returns>
        protected virtual List<Line> GetFiguresLinesCurent()
        {
            lock (LineLock)
            {
                List<Line> LineArrayInter = new List<Line>();
                for (int i = 0; i < Points.Count - 1; i++)
                {
                    Line ln = new Line();
                    ln.X1 = Points[i].X;
                    ln.Y1 = Points[i].Y;
                    ln.X2 = Points[i + 1].X;
                    ln.Y2 = Points[i + 1].Y;
                    ln.Stroke = Brushes.Black;
                    ln.StrokeThickness = 2;
                    ln.Name = Name + "I" + Convert.ToString(i);
                    LineArrayInter.Add(ln);
                }
                Line ln1 = new Line();
                ln1.X1 = Points[0].X;
                ln1.Y1 = Points[0].Y;
                ln1.X2 = Points[Points.Count - 1].X;
                ln1.Y2 = Points[Points.Count - 1].Y;
                ln1.Stroke = Brushes.Black;
                ln1.Name = Name + "I" + Convert.ToString(Points.Count - 1);
                ln1.StrokeThickness = 2;

                LineArrayInter.Add(ln1);
                Lines = LineArrayInter;
                return Lines;
            }
            
        }
        /// <summary>
        /// возвращает текущий набор графических точек синхронизированный с Point - лист
        /// </summary>
        /// <returns></returns>
        
        protected virtual List<Ellipse> GetFiguresElipsesCurent()
        {

            lock (EllipseLock)
            {
                List<Ellipse> EllipsesArraay = new List<Ellipse>();
                for (int i = 0; i < Points.Count; i++)
                {
                    Ellipse el = new Ellipse();
                    el.Width = 6;
                    el.Height = 6;

                    el.StrokeThickness = 6;
                    el.Stroke = Brushes.Red;
                    el.Fill = Brushes.Red;

                    el.Name = Name +this.GetHashCode() + "I" + Convert.ToString(i);

                    Canvas.SetLeft(el, Points[i].X - 3);
                    Canvas.SetTop(el, Points[i].Y - 3);
                    
                    
                    el.HorizontalAlignment = new HorizontalAlignment();
                    el.VerticalAlignment = new VerticalAlignment();
                    

                    EllipsesArraay.Add(el);
                    Ellipses = EllipsesArraay;
                }
                return Ellipses;
            }
            
            
        }

        /// <summary>
        /// Приводит все значения Point, Lines, Ellipses к достоверному виду
        /// </summary>
        /// 
        
        public void UpdateAllFigure(double vect_x, double vect_y)
        {
            ///обновяем Points
            Update_Points(vect_x, vect_y);
            ///обновляем Lines
            Update_Lines();
            ///обновляем Ellipses
            Update_Ellipse();
            //обновляем сервисные линии
            Update_Service_Lines();
        }
        public virtual void UpdateFigure(Ellipse ellipse, double x, double y)
        {
            ///обновяем Points
            Update_Points(ellipse, x, y);
            ///обновляем Lines
            Update_Lines();
            ///обновляем Ellipses
            Update_Ellipse();
            //обновляем сервисные линии
            Update_Service_Lines();
        }

        //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

        public void UpdateXFigure(double vect_x)
        {
            Update_Points_X(vect_x);
            ///обновляем Lines
            Update_Lines();
            ///обновляем Ellipses
            Update_Ellipse();
            //обновляем сервисные линии
            Update_Service_Lines();
        }
        public void UpdateYFigure(double vect_y)
        {
            Update_Points_Y(vect_y);
            ///обновляем Lines
            Update_Lines();
            ///обновляем Ellipses
            Update_Ellipse();
            //обновляем сервисные линии
            Update_Service_Lines();
        }

        //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

        protected void Update_Points_X(double vect_x)
        {
            for (int i = 0; i < Points.Count; i++)
            {
                Point po = Points[i];
                po.X += vect_x;
                Points[i] = po;
            }
        }
        protected void Update_Points_Y(double vect_y)
        {
            for (int i = 0; i < Points.Count; i++)
            {
                Point po = Points[i];
                po.Y += vect_y;
                Points[i] = po;
            }
        }

        
        //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

        protected void Update_Points(double vect_x, double vect_y)
        {
            for(int i = 0; i< Points.Count; i++)
            {
                Point po = Points[i];
                po.X += vect_x;
                po.Y += vect_y;
                Points[i] = po;
            } 
        }
        protected virtual void Update_Points(Ellipse ellipse, double x, double y)
        {
            //обновляем поинт
            //получаем имя Elips-a
            string name = ellipse.Name;
            //получаем индекс этого элипса
            int index = Convert.ToInt32(name.Split("_").Last());
            //Получаем обьект Point по индексу Elips-a
            Point point = Points[index];
            //устанавливаем переданные значения
            point.X = x;
            point.Y = y;
            //присваеваем обновленный элемент
            Points[index] = point;
        }

        //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

        protected void Create_Lines()
        {
            for (int i = 0; i < Points.Count - 1; i++)
            {
                Line ln = new Line();
                ln.X1 = Points[i].X;
                ln.Y1 = Points[i].Y;
                ln.X2 = Points[i + 1].X;
                ln.Y2 = Points[i + 1].Y;
                ln.Stroke = Brushes.Black;
                ln.StrokeThickness = 2;
                ln.Name = Name + this.GetHashCode() + "Line" + "_" + Convert.ToString(i);
                Lines.Add(ln);
            }
            Line ln1 = new Line();
            ln1.X1 = Points[0].X;
            ln1.Y1 = Points[0].Y;
            ln1.X2 = Points[Points.Count - 1].X;
            ln1.Y2 = Points[Points.Count - 1].Y;
            ln1.Stroke = Brushes.Black;
            ln1.Name = Name + this.GetHashCode() + "Line" + "_" + Convert.ToString(Points.Count - 1);
            ln1.StrokeThickness = 2;
            Lines.Add(ln1);
        }
        protected void Update_Lines()
        {
            for (int i = 0; i < Points.Count - 1; i++)
            {
                Line ln = Lines[i];
                ln.X1 = Points[i].X;
                ln.Y1 = Points[i].Y;
                ln.X2 = Points[i + 1].X;
                ln.Y2 = Points[i + 1].Y;
                Lines[i] = ln;
            }
            Line ln1 = Lines[Points.Count - 1];
            ln1.X1 = Points[0].X;
            ln1.Y1 = Points[0].Y;
            ln1.X2 = Points[Points.Count - 1].X;
            ln1.Y2 = Points[Points.Count - 1].Y;
            Lines[Points.Count - 1] = ln1;
        }
        
        //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

        protected void Create_Ellipses()
        {
            for (int i = 0; i < Points.Count; i++)
            {
                Ellipse el = new Ellipse();
                el.Width = 6;
                el.Height = 6;
                el.StrokeThickness = 6;
                el.Stroke = Brushes.Red;
                el.Fill = Brushes.Red;
                el.Name = Name + this.GetHashCode()+ "Ellipse" + "_" + Convert.ToString(i);
                Canvas.SetLeft(el, Points[i].X - 3);
                Canvas.SetTop(el, Points[i].Y - 3);
                Ellipses.Add(el);
            }
        }
        protected void Update_Ellipse()
        {
            for (int i = 0; i < Points.Count; i++)
            {
                Canvas.SetLeft(P_Ellipses[i], Points[i].X - 3);
                Canvas.SetTop(P_Ellipses[i], Points[i].Y - 3);
            }
        }

        //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

        protected void Create_Service_Lines()
        {
            for (int i = 0; i < Points.Count - 1; i++)
            {
                Line ln = new Line();
                ln.X1 = Points[i].X;
                ln.Y1 = Points[i].Y;
                ln.X2 = Points[i + 1].X;
                ln.Y2 = Points[i + 1].Y;
                ln.Stroke = Brushes.Red;
                ln.Opacity = 0.0;
                ln.StrokeThickness = 10;
                ln.Name = Name +this.GetHashCode()+ "ServiceLine"+ "_" + Convert.ToString(i);
                Service_Lines.Add(ln);
            }
            Line ln1 = new Line();
            ln1.X1 = Points[0].X;
            ln1.Y1 = Points[0].Y;
            ln1.X2 = Points[Points.Count - 1].X;
            ln1.Y2 = Points[Points.Count - 1].Y;
            ln1.Stroke = Brushes.Red;
            ln1.Opacity = 0.0;
            ln1.StrokeThickness = 10;
            ln1.Name = Name + this.GetHashCode() + "ServiceLine" + "_" + Convert.ToString(Points.Count - 1);
            Service_Lines.Add(ln1);
        }
        protected void Update_Service_Lines()
        {
            for (int i = 0; i < Points.Count - 1; i++)
            {
                Line ln = Service_Lines[i];
                ln.X1 = Points[i].X;
                ln.Y1 = Points[i].Y;
                ln.X2 = Points[i + 1].X;
                ln.Y2 = Points[i + 1].Y;
                Service_Lines[i] = ln;
            }
            Line ln1 = Service_Lines[Points.Count - 1];
            ln1.X1 = Points[0].X;
            ln1.Y1 = Points[0].Y;
            ln1.X2 = Points[Points.Count - 1].X;
            ln1.Y2 = Points[Points.Count - 1].Y;
            Service_Lines[Points.Count - 1] = ln1;
        }

        //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

        protected List<Point> GetСonnectedPoints(Point point)
        {
            List<Point> arr = new List<Point>(); 
            foreach(Line ln in Lines)
            {
                if(point.X == ln.X1 & point.Y ==ln.Y1)
                {
                    arr.Add(new Point(ln.X2, ln.Y2));
                }
                if (point.X == ln.X2 & point.Y == ln.Y2)
                {
                    arr.Add(new Point(ln.X1, ln.Y1));
                }
            }
            return arr;
        }

        //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
        protected Point GetPointbyElipse(Ellipse ellipse)
        {
            //получаем имя Elips-a
            string name = ellipse.Name;
            //получаем индекс этого элипса
            int index = Convert.ToInt32(name.Split("_").Last());
            //Получаем обьект Point по индексу Elips-a
            Point point = Points[index];
            return point;
        }
    }
}
