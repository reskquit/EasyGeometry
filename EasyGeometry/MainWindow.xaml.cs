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
using System.Threading;

using EasyGeometry.elements;
using EasyGeometry.sys;

namespace EasyGeometry
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            //инициализируем 
            InitializeComponent();
        }

        public List<Line> FocusedElementLines = new List<Line>();

        private void Create_Triangle(object sender, RoutedEventArgs e)
        {
            Triangle tri = new Triangle();
            ShapeManager.Add_ToCurent(tri);
            AddFunctionTo(tri);
            AddServiceFunctionTo(tri);
            Render();
            number_of_triangles.Text = Convert.ToString(ShapeManager.Get_CountOf("Triangle"));
        }

        private void Create_Square(object sender, RoutedEventArgs e)
        {
            Square sq = new Square();
            ShapeManager.Add_ToCurent(sq);
            AddFunctionTo(sq);
            AddServiceFunctionTo(sq);
            Render();
            number_of_squares.Text = Convert.ToString(ShapeManager.Get_CountOf("Square"));
        }

        public void AddServiceFunctionTo(MyFigure fr)
        {
            foreach(Line ln in fr.P_Service_Lines)
            {
                ln.MouseEnter += Line_MouseEnter_Service;
                ln.MouseLeftButtonDown += Line__MouseLeftButtonDown_Service;
                ln.MouseMove += Line_MouseMove_Service;
                ln.MouseLeftButtonUp += Line_MouseLeftButtonUp_Service;
                ln.MouseLeave += Line_MouseLeave_Service;
            }
        }

        public void AddFunctionTo(MyFigure fr)
        {
            foreach (Ellipse el in fr.P_Ellipses)
            {
                el.MouseLeftButtonDown += Ellipse_MouseLeftButtonDown;
                el.MouseLeftButtonUp += Ellipse_MouseLeftButtonUp;
                el.MouseMove += Any_MouseMove;
                el.MouseEnter += Ellipse_MouseEnter;
                el.MouseLeave += Ellipse_MouseLeave;
            }
        }
        public void Render()
        {
            Field.Children.Clear();

            foreach(MyFigure figure in ShapeManager.P_CurrentFigure)
            {
                foreach (Line ln in figure.P_Lines)
                {
                    Field.Children.Add(ln);
                    //Dispatcher.Invoke(() => ln.InvalidateVisual());
                }
                foreach (Line s_ln in figure.P_Service_Lines)
                {
                    Field.Children.Add(s_ln);
                }
                //для каждой точки в figure
                foreach (Ellipse el in figure.P_Ellipses)
                {
                    Field.Children.Add(el);
                }
            }
        }

        private void Field_MouseMove(object sender, MouseEventArgs e)
        {
            Point position = Mouse.GetPosition((Canvas)sender);
            DebugWindow.Text = "X" + position.X + "Y" + position.Y;
        }
        //we chose the point
        private void Field_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

            Point position = Mouse.GetPosition((Canvas)sender);
        }
        //we change this point position


        private UIElement source;

        private MyFigure sourceFigure;

        public bool captured;

        double x_shape, x_canvas, y_shape, y_canvas;

        private void Ellipse_MouseEnter(object sender, MouseEventArgs e)
        {
            Mouse.OverrideCursor = Cursors.SizeAll;
        }

        private void Ellipse_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Mouse.OverrideCursor = Cursors.SizeAll;
            source = sender as Ellipse;
            sourceFigure = ShapeManager.GetParentFigure((Ellipse)source);
            Mouse.Capture(source);
            captured = true;
            x_shape = Canvas.GetLeft(source);
            x_canvas = e.GetPosition(Field).X;
            y_shape = Canvas.GetTop(source);
            y_canvas = e.GetPosition(Field).Y;
            DebugWindow.Text = "x_shape " + Canvas.GetLeft(source).ToString() + "\n"
                + "x_canvas " + e.GetPosition(Field).X.ToString() + "\n"+
                "y_shape " + Canvas.GetTop(source).ToString() + "\n" +
                "y_canvas " + e.GetPosition(Field).Y.ToString();
        }

        private void Any_MouseMove(object sender, MouseEventArgs e)
        {
            if (captured)
            {
                //получаем фигуру к которой принадлежит точка
                MyFigure thisfigure = sourceFigure;
                DebugWindow.Text = thisfigure.Name;

                //получаем текущие координаты мыши
                double x = e.GetPosition(Field).X;
                double y = e.GetPosition(Field).Y;
                //вычисляем смещение и прибавляем его к текущим координатам обьекта точки

                if ((x_shape + x - x_canvas) >= 0 & (x_shape + x - x_canvas) <= Field.ActualWidth)
                {
                    //если нет выхода за границу то изменяем положение фигуры
                    x_shape += x - x_canvas;
                    //устанавливаем положение фигуры          
                    //обновляем фигуру
                    thisfigure.UpdateFigure((Ellipse)source, x_shape, y_shape);
                    //меняем позицию мыши
                    x_canvas = x;
                }

                if ((y_shape + y - y_canvas) >= 0 & (y_shape + y - y_canvas) <= Field.ActualHeight )
                {
                    //если нет выхода за границу то изменяем положение фигуры
                    y_shape += y - y_canvas;
                    //устанавливаем положение фигуры
                    //меняем позицию поинт
                    thisfigure.UpdateFigure((Ellipse)source, x_shape, y_shape);
                    //меняем позицию мыши
                    y_canvas = y;
                }
                DebugWindow.Text = "x_shape " + Canvas.GetLeft(source).ToString() + "\n"
                + "x_canvas " + e.GetPosition(Field).X.ToString() + "\n" +
                "y_shape " + Canvas.GetTop(source).ToString() + "\n" +
                "y_canvas " + e.GetPosition(Field).Y.ToString();
                //изменяем координату в обьекте фигуры
                Render();
            }
        }

        private void Ellipse_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            DebugWindow.Text = "Вызванна функция отжатия клавиши";
            Mouse.Capture(null);
            captured = false;
            Mouse.OverrideCursor = null;
        }

        private void Ellipse_MouseLeave(object sender, MouseEventArgs e)
        {
            Mouse.Capture(null);
            captured = false;
            Mouse.OverrideCursor = null;
        }

        //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
        private void Line_MouseEnter_Service(object sender, MouseEventArgs e)
        {
            Mouse.OverrideCursor = Cursors.Hand;
            //вызываеться когда пользователь навелся на служебную линию
        }
        private void Line__MouseLeftButtonDown_Service(object sender, MouseButtonEventArgs e)
        {
            Mouse.OverrideCursor = Cursors.Hand;
            source =(Line)sender;
            captured = true;
            Mouse.Capture(source);
            sourceFigure = ShapeManager.GetParentFigure_Service((Line)source);

            

            x_canvas = e.GetPosition(Field).X;
            y_canvas = e.GetPosition(Field).Y;

            DebugWindow.Text = "x_shape " + x_shape.ToString() + "\n"
               + "x_canvas " + x_canvas.ToString() + "\n" +
               "y_shape " + y_shape.ToString() + "\n" +
               "y_canvas " + y_canvas.ToString();
            //source = sender as Line;
        }

        private void Line_MouseMove_Service(object sender, MouseEventArgs e)
        {
            if (captured)
            {
                MyFigure thisfigure = sourceFigure;
                DebugWindow.Text = thisfigure.Name;
                
                double x = e.GetPosition(Field).X;
                double y = e.GetPosition(Field).Y;
                if (IsMovementXAllowed(x, y, x_canvas, y_canvas, thisfigure))
                {
                    
                    //обновляем фигуру
                    thisfigure.UpdateXFigure(x - x_canvas);
                    //меняем позицию мыши
                    x_canvas = x;
                }
                if (IsMovementYAllowed(x, y, x_canvas, y_canvas, thisfigure))
                {
                    
                    //устанавливаем положение фигуры
                    //обновляем фигуру
                    thisfigure.UpdateYFigure(y - y_canvas);
                    //меняем позицию мыши
                    y_canvas = y;
                }
                Render();
            }
        }
        private void Line_MouseLeftButtonUp_Service(object sender, MouseButtonEventArgs e)
        {
            DebugWindow.Text = "Вызванна функция отжатия клавиши";
            Mouse.Capture(null);
            captured = false;
            Mouse.OverrideCursor = null;
        }

        private void save_doc(object sender , RoutedEventArgs e)
        {
            Saver.saveDialog();
        }

        private void Line_MouseLeave_Service(object sender, MouseEventArgs e)
        {
            Mouse.Capture(null);
            Mouse.OverrideCursor = null;
            captured = false;
        }

        //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
        private bool IsMovementXAllowed(double x, double y, double x_canvas, double y_canvas, MyFigure thisfigure)
        {
            bool Flag = true;
            foreach(Line S_line in thisfigure.P_Service_Lines)
            {
                Flag = Flag & ((S_line.X1 + x - x_canvas) >= 0 & (S_line.X1 + x - x_canvas) <= Field.ActualWidth );
                Flag = Flag & ((S_line.X2 + x - x_canvas) >= 0 & (S_line.X2 + x - x_canvas) <= Field.ActualWidth );
            }
            return Flag;
        }
        private bool IsMovementYAllowed(double x, double y, double x_canvas, double y_canvas, MyFigure thisfigure)
        {
            bool Flag = true;
            foreach (Line S_line in thisfigure.P_Service_Lines)
            {
                Flag = Flag & ((S_line.Y1 + y - y_canvas) >= 0 & (S_line.Y1 + y - y_canvas) <= Field.ActualHeight );
                Flag = Flag & ((S_line.Y2 + y - y_canvas) >= 0 & (S_line.Y2 + y - y_canvas) <= Field.ActualHeight );
            }
            return Flag;
        }
    }
}
