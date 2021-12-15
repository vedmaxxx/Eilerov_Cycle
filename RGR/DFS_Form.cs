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
    public partial class DFS_Form : Form
    {
        public DFS_Form()
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

        /*private void BFS(object sender, EventArgs e)
        {
            string res = " ";
            if (V.Count == 0)
            {
                res = "а граф-то пустой";
                resultBox.Text = res;
                return;
            }
            AMatrix = new int[V.Count, V.Count];
            G.fillIncidenceMatrix(V.Count, E, AMatrix);
            Queue<Vertex> q = new Queue<Vertex>();
            
            char c = startVert.Text[0];
            int u = c - '1';                //индексация вершин
            Vertex vert = V[u];         //
            bool[] used = new bool[V.Count];
            // res = vert.num.ToString();

            used[u] = true;             //пометили стартовую вершину как помеченную - пометили стартову вершину.
            q.Enqueue(vert);            //занесли вершину в очередь

            while (q.Count != 0)
            {
                vert = q.Peek();        
                q.Dequeue();            //выкинули из очереди

                for(int i = 0; i < V.Count; i++)
                {
                    if (Convert.ToBoolean(AMatrix[vert.num-1, i]))      //vert.num-1 потому что в num индексация с единицы, а здесь - с нуля
                    {
                        if (!used[i])
                        {
                            used[i] = true;
                            q.Enqueue(V[i]); 
                            res = res + " " + V[i].num.ToString();
                        }
                    }
                }

            }
            resultBox.Text = res;
        }*/




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
            G.fillAdjacencyMatrix(V.Count, E, matrix);

            int[,] a = new int[V.Count, V.Count];
            G.fillAdjacencyMatrix(V.Count, E, a);

            int start = 0;
            

            Stack<int> c = new Stack<int>();
            int degree = 0;
            bool[] marks = new bool[V.Count];

            DFS(marks);
            for (int i = 0; i < V.Count; i++)
            {
                if (!marks[i])
                {
                    res = "граф несвязный"; resultBox.Text = res; return;
                }
            }

            int[] Step = new int[V.Count];          //вершина-степень
            for (int i = 0; i < V.Count; i++)
            {
                for (int j = 0; j < V.Count; j++)
                {
                    if (matrix[i, j] == 1)
                    {
                        Step[i] += 1;
                    }
                }
                if (Step[i] % 2 == 1) degree++;
            }

            if (degree > 2 || degree == 1)
            {
                res = "Нет ни цикла, ни цепи";
                resultBox.Text = res;
                return;
            }
            if (degree == 0)
            {
                res = res + "Цикл:";
            }
            else
            {
                if (degree == 2)
                {
                    start = -1;
                    res = res + "Цепь:";
                    for (int i = 0; i < V.Count; i++)
                    {
                        if (Step[i] % 2 == 1)
                        {
                            if (start == -1) { start = i; }

                        }
                    }
                }
            }

            SearchG(start, ref a, ref c);
            //while (c.Count != 0)

            void SearchG(int v, ref int[,] a_, ref Stack<int> c_) //во вложенном классе
            {
                for (int i = 0; i < V.Count; i++)
                {
                    if (a_[v, i] != 0)
                    {
                        a_[v, i] = 0; a_[i, v] = 0;
                        SearchG(i, ref a_, ref c_);
                    }
                }
                c_.Push(v);
            }

            while (c.Count != 0)
            {
                res = res + ((int)c.Pop() + 1).ToString();
            }
            resultBox.Text = res;
        }


        private void DFS(bool[] marks)
        {
            //string res = " ";
            /*if (V.Count == 0)
            {
                res = "а граф-то пустой";
                resultBox.Text = res;
                return;
            }*/
            AMatrix = new int[V.Count, V.Count];
            G.fillAdjacencyMatrix(V.Count, E, AMatrix);

            /*int start = Int32.Parse(startVert.Text); //номер стартовой вершины, заданной в поле startVert
            if (start > V.Count || start <= 0)
            {
                MessageBox.Show("Введите корректное значение начальной вершины");
                startVert.Text = "1";
                return;
            }
            start--;    //индексация с нуля*/
            int start = 0;

            Stack<int> stack = new Stack<int>();
            //bool[] marks = new bool[V.Count];
            bool[] stack_marks = new bool[V.Count];
            for (int i = 0; i < V.Count; i++)
            {
                marks[i] = false;
                stack_marks[i] = false;
            }
            //res = startVert.Text;
            stack.Push(start);
            int prom;

            while (stack.Count != 0)
            {

                prom = stack.Pop();

                if (marks[prom] != true)    //если вершина не помечена
                {
                    for (int i = V.Count - 1; i >= 0; i--)
                        if (AMatrix[prom, i] != 0 && AMatrix[prom, i] != INFINITY && marks[i] == false)
                        {
                            if (stack_marks[i] == false)
                            {
                                stack.Push(i);              //обрабатываем смежные вершины и кидаем их в стек
                                stack_marks[i] = true;
                            }

                        }
                    marks[prom] = true;
                    //помечаем вершину как обработанную
                }
                //res = res + (prom + 1).ToString() + " ";

            }

            //resultBox.Text = res;
        }
    }
    //это dfs
}
