using System;
using System.Drawing;
using System.Windows.Forms;

namespace Snowflake
{
    public partial class Main : Form
    {
        static Pen pencil1;
        static Pen pencil2;
        static Graphics g;

        public Main()
        {
            InitializeComponent();
        }

        private void Render(object sender, EventArgs e)
        {
            //Цвет первого карандаша 
            pencil1 = new Pen(Color.Lime, 1);
            //Цвет второго карандаша 
            pencil2 = new Pen(Color.Orange, 1);
            //Холст
            g = CreateGraphics();
            //Заливаем холст черным цветом
            g.Clear(Color.Black);

            //Координаты исходного треугольника
            var point1 = new PointF(250, 250);
            var point2 = new PointF(625, 250);
            var point3 = new PointF(437.5f, 500);

            //Рисуем исходный треугольник
            g.DrawLine(pencil1, point1, point2);
            g.DrawLine(pencil1, point2, point3);
            g.DrawLine(pencil1, point3, point1);

            //Три кривых Коха на сторонах треугольника
            RenderKoсh(point1, point2, point3, (int)this.numericUpDown1.Value);
            RenderKoсh(point2, point3, point1, (int)this.numericUpDown1.Value);
            RenderKoсh(point3, point1, point2, (int)this.numericUpDown1.Value);
        }

        static int RenderKoсh(PointF p1, PointF p2, PointF p3, int iteration)
        {
            if (iteration > 0)
            {
                //Средняя треть отрезка
                var p4 = new PointF((p2.X + 2 * p1.X) / 3, (p2.Y + 2 * p1.Y) / 3);
                var p5 = new PointF((2 * p2.X + p1.X) / 3, (p1.Y + 2 * p2.Y) / 3);
                //Координаты вершины угла
                var ps = new PointF((p2.X + p1.X) / 2, (p2.Y + p1.Y) / 2);
                var pn = new PointF((4 * ps.X - p3.X) / 3, (4 * ps.Y - p3.Y) / 3);
                //Отрисовка
                g.DrawLine(pencil1, p4, pn);
                g.DrawLine(pencil1, p5, pn);
                g.DrawLine(pencil2, p4, p5);

                //Рекурсия
                RenderKoсh(p4, pn, p5, iteration - 1);
                RenderKoсh(pn, p5, p4, iteration - 1);
                RenderKoсh(p1, p4, new PointF((2 * p1.X + p3.X) / 3, (2 * p1.Y + p3.Y) / 3), iteration - 1);
                RenderKoсh(p5, p2, new PointF((2 * p2.X + p3.X) / 3, (2 * p2.Y + p3.Y) / 3), iteration - 1);

            }
            return iteration;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
