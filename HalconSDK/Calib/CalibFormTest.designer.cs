
namespace HalconSDK.Calib
{
    partial class CalibFormTest
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
            this.notifyIcon1 = new System.Windows.Forms.NotifyIcon(this.components);
            this.btnGamma = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            this.button6 = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.pictureBox5 = new System.Windows.Forms.PictureBox();
            this.pictureBox4 = new System.Windows.Forms.PictureBox();
            this.pictureBox3 = new System.Windows.Forms.PictureBox();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.cbNestIndex = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.productColor1 = new System.Windows.Forms.ComboBox();
            this.label11 = new System.Windows.Forms.Label();
            this.btn_checkImg1 = new System.Windows.Forms.Button();
            this.pictureBox6 = new System.Windows.Forms.PictureBox();
            this.panel2 = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox6)).BeginInit();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // notifyIcon1
            // 
            this.notifyIcon1.Text = "notifyIcon1";
            this.notifyIcon1.Visible = true;
            // 
            // btnGamma
            // 
            this.btnGamma.BackColor = System.Drawing.Color.DarkGray;
            this.btnGamma.Location = new System.Drawing.Point(71, 152);
            this.btnGamma.Margin = new System.Windows.Forms.Padding(4);
            this.btnGamma.Name = "btnGamma";
            this.btnGamma.Size = new System.Drawing.Size(133, 48);
            this.btnGamma.TabIndex = 1;
            this.btnGamma.Text = "Gamma校正";
            this.btnGamma.UseVisualStyleBackColor = false;
            this.btnGamma.Click += new System.EventHandler(this.btnGamma_Click);
            // 
            // button2
            // 
            this.button2.BackColor = System.Drawing.Color.DarkGray;
            this.button2.Location = new System.Drawing.Point(71, 244);
            this.button2.Margin = new System.Windows.Forms.Padding(4);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(133, 48);
            this.button2.TabIndex = 2;
            this.button2.Text = "产品参数配置";
            this.button2.UseVisualStyleBackColor = false;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button3
            // 
            this.button3.BackColor = System.Drawing.Color.DarkGray;
            this.button3.Location = new System.Drawing.Point(71, 336);
            this.button3.Margin = new System.Windows.Forms.Padding(4);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(133, 48);
            this.button3.TabIndex = 3;
            this.button3.Text = "相机内参标定";
            this.button3.UseVisualStyleBackColor = false;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // button4
            // 
            this.button4.BackColor = System.Drawing.Color.DarkGray;
            this.button4.Location = new System.Drawing.Point(71, 514);
            this.button4.Margin = new System.Windows.Forms.Padding(4);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(133, 48);
            this.button4.TabIndex = 4;
            this.button4.Text = "生成绝对相位";
            this.button4.UseVisualStyleBackColor = false;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // button5
            // 
            this.button5.BackColor = System.Drawing.Color.DarkGray;
            this.button5.Location = new System.Drawing.Point(71, 425);
            this.button5.Margin = new System.Windows.Forms.Padding(4);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(133, 48);
            this.button5.TabIndex = 5;
            this.button5.Text = "参数配置文件";
            this.button5.UseVisualStyleBackColor = false;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // button6
            // 
            this.button6.BackColor = System.Drawing.Color.DarkGray;
            this.button6.Location = new System.Drawing.Point(71, 604);
            this.button6.Margin = new System.Windows.Forms.Padding(4);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(133, 48);
            this.button6.TabIndex = 6;
            this.button6.Text = "显示处理结果";
            this.button6.UseVisualStyleBackColor = false;
            this.button6.Click += new System.EventHandler(this.button6_Click);
            // 
            // panel1
            // 
            this.panel1.Location = new System.Drawing.Point(308, 106);
            this.panel1.Margin = new System.Windows.Forms.Padding(4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(765, 550);
            this.panel1.TabIndex = 11;
            // 
            // pictureBox5
            // 
            this.pictureBox5.Image = global::HalconSDK.Properties.Resource.ToDown;
            this.pictureBox5.Location = new System.Drawing.Point(106, 563);
            this.pictureBox5.Margin = new System.Windows.Forms.Padding(4);
            this.pictureBox5.Name = "pictureBox5";
            this.pictureBox5.Size = new System.Drawing.Size(65, 40);
            this.pictureBox5.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox5.TabIndex = 10;
            this.pictureBox5.TabStop = false;
            // 
            // pictureBox4
            // 
            this.pictureBox4.Image = global::HalconSDK.Properties.Resource.ToDown;
            this.pictureBox4.Location = new System.Drawing.Point(106, 473);
            this.pictureBox4.Margin = new System.Windows.Forms.Padding(4);
            this.pictureBox4.Name = "pictureBox4";
            this.pictureBox4.Size = new System.Drawing.Size(65, 40);
            this.pictureBox4.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox4.TabIndex = 9;
            this.pictureBox4.TabStop = false;
            // 
            // pictureBox3
            // 
            this.pictureBox3.Image = global::HalconSDK.Properties.Resource.ToDown;
            this.pictureBox3.Location = new System.Drawing.Point(106, 383);
            this.pictureBox3.Margin = new System.Windows.Forms.Padding(4);
            this.pictureBox3.Name = "pictureBox3";
            this.pictureBox3.Size = new System.Drawing.Size(65, 40);
            this.pictureBox3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox3.TabIndex = 8;
            this.pictureBox3.TabStop = false;
            // 
            // pictureBox2
            // 
            this.pictureBox2.Image = global::HalconSDK.Properties.Resource.ToDown;
            this.pictureBox2.Location = new System.Drawing.Point(106, 295);
            this.pictureBox2.Margin = new System.Windows.Forms.Padding(4);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(65, 40);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox2.TabIndex = 7;
            this.pictureBox2.TabStop = false;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::HalconSDK.Properties.Resource.ToDown;
            this.pictureBox1.Location = new System.Drawing.Point(106, 203);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(4);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(65, 40);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // cbNestIndex
            // 
            this.cbNestIndex.FormattingEnabled = true;
            this.cbNestIndex.Items.AddRange(new object[] {
            "1穴",
            "2穴"});
            this.cbNestIndex.Location = new System.Drawing.Point(453, 38);
            this.cbNestIndex.Margin = new System.Windows.Forms.Padding(4);
            this.cbNestIndex.Name = "cbNestIndex";
            this.cbNestIndex.Size = new System.Drawing.Size(117, 23);
            this.cbNestIndex.TabIndex = 13;
            this.cbNestIndex.Text = "1穴";
            this.cbNestIndex.SelectedIndexChanged += new System.EventHandler(this.cbNestIndex_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("微软雅黑", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(303, 32);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(134, 31);
            this.label1.TabIndex = 12;
            this.label1.Text = "穴号选择：";
            // 
            // productColor1
            // 
            this.productColor1.FormattingEnabled = true;
            this.productColor1.Items.AddRange(new object[] {
            "蓝色",
            "白色"});
            this.productColor1.Location = new System.Drawing.Point(813, 38);
            this.productColor1.Margin = new System.Windows.Forms.Padding(4);
            this.productColor1.Name = "productColor1";
            this.productColor1.Size = new System.Drawing.Size(117, 23);
            this.productColor1.TabIndex = 23;
            this.productColor1.Text = "蓝色";
            this.productColor1.SelectedIndexChanged += new System.EventHandler(this.productColor1_SelectedIndexChanged);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("微软雅黑", 14.25F);
            this.label11.Location = new System.Drawing.Point(663, 32);
            this.label11.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(134, 31);
            this.label11.TabIndex = 22;
            this.label11.Text = "产品颜色：";
            // 
            // btn_checkImg1
            // 
            this.btn_checkImg1.BackColor = System.Drawing.Color.DarkGray;
            this.btn_checkImg1.Location = new System.Drawing.Point(71, 52);
            this.btn_checkImg1.Margin = new System.Windows.Forms.Padding(4);
            this.btn_checkImg1.Name = "btn_checkImg1";
            this.btn_checkImg1.Size = new System.Drawing.Size(133, 48);
            this.btn_checkImg1.TabIndex = 7;
            this.btn_checkImg1.Text = "检测图像质量";
            this.btn_checkImg1.UseVisualStyleBackColor = false;
            this.btn_checkImg1.Click += new System.EventHandler(this.btn_checkImg1_Click);
            // 
            // pictureBox6
            // 
            this.pictureBox6.Image = global::HalconSDK.Properties.Resource.ToDown;
            this.pictureBox6.Location = new System.Drawing.Point(106, 108);
            this.pictureBox6.Margin = new System.Windows.Forms.Padding(4);
            this.pictureBox6.Name = "pictureBox6";
            this.pictureBox6.Size = new System.Drawing.Size(65, 40);
            this.pictureBox6.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox6.TabIndex = 0;
            this.pictureBox6.TabStop = false;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.btn_checkImg1);
            this.panel2.Controls.Add(this.productColor1);
            this.panel2.Controls.Add(this.pictureBox1);
            this.panel2.Controls.Add(this.label11);
            this.panel2.Controls.Add(this.pictureBox6);
            this.panel2.Controls.Add(this.cbNestIndex);
            this.panel2.Controls.Add(this.btnGamma);
            this.panel2.Controls.Add(this.label1);
            this.panel2.Controls.Add(this.button2);
            this.panel2.Controls.Add(this.button5);
            this.panel2.Controls.Add(this.button3);
            this.panel2.Controls.Add(this.panel1);
            this.panel2.Controls.Add(this.button4);
            this.panel2.Controls.Add(this.pictureBox5);
            this.panel2.Controls.Add(this.button6);
            this.panel2.Controls.Add(this.pictureBox4);
            this.panel2.Controls.Add(this.pictureBox2);
            this.panel2.Controls.Add(this.pictureBox3);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1232, 715);
            this.panel2.TabIndex = 24;
            // 
            // CalibFormTest
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlDark;
            this.ClientSize = new System.Drawing.Size(1232, 715);
            this.Controls.Add(this.panel2);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "CalibFormTest";
            this.Text = "CalibFormTest";
            this.Load += new System.EventHandler(this.CalibFormTest_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox6)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.NotifyIcon notifyIcon1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button btnGamma;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.Button button6;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.PictureBox pictureBox3;
        private System.Windows.Forms.PictureBox pictureBox4;
        private System.Windows.Forms.PictureBox pictureBox5;
        private System.Windows.Forms.ComboBox cbNestIndex;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox productColor1;
        private System.Windows.Forms.Label label11;
        public System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btn_checkImg1;
        private System.Windows.Forms.PictureBox pictureBox6;
        private System.Windows.Forms.Panel panel2;
    }
}