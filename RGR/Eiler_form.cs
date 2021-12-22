using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace RGR
{
    public partial class Eiler_form : Form
    {
        public Eiler_form()
        {
            InitializeComponent();
            V = new List<Vertex>();
            G = new DrawGraph(sheet.Width, sheet.Height);
            E = new List<Edge>();
            sheet.Image = G.GetBitmap();
        }
        DrawGraph G;
        List<Vertex> V;
        List<Edge> E;
        int[,] AMatrix;     //матрица смежности
        List<int> resultList;
        const int INFINITY = 999;

        int selected1;      //выбранные вершины, для соединения линиями
        int selected2;
        private void Btn_drawVert_Click(object sender, EventArgs e)
        {
            Btn_drawVert.Enabled = false;
            btn_Select.Enabled = true;
            Btn_drawEdge.Enabled = true;
            Btn_delete.Enabled = true;
            G.clearSheet();
            G.drawAllGraph(V, E);
            sheet.Image = G.GetBitmap();
        }

        private void btn_Select_Click(object sender, EventArgs e)
        {
            btn_Select.Enabled = false;
            Btn_drawVert.Enabled = true;
            Btn_drawEdge.Enabled = true;
            Btn_delete.Enabled = true;
            G.clearSheet();
            G.drawAllGraph(V, E);
            sheet.Image = G.GetBitmap();
            selected1 = -1;
        }
        
        private void Btn_drawEdge_Click(object sender, EventArgs e)
        {
            Btn_drawEdge.Enabled = false;
            btn_Select.Enabled = true;
            Btn_drawVert.Enabled = true;
            Btn_delete.Enabled = true;
            G.clearSheet();
            G.drawAllGraph(V, E);
            sheet.Image = G.GetBitmap();
            selected1 = -1;
            selected2 = -1;
        }


        private void Btn_delete_Click(object sender, EventArgs e)
        {
            Btn_drawEdge.Enabled = true;
            btn_Select.Enabled = true;
            Btn_drawVert.Enabled = true;
            Btn_delete.Enabled = false;
            G.clearSheet();
            G.drawAllGraph(V, E);
            sheet.Image = G.GetBitmap();
        }


        private void sheet_MouseClick(object sender, MouseEventArgs e)
        {
            //режим выделения
            if (btn_Select.Enabled == false) {
                for (int i = 0; i < V.Count(); i++)
                {
                    if (selected1 != -1)
                    {
                        selected1 = -1;
                        G.clearSheet();
                        G.drawAllGraph(V, E);
                        sheet.Image = G.GetBitmap();
                    }
                    if (selected1 == -1)
                    {
                        G.drawVert(V[i].x, V[i].y, V[i].num.ToString());
                        selected1 = i;
                        sheet.Image = G.GetBitmap();
                        createMatA_output();

                    }
                }
            }
            //режим рисования вершин
            if (Btn_drawVert.Enabled == false)
            {
                Vertex vert = new Vertex(e.X, e.Y, V.Count + 1);
                V.Add(vert);
                G.drawVert(e.X, e.Y,vert.num.ToString());
                sheet.Image = G.GetBitmap();
                //
                createMatA_output();

            }
            //режим рисования ребра
            if (Btn_drawEdge.Enabled == false)
            {
                if (e.Button == MouseButtons.Left)
                {
                    for(int i = 0; i < V.Count(); i++)
                    {
                        if (Math.Pow((V[i].x - e.X), 2) + Math.Pow((V[i].y - e.Y), 2)<=G.R*G.R)
                        {
                            if (selected1 == -1)
                            {
                                G.drawSelectedVert(V[i].x, V[i].y);
                                selected1 = i;
                                sheet.Image = G.GetBitmap();
                                //
                                createMatA_output();
        

                                break;
                            }
                            if (selected2 == -1)
                            {
                                G.drawSelectedVert(V[i].x, V[i].y);
                                selected2 = i;
                                
                                E.Add(new Edge(selected1, selected2,1));

                                G.drawEdge(V[selected1], V[selected2], E[E.Count - 1], E.Count - 1);
                                selected1 = -1;
                                selected2 = -1;
                                sheet.Image = G.GetBitmap();
                                createMatA_output();
       
                                
                                break;
                            }
                        }
                    }
                }
                if (e.Button == MouseButtons.Right)
                {
                    if((selected1!=-1)&& (Math.Pow((V[selected1].x - e.X), 2) + Math.Pow((V[selected1].y - e.Y), 2) <= G.R * G.R))
                    {
                        G.drawVert(V[selected1].x, V[selected1].y, (selected1 + 1).ToString());
                        selected1 = -1;
                        sheet.Image = G.GetBitmap();
                        //
                        //
                        createMatA_output();
 

                    }
                }
            }
            if (Btn_delete.Enabled == false)
            {
                bool flag = false;
                for(int i = 0; i < V.Count; i++)
                {
                    if (Math.Pow((V[i].x - e.X), 2) + Math.Pow((V[i].y - e.Y), 2) <= G.R * G.R)
                    {
                        for (int j = 0; j < E.Count; j++)
                        {
                            if ((E[j].v1==i)|| (E[j].v2 == i))
                            {
                                E.RemoveAt(j);
                                j--;
                            }
                            else
                            {
                                if (E[j].v1 > i) E[j].v1--;
                                if (E[j].v2 > i) E[j].v2--;
                            }
                        }
                        V.RemoveAt(i);
                        flag = true;
                        createMatA_output();

                        break;
                    }
                }
                if (!flag)
                {
                    for (int i = 0; i < E.Count; i++)
                    {
                        if (E[i].v1 == E[i].v2) //если это петля
                        {
                            if ((Math.Pow((V[E[i].v1].x - G.R - e.X), 2) + Math.Pow((V[E[i].v1].y - G.R - e.Y), 2) <= ((G.R + 2) * (G.R + 2))) &&
                                (Math.Pow((V[E[i].v1].x - G.R - e.X), 2) + Math.Pow((V[E[i].v1].y - G.R - e.Y), 2) >= ((G.R - 2) * (G.R - 2))))
                            {
                                E.RemoveAt(i);
                                flag = true;
                                createMatA_output();
                                break;
                            }
                        }
                        else //не петля
                        {
                            if (((e.X - V[E[i].v1].x) * (V[E[i].v2].y - V[E[i].v1].y) / (V[E[i].v2].x - V[E[i].v1].x) + V[E[i].v1].y) <= (e.Y + 4) &&
                                ((e.X - V[E[i].v1].x) * (V[E[i].v2].y - V[E[i].v1].y) / (V[E[i].v2].x - V[E[i].v1].x) + V[E[i].v1].y) >= (e.Y - 4))
                            {
                                if ((V[E[i].v1].x <= V[E[i].v2].x && V[E[i].v1].x <= e.X && e.X <= V[E[i].v2].x) ||
                                    (V[E[i].v1].x >= V[E[i].v2].x && V[E[i].v1].x >= e.X && e.X >= V[E[i].v2].x))
                                {
                                    E.RemoveAt(i);
                                    flag = true;
                                    //
                                    createMatA_output();
              
                                    break;
                                }
                            }
                        }
                    }
                }
                if (flag)
                {
                    G.clearSheet();
                    G.drawAllGraph(V, E);
                    sheet.Image = G.GetBitmap();
                }
            }
        }
        //возвращает первую смежную с num_v вершину
        int getAdjacency(int numberV, int num_v, int[,] matrix)
        {
            for (int i = 0; i < numberV; i++)
                if (matrix[num_v, i] != 0)
                    return i;
            return -1;
        }
        //возвращает степень вершины num_v
        int getDegree(int numberV, int num_v, int[,] matrix)
        {
            int degree = 0;
            for(int i = 0; i < numberV; i++)
                if (matrix[num_v, i] != 0) 
                    degree++;

            return degree;
        }
        //вывод матрицы смежности
        private void createMatA_output()
        {
            AMatrix = new int[V.Count, V.Count];
            G.fillAdjacencyMatrix(V.Count, E, AMatrix);
            listBox_Matrix.Items.Clear();
            string sOut = "   ";
            for (int i = 0; i < V.Count; i++)
                sOut += "\t" + (i + 1) + "      ";
            listBox_Matrix.Items.Add(sOut);
            sOut = "   ";

            for (int i = 0; i < V.Count; i++)
                sOut += "________";
            listBox_Matrix.Items.Add(sOut);

            for (int i = 0; i < V.Count; i++)
                {
                sOut = (i + 1) + " | \t";
                for (int j = 0; j < V.Count; j++)
                {
                    if (i == j)
                    {
                        sOut += " 0 \t";
                        continue;
                    }
                    if (AMatrix[i, j] == INFINITY)
                        sOut += " inf\t";
                    else
                        if (AMatrix[i, j] < 10)
                        {
                            sOut += " " + AMatrix[i, j] + "  \t";
                        }
                        else if (AMatrix[i, j] < 100)
                        {
                            sOut += " " + AMatrix[i, j] + " \t";
                        }
                        else 
                            sOut += " " + AMatrix[i, j] + "\t";

                }
                listBox_Matrix.Items.Add(sOut);
            }
        }

        //возвращает первую смежную с num_v вершину
  

        void Eiler(object sender, EventArgs e)
        {
            string res = "";
            if (V.Count == 0)
            {
                res = "а граф-то пустой";
                resultBox.Text = res;
                return;
            }

            int[,] matrix = new int[V.Count, V.Count];
            int[,] a = new int[V.Count, V.Count];
            int nechet_degree = 0;
            int start = 0;
            Stack<int> res_stack = new Stack<int>();
            Stack<int> stack = new Stack<int>();
            

            G.fillAdjacencyMatrix(V.Count, E, matrix);
            G.fillAdjacencyMatrix(V.Count, E, a);

            resultList = new List<int>();

            //поиск в глубину
            DFS(resultList, start);
            if (resultList.Count != V.Count) 
            { 
                res = "Граф несвязный - он не содержит Эйлерова цикла";
                resultBox.Text = res;
                return;
            }

            //перебор всех вершин
            for (int i = 0; i < V.Count; i++)
            {
                int tmp = getDegree(V.Count, i, matrix);
                if (tmp % 2 == 1) 
                    nechet_degree++;

                if (nechet_degree != 0)
                {
                    res = "Нет Эйлерова цикла: не все вершины имеют четную степень";
                    resultBox.Text = res;
                    return;
                }
            }

            //сам алгоритм поиска Эйлерова цикла
            stack.Push(start);
            while(stack.Count != 0)
            {
                int node = stack.Peek();
                if (getDegree(V.Count, node, matrix) != 0)
                {
                    int u = getAdjacency(V.Count, node, matrix);
                    stack.Push(u);
                    matrix[node, u] = 0;
                    matrix[u, node] = 0;
                }
                else res_stack.Push(stack.Pop());
            }

            while (res_stack.Count != 0)
                res += (res_stack.Pop() + 1).ToString() + " ";

            resultBox.Text = res;
        }


        private bool isEdgeExist(int[,] matrix, int v1, int v2)
        {
            if (matrix[v1, v2] != INFINITY && matrix[v1, v2] != 0)
                return true;
            else
                return false;
        }

        private void DFS(List<int> list, int _start)
        {
            AMatrix = new int[V.Count, V.Count];
            //заполняем матрицу смежности в соответствии с графом
            G.fillAdjacencyMatrix(V.Count, E, AMatrix);

            //в start заносим номер стартовой вершины, заданной в поле startVert
            
            //пользователь вводит числа в диапазоне [1, бесконечность)
            //но в программе индексация происходит с нуля

            //индексация с нуля

            //стек смежных вершин
            Stack<int> stack = new Stack<int>();
            //список обработанных вершин (вывод)


            //заносим в стек стартовую вершину
            stack.Push(_start);
            //stack.Push();
            int prom;
            //пока стек не пуст
            while (stack.Count != 0)
            {
                //извлекаем вершину из стека и обрабатываем её
                prom = stack.Pop();
                //если вершина не помечена
                if (!resultList.Contains(prom))
                {
                    //ищем смежные вершины
                    for (int i = V.Count - 1; i >= 0; i--)
                        //если смежная
                        if (isEdgeExist(AMatrix, prom, i))
                        {
                            //если смежная вершина не была обработана, заносим её в стек
                            if (!resultList.Contains(i))
                            {
                                stack.Push(i);
                            }
                        }
                    list.Add(prom);
                }
            }
        }
    }
}
