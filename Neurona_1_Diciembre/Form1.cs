using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Neurona_1_Diciembre
{
    public partial class Form1 : Form
    {
        double m = 0;
        double b = -1;
        double c = 0;
        double error = 0;
        double[] resultado1 = new double [7];
        double[] resultado2 = new double[7];
        double[] binario1 = new double[7];
        double[] binario2 = new double[7];

        double[] rectax = new double[7]  {0.1,0.2,0.3,0.4,0.5,0.6,0.7};
        double[] Datos1_x = new double[7] { 0, 0.1, 0.4, 0.3, 0.2, 0.4, 0.2 };
        double[] Datos1_y = new double[7] { 1, 0.9, 0.6, 0.7, 0.5, 0.8, 0.9 };

        double[] Datos2_x = new double[7] { 0.4, 0.5, 0.5, 0.6, 0.5, 0.6, 0.7 };
        double[] Datos2_y = new double[7] { 0.1, 0.3, 0.1, 0.6, 0.4, 0.2, 0.1 };
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {


            for (int i = 0; i < Datos1_x.Length; i++)
            {
                chart1.Series[1].Points.AddXY(Datos1_x[i], Datos1_y[i]);
                chart1.Series[2].Points.AddXY(Datos2_x[i], Datos2_y[i]);
            }
            if (timer1.Enabled)
                timer1.Stop();
            else
                timer1.Start();
            chart1.ChartAreas[0].AxisX.ScaleView.Zoomable = true;
            chart1.ChartAreas[0].CursorX.AutoScroll = true;
            chart1.ChartAreas[0].CursorX.IsUserSelectionEnabled = true;
            chart2.ChartAreas[0].AxisX.ScaleView.Zoomable = true;
            chart2.ChartAreas[0].CursorX.AutoScroll = true;
            chart2.ChartAreas[0].CursorX.IsUserSelectionEnabled = true;


        }
        int k = 0;
        private void timer1_Tick(object sender, EventArgs e)
        {
            
            chart1.Series[0].Points.Clear();
            for (int i = 0; i < Datos1_x.Length; i++)
            {
                resultado1[i] = Datos1_x[i] *m + Datos1_y[i] *b + c;
                resultado2[i] = Datos2_x[i] *m + Datos2_y[i] *b + c; 
            }
            for (int i = 0; i < Datos1_x.Length; i++)
            {
                if (resultado1[i]<= 0)
                {
                    binario1[i] = 0;
                }
                else
                {
                    binario1[i] = 1;
                }
            }
            for (int i = 0; i < Datos2_x.Length; i++)
            {
                if (resultado2[i] >= 0)
                {
                    binario2[i] = 0;
                }
                else
                {
                    binario2[i] = 1;
                }
            }
            for (int i = 0; i <rectax.Length; i++)
            {
                chart1.Series[0].Points.AddXY(rectax[i], rectax[i] * m + 0 );
                textBox3.Text = Convert.ToString(error);

            }
            if (error == 0)
            {
                textBox2.Text = Convert.ToString(m);
                timer1.Stop();
            }
            error = Math.Abs((binario1.Sum() - binario2.Sum()) / Datos1_x.Count());
             chart2.Series[0].Points.AddXY(k, error);
            m = m + .1;
            k++;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            timer1.Tick += timer1_Tick;
            timer1.Interval = 200;
        }
    }
}
