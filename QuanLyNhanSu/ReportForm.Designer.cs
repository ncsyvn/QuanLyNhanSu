namespace QuanLyNhanSu
{
    partial class ReportForm
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
            Microsoft.Reporting.WinForms.ReportDataSource reportDataSource1 = new Microsoft.Reporting.WinForms.ReportDataSource();
            this.n05_Ql_NhanSu_T5DataSet_NhanVien_NoiHoc = new QuanLyNhanSu.N05_Ql_NhanSu_T5DataSet_NhanVien_NoiHoc();
            this.getNhanVienNoiHocBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.getNhanVien_NoiHocTableAdapter = new QuanLyNhanSu.N05_Ql_NhanSu_T5DataSet_NhanVien_NoiHocTableAdapters.GetNhanVien_NoiHocTableAdapter();
            this.reportViewer2 = new Microsoft.Reporting.WinForms.ReportViewer();
            this.GetNhanVien_NoiHocBindingSource = new System.Windows.Forms.BindingSource(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.n05_Ql_NhanSu_T5DataSet_NhanVien_NoiHoc)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.getNhanVienNoiHocBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.GetNhanVien_NoiHocBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // n05_Ql_NhanSu_T5DataSet_NhanVien_NoiHoc
            // 
            this.n05_Ql_NhanSu_T5DataSet_NhanVien_NoiHoc.DataSetName = "N05_Ql_NhanSu_T5DataSet_NhanVien_NoiHoc";
            this.n05_Ql_NhanSu_T5DataSet_NhanVien_NoiHoc.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // getNhanVienNoiHocBindingSource
            // 
            this.getNhanVienNoiHocBindingSource.DataMember = "GetNhanVien_NoiHoc";
            this.getNhanVienNoiHocBindingSource.DataSource = this.n05_Ql_NhanSu_T5DataSet_NhanVien_NoiHoc;
            // 
            // getNhanVien_NoiHocTableAdapter
            // 
            this.getNhanVien_NoiHocTableAdapter.ClearBeforeFill = true;
            // 
            // reportViewer2
            // 
            reportDataSource1.Name = "DataSet_Report_NhanVien_NoiHoc";
            reportDataSource1.Value = this.GetNhanVien_NoiHocBindingSource;
            this.reportViewer2.LocalReport.DataSources.Add(reportDataSource1);
            this.reportViewer2.LocalReport.ReportEmbeddedResource = "QuanLyNhanSu.Report.rdlc";
            this.reportViewer2.Location = new System.Drawing.Point(12, 24);
            this.reportViewer2.Name = "reportViewer2";
            this.reportViewer2.ServerReport.BearerToken = null;
            this.reportViewer2.Size = new System.Drawing.Size(752, 692);
            this.reportViewer2.TabIndex = 1;
            // 
            // GetNhanVien_NoiHocBindingSource
            // 
            this.GetNhanVien_NoiHocBindingSource.DataMember = "GetNhanVien_NoiHoc";
            this.GetNhanVien_NoiHocBindingSource.DataSource = this.n05_Ql_NhanSu_T5DataSet_NhanVien_NoiHoc;
            // 
            // ReportForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(776, 728);
            this.Controls.Add(this.reportViewer2);
            this.Name = "ReportForm";
            this.Text = "ReportForm";
            this.Load += new System.EventHandler(this.ReportForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.n05_Ql_NhanSu_T5DataSet_NhanVien_NoiHoc)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.getNhanVienNoiHocBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.GetNhanVien_NoiHocBindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.BindingSource getNhanVienNoiHocBindingSource;
        private N05_Ql_NhanSu_T5DataSet_NhanVien_NoiHoc n05_Ql_NhanSu_T5DataSet_NhanVien_NoiHoc;
        private N05_Ql_NhanSu_T5DataSet_NhanVien_NoiHocTableAdapters.GetNhanVien_NoiHocTableAdapter getNhanVien_NoiHocTableAdapter;
        private Microsoft.Reporting.WinForms.ReportViewer reportViewer2;
        private System.Windows.Forms.BindingSource GetNhanVien_NoiHocBindingSource;
    }
}