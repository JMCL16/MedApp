namespace MedApp
{
    partial class Main
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Main));
            this.newPacientBtn = new System.Windows.Forms.Button();
            this.searchPacientBtn = new System.Windows.Forms.Button();
            this.newConsultaBtn = new System.Windows.Forms.Button();
            this.gestionarUserBtn = new System.Windows.Forms.Button();
            this.CloseSessionBtn = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // newPacientBtn
            // 
            this.newPacientBtn.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.newPacientBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.newPacientBtn.Location = new System.Drawing.Point(439, 168);
            this.newPacientBtn.Name = "newPacientBtn";
            this.newPacientBtn.Size = new System.Drawing.Size(398, 87);
            this.newPacientBtn.TabIndex = 2;
            this.newPacientBtn.Text = "Nuevo Paciente";
            this.newPacientBtn.UseVisualStyleBackColor = true;
            this.newPacientBtn.Click += new System.EventHandler(this.newPacientBtn_Click);
            // 
            // searchPacientBtn
            // 
            this.searchPacientBtn.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.searchPacientBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.searchPacientBtn.Location = new System.Drawing.Point(439, 283);
            this.searchPacientBtn.Name = "searchPacientBtn";
            this.searchPacientBtn.Size = new System.Drawing.Size(398, 85);
            this.searchPacientBtn.TabIndex = 3;
            this.searchPacientBtn.Text = "Buscar Paciente";
            this.searchPacientBtn.UseVisualStyleBackColor = true;
            this.searchPacientBtn.Click += new System.EventHandler(this.searchPacientBtn_Click);
            // 
            // newConsultaBtn
            // 
            this.newConsultaBtn.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.newConsultaBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.newConsultaBtn.Location = new System.Drawing.Point(439, 400);
            this.newConsultaBtn.Name = "newConsultaBtn";
            this.newConsultaBtn.Size = new System.Drawing.Size(398, 88);
            this.newConsultaBtn.TabIndex = 4;
            this.newConsultaBtn.Text = "Nueva Consulta";
            this.newConsultaBtn.UseVisualStyleBackColor = true;
            this.newConsultaBtn.Click += new System.EventHandler(this.newConsultaBtn_Click);
            // 
            // gestionarUserBtn
            // 
            this.gestionarUserBtn.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.gestionarUserBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gestionarUserBtn.Location = new System.Drawing.Point(439, 646);
            this.gestionarUserBtn.Name = "gestionarUserBtn";
            this.gestionarUserBtn.Size = new System.Drawing.Size(398, 87);
            this.gestionarUserBtn.TabIndex = 5;
            this.gestionarUserBtn.Text = "Gestionar Usuarios";
            this.gestionarUserBtn.UseVisualStyleBackColor = true;
            // 
            // CloseSessionBtn
            // 
            this.CloseSessionBtn.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.CloseSessionBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CloseSessionBtn.Location = new System.Drawing.Point(439, 523);
            this.CloseSessionBtn.Name = "CloseSessionBtn";
            this.CloseSessionBtn.Size = new System.Drawing.Size(398, 87);
            this.CloseSessionBtn.TabIndex = 6;
            this.CloseSessionBtn.Text = "Cerrar Sesion";
            this.CloseSessionBtn.UseVisualStyleBackColor = true;
            this.CloseSessionBtn.Click += new System.EventHandler(this.CloseSessionBtn_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.CloseSessionBtn);
            this.panel1.Controls.Add(this.gestionarUserBtn);
            this.panel1.Controls.Add(this.newConsultaBtn);
            this.panel1.Controls.Add(this.searchPacientBtn);
            this.panel1.Controls.Add(this.newPacientBtn);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1321, 819);
            this.panel1.TabIndex = 7;
            // 
            // label4
            // 
            this.label4.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(441, 121);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(396, 22);
            this.label4.TabIndex = 12;
            this.label4.Text = "Calle Nicolas Heredia #40, Esq. Jacinto Castro";
            // 
            // label3
            // 
            this.label3.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Times New Roman", 16.2F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(514, 88);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(236, 33);
            this.label3.TabIndex = 11;
            this.label3.Text = "Dra. Luchy Romero";
            // 
            // label5
            // 
            this.label5.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Times New Roman", 19.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(480, 50);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(307, 37);
            this.label5.TabIndex = 10;
            this.label5.Text = "Consultorio Medico ";
            this.label5.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.ClientSize = new System.Drawing.Size(1321, 819);
            this.Controls.Add(this.panel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Main";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Principal";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button newPacientBtn;
        private System.Windows.Forms.Button searchPacientBtn;
        private System.Windows.Forms.Button newConsultaBtn;
        private System.Windows.Forms.Button gestionarUserBtn;
        private System.Windows.Forms.Button CloseSessionBtn;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label5;
    }
}

