using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;
using System.Data;
using System.ComponentModel;



namespace RGR
{

    class Vertex
    {
        public int x, y,num=1;

        public Vertex(int x, int y,int num)
        {
            this.x = x;
            this.y = y;
            this.num = num;
        }
    }

    class Edge
    {
        public int v1, v2;
        public int weight;
        public Edge(int v1, int v2, int weight)
        {
            
            this.v1 = v1;
            this.v2 = v2;
            if (weight == 0)
            {
                if (this.v1 != this.v2)
                {
                    Random rand = new Random();
                    this.weight = rand.Next(14) + 1;
                }
                else
                {
                    this.weight = 0;
                }
            }
            else
            {
                this.weight = weight;
            }
            
        }
    }

    class DrawGraph
    {
        Bitmap bitmap;
        Pen blackpen;
        Pen redpen;
        Pen edgepen;
        Graphics gr;
        Font font_;
        Brush br;
        PointF point;

        public int R = 20;

        public DrawGraph(int width,int height)
        {
            bitmap = new Bitmap(width, height);
            gr = Graphics.FromImage(bitmap);
            clearSheet();
            blackpen = new Pen(Color.Black);
            blackpen.Width = 2;
            redpen = new Pen(Color.Red);
            redpen.Width = 2;
            edgepen = new Pen(Color.Purple);
            edgepen.Width = 2;
            font_ = new Font("Arial", 15);
            br = Brushes.Black;
        }
        public void clearSheet()
        {
            gr.Clear(Color.White);
        }

        public Bitmap GetBitmap()
        {
            return bitmap;
        }

        public void drawVert(int x,int y,string Num)
        {
            gr.FillEllipse(Brushes.White,(x-R),(y-R),2*R,2*R);
            gr.DrawEllipse(blackpen, (x-R), (y-R), 2 * R, 2 * R);
            point = new PointF(x - (R / 2), y - (R / 2));
            gr.DrawString(Num, font_, br, point);
        }

        public void drawSelectedVert(int x,int y)
        {
            gr.DrawEllipse(redpen, (x - R), (y - R), 2 * R, 2 * R);
        }

        public void drawEdge(Vertex V1,Vertex V2,Edge E,int numE)
        {
            if (E.v1 == E.v2)
            {
                gr.DrawArc(edgepen, (V1.x - 2 * R), (V1.y - 2 * R), 2 * R, 2 * R, 270, 0);
                point = new PointF(V1.x - (int)(2.75 * R), V1.y - (int)(2.75 * R));
                gr.DrawString((0).ToString(), font_, br, point);
                drawVert(V1.x, V1.y, (E.v1 + 1).ToString());
            }
            else
            {
                gr.DrawLine(edgepen, V1.x, V1.y, V2.x, V2.y);
                point = new PointF((V1.x + V2.x) / 2, (V1.y + V2.y) / 2);
                gr.DrawString((E.weight).ToString(), font_, br, point);
                drawVert(V1.x, V1.y, (E.v1 + 1).ToString());
                drawVert(V2.x, V2.y, (E.v2 + 1).ToString());

            }
        }

        public void drawAllGraph(List<Vertex> V, List<Edge> E)
        {
            //отрисовка рёбер
            for (int i = 0; i < E.Count; i++)
            {
                if (E[i].v1 == E[i].v2)
                {
                    //Если вершины начала и конца ребра равны-> петля
                    gr.DrawArc(edgepen, (V[E[i].v1].x - 2 * R), (V[E[i].v1].y - 2 * R), 2 * R, 2 * R, 90, 270);
                    point = new PointF(V[E[i].v1].x - (int)(2.75 * R), V[E[i].v1].y - (int)(2.75 * R));
                    gr.DrawString((0).ToString(), font_, br, point);
                }
                else
                {
                    //соединяем 2 вершины отрезком
                    gr.DrawLine(edgepen, V[E[i].v1].x, V[E[i].v1].y, V[E[i].v2].x, V[E[i].v2].y);
                    point = new PointF((V[E[i].v1].x + V[E[i].v2].x) / 2, (V[E[i].v1].y + V[E[i].v2].y) / 2);
                    gr.DrawString((E[i].weight).ToString(), font_, br, point);
                }
            }
            //отрисовка вершин
            for (int i = 0; i < V.Count; i++)
            {
                V[i].num = i+1;
                drawVert(V[i].x, V[i].y, V[i].num.ToString());
            }
        }



        public void fillIncidenceMatrix(int numberV, List<Edge> E, int[,] matrix)
        {
            for (int i = 0; i < numberV; i++)
                for (int j = 0; j < numberV; j++)
                    matrix[i, j] = 0;
            for (int i = 0; i < E.Count; i++)
            {
                matrix[E[i].v1, E[i].v2] = 1;
                matrix[E[i].v2, E[i].v1] = 1;
            }
        }

        public void fillAdjacencyMatrix(int numberV, List<Edge> E, int[,] matrix)
        {
            for (int i = 0; i < E.Count; i++)
            {
                matrix[E[i].v1, E[i].v2] = 1;
                matrix[E[i].v2, E[i].v1] = 1;
            }
        }


        /*        public void fillAdjacencyList(int numberV, List<Edge> E, int[,] AMatrix, List<Tuple<int,int>> AList)
                {
                    for(int i = 0; i < numberV; i++)
                    {
                        AList.Add(new Tuple<int, int>(E[i].v1, E[i].v2));
                    }
                }*/
    }
}
