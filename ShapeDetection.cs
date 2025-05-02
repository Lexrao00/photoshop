using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Structure;
using Emgu.CV.Util;

namespace PhotoShopss
{
    public partial class ShapeDetection : Form
    {
        private Bitmap originalImage;
        private Bitmap processedImage;
        private Point[] simplified;

        public ShapeDetection(Bitmap processedImage)
        {
            InitializeComponent();
            InitializeUI();
            this.originalImage = processedImage;
            pictureBoxProcessed.Image = processedImage;
        }

        private void InitializeUI()
        {
            listResults.Columns.Add("Shape Type", 150);
            listResults.Columns.Add("Center X", 100);
            listResults.Columns.Add("Center Y", 100);
            listResults.Columns.Add("Size", 100);
            listResults.Columns.Add("Confidence", 100);

            listViewColor.Columns.Add("Hex", 100);
            listViewColor.Columns.Add("RGB", 100);
            listViewColor.Columns.Add("Percentage", 100);

            btnDetect.Click += (s, e) => DetectShapes(pictureBoxProcessed, pictureBoxProcessed);
        }

        private string ClassifyColor(Color color)
        {
            double r = color.R / 255.0;
            double g = color.G / 255.0;
            double b = color.B / 255.0;

            double max = Math.Max(r, Math.Max(g, b));
            double min = Math.Min(r, Math.Min(g, b));
            double delta = max - min;

            double hue = 0;
            if (delta != 0)
            {
                if (max == r) hue = (g - b) / delta;
                else if (max == g) hue = 2 + (b - r) / delta;
                else hue = 4 + (r - g) / delta;
            }
            hue *= 60;
            if (hue < 0) hue += 360;

            double saturation = (max == 0) ? 0 : delta / max;
            double value = max;

            if (saturation < 0.2 || value < 0.2) return "Other";

            if (hue >= 0 && hue <= 30 || hue >= 330) return "Red";
            if (hue >= 40 && hue <= 80) return "Yellow";
            if (hue >= 90 && hue <= 150) return "Green";
            if (hue >= 200 && hue <= 260) return "Blue";

            return "Other";
        }

        private void DetectShapes(PictureBox originalPictureBox, PictureBox processedPictureBox)
        {
            if (originalImage == null)
            {
                MessageBox.Show("Please load an image first.");
                return;
            }

            Mat image = BitmapToMat(originalImage);
            processedImage = new Bitmap(originalImage.Width, originalImage.Height);

            Mat grayscale = new Mat();
            CvInvoke.CvtColor(image, grayscale, ColorConversion.Bgr2Gray);

            CvInvoke.GaussianBlur(grayscale, grayscale, new Size(5, 5), 0);

            Mat edges = new Mat();
            CvInvoke.Canny(grayscale, edges, 20, 80); // Adjusted thresholds for better edge detection

            VectorOfVectorOfPoint contours = new VectorOfVectorOfPoint();
            Mat hierarchy = new Mat();
            CvInvoke.FindContours(edges, contours, hierarchy, RetrType.List, ChainApproxMethod.ChainApproxSimple);

            List<DetectedShape> detectedShapes = new List<DetectedShape>();
            using (Graphics g = Graphics.FromImage(processedImage))
            {
                g.DrawImage(originalImage, 0, 0);
                for (int i = 0; i < contours.Size; i++)
                {
                    VectorOfPoint contour = contours[i];
                    if (contour.Size < 3) continue; // Reduced from 5 to 3

                    DetectedShape shape = ClassifyShape(contour.ToArray());
                    if (shape != null && (shape.Confidence >= 0.5 || shape.Type == ShapeType.Other)) // Lowered confidence threshold to 0.5
                    {
                        bool isDuplicate = detectedShapes.Any(s =>
                            Math.Abs(s.CenterX - shape.CenterX) < 20 &&
                            Math.Abs(s.CenterY - shape.CenterY) < 20);

                        if (!isDuplicate)
                        {
                            if (shape.CenterX >= 0 && shape.CenterX < originalImage.Width &&
                                shape.CenterY >= 0 && shape.CenterY < originalImage.Height)
                            {
                                Color shapeColor = originalImage.GetPixel(shape.CenterX, shape.CenterY);
                                shape.Color = shapeColor;
                                shape.ColorCategory = ClassifyColor(shapeColor);
                            }

                            detectedShapes.Add(shape);
                            DrawShape(g, shape, contour.ToArray());
                        }
                    }
                }
            }

            processedPictureBox.Image = processedImage;

            listResults.Items.Clear();
            foreach (DetectedShape shape in detectedShapes)
            {
                ListViewItem item = new ListViewItem(shape.Type.ToString());
                item.SubItems.Add(shape.CenterX.ToString());
                item.SubItems.Add(shape.CenterY.ToString());
                item.SubItems.Add(shape.Size.ToString("F1"));
                item.SubItems.Add((shape.Confidence * 100).ToString("F1") + "%");
                listResults.Items.Add(item);
            }

            int circleCount = detectedShapes.Count(s => s.Type == ShapeType.Circle);
            int rectangleCount = detectedShapes.Count(s => s.Type == ShapeType.Rectangle);
            int triangleCount = detectedShapes.Count(s => s.Type == ShapeType.Triangle);
            int squareCount = detectedShapes.Count(s => s.Type == ShapeType.Square);
            int pentagonCount = detectedShapes.Count(s => s.Type == ShapeType.Pentagon);
            int hexagonCount = detectedShapes.Count(s => s.Type == ShapeType.Hexagon);
            int heptagonCount = detectedShapes.Count(s => s.Type == ShapeType.Heptagon);
            int diamondCount = detectedShapes.Count(s => s.Type == ShapeType.Diamond);
            int starCount = detectedShapes.Count(s => s.Type == ShapeType.Star);
            int otherCount = detectedShapes.Count(s => s.Type == ShapeType.Other);

            Console.WriteLine($"Circle: {circleCount}, Rectangle: {rectangleCount}, Triangle: {triangleCount}, " +
                $"Square: {squareCount}, Pentagon: {pentagonCount}, Hexagon: {hexagonCount}, " +
                $"Heptagon: {heptagonCount}, Diamond: {diamondCount}, Star: {starCount}, Other: {otherCount}");

            labelCircle.Text = $"Number of Circle: {circleCount}";
            labelRectangle.Text = $"Number of Rectangle: {rectangleCount}";
            labelTriangle.Text = $"Number of Triangle: {triangleCount}";
            labelSquare.Text = $"Number of Square: {squareCount}";

            int blueCount = detectedShapes.Count(s => s.ColorCategory == "Blue");
            int redCount = detectedShapes.Count(s => s.ColorCategory == "Red");
            int yellowCount = detectedShapes.Count(s => s.ColorCategory == "Yellow");
            int greenCount = detectedShapes.Count(s => s.ColorCategory == "Green");

            labelBlue.Text = $"Number of Blue: {blueCount}";
            labelRed.Text = $"Number of Red: {redCount}";
            labelYellow.Text = $"Number of Yellow: {yellowCount}";
            labelGreen.Text = $"Number of Green: {greenCount}";

            this.Refresh();
        }

        private Mat BitmapToMat(Bitmap bitmap)
        {
            using (var bmp = new Bitmap(bitmap))
            {
                return bmp.ToImage<Bgr, byte>().Mat;
            }
        }

        private DetectedShape ClassifyShape(Point[] contour)
        {
            double area = CvInvoke.ContourArea(new VectorOfPoint(contour));
            if (area < 100) return null; // Reduced from 300 to 100

            Moments moments = CvInvoke.Moments(new VectorOfPoint(contour));
            double cx, cy;
            if (moments.M00 == 0)
            {
                cx = contour.Average(p => p.X);
                cy = contour.Average(p => p.Y);
            }
            else
            {
                cx = moments.M10 / moments.M00;
                cy = moments.M01 / moments.M00;
            }

            VectorOfPoint simplifiedContour = new VectorOfPoint();
            CvInvoke.ApproxPolyDP(new VectorOfPoint(contour), simplifiedContour, CvInvoke.ArcLength(new VectorOfPoint(contour), true) * 0.01, true);
            simplified = simplifiedContour.ToArray();
            if (simplified.Length < 3) simplified = contour;

            double perimeter = CvInvoke.ArcLength(new VectorOfPoint(contour), true);
            double compactness = (4 * Math.PI * area) / (perimeter * perimeter);
            double[] distances = contour.Select(p => Math.Sqrt((p.X - cx) * (p.X - cx) + (p.Y - cy) * (p.Y - cy))).ToArray();
            double meanDist = distances.Average();
            double stdDev = Math.Sqrt(distances.Select(d => (d - meanDist) * (d - meanDist)).Average());
            double circularity = stdDev / meanDist;

            int corners = simplified.Length;
            double[] sideLengths = null;
            if (corners == 4 && CvInvoke.IsContourConvex(new VectorOfPoint(simplified)))
            {
                sideLengths = new double[4];
                for (int i = 0; i < 4; i++)
                {
                    int next = (i + 1) % 4;
                    sideLengths[i] = Math.Sqrt(Math.Pow(simplified[next].X - simplified[i].X, 2) +
                                               Math.Pow(simplified[next].Y - simplified[i].Y, 2));
                }
            }

            using (System.IO.StreamWriter file = new System.IO.StreamWriter("shape_metrics.txt", true))
            {
                file.WriteLine($"Shape at ({cx},{cy}): Circularity={circularity:F3}, Compactness={compactness:F3}, Corners={corners}" +
                              (sideLengths != null ? $", SideLengths={string.Join(",", sideLengths.Select(s => s.ToString("F1")))}" : ""));
            }

            DetectedShape shape = new DetectedShape
            {
                CenterX = (int)cx,
                CenterY = (int)cy,
                Size = meanDist * 2,
                Metrics = new Dictionary<string, double>
                {
                    { "Circularity", circularity },
                    { "Compactness", compactness },
                    { "Corners", corners }
                }
            };

            double circleScore = ScoreForCircle(circularity, compactness, corners);
            double rectangleScore = ScoreForRectangle(circularity, compactness, corners, sideLengths);
            double triangleScore = ScoreForTriangle(simplified.ToList(), circularity, compactness, corners);
            double squareScore = ScoreForSquare(circularity, compactness, corners, sideLengths);
            double pentagonScore = ScoreForPentagon(circularity, compactness, corners);
            double hexagonScore = ScoreForHexagon(circularity, compactness, corners);
            double heptagonScore = ScoreForHeptagon(circularity, compactness, corners);
            double diamondScore = ScoreForDiamond(circularity, compactness, corners, sideLengths);
            double starScore = ScoreForStar(circularity, compactness, corners);

            Dictionary<ShapeType, double> scores = new Dictionary<ShapeType, double>
            {
                { ShapeType.Circle, circleScore },
                { ShapeType.Rectangle, rectangleScore },
                { ShapeType.Triangle, triangleScore },
                { ShapeType.Square, squareScore },
                { ShapeType.Pentagon, pentagonScore },
                { ShapeType.Hexagon, hexagonScore },
                { ShapeType.Heptagon, heptagonScore },
                { ShapeType.Diamond, diamondScore },
                { ShapeType.Star, starScore }
            };

            using (System.IO.StreamWriter file = new System.IO.StreamWriter("shape_metrics.txt", true))
            {
                file.WriteLine($"Shape at ({cx},{cy}): Scores: Circle={circleScore:F3}, Rectangle={rectangleScore:F3}, " +
                    $"Triangle={triangleScore:F3}, Square={squareScore:F3}, Pentagon={pentagonScore:F3}, " +
                    $"Hexagon={hexagonScore:F3}, Heptagon={heptagonScore:F3}, Diamond={diamondScore:F3}, Star={starScore:F3}");
            }

            var bestMatch = scores.OrderByDescending(x => x.Value).First();
            if (bestMatch.Value < 0.5) // Adjusted to match the lowered threshold
            {
                shape.Type = ShapeType.Other;
                shape.Confidence = 0.5;
            }
            else
            {
                shape.Type = bestMatch.Key;
                shape.Confidence = bestMatch.Value;
            }

            return shape;
        }

        private void DrawShape(Graphics g, DetectedShape shape, Point[] contour)
        {
            Pen circlePen = new Pen(Color.Green, 2);
            Pen rectanglePen = new Pen(Color.Blue, 2);
            Pen trianglePen = new Pen(Color.Red, 2);
            Pen squarePen = new Pen(Color.Yellow, 2);
            Pen pentagonPen = new Pen(Color.Purple, 2);
            Pen hexagonPen = new Pen(Color.Orange, 2);
            Pen heptagonPen = new Pen(Color.Cyan, 2);
            Pen diamondPen = new Pen(Color.Magenta, 2);
            Pen starPen = new Pen(Color.Gold, 2);
            Pen otherPen = new Pen(Color.Gray, 2);
            Font font = new Font("Arial", 10, FontStyle.Bold);
            Brush textBrush = Brushes.White;
            Brush shadowBrush = Brushes.Black;
            Brush redDotBrush = Brushes.Red;

            Pen contourPen;
            string shapeLabel;
            switch (shape.Type)
            {
                case ShapeType.Circle:
                    contourPen = circlePen;
                    shapeLabel = "Circle";
                    break;
                case ShapeType.Rectangle:
                    contourPen = rectanglePen;
                    shapeLabel = "Rectangle";
                    break;
                case ShapeType.Triangle:
                    contourPen = trianglePen;
                    shapeLabel = "Triangle";
                    break;
                case ShapeType.Square:
                    contourPen = squarePen;
                    shapeLabel = "Square";
                    break;
                case ShapeType.Pentagon:
                    contourPen = pentagonPen;
                    shapeLabel = "Pentagon";
                    break;
                case ShapeType.Hexagon:
                    contourPen = hexagonPen;
                    shapeLabel = "Hexagon";
                    break;
                case ShapeType.Heptagon:
                    contourPen = heptagonPen;
                    shapeLabel = "Heptagon";
                    break;
                case ShapeType.Diamond:
                    contourPen = diamondPen;
                    shapeLabel = "Diamond";
                    break;
                case ShapeType.Star:
                    contourPen = starPen;
                    shapeLabel = "Star";
                    break;
                case ShapeType.Other:
                    contourPen = otherPen;
                    shapeLabel = "Other";
                    break;
                default:
                    return;
            }

            g.DrawPolygon(contourPen, contour);
            g.DrawString(shapeLabel, font, shadowBrush, shape.CenterX - 1, shape.CenterY - 1);
            g.DrawString(shapeLabel, font, textBrush, shape.CenterX, shape.CenterY);

            float dotSize = 5;
            float dotX = shape.CenterX - dotSize / 2;
            float dotY = shape.CenterY + 15;
            g.FillEllipse(redDotBrush, dotX, dotY, dotSize, dotSize);
        }

        private double ScoreForCircle(double circularity, double compactness, int corners)
        {
            double score = 0;
            score += (circularity < 0.015 ? 0.5 : (circularity < 0.03 ? 0.3 : 0.1));
            score += (compactness > 0.95 ? 0.4 : compactness * 0.4 / 0.95);
            score += (corners >= 12 ? 0.3 : (corners < 6 ? 0.1 : corners * 0.3 / 12));
            if (corners == 4) score -= 0.2;
            return Math.Min(1.0, score);
        }

        private double ScoreForRectangle(double circularity, double compactness, int corners, double[] sideLengths)
        {
            double score = 0;
            score += (circularity > 0.05 && circularity < 0.4 ? 0.2 : 0.1);
            score += (compactness > 0.7 && compactness < 0.85 ? 0.3 : 0.1);
            score += (corners == 4 ? 0.4 : 0.1);

            if (corners == 4 && sideLengths != null)
            {
                double angle1 = CalculateAngle(simplified[0], simplified[1], simplified[2]);
                double angle2 = CalculateAngle(simplified[1], simplified[2], simplified[3]);
                bool allAnglesNear90 = Math.Abs(angle1 - Math.PI / 2) < 0.3 && Math.Abs(angle2 - Math.PI / 2) < 0.3;
                if (allAnglesNear90) score += 0.3;

                double meanSideLength = sideLengths.Average();
                double variance = sideLengths.Select(s => Math.Pow(s - meanSideLength, 2)).Average();
                double sideRatio = Math.Sqrt(variance) / meanSideLength;
                if (sideRatio < 0.15) score -= 0.2;
            }
            return Math.Min(1.0, score);
        }

        private double ScoreForTriangle(List<Point> simplified, double circularity, double compactness, int corners)
        {
            double score = 0;
            score += (corners == 3 ? 0.6 : (corners >= 2 && corners <= 4 ? 0.3 : 0));
            score += (compactness > 0.3 && compactness < 0.7 ? 0.2 : 0.1);
            score += (circularity > 0.1 && circularity < 0.6 ? 0.2 : 0.1);

            if (corners == 3 && simplified.Count == 3)
            {
                double angle1 = CalculateAngle(simplified[0], simplified[1], simplified[2]);
                double angle2 = CalculateAngle(simplified[1], simplified[2], simplified[0]);
                double angle3 = CalculateAngle(simplified[2], simplified[0], simplified[1]);
                double angleSum = angle1 + angle2 + angle3;
                if (Math.Abs(angleSum - Math.PI) < 0.5) score += 0.3;
            }
            return Math.Min(1.0, score);
        }

        private double ScoreForSquare(double circularity, double compactness, int corners, double[] sideLengths)
        {
            double score = 0;
            if (corners == 4 && sideLengths != null)
            {
                double meanSideLength = sideLengths.Average();
                double variance = sideLengths.Select(s => Math.Pow(s - meanSideLength, 2)).Average();
                double sideRatio = Math.Sqrt(variance) / meanSideLength;
                score += (sideRatio < 0.15 ? 0.6 : (sideRatio < 0.2 ? 0.4 : 0.1));
                score += (compactness > 0.7 && compactness < 0.85 ? 0.2 : 0.1);

                double angle1 = CalculateAngle(simplified[0], simplified[1], simplified[2]);
                double angle2 = CalculateAngle(simplified[1], simplified[2], simplified[3]);
                bool allAnglesNear90 = Math.Abs(angle1 - Math.PI / 2) < 0.3 && Math.Abs(angle2 - Math.PI / 2) < 0.3;
                if (allAnglesNear90) score += 0.4;

                if (circularity < 0.05) score -= 0.2;
            }
            return Math.Min(1.0, score);
        }

        private double ScoreForPentagon(double circularity, double compactness, int corners)
        {
            double score = 0;
            score += (corners == 5 ? 0.6 : (corners >= 4 && corners <= 6 ? 0.3 : 0));
            score += (compactness > 0.5 && compactness < 0.8 ? 0.2 : 0.1);
            score += (circularity > 0.05 && circularity < 0.5 ? 0.2 : 0.1);

            if (corners == 5 && simplified.Length == 5)
            {
                double angleSum = 0;
                for (int i = 0; i < 5; i++)
                {
                    angleSum += CalculateAngle(simplified[i], simplified[(i + 1) % 5], simplified[(i + 2) % 5]);
                }
                if (Math.Abs(angleSum - 3 * Math.PI) < 0.5) score += 0.3;
            }
            return Math.Min(1.0, score);
        }

        private double ScoreForHexagon(double circularity, double compactness, int corners)
        {
            double score = 0;
            score += (corners == 6 ? 0.6 : (corners >= 5 && corners <= 7 ? 0.3 : 0));
            score += (compactness > 0.6 && compactness < 0.9 ? 0.2 : 0.1);
            score += (circularity > 0.03 && circularity < 0.4 ? 0.2 : 0.1);

            if (corners == 6 && simplified.Length == 6)
            {
                double angleSum = 0;
                for (int i = 0; i < 6; i++)
                {
                    angleSum += CalculateAngle(simplified[i], simplified[(i + 1) % 6], simplified[(i + 2) % 6]);
                }
                if (Math.Abs(angleSum - 4 * Math.PI) < 0.5) score += 0.3;
            }
            return Math.Min(1.0, score);
        }

        private double ScoreForHeptagon(double circularity, double compactness, int corners)
        {
            double score = 0;
            score += (corners == 7 ? 0.6 : (corners >= 6 && corners <= 8 ? 0.3 : 0));
            score += (compactness > 0.6 && compactness < 0.9 ? 0.2 : 0.1);
            score += (circularity > 0.03 && circularity < 0.4 ? 0.2 : 0.1);

            if (corners == 7 && simplified.Length == 7)
            {
                double angleSum = 0;
                for (int i = 0; i < 7; i++)
                {
                    angleSum += CalculateAngle(simplified[i], simplified[(i + 1) % 7], simplified[(i + 2) % 7]);
                }
                if (Math.Abs(angleSum - 5 * Math.PI) < 0.5) score += 0.3;
            }
            return Math.Min(1.0, score);
        }

        private double ScoreForDiamond(double circularity, double compactness, int corners, double[] sideLengths)
        {
            double score = 0;
            if (corners == 4 && sideLengths != null)
            {
                double meanSideLength = sideLengths.Average();
                double variance = sideLengths.Select(s => Math.Pow(s - meanSideLength, 2)).Average();
                double sideRatio = Math.Sqrt(variance) / meanSideLength;
                score += (sideRatio < 0.15 ? 0.6 : (sideRatio < 0.2 ? 0.4 : 0.1));
                score += (compactness > 0.5 && compactness < 0.8 ? 0.2 : 0.1);

                double angle1 = CalculateAngle(simplified[0], simplified[1], simplified[2]);
                double angle2 = CalculateAngle(simplified[1], simplified[2], simplified[3]);
                double angle3 = CalculateAngle(simplified[2], simplified[3], simplified[0]);
                double angle4 = CalculateAngle(simplified[3], simplified[0], simplified[1]);
                bool anglesMatch = Math.Abs(angle1 - angle3) < 0.3 && Math.Abs(angle2 - angle4) < 0.3;
                if (anglesMatch) score += 0.4;

                bool allAnglesNear90 = Math.Abs(angle1 - Math.PI / 2) < 0.3 && Math.Abs(angle2 - Math.PI / 2) < 0.3;
                if (allAnglesNear90) score -= 0.3;
            }
            return Math.Min(1.0, score);
        }

        private double ScoreForStar(double circularity, double compactness, int corners)
        {
            double score = 0;
            score += (corners >= 8 && corners <= 12 ? 0.6 : (corners >= 6 && corners <= 14 ? 0.3 : 0));
            score += (compactness > 0.2 && compactness < 0.5 ? 0.3 : 0.1);
            score += (circularity > 0.1 && circularity < 0.5 ? 0.2 : 0.1);

            if (!CvInvoke.IsContourConvex(new VectorOfPoint(simplified)))
            {
                score += 0.4;
            }

            if (corners >= 8 && corners % 2 == 0)
            {
                double angleSum = 0;
                for (int i = 0; i < corners; i++)
                {
                    angleSum += CalculateAngle(simplified[i], simplified[(i + 1) % corners], simplified[(i + 2) % corners]);
                }
                if (Math.Abs(angleSum - (corners - 2) * Math.PI) < 1.0) score += 0.3;
            }

            return Math.Min(1.0, score);
        }

        private double CalculateAngle(Point p1, Point p2, Point p3)
        {
            double v1x = p1.X - p2.X;
            double v1y = p1.Y - p2.Y;
            double v2x = p3.X - p2.X;
            double v2y = p3.Y - p2.Y;
            double dot = v1x * v2x + v1y * v2y;
            double mag1 = Math.Sqrt(v1x * v1x + v1y * v1y);
            double mag2 = Math.Sqrt(v2x * v2x + v2y * v2y);
            double cosAngle = dot / (mag1 * mag2);
            cosAngle = Math.Max(-1.0, Math.Min(1.0, cosAngle));
            return Math.Acos(cosAngle);
        }

        public enum ShapeType
        {
            Circle,
            Rectangle,
            Triangle,
            Square,
            Pentagon,
            Hexagon,
            Heptagon,
            Diamond,
            Star,
            Other
        }

        public class DetectedShape
        {
            public ShapeType Type { get; set; }
            public int CenterX { get; set; }
            public int CenterY { get; set; }
            public double Size { get; set; }
            public double Confidence { get; set; }
            public Dictionary<string, double> Metrics { get; set; }
            public Color Color { get; set; }
            public string ColorCategory { get; set; }
        }

        private void btnDetectColor_Click(object sender, EventArgs e)
        {
            if (originalImage == null)
            {
                MessageBox.Show("Please load an image first.");
                return;
            }

            Bitmap imageCopy = new Bitmap(originalImage);
            List<ColorInfo> dominantColors = DetectDominantColors(imageCopy, 5);
            DisplayDetectedColors(dominantColors);
            VisualizeColorRegions(imageCopy, dominantColors);
            pictureBoxProcessed.Image = imageCopy;
        }

        public class ColorInfo
        {
            public Color Color { get; set; }
            public int Count { get; set; }
            public double Percentage { get; set; }
            public string HexValue { get; set; }
            public List<Point> Pixels { get; set; } = new List<Point>();
        }

        private List<ColorInfo> DetectDominantColors(Bitmap image, int maxColors)
        {
            Dictionary<int, ColorInfo> colorFrequency = new Dictionary<int, ColorInfo>();
            int totalPixels = image.Width * image.Height;

            for (int y = 0; y < image.Height; y++)
            {
                for (int x = 0; x < image.Width; x++)
                {
                    Color pixelColor = image.GetPixel(x, y);
                    int r = (pixelColor.R / 16) * 16;
                    int g = (pixelColor.G / 16) * 16;
                    int b = (pixelColor.B / 16) * 16;
                    Color quantizedColor = Color.FromArgb(r, g, b);
                    int colorKey = (r << 16) | (g << 8) | b;

                    if (!colorFrequency.ContainsKey(colorKey))
                    {
                        colorFrequency[colorKey] = new ColorInfo
                        {
                            Color = quantizedColor,
                            Count = 1,
                            HexValue = $"#{r:X2}{g:X2}{b:X2}"
                        };
                    }
                    colorFrequency[colorKey].Count++;
                    colorFrequency[colorKey].Pixels.Add(new Point(x, y));
                }
            }

            foreach (var info in colorFrequency.Values)
            {
                info.Percentage = (double)info.Count / totalPixels * 100;
            }

            return colorFrequency.Values.OrderByDescending(c => c.Count).Take(maxColors).ToList();
        }

        private void DisplayDetectedColors(List<ColorInfo> colors)
        {
            listViewColor.Items.Clear();
            foreach (var colorInfo in colors)
            {
                ListViewItem item = new ListViewItem(colorInfo.HexValue);
                item.BackColor = colorInfo.Color;
                item.SubItems.Add($"{colorInfo.Color.R}, {colorInfo.Color.G}, {colorInfo.Color.B}");
                item.SubItems.Add($"{colorInfo.Percentage:F2}%");
                listViewColor.Items.Add(item);
            }
        }

        private void VisualizeColorRegions(Bitmap image, List<ColorInfo> colors)
        {
            using (Graphics g = Graphics.FromImage(image))
            {
                foreach (var colorInfo in colors)
                {
                    Color contrastColor = GetContrastColor(colorInfo.Color);
                    using (Pen pen = new Pen(contrastColor, 2))
                    {
                        foreach (var pixel in colorInfo.Pixels.Take(50))
                        {
                            Rectangle rect = new Rectangle(pixel.X - 5, pixel.Y - 5, 10, 10);
                            g.DrawRectangle(pen, rect);
                        }
                    }
                }
            }
        }

        private Color GetContrastColor(Color color)
        {
            double luminance = (0.299 * color.R + 0.587 * color.G + 0.114 * color.B) / 255;
            return luminance > 0.5 ? Color.Black : Color.White;
        }
    }
}