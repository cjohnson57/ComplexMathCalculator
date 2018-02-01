using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace ComplexMathCalculator
{
    public struct ComplexNumber
    {
        public double real;
        public double imaginary;
    }
    public class ParseException : Exception
    {
        public string number;
    }
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private ComplexNumber ComplexNumberParse(string s)
        {
            Regex both = new Regex("([-]?[0-9]+\\.?([0-9]+)?)([-|+]+[0-9]+\\.?([0-9]+)?)[j$]+");
            Regex real = new Regex("([-]?[0-9]+\\.?([0-9]+)?)$");
            Regex imaginary = new Regex("([-]?[0-9]+\\.?([0-9]+)?)[j$]");
            ComplexNumber C = new ComplexNumber();
            if (both.Match(s).Success)
            {
                try
                {
                    int endindex = 0;
                    int extra = 0;
                    if (s.Contains("+"))
                    {
                        endindex = s.IndexOf("+");
                        extra = 1;
                    }
                    else if (s.Contains("-") && s.IndexOf("-") > 0)
                    {
                        endindex = s.IndexOf("-");
                    }
                    else if (s.Contains("-") && s.IndexOf("-") == 0)
                    {
                        endindex = s.IndexOf("-", 1);
                    }
                    C.real = double.Parse(s.Substring(0, endindex));
                    string a = s.Substring(endindex + extra, s.IndexOf("j") - endindex - 1);
                    C.imaginary = double.Parse(s.Substring(endindex + extra, s.IndexOf("j") - endindex - extra));
                }
                catch
                {
                    ParseException ex = new ParseException();
                    ex.number = s;
                    throw ex;
                }
            }
            else if(real.Match(s).Success)
            {
                try
                {
                    C.real = double.Parse(s);
                    C.imaginary = 0;
                }
                catch
                {
                    ParseException ex = new ParseException();
                    ex.number = s;
                    throw ex;
                }
            }
            else if(imaginary.Match(s).Success)
            {
                try
                {
                    C.real = 0;
                    C.imaginary = double.Parse(s.Replace("j", ""));
                }
                catch
                {
                    ParseException ex = new ParseException();
                    ex.number = s;
                    throw ex;
                }
            }
            else
            {
                ParseException ex = new ParseException();
                ex.number = s;
                throw ex;
            }
            return C;

        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                ComplexNumber C1 = ComplexNumberParse(textBox1.Text);
                ComplexNumber C2 = ComplexNumberParse(textBox2.Text);
                double newreal = C1.real + C2.real;
                double newimaginary = C1.imaginary + C2.imaginary;
                if (newimaginary < 0)
                {
                    textBox3.Text = newreal.ToString("#.00000") + newimaginary.ToString("#.00000") + "j";
                }
                else
                {
                    textBox3.Text = newreal.ToString("#.00000") + "+" + newimaginary.ToString("#.00000") + "j";
                }
            }
            catch (ParseException ex)
            {
                MessageBox.Show("Could not parse " + ex.number);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                ComplexNumber C1 = ComplexNumberParse(textBox1.Text);
                ComplexNumber C2 = ComplexNumberParse(textBox2.Text);
                double newreal = C1.real - C2.real;
                double newimaginary = C1.imaginary - C2.imaginary;
                if (newimaginary < 0)
                {
                    textBox3.Text = newreal.ToString("#.00000") + newimaginary.ToString("#.00000") + "j";
                }
                else
                {
                    textBox3.Text = newreal.ToString("#.00000") + "+" + newimaginary.ToString("#.00000") + "j";
                }
            }
            catch (ParseException ex)
            {
                MessageBox.Show("Could not parse " + ex.number);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                ComplexNumber C1 = ComplexNumberParse(textBox1.Text);
                ComplexNumber C2 = ComplexNumberParse(textBox2.Text);
                double newreal = (C1.real * C2.real) - (C1.imaginary * C2.imaginary); 
                double newimaginary = (C1.real * C2.imaginary) + (C1.imaginary * C2.real);
                if (newimaginary < 0)
                {
                    textBox3.Text = newreal.ToString("#.00000") + newimaginary.ToString("#.00000") + "j";
                }
                else
                {
                    textBox3.Text = newreal.ToString("#.00000") + "+" + newimaginary.ToString("#.00000") + "j";
                }
            }
            catch (ParseException ex)
            {
                MessageBox.Show("Could not parse " + ex.number);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                ComplexNumber C1 = ComplexNumberParse(textBox1.Text);
                ComplexNumber C2 = ComplexNumberParse(textBox2.Text);
                ComplexNumber C3 = C2;
                C3.imaginary = -C3.imaginary;
                ComplexNumber C4 = new ComplexNumber();
                C4.real = (C1.real * C3.real) - (C1.imaginary * C3.imaginary);
                C4.imaginary = (C1.real * C3.imaginary) + (C1.imaginary * C3.real);
                double denominator = (C2.real * C2.real) + (C2.imaginary * C2.imaginary);
                double newreal = C4.real / denominator;
                double newimaginary = C4.imaginary / denominator;
                if (newimaginary < 0)
                {
                    textBox3.Text = newreal.ToString("#.00000") + newimaginary.ToString("#.00000") + "j";
                }
                else
                {
                    textBox3.Text = newreal.ToString("#.00000") + "+" + newimaginary.ToString("#.00000") + "j";
                }
            }
            catch (ParseException ex)
            {
                MessageBox.Show("Could not parse " + ex.number);
            }
        }
    }
}
