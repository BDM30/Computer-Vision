namespace SingleView
{
  partial class Form1
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
      this.pictureBox = new System.Windows.Forms.PictureBox();
      this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.editToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.xLinesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.referencePlaneToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.referenceDirectionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.pointsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.calculationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.xVPointToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.homographyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.alphaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.sceneToImageToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.menuStrip = new System.Windows.Forms.MenuStrip();
      this.listBox = new System.Windows.Forms.ListBox();
      this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
      ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).BeginInit();
      this.menuStrip.SuspendLayout();
      this.SuspendLayout();
      // 
      // pictureBox
      // 
      this.pictureBox.Location = new System.Drawing.Point(0, 28);
      this.pictureBox.Margin = new System.Windows.Forms.Padding(0);
      this.pictureBox.Name = "pictureBox";
      this.pictureBox.Size = new System.Drawing.Size(1050, 502);
      this.pictureBox.TabIndex = 1;
      this.pictureBox.TabStop = false;
      this.pictureBox.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pictureBox_MouseDown);
      // 
      // fileToolStripMenuItem
      // 
      this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openToolStripMenuItem});
      this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
      this.fileToolStripMenuItem.Size = new System.Drawing.Size(44, 24);
      this.fileToolStripMenuItem.Text = "File";
      // 
      // openToolStripMenuItem
      // 
      this.openToolStripMenuItem.Name = "openToolStripMenuItem";
      this.openToolStripMenuItem.Size = new System.Drawing.Size(120, 26);
      this.openToolStripMenuItem.Text = "Open";
      this.openToolStripMenuItem.Click += new System.EventHandler(this.openToolStripMenuItem_Click);
      // 
      // editToolStripMenuItem
      // 
      this.editToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.xLinesToolStripMenuItem,
            this.referencePlaneToolStripMenuItem,
            this.referenceDirectionToolStripMenuItem,
            this.pointsToolStripMenuItem});
      this.editToolStripMenuItem.Name = "editToolStripMenuItem";
      this.editToolStripMenuItem.Size = new System.Drawing.Size(47, 24);
      this.editToolStripMenuItem.Text = "Edit";
      // 
      // xLinesToolStripMenuItem
      // 
      this.xLinesToolStripMenuItem.Name = "xLinesToolStripMenuItem";
      this.xLinesToolStripMenuItem.Size = new System.Drawing.Size(215, 26);
      this.xLinesToolStripMenuItem.Text = "Pick XYZ Lines";
      this.xLinesToolStripMenuItem.Click += new System.EventHandler(this.xLinesToolStripMenuItem_Click);
      // 
      // referencePlaneToolStripMenuItem
      // 
      this.referencePlaneToolStripMenuItem.Name = "referencePlaneToolStripMenuItem";
      this.referencePlaneToolStripMenuItem.Size = new System.Drawing.Size(215, 26);
      this.referencePlaneToolStripMenuItem.Text = "Reference Plane";
      this.referencePlaneToolStripMenuItem.Click += new System.EventHandler(this.referencePlaneToolStripMenuItem_Click);
      // 
      // referenceDirectionToolStripMenuItem
      // 
      this.referenceDirectionToolStripMenuItem.Name = "referenceDirectionToolStripMenuItem";
      this.referenceDirectionToolStripMenuItem.Size = new System.Drawing.Size(215, 26);
      this.referenceDirectionToolStripMenuItem.Text = "Reference Direction";
      // 
      // pointsToolStripMenuItem
      // 
      this.pointsToolStripMenuItem.Name = "pointsToolStripMenuItem";
      this.pointsToolStripMenuItem.Size = new System.Drawing.Size(215, 26);
      this.pointsToolStripMenuItem.Text = "Points";
      // 
      // calculationToolStripMenuItem
      // 
      this.calculationToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.xVPointToolStripMenuItem,
            this.homographyToolStripMenuItem,
            this.alphaToolStripMenuItem,
            this.sceneToImageToolStripMenuItem});
      this.calculationToolStripMenuItem.Name = "calculationToolStripMenuItem";
      this.calculationToolStripMenuItem.Size = new System.Drawing.Size(49, 24);
      this.calculationToolStripMenuItem.Text = "Calc";
      // 
      // xVPointToolStripMenuItem
      // 
      this.xVPointToolStripMenuItem.Name = "xVPointToolStripMenuItem";
      this.xVPointToolStripMenuItem.Size = new System.Drawing.Size(220, 26);
      this.xVPointToolStripMenuItem.Text = "Vanishing Points XYZ";
      this.xVPointToolStripMenuItem.Click += new System.EventHandler(this.xVPointToolStripMenuItem_Click);
      // 
      // homographyToolStripMenuItem
      // 
      this.homographyToolStripMenuItem.Name = "homographyToolStripMenuItem";
      this.homographyToolStripMenuItem.Size = new System.Drawing.Size(220, 26);
      this.homographyToolStripMenuItem.Text = "Homography";
      this.homographyToolStripMenuItem.Click += new System.EventHandler(this.homographyToolStripMenuItem_Click);
      // 
      // alphaToolStripMenuItem
      // 
      this.alphaToolStripMenuItem.Name = "alphaToolStripMenuItem";
      this.alphaToolStripMenuItem.Size = new System.Drawing.Size(220, 26);
      this.alphaToolStripMenuItem.Text = "Alpha";
      // 
      // sceneToImageToolStripMenuItem
      // 
      this.sceneToImageToolStripMenuItem.Name = "sceneToImageToolStripMenuItem";
      this.sceneToImageToolStripMenuItem.Size = new System.Drawing.Size(220, 26);
      this.sceneToImageToolStripMenuItem.Text = "Scene to Image";
      // 
      // menuStrip
      // 
      this.menuStrip.ImageScalingSize = new System.Drawing.Size(20, 20);
      this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.editToolStripMenuItem,
            this.calculationToolStripMenuItem});
      this.menuStrip.Location = new System.Drawing.Point(0, 0);
      this.menuStrip.Name = "menuStrip";
      this.menuStrip.Size = new System.Drawing.Size(1091, 28);
      this.menuStrip.TabIndex = 0;
      this.menuStrip.Text = "menuStrip";
      // 
      // listBox
      // 
      this.listBox.FormattingEnabled = true;
      this.listBox.ItemHeight = 16;
      this.listBox.Location = new System.Drawing.Point(1, 533);
      this.listBox.Name = "listBox";
      this.listBox.Size = new System.Drawing.Size(1050, 116);
      this.listBox.TabIndex = 2;
      // 
      // openFileDialog
      // 
      this.openFileDialog.FileName = "openFileDialog";
      // 
      // Form1
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(1091, 652);
      this.Controls.Add(this.listBox);
      this.Controls.Add(this.pictureBox);
      this.Controls.Add(this.menuStrip);
      this.MainMenuStrip = this.menuStrip;
      this.Name = "Form1";
      this.Text = "Form1";
      ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).EndInit();
      this.menuStrip.ResumeLayout(false);
      this.menuStrip.PerformLayout();
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.PictureBox pictureBox;
    private System.Windows.Forms.ListBox listBox;
    private System.Windows.Forms.MenuStrip menuStrip1;
    private System.Windows.Forms.ToolStripMenuItem sceneToImageToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem alphaToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem homographyToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem calculationToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem pointsToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem referenceDirectionToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem referencePlaneToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem xLinesToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem editToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
    private System.Windows.Forms.MenuStrip menuStrip;
    private System.Windows.Forms.OpenFileDialog openFileDialog;
    private System.Windows.Forms.ToolStripMenuItem xVPointToolStripMenuItem;
  }
}

