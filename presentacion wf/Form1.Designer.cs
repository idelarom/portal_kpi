namespace presentacion_wf
{
    partial class Form1
    {
        /// <summary>
        /// Variable del diseñador necesaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén usando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnseleccionar = new System.Windows.Forms.Button();
            this.lblpath = new System.Windows.Forms.Label();
            this.ltvPictures = new System.Windows.Forms.ListView();
            this.btnsubir = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnseleccionar
            // 
            this.btnseleccionar.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnseleccionar.Location = new System.Drawing.Point(373, 27);
            this.btnseleccionar.Name = "btnseleccionar";
            this.btnseleccionar.Size = new System.Drawing.Size(144, 24);
            this.btnseleccionar.TabIndex = 1;
            this.btnseleccionar.Text = "Seleccionar";
            this.btnseleccionar.UseVisualStyleBackColor = true;
            this.btnseleccionar.Click += new System.EventHandler(this.btnseleccionar_Click);
            // 
            // lblpath
            // 
            this.lblpath.AutoSize = true;
            this.lblpath.Location = new System.Drawing.Point(247, 33);
            this.lblpath.Name = "lblpath";
            this.lblpath.Size = new System.Drawing.Size(120, 13);
            this.lblpath.TabIndex = 2;
            this.lblpath.Text = "--Seleccione un carpeta";
            // 
            // ltvPictures
            // 
            this.ltvPictures.Location = new System.Drawing.Point(15, 57);
            this.ltvPictures.Name = "ltvPictures";
            this.ltvPictures.Size = new System.Drawing.Size(502, 250);
            this.ltvPictures.TabIndex = 3;
            this.ltvPictures.UseCompatibleStateImageBehavior = false;
            this.ltvPictures.View = System.Windows.Forms.View.List;
            // 
            // btnsubir
            // 
            this.btnsubir.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnsubir.Location = new System.Drawing.Point(373, 343);
            this.btnsubir.Name = "btnsubir";
            this.btnsubir.Size = new System.Drawing.Size(144, 24);
            this.btnsubir.TabIndex = 4;
            this.btnsubir.Text = "Subir a Directorio activo";
            this.btnsubir.UseVisualStyleBackColor = true;
            this.btnsubir.Click += new System.EventHandler(this.btnsubir_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(529, 379);
            this.Controls.Add(this.btnsubir);
            this.Controls.Add(this.ltvPictures);
            this.Controls.Add(this.lblpath);
            this.Controls.Add(this.btnseleccionar);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Inicio";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button btnseleccionar;
        private System.Windows.Forms.Label lblpath;
        private System.Windows.Forms.ListView ltvPictures;
        private System.Windows.Forms.Button btnsubir;
    }
}

