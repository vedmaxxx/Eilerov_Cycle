namespace RGR
{
    partial class DFS_Form
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.sheet = new System.Windows.Forms.PictureBox();
            this.Btn_drawVert = new System.Windows.Forms.Button();
            this.Btn_drawEdge = new System.Windows.Forms.Button();
            this.Btn_delete = new System.Windows.Forms.Button();
            this.btn_Select = new System.Windows.Forms.Button();
            this.listBox_Matrix = new System.Windows.Forms.ListBox();
            this.resultBox = new System.Windows.Forms.TextBox();
            this.btn_DFS = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.listBox_Matrix_lbl = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.sheet)).BeginInit();
            this.SuspendLayout();
            // 
            // sheet
            // 
            this.sheet.Location = new System.Drawing.Point(9, 12);
            this.sheet.Name = "sheet";
            this.sheet.Size = new System.Drawing.Size(403, 267);
            this.sheet.TabIndex = 0;
            this.sheet.TabStop = false;
            this.sheet.MouseClick += new System.Windows.Forms.MouseEventHandler(this.sheet_MouseClick);
            // 
            // Btn_drawVert
            // 
            this.Btn_drawVert.Location = new System.Drawing.Point(100, 288);
            this.Btn_drawVert.Name = "Btn_drawVert";
            this.Btn_drawVert.Size = new System.Drawing.Size(100, 39);
            this.Btn_drawVert.TabIndex = 1;
            this.Btn_drawVert.Text = "Нарисовать вершины";
            this.Btn_drawVert.UseVisualStyleBackColor = true;
            this.Btn_drawVert.Click += new System.EventHandler(this.Btn_drawVert_Click);
            // 
            // Btn_drawEdge
            // 
            this.Btn_drawEdge.Location = new System.Drawing.Point(206, 288);
            this.Btn_drawEdge.Name = "Btn_drawEdge";
            this.Btn_drawEdge.Size = new System.Drawing.Size(100, 39);
            this.Btn_drawEdge.TabIndex = 2;
            this.Btn_drawEdge.Text = "Нарисовать ребра";
            this.Btn_drawEdge.UseVisualStyleBackColor = true;
            this.Btn_drawEdge.Click += new System.EventHandler(this.Btn_drawEdge_Click);
            // 
            // Btn_delete
            // 
            this.Btn_delete.Location = new System.Drawing.Point(312, 288);
            this.Btn_delete.Name = "Btn_delete";
            this.Btn_delete.Size = new System.Drawing.Size(100, 39);
            this.Btn_delete.TabIndex = 3;
            this.Btn_delete.Text = "Удалить объект";
            this.Btn_delete.UseVisualStyleBackColor = true;
            this.Btn_delete.Click += new System.EventHandler(this.Btn_delete_Click);
            // 
            // btn_Select
            // 
            this.btn_Select.Location = new System.Drawing.Point(12, 288);
            this.btn_Select.Name = "btn_Select";
            this.btn_Select.Size = new System.Drawing.Size(82, 39);
            this.btn_Select.TabIndex = 4;
            this.btn_Select.Text = "Отмена";
            this.btn_Select.UseVisualStyleBackColor = true;
            this.btn_Select.Click += new System.EventHandler(this.btn_Select_Click);
            // 
            // listBox_Matrix
            // 
            this.listBox_Matrix.FormattingEnabled = true;
            this.listBox_Matrix.HorizontalScrollbar = true;
            this.listBox_Matrix.Location = new System.Drawing.Point(445, 25);
            this.listBox_Matrix.Name = "listBox_Matrix";
            this.listBox_Matrix.Size = new System.Drawing.Size(271, 173);
            this.listBox_Matrix.TabIndex = 5;
            // 
            // resultBox
            // 
            this.resultBox.Location = new System.Drawing.Point(440, 415);
            this.resultBox.Name = "resultBox";
            this.resultBox.Size = new System.Drawing.Size(248, 20);
            this.resultBox.TabIndex = 6;
            // 
            // btn_DFS
            // 
            this.btn_DFS.Location = new System.Drawing.Point(312, 360);
            this.btn_DFS.Name = "btn_DFS";
            this.btn_DFS.Size = new System.Drawing.Size(100, 39);
            this.btn_DFS.TabIndex = 10;
            this.btn_DFS.Text = "Найти Эйлеров цикл";
            this.btn_DFS.UseVisualStyleBackColor = true;
            this.btn_DFS.Click += new System.EventHandler(this.Eiler);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(437, 399);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(62, 13);
            this.label2.TabIndex = 11;
            this.label2.Text = "Результат:";
            // 
            // listBox_Matrix_lbl
            // 
            this.listBox_Matrix_lbl.AutoSize = true;
            this.listBox_Matrix_lbl.Location = new System.Drawing.Point(442, 9);
            this.listBox_Matrix_lbl.Name = "listBox_Matrix_lbl";
            this.listBox_Matrix_lbl.Size = new System.Drawing.Size(111, 13);
            this.listBox_Matrix_lbl.TabIndex = 21;
            this.listBox_Matrix_lbl.Text = "Матрица смежности";
            // 
            // DFS_Form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(730, 450);
            this.Controls.Add(this.listBox_Matrix_lbl);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btn_DFS);
            this.Controls.Add(this.resultBox);
            this.Controls.Add(this.listBox_Matrix);
            this.Controls.Add(this.btn_Select);
            this.Controls.Add(this.Btn_delete);
            this.Controls.Add(this.Btn_drawEdge);
            this.Controls.Add(this.Btn_drawVert);
            this.Controls.Add(this.sheet);
            this.Name = "DFS_Form";
            this.Text = "DFS";
            ((System.ComponentModel.ISupportInitialize)(this.sheet)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox sheet;
        private System.Windows.Forms.Button Btn_drawVert;
        private System.Windows.Forms.Button Btn_drawEdge;
        private System.Windows.Forms.Button Btn_delete;
        private System.Windows.Forms.Button btn_Select;
        private System.Windows.Forms.ListBox listBox_Matrix;
        private System.Windows.Forms.TextBox resultBox;
        private System.Windows.Forms.Button btn_DFS;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label listBox_Matrix_lbl;
    }
}

