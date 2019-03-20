namespace ArcEngineDemos.Forms
{
    partial class AttributeQueryForm
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
            this.cboLayer = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.listBoxField = new System.Windows.Forms.ListBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.listBoxValue = new System.Windows.Forms.ListBox();
            this.Btnequal = new System.Windows.Forms.Button();
            this.btnunequal = new System.Windows.Forms.Button();
            this.btnis = new System.Windows.Forms.Button();
            this.btnlike = new System.Windows.Forms.Button();
            this.btnmore = new System.Windows.Forms.Button();
            this.btnnull = new System.Windows.Forms.Button();
            this.btnor = new System.Windows.Forms.Button();
            this.btnloe = new System.Windows.Forms.Button();
            this.btnmoe = new System.Windows.Forms.Button();
            this.Btnless = new System.Windows.Forms.Button();
            this.btnpercent = new System.Windows.Forms.Button();
            this.btnunderline = new System.Windows.Forms.Button();
            this.btnin = new System.Windows.Forms.Button();
            this.btnand = new System.Windows.Forms.Button();
            this.Btnnot = new System.Windows.Forms.Button();
            this.Btncharacter = new System.Windows.Forms.Button();
            this.btnbetween = new System.Windows.Forms.Button();
            this.btnspace = new System.Windows.Forms.Button();
            this.btnempty = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.textBoxSql = new System.Windows.Forms.RichTextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnOK = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // cboLayer
            // 
            this.cboLayer.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cboLayer.FormattingEnabled = true;
            this.cboLayer.Location = new System.Drawing.Point(96, 8);
            this.cboLayer.Name = "cboLayer";
            this.cboLayer.Size = new System.Drawing.Size(294, 24);
            this.cboLayer.TabIndex = 0;
            this.cboLayer.SelectedIndexChanged += new System.EventHandler(this.cboLayer_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(34, 11);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(56, 16);
            this.label1.TabIndex = 1;
            this.label1.Text = "图层：";
            // 
            // listBoxField
            // 
            this.listBoxField.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.listBoxField.FormattingEnabled = true;
            this.listBoxField.ItemHeight = 16;
            this.listBoxField.Location = new System.Drawing.Point(37, 70);
            this.listBoxField.Name = "listBoxField";
            this.listBoxField.Size = new System.Drawing.Size(146, 100);
            this.listBoxField.TabIndex = 2;
            this.listBoxField.Click += new System.EventHandler(this.listBoxField_Click);
            this.listBoxField.SelectedIndexChanged += new System.EventHandler(this.listBoxField_SelectedIndexChanged);
            this.listBoxField.DoubleClick += new System.EventHandler(this.listBoxField_DoubleClick);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.Location = new System.Drawing.Point(34, 48);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(56, 16);
            this.label2.TabIndex = 3;
            this.label2.Text = "字段：";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label3.Location = new System.Drawing.Point(241, 48);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(56, 16);
            this.label3.TabIndex = 4;
            this.label3.Text = "取值：";
            // 
            // listBoxValue
            // 
            this.listBoxValue.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.listBoxValue.FormattingEnabled = true;
            this.listBoxValue.ItemHeight = 16;
            this.listBoxValue.Location = new System.Drawing.Point(244, 70);
            this.listBoxValue.Name = "listBoxValue";
            this.listBoxValue.Size = new System.Drawing.Size(146, 100);
            this.listBoxValue.TabIndex = 5;
            this.listBoxValue.Click += new System.EventHandler(this.listBoxValue_Click);
            this.listBoxValue.DoubleClick += new System.EventHandler(this.listBoxValue_DoubleClick);
            // 
            // Btnequal
            // 
            this.Btnequal.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Btnequal.Location = new System.Drawing.Point(13, 26);
            this.Btnequal.Name = "Btnequal";
            this.Btnequal.Size = new System.Drawing.Size(65, 23);
            this.Btnequal.TabIndex = 7;
            this.Btnequal.Text = "=";
            this.Btnequal.UseVisualStyleBackColor = true;
            this.Btnequal.Click += new System.EventHandler(this.Btnequal_Click);
            // 
            // btnunequal
            // 
            this.btnunequal.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnunequal.Location = new System.Drawing.Point(84, 26);
            this.btnunequal.Name = "btnunequal";
            this.btnunequal.Size = new System.Drawing.Size(65, 23);
            this.btnunequal.TabIndex = 8;
            this.btnunequal.Text = "！=";
            this.btnunequal.UseVisualStyleBackColor = true;
            this.btnunequal.Click += new System.EventHandler(this.btnunequal_Click);
            // 
            // btnis
            // 
            this.btnis.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnis.Location = new System.Drawing.Point(155, 26);
            this.btnis.Name = "btnis";
            this.btnis.Size = new System.Drawing.Size(65, 23);
            this.btnis.TabIndex = 9;
            this.btnis.Text = "is";
            this.btnis.UseVisualStyleBackColor = true;
            this.btnis.Click += new System.EventHandler(this.btnis_Click);
            // 
            // btnlike
            // 
            this.btnlike.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnlike.Location = new System.Drawing.Point(226, 26);
            this.btnlike.Name = "btnlike";
            this.btnlike.Size = new System.Drawing.Size(65, 23);
            this.btnlike.TabIndex = 10;
            this.btnlike.Text = "like";
            this.btnlike.UseVisualStyleBackColor = true;
            this.btnlike.Click += new System.EventHandler(this.btnlike_Click);
            // 
            // btnmore
            // 
            this.btnmore.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnmore.Location = new System.Drawing.Point(297, 26);
            this.btnmore.Name = "btnmore";
            this.btnmore.Size = new System.Drawing.Size(65, 23);
            this.btnmore.TabIndex = 11;
            this.btnmore.Text = ">";
            this.btnmore.UseVisualStyleBackColor = true;
            this.btnmore.Click += new System.EventHandler(this.btnmore_Click);
            // 
            // btnnull
            // 
            this.btnnull.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnnull.Location = new System.Drawing.Point(297, 56);
            this.btnnull.Name = "btnnull";
            this.btnnull.Size = new System.Drawing.Size(65, 23);
            this.btnnull.TabIndex = 16;
            this.btnnull.Text = "null";
            this.btnnull.UseVisualStyleBackColor = true;
            this.btnnull.Click += new System.EventHandler(this.btnnull_Click);
            // 
            // btnor
            // 
            this.btnor.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnor.Location = new System.Drawing.Point(226, 56);
            this.btnor.Name = "btnor";
            this.btnor.Size = new System.Drawing.Size(65, 23);
            this.btnor.TabIndex = 15;
            this.btnor.Text = "Or";
            this.btnor.UseVisualStyleBackColor = true;
            this.btnor.Click += new System.EventHandler(this.btnor_Click);
            // 
            // btnloe
            // 
            this.btnloe.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnloe.Location = new System.Drawing.Point(155, 56);
            this.btnloe.Name = "btnloe";
            this.btnloe.Size = new System.Drawing.Size(65, 23);
            this.btnloe.TabIndex = 14;
            this.btnloe.Text = "<=";
            this.btnloe.UseVisualStyleBackColor = true;
            this.btnloe.Click += new System.EventHandler(this.btnloe_Click);
            // 
            // btnmoe
            // 
            this.btnmoe.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnmoe.Location = new System.Drawing.Point(84, 56);
            this.btnmoe.Name = "btnmoe";
            this.btnmoe.Size = new System.Drawing.Size(65, 23);
            this.btnmoe.TabIndex = 13;
            this.btnmoe.Text = ">=";
            this.btnmoe.UseVisualStyleBackColor = true;
            this.btnmoe.Click += new System.EventHandler(this.btnmoe_Click);
            // 
            // Btnless
            // 
            this.Btnless.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Btnless.Location = new System.Drawing.Point(13, 56);
            this.Btnless.Name = "Btnless";
            this.Btnless.Size = new System.Drawing.Size(65, 23);
            this.Btnless.TabIndex = 12;
            this.Btnless.Text = "<";
            this.Btnless.UseVisualStyleBackColor = true;
            this.Btnless.Click += new System.EventHandler(this.Btnless_Click);
            // 
            // btnpercent
            // 
            this.btnpercent.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnpercent.Location = new System.Drawing.Point(297, 87);
            this.btnpercent.Name = "btnpercent";
            this.btnpercent.Size = new System.Drawing.Size(65, 23);
            this.btnpercent.TabIndex = 21;
            this.btnpercent.Text = "%";
            this.btnpercent.UseVisualStyleBackColor = true;
            this.btnpercent.Click += new System.EventHandler(this.btnpercent_Click);
            // 
            // btnunderline
            // 
            this.btnunderline.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnunderline.Location = new System.Drawing.Point(226, 87);
            this.btnunderline.Name = "btnunderline";
            this.btnunderline.Size = new System.Drawing.Size(65, 23);
            this.btnunderline.TabIndex = 20;
            this.btnunderline.Text = "_";
            this.btnunderline.UseVisualStyleBackColor = true;
            // 
            // btnin
            // 
            this.btnin.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnin.Location = new System.Drawing.Point(155, 87);
            this.btnin.Name = "btnin";
            this.btnin.Size = new System.Drawing.Size(65, 23);
            this.btnin.TabIndex = 19;
            this.btnin.Text = "In";
            this.btnin.UseVisualStyleBackColor = true;
            this.btnin.Click += new System.EventHandler(this.btnin_Click);
            // 
            // btnand
            // 
            this.btnand.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnand.Location = new System.Drawing.Point(84, 87);
            this.btnand.Name = "btnand";
            this.btnand.Size = new System.Drawing.Size(65, 23);
            this.btnand.TabIndex = 18;
            this.btnand.Text = "And";
            this.btnand.UseVisualStyleBackColor = true;
            this.btnand.Click += new System.EventHandler(this.btnand_Click);
            // 
            // Btnnot
            // 
            this.Btnnot.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Btnnot.Location = new System.Drawing.Point(13, 87);
            this.Btnnot.Name = "Btnnot";
            this.Btnnot.Size = new System.Drawing.Size(65, 23);
            this.Btnnot.TabIndex = 17;
            this.Btnnot.Text = "Not";
            this.Btnnot.UseVisualStyleBackColor = true;
            this.Btnnot.Click += new System.EventHandler(this.Btnnot_Click);
            // 
            // Btncharacter
            // 
            this.Btncharacter.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Btncharacter.Location = new System.Drawing.Point(13, 116);
            this.Btncharacter.Name = "Btncharacter";
            this.Btncharacter.Size = new System.Drawing.Size(78, 23);
            this.Btncharacter.TabIndex = 22;
            this.Btncharacter.Text = "\'   \'";
            this.Btncharacter.UseVisualStyleBackColor = true;
            this.Btncharacter.Click += new System.EventHandler(this.Btncharacter_Click);
            // 
            // btnbetween
            // 
            this.btnbetween.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnbetween.Location = new System.Drawing.Point(97, 116);
            this.btnbetween.Name = "btnbetween";
            this.btnbetween.Size = new System.Drawing.Size(78, 23);
            this.btnbetween.TabIndex = 23;
            this.btnbetween.Text = "Between";
            this.btnbetween.UseVisualStyleBackColor = true;
            this.btnbetween.Click += new System.EventHandler(this.btnbetween_Click);
            // 
            // btnspace
            // 
            this.btnspace.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnspace.Location = new System.Drawing.Point(181, 116);
            this.btnspace.Name = "btnspace";
            this.btnspace.Size = new System.Drawing.Size(78, 23);
            this.btnspace.TabIndex = 24;
            this.btnspace.Text = "空格";
            this.btnspace.UseVisualStyleBackColor = true;
            // 
            // btnempty
            // 
            this.btnempty.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnempty.Location = new System.Drawing.Point(265, 116);
            this.btnempty.Name = "btnempty";
            this.btnempty.Size = new System.Drawing.Size(78, 23);
            this.btnempty.TabIndex = 25;
            this.btnempty.Text = "清空";
            this.btnempty.UseVisualStyleBackColor = true;
            this.btnempty.Click += new System.EventHandler(this.btnempty_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label5.Location = new System.Drawing.Point(34, 336);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(208, 16);
            this.label5.TabIndex = 26;
            this.label5.Text = "Select * From Table Where";
            // 
            // textBoxSql
            // 
            this.textBoxSql.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.textBoxSql.Location = new System.Drawing.Point(37, 356);
            this.textBoxSql.Name = "textBoxSql";
            this.textBoxSql.Size = new System.Drawing.Size(353, 56);
            this.textBoxSql.TabIndex = 27;
            this.textBoxSql.Text = "";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnnull);
            this.groupBox1.Controls.Add(this.Btnequal);
            this.groupBox1.Controls.Add(this.btnunequal);
            this.groupBox1.Controls.Add(this.btnempty);
            this.groupBox1.Controls.Add(this.btnis);
            this.groupBox1.Controls.Add(this.btnspace);
            this.groupBox1.Controls.Add(this.btnlike);
            this.groupBox1.Controls.Add(this.btnbetween);
            this.groupBox1.Controls.Add(this.btnmore);
            this.groupBox1.Controls.Add(this.Btncharacter);
            this.groupBox1.Controls.Add(this.Btnless);
            this.groupBox1.Controls.Add(this.btnpercent);
            this.groupBox1.Controls.Add(this.btnmoe);
            this.groupBox1.Controls.Add(this.btnunderline);
            this.groupBox1.Controls.Add(this.btnloe);
            this.groupBox1.Controls.Add(this.btnin);
            this.groupBox1.Controls.Add(this.btnor);
            this.groupBox1.Controls.Add(this.btnand);
            this.groupBox1.Controls.Add(this.Btnnot);
            this.groupBox1.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.groupBox1.Location = new System.Drawing.Point(37, 181);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(381, 152);
            this.groupBox1.TabIndex = 28;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "表达式";
            // 
            // btnOK
            // 
            this.btnOK.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnOK.Location = new System.Drawing.Point(53, 418);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 29;
            this.btnOK.Text = "查找";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // button2
            // 
            this.button2.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button2.Location = new System.Drawing.Point(280, 418);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 30;
            this.button2.Text = "取消";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // AttributeQueryForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(426, 450);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.textBoxSql);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.listBoxValue);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.listBoxField);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cboLayer);
            this.Name = "AttributeQueryForm";
            this.Text = "属性查询";
            this.Load += new System.EventHandler(this.AttributeQueryForm_Load);
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cboLayer;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ListBox listBoxField;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ListBox listBoxValue;
        private System.Windows.Forms.Button Btnequal;
        private System.Windows.Forms.Button btnunequal;
        private System.Windows.Forms.Button btnis;
        private System.Windows.Forms.Button btnlike;
        private System.Windows.Forms.Button btnmore;
        private System.Windows.Forms.Button btnnull;
        private System.Windows.Forms.Button btnor;
        private System.Windows.Forms.Button btnloe;
        private System.Windows.Forms.Button btnmoe;
        private System.Windows.Forms.Button Btnless;
        private System.Windows.Forms.Button btnpercent;
        private System.Windows.Forms.Button btnunderline;
        private System.Windows.Forms.Button btnin;
        private System.Windows.Forms.Button btnand;
        private System.Windows.Forms.Button Btnnot;
        private System.Windows.Forms.Button Btncharacter;
        private System.Windows.Forms.Button btnbetween;
        private System.Windows.Forms.Button btnspace;
        private System.Windows.Forms.Button btnempty;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.RichTextBox textBoxSql;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button button2;
    }
}