namespace FoodOrderView
{
    partial class FormReportStorageDishes
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.dataGridView = new System.Windows.Forms.DataGridView();
            this.Storage = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Dish = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Count = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.buttonMake = new System.Windows.Forms.Button();
            this.buttonSaveToExcel = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView
            // 
            this.dataGridView.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight;
            this.dataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Storage,
            this.Dish,
            this.Count});
            this.dataGridView.GridColor = System.Drawing.SystemColors.ButtonHighlight;
            this.dataGridView.Location = new System.Drawing.Point(14, 68);
            this.dataGridView.Name = "dataGridView";
            this.dataGridView.RowHeadersVisible = false;
            this.dataGridView.RowHeadersWidth = 51;
            this.dataGridView.RowTemplate.Height = 24;
            this.dataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView.Size = new System.Drawing.Size(765, 370);
            this.dataGridView.TabIndex = 0;
            // 
            // Storage
            // 
            this.Storage.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Storage.HeaderText = "Склад";
            this.Storage.MinimumWidth = 6;
            this.Storage.Name = "Storage";
            // 
            // Dish
            // 
            this.Dish.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Dish.HeaderText = "Блюдо";
            this.Dish.MinimumWidth = 6;
            this.Dish.Name = "Dish";
            // 
            // Count
            // 
            this.Count.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Count.HeaderText = "Количество";
            this.Count.MinimumWidth = 6;
            this.Count.Name = "Count";
            // 
            // buttonMake
            // 
            this.buttonMake.Location = new System.Drawing.Point(31, 20);
            this.buttonMake.Name = "buttonMake";
            this.buttonMake.Size = new System.Drawing.Size(186, 30);
            this.buttonMake.TabIndex = 1;
            this.buttonMake.Text = "Сформировать";
            this.buttonMake.UseVisualStyleBackColor = true;
            this.buttonMake.Click += new System.EventHandler(this.buttonMake_Click);
            // 
            // buttonSaveToExcel
            // 
            this.buttonSaveToExcel.Location = new System.Drawing.Point(255, 20);
            this.buttonSaveToExcel.Name = "buttonSaveToExcel";
            this.buttonSaveToExcel.Size = new System.Drawing.Size(186, 30);
            this.buttonSaveToExcel.TabIndex = 2;
            this.buttonSaveToExcel.Text = "Сохранить в Excel";
            this.buttonSaveToExcel.UseVisualStyleBackColor = true;
            this.buttonSaveToExcel.Click += new System.EventHandler(this.buttonSaveToExcel_Click);
            // 
            // FormReportStorageDishes
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.buttonSaveToExcel);
            this.Controls.Add(this.buttonMake);
            this.Controls.Add(this.dataGridView);
            this.Name = "FormReportStorageDishes";
            this.Text = "Отчет по блюдам на складе";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView;
        private System.Windows.Forms.DataGridViewTextBoxColumn Storage;
        private System.Windows.Forms.DataGridViewTextBoxColumn Dish;
        private System.Windows.Forms.DataGridViewTextBoxColumn Count;
        private System.Windows.Forms.Button buttonMake;
        private System.Windows.Forms.Button buttonSaveToExcel;
    }
}