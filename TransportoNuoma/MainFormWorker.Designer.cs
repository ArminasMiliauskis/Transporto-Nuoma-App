namespace TransportoNuoma
{
    partial class MainFormWorker
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
            this.gmap = new GMap.NET.WindowsForms.GMapControl();
            this.workerButton = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label13 = new System.Windows.Forms.Label();
            this.dataGridView5 = new System.Windows.Forms.DataGridView();
            this.updatePakrovimasPanel = new System.Windows.Forms.Panel();
            this.updatePakrovimasTransId = new System.Windows.Forms.TextBox();
            this.updatePakrovimasPakrId = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.updatePakrovimasPakrovDyd = new System.Windows.Forms.TextBox();
            this.updatePakrovimas = new System.Windows.Forms.Button();
            this.label16 = new System.Windows.Forms.Label();
            this.addPakrovimasPanel = new System.Windows.Forms.Panel();
            this.addPakrovimas = new System.Windows.Forms.Button();
            this.addPakrovimasTransId = new System.Windows.Forms.TextBox();
            this.label17 = new System.Windows.Forms.Label();
            this.addPakrovimasPakrovDydis = new System.Windows.Forms.TextBox();
            this.label18 = new System.Windows.Forms.Label();
            this.updatePakrovimasShow = new System.Windows.Forms.Button();
            this.addPakrovimasShow = new System.Windows.Forms.Button();
            this.getPakrovimas = new System.Windows.Forms.Button();
            this.dataGridView6 = new System.Windows.Forms.DataGridView();
            this.label19 = new System.Windows.Forms.Label();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView5)).BeginInit();
            this.updatePakrovimasPanel.SuspendLayout();
            this.addPakrovimasPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView6)).BeginInit();
            this.SuspendLayout();
            // 
            // gmap
            // 
            this.gmap.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gmap.Bearing = 0F;
            this.gmap.CanDragMap = true;
            this.gmap.EmptyTileColor = System.Drawing.Color.Navy;
            this.gmap.GrayScaleMode = false;
            this.gmap.HelperLineOption = GMap.NET.WindowsForms.HelperLineOptions.DontShow;
            this.gmap.LevelsKeepInMemory = 5;
            this.gmap.Location = new System.Drawing.Point(13, 13);
            this.gmap.MarkersEnabled = true;
            this.gmap.MaxZoom = 2;
            this.gmap.MinZoom = 2;
            this.gmap.MouseWheelZoomEnabled = true;
            this.gmap.MouseWheelZoomType = GMap.NET.MouseWheelZoomType.MousePositionAndCenter;
            this.gmap.Name = "gmap";
            this.gmap.NegativeMode = false;
            this.gmap.PolygonsEnabled = true;
            this.gmap.RetryLoadTile = 0;
            this.gmap.RoutesEnabled = true;
            this.gmap.ScaleMode = GMap.NET.WindowsForms.ScaleModes.Integer;
            this.gmap.SelectedAreaFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(65)))), ((int)(((byte)(105)))), ((int)(((byte)(225)))));
            this.gmap.ShowTileGridLines = false;
            this.gmap.Size = new System.Drawing.Size(482, 606);
            this.gmap.TabIndex = 0;
            this.gmap.Zoom = 0D;
            // 
            // workerButton
            // 
            this.workerButton.Location = new System.Drawing.Point(501, 13);
            this.workerButton.Name = "workerButton";
            this.workerButton.Size = new System.Drawing.Size(108, 23);
            this.workerButton.TabIndex = 1;
            this.workerButton.Text = "Administrate testing";
            this.workerButton.UseVisualStyleBackColor = true;
            this.workerButton.Click += new System.EventHandler(this.workerButton_Click);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.label13);
            this.panel2.Controls.Add(this.dataGridView5);
            this.panel2.Controls.Add(this.updatePakrovimasPanel);
            this.panel2.Controls.Add(this.addPakrovimasPanel);
            this.panel2.Controls.Add(this.updatePakrovimasShow);
            this.panel2.Controls.Add(this.addPakrovimasShow);
            this.panel2.Controls.Add(this.getPakrovimas);
            this.panel2.Controls.Add(this.dataGridView6);
            this.panel2.Controls.Add(this.label19);
            this.panel2.Location = new System.Drawing.Point(501, 42);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(555, 584);
            this.panel2.TabIndex = 22;
            this.panel2.Visible = false;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.Location = new System.Drawing.Point(14, 216);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(109, 24);
            this.label13.TabIndex = 19;
            this.label13.Text = "Transportas";
            // 
            // dataGridView5
            // 
            this.dataGridView5.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView5.Location = new System.Drawing.Point(18, 240);
            this.dataGridView5.Name = "dataGridView5";
            this.dataGridView5.Size = new System.Drawing.Size(534, 111);
            this.dataGridView5.TabIndex = 18;
            // 
            // updatePakrovimasPanel
            // 
            this.updatePakrovimasPanel.Controls.Add(this.updatePakrovimasTransId);
            this.updatePakrovimasPanel.Controls.Add(this.updatePakrovimasPakrId);
            this.updatePakrovimasPanel.Controls.Add(this.label14);
            this.updatePakrovimasPanel.Controls.Add(this.label15);
            this.updatePakrovimasPanel.Controls.Add(this.updatePakrovimasPakrovDyd);
            this.updatePakrovimasPanel.Controls.Add(this.updatePakrovimas);
            this.updatePakrovimasPanel.Controls.Add(this.label16);
            this.updatePakrovimasPanel.Location = new System.Drawing.Point(204, 385);
            this.updatePakrovimasPanel.Name = "updatePakrovimasPanel";
            this.updatePakrovimasPanel.Size = new System.Drawing.Size(178, 196);
            this.updatePakrovimasPanel.TabIndex = 17;
            this.updatePakrovimasPanel.Visible = false;
            // 
            // updatePakrovimasTransId
            // 
            this.updatePakrovimasTransId.Location = new System.Drawing.Point(20, 67);
            this.updatePakrovimasTransId.Name = "updatePakrovimasTransId";
            this.updatePakrovimasTransId.Size = new System.Drawing.Size(126, 20);
            this.updatePakrovimasTransId.TabIndex = 18;
            // 
            // updatePakrovimasPakrId
            // 
            this.updatePakrovimasPakrId.Location = new System.Drawing.Point(20, 107);
            this.updatePakrovimasPakrId.Name = "updatePakrovimasPakrId";
            this.updatePakrovimasPakrId.Size = new System.Drawing.Size(126, 20);
            this.updatePakrovimasPakrId.TabIndex = 20;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(17, 50);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(49, 13);
            this.label14.TabIndex = 17;
            this.label14.Text = "Trans_Id";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(17, 90);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(67, 13);
            this.label15.TabIndex = 19;
            this.label15.Text = "pakrovimoNr";
            // 
            // updatePakrovimasPakrovDyd
            // 
            this.updatePakrovimasPakrovDyd.Location = new System.Drawing.Point(20, 27);
            this.updatePakrovimasPakrovDyd.Name = "updatePakrovimasPakrovDyd";
            this.updatePakrovimasPakrovDyd.Size = new System.Drawing.Size(126, 20);
            this.updatePakrovimasPakrovDyd.TabIndex = 16;
            // 
            // updatePakrovimas
            // 
            this.updatePakrovimas.Location = new System.Drawing.Point(20, 133);
            this.updatePakrovimas.Name = "updatePakrovimas";
            this.updatePakrovimas.Size = new System.Drawing.Size(126, 33);
            this.updatePakrovimas.TabIndex = 14;
            this.updatePakrovimas.Text = "Update";
            this.updatePakrovimas.UseVisualStyleBackColor = true;
            this.updatePakrovimas.Click += new System.EventHandler(this.updatePakrovimas_Click);
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(17, 10);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(83, 13);
            this.label16.TabIndex = 15;
            this.label16.Text = "PakrovimoDydis";
            // 
            // addPakrovimasPanel
            // 
            this.addPakrovimasPanel.Controls.Add(this.addPakrovimas);
            this.addPakrovimasPanel.Controls.Add(this.addPakrovimasTransId);
            this.addPakrovimasPanel.Controls.Add(this.label17);
            this.addPakrovimasPanel.Controls.Add(this.addPakrovimasPakrovDydis);
            this.addPakrovimasPanel.Controls.Add(this.label18);
            this.addPakrovimasPanel.Location = new System.Drawing.Point(18, 386);
            this.addPakrovimasPanel.Name = "addPakrovimasPanel";
            this.addPakrovimasPanel.Size = new System.Drawing.Size(178, 155);
            this.addPakrovimasPanel.TabIndex = 5;
            this.addPakrovimasPanel.Visible = false;
            // 
            // addPakrovimas
            // 
            this.addPakrovimas.Location = new System.Drawing.Point(20, 106);
            this.addPakrovimas.Name = "addPakrovimas";
            this.addPakrovimas.Size = new System.Drawing.Size(126, 33);
            this.addPakrovimas.TabIndex = 14;
            this.addPakrovimas.Text = "Add";
            this.addPakrovimas.UseVisualStyleBackColor = true;
            this.addPakrovimas.Click += new System.EventHandler(this.addPakrovimas_Click);
            // 
            // addPakrovimasTransId
            // 
            this.addPakrovimasTransId.Location = new System.Drawing.Point(20, 66);
            this.addPakrovimasTransId.Name = "addPakrovimasTransId";
            this.addPakrovimasTransId.Size = new System.Drawing.Size(126, 20);
            this.addPakrovimasTransId.TabIndex = 3;
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(17, 49);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(49, 13);
            this.label17.TabIndex = 2;
            this.label17.Text = "Trans_Id";
            // 
            // addPakrovimasPakrovDydis
            // 
            this.addPakrovimasPakrovDydis.Location = new System.Drawing.Point(20, 26);
            this.addPakrovimasPakrovDydis.Name = "addPakrovimasPakrovDydis";
            this.addPakrovimasPakrovDydis.Size = new System.Drawing.Size(126, 20);
            this.addPakrovimasPakrovDydis.TabIndex = 1;
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(17, 9);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(83, 13);
            this.label18.TabIndex = 0;
            this.label18.Text = "PakrovimoDydis";
            // 
            // updatePakrovimasShow
            // 
            this.updatePakrovimasShow.Location = new System.Drawing.Point(240, 356);
            this.updatePakrovimasShow.Name = "updatePakrovimasShow";
            this.updatePakrovimasShow.Size = new System.Drawing.Size(110, 23);
            this.updatePakrovimasShow.TabIndex = 4;
            this.updatePakrovimasShow.Text = "Update pakrovimas";
            this.updatePakrovimasShow.UseVisualStyleBackColor = true;
            this.updatePakrovimasShow.Click += new System.EventHandler(this.updatePakrovimasShow_Click);
            // 
            // addPakrovimasShow
            // 
            this.addPakrovimasShow.Location = new System.Drawing.Point(137, 356);
            this.addPakrovimasShow.Name = "addPakrovimasShow";
            this.addPakrovimasShow.Size = new System.Drawing.Size(97, 23);
            this.addPakrovimasShow.TabIndex = 3;
            this.addPakrovimasShow.Text = "Add pakrovimas";
            this.addPakrovimasShow.UseVisualStyleBackColor = true;
            this.addPakrovimasShow.Click += new System.EventHandler(this.addPakrovimasShow_Click);
            // 
            // getPakrovimas
            // 
            this.getPakrovimas.Location = new System.Drawing.Point(18, 357);
            this.getPakrovimas.Name = "getPakrovimas";
            this.getPakrovimas.Size = new System.Drawing.Size(113, 23);
            this.getPakrovimas.TabIndex = 2;
            this.getPakrovimas.Text = "Get pakrovimas";
            this.getPakrovimas.UseVisualStyleBackColor = true;
            this.getPakrovimas.Click += new System.EventHandler(this.getPakrovimas_Click);
            // 
            // dataGridView6
            // 
            this.dataGridView6.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView6.Location = new System.Drawing.Point(18, 42);
            this.dataGridView6.Name = "dataGridView6";
            this.dataGridView6.Size = new System.Drawing.Size(534, 171);
            this.dataGridView6.TabIndex = 1;
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label19.Location = new System.Drawing.Point(14, 15);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(106, 24);
            this.label19.TabIndex = 0;
            this.label19.Text = "Pakrovimas";
            // 
            // MainFormWorker
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1068, 631);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.workerButton);
            this.Controls.Add(this.gmap);
            this.Name = "MainFormWorker";
            this.Text = "MainFormWorker";
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView5)).EndInit();
            this.updatePakrovimasPanel.ResumeLayout(false);
            this.updatePakrovimasPanel.PerformLayout();
            this.addPakrovimasPanel.ResumeLayout(false);
            this.addPakrovimasPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView6)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private GMap.NET.WindowsForms.GMapControl gmap;
        private System.Windows.Forms.Button workerButton;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.DataGridView dataGridView5;
        private System.Windows.Forms.Panel updatePakrovimasPanel;
        private System.Windows.Forms.TextBox updatePakrovimasTransId;
        private System.Windows.Forms.TextBox updatePakrovimasPakrId;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.TextBox updatePakrovimasPakrovDyd;
        private System.Windows.Forms.Button updatePakrovimas;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Panel addPakrovimasPanel;
        private System.Windows.Forms.Button addPakrovimas;
        private System.Windows.Forms.TextBox addPakrovimasTransId;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.TextBox addPakrovimasPakrovDydis;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Button updatePakrovimasShow;
        private System.Windows.Forms.Button addPakrovimasShow;
        private System.Windows.Forms.Button getPakrovimas;
        private System.Windows.Forms.DataGridView dataGridView6;
        private System.Windows.Forms.Label label19;
    }
}