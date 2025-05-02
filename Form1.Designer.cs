namespace PhotoShopss
{
    partial class Form1
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            btnLoad = new Button();
            btnArrow = new Button();
            btnGrayscale = new Button();
            btnSave = new Button();
            sliderGrayscale = new TrackBar();
            pictureBox1 = new PictureBox();
            pictureBox2 = new PictureBox();
            trackBarGreen = new TrackBar();
            trackBarBlue = new TrackBar();
            labelRed = new Label();
            labelGreen = new Label();
            labelBlue = new Label();
            menuStrip1 = new MenuStrip();
            fliterToolStripMenuItem = new ToolStripMenuItem();
            toolStripMenuItem1 = new ToolStripMenuItem();
            sepiaToolStripMenuItem = new ToolStripMenuItem();
            frozenToolStripMenuItem = new ToolStripMenuItem();
            flashToolStripMenuItem = new ToolStripMenuItem();
            bWToolStripMenuItem = new ToolStripMenuItem();
            kakaoToolStripMenuItem = new ToolStripMenuItem();
            winterToolStripMenuItem = new ToolStripMenuItem();
            pineToolStripMenuItem = new ToolStripMenuItem();
            negativeToolStripMenuItem = new ToolStripMenuItem();
            cobaltToolStripMenuItem = new ToolStripMenuItem();
            noneToolStripMenuItem = new ToolStripMenuItem();
            detectionToolStripMenuItem = new ToolStripMenuItem();
            geometricShapeDetectionToolStripMenuItem = new ToolStripMenuItem();
            segmentationToolStripMenuItem = new ToolStripMenuItem();
            semanticSegmentationToolStripMenuItem = new ToolStripMenuItem();
            panopticSegmentationToolStripMenuItem = new ToolStripMenuItem();
            convolutionToolStripMenuItem = new ToolStripMenuItem();
            smoothToolStripMenuItem = new ToolStripMenuItem();
            gaussianBlurToolStripMenuItem = new ToolStripMenuItem();
            meanRemovalToolStripMenuItem = new ToolStripMenuItem();
            sharpenToolStripMenuItem = new ToolStripMenuItem();
            embossToolStripMenuItem = new ToolStripMenuItem();
            btnRotate = new Button();
            btnMirror = new Button();
            btnAdjust = new Button();
            trackBarRed = new TrackBar();
            btnReset = new Button();
            trackBarBrightness = new TrackBar();
            labelBrightness = new Label();
            button1 = new Button();
            btnBinaryImage = new Button();
            trackBarOpacity = new TrackBar();
            trackBarContrast = new TrackBar();
            trackBarSaturation = new TrackBar();
            trackBarBlur = new TrackBar();
            labelOpacity = new Label();
            labelContrast = new Label();
            labelSaturation = new Label();
            labelBlur = new Label();
            btnThresholding = new Button();
            btnAdaptiveBinarization = new Button();
            pictureBoxHistogram = new PictureBox();
            btnHistogram = new Button();
            labelHistogram = new Label();
            label1 = new Label();
            labelGrayscale = new Label();
            trackBarWhite = new TrackBar();
            lblWhiteThreshold = new Label();
            trackBarBlack = new TrackBar();
            lblBlackThreshold = new Label();
            trackBarThreshold = new TrackBar();
            labelThreshold = new Label();
            trackBarRotate = new TrackBar();
            btnTranslation = new Button();
            textBoxTranslation = new TextBox();
            btnProjection = new Button();
            btnHorizontal = new Button();
            btnVertical = new Button();
            label2 = new Label();
            btnSmooth = new Button();
            btnGaussianBlur = new Button();
            btnMeanRemoval = new Button();
            btnSharpen = new Button();
            btnEmboss = new Button();
            ((System.ComponentModel.ISupportInitialize)sliderGrayscale).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)trackBarGreen).BeginInit();
            ((System.ComponentModel.ISupportInitialize)trackBarBlue).BeginInit();
            menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)trackBarRed).BeginInit();
            ((System.ComponentModel.ISupportInitialize)trackBarBrightness).BeginInit();
            ((System.ComponentModel.ISupportInitialize)trackBarOpacity).BeginInit();
            ((System.ComponentModel.ISupportInitialize)trackBarContrast).BeginInit();
            ((System.ComponentModel.ISupportInitialize)trackBarSaturation).BeginInit();
            ((System.ComponentModel.ISupportInitialize)trackBarBlur).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBoxHistogram).BeginInit();
            ((System.ComponentModel.ISupportInitialize)trackBarWhite).BeginInit();
            ((System.ComponentModel.ISupportInitialize)trackBarBlack).BeginInit();
            ((System.ComponentModel.ISupportInitialize)trackBarThreshold).BeginInit();
            ((System.ComponentModel.ISupportInitialize)trackBarRotate).BeginInit();
            SuspendLayout();
            // 
            // btnLoad
            // 
            btnLoad.Location = new Point(0, 93);
            btnLoad.Name = "btnLoad";
            btnLoad.Size = new Size(100, 40);
            btnLoad.TabIndex = 0;
            btnLoad.Text = "Load Image";
            btnLoad.Click += btnLoad_Click;
            // 
            // btnArrow
            // 
            btnArrow.Location = new Point(459, 175);
            btnArrow.Name = "btnArrow";
            btnArrow.Size = new Size(50, 50);
            btnArrow.TabIndex = 1;
            btnArrow.Text = "→";
            btnArrow.Click += btnArrow_Click;
            // 
            // btnGrayscale
            // 
            btnGrayscale.Location = new Point(915, 323);
            btnGrayscale.Name = "btnGrayscale";
            btnGrayscale.Size = new Size(114, 40);
            btnGrayscale.TabIndex = 2;
            btnGrayscale.Text = "Grayscale";
            btnGrayscale.Click += btnGrayscale_Click;
            // 
            // btnSave
            // 
            btnSave.Location = new Point(0, 139);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(100, 40);
            btnSave.TabIndex = 3;
            btnSave.Text = "Save";
            btnSave.Click += btnSave_Click;
            // 
            // sliderGrayscale
            // 
            sliderGrayscale.Location = new Point(1024, 343);
            sliderGrayscale.Maximum = 100;
            sliderGrayscale.Name = "sliderGrayscale";
            sliderGrayscale.Size = new Size(159, 56);
            sliderGrayscale.TabIndex = 4;
            sliderGrayscale.Value = 100;
            sliderGrayscale.Visible = false;
            sliderGrayscale.Scroll += sliderGrayscale_Scroll;
            // 
            // pictureBox1
            // 
            pictureBox1.BorderStyle = BorderStyle.FixedSingle;
            pictureBox1.Location = new Point(106, 89);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(330, 287);
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox1.TabIndex = 5;
            pictureBox1.TabStop = false;
            pictureBox1.Click += pictureBox1_Click;
            // 
            // pictureBox2
            // 
            pictureBox2.BorderStyle = BorderStyle.FixedSingle;
            pictureBox2.Location = new Point(531, 89);
            pictureBox2.Name = "pictureBox2";
            pictureBox2.Size = new Size(331, 287);
            pictureBox2.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox2.TabIndex = 6;
            pictureBox2.TabStop = false;
            pictureBox2.Click += pictureBox2_Click_1;
            // 
            // trackBarGreen
            // 
            trackBarGreen.Location = new Point(38, 604);
            trackBarGreen.Maximum = 255;
            trackBarGreen.Minimum = -255;
            trackBarGreen.Name = "trackBarGreen";
            trackBarGreen.Size = new Size(158, 56);
            trackBarGreen.TabIndex = 8;
            trackBarGreen.TickFrequency = 10;
            trackBarGreen.TickStyle = TickStyle.None;
            trackBarGreen.Scroll += trackBarGreen_Scroll;
            // 
            // trackBarBlue
            // 
            trackBarBlue.Location = new Point(38, 666);
            trackBarBlue.Maximum = 255;
            trackBarBlue.Minimum = -255;
            trackBarBlue.Name = "trackBarBlue";
            trackBarBlue.Size = new Size(158, 56);
            trackBarBlue.TabIndex = 9;
            trackBarBlue.TickStyle = TickStyle.None;
            trackBarBlue.Scroll += trackBarBlue_Scroll;
            // 
            // labelRed
            // 
            labelRed.AutoSize = true;
            labelRed.BackColor = Color.Red;
            labelRed.Location = new Point(17, 545);
            labelRed.Name = "labelRed";
            labelRed.Size = new Size(18, 20);
            labelRed.TabIndex = 10;
            labelRed.Text = "R";
            labelRed.Click += label1_Click;
            // 
            // labelGreen
            // 
            labelGreen.AutoSize = true;
            labelGreen.BackColor = Color.Green;
            labelGreen.Location = new Point(17, 604);
            labelGreen.Name = "labelGreen";
            labelGreen.Size = new Size(19, 20);
            labelGreen.TabIndex = 11;
            labelGreen.Text = "G";
            labelGreen.Click += label2_Click;
            // 
            // labelBlue
            // 
            labelBlue.AutoSize = true;
            labelBlue.BackColor = Color.Blue;
            labelBlue.Location = new Point(17, 669);
            labelBlue.Name = "labelBlue";
            labelBlue.Size = new Size(18, 20);
            labelBlue.TabIndex = 12;
            labelBlue.Text = "B";
            labelBlue.Click += label3_Click;
            // 
            // menuStrip1
            // 
            menuStrip1.ImageScalingSize = new Size(20, 20);
            menuStrip1.Items.AddRange(new ToolStripItem[] { fliterToolStripMenuItem, detectionToolStripMenuItem, segmentationToolStripMenuItem, convolutionToolStripMenuItem });
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Size = new Size(1374, 28);
            menuStrip1.TabIndex = 13;
            menuStrip1.Text = "menuStrip1";
            menuStrip1.ItemClicked += menuStrip1_ItemClicked;
            // 
            // fliterToolStripMenuItem
            // 
            fliterToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { toolStripMenuItem1, sepiaToolStripMenuItem, frozenToolStripMenuItem, flashToolStripMenuItem, bWToolStripMenuItem, kakaoToolStripMenuItem, winterToolStripMenuItem, pineToolStripMenuItem, negativeToolStripMenuItem, cobaltToolStripMenuItem, noneToolStripMenuItem });
            fliterToolStripMenuItem.Name = "fliterToolStripMenuItem";
            fliterToolStripMenuItem.Size = new Size(56, 24);
            fliterToolStripMenuItem.Text = "Fliter";
            fliterToolStripMenuItem.Click += fliterToolStripMenuItem_Click;
            // 
            // toolStripMenuItem1
            // 
            toolStripMenuItem1.Name = "toolStripMenuItem1";
            toolStripMenuItem1.Size = new Size(152, 26);
            toolStripMenuItem1.Text = "Fog";
            toolStripMenuItem1.Click += toolStripMenuItem1_Click;
            // 
            // sepiaToolStripMenuItem
            // 
            sepiaToolStripMenuItem.Name = "sepiaToolStripMenuItem";
            sepiaToolStripMenuItem.Size = new Size(152, 26);
            sepiaToolStripMenuItem.Text = "Sepia";
            sepiaToolStripMenuItem.Click += sepiaToolStripMenuItem_Click;
            // 
            // frozenToolStripMenuItem
            // 
            frozenToolStripMenuItem.Name = "frozenToolStripMenuItem";
            frozenToolStripMenuItem.Size = new Size(152, 26);
            frozenToolStripMenuItem.Text = "Frozen";
            frozenToolStripMenuItem.Click += frozenToolStripMenuItem_Click;
            // 
            // flashToolStripMenuItem
            // 
            flashToolStripMenuItem.Name = "flashToolStripMenuItem";
            flashToolStripMenuItem.Size = new Size(152, 26);
            flashToolStripMenuItem.Text = "Flash";
            flashToolStripMenuItem.Click += flashToolStripMenuItem_Click;
            // 
            // bWToolStripMenuItem
            // 
            bWToolStripMenuItem.Name = "bWToolStripMenuItem";
            bWToolStripMenuItem.Size = new Size(152, 26);
            bWToolStripMenuItem.Text = "B/W";
            bWToolStripMenuItem.Click += bWToolStripMenuItem_Click;
            // 
            // kakaoToolStripMenuItem
            // 
            kakaoToolStripMenuItem.Name = "kakaoToolStripMenuItem";
            kakaoToolStripMenuItem.Size = new Size(152, 26);
            kakaoToolStripMenuItem.Text = "Kakao";
            kakaoToolStripMenuItem.Click += kakaoToolStripMenuItem_Click;
            // 
            // winterToolStripMenuItem
            // 
            winterToolStripMenuItem.Name = "winterToolStripMenuItem";
            winterToolStripMenuItem.Size = new Size(152, 26);
            winterToolStripMenuItem.Text = "Winter";
            winterToolStripMenuItem.Click += winterToolStripMenuItem_Click;
            // 
            // pineToolStripMenuItem
            // 
            pineToolStripMenuItem.Name = "pineToolStripMenuItem";
            pineToolStripMenuItem.Size = new Size(152, 26);
            pineToolStripMenuItem.Text = "Pine";
            pineToolStripMenuItem.Click += pineToolStripMenuItem_Click;
            // 
            // negativeToolStripMenuItem
            // 
            negativeToolStripMenuItem.Name = "negativeToolStripMenuItem";
            negativeToolStripMenuItem.Size = new Size(152, 26);
            negativeToolStripMenuItem.Text = "Negative";
            negativeToolStripMenuItem.Click += negativeToolStripMenuItem_Click;
            // 
            // cobaltToolStripMenuItem
            // 
            cobaltToolStripMenuItem.Name = "cobaltToolStripMenuItem";
            cobaltToolStripMenuItem.Size = new Size(152, 26);
            cobaltToolStripMenuItem.Text = "Cobalt";
            cobaltToolStripMenuItem.Click += cobaltToolStripMenuItem_Click;
            // 
            // noneToolStripMenuItem
            // 
            noneToolStripMenuItem.Name = "noneToolStripMenuItem";
            noneToolStripMenuItem.Size = new Size(152, 26);
            noneToolStripMenuItem.Text = "None";
            noneToolStripMenuItem.Click += noneToolStripMenuItem_Click;
            // 
            // detectionToolStripMenuItem
            // 
            detectionToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { geometricShapeDetectionToolStripMenuItem });
            detectionToolStripMenuItem.Name = "detectionToolStripMenuItem";
            detectionToolStripMenuItem.Size = new Size(88, 24);
            detectionToolStripMenuItem.Text = "Detection";
            // 
            // geometricShapeDetectionToolStripMenuItem
            // 
            geometricShapeDetectionToolStripMenuItem.Name = "geometricShapeDetectionToolStripMenuItem";
            geometricShapeDetectionToolStripMenuItem.Size = new Size(157, 26);
            geometricShapeDetectionToolStripMenuItem.Text = "Detection";
            geometricShapeDetectionToolStripMenuItem.Click += geometricShapeDetectionToolStripMenuItem_Click_1;
            // 
            // segmentationToolStripMenuItem
            // 
            segmentationToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { semanticSegmentationToolStripMenuItem, panopticSegmentationToolStripMenuItem });
            segmentationToolStripMenuItem.Name = "segmentationToolStripMenuItem";
            segmentationToolStripMenuItem.Size = new Size(116, 24);
            segmentationToolStripMenuItem.Text = "Segmentation";
            // 
            // semanticSegmentationToolStripMenuItem
            // 
            semanticSegmentationToolStripMenuItem.Name = "semanticSegmentationToolStripMenuItem";
            semanticSegmentationToolStripMenuItem.Size = new Size(250, 26);
            semanticSegmentationToolStripMenuItem.Text = "Semantic Segmentation";
            semanticSegmentationToolStripMenuItem.Click += semanticSegmentationToolStripMenuItem_Click_1;
            // 
            // panopticSegmentationToolStripMenuItem
            // 
            panopticSegmentationToolStripMenuItem.Name = "panopticSegmentationToolStripMenuItem";
            panopticSegmentationToolStripMenuItem.Size = new Size(250, 26);
            panopticSegmentationToolStripMenuItem.Text = "Panoptic Segmentation";
            panopticSegmentationToolStripMenuItem.Click += panopticSegmentationToolStripMenuItem_Click_1;
            // 
            // convolutionToolStripMenuItem
            // 
            convolutionToolStripMenuItem.Name = "convolutionToolStripMenuItem";
            convolutionToolStripMenuItem.Size = new Size(14, 24);
            // 
            // smoothToolStripMenuItem
            // 
            smoothToolStripMenuItem.Name = "smoothToolStripMenuItem";
            smoothToolStripMenuItem.Size = new Size(32, 19);
            // 
            // gaussianBlurToolStripMenuItem
            // 
            gaussianBlurToolStripMenuItem.Name = "gaussianBlurToolStripMenuItem";
            gaussianBlurToolStripMenuItem.Size = new Size(32, 19);
            // 
            // meanRemovalToolStripMenuItem
            // 
            meanRemovalToolStripMenuItem.Name = "meanRemovalToolStripMenuItem";
            meanRemovalToolStripMenuItem.Size = new Size(32, 19);
            // 
            // sharpenToolStripMenuItem
            // 
            sharpenToolStripMenuItem.Name = "sharpenToolStripMenuItem";
            sharpenToolStripMenuItem.Size = new Size(32, 19);
            // 
            // embossToolStripMenuItem
            // 
            embossToolStripMenuItem.Name = "embossToolStripMenuItem";
            embossToolStripMenuItem.Size = new Size(32, 19);
            // 
            // btnRotate
            // 
            btnRotate.Location = new Point(915, 139);
            btnRotate.Name = "btnRotate";
            btnRotate.Size = new Size(114, 40);
            btnRotate.TabIndex = 15;
            btnRotate.Text = "Rotate";
            btnRotate.Click += btnRotate_Click;
            // 
            // btnMirror
            // 
            btnMirror.Location = new Point(915, 89);
            btnMirror.Name = "btnMirror";
            btnMirror.Size = new Size(114, 40);
            btnMirror.TabIndex = 16;
            btnMirror.Text = "Mirror";
            btnMirror.Click += btnMirror_Click;
            // 
            // btnAdjust
            // 
            btnAdjust.Location = new Point(0, 323);
            btnAdjust.Name = "btnAdjust";
            btnAdjust.Size = new Size(100, 53);
            btnAdjust.TabIndex = 18;
            btnAdjust.Text = "Adjust";
            btnAdjust.Click += btnAdjust_Click;
            // 
            // trackBarRed
            // 
            trackBarRed.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            trackBarRed.Location = new Point(38, 544);
            trackBarRed.Margin = new Padding(1);
            trackBarRed.Maximum = 255;
            trackBarRed.Minimum = -255;
            trackBarRed.Name = "trackBarRed";
            trackBarRed.RightToLeft = RightToLeft.No;
            trackBarRed.RightToLeftLayout = true;
            trackBarRed.Size = new Size(158, 56);
            trackBarRed.TabIndex = 19;
            trackBarRed.TickStyle = TickStyle.None;
            trackBarRed.Scroll += trackBarRed_Scroll;
            // 
            // btnReset
            // 
            btnReset.Location = new Point(0, 185);
            btnReset.Name = "btnReset";
            btnReset.Size = new Size(100, 40);
            btnReset.TabIndex = 20;
            btnReset.Text = "Reset";
            btnReset.Click += btnReset_Click;
            // 
            // trackBarBrightness
            // 
            trackBarBrightness.Location = new Point(634, 388);
            trackBarBrightness.Maximum = 100;
            trackBarBrightness.Minimum = -100;
            trackBarBrightness.Name = "trackBarBrightness";
            trackBarBrightness.Size = new Size(204, 56);
            trackBarBrightness.TabIndex = 4;
            trackBarBrightness.Scroll += TrackBar_Scroll;
            // 
            // labelBrightness
            // 
            labelBrightness.AutoSize = true;
            labelBrightness.ForeColor = SystemColors.ControlText;
            labelBrightness.Location = new Point(552, 403);
            labelBrightness.Name = "labelBrightness";
            labelBrightness.Size = new Size(77, 20);
            labelBrightness.TabIndex = 25;
            labelBrightness.Text = "Brightness";
            labelBrightness.Click += labelBrightness_Click;
            // 
            // button1
            // 
            button1.Location = new Point(0, 231);
            button1.Name = "button1";
            button1.Size = new Size(100, 40);
            button1.TabIndex = 31;
            button1.Text = "Undo";
            // 
            // btnBinaryImage
            // 
            btnBinaryImage.Location = new Point(915, 277);
            btnBinaryImage.Name = "btnBinaryImage";
            btnBinaryImage.Size = new Size(114, 40);
            btnBinaryImage.TabIndex = 32;
            btnBinaryImage.Text = "Binaryimage";
            btnBinaryImage.Click += btnBinaryImage_Click;
            // 
            // trackBarOpacity
            // 
            trackBarOpacity.Location = new Point(634, 440);
            trackBarOpacity.Maximum = 100;
            trackBarOpacity.Name = "trackBarOpacity";
            trackBarOpacity.Size = new Size(204, 56);
            trackBarOpacity.TabIndex = 3;
            trackBarOpacity.Scroll += TrackBar_Scroll;
            // 
            // trackBarContrast
            // 
            trackBarContrast.Location = new Point(634, 627);
            trackBarContrast.Maximum = 100;
            trackBarContrast.Minimum = -100;
            trackBarContrast.Name = "trackBarContrast";
            trackBarContrast.Size = new Size(204, 56);
            trackBarContrast.TabIndex = 5;
            trackBarContrast.Scroll += TrackBar_Scroll;
            // 
            // trackBarSaturation
            // 
            trackBarSaturation.Location = new Point(634, 513);
            trackBarSaturation.Maximum = 100;
            trackBarSaturation.Minimum = -100;
            trackBarSaturation.Name = "trackBarSaturation";
            trackBarSaturation.Size = new Size(204, 56);
            trackBarSaturation.TabIndex = 6;
            trackBarSaturation.Scroll += TrackBar_Scroll;
            // 
            // trackBarBlur
            // 
            trackBarBlur.Location = new Point(758, 604);
            trackBarBlur.Name = "trackBarBlur";
            trackBarBlur.Size = new Size(204, 56);
            trackBarBlur.TabIndex = 7;
            trackBarBlur.Scroll += TrackBar_Scroll;
            // 
            // labelOpacity
            // 
            labelOpacity.AutoSize = true;
            labelOpacity.ForeColor = SystemColors.ControlText;
            labelOpacity.Location = new Point(552, 462);
            labelOpacity.Name = "labelOpacity";
            labelOpacity.Size = new Size(60, 20);
            labelOpacity.TabIndex = 37;
            labelOpacity.Text = "Opacity";
            labelOpacity.Click += label1_Click_1;
            // 
            // labelContrast
            // 
            labelContrast.AutoSize = true;
            labelContrast.ForeColor = SystemColors.ControlText;
            labelContrast.Location = new Point(552, 635);
            labelContrast.Name = "labelContrast";
            labelContrast.Size = new Size(64, 20);
            labelContrast.TabIndex = 38;
            labelContrast.Text = "Contrast";
            labelContrast.Click += labelContrast_Click;
            // 
            // labelSaturation
            // 
            labelSaturation.AutoSize = true;
            labelSaturation.ForeColor = SystemColors.ControlText;
            labelSaturation.Location = new Point(552, 518);
            labelSaturation.Name = "labelSaturation";
            labelSaturation.Size = new Size(77, 20);
            labelSaturation.TabIndex = 39;
            labelSaturation.Text = "Saturation";
            // 
            // labelBlur
            // 
            labelBlur.AutoSize = true;
            labelBlur.ForeColor = SystemColors.ControlText;
            labelBlur.Location = new Point(552, 580);
            labelBlur.Name = "labelBlur";
            labelBlur.Size = new Size(35, 20);
            labelBlur.TabIndex = 40;
            labelBlur.Text = "Blur";
            // 
            // btnThresholding
            // 
            btnThresholding.Location = new Point(915, 231);
            btnThresholding.Name = "btnThresholding";
            btnThresholding.Size = new Size(114, 40);
            btnThresholding.TabIndex = 41;
            btnThresholding.Text = "Thresholding ";
            btnThresholding.Click += btnThresholding_Click;
            // 
            // btnAdaptiveBinarization
            // 
            btnAdaptiveBinarization.Location = new Point(915, 185);
            btnAdaptiveBinarization.Name = "btnAdaptiveBinarization";
            btnAdaptiveBinarization.Size = new Size(114, 40);
            btnAdaptiveBinarization.TabIndex = 42;
            btnAdaptiveBinarization.Text = "Adaptive Binarization";
            btnAdaptiveBinarization.Click += btnAdaptiveBinarization_Click;
            // 
            // pictureBoxHistogram
            // 
            pictureBoxHistogram.BorderStyle = BorderStyle.FixedSingle;
            pictureBoxHistogram.Location = new Point(235, 408);
            pictureBoxHistogram.Name = "pictureBoxHistogram";
            pictureBoxHistogram.Size = new Size(297, 252);
            pictureBoxHistogram.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBoxHistogram.TabIndex = 43;
            pictureBoxHistogram.TabStop = false;
            pictureBoxHistogram.Click += pictureBoxHistogram_Click;
            // 
            // btnHistogram
            // 
            btnHistogram.Location = new Point(0, 277);
            btnHistogram.Name = "btnHistogram";
            btnHistogram.Size = new Size(100, 40);
            btnHistogram.TabIndex = 44;
            btnHistogram.Text = "Histogram";
            btnHistogram.Click += btnHistogram_Click;
            // 
            // labelHistogram
            // 
            labelHistogram.AutoSize = true;
            labelHistogram.ForeColor = SystemColors.ControlText;
            labelHistogram.Location = new Point(235, 388);
            labelHistogram.Name = "labelHistogram";
            labelHistogram.Size = new Size(79, 20);
            labelHistogram.TabIndex = 45;
            labelHistogram.Text = "Histogram";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Microsoft Sans Serif", 7.8F, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Point, 0);
            label1.Location = new Point(438, 44);
            label1.Name = "label1";
            label1.Size = new Size(102, 16);
            label1.TabIndex = 46;
            label1.Text = "PHOTOSHOP";
            label1.Click += label1_Click_2;
            // 
            // labelGrayscale
            // 
            labelGrayscale.AutoSize = true;
            labelGrayscale.Location = new Point(1035, 333);
            labelGrayscale.Name = "labelGrayscale";
            labelGrayscale.Size = new Size(72, 20);
            labelGrayscale.TabIndex = 47;
            labelGrayscale.Text = "Grayscale";
            labelGrayscale.Click += labelGrayscale_Click;
            // 
            // trackBarWhite
            // 
            trackBarWhite.Location = new Point(1215, 192);
            trackBarWhite.Maximum = 255;
            trackBarWhite.Name = "trackBarWhite";
            trackBarWhite.Size = new Size(159, 56);
            trackBarWhite.TabIndex = 0;
            trackBarWhite.Value = 180;
            trackBarWhite.Scroll += trackBarWhite_Scroll;
            // 
            // lblWhiteThreshold
            // 
            lblWhiteThreshold.Location = new Point(1226, 175);
            lblWhiteThreshold.Name = "lblWhiteThreshold";
            lblWhiteThreshold.Size = new Size(100, 23);
            lblWhiteThreshold.TabIndex = 1;
            lblWhiteThreshold.Text = "WhiteT1";
            lblWhiteThreshold.Click += lblWhiteThreshold_Click;
            // 
            // trackBarBlack
            // 
            trackBarBlack.Location = new Point(1215, 277);
            trackBarBlack.Maximum = 255;
            trackBarBlack.Name = "trackBarBlack";
            trackBarBlack.Size = new Size(159, 56);
            trackBarBlack.TabIndex = 2;
            trackBarBlack.Value = 100;
            trackBarBlack.Scroll += trackBarBlack_Scroll;
            // 
            // lblBlackThreshold
            // 
            lblBlackThreshold.Location = new Point(1226, 251);
            lblBlackThreshold.Name = "lblBlackThreshold";
            lblBlackThreshold.Size = new Size(100, 23);
            lblBlackThreshold.TabIndex = 3;
            lblBlackThreshold.Text = "BlackT2";
            // 
            // trackBarThreshold
            // 
            trackBarThreshold.Location = new Point(1035, 231);
            trackBarThreshold.Maximum = 255;
            trackBarThreshold.Name = "trackBarThreshold";
            trackBarThreshold.Size = new Size(187, 56);
            trackBarThreshold.TabIndex = 49;
            trackBarThreshold.Value = 180;
            trackBarThreshold.Scroll += trackBarThreshold_Scroll;
            // 
            // labelThreshold
            // 
            labelThreshold.AutoSize = true;
            labelThreshold.Location = new Point(1048, 218);
            labelThreshold.Name = "labelThreshold";
            labelThreshold.Size = new Size(74, 20);
            labelThreshold.TabIndex = 50;
            labelThreshold.Text = "Threshold";
            // 
            // trackBarRotate
            // 
            trackBarRotate.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            trackBarRotate.Location = new Point(1048, 123);
            trackBarRotate.Margin = new Padding(1);
            trackBarRotate.Maximum = 360;
            trackBarRotate.Name = "trackBarRotate";
            trackBarRotate.RightToLeft = RightToLeft.No;
            trackBarRotate.RightToLeftLayout = true;
            trackBarRotate.Size = new Size(158, 56);
            trackBarRotate.TabIndex = 51;
            trackBarRotate.TickStyle = TickStyle.None;
            trackBarRotate.Scroll += trackBarRotate_Scroll;
            // 
            // btnTranslation
            // 
            btnTranslation.Location = new Point(915, 37);
            btnTranslation.Name = "btnTranslation";
            btnTranslation.Size = new Size(114, 40);
            btnTranslation.TabIndex = 52;
            btnTranslation.Text = "Translation";
            btnTranslation.Click += btnTranslation_Click;
            // 
            // textBoxTranslation
            // 
            textBoxTranslation.Location = new Point(1035, 44);
            textBoxTranslation.Name = "textBoxTranslation";
            textBoxTranslation.Size = new Size(125, 27);
            textBoxTranslation.TabIndex = 53;
            textBoxTranslation.TextChanged += textBoxTranslation_TextChanged;
            // 
            // btnProjection
            // 
            btnProjection.Location = new Point(0, 382);
            btnProjection.Name = "btnProjection";
            btnProjection.Size = new Size(100, 40);
            btnProjection.TabIndex = 54;
            btnProjection.Text = "Projection";
            btnProjection.Click += btnProjection_Click;
            // 
            // btnHorizontal
            // 
            btnHorizontal.Location = new Point(0, 428);
            btnHorizontal.Name = "btnHorizontal";
            btnHorizontal.Size = new Size(100, 40);
            btnHorizontal.TabIndex = 55;
            btnHorizontal.Text = "Horizontal";
            btnHorizontal.Click += btnHorizontal_Click;
            // 
            // btnVertical
            // 
            btnVertical.Location = new Point(106, 428);
            btnVertical.Name = "btnVertical";
            btnVertical.Size = new Size(100, 40);
            btnVertical.TabIndex = 56;
            btnVertical.Text = "Vertical";
            btnVertical.Click += btnVertical_Click;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.ForeColor = SystemColors.ControlText;
            label2.Location = new Point(915, 395);
            label2.Name = "label2";
            label2.Size = new Size(89, 20);
            label2.TabIndex = 57;
            label2.Text = "Convolution";
            // 
            // btnSmooth
            // 
            btnSmooth.Location = new Point(915, 428);
            btnSmooth.Name = "btnSmooth";
            btnSmooth.Size = new Size(114, 40);
            btnSmooth.TabIndex = 58;
            btnSmooth.Text = "Smooth";
            btnSmooth.Click += btnSmooth_Click;
            // 
            // btnGaussianBlur
            // 
            btnGaussianBlur.Location = new Point(915, 474);
            btnGaussianBlur.Name = "btnGaussianBlur";
            btnGaussianBlur.Size = new Size(114, 40);
            btnGaussianBlur.TabIndex = 59;
            btnGaussianBlur.Text = "Gaussion Blur";
            btnGaussianBlur.Click += btnGaussianBlur_Click;
            // 
            // btnMeanRemoval
            // 
            btnMeanRemoval.Location = new Point(915, 520);
            btnMeanRemoval.Name = "btnMeanRemoval";
            btnMeanRemoval.Size = new Size(114, 40);
            btnMeanRemoval.TabIndex = 60;
            btnMeanRemoval.Text = "Mean Removal";
            btnMeanRemoval.Click += btnMeanRemoval_Click;
            // 
            // btnSharpen
            // 
            btnSharpen.Location = new Point(1035, 428);
            btnSharpen.Name = "btnSharpen";
            btnSharpen.Size = new Size(114, 40);
            btnSharpen.TabIndex = 61;
            btnSharpen.Text = "Sharpen";
            btnSharpen.Click += btnSharpen_Click;
            // 
            // btnEmboss
            // 
            btnEmboss.Location = new Point(1035, 474);
            btnEmboss.Name = "btnEmboss";
            btnEmboss.Size = new Size(114, 40);
            btnEmboss.TabIndex = 62;
            btnEmboss.Text = "Emboss";
            btnEmboss.Click += btnEmboss_Click;
            // 
            // Form1
            // 
            BackColor = SystemColors.GradientActiveCaption;
            ClientSize = new Size(1374, 712);
            Controls.Add(btnEmboss);
            Controls.Add(btnSharpen);
            Controls.Add(btnMeanRemoval);
            Controls.Add(btnGaussianBlur);
            Controls.Add(btnSmooth);
            Controls.Add(label2);
            Controls.Add(btnVertical);
            Controls.Add(btnHorizontal);
            Controls.Add(btnProjection);
            Controls.Add(textBoxTranslation);
            Controls.Add(btnTranslation);
            Controls.Add(trackBarRotate);
            Controls.Add(labelThreshold);
            Controls.Add(trackBarThreshold);
            Controls.Add(trackBarWhite);
            Controls.Add(lblWhiteThreshold);
            Controls.Add(trackBarBlack);
            Controls.Add(lblBlackThreshold);
            Controls.Add(labelGrayscale);
            Controls.Add(label1);
            Controls.Add(labelHistogram);
            Controls.Add(btnHistogram);
            Controls.Add(pictureBoxHistogram);
            Controls.Add(btnAdaptiveBinarization);
            Controls.Add(btnThresholding);
            Controls.Add(labelBlur);
            Controls.Add(labelSaturation);
            Controls.Add(labelContrast);
            Controls.Add(labelOpacity);
            Controls.Add(trackBarBlur);
            Controls.Add(trackBarSaturation);
            Controls.Add(trackBarContrast);
            Controls.Add(trackBarOpacity);
            Controls.Add(btnBinaryImage);
            Controls.Add(button1);
            Controls.Add(labelBrightness);
            Controls.Add(trackBarBrightness);
            Controls.Add(btnReset);
            Controls.Add(trackBarRed);
            Controls.Add(btnAdjust);
            Controls.Add(btnMirror);
            Controls.Add(btnRotate);
            Controls.Add(labelBlue);
            Controls.Add(labelGreen);
            Controls.Add(labelRed);
            Controls.Add(trackBarBlue);
            Controls.Add(trackBarGreen);
            Controls.Add(btnLoad);
            Controls.Add(btnArrow);
            Controls.Add(btnGrayscale);
            Controls.Add(btnSave);
            Controls.Add(sliderGrayscale);
            Controls.Add(pictureBox1);
            Controls.Add(pictureBox2);
            Controls.Add(menuStrip1);
            MainMenuStrip = menuStrip1;
            Name = "Form1";
            Text = "Photoshop";
            Load += Form1_Load;
            ((System.ComponentModel.ISupportInitialize)sliderGrayscale).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).EndInit();
            ((System.ComponentModel.ISupportInitialize)trackBarGreen).EndInit();
            ((System.ComponentModel.ISupportInitialize)trackBarBlue).EndInit();
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)trackBarRed).EndInit();
            ((System.ComponentModel.ISupportInitialize)trackBarBrightness).EndInit();
            ((System.ComponentModel.ISupportInitialize)trackBarOpacity).EndInit();
            ((System.ComponentModel.ISupportInitialize)trackBarContrast).EndInit();
            ((System.ComponentModel.ISupportInitialize)trackBarSaturation).EndInit();
            ((System.ComponentModel.ISupportInitialize)trackBarBlur).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBoxHistogram).EndInit();
            ((System.ComponentModel.ISupportInitialize)trackBarWhite).EndInit();
            ((System.ComponentModel.ISupportInitialize)trackBarBlack).EndInit();
            ((System.ComponentModel.ISupportInitialize)trackBarThreshold).EndInit();
            ((System.ComponentModel.ISupportInitialize)trackBarRotate).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        private System.Windows.Forms.Button btnLoad;
        private System.Windows.Forms.Button btnArrow;
        private System.Windows.Forms.Button btnGrayscale;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.TrackBar sliderGrayscale;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.TrackBar trackBarGreen;
        private System.Windows.Forms.TrackBar trackBarBlue;
        private System.Windows.Forms.Label labelRed;
        private System.Windows.Forms.Label labelGreen;
        private System.Windows.Forms.Label labelBlue;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fliterToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem sepiaToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem frozenToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem flashToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem bWToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem kakaoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem winterToolStripMenuItem;
        private System.Windows.Forms.Button btnRotate;
        private System.Windows.Forms.Button btnMirror;
        private System.Windows.Forms.Button btnAdjust;
        private System.Windows.Forms.TrackBar trackBarRed;
        private System.Windows.Forms.Button btnReset;
        private System.Windows.Forms.TrackBar trackBarBrightness;
        private System.Windows.Forms.Label labelBrightness;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button btnBinaryImage;
        private System.Windows.Forms.ToolStripMenuItem pineToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem negativeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem cobaltToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem noneToolStripMenuItem;
        private System.Windows.Forms.TrackBar trackBarOpacity;
        private System.Windows.Forms.TrackBar trackBarContrast;
        private System.Windows.Forms.TrackBar trackBarSaturation;
        private System.Windows.Forms.TrackBar trackBarBlur;
        private System.Windows.Forms.Label labelOpacity;
        private System.Windows.Forms.Label labelContrast;
        private System.Windows.Forms.Label labelSaturation;
        private System.Windows.Forms.Label labelBlur;
        private System.Windows.Forms.Button btnThresholding;
        private System.Windows.Forms.Button btnAdaptiveBinarization;
        private System.Windows.Forms.PictureBox pictureBoxHistogram;
        private System.Windows.Forms.Button btnHistogram;
        private System.Windows.Forms.Label labelHistogram;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label labelGrayscale;
        private System.Windows.Forms.TrackBar trackBarWhite;
        private System.Windows.Forms.TrackBar trackBarBlack;
        private System.Windows.Forms.Label lblWhiteThreshold;
        private System.Windows.Forms.Label lblBlackThreshold;
        private System.Windows.Forms.ToolStripMenuItem detectionToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem geometricShapeDetectionToolStripMenuItem;
        private ToolStripMenuItem segmentationToolStripMenuItem;
        private ToolStripMenuItem semanticSegmentationToolStripMenuItem;
        private ToolStripMenuItem panopticSegmentationToolStripMenuItem;
        private ToolStripMenuItem convolutionToolStripMenuItem;
        private ToolStripMenuItem smoothToolStripMenuItem;
        private ToolStripMenuItem gaussianBlurToolStripMenuItem;
        private ToolStripMenuItem meanRemovalToolStripMenuItem;
        private ToolStripMenuItem sharpenToolStripMenuItem;
        private ToolStripMenuItem embossToolStripMenuItem;
        private TrackBar trackBarThreshold;
        private Label labelThreshold;
        private TrackBar trackBarRotate;
        private Button btnTranslation;
        private TextBox textBoxTranslation;
        private Button btnProjection;
        private Button btnHorizontal;
        private Button btnVertical;
        private Label label2;
        private Button btnSmooth;
        private Button btnGaussianBlur;
        private Button btnMeanRemoval;
        private Button btnSharpen;
        private Button btnEmboss;
    }
}