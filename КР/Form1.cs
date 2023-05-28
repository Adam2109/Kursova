using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace КР
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        bool errs;
        public static double f1(double q, double x)
        {
            return q * Math.Log(q * x);
        }
        public static double f2(double q, double x)
        {
            return Math.Sqrt(q) / Math.Sin(x);
        }
        public static bool err_check1(double q, double x)
        {
            if ((q * x) > 0) return false;
            else return true;
        }
        public static bool err_check2(double q, double x)
        {
            if (q >= 0 && Math.Sin(x) != 0) return false;
            else return true;
        }

        public bool LogicErrors(double x_min, double x_max, double dx)
        {
            if (errs == false)
            {
                if (x_max > x_min && (x_max - x_min) >= dx && dx > 0)
                {
                    return false;
                }
                else
                {
                    errs = true;
                    return true;
                }
            }
            else
            {
                return true;
            }
        }

        public bool Errors(double q, double x_min, double x_max, double dx)
        {
            if (errs == false)
            {
                if (q <= 0.2)
                {
                    for (double i = x_min; i <= x_max; i += dx)
                    {
                        if (!err_check1(q, i)) return true;
                    }
                }
                else
                {
                    for (double i = x_min; i <= x_max; i += dx)
                    {
                        if (!err_check2(q, i)) return true;
                    }
                }
            }
            else
            {
                return errs;
            }
            return false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "" || textBox2.Text == "" || textBox3.Text == "")
            {
                MessageBox.Show("Заповніть необхідні поля", "Помилка");
            }
            else
            {
                Random ran = new Random();

                double x_min, x_max, dx;
                bool error1, error2, error3;
                error1 = Double.TryParse(textBox1.Text, out x_min);
                error2 = Double.TryParse(textBox2.Text, out x_max);
                error3 = Double.TryParse(textBox3.Text, out dx);
                if (!error1 || !error2 || !error3)
                {
                    MessageBox.Show("Поля введено некоректно", "Помилка введених даних", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                else
                {
                    int f1Count = 0;
                    int f2Count = 0;
                    listBox1.Items.Clear();
                    listBox2.Items.Clear();

                    if (LogicErrors(x_min, x_max, dx))
                    {
                        MessageBox.Show("Були введені невірні значення табуляції", "Помилка введених даних", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else if (Errors(0, x_min, x_max, dx))
                    {
                        MessageBox.Show("Введені дані не задовільняють ОДЗ", "Помилка введених даних", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                     
                    else
                    {
                        for (double i = x_min; i <= x_max; i += dx)
                        {
                            double q = Math.Round(ran.NextDouble(), 2);
                            if (q <= 0.2)
                            {
                                if (double.IsNaN(f1(q, i)) || double.IsInfinity(f1(q, i)))
                                {
                                    listBox1.Items.Add("x = " + Math.Round(i, 2) + "; y = There is no solution.; q = " + q);

                                }
                                else
                                {
                                    listBox1.Items.Add("x = " + Math.Round(i, 2) + "; y = " + Math.Round(f1(q, i), 2) + "; q = " + q);
                                    f1Count++;

                                }
                                label8.Text = $"Amount for f1(x) = {f1Count}";

                            }
                            else
                            {
                                if (double.IsNaN(f2(q, i)) || double.IsInfinity(f2(q, i)))
                                {
                                    listBox2.Items.Add("x = " + Math.Round(i, 2) + "; y = There is no solution.; q = " + q);

                                }
                                else
                                {
                                    listBox2.Items.Add("x = " + Math.Round(i, 2) + "; y = " + Math.Round(f2(q, i), 2) + "; q = " + q);
                                    f2Count++;

                                }
                                label9.Text = $"Amount for f2(x) = {f2Count}";

                            }
                        }
                    }
                }
            }
        }
    }
}
