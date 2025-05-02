using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Collections.Generic;
using System.Linq;
using Emgu.CV;
using Emgu.CV.Structure;
using Emgu.CV.CvEnum;

namespace PhotoShopss
{

    public partial class Form1 : Form
    {
        private Bitmap originalImage;
        private Bitmap processedImage;
        private Bitmap adjustedImage;
        private DateTime lastTrackBarUpdate = DateTime.MinValue;
        private const int DEBOUNCE_DELAY_MS = 50;
        private bool isGrayscaleMode = false; // Flag to track grayscale state
        private bool isBinaryMode = false;



        public Form1()
        {
            InitializeComponent();
            DoubleBuffered = true;
        }

      

        // Button click handlers for convolution filters


        // Kernel definitions for convolution filters

        

        // Load Image
        private void btnLoad_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp"
            };

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                originalImage = new Bitmap(openFileDialog.FileName);
                pictureBox1.Image = originalImage;
                processedImage = new Bitmap(originalImage);

                UpdateHistogram();
            }
        }

        private int Clamp(int value, int min, int max)
        {
            return Math.Min(Math.Max(value, min), max);
        }

        // Move Image to PictureBox2 (Arrow Button)
        private void btnArrow_Click(object sender, EventArgs e)
        {
            if (pictureBox1.Image != null)
            {
                pictureBox2.Image = pictureBox1.Image;
            }
            else
            {
                MessageBox.Show("Please load an image first.");
            }
        }

        // Convert to Grayscale
        private void btnGrayscale_Click(object sender, EventArgs e)
        {
            if (originalImage == null)
            {
                MessageBox.Show("Please load an image first.");
                return;
            }

            sliderGrayscale.Visible = true;
            labelGrayscale.Visible = true;
            isGrayscaleMode = true;

            processedImage = ConvertToGrayscale(originalImage, sliderGrayscale.Value / 100.0);
            pictureBox2.Image = processedImage;
        }

        // Optimized ConvertToGrayscale using LockBits
        private Bitmap ConvertToGrayscale(Bitmap original, double intensity)
        {
            Bitmap grayscaleImage = new Bitmap(original.Width, original.Height);
            BitmapData srcData = original.LockBits(new Rectangle(0, 0, original.Width, original.Height), ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);
            BitmapData dstData = grayscaleImage.LockBits(new Rectangle(0, 0, grayscaleImage.Width, grayscaleImage.Height), ImageLockMode.WriteOnly, PixelFormat.Format32bppArgb);

            int bytes = srcData.Stride * srcData.Height;
            byte[] srcBuffer = new byte[bytes];
            byte[] dstBuffer = new byte[bytes];

            Marshal.Copy(srcData.Scan0, srcBuffer, 0, bytes);

            for (int i = 0; i < bytes; i += 4)
            {
                byte b = srcBuffer[i];
                byte g = srcBuffer[i + 1];
                byte r = srcBuffer[i + 2];
                byte a = srcBuffer[i + 3];

                int grayValue = (int)((r * 0.299 + g * 0.587 + b * 0.114) * intensity);
                grayValue = Clamp(grayValue, 0, 255);

                dstBuffer[i] = (byte)grayValue;
                dstBuffer[i + 1] = (byte)grayValue;
                dstBuffer[i + 2] = (byte)grayValue;
                dstBuffer[i + 3] = a;
            }

            Marshal.Copy(dstBuffer, 0, dstData.Scan0, bytes);

            original.UnlockBits(srcData);
            grayscaleImage.UnlockBits(dstData);

            return grayscaleImage;
        }


        // Adjust Grayscale with Slider (Debounced)
        private void sliderGrayscale_Scroll(object sender, EventArgs e)
        {
            if (originalImage != null && Debounce())
            {
                processedImage = ConvertToGrayscale(originalImage, sliderGrayscale.Value / 100.0);
                pictureBox2.Image = processedImage;
                isGrayscaleMode = true;
            }
        }

        // Save Image
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (processedImage == null)
            {
                MessageBox.Show("No processed image to save.");
                return;
            }

            SaveFileDialog saveFileDialog = new SaveFileDialog
            {
                Filter = "PNG Image|*.png|JPEG Image|*.jpg|Bitmap Image|*.bmp"
            };

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                processedImage.Save(saveFileDialog.FileName);
                MessageBox.Show("Image saved successfully.");
            }
        }

        private void trackBarRed_Scroll(object sender, EventArgs e)
        {
            if (originalImage == null || !Debounce()) return;

            int redAdjustment = trackBarRed.Value;
            Bitmap baseImage = isGrayscaleMode ? processedImage : originalImage;
            processedImage = AdjustRedTone(baseImage, redAdjustment);
            pictureBox2.Image = processedImage;
        }

        // Optimized AdjustRedTone using LockBits
        private Bitmap AdjustRedTone(Bitmap original, int redLevel)
        {
            Bitmap adjustedImage = new Bitmap(original.Width, original.Height);
            BitmapData srcData = original.LockBits(new Rectangle(0, 0, original.Width, original.Height), ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);
            BitmapData dstData = adjustedImage.LockBits(new Rectangle(0, 0, adjustedImage.Width, adjustedImage.Height), ImageLockMode.WriteOnly, PixelFormat.Format32bppArgb);

            int bytes = srcData.Stride * srcData.Height;
            byte[] srcBuffer = new byte[bytes];
            byte[] dstBuffer = new byte[bytes];

            Marshal.Copy(srcData.Scan0, srcBuffer, 0, bytes);

            for (int i = 0; i < bytes; i += 4)
            {
                byte b = srcBuffer[i];
                byte g = srcBuffer[i + 1];
                byte r = srcBuffer[i + 2];
                byte a = srcBuffer[i + 3];

                int newRed = Clamp(r + redLevel, 0, 255);

                dstBuffer[i] = b;
                dstBuffer[i + 1] = g;
                dstBuffer[i + 2] = (byte)newRed;
                dstBuffer[i + 3] = a;
            }

            Marshal.Copy(dstBuffer, 0, dstData.Scan0, bytes);

            original.UnlockBits(srcData);
            adjustedImage.UnlockBits(dstData);

            return adjustedImage;
        }

        private void trackBarGreen_Scroll(object sender, EventArgs e)
        {
            if (originalImage == null || !Debounce()) return;

            int greenAdjustment = trackBarGreen.Value;
            Bitmap baseImage = isGrayscaleMode ? processedImage : originalImage;
            processedImage = AdjustGreenTone(baseImage, greenAdjustment);
            pictureBox2.Image = processedImage;
        }

        // Optimized AdjustGreenTone using LockBits
        private Bitmap AdjustGreenTone(Bitmap original, int greenLevel)
        {
            Bitmap adjustedImage = new Bitmap(original.Width, original.Height);
            BitmapData srcData = original.LockBits(new Rectangle(0, 0, original.Width, original.Height), ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);
            BitmapData dstData = adjustedImage.LockBits(new Rectangle(0, 0, adjustedImage.Width, adjustedImage.Height), ImageLockMode.WriteOnly, PixelFormat.Format32bppArgb);

            int bytes = srcData.Stride * srcData.Height;
            byte[] srcBuffer = new byte[bytes];
            byte[] dstBuffer = new byte[bytes];

            Marshal.Copy(srcData.Scan0, srcBuffer, 0, bytes);

            for (int i = 0; i < bytes; i += 4)
            {
                byte b = srcBuffer[i];
                byte g = srcBuffer[i + 1];
                byte r = srcBuffer[i + 2];
                byte a = srcBuffer[i + 3];

                int newGreen = Clamp(g + greenLevel, 0, 255);

                dstBuffer[i] = b;
                dstBuffer[i + 1] = (byte)newGreen;
                dstBuffer[i + 2] = r;
                dstBuffer[i + 3] = a;
            }

            Marshal.Copy(dstBuffer, 0, dstData.Scan0, bytes);

            original.UnlockBits(srcData);
            adjustedImage.UnlockBits(dstData);

            return adjustedImage;
        }

        private void trackBarBlue_Scroll(object sender, EventArgs e)
        {
            if (originalImage == null || !Debounce()) return;

            int blueAdjustment = trackBarBlue.Value;
            Bitmap baseImage = isGrayscaleMode ? processedImage : originalImage;
            processedImage = AdjustBlueTone(baseImage, blueAdjustment);
            pictureBox2.Image = processedImage;
        }

        // Optimized AdjustBlueTone using LockBits
        private Bitmap AdjustBlueTone(Bitmap original, int blueLevel)
        {
            Bitmap adjustedImage = new Bitmap(original.Width, original.Height);
            BitmapData srcData = original.LockBits(new Rectangle(0, 0, original.Width, original.Height), ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);
            BitmapData dstData = adjustedImage.LockBits(new Rectangle(0, 0, adjustedImage.Width, adjustedImage.Height), ImageLockMode.WriteOnly, PixelFormat.Format32bppArgb);

            int bytes = srcData.Stride * srcData.Height;
            byte[] srcBuffer = new byte[bytes];
            byte[] dstBuffer = new byte[bytes];

            Marshal.Copy(srcData.Scan0, srcBuffer, 0, bytes);

            for (int i = 0; i < bytes; i += 4)
            {
                byte b = srcBuffer[i];
                byte g = srcBuffer[i + 1];
                byte r = srcBuffer[i + 2];
                byte a = srcBuffer[i + 3];

                int newBlue = Clamp(b + blueLevel, 0, 255);

                dstBuffer[i] = (byte)newBlue;
                dstBuffer[i + 1] = g;
                dstBuffer[i + 2] = r;
                dstBuffer[i + 3] = a;
            }

            Marshal.Copy(dstBuffer, 0, dstData.Scan0, bytes);

            original.UnlockBits(srcData);
            adjustedImage.UnlockBits(dstData);

            return adjustedImage;
        }

        private void trackBarRGB_Scroll(object sender, EventArgs e)
        {
            if (originalImage == null || !Debounce()) return;

            int redAdjustment = trackBarRed.Value;
            int greenAdjustment = trackBarGreen.Value;
            int blueAdjustment = trackBarBlue.Value;

            Bitmap baseImage = isGrayscaleMode ? processedImage : originalImage;
            processedImage = AdjustRGB(baseImage, redAdjustment, greenAdjustment, blueAdjustment);
            pictureBox2.Image = processedImage;
        }

        // Optimized AdjustRGB using LockBits
        private Bitmap AdjustRGB(Bitmap original, int redLevel, int greenLevel, int blueLevel)
        {
            Bitmap adjustedImage = new Bitmap(original.Width, original.Height);
            BitmapData srcData = original.LockBits(new Rectangle(0, 0, original.Width, original.Height), ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);
            BitmapData dstData = adjustedImage.LockBits(new Rectangle(0, 0, adjustedImage.Width, adjustedImage.Height), ImageLockMode.WriteOnly, PixelFormat.Format32bppArgb);

            int bytes = srcData.Stride * srcData.Height;
            byte[] srcBuffer = new byte[bytes];
            byte[] dstBuffer = new byte[bytes];

            Marshal.Copy(srcData.Scan0, srcBuffer, 0, bytes);

            for (int i = 0; i < bytes; i += 4)
            {
                byte b = srcBuffer[i];
                byte g = srcBuffer[i + 1];
                byte r = srcBuffer[i + 2];
                byte a = srcBuffer[i + 3];

                int newRed = Clamp(r + redLevel, 0, 255);
                int newGreen = Clamp(g + greenLevel, 0, 255);
                int newBlue = Clamp(b + blueLevel, 0, 255);

                dstBuffer[i] = (byte)newBlue;
                dstBuffer[i + 1] = (byte)newGreen;
                dstBuffer[i + 2] = (byte)newRed;
                dstBuffer[i + 3] = a;
            }

            Marshal.Copy(dstBuffer, 0, dstData.Scan0, bytes);

            original.UnlockBits(srcData);
            adjustedImage.UnlockBits(dstData);

            return adjustedImage;
        }

        private Bitmap ApplySepia(Bitmap original)
        {
            Bitmap sepiaImage = new Bitmap(original.Width, original.Height);
            BitmapData srcData = original.LockBits(new Rectangle(0, 0, original.Width, original.Height), ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);
            BitmapData dstData = sepiaImage.LockBits(new Rectangle(0, 0, sepiaImage.Width, sepiaImage.Height), ImageLockMode.WriteOnly, PixelFormat.Format32bppArgb);

            int bytes = srcData.Stride * srcData.Height;
            byte[] srcBuffer = new byte[bytes];
            byte[] dstBuffer = new byte[bytes];

            Marshal.Copy(srcData.Scan0, srcBuffer, 0, bytes);

            for (int i = 0; i < bytes; i += 4)
            {
                byte b = srcBuffer[i];
                byte g = srcBuffer[i + 1];
                byte r = srcBuffer[i + 2];
                byte a = srcBuffer[i + 3];

                int newR = (int)(r * 0.393 + g * 0.769 + b * 0.189);
                int newG = (int)(r * 0.349 + g * 0.686 + b * 0.168);
                int newB = (int)(r * 0.272 + g * 0.534 + b * 0.131);

                dstBuffer[i] = (byte)Math.Min(255, newB);
                dstBuffer[i + 1] = (byte)Math.Min(255, newG);
                dstBuffer[i + 2] = (byte)Math.Min(255, newR);
                dstBuffer[i + 3] = a;
            }

            Marshal.Copy(dstBuffer, 0, dstData.Scan0, bytes);

            original.UnlockBits(srcData);
            sepiaImage.UnlockBits(dstData);

            return sepiaImage;
        }

        private Bitmap ApplyBlackWhite(Bitmap original)
        {
            Bitmap bwImage = new Bitmap(original.Width, original.Height);
            BitmapData srcData = original.LockBits(new Rectangle(0, 0, original.Width, original.Height), ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);
            BitmapData dstData = bwImage.LockBits(new Rectangle(0, 0, bwImage.Width, bwImage.Height), ImageLockMode.WriteOnly, PixelFormat.Format32bppArgb);

            int bytes = srcData.Stride * srcData.Height;
            byte[] srcBuffer = new byte[bytes];
            byte[] dstBuffer = new byte[bytes];

            Marshal.Copy(srcData.Scan0, srcBuffer, 0, bytes);

            for (int i = 0; i < bytes; i += 4)
            {
                byte b = srcBuffer[i];
                byte g = srcBuffer[i + 1];
                byte r = srcBuffer[i + 2];
                byte a = srcBuffer[i + 3];

                int avg = (r + g + b) / 3;

                dstBuffer[i] = (byte)avg;
                dstBuffer[i + 1] = (byte)avg;
                dstBuffer[i + 2] = (byte)avg;
                dstBuffer[i + 3] = a;
            }

            Marshal.Copy(dstBuffer, 0, dstData.Scan0, bytes);

            original.UnlockBits(srcData);
            bwImage.UnlockBits(dstData);

            return bwImage;
        }

        private Bitmap ApplyFrozen(Bitmap original)
        {
            Bitmap frozenImage = new Bitmap(original.Width, original.Height);
            BitmapData srcData = original.LockBits(new Rectangle(0, 0, original.Width, original.Height), ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);
            BitmapData dstData = frozenImage.LockBits(new Rectangle(0, 0, frozenImage.Width, frozenImage.Height), ImageLockMode.WriteOnly, PixelFormat.Format32bppArgb);

            int bytes = srcData.Stride * srcData.Height;
            byte[] srcBuffer = new byte[bytes];
            byte[] dstBuffer = new byte[bytes];

            Marshal.Copy(srcData.Scan0, srcBuffer, 0, bytes);

            for (int i = 0; i < bytes; i += 4)
            {
                byte b = srcBuffer[i];
                byte g = srcBuffer[i + 1];
                byte r = srcBuffer[i + 2];
                byte a = srcBuffer[i + 3];

                dstBuffer[i] = r;
                dstBuffer[i + 1] = g;
                dstBuffer[i + 2] = b;
                dstBuffer[i + 3] = a;
            }

            Marshal.Copy(dstBuffer, 0, dstData.Scan0, bytes);

            original.UnlockBits(srcData);
            frozenImage.UnlockBits(dstData);

            return frozenImage;
        }

        private void sepiaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (originalImage != null)
            {
                isGrayscaleMode = false;
                processedImage = ApplySepia(originalImage);
                pictureBox2.Image = processedImage;
            }
        }

        private void bWToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (originalImage != null)
            {
                isGrayscaleMode = true;
                processedImage = ApplyBlackWhite(originalImage);
                pictureBox2.Image = processedImage;
            }
        }

        private void frozenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (originalImage != null)
            {
                isGrayscaleMode = false;
                processedImage = ApplyFrozen(originalImage);
                pictureBox2.Image = processedImage;
            }
        }

        private Bitmap ApplyFog(Bitmap original)
        {
            Bitmap fogImage = new Bitmap(original.Width, original.Height);
            Random rand = new Random();
            BitmapData srcData = original.LockBits(new Rectangle(0, 0, original.Width, original.Height), ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);
            BitmapData dstData = fogImage.LockBits(new Rectangle(0, 0, fogImage.Width, fogImage.Height), ImageLockMode.WriteOnly, PixelFormat.Format32bppArgb);

            int bytes = srcData.Stride * srcData.Height;
            byte[] srcBuffer = new byte[bytes];
            byte[] dstBuffer = new byte[bytes];

            Marshal.Copy(srcData.Scan0, srcBuffer, 0, bytes);

            for (int i = 0; i < bytes; i += 4)
            {
                byte b = srcBuffer[i];
                byte g = srcBuffer[i + 1];
                byte r = srcBuffer[i + 2];
                byte a = srcBuffer[i + 3];

                int fogFactor = rand.Next(-30, 30);
                int newR = Clamp(r + fogFactor, 0, 255);
                int newG = Clamp(g + fogFactor, 0, 255);
                int newB = Clamp(b + fogFactor, 0, 255);

                dstBuffer[i] = (byte)newB;
                dstBuffer[i + 1] = (byte)newG;
                dstBuffer[i + 2] = (byte)newR;
                dstBuffer[i + 3] = a;
            }

            Marshal.Copy(dstBuffer, 0, dstData.Scan0, bytes);

            original.UnlockBits(srcData);
            fogImage.UnlockBits(dstData);

            return fogImage;
        }

        private Bitmap ApplyFlash(Bitmap original)
        {
            Bitmap flashImage = new Bitmap(original.Width, original.Height);
            BitmapData srcData = original.LockBits(new Rectangle(0, 0, original.Width, original.Height), ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);
            BitmapData dstData = flashImage.LockBits(new Rectangle(0, 0, flashImage.Width, flashImage.Height), ImageLockMode.WriteOnly, PixelFormat.Format32bppArgb);

            int bytes = srcData.Stride * srcData.Height;
            byte[] srcBuffer = new byte[bytes];
            byte[] dstBuffer = new byte[bytes];

            Marshal.Copy(srcData.Scan0, srcBuffer, 0, bytes);

            for (int i = 0; i < bytes; i += 4)
            {
                byte b = srcBuffer[i];
                byte g = srcBuffer[i + 1];
                byte r = srcBuffer[i + 2];
                byte a = srcBuffer[i + 3];

                int newR = Clamp(r + 50, 0, 255);
                int newG = Clamp(g + 50, 0, 255);
                int newB = Clamp(b + 50, 0, 255);

                dstBuffer[i] = (byte)newB;
                dstBuffer[i + 1] = (byte)newG;
                dstBuffer[i + 2] = (byte)newR;
                dstBuffer[i + 3] = a;
            }

            Marshal.Copy(dstBuffer, 0, dstData.Scan0, bytes);

            original.UnlockBits(srcData);
            flashImage.UnlockBits(dstData);

            return flashImage;
        }

        private Bitmap ApplyKakao(Bitmap original)
        {
            Bitmap kakaoImage = new Bitmap(original.Width, original.Height);
            BitmapData srcData = original.LockBits(new Rectangle(0, 0, original.Width, original.Height), ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);
            BitmapData dstData = kakaoImage.LockBits(new Rectangle(0, 0, kakaoImage.Width, kakaoImage.Height), ImageLockMode.WriteOnly, PixelFormat.Format32bppArgb);

            int bytes = srcData.Stride * srcData.Height;
            byte[] srcBuffer = new byte[bytes];
            byte[] dstBuffer = new byte[bytes];

            Marshal.Copy(srcData.Scan0, srcBuffer, 0, bytes);

            for (int i = 0; i < bytes; i += 4)
            {
                byte b = srcBuffer[i];
                byte g = srcBuffer[i + 1];
                byte r = srcBuffer[i + 2];
                byte a = srcBuffer[i + 3];

                int newR = Clamp((int)(r * 1.1), 0, 255);
                int newG = Clamp((int)(g * 0.9), 0, 255);
                int newB = Clamp((int)(b * 0.7), 0, 255);

                dstBuffer[i] = (byte)newB;
                dstBuffer[i + 1] = (byte)newG;
                dstBuffer[i + 2] = (byte)newR;
                dstBuffer[i + 3] = a;
            }

            Marshal.Copy(dstBuffer, 0, dstData.Scan0, bytes);

            original.UnlockBits(srcData);
            kakaoImage.UnlockBits(dstData);

            return kakaoImage;
        }

        private Bitmap ApplyWinter(Bitmap original)
        {
            Bitmap winterImage = new Bitmap(original.Width, original.Height);
            BitmapData srcData = original.LockBits(new Rectangle(0, 0, original.Width, original.Height), ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);
            BitmapData dstData = winterImage.LockBits(new Rectangle(0, 0, winterImage.Width, winterImage.Height), ImageLockMode.WriteOnly, PixelFormat.Format32bppArgb);

            int bytes = srcData.Stride * srcData.Height;
            byte[] srcBuffer = new byte[bytes];
            byte[] dstBuffer = new byte[bytes];

            Marshal.Copy(srcData.Scan0, srcBuffer, 0, bytes);

            for (int i = 0; i < bytes; i += 4)
            {
                byte b = srcBuffer[i];
                byte g = srcBuffer[i + 1];
                byte r = srcBuffer[i + 2];
                byte a = srcBuffer[i + 3];

                int newR = Clamp((int)(r * 0.8), 0, 255);
                int newG = Clamp((int)(g * 0.9), 0, 255);
                int newB = Clamp((int)(b * 1.2), 0, 255);

                dstBuffer[i] = (byte)newB;
                dstBuffer[i + 1] = (byte)newG;
                dstBuffer[i + 2] = (byte)newR;
                dstBuffer[i + 3] = a;
            }

            Marshal.Copy(dstBuffer, 0, dstData.Scan0, bytes);

            original.UnlockBits(srcData);
            winterImage.UnlockBits(dstData);

            return winterImage;
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e) // Fog
        {
            if (originalImage != null)
            {
                isGrayscaleMode = false;
                processedImage = ApplyFog(originalImage);
                pictureBox2.Image = processedImage;
            }
        }

        private void flashToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (originalImage != null)
            {
                isGrayscaleMode = false;
                processedImage = ApplyFlash(originalImage);
                pictureBox2.Image = processedImage;
            }
        }

        private void kakaoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (originalImage != null)
            {
                isGrayscaleMode = false;
                processedImage = ApplyKakao(originalImage);
                pictureBox2.Image = processedImage;
            }
        }

        private void winterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (originalImage != null)
            {
                isGrayscaleMode = false;
                processedImage = ApplyWinter(originalImage);
                pictureBox2.Image = processedImage;
            }
        }

        private void btnMirror_Click(object sender, EventArgs e)
        {
            if (processedImage != null)
            {
                processedImage.RotateFlip(RotateFlipType.RotateNoneFlipX);
                pictureBox2.Image = processedImage;
            }
        }

        private void btnRotate_Click(object sender, EventArgs e)
        {
            if (processedImage != null)
            {
                processedImage.RotateFlip(RotateFlipType.Rotate90FlipNone);
                pictureBox2.Image = processedImage;

                // Update trackbar value (optional)
                trackBarRotate.Value = (trackBarRotate.Value + 90) % 360;
            }

            trackBarRotate.Visible = true;
        }

        private void btnText_Click(object sender, EventArgs e)
        {
            if (processedImage != null)
            {
                using (Graphics g = Graphics.FromImage(processedImage))
                {
                    string text = "Sample Text";
                    Font font = new Font("Arial", 50, FontStyle.Bold);
                    SolidBrush brush = new SolidBrush(Color.Red);
                    g.DrawString(text, font, brush, new Point(20, 20));
                }
                pictureBox2.Image = processedImage;
            }
        }

        private void btnAdjust_Click(object sender, EventArgs e)
        {
            trackBarRed.Visible = true;
            trackBarGreen.Visible = true;
            trackBarBlue.Visible = true;
            trackBarBrightness.Visible = true;
            trackBarOpacity.Visible = true;
            trackBarContrast.Visible = true;
            trackBarSaturation.Visible = true;
            trackBarBlur.Visible = true;
            labelRed.Visible = true;
            labelGreen.Visible = true;
            labelBlue.Visible = true;
            labelBrightness.Visible = true;
            labelOpacity.Visible = true;
            labelContrast.Visible = true;
            labelSaturation.Visible = true;
            labelBlur.Visible = true;
        }

        private void label3_Click(object sender, EventArgs e) { }

        private void label2_Click(object sender, EventArgs e) { }

        private void label1_Click(object sender, EventArgs e) { }

        private void Form1_Load(object sender, EventArgs e)
        {
            trackBarRed.Visible = false;
            trackBarGreen.Visible = false;
            trackBarBlue.Visible = false;
            trackBarBrightness.Visible = false;
            trackBarOpacity.Visible = false;
            trackBarContrast.Visible = false;
            trackBarSaturation.Visible = false;
            trackBarBlur.Visible = false;
            trackBarThreshold.Visible = false;
            labelRed.Visible = false;
            labelGreen.Visible = false;
            labelBlue.Visible = false;
            labelBrightness.Visible = false;
            pictureBoxHistogram.Visible = false;
            labelOpacity.Visible = false;
            labelContrast.Visible = false;
            labelSaturation.Visible = false;
            labelBlur.Visible = false;
            labelHistogram.Visible = false;
            lblWhiteThreshold.Visible = false;
            lblBlackThreshold.Visible = false;
            trackBarBlack.Visible = false;
            trackBarWhite.Visible = false;
            labelGrayscale.Visible = false;
            labelThreshold.Visible = false;
            trackBarRotate.Visible = false;
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            if (originalImage != null)
            {
                if (isGrayscaleMode)
                {
                    processedImage = ConvertToGrayscale(originalImage, sliderGrayscale.Value / 100.0);
                }
                else
                {
                    processedImage = new Bitmap(originalImage);
                }
                pictureBox2.Image = processedImage;
                ResetSliders();
            }
        }

        private void ResetSliders()
        {
            trackBarRed.Value = 0;
            trackBarGreen.Value = 0;
            trackBarBlue.Value = 0;
            sliderGrayscale.Value = 100;
            trackBarBrightness.Value = 0;
            trackBarContrast.Value = 0;
            trackBarSaturation.Value = 0;
            trackBarOpacity.Value = 100;
            trackBarBlur.Value = 0;
            trackBarRotate.Value = 0;
            textBoxTranslation.Text = "0, 0";
        }

        private void label7_Click(object sender, EventArgs e) { }

        private void labelBrightness_Click(object sender, EventArgs e) { }

        private void btnBinaryImage_Click(object sender, EventArgs e)
        {
            if (originalImage == null)
            {
                MessageBox.Show("Please load an image first.");
                return;
            }

            if (adjustedImage == null)
            {
                MessageBox.Show("Adjusted image is not available.");
                return;
            }

            isBinaryMode = !isBinaryMode;

            if (isBinaryMode)
            {
                processedImage = ConvertToBinary(adjustedImage);
            }
            else
            {
                processedImage = adjustedImage;
            }

            pictureBox2.Image = processedImage;
        }

        // Optimized ConvertToBinary using LockBits
        private Bitmap ConvertToBinary(Bitmap original)
        {
            Bitmap binaryImage = new Bitmap(original.Width, original.Height);
            BitmapData srcData = original.LockBits(new Rectangle(0, 0, original.Width, original.Height), ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);
            BitmapData dstData = binaryImage.LockBits(new Rectangle(0, 0, binaryImage.Width, binaryImage.Height), ImageLockMode.WriteOnly, PixelFormat.Format32bppArgb);

            int bytes = srcData.Stride * srcData.Height;
            byte[] srcBuffer = new byte[bytes];
            byte[] dstBuffer = new byte[bytes];
            int threshold = 128;

            Marshal.Copy(srcData.Scan0, srcBuffer, 0, bytes);

            for (int i = 0; i < bytes; i += 4)
            {
                byte b = srcBuffer[i];
                byte g = srcBuffer[i + 1];
                byte r = srcBuffer[i + 2];
                byte a = srcBuffer[i + 3];

                int grayValue = (r + g + b) / 3;
                byte newValue = grayValue < threshold ? (byte)0 : (byte)255;

                dstBuffer[i] = newValue;
                dstBuffer[i + 1] = newValue;
                dstBuffer[i + 2] = newValue;
                dstBuffer[i + 3] = a;
            }

            Marshal.Copy(dstBuffer, 0, dstData.Scan0, bytes);

            original.UnlockBits(srcData);
            binaryImage.UnlockBits(dstData);

            return binaryImage;
        }

        private void pineToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (originalImage != null)
            {
                isGrayscaleMode = false;
                processedImage = ApplyPineFilter(originalImage);
                pictureBox2.Image = processedImage;
            }
        }

        private Bitmap ApplyPineFilter(Bitmap original)
        {
            Bitmap filteredImage = new Bitmap(original.Width, original.Height);
            BitmapData srcData = original.LockBits(new Rectangle(0, 0, original.Width, original.Height), ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);
            BitmapData dstData = filteredImage.LockBits(new Rectangle(0, 0, filteredImage.Width, filteredImage.Height), ImageLockMode.WriteOnly, PixelFormat.Format32bppArgb);

            int bytes = srcData.Stride * srcData.Height;
            byte[] srcBuffer = new byte[bytes];
            byte[] dstBuffer = new byte[bytes];

            Marshal.Copy(srcData.Scan0, srcBuffer, 0, bytes);

            for (int i = 0; i < bytes; i += 4)
            {
                byte b = srcBuffer[i];
                byte g = srcBuffer[i + 1];
                byte r = srcBuffer[i + 2];
                byte a = srcBuffer[i + 3];

                int newR = r / 2;
                int newB = b / 2;

                dstBuffer[i] = (byte)newB;
                dstBuffer[i + 1] = g;
                dstBuffer[i + 2] = (byte)newR;
                dstBuffer[i + 3] = a;
            }

            Marshal.Copy(dstBuffer, 0, dstData.Scan0, bytes);

            original.UnlockBits(srcData);
            filteredImage.UnlockBits(dstData);

            return filteredImage;
        }

        private void fliterToolStripMenuItem_Click(object sender, EventArgs e) { }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e) { }

        private void negativeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (originalImage != null)
            {
                isGrayscaleMode = false;
                processedImage = ApplyNegativeFilter(originalImage);
                pictureBox2.Image = processedImage;
            }
        }

        private Bitmap ApplyNegativeFilter(Bitmap original)
        {
            Bitmap filteredImage = new Bitmap(original.Width, original.Height);
            BitmapData srcData = original.LockBits(new Rectangle(0, 0, original.Width, original.Height), ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);
            BitmapData dstData = filteredImage.LockBits(new Rectangle(0, 0, filteredImage.Width, filteredImage.Height), ImageLockMode.WriteOnly, PixelFormat.Format32bppArgb);

            int bytes = srcData.Stride * srcData.Height;
            byte[] srcBuffer = new byte[bytes];
            byte[] dstBuffer = new byte[bytes];

            Marshal.Copy(srcData.Scan0, srcBuffer, 0, bytes);

            for (int i = 0; i < bytes; i += 4)
            {
                byte b = srcBuffer[i];
                byte g = srcBuffer[i + 1];
                byte r = srcBuffer[i + 2];
                byte a = srcBuffer[i + 3];

                dstBuffer[i] = (byte)(255 - b);
                dstBuffer[i + 1] = (byte)(255 - g);
                dstBuffer[i + 2] = (byte)(255 - r);
                dstBuffer[i + 3] = a;
            }

            Marshal.Copy(dstBuffer, 0, dstData.Scan0, bytes);

            original.UnlockBits(srcData);
            filteredImage.UnlockBits(dstData);

            return filteredImage;
        }

        private void cobaltToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (originalImage != null)
            {
                isGrayscaleMode = false;
                processedImage = ApplyCobaltFilter(originalImage);
                pictureBox2.Image = processedImage;
            }
        }

        private Bitmap ApplyCobaltFilter(Bitmap original)
        {
            Bitmap filteredImage = new Bitmap(original.Width, original.Height);
            BitmapData srcData = original.LockBits(new Rectangle(0, 0, original.Width, original.Height), ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);
            BitmapData dstData = filteredImage.LockBits(new Rectangle(0, 0, filteredImage.Width, filteredImage.Height), ImageLockMode.WriteOnly, PixelFormat.Format32bppArgb);

            int bytes = srcData.Stride * srcData.Height;
            byte[] srcBuffer = new byte[bytes];
            byte[] dstBuffer = new byte[bytes];

            Marshal.Copy(srcData.Scan0, srcBuffer, 0, bytes);

            for (int i = 0; i < bytes; i += 4)
            {
                byte b = srcBuffer[i];
                byte g = srcBuffer[i + 1];
                byte r = srcBuffer[i + 2];
                byte a = srcBuffer[i + 3];

                int newR = r / 2;
                int newG = g / 2;

                dstBuffer[i] = b;
                dstBuffer[i + 1] = (byte)newG;
                dstBuffer[i + 2] = (byte)newR;
                dstBuffer[i + 3] = a;
            }

            Marshal.Copy(dstBuffer, 0, dstData.Scan0, bytes);

            original.UnlockBits(srcData);
            filteredImage.UnlockBits(dstData);

            return filteredImage;
        }

        private void noneToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (originalImage != null)
            {
                isGrayscaleMode = false;
                pictureBox2.Image = originalImage;
                processedImage = new Bitmap(originalImage);
            }
        }

        private void label1_Click_1(object sender, EventArgs e) { }

        private void btnThresholding_Click(object sender, EventArgs e)
        {
            if (originalImage == null) return;

            int T1 = trackBarBlack.Value;
            int T2 = trackBarWhite.Value;
            trackBarThreshold.Visible = true;
            lblWhiteThreshold.Visible = true;
            lblBlackThreshold.Visible = true;
            labelThreshold.Visible = true;
            trackBarBlack.Visible = true;
            trackBarWhite.Visible = true;

            processedImage = ApplyDoubleThreshold(originalImage, T1, T2);
            pictureBox2.Image = processedImage;
        }

        // Optimized ApplyDoubleThreshold using LockBits
        private Bitmap ApplyDoubleThreshold(Bitmap original, int T1, int T2)
        {
            Bitmap newBitmap = new Bitmap(original.Width, original.Height);
            BitmapData srcData = original.LockBits(new Rectangle(0, 0, original.Width, original.Height), ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);
            BitmapData dstData = newBitmap.LockBits(new Rectangle(0, 0, newBitmap.Width, newBitmap.Height), ImageLockMode.WriteOnly, PixelFormat.Format32bppArgb);

            int bytes = srcData.Stride * srcData.Height;
            byte[] srcBuffer = new byte[bytes];
            byte[] dstBuffer = new byte[bytes];

            Marshal.Copy(srcData.Scan0, srcBuffer, 0, bytes);

            for (int i = 0; i < bytes; i += 4)
            {
                byte b = srcBuffer[i];
                byte g = srcBuffer[i + 1];
                byte r = srcBuffer[i + 2];
                byte a = srcBuffer[i + 3];

                int grayValue = (r + g + b) / 3;
                byte newValue = (grayValue >= T1 && grayValue <= T2) ? (byte)255 : (byte)0;

                dstBuffer[i] = newValue;
                dstBuffer[i + 1] = newValue;
                dstBuffer[i + 2] = newValue;
                dstBuffer[i + 3] = a;
            }

            Marshal.Copy(dstBuffer, 0, dstData.Scan0, bytes);

            original.UnlockBits(srcData);
            newBitmap.UnlockBits(dstData);

            return newBitmap;
        }

        private void trackBarWhite_Scroll(object sender, EventArgs e)
        {
            if (originalImage == null || !Debounce()) return;

            lblWhiteThreshold.Text = $"White Threshold (T2): {trackBarWhite.Value}";
            int T1 = trackBarBlack.Value;
            int T2 = trackBarWhite.Value;
            processedImage = ApplyDoubleThreshold(originalImage, T1, T2);
            pictureBox2.Image = processedImage;
        }

        private void trackBarBlack_Scroll(object sender, EventArgs e)
        {
            if (originalImage == null || !Debounce()) return;

            lblBlackThreshold.Text = $"Black Threshold (T1): {trackBarBlack.Value}";
            int T1 = trackBarBlack.Value;
            int T2 = trackBarWhite.Value;
            processedImage = ApplyDoubleThreshold(originalImage, T1, T2);
            pictureBox2.Image = processedImage;
        }

        private void btnAdaptiveBinarization_Click(object sender, EventArgs e)
        {
            if (originalImage == null)
            {
                MessageBox.Show("Please load an image first.");
                return;
            }

            int windowSize = 15;
            double offset = 10;

            processedImage = ApplyAdaptiveBinarization(originalImage, windowSize, offset);
            pictureBox2.Image = processedImage;
        }

        // Optimized ApplyAdaptiveBinarization using LockBits
        private Bitmap ApplyAdaptiveBinarization(Bitmap original, int windowSize, double offset)
        {
            Bitmap binarizedImage = new Bitmap(original.Width, original.Height);
            int halfWindow = windowSize / 2;

            BitmapData srcData = original.LockBits(new Rectangle(0, 0, original.Width, original.Height), ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);
            BitmapData dstData = binarizedImage.LockBits(new Rectangle(0, 0, binarizedImage.Width, binarizedImage.Height), ImageLockMode.WriteOnly, PixelFormat.Format32bppArgb);

            int bytes = srcData.Stride * srcData.Height;
            byte[] srcBuffer = new byte[bytes];
            byte[] dstBuffer = new byte[bytes];

            Marshal.Copy(srcData.Scan0, srcBuffer, 0, bytes);

            for (int y = 0; y < original.Height; y++)
            {
                for (int x = 0; x < original.Width; x++)
                {
                    int startX = Math.Max(0, x - halfWindow);
                    int startY = Math.Max(0, y - halfWindow);
                    int endX = Math.Min(original.Width - 1, x + halfWindow);
                    int endY = Math.Min(original.Height - 1, y + halfWindow);

                    int sum = 0, count = 0;

                    for (int i = startY; i <= endY; i++)
                    {
                        for (int j = startX; j <= endX; j++)
                        {
                            int index = (i * srcData.Stride) + (j * 4);
                            byte b = srcBuffer[index];
                            byte g = srcBuffer[index + 1];
                            byte r = srcBuffer[index + 2];
                            int gray = (r + g + b) / 3;
                            sum += gray;
                            count++;
                        }
                    }

                    int localThreshold = (int)(sum / count - offset);
                    int currentIndex = (y * srcData.Stride) + (x * 4);
                    byte currentB = srcBuffer[currentIndex];
                    byte currentG = srcBuffer[currentIndex + 1];
                    byte currentR = srcBuffer[currentIndex + 2];
                    byte currentA = srcBuffer[currentIndex + 3];
                    int grayValue = (currentR + currentG + currentB) / 3;

                    byte newValue = grayValue > localThreshold ? (byte)255 : (byte)0;
                    dstBuffer[currentIndex] = newValue;
                    dstBuffer[currentIndex + 1] = newValue;
                    dstBuffer[currentIndex + 2] = newValue;
                    dstBuffer[currentIndex + 3] = currentA;
                }
            }

            Marshal.Copy(dstBuffer, 0, dstData.Scan0, bytes);

            original.UnlockBits(srcData);
            binarizedImage.UnlockBits(dstData);

            return binarizedImage;
        }

        private void pictureBox2_Click(object sender, EventArgs e) { }

        private void trackBarContrast_Scroll(object sender, EventArgs e) { }

        private void pictureBox1_Click(object sender, EventArgs e) { }

        private void TrackBar_Scroll(object sender, EventArgs e)
        {
            if (originalImage == null || !Debounce()) return;

            float opacity = trackBarOpacity.Value / 100f;
            int brightness = trackBarBrightness.Value;
            int contrast = trackBarContrast.Value;
            int saturation = trackBarSaturation.Value;
            int blur = trackBarBlur.Value;

            Bitmap baseImage = isGrayscaleMode ? processedImage : originalImage;

            adjustedImage = ApplyImageAdjustments(baseImage, opacity, brightness, contrast, saturation, blur);

            int redAdjustment = trackBarRed.Value;
            int greenAdjustment = trackBarGreen.Value;
            int blueAdjustment = trackBarBlue.Value;
            adjustedImage = AdjustRGB(adjustedImage, redAdjustment, greenAdjustment, blueAdjustment);

            if (isBinaryMode)
            {
                adjustedImage = ConvertToBinary(adjustedImage);
            }

            processedImage = new Bitmap(adjustedImage);
            pictureBox2.Image = adjustedImage;
        }

        private Bitmap ApplyImageAdjustments(Bitmap image, float opacity, int brightness, int contrast, int saturation, int blur)
        {
            if (image == null)
                throw new ArgumentNullException(nameof(image), "Input image cannot be null.");

            Bitmap adjustedBitmap = new Bitmap(image.Width, image.Height);

            using (Graphics g = Graphics.FromImage(adjustedBitmap))
            {
                ColorMatrix matrix = new ColorMatrix();

                matrix.Matrix33 = opacity;

                float brightnessFactor = brightness / 100f;
                matrix.Matrix40 = brightnessFactor;
                matrix.Matrix41 = brightnessFactor;
                matrix.Matrix42 = brightnessFactor;

                float contrastFactor = 1.0f + (contrast / 100f);
                float contrastTranslation = (1.0f - contrastFactor) / 2.0f;
                matrix.Matrix00 = contrastFactor;
                matrix.Matrix11 = contrastFactor;
                matrix.Matrix22 = contrastFactor;
                matrix.Matrix40 += contrastTranslation;
                matrix.Matrix41 += contrastTranslation;
                matrix.Matrix42 += contrastTranslation;

                float saturationFactor = 1.0f + (saturation / 100f);
                float rwgt = 0.3086f;
                float gwgt = 0.6094f;
                float bwgt = 0.0820f;
                float s = saturationFactor;
                float sr = (1 - s) * rwgt;
                float sg = (1 - s) * gwgt;
                float sb = (1 - s) * bwgt;

                matrix.Matrix00 = sr + s; matrix.Matrix01 = sg; matrix.Matrix02 = sb;
                matrix.Matrix10 = sr; matrix.Matrix11 = sg + s; matrix.Matrix12 = sb;
                matrix.Matrix20 = sr; matrix.Matrix21 = sg; matrix.Matrix22 = sb + s;

                ImageAttributes attributes = new ImageAttributes();
                attributes.SetColorMatrix(matrix, ColorMatrixFlag.Default, ColorAdjustType.Bitmap);

                g.DrawImage(image, new Rectangle(0, 0, image.Width, image.Height),
                            0, 0, image.Width, image.Height, GraphicsUnit.Pixel, attributes);
            }

            if (blur > 0)
            {
                adjustedBitmap = ApplyGaussianBlur(adjustedBitmap, blur);
            }

            return adjustedBitmap;
        }

        private Bitmap ApplyGaussianBlur(Bitmap image, int blurRadius)
        {
            Bitmap blurredBitmap = new Bitmap(image.Width, image.Height);
            float[,] kernel = GenerateGaussianKernel(blurRadius);
            int kernelSize = kernel.GetLength(0);
            int halfKernel = kernelSize / 2;

            BitmapData srcData = image.LockBits(new Rectangle(0, 0, image.Width, image.Height), ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);
            BitmapData dstData = blurredBitmap.LockBits(new Rectangle(0, 0, blurredBitmap.Width, blurredBitmap.Height), ImageLockMode.WriteOnly, PixelFormat.Format32bppArgb);

            int bytes = srcData.Stride * srcData.Height;
            byte[] srcBuffer = new byte[bytes];
            byte[] dstBuffer = new byte[bytes];

            Marshal.Copy(srcData.Scan0, srcBuffer, 0, bytes);

            for (int y = 0; y < image.Height; y++)
            {
                for (int x = 0; x < image.Width; x++)
                {
                    float r = 0, g = 0, b = 0;
                    float weightSum = 0;

                    for (int ky = -halfKernel; ky <= halfKernel; ky++)
                    {
                        for (int kx = -halfKernel; kx <= halfKernel; kx++)
                        {
                            int px = Math.Clamp(x + kx, 0, image.Width - 1);
                            int py = Math.Clamp(y + ky, 0, image.Height - 1);
                            int index = (py * srcData.Stride) + (px * 4);

                            byte pixelB = srcBuffer[index];
                            byte pixelG = srcBuffer[index + 1];
                            byte pixelR = srcBuffer[index + 2];
                            float weight = kernel[ky + halfKernel, kx + halfKernel];

                            r += pixelR * weight;
                            g += pixelG * weight;
                            b += pixelB * weight;
                            weightSum += weight;
                        }
                    }

                    int newIndex = (y * srcData.Stride) + (x * 4);
                    dstBuffer[newIndex] = (byte)Math.Clamp((int)(b / weightSum), 0, 255);
                    dstBuffer[newIndex + 1] = (byte)Math.Clamp((int)(g / weightSum), 0, 255);
                    dstBuffer[newIndex + 2] = (byte)Math.Clamp((int)(r / weightSum), 0, 255);
                    dstBuffer[newIndex + 3] = srcBuffer[newIndex + 3];
                }
            }

            Marshal.Copy(dstBuffer, 0, dstData.Scan0, bytes);

            image.UnlockBits(srcData);
            blurredBitmap.UnlockBits(dstData);

            return blurredBitmap;
        }

        private float[,] GenerateGaussianKernel(int radius)
        {
            int size = radius * 2 + 1;
            float[,] kernel = new float[size, size];
            float sigma = radius / 3f;
            float sigma22 = 2 * sigma * sigma;
            float sigmaPi2 = (float)(Math.PI * sigma22);
            float sum = 0;

            for (int y = -radius; y <= radius; y++)
            {
                for (int x = -radius; x <= radius; x++)
                {
                    float value = (float)Math.Exp(-(x * x + y * y) / sigma22) / sigmaPi2;
                    kernel[y + radius, x + radius] = value;
                    sum += value;
                }
            }

            for (int y = 0; y < size; y++)
                for (int x = 0; x < size; x++)
                    kernel[y, x] /= sum;

            return kernel;
        }

        private Bitmap ApplyBlur(Bitmap image, int blurRadius)
        {
            Bitmap blurredBitmap = new Bitmap(image.Width, image.Height);

            using (Graphics g = Graphics.FromImage(blurredBitmap))
            {
                for (int i = 0; i < blurRadius; i++)
                {
                    g.DrawImage(image, new Rectangle(0, 0, image.Width, image.Height), new Rectangle(1, 1, image.Width - 2, image.Height - 2), GraphicsUnit.Pixel);
                }
            }

            return blurredBitmap;
        }

        private void textBox1_TextChanged(object sender, EventArgs e) { }

        private int[,] CalculateHistogram(Bitmap image)
        {
            int[,] histogram = new int[3, 256];
            BitmapData srcData = image.LockBits(new Rectangle(0, 0, image.Width, image.Height), ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);

            int bytes = srcData.Stride * srcData.Height;
            byte[] buffer = new byte[bytes];

            Marshal.Copy(srcData.Scan0, buffer, 0, bytes);

            for (int i = 0; i < bytes; i += 4)
            {
                histogram[0, buffer[i + 2]]++; // Red
                histogram[1, buffer[i + 1]]++; // Green
                histogram[2, buffer[i]]++;     // Blue
            }

            image.UnlockBits(srcData);

            return histogram;
        }

        private void DrawHistogram(int[,] histogram)
        {
            int width = pictureBoxHistogram.Width;
            int height = pictureBoxHistogram.Height;

            Bitmap histogramImage = new Bitmap(width, height);
            using (Graphics g = Graphics.FromImage(histogramImage))
            {
                g.Clear(Color.White);

                int maxCount = 0;
                for (int i = 0; i < 3; i++)
                {
                    for (int j = 0; j < 256; j++)
                    {
                        if (histogram[i, j] > maxCount)
                            maxCount = histogram[i, j];
                    }
                }

                for (int i = 0; i < 256; i++)
                {
                    int redHeight = (int)((float)histogram[0, i] / maxCount * height);
                    int greenHeight = (int)((float)histogram[1, i] / maxCount * height);
                    int blueHeight = (int)((float)histogram[2, i] / maxCount * height);

                    g.DrawLine(Pens.Red, i, height, i, height - redHeight);
                    g.DrawLine(Pens.Green, i, height, i, height - greenHeight);
                    g.DrawLine(Pens.Blue, i, height, i, height - blueHeight);
                }
            }

            pictureBoxHistogram.Image = histogramImage;
        }

        private void UpdateHistogram()
        {
            if (originalImage == null) return;

            int[,] histogram = CalculateHistogram(originalImage);
            DrawHistogram(histogram);
        }

        private void btnHistogram_Click(object sender, EventArgs e)
        {
            pictureBoxHistogram.Visible = true;
            labelHistogram.Visible = true;
        }

        private void pictureBoxHistogram_Click(object sender, EventArgs e) { }

        private void geometricShapeDetectionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (processedImage == null)
            {
                MessageBox.Show("Please process an image first!");
                return;
            }
        }

        private void label1_Click_2(object sender, EventArgs e) { }

        private void pictureBox2_Click_1(object sender, EventArgs e) { }

        private void label2_Click_1(object sender, EventArgs e) { }

        private void lblWhiteThreshold_Click(object sender, EventArgs e) { }

        private void labelGrayscale_Click(object sender, EventArgs e) { }

        private void dToolStripMenuItem_Click(object sender, EventArgs e) { }

        private void geometricShapeDetectionToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            if (processedImage != null)
            {
                ShapeDetection shapeDetectionForm = new ShapeDetection(processedImage);
                shapeDetectionForm.Show();
            }
            else
            {
                MessageBox.Show("Please process an image first.");
            }
        }

        // Debouncing method to reduce trackbar lag
        private bool Debounce()
        {
            DateTime now = DateTime.Now;
            if ((now - lastTrackBarUpdate).TotalMilliseconds < DEBOUNCE_DELAY_MS)
                return false;

            lastTrackBarUpdate = now;
            return true;
        }

        // Enhanced Semantic Segmentation using K-means clustering
        private Bitmap ApplySemanticSegmentation(Bitmap original, int numClusters = 3)
        {
            Bitmap segmentedImage = new Bitmap(original.Width, original.Height);
            BitmapData srcData = original.LockBits(new Rectangle(0, 0, original.Width, original.Height), ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);
            BitmapData dstData = segmentedImage.LockBits(new Rectangle(0, 0, segmentedImage.Width, segmentedImage.Height), ImageLockMode.WriteOnly, PixelFormat.Format32bppArgb);

            int bytes = srcData.Stride * srcData.Height;
            byte[] srcBuffer = new byte[bytes];
            byte[] dstBuffer = new byte[bytes];

            Marshal.Copy(srcData.Scan0, srcBuffer, 0, bytes);

            // Collect pixel data for clustering
            List<(int r, int g, int b)> pixels = new List<(int, int, int)>();
            for (int i = 0; i < bytes; i += 4)
            {
                byte b = srcBuffer[i];
                byte g = srcBuffer[i + 1];
                byte r = srcBuffer[i + 2];
                pixels.Add((r, g, b));
            }

            // Simple K-means clustering
            var clusters = KMeansClustering(pixels, numClusters);
            Dictionary<(int r, int g, int b), int> clusterAssignments = new Dictionary<(int, int, int), int>();
            for (int i = 0; i < pixels.Count; i++)
            {
                clusterAssignments[pixels[i]] = clusters.Item2[i];
            }

            // Assign cluster colors (for visualization)
            byte[][] clusterColors = new byte[numClusters][];
            Random rand = new Random();
            for (int i = 0; i < numClusters; i++)
            {
                clusterColors[i] = new byte[] { (byte)rand.Next(256), (byte)rand.Next(256), (byte)rand.Next(256) };
            }

            // Apply cluster colors to the image
            for (int i = 0; i < bytes; i += 4)
            {
                byte b = srcBuffer[i];
                byte g = srcBuffer[i + 1];
                byte r = srcBuffer[i + 2];
                byte a = srcBuffer[i + 3];

                int cluster = clusterAssignments[(r, g, b)];
                dstBuffer[i] = clusterColors[cluster][2];     // B
                dstBuffer[i + 1] = clusterColors[cluster][1]; // G
                dstBuffer[i + 2] = clusterColors[cluster][0]; // R
                dstBuffer[i + 3] = a;
            }

            Marshal.Copy(dstBuffer, 0, dstData.Scan0, bytes);

            original.UnlockBits(srcData);
            segmentedImage.UnlockBits(dstData);

            return segmentedImage;
        }

        // Simple K-means clustering implementation
        private (List<(int r, int g, int b)>, List<int>) KMeansClustering(List<(int r, int g, int b)> pixels, int k, int maxIterations = 10)
        {
            Random rand = new Random();
            List<(int r, int g, int b)> centroids = new List<(int r, int g, int b)>();
            List<int> assignments = new List<int>(new int[pixels.Count]);

            // Initialize centroids randomly
            for (int i = 0; i < k; i++)
            {
                centroids.Add(pixels[rand.Next(pixels.Count)]);
            }

            for (int iter = 0; iter < maxIterations; iter++)
            {
                // Assign pixels to nearest centroid
                for (int i = 0; i < pixels.Count; i++)
                {
                    var pixel = pixels[i];
                    double minDist = double.MaxValue;
                    int bestCluster = 0;

                    for (int j = 0; j < k; j++)
                    {
                        var centroid = centroids[j];
                        double dist = Math.Sqrt(Math.Pow(pixel.r - centroid.r, 2) + Math.Pow(pixel.g - centroid.g, 2) + Math.Pow(pixel.b - centroid.b, 2));
                        if (dist < minDist)
                        {
                            minDist = dist;
                            bestCluster = j;
                        }
                    }
                    assignments[i] = bestCluster;
                }

                // Update centroids
                for (int j = 0; j < k; j++)
                {
                    var clusterPixels = pixels.Where((_, idx) => assignments[idx] == j).ToList();
                    if (clusterPixels.Count == 0) continue;

                    int rSum = 0, gSum = 0, bSum = 0;
                    foreach (var pixel in clusterPixels)
                    {
                        rSum += pixel.r;
                        gSum += pixel.g;
                        bSum += pixel.b;
                    }
                    centroids[j] = (rSum / clusterPixels.Count, gSum / clusterPixels.Count, bSum / clusterPixels.Count);
                }
            }

            return (centroids, assignments);
        }

        // Instance Segmentation (simplified using connected components)
        private Bitmap ApplyInstanceSegmentation(Bitmap original)
        {
            // First, apply semantic segmentation to get clusters
            Bitmap semanticImage = ApplySemanticSegmentation(original, 3);
            Bitmap instanceImage = new Bitmap(original.Width, original.Height);

            BitmapData srcData = semanticImage.LockBits(new Rectangle(0, 0, original.Width, original.Height), ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);
            BitmapData dstData = instanceImage.LockBits(new Rectangle(0, 0, instanceImage.Width, instanceImage.Height), ImageLockMode.WriteOnly, PixelFormat.Format32bppArgb);

            int bytes = srcData.Stride * srcData.Height;
            byte[] srcBuffer = new byte[bytes];
            byte[] dstBuffer = new byte[bytes];

            Marshal.Copy(srcData.Scan0, srcBuffer, 0, bytes);

            // Create a label map for connected components
            int[,] labels = new int[original.Height, original.Width];
            int currentLabel = 1;

            // First pass: Label connected components
            for (int y = 0; y < original.Height; y++)
            {
                for (int x = 0; x < original.Width; x++)
                {
                    int index = (y * srcData.Stride) + (x * 4);
                    byte r = srcBuffer[index + 2];
                    byte g = srcBuffer[index + 1];
                    byte b = srcBuffer[index];

                    if (r == 0 && g == 0 && b == 0) continue; // Skip background

                    // Check neighbors (4-connectivity)
                    int label = 0;
                    if (x > 0 && labels[y, x - 1] != 0) label = labels[y, x - 1];
                    if (y > 0 && labels[y - 1, x] != 0 && (label == 0 || labels[y - 1, x] < label)) label = labels[y - 1, x];

                    if (label == 0)
                    {
                        label = currentLabel++;
                    }

                    labels[y, x] = label;
                }
            }

            // Assign colors to each instance
            Random rand = new Random();
            Dictionary<int, (byte r, byte g, byte b)> instanceColors = new Dictionary<int, (byte, byte, byte)>();
            for (int i = 1; i < currentLabel; i++)
            {
                instanceColors[i] = ((byte)rand.Next(256), (byte)rand.Next(256), (byte)rand.Next(256));
            }

            // Apply instance colors
            for (int y = 0; y < original.Height; y++)
            {
                for (int x = 0; x < original.Width; x++)
                {
                    int index = (y * srcData.Stride) + (x * 4);
                    byte a = srcBuffer[index + 3];
                    int label = labels[y, x];

                    if (label == 0)
                    {
                        dstBuffer[index] = 0;
                        dstBuffer[index + 1] = 0;
                        dstBuffer[index + 2] = 0;
                        dstBuffer[index + 3] = a;
                    }
                    else
                    {
                        var color = instanceColors[label];
                        dstBuffer[index] = color.b;
                        dstBuffer[index + 1] = color.g;
                        dstBuffer[index + 2] = color.r;
                        dstBuffer[index + 3] = a;
                    }
                }
            }

            Marshal.Copy(dstBuffer, 0, dstData.Scan0, bytes);

            semanticImage.UnlockBits(srcData);
            instanceImage.UnlockBits(dstData);

            return instanceImage;
        }

        // Panoptic Segmentation (combine semantic and instance)
        private Bitmap ApplyPanopticSegmentation(Bitmap original)
        {
            // First, apply semantic segmentation
            Bitmap semanticImage = ApplySemanticSegmentation(original, 3);
            Bitmap panopticImage = new Bitmap(original.Width, original.Height);

            BitmapData srcData = semanticImage.LockBits(new Rectangle(0, 0, original.Width, original.Height), ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);
            BitmapData dstData = panopticImage.LockBits(new Rectangle(0, 0, panopticImage.Width, panopticImage.Height), ImageLockMode.WriteOnly, PixelFormat.Format32bppArgb);

            int bytes = srcData.Stride * srcData.Height;
            byte[] srcBuffer = new byte[bytes];
            byte[] dstBuffer = new byte[bytes];

            Marshal.Copy(srcData.Scan0, srcBuffer, 0, bytes);

            // Create a label map for connected components (instance segmentation)
            int[,] labels = new int[original.Height, original.Width];
            int currentLabel = 1;

            // First pass: Label connected components
            for (int y = 0; y < original.Height; y++)
            {
                for (int x = 0; x < original.Width; x++)
                {
                    int index = (y * srcData.Stride) + (x * 4);
                    byte r = srcBuffer[index + 2];
                    byte g = srcBuffer[index + 1];
                    byte b = srcBuffer[index];

                    if (r == 0 && g == 0 && b == 0) continue; // Skip background

                    // Check neighbors (4-connectivity)
                    int label = 0;
                    if (x > 0 && labels[y, x - 1] != 0) label = labels[y, x - 1];
                    if (y > 0 && labels[y - 1, x] != 0 && (label == 0 || labels[y - 1, x] < label)) label = labels[y - 1, x];

                    if (label == 0)
                    {
                        label = currentLabel++;
                    }

                    labels[y, x] = label;
                }
            }

            // Assign colors based on both semantic category and instance
            Random rand = new Random();
            Dictionary<(byte r, byte g, byte b, int instance), (byte r, byte g, byte b)> panopticColors = new Dictionary<(byte, byte, byte, int), (byte, byte, byte)>();
            for (int y = 0; y < original.Height; y++)
            {
                for (int x = 0; x < original.Width; x++)
                {
                    int index = (y * srcData.Stride) + (x * 4);
                    byte r = srcBuffer[index + 2];
                    byte g = srcBuffer[index + 1];
                    byte b = srcBuffer[index];
                    int instance = labels[y, x];

                    var key = (r, g, b, instance);
                    if (!panopticColors.ContainsKey(key) && instance != 0)
                    {
                        panopticColors[key] = ((byte)rand.Next(256), (byte)rand.Next(256), (byte)rand.Next(256));
                    }
                }
            }

            // Apply panoptic colors
            for (int y = 0; y < original.Height; y++)
            {
                for (int x = 0; x < original.Width; x++)
                {
                    int index = (y * srcData.Stride) + (x * 4);
                    byte r = srcBuffer[index + 2];
                    byte g = srcBuffer[index + 1];
                    byte b = srcBuffer[index];
                    byte a = srcBuffer[index + 3];
                    int instance = labels[y, x];

                    var key = (r, g, b, instance);
                    if (instance == 0)
                    {
                        dstBuffer[index] = 0;
                        dstBuffer[index + 1] = 0;
                        dstBuffer[index + 2] = 0;
                        dstBuffer[index + 3] = a;
                    }
                    else
                    {
                        var color = panopticColors[key];
                        dstBuffer[index] = color.b;
                        dstBuffer[index + 1] = color.g;
                        dstBuffer[index + 2] = color.r;
                        dstBuffer[index + 3] = a;
                    }
                }
            }

            Marshal.Copy(dstBuffer, 0, dstData.Scan0, bytes);

            semanticImage.UnlockBits(srcData);
            panopticImage.UnlockBits(dstData);

            return panopticImage;
        }

        // Event handlers for the segmentation menu items




        private void semanticSegmentationToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            if (originalImage != null)
            {
                isGrayscaleMode = false;
                isBinaryMode = false; // Disable binary mode to show colors
                processedImage = ApplySemanticSegmentation(originalImage, 3);
                pictureBox2.Image = processedImage;
            }
            else
            {
                MessageBox.Show("Please load an image first.");
            }
        }

        private void panopticSegmentationToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            if (originalImage != null)
            {
                isGrayscaleMode = false;
                isBinaryMode = false; // Disable binary mode to show colors
                processedImage = ApplyPanopticSegmentation(originalImage);
                pictureBox2.Image = processedImage;
            }
            else
            {
                MessageBox.Show("Please load an image first.");
            }
        }

        private void trackBarThreshold_Scroll(object sender, EventArgs e)
        {
            if (originalImage == null || !Debounce()) return;

            int threshold = trackBarThreshold.Value;
            processedImage = ApplySingleThreshold(originalImage, threshold);
            pictureBox2.Image = processedImage;
        }

        private Bitmap ApplySingleThreshold(Bitmap original, int threshold)
        {
            Bitmap thresholdImage = new Bitmap(original.Width, original.Height);
            BitmapData srcData = original.LockBits(new Rectangle(0, 0, original.Width, original.Height), ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);
            BitmapData dstData = thresholdImage.LockBits(new Rectangle(0, 0, thresholdImage.Width, thresholdImage.Height), ImageLockMode.WriteOnly, PixelFormat.Format32bppArgb);

            int bytes = srcData.Stride * srcData.Height;
            byte[] srcBuffer = new byte[bytes];
            byte[] dstBuffer = new byte[bytes];

            Marshal.Copy(srcData.Scan0, srcBuffer, 0, bytes);

            for (int i = 0; i < bytes; i += 4)
            {
                byte b = srcBuffer[i];
                byte g = srcBuffer[i + 1];
                byte r = srcBuffer[i + 2];
                byte a = srcBuffer[i + 3];

                // Calculate grayscale value using luminance formula
                int grayValue = (int)(r * 0.299 + g * 0.587 + b * 0.114);
                byte newValue = grayValue < threshold ? (byte)0 : (byte)255;

                dstBuffer[i] = newValue;
                dstBuffer[i + 1] = newValue;
                dstBuffer[i + 2] = newValue;
                dstBuffer[i + 3] = a; // Preserve alpha channel
            }

            Marshal.Copy(dstBuffer, 0, dstData.Scan0, bytes);

            original.UnlockBits(srcData);
            thresholdImage.UnlockBits(dstData);

            return thresholdImage;
        }

        private void buttonSegmentation_Click(object sender, EventArgs e)
        {

        }

        private void trackBarRotate_Scroll(object sender, EventArgs e)
        {
            if (originalImage == null || !Debounce()) return;

            float angle = trackBarRotate.Value;

            // Create a new bitmap for the rotated image
            Bitmap rotatedImage = new Bitmap(originalImage.Width, originalImage.Height);

            using (Graphics g = Graphics.FromImage(rotatedImage))
            {
                g.Clear(Color.Transparent);

                // Set the rotation point to the center of the image
                g.TranslateTransform(originalImage.Width / 2f, originalImage.Height / 2f);

                // Apply the rotation
                g.RotateTransform(angle);

                // Move the image back
                g.TranslateTransform(-originalImage.Width / 2f, -originalImage.Height / 2f);

                // Draw the original image
                g.DrawImage(originalImage, new Point(0, 0));
            }

            // Update processedImage and display
            if (processedImage != null)
                processedImage.Dispose();

            processedImage = rotatedImage;
            pictureBox2.Image = processedImage;
        }

        private void btnTranslation_Click(object sender, EventArgs e)
        {
            if (originalImage == null)
            {
                MessageBox.Show("Please load an image first.");
                return;
            }

            // Parse the coordinates from textBoxTranslation
            if (!TryParseCoordinates(textBoxTranslation.Text, out int x, out int y))
            {
                MessageBox.Show("Please enter valid coordinates in format 'x, y' (e.g., '120, 50')");
                return;
            }

            ApplyTranslation(x, y);
        }

        private void textBoxTranslation_TextChanged(object sender, EventArgs e)
        {
            if (originalImage == null || !Debounce()) return;

            if (TryParseCoordinates(textBoxTranslation.Text, out int x, out int y))
            {
                ApplyTranslation(x, y);
            }
        }

        private bool TryParseCoordinates(string input, out int x, out int y)
        {
            x = 0;
            y = 0;

            if (string.IsNullOrWhiteSpace(input)) return false;

            string[] parts = input.Split(',');
            if (parts.Length != 2) return false;

            return int.TryParse(parts[0].Trim(), out x) && int.TryParse(parts[1].Trim(), out y);
        }

        private void ApplyTranslation(int x, int y)
        {
            // Create a new bitmap with the same size as the original
            Bitmap translatedImage = new Bitmap(originalImage.Width, originalImage.Height);

            using (Graphics g = Graphics.FromImage(translatedImage))
            {
                g.Clear(Color.Transparent); // Clear with transparent background

                // Apply translation and draw the image
                g.TranslateTransform(x, y);
                g.DrawImage(originalImage, new Point(0, 0));

                // Reset transform to prevent affecting future operations
                g.ResetTransform();
            }

            // Update processedImage
            if (processedImage != null)
                processedImage.Dispose();

            processedImage = translatedImage;
            pictureBox2.Image = processedImage;
        }

        private void segmentationToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }



        private void labelContrast_Click(object sender, EventArgs e)
        {

        }

        private void btnProjection_Click(object sender, EventArgs e)
        {
            btnHorizontal.Visible = true;
            btnVertical.Visible = true;
        }

        private void btnHorizontal_Click(object sender, EventArgs e)
        {
            if (originalImage == null)
            {
                MessageBox.Show("Please load an image first.");
                return;
            }

            int[] projection = new int[originalImage.Height];
            BitmapData data = originalImage.LockBits(new Rectangle(0, 0, originalImage.Width, originalImage.Height), ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);
            int bytes = data.Stride * data.Height;
            byte[] buffer = new byte[bytes];
            Marshal.Copy(data.Scan0, buffer, 0, bytes);
            originalImage.UnlockBits(data);

            for (int y = 0; y < originalImage.Height; y++)
            {
                int sum = 0;
                for (int x = 0; x < originalImage.Width; x++)
                {
                    int index = y * data.Stride + x * 4;
                    int gray = (int)(buffer[index] * 0.114 + buffer[index + 1] * 0.587 + buffer[index + 2] * 0.299);
                    sum += gray;
                }
                projection[y] = sum / originalImage.Width;
            }

            Bitmap projectionImage = new Bitmap(originalImage.Width, originalImage.Height);
            using (Graphics g = Graphics.FromImage(projectionImage))
            {
                g.Clear(Color.White);
                for (int y = 0; y < originalImage.Height; y++)
                {
                    int length = projection[y] * originalImage.Width / 255;
                    g.DrawLine(Pens.Black, 0, y, length, y);
                }
            }

            pictureBox2.Image = projectionImage;
        }

        private void btnVertical_Click(object sender, EventArgs e)
        {
            if (originalImage == null)
            {
                MessageBox.Show("Please load an image first.");
                return;
            }

            int[] projection = new int[originalImage.Width];
            BitmapData data = originalImage.LockBits(new Rectangle(0, 0, originalImage.Width, originalImage.Height), ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);
            int bytes = data.Stride * data.Height;
            byte[] buffer = new byte[bytes];
            Marshal.Copy(data.Scan0, buffer, 0, bytes);
            originalImage.UnlockBits(data);

            for (int x = 0; x < originalImage.Width; x++)
            {
                int sum = 0;
                for (int y = 0; y < originalImage.Height; y++)
                {
                    int index = y * data.Stride + x * 4;
                    int gray = (int)(buffer[index] * 0.114 + buffer[index + 1] * 0.587 + buffer[index + 2] * 0.299);
                    sum += gray;
                }
                projection[x] = sum / originalImage.Height;
            }

            Bitmap projectionImage = new Bitmap(originalImage.Width, originalImage.Height);
            using (Graphics g = Graphics.FromImage(projectionImage))
            {
                g.Clear(Color.White);
                for (int x = 0; x < originalImage.Width; x++)
                {
                    int length = projection[x] * originalImage.Height / 255;
                    g.DrawLine(Pens.Black, x, originalImage.Height, x, originalImage.Height - length);
                }
            }

            pictureBox2.Image = projectionImage;
        }

        private void DisplayProjection(int[] projection, string title)
        {
            pictureBoxHistogram.Visible = true;
            labelHistogram.Visible = true;
            labelHistogram.Text = title;

            int maxCount = projection.Max();
            if (maxCount == 0) maxCount = 1; // Avoid division by zero

            Bitmap histogramImage = new Bitmap(pictureBoxHistogram.Width, pictureBoxHistogram.Height);
            using (Graphics g = Graphics.FromImage(histogramImage))
            {
                g.Clear(Color.White);
                float widthPerBin = (float)pictureBoxHistogram.Width / projection.Length;
                float heightScale = (float)pictureBoxHistogram.Height / maxCount;

                for (int i = 0; i < projection.Length; i++)
                {
                    float x = i * widthPerBin;
                    float height = projection[i] * heightScale;
                    g.DrawLine(Pens.Black, x, pictureBoxHistogram.Height, x, pictureBoxHistogram.Height - height);
                }
            }

            pictureBoxHistogram.Image = histogramImage;
        }


        private void btnSmooth_Click(object sender, EventArgs e)
        {
            try
            {
                if (originalImage == null)
                {
                    MessageBox.Show("Please load an image first.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                double[,] filter = new double[,]
                {
            { 1, 1, 1 },
            { 1, 1, 1 },
            { 1, 1, 1 }
                };

                double factor = 1.0 / 9.0;
                int bias = 0;

                if (processedImage != null)
                    processedImage.Dispose();

                processedImage = ConvolutionFilter(originalImage, filter, factor, bias);
                pictureBox2.Image = processedImage;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnGaussianBlur_Click(object sender, EventArgs e)
        {
            try
            {
                if (originalImage == null)
                {
                    MessageBox.Show("Please load an image first.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                double[,] filter = new double[,]
                {
            { 1, 2, 1 },
            { 2, 4, 2 },
            { 1, 2, 1 }
                };

                double factor = 1.0 / 16.0;
                int bias = 0;

                if (processedImage != null)
                    processedImage.Dispose();

                processedImage = ConvolutionFilter(originalImage, filter, factor, bias);
                pictureBox2.Image = processedImage;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSharpen_Click(object sender, EventArgs e)
        {
            try
            {
                if (originalImage == null)
                {
                    MessageBox.Show("Please load an image first.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                double[,] filter = new double[,]
                {
            { 0, -2, 0 },
            { -2, 11, -2 },
            { 0, -2, 0 }
                };

                double factor = 1.0 / 3.0;
                int bias = 0;

                if (processedImage != null)
                    processedImage.Dispose();

                processedImage = ConvolutionFilter(originalImage, filter, factor, bias);
                pictureBox2.Image = processedImage;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnMeanRemoval_Click(object sender, EventArgs e)
        {
            try
            {
                if (originalImage == null)
                {
                    MessageBox.Show("Please load an image first.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                double[,] filter = new double[,]
                {
            { -1, -1, -1 },
            { -1, 9, -1 },
            { -1, -1, -1 }
                };

                double factor = 1.0 / 7.0; // Reverted to original factor
                int bias = 0;

                if (processedImage != null)
                    processedImage.Dispose();

                processedImage = ConvolutionFilter(originalImage, filter, factor, bias);
                pictureBox2.Image = processedImage;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnEmboss_Click(object sender, EventArgs e)
        {
            try
            {
                if (originalImage == null)
                {
                    MessageBox.Show("Please load an image first.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                double[,] filter = new double[,]
                {
            { -1, 0, -1 },
            { 0, 4, 0 },
            { -1, 0, -1 }
                };

                double factor = 1.0;
                int bias = 127; // Matches provided code

                if (processedImage != null)
                    processedImage.Dispose();

                processedImage = ConvolutionFilter(originalImage, filter, factor, bias);
                pictureBox2.Image = processedImage;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        // Helper method to apply convolution
        private Bitmap ConvolutionFilter(Bitmap sourceBitmap, double[,] filterMatrix, double factor = 1, int bias = 0)
        {
            BitmapData sourceData = sourceBitmap.LockBits(new Rectangle(0, 0,
                                  sourceBitmap.Width, sourceBitmap.Height),
                                  ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);

            byte[] pixelBuffer = new byte[sourceData.Stride * sourceData.Height];
            byte[] resultBuffer = new byte[sourceData.Stride * sourceData.Height];

            Marshal.Copy(sourceData.Scan0, pixelBuffer, 0, pixelBuffer.Length);
            sourceBitmap.UnlockBits(sourceData);

            int filterWidth = filterMatrix.GetLength(1);
            int filterHeight = filterMatrix.GetLength(0);

            int filterOffset = (filterWidth - 1) / 2;
            int calcOffset = 0;

            int byteOffset = 0;

            for (int offsetY = filterOffset; offsetY < sourceBitmap.Height - filterOffset; offsetY++)
            {
                for (int offsetX = filterOffset; offsetX < sourceBitmap.Width - filterOffset; offsetX++)
                {
                    double blue = 0;
                    double green = 0;
                    double red = 0;

                    byteOffset = offsetY * sourceData.Stride + offsetX * 4;

                    for (int filterY = -filterOffset; filterY <= filterOffset; filterY++)
                    {
                        for (int filterX = -filterOffset; filterX <= filterOffset; filterX++)
                        {
                            calcOffset = byteOffset + (filterX * 4) + (filterY * sourceData.Stride);

                            blue += (double)(pixelBuffer[calcOffset]) * filterMatrix[filterY + filterOffset, filterX + filterOffset];
                            green += (double)(pixelBuffer[calcOffset + 1]) * filterMatrix[filterY + filterOffset, filterX + filterOffset];
                            red += (double)(pixelBuffer[calcOffset + 2]) * filterMatrix[filterY + filterOffset, filterX + filterOffset];
                        }
                    }

                    blue = factor * blue + bias;
                    green = factor * green + bias;
                    red = factor * red + bias;

                    blue = blue > 255 ? 255 : (blue < 0 ? 0 : blue);
                    green = green > 255 ? 255 : (green < 0 ? 0 : green);
                    red = red > 255 ? 255 : (red < 0 ? 0 : red);

                    resultBuffer[byteOffset] = (byte)blue;
                    resultBuffer[byteOffset + 1] = (byte)green;
                    resultBuffer[byteOffset + 2] = (byte)red;
                    resultBuffer[byteOffset + 3] = pixelBuffer[byteOffset + 3];
                }
            }

            Bitmap resultBitmap = new Bitmap(sourceBitmap.Width, sourceBitmap.Height);

            BitmapData resultData = resultBitmap.LockBits(new Rectangle(0, 0,
                                    resultBitmap.Width, resultBitmap.Height),
                                    ImageLockMode.WriteOnly, PixelFormat.Format32bppArgb);

            Marshal.Copy(resultBuffer, 0, resultData.Scan0, resultBuffer.Length);
            resultBitmap.UnlockBits(resultData);

            return resultBitmap;
        }
    }
}
