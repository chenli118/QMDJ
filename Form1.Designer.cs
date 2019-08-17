namespace QMDJ
{
    partial class Form1
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.subGrid1 = new QMDJ.SubGridView();
            this.subGrid2 = new QMDJ.SubGridView();
            this.subGrid3 = new QMDJ.SubGridView();
            this.subGrid4 = new QMDJ.SubGridView();
            this.subGrid5 = new QMDJ.SubGridView();
            this.subGrid6 = new QMDJ.SubGridView();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.subGrid1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.subGrid2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.subGrid3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.subGrid4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.subGrid5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.subGrid6)).BeginInit();
            this.SuspendLayout();
            // 
            // subGrid1
            // 
            
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(18, 397);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(0, 18);
            this.label1.TabIndex = 7;
            this.label1.DoubleClick += new System.EventHandler(this.Label1_DoubleClick);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1039, 678);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.subGrid1);
            this.Controls.Add(this.subGrid2);
            this.Controls.Add(this.subGrid3);
            this.Controls.Add(this.subGrid4);
            this.Controls.Add(this.subGrid5);
            this.Controls.Add(this.subGrid6);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.subGrid1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.subGrid2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.subGrid3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.subGrid4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.subGrid5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.subGrid6)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private SubGridView subGrid1;
        private SubGridView subGrid2;
        private SubGridView subGrid3;
        private SubGridView subGrid4;
        private SubGridView subGrid5;
        private SubGridView subGrid6; 
        private System.Windows.Forms.Label label1;
    }
}

