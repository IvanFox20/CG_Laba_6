namespace CG_Laba_6
{
    public partial class Main_Form : Form
    {
        private const int GridWidth = 630;
        private const int GridHeight = 630;
        private const int CellSize = 30;
        private Graphics g;
        private Bitmap bitmap;
        private List<Action> drawActions = new List<Action>();

        public Main_Form()
        {
            InitializeComponent();
            bitmap = new Bitmap(GridWidth, GridHeight);
            g = Graphics.FromImage(bitmap);
            ConvexHull();
        }

        private void nextStep_button_Click(object sender, EventArgs e)
        {
            ExecuteNextAction();
        }

        private async void ConvexHull()
        {
            List<PointF> startPoints = FileInput();
            startPoints = SortPoints(startPoints);
            int n = startPoints.Count;
            List<bool> flags = Enumerable.Repeat(true, n).ToList();
            for (int i = 0; i < n;i++)
            {
                for (int j = i +1; j < n;j++)
                {
                    for (int k = j + 1; k < n;k++)
                    {
                        List<PointF> convertedStartPoints = PointsConvertionToCoordinates(startPoints);
                        List<PointF> currentIterationTriangle = new List<PointF>(3) { startPoints[i], startPoints[j], startPoints[k] };
                        currentIterationTriangle = PointsConvertionToCoordinates(currentIterationTriangle);
                        ClearCanvas();
                        DrawGrid();
                        DrawPoints(Brushes.Black, convertedStartPoints);
                        g.DrawPolygon(Pens.Blue, currentIterationTriangle.ToArray());
                        DrawPoints(Brushes.Magenta, currentIterationTriangle);
                        await Task.Delay(300);
                        for (int l = 0; l < n;l++)
                        {
                            if (l == i || l == j || l == k) continue;
                            if (IsPointInTriangle(startPoints[i], startPoints[j], startPoints[k], startPoints[l]))
                            {
                                List<(int, int)> pairs = new List<(int, int)>() { (i, j), (j, k), (i, k) };
                                if (!pairs.Any(p => ArePointsCollenear(startPoints[p.Item1], startPoints[p.Item2], startPoints[l])))
                                {
                                    flags[l] = false;
                                }
                            }
                        }
                    }
                }
            }
            List<PointF> convexHull = new List<PointF>();
            for(int i = 0; i < n;i++)
            {
                if (flags[i])
                {
                    convexHull.Add(startPoints[i]);
                }
            }
            DrawConvexHull(startPoints, convexHull);
        }

        private void DrawConvexHull(List<PointF> startPoints, List<PointF> convexHull)
        {
            ClearCanvas();
            DrawGrid();
            List<PointF> convertedStartPoints = PointsConvertionToCoordinates(startPoints);
            List<PointF> convertedConvexHullPoints = PointsConvertionToCoordinates(convexHull);
            DrawPoints(Brushes.Black, convertedStartPoints);
            DrawPoints(Brushes.Magenta, convertedConvexHullPoints);
            g.DrawPolygon(Pens.Red, convertedConvexHullPoints.ToArray());
        }

        double AreaCalculation(PointF a, PointF b, PointF c)
        {
            return Math.Abs((a.X * (b.Y - c.Y) + b.X * (c.Y - a.Y) + c.X * (a.Y - b.Y)) / 2);
        }

        bool IsPointInTriangle(PointF a, PointF b, PointF c, PointF testPoint)
        {
            double eps = 0.001;
            double fullArea = AreaCalculation(a, b, c);
            double area1 = AreaCalculation(testPoint, b, c);
            double area2 = AreaCalculation(a, testPoint, c);
            double area3 = AreaCalculation(a, b, testPoint);
            return (Math.Abs(fullArea - (area1 + area2 + area3)) < eps);
        }

        bool ArePointsCollenear(PointF a, PointF b, PointF c)
        {

            return (c.Y - a.Y) * (b.X - a.X) == (b.Y - a.Y) * (c.X - a.X);
        }

        //private void EndDrawing(List<PointF> convertedStartPoints, List<PointF> convertedConvexHull)
        //{
        //    ClearCanvas();
        //    DrawGrid();
        //    drawPoints(Brushes.Black, convertedStartPoints);
        //    drawPoints(Brushes.Green, convertedConvexHull);
        //    g.DrawPolygon(Pens.Blue, convertedConvexHull.ToArray());
        //}

        //private List<PointF> BruteForceConvexHull(List<PointF> points)
        //{
        //    List<PointF> convexHull = new List<PointF>();
        //    for (int i = 0; i < points.Count; i++)
        //    {
        //        for (int j = 0; j < points.Count; j++)
        //        {
        //            if (i == j)
        //                continue;

        //            bool isConvex = true;
        //            List<PointF> currentConvexHullPoints = new List<PointF>(convexHull);
        //            List<PointF> currentInnerPoints = new List<PointF>();
        //            for (int k = 0; k < points.Count; k++)
        //            {
        //                List<PointF> currentIterInnerPoints = new List<PointF>(currentInnerPoints);
        //                if (k == i || k == j)
        //                    continue;
        //                PointF a = points[i];
        //                PointF b = points[j];
        //                PointF c = points[k];
        //                float crossProduct = CrossProduct(a, b, c);

        //                if (crossProduct < 0)
        //                {
        //                    isConvex = false;
        //                   drawActions.Add(() => DrawCurrentIteration(a, b, c, currentConvexHullPoints, currentIterInnerPoints, isConvex));
        //                    break;
        //                }
        //                currentInnerPoints.Add(c);
        //                drawActions.Add(() => DrawCurrentIteration(a,b,c,currentConvexHullPoints,currentIterInnerPoints, isConvex));
        //            }

        //            if (isConvex)
        //            {
        //                if (!convexHull.Contains(points[i]))
        //                    convexHull.Add(points[i]);
        //                if (!convexHull.Contains(points[j]))
        //                    convexHull.Add(points[j]);
        //            }
        //        }
        //    }
        //    return convexHull;
        //}

        //private void DrawCurrentIteration(PointF a, PointF b, PointF c, List<PointF> currentConvexHull, List<PointF> currentInnerPoints, bool isConvex)
        //{
        //    ClearCanvas();
        //    DrawGrid();
        //    List<PointF> convertedStartPoints = PointsConvertionToCoordinates(startPoints);
        //    List<PointF> convertedCurrentConvexHull = PointsConvertionToCoordinates(currentConvexHull);
        //    List<PointF> convertedInnerPoints = PointsConvertionToCoordinates(currentInnerPoints);
        //    drawPoints(Brushes.Black, convertedStartPoints);
        //    drawPoints(Brushes.Green, convertedCurrentConvexHull);
        //    drawPoints(Brushes.Yellow, convertedInnerPoints);
        //    PointF convertedA = PointConvertToCoordinates(a);
        //    PointF convertedB = PointConvertToCoordinates(b);
        //    PointF convertedC = PointConvertToCoordinates(c);
        //    drawPoint(Brushes.Magenta, convertedA);
        //    drawPoint(Brushes.Magenta, convertedB);
        //    g.DrawLine(Pens.Blue, convertedA, convertedB);
        //    Brush isConvexBrush = Brushes.Yellow;
        //    if(!isConvex)
        //    {
        //        isConvexBrush = Brushes.Red;
        //    }
        //    drawPoint(isConvexBrush, convertedC);
        //}

        //private static float CrossProduct(PointF A, PointF B, PointF C)
        //{
        //    return (B.X - A.X) * (C.Y - A.Y) - (B.Y - A.Y) * (C.X - A.X);
        //}

        private List<PointF> SortPoints(List<PointF> points)
        {
            PointF startPoint = points.OrderBy(p => p.Y).ThenBy(p => p.X).First();
            List<PointF> sortedPoints = new List<PointF>(points);
            sortedPoints.Sort((a, b) =>
            {
                double angleA = Math.Atan2(a.Y - startPoint.Y, a.X - startPoint.X);
                double angleB = Math.Atan2(b.Y - startPoint.Y, b.X - startPoint.X);

                if (angleA < angleB) return -1;
                if (angleA > angleB) return 1;

                double distanceA = Math.Pow(a.X - startPoint.X, 2) + Math.Pow(a.Y - startPoint.Y, 2);
                double distanceB = Math.Pow(b.X - startPoint.X, 2) + Math.Pow(b.Y - startPoint.Y, 2);

                if (distanceA < distanceB) return -1;
                if (distanceA > distanceB) return 1;

                return 0;
            });

            return sortedPoints;
        }

        PointF PointConvertToCoordinates(PointF point)
        {
            float newX = GridWidth / 2 + point.X * CellSize - CellSize / 2;
            float newY = GridHeight / 2 - point.Y * CellSize - CellSize / 2;
            PointF convertedPoint = new PointF(newX, newY);
            return convertedPoint;
        }
        List<PointF> PointsConvertionToCoordinates(List<PointF> points)
        {
            List<PointF> newPoints = new List<PointF>();
            foreach (PointF point in points)
            {
                float newX = GridWidth / 2 + point.X * CellSize - CellSize / 2;
                float newY = GridHeight / 2 - point.Y * CellSize - CellSize / 2;
                newPoints.Add(new PointF(newX, newY));
            }
            return newPoints;
        }

        public void DrawPoint(Brush brush, PointF point)
        {
            int r = 10;
            g.FillEllipse(brush, point.X - r / 2, point.Y - r / 2, r, r);
        }
        public void DrawPoints(Brush brush, List<PointF> points)
        {
            int r = 10;
            foreach (var point in points)
            {
                g.FillEllipse(brush, point.X - r / 2, point.Y - r / 2, r, r);
            }
        }

        List<PointF> FileInput()
        {
            List<PointF> startPoints = new List<PointF>();
            try
            {
                using (StreamReader sr = new StreamReader("PointsInput.txt"))
                {
                    string line;
                    while ((line = sr.ReadLine()) != null)
                    {
                        string[] coordinates = line.Split(' ');
                        float x = float.Parse(coordinates[0]);
                        float y = float.Parse(coordinates[1]);
                        startPoints.Add(new PointF(x, y));
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("The file could not be read:");
                Console.WriteLine(e.Message);
            }
            return startPoints;
        }
        private void ClearCanvas()
        {
            draw_pictureBox.Refresh();
            g.Clear(Color.White);
        }

        private void DrawGrid()
        {
            using (Graphics g = Graphics.FromImage(bitmap))
            {
                for (int xPos = 0; xPos <= GridWidth; xPos += CellSize)
                {
                    g.DrawLine(Pens.Black, xPos, 0, xPos, GridHeight);
                    int value = (xPos / CellSize) - 10;
                    string text = value.ToString();
                    SizeF textSize = g.MeasureString(text, this.Font);
                    float textX = xPos + (CellSize - textSize.Width) / 2;
                    float textY = GridHeight - 15;
                    g.DrawString(text, this.Font, Brushes.Black, textX, textY);
                }

                for (int yPos = 0; yPos <= GridHeight; yPos += CellSize)
                {
                    g.DrawLine(Pens.Black, 0, yPos, GridWidth, yPos);
                    int value = 10 - (yPos / CellSize);
                    g.DrawString(value.ToString(), this.Font, Brushes.Black, 0, yPos + 2);
                }
            }
            draw_pictureBox.Image = bitmap;
        }

        private void ExecuteNextAction()
        {
            if (drawActions.Count > 0)
            {
                drawActions[0]();
                drawActions.RemoveAt(0);
            }
        }
    }
}