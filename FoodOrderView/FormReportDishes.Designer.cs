namespace FoodOrderView
{
    partial class FormReportDishes
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
            this.ReportStorageDishViewModelBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.buttonMake = new System.Windows.Forms.Button();
            this.buttonSaveToPdf = new System.Windows.Forms.Button();
            this.reportViewer = new Microsoft.Reporting.WinForms.ReportViewer();
            ((System.ComponentModel.ISupportInitialize)(this.ReportStorageDishViewModelBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // ReportStorageDishViewModelBindingSource
            // 
            this.ReportStorageDishViewModelBindingSource.DataSource = typeof(FoodOrderBusinessLogic.ViewModels.ReportStorageDishViewModel);
            // 
            // buttonMake
            // 
            this.buttonMake.Location = new System.Drawing.Point(28, 12);
            this.buttonMake.Name = "buttonMake";
            this.buttonMake.Size = new System.Drawing.Size(166, 30);
            this.buttonMake.TabIndex = 1;
            this.buttonMake.Text = "Сформировать";
            this.buttonMake.UseVisualStyleBackColor = true;
            this.buttonMake.Click += new System.EventHandler(this.buttonMake_Click);
            // 
            // buttonSaveToPdf
            // 
            this.buttonSaveToPdf.Location = new System.Drawing.Point(217, 12);
            this.buttonSaveToPdf.Name = "buttonSaveToPdf";
            this.buttonSaveToPdf.Size = new System.Drawing.Size(166, 30);
            this.buttonSaveToPdf.TabIndex = 2;
            this.buttonSaveToPdf.Text = "в Pdf";
            this.buttonSaveToPdf.UseVisualStyleBackColor = true;
            this.buttonSaveToPdf.Click += new System.EventHandler(this.buttonSaveToPdf_Click);
            // 
            // reportViewer
            // 
            this.reportViewer.LocalReport.ReportEmbeddedResource = "FoodOrderView.ReportStorageDishes.rdlc";
            this.reportViewer.Location = new System.Drawing.Point(12, 60);
            this.reportViewer.Name = "reportViewer";
            this.reportViewer.ServerReport.BearerToken = null;
            this.reportViewer.Size = new System.Drawing.Size(963, 480);
            this.reportViewer.TabIndex = 3;
            // 
            // FormReportDishes
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(987, 552);
            this.Controls.Add(this.reportViewer);
            this.Controls.Add(this.buttonSaveToPdf);
            this.Controls.Add(this.buttonMake);
            this.Name = "FormReportDishes";
            this.Text = "Отчет по блюдам со складов";
            ((System.ComponentModel.ISupportInitialize)(this.ReportStorageDishViewModelBindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button buttonMake;
        private System.Windows.Forms.Button buttonSaveToPdf;
        private Microsoft.Reporting.WinForms.ReportViewer reportViewer;
        private System.Windows.Forms.BindingSource ReportStorageDishViewModelBindingSource;
    }
}