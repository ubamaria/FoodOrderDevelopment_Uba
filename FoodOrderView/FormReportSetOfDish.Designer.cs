namespace FoodOrderView
{
    partial class FormReportSetOfDish
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
            this.components = new System.ComponentModel.Container();
            this.ReportSetOfDishViewModelBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.buttonSaveToPdf = new System.Windows.Forms.Button();
            this.buttonMake = new System.Windows.Forms.Button();
            this.reportViewer = new Microsoft.Reporting.WinForms.ReportViewer();
            ((System.ComponentModel.ISupportInitialize)(this.ReportSetOfDishViewModelBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // ReportSetOfDishViewModelBindingSource
            // 
            this.ReportSetOfDishViewModelBindingSource.DataSource = typeof(FoodOrderBusinessLogic.ViewModels.ReportSetOfDishViewModel);
            // 
            // buttonSaveToPdf
            // 
            this.buttonSaveToPdf.Location = new System.Drawing.Point(230, 12);
            this.buttonSaveToPdf.Name = "buttonSaveToPdf";
            this.buttonSaveToPdf.Size = new System.Drawing.Size(225, 28);
            this.buttonSaveToPdf.TabIndex = 1;
            this.buttonSaveToPdf.Text = "в Pdf";
            this.buttonSaveToPdf.UseVisualStyleBackColor = true;
            this.buttonSaveToPdf.Click += new System.EventHandler(this.buttonSaveToPdf_Click);
            // 
            // buttonMake
            // 
            this.buttonMake.Location = new System.Drawing.Point(15, 13);
            this.buttonMake.Name = "buttonMake";
            this.buttonMake.Size = new System.Drawing.Size(192, 27);
            this.buttonMake.TabIndex = 2;
            this.buttonMake.Text = "Сформировать";
            this.buttonMake.UseVisualStyleBackColor = true;
            this.buttonMake.Click += new System.EventHandler(this.buttonMake_Click);
            // 
            // reportViewer
            // 
            this.reportViewer.LocalReport.ReportEmbeddedResource = "FoodOrderView.ReportSetOfDish.rdlc";
            this.reportViewer.Location = new System.Drawing.Point(0, 51);
            this.reportViewer.Name = "reportViewer";
            this.reportViewer.ServerReport.BearerToken = null;
            this.reportViewer.Size = new System.Drawing.Size(861, 522);
            this.reportViewer.TabIndex = 3;
            // 
            // FormReportSetOfDish
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(859, 571);
            this.Controls.Add(this.reportViewer);
            this.Controls.Add(this.buttonMake);
            this.Controls.Add(this.buttonSaveToPdf);
            this.Name = "FormReportSetOfDish";
            this.Text = "Отчет по блюдам и наборам";
            ((System.ComponentModel.ISupportInitialize)(this.ReportSetOfDishViewModelBindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button buttonSaveToPdf;
        private System.Windows.Forms.Button buttonMake;
        private Microsoft.Reporting.WinForms.ReportViewer reportViewer;
        private System.Windows.Forms.BindingSource ReportSetOfDishViewModelBindingSource;
    }
}