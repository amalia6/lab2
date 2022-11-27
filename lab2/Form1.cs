using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace lab2
{
    public partial class Form1 : Form
    {
        int[] x = new int[10000];
        int[] similaritate = new int[10000];
        int[] y = new int[10000];
        int[] xk = new int[10000];
        int[] yk = new int[10000];
        int[] ct = new int[13];
        int[] sumX = new int[10000];
        int[] sumY = new int[10000];



        Pen myPen = new Pen(Color.Black);

        Graphics graph;
        Random rand = new Random();
        SolidBrush[] culoare = new SolidBrush[] { new SolidBrush(Color.Red), new SolidBrush(Color.Yellow),
            new SolidBrush(Color.Orange), new SolidBrush(Color.Purple), new SolidBrush(Color.Green), new SolidBrush(Color.Blue),
            new SolidBrush(Color.Turquoise), new SolidBrush(Color.Chocolate), new SolidBrush(Color.HotPink), new SolidBrush(Color.MintCream),
            new SolidBrush(Color.Black)};

        List<Pen> Culori = new List<Pen>()
        {
            new Pen(Color.Red),
            new Pen(Color.Yellow),
            new Pen(Color.Orange),
            new Pen(Color.Purple),
            new Pen(Color.Green),
            new Pen(Color.Blue),
            new Pen(Color.Turquoise),
            new Pen(Color.Chocolate),
            new Pen(Color.HotPink),
            new Pen(Color.MintCream),
        };
        public Form1()
        {
            InitializeComponent();
        }
        int ManhattanDistance(int x1, int x2, int y1, int y2)
        {
            return Math.Abs(x1 - x2) + Math.Abs(y1 - y2);
        }
        void Citire()
        {
            StreamReader sr = new StreamReader("punctuletze.txt");

            string[] line = new string[3];

            for (int i = 0; i < 10000; i++)
            {
                line = sr.ReadLine().Split(' ');
                x[i] = Convert.ToInt32(line[0]);
                y[i] = Convert.ToInt32(line[1]);

                graph.DrawRectangle(myPen, x[i] + 300, 300 - y[i], 1, 1);
            }
        }
        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            graph = panel1.CreateGraphics();
            Pen line = new Pen(Color.Black);

            graph.DrawLine(line, new Point(0, 300), new Point(600, 300));
            graph.DrawLine(line, new Point(300, 0), new Point(300, 600));

            int k = rand.Next(2, 11);
            for (int i = 0; i < k; i++)
            {
                xk[i] = rand.Next(-300, 300);
                yk[i] = rand.Next(-300, 300);
                graph.FillEllipse(culoare[i], xk[i] + 300, 300 - yk[i], 8, 8);
            }

            Citire();

             
            for (int i = 0; i < 10000; i++)
            {
                double min = Double.MaxValue;

                
                for (int j = 0; j < k; j++)
                {
                    int md = ManhattanDistance(x[i], xk[j], y[i], yk[j]);
                    //compar prima coord cu toti centroizii si care are distanta cea mai mica pe ala il iau!!!
                    if (min > md)
                    {
                        min = md;
                        similaritate[i] = j;
                        sumX[j] = sumX[j] + x[i];
                        sumY[j] = sumY[j] + y[i];
                        ct[j]++;
                        //graph.FillEllipse(culoare[j], x[i] + 300, 300 - y[i], 1, 1);
                    }
                }
            }

            for (int i = 0; i < 10000; i++)
            {
                Pen point = new Pen(Culori[similaritate[i]].Color);
                graph.DrawRectangle(point, x[i] + 300, 300 - y[i], 1, 1);
            }

            //calc centru de greutate
            for (int i=0; i < k; i++)
            {
                if(ct[i] != 0)
                {
                    float noulCentroidX = sumX[i] / ct[i];
                    float noulCentroidY = sumY[i] / ct[i];

                    graph.FillEllipse(culoare[i], noulCentroidX + 300, 300 - noulCentroidY, 8, 8);

                }
            }
        }
        }
    }

