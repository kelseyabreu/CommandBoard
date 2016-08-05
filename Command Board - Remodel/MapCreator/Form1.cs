using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Windows.Forms;
using System.Windows.Input;

namespace MapCreator
{
    public partial class Form1 : Form
    {
        int[][] values;
        System.Drawing.Rectangle[][] rectangleGrid;
        System.Drawing.Rectangle[][] circleGrid;
        Image smallBlueHome = Image.FromFile("C:\\Users\\Kelsey\\Desktop\\smallBlueHome.png");
        Image bigBlueHome = Image.FromFile("C:\\Users\\Kelsey\\Desktop\\bigBlueHome.png");
        Image smallBlueCloud = Image.FromFile("C:\\Users\\Kelsey\\Desktop\\smallBlueCloud.png");
        Image bigBlueCloud = Image.FromFile("C:\\Users\\Kelsey\\Desktop\\bigBlueCloud.png");
        int[][] types;
        int type;
        int[][] rotation;
        Size gridSize;
        System.Drawing.Pen[][] rectColor;
        System.Drawing.Pen[][] shapeColor;
        System.Drawing.SolidBrush[][] innerRectColor;
        System.Drawing.SolidBrush[][] innerShapeColor;
        System.Drawing.Pen prevRectColor = new Pen(Color.Red, 1f);
        System.Drawing.Pen prevShapeColor = new Pen(Color.Black, 4f);
        System.Drawing.SolidBrush prevInnerShapeBrush = new SolidBrush(Color.Black);
        System.Drawing.SolidBrush prevInnerRectBrush = new SolidBrush(Color.Red);
        List<Location> listOfSelection = new List<Location>();
        int displayedRowIndex = -1;
        int displayedColumnIndex = -1;
        int visible = 0;

        public Form1()
        {
            InitializeComponent();
            selectionBox.SelectedIndex = 0;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != "" && textBox2.Text != "")
            {
                DrawIt();
            }
        }

        private void DrawIt()
        {
            int row = Convert.ToInt32(textBox1.Text);
            int column = Convert.ToInt32(textBox2.Text);
            gridSize = new Size(column, row);
            visible = 1;

            rectangleGrid = converter.ToJagged(new System.Drawing.Rectangle[row,column]);
            circleGrid = converter.ToJagged(new System.Drawing.Rectangle[row,column]);
            rectColor = converter.ToJagged(new System.Drawing.Pen[row,column]);
            shapeColor = converter.ToJagged(new System.Drawing.Pen[row,column]);
            innerRectColor = converter.ToJagged(new SolidBrush[row,column]);
            innerShapeColor = converter.ToJagged(new SolidBrush[row,column]);
            types = converter.ToJagged(new int[row,column]);
            rotation = converter.ToJagged(new int[row,column]);
            values = converter.ToJagged(new int[row,column]);

            flowLayoutPanel1.Width = (column * 50) + column;
            flowLayoutPanel1.Height = (row * 50) + row;

            System.Drawing.Graphics graphics = flowLayoutPanel1.CreateGraphics();
            graphics.Clear(Form1.ActiveForm.BackColor);

            for (int i = 0; i < row; i++)
            {
                for (int j = 0; j < column; j++)
                {
                    System.Drawing.Rectangle rectangle = new System.Drawing.Rectangle(50 * j + j, 50 * i + i, 50, 50);
                    System.Drawing.Rectangle shapeRect = new System.Drawing.Rectangle(50 * j + j + 5, 50 * i + i + 5, 40, 40);

                    rectangleGrid[i][j] = rectangle;
                    circleGrid[i][j] = shapeRect;

                    types[i][j] = 0;

                    rectColor[i][j] = new Pen(Color.Red, 1f);
                    innerRectColor[i][j] = new SolidBrush(Form1.ActiveForm.BackColor);
                    shapeColor[i][j] = new Pen(Color.Black, 9f);
                    innerShapeColor[i][j] = new SolidBrush(Form1.ActiveForm.BackColor);
                }
            }
            flowLayoutPanel1_Paint(new Object(), new PaintEventArgs(flowLayoutPanel1.CreateGraphics(), new Rectangle()));

        }

        private void flowLayoutPanel1_MouseClick(object sender, MouseEventArgs e)
        {
            if (visible == 1)
            {
                displayedRowIndex = e.Y / 51;
                displayedColumnIndex = e.X / 51;

                if (displayedRowIndex == gridSize.Height)
                    displayedRowIndex--;

                if (displayedColumnIndex == gridSize.Width)
                    displayedColumnIndex--;

                selectionBox.SelectedIndex = types[displayedRowIndex][displayedColumnIndex];

                if (Control.ModifierKeys == Keys.Control)
                {
                    listOfSelection.Add(new Location(displayedColumnIndex, displayedRowIndex));
                }
                else
                {
                    listOfSelection.Clear();
                }

                prevRectColor = rectColor[displayedRowIndex][displayedColumnIndex];
                prevInnerRectBrush = innerRectColor[displayedRowIndex][displayedColumnIndex];

                prevShapeColor = shapeColor[displayedRowIndex][displayedColumnIndex];
                prevInnerShapeBrush = innerShapeColor[displayedRowIndex][displayedColumnIndex];

                valueLabel.Text = values[displayedRowIndex][displayedColumnIndex].ToString();

                previewIt(prevShapeColor, prevRectColor, prevInnerShapeBrush, prevInnerRectBrush);
            }
        }

        private void previewIt(Pen shapePen, Pen rectPen, SolidBrush shapeBrush, SolidBrush rectBrush)
        {
            System.Drawing.Graphics graphics = propertyLayoutPanel.CreateGraphics();
            graphics.Clear(Form1.DefaultBackColor);
            System.Drawing.Rectangle rectangle = new System.Drawing.Rectangle(1, 1, 200, 200);
            System.Drawing.Rectangle shapeRect = new System.Drawing.Rectangle(21, 21, 160, 160);


            graphics.FillRectangle(rectBrush, rectangle);

            if (selectionBox.SelectedItem.Equals("Properties"))
            {
                shapePen.Width = 40f;
                graphics.DrawEllipse(shapePen, shapeRect);
                graphics.FillEllipse(shapeBrush, shapeRect);
                shapePen.Width = 9f;
            }
            else if (selectionBox.SelectedItem.Equals("Home"))
            {
                graphics.DrawImage(bigBlueHome, rectangle);
            }
            else if (selectionBox.SelectedItem.Equals("MovingCloud"))
            {
                graphics.DrawImage(bigBlueCloud, rectangle);
            }
            else if (selectionBox.SelectedItem.Equals("Lines"))
            {
                Point start;
                Point finish;

                if (rotation[displayedRowIndex][displayedColumnIndex] == 1)
                {
                    start = new Point(0, 101);
                    finish = new Point(200, 101);
                }
                else
                {
                    start = new Point(100, 1);
                    finish = new Point(100, 201);
                }

                graphics.DrawLine(shapePen, start, finish);

            }
            else if (selectionBox.SelectedItem.Equals("Deadzones"))
            {
                Point start1, start2;
                Point finish1, finish2;
                shapePen.Width = 40f;
                start1 = new Point(20, 20);
                start2 = new Point(20, 180);
                finish1 = new Point(180, 20);
                finish2 = new Point(180, 180);

                graphics.DrawLine(shapePen, start1, finish2);
                graphics.DrawLine(shapePen, start2, finish1);
                shapePen.Width = 9f;

            }
            else if (selectionBox.SelectedItem.Equals("Checkpoints"))
            {
                Point[] bigDiamond = { new Point(100, 20), new Point(40, 100), new Point(100, 180), new Point(160, 100) };
                Point[] miniDiamondLeft1 = { new Point(40, 12), new Point(20, 40), new Point(40, 68), new Point(60, 40) };
                Point[] miniDiamondLeft2 = { new Point(40, 132), new Point(20, 160), new Point(40, 188), new Point(60, 160) };
                Point[] miniDiamondRight1 = { new Point(160, 12), new Point(140, 40), new Point(160, 68), new Point(180, 40) };
                Point[] miniDiamondRight2 = { new Point(160, 132), new Point(140, 160), new Point(160, 188), new Point(180, 160) };

                graphics.DrawPolygon(shapePen, miniDiamondLeft1);
                graphics.FillPolygon(shapeBrush, miniDiamondLeft1);

                graphics.DrawPolygon(shapePen, miniDiamondLeft2);
                graphics.FillPolygon(shapeBrush, miniDiamondLeft2);

                graphics.DrawPolygon(shapePen, miniDiamondRight1);
                graphics.FillPolygon(shapeBrush, miniDiamondRight1);

                graphics.DrawPolygon(shapePen, miniDiamondRight2);
                graphics.FillPolygon(shapeBrush, miniDiamondRight2);

                graphics.DrawPolygon(shapePen, bigDiamond);
                graphics.FillPolygon(shapeBrush, bigDiamond);
            }
            else if (selectionBox.SelectedItem.Equals("Specials"))
            {
                Point[] star = { new Point(25,5),new Point(20,21), new Point(5,21),
                                         new Point(17,30),new Point(13,46),new Point(25,37),
                                         new Point(37,46),new Point(33,30),new Point(45,21), new Point(30,21)};

                for (int i = 0; i < 10; i++)
                {
                    star[i].X *= 4;
                    star[i].Y *= 4;
                }

                graphics.DrawPolygon(prevShapeColor, star);
                graphics.FillPolygon(prevInnerShapeBrush, star);

            }
            graphics.DrawRectangle(rectPen, rectangle);
        }


        private void shapeColorButton_Click(object sender, EventArgs e)
        {
            if (displayedColumnIndex != -1)
            {
                if (shapeColorDiag.ShowDialog() == DialogResult.OK)
                {
                    prevShapeColor = new Pen(shapeColorDiag.Color, 9f);
                    previewIt(prevShapeColor, prevRectColor, prevInnerShapeBrush, prevInnerRectBrush);
                }
            }
        }

        private void rectColorButton_Click(object sender, EventArgs e)
        {
            if (displayedColumnIndex != -1)
            {
                if (rectColorDiag.ShowDialog() == DialogResult.OK)
                {
                    prevRectColor = new Pen(rectColorDiag.Color, 1f);
                    previewIt(prevShapeColor, prevRectColor, prevInnerShapeBrush, prevInnerRectBrush);
                }
            }
        }

        private void innerShapeButton_Click(object sender, EventArgs e)
        {
            if (displayedColumnIndex != -1)
            {
                if (innerShapeButton.Text.Equals("Rotate"))
                {
                    rotateButton_Click(new Object(), new EventArgs());
                }
                else if (innerShapeDiag.ShowDialog() == DialogResult.OK)
                {
                    prevInnerShapeBrush = new SolidBrush(innerShapeDiag.Color);
                    previewIt(prevShapeColor, prevRectColor, prevInnerShapeBrush, prevInnerRectBrush);
                }
            }
        }

        private void innerRectButton_Click(object sender, EventArgs e)
        {
            if (displayedColumnIndex != -1)
            {
                if (innerRectDiag.ShowDialog() == DialogResult.OK)
                {
                    prevInnerRectBrush = new SolidBrush(innerRectDiag.Color);
                    previewIt(prevShapeColor, prevRectColor, prevInnerShapeBrush, prevInnerRectBrush);
                }
            }
        }

        private void submitButton_Click(object sender, EventArgs e)
        {
            if (listOfSelection.Count > 1)
            {
                for (int i = 0; i < listOfSelection.Count; i++)
                {
                    int row = listOfSelection.ElementAt(i).row;
                    int column = listOfSelection.ElementAt(i).column;
                    redraw(row, column, 0);
                }
            }
            else
            {
                redraw(displayedRowIndex, displayedColumnIndex, 0);
            }
        }

        public void redraw(int row, int column, int save)
        {
            System.Drawing.Graphics graphics = flowLayoutPanel1.CreateGraphics();
            System.Drawing.Rectangle rectangle = rectangleGrid[row][column];
            System.Drawing.Rectangle shapeRect = circleGrid[row][column];

            if (save == 0)
            {
                types[row][column] = type;
                shapeColor[row][column] = prevShapeColor;
                rectColor[row][column] = prevRectColor;
                innerShapeColor[row][column] = prevInnerShapeBrush;
                innerRectColor[row][column] = prevInnerRectBrush;
            }

            drawSquare(row, column, shapeRect, rectangle, graphics);
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                BinaryFormatter bFormatter = new BinaryFormatter();
                FileStream outPut = new FileStream(saveFileDialog1.FileName, FileMode.OpenOrCreate, FileAccess.Write);

                Color[][] innerRectColors = converter.ToJagged(new Color[gridSize.Height, gridSize.Width]);
                Color[][] innerShapeColors = converter.ToJagged(new Color[gridSize.Height, gridSize.Width]);
                Color[][] rectColors = converter.ToJagged(new Color[gridSize.Height, gridSize.Width]);
                Color[][] shapeColors = converter.ToJagged(new Color[gridSize.Height, gridSize.Width]);

                for (int i = 0; i < gridSize.Height; i++)
                {
                    for (int j = 0; j < gridSize.Width; j++)
                    {
                        innerRectColors[i][j] = innerRectColor[i][j].Color;
                        innerShapeColors[i][j] = innerShapeColor[i][j].Color;
                        rectColors[i][j] = rectColor[i][j].Color;
                        shapeColors[i][j] = shapeColor[i][j].Color;
                    }
                }

                bFormatter.Serialize(outPut, gridSize);
                bFormatter.Serialize(outPut, innerRectColors);
                bFormatter.Serialize(outPut, innerShapeColors);
                bFormatter.Serialize(outPut, rectColors);
                bFormatter.Serialize(outPut, shapeColors);
                bFormatter.Serialize(outPut, types);
                bFormatter.Serialize(outPut, rotation);
                bFormatter.Serialize(outPut, values);
                bFormatter.Serialize(outPut, circleGrid);
                bFormatter.Serialize(outPut, rectangleGrid);

                outPut.Close();
            }
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() != DialogResult.Cancel)
            {
                BinaryFormatter bFormatter = new BinaryFormatter();
                FileStream inPut = new FileStream(openFileDialog1.FileName, FileMode.Open, FileAccess.Read);

                gridSize = (Size)bFormatter.Deserialize(inPut);
                textBox1.Text = gridSize.Height.ToString();
                textBox2.Text = gridSize.Width.ToString();

                DrawIt();

                Color[][] innerRectColors = converter.ToJagged(new Color[gridSize.Height, gridSize.Width]);
                Color[][] innerShapeColors = converter.ToJagged(new Color[gridSize.Height, gridSize.Width]);
                Color[][] rectColors = converter.ToJagged(new Color[gridSize.Height, gridSize.Width]);
                Color[][] shapeColors = converter.ToJagged(new Color[gridSize.Height, gridSize.Width]);

                innerRectColors = converter.ToJagged((Color[,])bFormatter.Deserialize(inPut));
                innerShapeColors = converter.ToJagged((Color[,])bFormatter.Deserialize(inPut));
                rectColors = converter.ToJagged((Color[,])bFormatter.Deserialize(inPut));
                shapeColors = converter.ToJagged((Color[,])bFormatter.Deserialize(inPut));
                types = converter.ToJagged((int[,])bFormatter.Deserialize(inPut));
                rotation = converter.ToJagged((int[,])bFormatter.Deserialize(inPut));
                values = converter.ToJagged((int[,])bFormatter.Deserialize(inPut));
                //circleGrid = (Rectangle[][])bFormatter.Deserialize(inPut);
                //rectangleGrid = (Rectangle[][])bFormatter.Deserialize(inPut);

                int row = gridSize.Height;
                int column = gridSize.Width;

                for (int i = 0; i < row; i++)
                {
                    for (int j = 0; j < column; j++)
                    {
                        rectColor[i][j] = new Pen(rectColors[i][j], 1f);
                        shapeColor[i][j] = new Pen(shapeColors[i][j], 9f);
                        innerRectColor[i][j] = new SolidBrush(innerRectColors[i][j]);
                        innerShapeColor[i][j] = new SolidBrush(innerShapeColors[i][j]);

                        selectionBox.SelectedIndex = types[i][j];
                    }
                }

                inPut.Close();
                flowLayoutPanel1_Paint(new Object(), new PaintEventArgs(flowLayoutPanel1.CreateGraphics(), new Rectangle()));
            }
        }

        private void selectionBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (selectionBox.SelectedItem.Equals("Properties"))
            {
                type = 0;
                shapeColorButton.Text = "Shape Color";
                innerShapeButton.Text = "Inner Shape";
            }
            else if (selectionBox.SelectedItem.Equals("Specials"))
            {
                type = 1;
                shapeColorButton.Text = "Shape Color";
                innerShapeButton.Text = "Inner Shape";
            }
            else if (selectionBox.SelectedItem.Equals("Checkpoints"))
            {
                type = 2;
                shapeColorButton.Text = "Shape Color";
                innerShapeButton.Text = "Inner Shape";
            }
            else if (selectionBox.SelectedItem.Equals("Lines"))
            {
                type = 3;
                shapeColorButton.Text = "Line Color";
                innerShapeButton.Text = "Rotate";
            }
            else if (selectionBox.SelectedItem.Equals("Home"))
            {
                type = 4;
                shapeColorButton.Text = "Shape Color";
                innerShapeButton.Text = "Inner Shape";
            }
            else if (selectionBox.SelectedItem.Equals("Deadzones"))
            {
                type = 5;
                shapeColorButton.Text = "Line Color";
                innerShapeButton.Text = "Rotate";
            }
            else if (selectionBox.SelectedItem.Equals("MovingCloud"))
            {
                type = 6;
                shapeColorButton.Text = "Shape Color";
                innerShapeButton.Text = "Inner Shape";
            }

            if (displayedColumnIndex != -1)
                previewIt(prevShapeColor, prevRectColor, prevInnerShapeBrush, prevInnerRectBrush);

        }

        private void rotateButton_Click(object sender, EventArgs e)
        {

            if (listOfSelection.Count > 1)
            {
                for (int i = 0; i < listOfSelection.Count; i++)
                {
                    int row = listOfSelection.ElementAt(i).row;
                    int column = listOfSelection.ElementAt(i).column;

                    if (rotation[row][column] == 1)
                        rotation[row][column] = 0;
                    else
                        rotation[row][column] = 1;
                }
            }
            else
            {
                if (rotation[displayedRowIndex][displayedColumnIndex] == 1)
                    rotation[displayedRowIndex][displayedColumnIndex] = 0;
                else
                    rotation[displayedRowIndex][displayedColumnIndex] = 1;
            }
            previewIt(prevShapeColor, prevRectColor, prevInnerShapeBrush, prevInnerRectBrush);
        }

        private void flowLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {
            System.Drawing.Graphics graphics = e.Graphics;

            int row = gridSize.Height;
            int column = gridSize.Width;

            for (int i = 0; i < row; i++)
            {
                for (int j = 0; j < column; j++)
                {
                    System.Drawing.Rectangle rectangle = rectangleGrid[i][j];
                    System.Drawing.Rectangle shapeRect = circleGrid[i][j];

                    prevRectColor = rectColor[i][j];
                    prevShapeColor = shapeColor[i][j];
                    prevInnerRectBrush = innerRectColor[i][j];
                    prevInnerShapeBrush = innerShapeColor[i][j];

                    selectionBox.SelectedIndex = types[i][j];

                    drawSquare(i, j, shapeRect, rectangle, graphics);
                }
            }
        }

        public void drawSquare(int row, int column, Rectangle shapeRect, Rectangle rectangle, Graphics graphics)
        {
            graphics.FillRectangle(prevInnerRectBrush, rectangle);

            if (selectionBox.SelectedItem.Equals("Properties"))
            {
                graphics.DrawEllipse(prevShapeColor, shapeRect);
                graphics.FillEllipse(prevInnerShapeBrush, shapeRect);
            }
            else if (selectionBox.SelectedItem.Equals("Home"))
            {
                graphics.DrawImage(smallBlueHome, rectangle);
            }
            else if (selectionBox.SelectedItem.Equals("MovingCloud"))
            {
                graphics.DrawImage(smallBlueCloud, rectangle);
            }
            else if (selectionBox.SelectedItem.Equals("Lines"))
            {
                Point start;
                Point finish;

                if (rotation[row][column] == 1)
                {
                    start = new Point(0, 25);
                    finish = new Point(50, 25);
                }
                else
                {
                    start = new Point(25, 1);
                    finish = new Point(25, 50);
                }

                start.X += rectangle.Location.X;
                start.Y += rectangle.Location.Y;

                finish.X += rectangle.Location.X;
                finish.Y += rectangle.Location.Y;

                graphics.DrawLine(prevShapeColor, start, finish);

            }
            else if (selectionBox.SelectedItem.Equals("Deadzones"))
            {
                Point start1, start2;
                Point finish1, finish2;

                start1 = new Point(5, 5);
                start2 = new Point(5, 45);
                finish1 = new Point(45, 5);
                finish2 = new Point(45, 45);

                start1.X += rectangle.Location.X;
                start1.Y += rectangle.Location.Y;

                finish1.X += rectangle.Location.X;
                finish1.Y += rectangle.Location.Y;

                start2.X += rectangle.Location.X;
                start2.Y += rectangle.Location.Y;

                finish2.X += rectangle.Location.X;
                finish2.Y += rectangle.Location.Y;

                graphics.DrawLine(prevShapeColor, start1, finish2);
                graphics.DrawLine(prevShapeColor, start2, finish1);

            }
            else if (selectionBox.SelectedItem.Equals("Checkpoints"))
            {
                Point[] bigDiamond = { new Point(25, 5), new Point(10, 25), new Point(25, 45), new Point(40, 25) };
                Point[] miniDiamondLeft1 = { new Point(10, 5), new Point(5, 10), new Point(10, 15), new Point(15, 10) };
                Point[] miniDiamondLeft2 = { new Point(10, 35), new Point(5, 40), new Point(10, 45), new Point(15, 40) };
                Point[] miniDiamondRight1 = { new Point(40, 5), new Point(35, 10), new Point(40, 15), new Point(45, 10) };
                Point[] miniDiamondRight2 = { new Point(40, 35), new Point(35, 40), new Point(40, 45), new Point(45, 40) };

                for (int k = 0; k < 4; k++)
                {
                    bigDiamond[k].X += rectangle.Location.X;
                    bigDiamond[k].Y += rectangle.Location.Y;

                    miniDiamondLeft1[k].X += rectangle.Location.X - 1;
                    miniDiamondLeft1[k].Y += rectangle.Location.Y - 1;

                    miniDiamondLeft2[k].X += rectangle.Location.X - 1;
                    miniDiamondLeft2[k].Y += rectangle.Location.Y + 1;

                    miniDiamondRight1[k].X += rectangle.Location.X + 1;
                    miniDiamondRight1[k].Y += rectangle.Location.Y - 1;

                    miniDiamondRight2[k].X += rectangle.Location.X + 1;
                    miniDiamondRight2[k].Y += rectangle.Location.Y + 1;
                }

                prevShapeColor.Width = 5f;
                graphics.DrawPolygon(prevShapeColor, miniDiamondLeft1);
                graphics.FillPolygon(prevInnerShapeBrush, miniDiamondLeft1);

                graphics.DrawPolygon(prevShapeColor, miniDiamondLeft2);
                graphics.FillPolygon(prevInnerShapeBrush, miniDiamondLeft2);

                graphics.DrawPolygon(prevShapeColor, miniDiamondRight1);
                graphics.FillPolygon(prevInnerShapeBrush, miniDiamondRight1);

                graphics.DrawPolygon(prevShapeColor, miniDiamondRight2);
                graphics.FillPolygon(prevInnerShapeBrush, miniDiamondRight2);

                graphics.DrawPolygon(prevShapeColor, bigDiamond);
                graphics.FillPolygon(prevInnerShapeBrush, bigDiamond);
            }
            else if (selectionBox.SelectedItem.Equals("Specials"))
            {
                Point[] star = { new Point(25,6),new Point(20,21), new Point(5,21),
                                         new Point(17,30),new Point(13,46),new Point(25,37),
                                         new Point(37,46),new Point(33,30),new Point(45,21), new Point(30,21)};

                for (int k = 0; k < 10; k++)
                {
                    star[k].X += rectangle.Location.X;
                    star[k].Y += rectangle.Location.Y;
                }

                prevShapeColor.Width = 4f;
                graphics.DrawPolygon(prevShapeColor, star);
                graphics.FillPolygon(prevInnerShapeBrush, star);
            }
            graphics.DrawRectangle(prevRectColor, rectangle);

            if (values[row][column] != 0){
                String s = values[row][column].ToString();
                PointF pf = new PointF(rectangle.X+27,rectangle.Y+27);
                StringFormat sf = new StringFormat();
                Font f = new Font("Calibri",11, FontStyle.Bold);
                SolidBrush sb = new SolidBrush(Color.Black);

                sf.LineAlignment = StringAlignment.Center;
                sf.Alignment = StringAlignment.Center;

                graphics.DrawString(s,f, sb, pf,sf);
            }
        }

        private void submitValueButton_Click(object sender, EventArgs e)
        {

            if (listOfSelection.Count > 1)
            {
                for (int i = 0; i < listOfSelection.Count; i++)
                {
                    int row = listOfSelection.ElementAt(i).row;
                    int column = listOfSelection.ElementAt(i).column;
                    values[row][column] = Convert.ToInt32(valueText.Text);
                    valueLabel.Text = valueText.Text;
                }
            }
            else
            {
                values[displayedRowIndex][displayedColumnIndex] = Convert.ToInt32(valueText.Text);
                valueLabel.Text = valueText.Text;
            }
        }

        private void valueText_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                submitValueButton_Click(new Object(), new EventArgs());
            }
        }

    }

    public class Location
    {
        public int column { set; get; }
        public int row { set; get; }

        public Location(int x, int y)
        {
            this.column = x;
            this.row = y;
        }
    }
}

public class CustomFlowLayoutPanel : FlowLayoutPanel
{

    public CustomFlowLayoutPanel()
    {             // Set the value of the double-buffering style bits to true. 
        this.SetStyle(ControlStyles.DoubleBuffer |
           ControlStyles.UserPaint |
           ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer,
           true);
        this.UpdateStyles();
    }
}

public static class converter
{
    public static T[,] ToMultiD<T>(this T[][] jArray)
    {
        int i = jArray.Count();
        int j = jArray.Select(x => x.Count()).Aggregate(0, (current, c) => (current > c) ? current : c);


        var mArray = new T[i, j];

        for (int ii = 0; ii < i; ii++)
        {
            for (int jj = 0; jj < j; jj++)
            {
                mArray[ii, jj] = jArray[ii][jj];
            }
        }

        return mArray;
    }

    public static T[][] ToJagged<T>(this T[,] mArray)
    {
        var cols = mArray.GetLength(0);
        var rows = mArray.GetLength(1);
        var jArray = new T[cols][];
        for (int i = 0; i < cols; i++)
        {
            jArray[i] = new T[rows];
            for (int j = 0; j < rows; j++)
            {
                jArray[i][j] = mArray[i, j];
            }
        }
        return jArray;
    }

}

