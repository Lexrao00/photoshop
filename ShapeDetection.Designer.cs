namespace PhotoShopss
{
    partial class ShapeDetection
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
            components = new System.ComponentModel.Container();
            pictureBoxProcessed = new PictureBox();
            btnDetect = new Button();
            listResults = new ListView();
            labelCircle = new Label();
            labelRectangle = new Label();
            labelTriangle = new Label();
            imageList1 = new ImageList(components);
            labelSquare = new Label();
            btnDetectColor = new Button();
            listViewColor = new ListView();
            labelBlue = new Label();
            labelRed = new Label();
            labelYellow = new Label();
            labelGreen = new Label();
            ((System.ComponentModel.ISupportInitialize)pictureBoxProcessed).BeginInit();
            SuspendLayout();
            // 
            // pictureBoxProcessed
            // 
            pictureBoxProcessed.BorderStyle = BorderStyle.FixedSingle;
            pictureBoxProcessed.Location = new Point(30, 35);
            pictureBoxProcessed.Margin = new Padding(4, 5, 4, 5);
            pictureBoxProcessed.Name = "pictureBoxProcessed";
            pictureBoxProcessed.Size = new Size(521, 353);
            pictureBoxProcessed.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBoxProcessed.TabIndex = 1;
            pictureBoxProcessed.TabStop = false;
            // 
            // btnDetect
            // 
            btnDetect.Location = new Point(558, 25);
            btnDetect.Margin = new Padding(4, 5, 4, 5);
            btnDetect.Name = "btnDetect";
            btnDetect.Size = new Size(133, 46);
            btnDetect.TabIndex = 3;
            btnDetect.Text = "Detect Shapes";
            btnDetect.UseVisualStyleBackColor = true;
            // 
            // listResults
            // 
            listResults.Location = new Point(559, 210);
            listResults.Margin = new Padding(4, 5, 4, 5);
            listResults.Name = "listResults";
            listResults.Size = new Size(547, 229);
            listResults.TabIndex = 4;
            listResults.UseCompatibleStateImageBehavior = false;
            listResults.View = View.Details;
            // 
            // labelCircle
            // 
            labelCircle.AutoSize = true;
            labelCircle.Font = new Font("Microsoft Sans Serif", 13.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            labelCircle.Location = new Point(558, 89);
            labelCircle.Name = "labelCircle";
            labelCircle.Size = new Size(239, 29);
            labelCircle.TabIndex = 5;
            labelCircle.Text = "Number of Circle: 0";
            labelCircle.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // labelRectangle
            // 
            labelRectangle.AutoSize = true;
            labelRectangle.Font = new Font("Microsoft Sans Serif", 13.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            labelRectangle.Location = new Point(558, 118);
            labelRectangle.Name = "labelRectangle";
            labelRectangle.Size = new Size(288, 29);
            labelRectangle.TabIndex = 6;
            labelRectangle.Text = "Number of Rectangle: 0";
            labelRectangle.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // labelTriangle
            // 
            labelTriangle.AutoSize = true;
            labelTriangle.Font = new Font("Microsoft Sans Serif", 13.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            labelTriangle.Location = new Point(558, 147);
            labelTriangle.Name = "labelTriangle";
            labelTriangle.Size = new Size(268, 29);
            labelTriangle.TabIndex = 7;
            labelTriangle.Text = "Number of Triangle: 0";
            labelTriangle.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // imageList1
            // 
            imageList1.ColorDepth = ColorDepth.Depth8Bit;
            imageList1.ImageSize = new Size(16, 16);
            imageList1.TransparentColor = Color.Transparent;
            // 
            // labelSquare
            // 
            labelSquare.AutoSize = true;
            labelSquare.Font = new Font("Microsoft Sans Serif", 13.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            labelSquare.Location = new Point(558, 176);
            labelSquare.Name = "labelSquare";
            labelSquare.Size = new Size(254, 29);
            labelSquare.TabIndex = 11;
            labelSquare.Text = "Number of Square: 0";
            labelSquare.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // btnDetectColor
            // 
            btnDetectColor.Location = new Point(826, 25);
            btnDetectColor.Margin = new Padding(4, 5, 4, 5);
            btnDetectColor.Name = "btnDetectColor";
            btnDetectColor.Size = new Size(133, 46);
            btnDetectColor.TabIndex = 12;
            btnDetectColor.Text = "Detect Colors";
            btnDetectColor.UseVisualStyleBackColor = true;
            btnDetectColor.Click += btnDetectColor_Click;
            // 
            // listViewColor
            // 
            listViewColor.Location = new Point(559, 449);
            listViewColor.Margin = new Padding(4, 5, 4, 5);
            listViewColor.Name = "listViewColor";
            listViewColor.Size = new Size(547, 229);
            listViewColor.TabIndex = 14;
            listViewColor.UseCompatibleStateImageBehavior = false;
            listViewColor.View = View.Details;
            // 
            // labelBlue
            // 
            labelBlue.AutoSize = true;
            labelBlue.Font = new Font("Microsoft Sans Serif", 13.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            labelBlue.Location = new Point(851, 89);
            labelBlue.Name = "labelBlue";
            labelBlue.Size = new Size(223, 29);
            labelBlue.TabIndex = 15;
            labelBlue.Text = "Number of Blue: 0";
            labelBlue.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // labelRed
            // 
            labelRed.AutoSize = true;
            labelRed.Font = new Font("Microsoft Sans Serif", 13.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            labelRed.Location = new Point(851, 118);
            labelRed.Name = "labelRed";
            labelRed.Size = new Size(218, 29);
            labelRed.TabIndex = 16;
            labelRed.Text = "Number of Red: 0";
            labelRed.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // labelYellow
            // 
            labelYellow.AutoSize = true;
            labelYellow.Font = new Font("Microsoft Sans Serif", 13.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            labelYellow.Location = new Point(851, 147);
            labelYellow.Name = "labelYellow";
            labelYellow.Size = new Size(250, 29);
            labelYellow.TabIndex = 17;
            labelYellow.Text = "Number of Yellow: 0";
            labelYellow.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // labelGreen
            // 
            labelGreen.AutoSize = true;
            labelGreen.Font = new Font("Microsoft Sans Serif", 13.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            labelGreen.Location = new Point(851, 176);
            labelGreen.Name = "labelGreen";
            labelGreen.Size = new Size(242, 29);
            labelGreen.TabIndex = 18;
            labelGreen.Text = "Number of Green: 0";
            labelGreen.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // ShapeDetection
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1260, 1000);
            Controls.Add(labelGreen);
            Controls.Add(labelYellow);
            Controls.Add(labelRed);
            Controls.Add(labelBlue);
            Controls.Add(listViewColor);
            Controls.Add(btnDetectColor);
            Controls.Add(labelSquare);
            Controls.Add(labelTriangle);
            Controls.Add(labelRectangle);
            Controls.Add(labelCircle);
            Controls.Add(listResults);
            Controls.Add(btnDetect);
            Controls.Add(pictureBoxProcessed);
            Margin = new Padding(4, 5, 4, 5);
            Name = "ShapeDetection";
            Text = "Shape and Color Detection";
            ((System.ComponentModel.ISupportInitialize)pictureBoxProcessed).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private System.Windows.Forms.PictureBox pictureBoxProcessed;
        private System.Windows.Forms.Button btnDetect;
        private System.Windows.Forms.ListView listResults;
        private System.Windows.Forms.Label labelCircle;
        private System.Windows.Forms.Label labelRectangle;
        private System.Windows.Forms.Label labelTriangle;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.Label labelSquare;
        private Button btnDetectColor;
        private ListView listViewColor;
        private Label labelBlue;
        private Label labelRed;
        private Label labelYellow;
        private Label labelGreen;
    }
}