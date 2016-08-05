using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Resources;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Windows.Forms;
using System.Windows;
using System.Net;
using System.Net.Sockets;
using System.Runtime.Serialization;
using System.ServiceModel;
using CommandBoardServiceLibrary;
using Command_Board;

namespace Command_Board {

    public partial class Form1 : Form {
        ICommandBoardService proxy;
        ChannelFactory<ICommandBoardService> remoteFactory;
        Player[] players = new Player[4];
        List<int> cardsPossesed = new List<int>();
        bool win = false;
        Color[] background = { Color.Red, Color.Blue, Color.Green, Color.Black, Color.Yellow };
        Color[] propertyBackground = { Color.FromArgb(255, 106, 106), Color.FromArgb(106, 106, 255), Color.FromArgb(106, 255, 106), Color.FromArgb(97, 97, 97) };
        int totalValue;
        int playerNumber;
        int Rolled;
        public int rolled {
            set {
                Rolled = value;
                movesLeftLabel.Text = "Moves Left: " + value;
            }
            get {
                return Rolled;
            }
        }
        int isRolling = 0;
        int numberOfRolls = 1;
        int numOfPlayers;
        bool selling = false;
        bool upgradeAllowed = false;
        int mustUpgradeRow = -1;
        int mustUpgradeColumn = -1;
        Image smallBlueHome = Properties.Resources.smallBlueHome;
        Image bigBlueHome = Properties.Resources.bigBlueHome;
        Image smallBlueCloud = Properties.Resources.smallBlueCloud;
        Image bigBlueCloud = Properties.Resources.bigBlueCloud;
        Image blackGuy = Properties.Resources._char;
        Image no = Properties.Resources.char4;
        Image no1 = Properties.Resources.char2;
        Image no2 = Properties.Resources.char3;
        Image[] pics = new Image[4];
        int type;
        int turns = 1;
        int direction = (int)Direction.NONE;
        States state = new States();
        System.Drawing.Pen prevRectColor = new Pen(Color.Red, 1f);
        System.Drawing.Pen prevShapeColor = new Pen(Color.Black, 4f);
        System.Drawing.SolidBrush prevInnerShapeBrush = new SolidBrush(Color.Black);
        System.Drawing.SolidBrush prevInnerRectBrush = new SolidBrush(Color.Red);
        List<Location> listOfSelection = new List<Location>();
        int displayedRowIndex = -1;
        int displayedColumnIndex = -1;
        int visible = 0;
        CommandBoardService c = new CommandBoardService();

        public Form1() {
            pics[0] = blackGuy;
            pics[1] = no;
            pics[2] = no1;
            pics[3] = no2;
            Form4 form = new Form4();
            form.ShowDialog();
            InitializeComponent();
            if (form.valid == false)
                this.Close();
        }

        private void DrawIt() {
            int row = state.gridSize.Height;
            int column = state.gridSize.Width;
            state.gridSize = new Size(column, row);
            visible = 1;

            state.rectangleGrid = converter.ToJagged(new System.Drawing.Rectangle[row, column]);
            state.circleGrid = converter.ToJagged(new System.Drawing.Rectangle[row, column]);
            state.rectColor = converter.ToJagged(new Color[row, column]);
            state.shapeColor = converter.ToJagged(new Color[row, column]);
            state.innerRectColor = converter.ToJagged(new Color[row, column]);
            state.innerShapeColor = converter.ToJagged(new Color[row, column]);
            state.types = converter.ToJagged(new int[row, column]);
            state.rotation = converter.ToJagged(new int[row, column]);
            state.values = converter.ToJagged(new int[row, column]);

            if ((column * 50) + column > flowLayoutPanel1.Width) {
                flowLayoutPanel1.Width = (column * 50) + column;
                panel2.Location = new Point((column * 50) + column + 50, panel2.Location.Y);
            }

            if ((row * 50) + row > flowLayoutPanel1.Height) {
                flowLayoutPanel1.Height = (row * 50) + row;
                cardPanel.Location = new Point(cardPanel.Location.X, (row * 50) + row + 50);
                turnLabel.Location = new Point(turnLabel.Location.X, (row * 50) + row + 50);
                personsTurn.Location = new Point(personsTurn.Location.X, (row * 50) + row + 117);
            }

            this.Size = new Size(flowLayoutPanel1.Width + playerPanels.Size.Width + propertiesPanel.Width + selectionBox.Width + 150, flowLayoutPanel1.Height + cardPanel.Size.Height + 100);

            System.Drawing.Graphics graphics = flowLayoutPanel1.CreateGraphics();
            graphics.Clear(this.BackColor);

            for (int i = 0; i < row; i++) {
                for (int j = 0; j < column; j++) {
                    System.Drawing.Rectangle rectangle = new System.Drawing.Rectangle(50 * j + j, 50 * i + i, 50, 50);
                    System.Drawing.Rectangle shapeRect = new System.Drawing.Rectangle(50 * j + j + 5, 50 * i + i + 5, 40, 40);

                    state.rectangleGrid[i][j] = rectangle;
                    state.circleGrid[i][j] = shapeRect;

                    state.types[i][j] = 0;

                    state.rectColor[i][j] = Color.Red;
                    state.innerRectColor[i][j] = this.BackColor;
                    state.shapeColor[i][j] = Color.Black;
                    state.innerShapeColor[i][j] = this.BackColor;
                }
            }
            flowLayoutPanel1_Paint(new Object(), new PaintEventArgs(flowLayoutPanel1.CreateGraphics(), new Rectangle()));

        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e) {
            if (openFileDialog1.ShowDialog() != DialogResult.Cancel) {
                BinaryFormatter bFormatter = new BinaryFormatter();
                FileStream inPut = new FileStream(openFileDialog1.FileName, FileMode.Open, FileAccess.Read);

                state.gridSize = (Size)bFormatter.Deserialize(inPut);
                flowLayoutPanel1.Visible = true;

                DrawIt();

                Color[][] innerRectColors = converter.ToJagged(new Color[state.gridSize.Height, state.gridSize.Width]);
                Color[][] innerShapeColors = converter.ToJagged(new Color[state.gridSize.Height, state.gridSize.Width]);
                Color[][] rectColors = converter.ToJagged(new Color[state.gridSize.Height, state.gridSize.Width]);
                Color[][] shapeColors = converter.ToJagged(new Color[state.gridSize.Height, state.gridSize.Width]);

                state.innerRectColor = (Color[][])bFormatter.Deserialize(inPut);
                state.innerShapeColor = (Color[][])bFormatter.Deserialize(inPut);
                state.rectColor = (Color[][])bFormatter.Deserialize(inPut);
                state.shapeColor = (Color[][])bFormatter.Deserialize(inPut);
                state.types = (int[][])bFormatter.Deserialize(inPut);
                state.rotation = (int[][])bFormatter.Deserialize(inPut);
                state.values = (int[][])bFormatter.Deserialize(inPut);
                state.circleGrid = (Rectangle[][])bFormatter.Deserialize(inPut);
                state.rectangleGrid = (Rectangle[][])bFormatter.Deserialize(inPut);

                int row = state.gridSize.Height;
                int column = state.gridSize.Width;

                inPut.Close();
                flowLayoutPanel1_Paint(new Object(), new PaintEventArgs(flowLayoutPanel1.CreateGraphics(), new Rectangle()));

                Form2 players1 = new Form2();
                players1.ShowDialog();
                state.numOfPlayers = players1.numberOfPlayer;
                state.totalValue = players1.totalValue;

                Panel[] playerPanel = new Panel[state.numOfPlayers];
                playerPanels.Height = state.numOfPlayers * 97 + (state.numOfPlayers + 2) * 2 + 2 - (4 - state.numOfPlayers);

                for (int i = 0; i < state.numOfPlayers; i++) {
                    playerPanel[i] = setUpPanels(i + 1);
                    playerPanel[i].Paint += new System.Windows.Forms.PaintEventHandler(playerPanel_paint);
                    playerPanels.Controls.Add(playerPanel[i]);
                }

                turnLabel.Visible = true;
                personsTurn.Visible = true;

                turnLabel.Text = "Turn " + turns;
                personsTurn.Text = "Player 1's turn";

                /*
                using (ServiceHost host = new ServiceHost(typeof(CommandBoardServiceLibrary.CommandBoardService)))
                {
                    host.AddServiceEndpoint(typeof(
                        CommandBoardServiceLibrary.ICommandBoardService),
                        new NetTcpBinding(),
                        "net.tcp://localHost:9000/CommandBoardEndPoint");
                    host.Open();
                }

                proxy = ChannelFactory<ICommandBoardService>.CreateChannel(
                             new NetTcpBinding(),
                            new EndpointAddress(
                            "net.tcp://localhost:9000/CommandBoardEndPoint"));

                proxy.setConnected(proxy.getConnected()+1);
                MessageBox.Show(proxy.getConnected().ToString());

                */
            }
        }

        private void flowLayoutPanel1_Paint(object sender, PaintEventArgs e) {
            System.Drawing.Graphics graphics = e.Graphics;

            int row = state.gridSize.Height;
            int column = state.gridSize.Width;

            for (int i = 0; i < row; i++) {
                for (int j = 0; j < column; j++) {
                    System.Drawing.Rectangle rectangle = state.rectangleGrid[i][j];
                    System.Drawing.Rectangle shapeRect = state.circleGrid[i][j];

                    prevRectColor = new Pen(state.rectColor[i][j], 1f); ;
                    prevShapeColor = new Pen(state.shapeColor[i][j], 9f);
                    prevInnerRectBrush = new SolidBrush(state.innerRectColor[i][j]);
                    prevInnerShapeBrush = new SolidBrush(state.innerShapeColor[i][j]);

                    drawSquare(i, j, shapeRect, rectangle, graphics);
                }
            }

            DrawChar(e.Graphics);
        }

        public void drawSquare(int row, int column, Rectangle shapeRect, Rectangle rectangle, Graphics graphics) {
            graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            graphics.FillRectangle(prevInnerRectBrush, rectangle);

            if (state.types[row][column] == 0) {
                graphics.DrawEllipse(prevShapeColor, shapeRect);
                graphics.FillEllipse(prevInnerShapeBrush, shapeRect);
            } else if (state.types[row][column] == 4) {
                graphics.DrawImage(smallBlueHome, rectangle);
            } else if (state.types[row][column] == 6) {
                graphics.DrawImage(smallBlueCloud, rectangle);
            } else if (state.types[row][column] == 3) {
                Point start;
                Point finish;

                if (state.rotation[row][column] == 1) {
                    start = new Point(0, 25);
                    finish = new Point(50, 25);
                } else {
                    start = new Point(25, 1);
                    finish = new Point(25, 50);
                }

                start.X += rectangle.Location.X;
                start.Y += rectangle.Location.Y;

                finish.X += rectangle.Location.X;
                finish.Y += rectangle.Location.Y;

                graphics.DrawLine(prevShapeColor, start, finish);

            } else if (state.types[row][column] == 5) {
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

            } else if (state.types[row][column] == 2) {
                Point[] bigDiamond = { new Point(25, 5), new Point(10, 25), new Point(25, 45), new Point(40, 25) };
                Point[] miniDiamondLeft1 = { new Point(10, 5), new Point(5, 10), new Point(10, 15), new Point(15, 10) };
                Point[] miniDiamondLeft2 = { new Point(10, 35), new Point(5, 40), new Point(10, 45), new Point(15, 40) };
                Point[] miniDiamondRight1 = { new Point(40, 5), new Point(35, 10), new Point(40, 15), new Point(45, 10) };
                Point[] miniDiamondRight2 = { new Point(40, 35), new Point(35, 40), new Point(40, 45), new Point(45, 40) };

                for (int k = 0; k < 4; k++) {
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
            } else if (state.types[row][column] == 1) {
                Point[] star = { new Point(25,6),new Point(20,21), new Point(5,21),
                                         new Point(17,30),new Point(13,46),new Point(25,37),
                                         new Point(37,46),new Point(33,30),new Point(45,21), new Point(30,21)};

                for (int k = 0; k < 10; k++) {
                    star[k].X += rectangle.Location.X;
                    star[k].Y += rectangle.Location.Y;
                }

                prevShapeColor.Width = 4f;
                graphics.DrawPolygon(prevShapeColor, star);
                graphics.FillPolygon(prevInnerShapeBrush, star);
            }
            graphics.DrawRectangle(prevRectColor, rectangle);

            if (state.values[row][column] != 0) {
                String s = (state.values[row][column] + UpgradeCost(state.values[row][column], state.lvls[row][column])).ToString();
                PointF pf = new PointF(rectangle.X + 27, rectangle.Y + 27);
                StringFormat sf = new StringFormat();
                Font f = new Font("Calibri", 11, FontStyle.Bold);
                SolidBrush sb = new SolidBrush(Color.Black);

                sf.LineAlignment = StringAlignment.Center;
                sf.Alignment = StringAlignment.Center;

                graphics.DrawString(s, f, sb, pf, sf);
            }
        }

        private void playerPanel_paint(object sender, PaintEventArgs e) {
            Rectangle rect = playerPanels.Controls[0].ClientRectangle;
            rect.Width--;
            rect.Height--;
            e.Graphics.DrawRectangle(Pens.Black, rect);
            //playerPanels.Controls.Find(
        }

        private void playerPanels_Paint(object sender, PaintEventArgs e) {
            Rectangle rect = playerPanels.ClientRectangle;
            rect.Width--;
            rect.Height--;
            e.Graphics.DrawRectangle(Pens.Black, rect);
        }

        public Panel setUpPanels(int i) {
            Panel p;
            Label l1, l2, l3, l4, l5;
            Panel redPanel, yellowPanel, greenPanel, bluePanel;
            redPanel = new System.Windows.Forms.Panel();
            yellowPanel = new System.Windows.Forms.Panel();
            greenPanel = new System.Windows.Forms.Panel();
            bluePanel = new System.Windows.Forms.Panel();
            p = new System.Windows.Forms.Panel();
            l1 = new System.Windows.Forms.Label();
            l2 = new System.Windows.Forms.Label();
            l3 = new System.Windows.Forms.Label();
            l4 = new System.Windows.Forms.Label();
            l5 = new System.Windows.Forms.Label();

            // 
            // p
            // 
            p.BackColor = background[i - 1];
            p.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            p.Controls.Add(l5);
            p.Controls.Add(l4);
            p.Controls.Add(l3);
            p.Controls.Add(l2);
            p.Controls.Add(l1);
            p.Controls.Add(greenPanel);
            p.Controls.Add(bluePanel);
            p.Controls.Add(yellowPanel);
            p.Controls.Add(redPanel);
            p.Location = new System.Drawing.Point(0, 0 + 100 * (i - 1));
            p.MaximumSize = new System.Drawing.Size(200, 100);
            p.AutoSize = true;
            p.Name = "panel";
            p.Size = new System.Drawing.Size(200, 100);
            p.TabIndex = 0;
            // 
            // l1
            // 
            l1.AutoSize = true;
            l1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            l1.ForeColor = System.Drawing.Color.Yellow;
            l1.Location = new System.Drawing.Point(14, 10);
            l1.Name = "playerLabel";
            l1.Size = new System.Drawing.Size(67, 24);
            l1.TabIndex = 0;
            l1.Text = "Player " + i;
            // 
            // l2
            // 
            l2.AutoSize = true;
            l2.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            l2.ForeColor = System.Drawing.Color.Yellow;
            l2.Location = new System.Drawing.Point(13, 38);
            l2.Name = "l2";
            l2.Size = new System.Drawing.Size(62, 25);
            l2.TabIndex = 1;
            l2.Text = "Cash";
            // 
            // l3
            // 
            l3.AutoSize = true;
            l3.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            l3.ForeColor = System.Drawing.Color.Yellow;
            l3.Location = new System.Drawing.Point(128, 37);
            l3.Name = "cashLabel";
            l3.Size = new System.Drawing.Size(72, 25);
            l3.TabIndex = 2;
            l3.DataBindings.Add(new Binding("Text", players[i - 1], "cashS"));
            //l3.Text = "1000";
            // 
            // l4
            // 
            l4.AutoSize = true;
            l4.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            l4.ForeColor = System.Drawing.Color.Yellow;
            l4.Location = new System.Drawing.Point(13, 62);
            l4.Name = "l4";
            l4.Size = new System.Drawing.Size(121, 25);
            l4.TabIndex = 3;
            l4.Text = "Total Value";
            // 
            // l5
            // 
            l5.AutoSize = true;
            l5.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            l5.ForeColor = System.Drawing.Color.Yellow;
            l5.Location = new System.Drawing.Point(128, 62);
            l5.Name = "totalValueLabel";
            l5.Size = new System.Drawing.Size(72, 25);
            l5.TabIndex = 4;
            l5.DataBindings.Add(new Binding("Text", players[i - 1], "totalValueS"));
            //l5.Text = "1000";

            //Green Panels
            greenPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            greenPanel.Location = new System.Drawing.Point(142, 19);
            greenPanel.Name = "greenPanel";
            greenPanel.Size = new System.Drawing.Size(15, 15);
            greenPanel.TabIndex = 0;
            greenPanel.Visible = true;
            greenPanel.BackColor = SystemColors.Control;
            // 
            // bluePanel
            // 
            bluePanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            bluePanel.Location = new System.Drawing.Point(142, 3);
            bluePanel.Name = "bluePanel";
            bluePanel.Size = new System.Drawing.Size(15, 15);
            bluePanel.TabIndex = 0;
            bluePanel.BackColor = SystemColors.Control;
            // 
            // redPanel
            // 
            redPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            redPanel.Location = new System.Drawing.Point(158, 3);
            redPanel.Name = "redPanel";
            redPanel.Size = new System.Drawing.Size(15, 15);
            redPanel.TabIndex = 5;
            redPanel.BackColor = SystemColors.Control;
            // 
            // yellowPanel
            // 
            yellowPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            yellowPanel.Location = new System.Drawing.Point(158, 19);
            yellowPanel.Name = "yellowPanel";
            yellowPanel.Size = new System.Drawing.Size(15, 15);
            yellowPanel.TabIndex = 5;
            yellowPanel.BackColor = SystemColors.Control;


            return p;
        }

        private void connectToolStripMenuItem_Click(object sender, EventArgs e) {
            remoteFactory = new ChannelFactory<ICommandBoardService>("CommandBoard");
            proxy = remoteFactory.CreateChannel();

            state = proxy.getState();
            int row = state.gridSize.Height;
            int col = state.gridSize.Width;
            visible = 1;

            SuspendLayout();

            if ((col * 50) + col > flowLayoutPanel1.Width) {
                flowLayoutPanel1.Width = (col * 50) + col;
                panel2.Location = new Point((col * 50) + col + 50, panel2.Location.Y);
                columnPanel.Width = (col * 50) + col;

                //Just to fill the columnPanel
                for (int i = 0; i < col; i++) {
                    Label l = new Label();
                    l.AutoSize = true;
                    l.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                    l.Location = new System.Drawing.Point(18 + 51 * i, 1);
                    l.Name = "label" + i;
                    l.Size = new System.Drawing.Size(16, 16);
                    l.TabIndex = 17;
                    l.Text = i.ToString();

                    columnPanel.Controls.Add(l);
                }
            }

            if ((row * 50) + row > flowLayoutPanel1.Height) {
                flowLayoutPanel1.Height = (row * 50) + row;
                rowPanel.Height = (row * 50) + row;
                cardPanel.Location = new Point(cardPanel.Location.X, (row * 50) + row + 50);
                turnLabel.Location = new Point(turnLabel.Location.X, (row * 50) + row + 50);
                personsTurn.Location = new Point(personsTurn.Location.X, (row * 50) + row + 117);

                for (int i = 0; i < row; i++) {
                    Label l = new Label();
                    l.AutoSize = true;
                    l.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                    l.Location = new System.Drawing.Point(4, 18 + 51 * i);
                    l.Name = "label" + i;
                    l.Size = new System.Drawing.Size(18, 16);
                    l.TabIndex = 18;
                    l.Text = ((char)((int)'A' + i)).ToString();

                    rowPanel.Controls.Add(l);
                }
            }

            this.Size = new Size(flowLayoutPanel1.Width + playerPanels.Size.Width + propertiesPanel.Width + selectionBox.Width + 150, flowLayoutPanel1.Height + cardPanel.Size.Height + 100);

            CreatePlayers();

            Panel[] playerPanel = new Panel[state.numOfPlayers];
            playerPanels.Height = state.numOfPlayers * 97 + (state.numOfPlayers + 2) * 2 + 2 - (4 - state.numOfPlayers);

            for (int i = 0; i < state.numOfPlayers; i++) {
                playerPanel[i] = setUpPanels(i + 1);
                playerPanel[i].Paint += new System.Windows.Forms.PaintEventHandler(playerPanel_paint);
                playerPanels.Controls.Add(playerPanel[i]);
            }

            turnLabel.Visible = true;
            personsTurn.Visible = true;

            turnLabel.Text = "Turn " + turns;
            personsTurn.Text = "Player 1's turn";

            flowLayoutPanel1_Paint(new Object(), new PaintEventArgs(flowLayoutPanel1.CreateGraphics(), new Rectangle()));

            for (int i = 0; i < 5; i++) {
                cardPanel.Controls.Add(CreateCard(i + 1));
            }
            ResumeLayout();
            MessageBox.Show(proxy.setConnected(1));
            playerNumber = proxy.getConnected();
        }

        private void CreatePlayers() {
            int row = 0;
            int col = 0;
            for (int i = 0; i < state.gridSize.Width; i++) {
                for (int j = 0; j < state.gridSize.Height; j++) {
                    if (state.types[i][j] == 4) {
                        row = i;
                        col = j;
                    }
                }
            }

            for (int i = 0; i < state.numOfPlayers; i++) {
                players[i] = new Player();
                players[i].cash = 1000;
                players[i].totalValue = 1000;
                players[i].row = row;
                players[i].column = col;
                players[i].checkPoints = new List<Location>();
            }
        }

        private void DrawChar(Graphics g) {
            for (int i = 0; i < state.numOfPlayers; i++) {
                g.DrawImage(pics[i], state.rectangleGrid[players[i].row][players[i].column]);
            }
        }

        Random randomRoll = new Random();
        private void rollButton_Click(object sender, EventArgs e) {
            if (everyoneConnected() && ((proxy.getTurns() % state.numOfPlayers) == (playerNumber - 1)) && rolled == 0) {

                String s = "You rolled a: ";
                int k;
                for (int i = 0; i < numberOfRolls; i++) {
                    k = randomRoll.Next(6) + 1;
                    s += k + " ";
                    rolled += k;
                }

                MessageBox.Show(s + ". A total of " + rolled);
                isRolling = 1;
            }
            submitButton.Select();
        }

        private bool everyoneConnected() {
            if (proxy.getConnected() == state.numOfPlayers) {
                return true;
            } else {
                MessageBox.Show("Not everyone is connected");
                return false;
            }
        }

        private void flowLayoutPanel1_MouseClick(object sender, MouseEventArgs e) {
            if (visible == 1) {
                displayedRowIndex = e.Y / 51;
                displayedColumnIndex = e.X / 51;

                if (displayedRowIndex == state.gridSize.Height)
                    displayedRowIndex--;

                if (displayedColumnIndex == state.gridSize.Width)
                    displayedColumnIndex--;

                selectionBox1.SelectedIndex = state.types[displayedRowIndex][displayedColumnIndex];

                if (Control.ModifierKeys == Keys.Control && selectionBox1.SelectedIndex != 3) {
                    listOfSelection.Add(new Location(displayedColumnIndex, displayedRowIndex));
                } else if (selectionBox1.SelectedIndex != 3) {
                    listOfSelection.Clear();
                }

                prevRectColor = new Pen(state.rectColor[displayedRowIndex][displayedColumnIndex], 1f);
                prevInnerRectBrush = new SolidBrush(state.innerRectColor[displayedRowIndex][displayedColumnIndex]);

                prevShapeColor = new Pen(state.shapeColor[displayedRowIndex][displayedColumnIndex], 9f);
                prevInnerShapeBrush = new SolidBrush(state.innerShapeColor[displayedRowIndex][displayedColumnIndex]);

                previewIt(prevShapeColor, prevRectColor, prevInnerShapeBrush, prevInnerRectBrush);


                if (state.values[displayedRowIndex][displayedColumnIndex] > 0) {
                    tollLabel.Text = "Toll : " + TollAmount(state.values[displayedRowIndex][displayedColumnIndex], state.lvls[displayedRowIndex][displayedColumnIndex]);
                    levelLabel.Text = "Current Level: " + state.lvls[displayedRowIndex][displayedColumnIndex];
                    int totalValueAlready = UpgradeCost(state.values[displayedRowIndex][displayedColumnIndex], state.lvls[displayedRowIndex][displayedColumnIndex]);
                    for (int i = 0; i < 5; i++) {
                        ((RadioButton)radioPanel.Controls[i]).Checked = false;
                        if (state.lvls[displayedRowIndex][displayedColumnIndex] >= i)
                            radioPanel.Controls[i].Text = String.Format("Level {0}. {1,5} | {2,5}", i, 0, TollAmount(state.values[displayedRowIndex][displayedColumnIndex], i));
                        else
                            radioPanel.Controls[i].Text = String.Format("Level {0}. {1,5} | {2,5}", i, UpgradeCost(state.values[displayedRowIndex][displayedColumnIndex], i) - totalValueAlready, TollAmount(state.values[displayedRowIndex][displayedColumnIndex], i));

                        if (players[playerNumber - 1].cash < UpgradeCost(state.values[displayedRowIndex][displayedColumnIndex], i) - totalValueAlready)
                            radioPanel.Controls[i].Enabled = false;
                        else
                            radioPanel.Controls[i].Enabled = true;
                    }

                    levelPanel.Visible = true;
                    upgradeButton.Visible = true;
                } else {
                    levelPanel.Visible = false;
                    upgradeButton.Visible = false;
                }

                bool con = true;

                //Checks to see if user selected square they are standing on, or the square they
                //just moved from
                if (selectionBox.Items.Count > 0) {
                    Location l = (Location)selectionBox.Items[selectionBox.Items.Count - 1];

                    if ((l.column == displayedColumnIndex && l.row == displayedRowIndex))
                        con = false;
                }

                if (players[playerNumber - 1].column == displayedColumnIndex && players[playerNumber - 1].row == displayedRowIndex && selectionBox.Items.Count == 0) {
                    con = false;
                }

                if (isRolling == 1 && Control.ModifierKeys == Keys.Control && con && state.types[displayedRowIndex][displayedColumnIndex] != 3) {
                    Location l = new Location(displayedColumnIndex, displayedRowIndex);
                    selectionBox.Items.Add(l);
                    movesLeftLabel.Text = "Moves Left: " + (rolled - selectionBox.Items.Count);
                }

                //Sell the property since you are low on money
                if (selling && state.owned[playerNumber - 1].Contains(new Location(displayedColumnIndex, displayedRowIndex))) {
                    DialogResult = MessageBox.Show("Would you like to sell this property for " + TollAmount(state.values[displayedRowIndex][displayedColumnIndex], state.lvls[displayedRowIndex][displayedColumnIndex]) + "?", "Sell?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (DialogResult == DialogResult.Yes) {
                        state.owned[playerNumber - 1].Remove(new Location(displayedColumnIndex, displayedRowIndex));
                        players[playerNumber - 1].cash += TollAmount(state.values[displayedRowIndex][displayedColumnIndex], state.lvls[displayedRowIndex][displayedColumnIndex]);
                        state.innerShapeColor[displayedRowIndex][displayedColumnIndex] = SystemColors.Control;
                        prevInnerShapeBrush = new SolidBrush(SystemColors.Control);
                        drawSquare(displayedRowIndex, displayedColumnIndex, state.circleGrid[displayedRowIndex][displayedColumnIndex], state.rectangleGrid[displayedRowIndex][displayedColumnIndex], flowLayoutPanel1.CreateGraphics());

                        //update Total Value

                        if (players[playerNumber - 1].cash < 0 && state.owned[playerNumber - 1].Count > 0) {
                            selling = true;
                        } else {
                            selling = false;
                            finishButton_Click(new Object(), new EventArgs());
                        }
                    }
                } else if (upgradeAllowed) {

                }
            }
        }

        private int UpgradeCost(int value, int lvl) {

            if (lvl == 0) {
                return 0;
            } else if (lvl == 1) {
                return value / 2;
            } else if (lvl == 2) {
                return value * 2;
            } else if (lvl == 3) {
                return value * 4;
            } else if (lvl == 4) {
                return value * 7;
            }
            return -1;
        }

        private int TollAmount(int value, int lvl) {

            if (lvl == 0) {
                return (int)(value * .4);
            } else if (lvl == 1) {
                return (int)(value * .6);
            } else if (lvl == 2) {
                return (int)(value * 1.5);
            } else if (lvl == 3) {
                return (int)(value * 2.5);
            } else if (lvl == 4) {
                return (int)(value * 4.8);
            }

            return -1;
        }

        private void previewIt(Pen shapePen, Pen rectPen, SolidBrush shapeBrush, SolidBrush rectBrush) {
            System.Drawing.Graphics graphics = propertyLayoutPanel.CreateGraphics();
            graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;

            graphics.Clear(Form1.DefaultBackColor);
            System.Drawing.Rectangle rectangle = new System.Drawing.Rectangle(1, 1, 200, 200);
            System.Drawing.Rectangle shapeRect = new System.Drawing.Rectangle(21, 21, 160, 160);


            graphics.FillRectangle(rectBrush, rectangle);

            if (selectionBox1.SelectedItem.Equals("Properties")) {
                shapePen.Width = 40f;
                graphics.DrawEllipse(shapePen, shapeRect);
                graphics.FillEllipse(shapeBrush, shapeRect);
                shapePen.Width = 9f;

                if (state.values[displayedRowIndex][displayedColumnIndex] != 0) {
                    String s = (state.values[displayedRowIndex][displayedColumnIndex] + UpgradeCost(state.values[displayedRowIndex][displayedColumnIndex], state.lvls[displayedRowIndex][displayedColumnIndex])).ToString();
                    PointF pf = new PointF(rectangle.X + 100, rectangle.Y + 100);
                    StringFormat sf = new StringFormat();
                    Font f = new Font("Calibri", 40, FontStyle.Bold);
                    SolidBrush sb = new SolidBrush(Color.Black);

                    sf.LineAlignment = StringAlignment.Center;
                    sf.Alignment = StringAlignment.Center;

                    graphics.DrawString(s, f, sb, pf, sf);
                }
            } else if (selectionBox1.SelectedItem.Equals("Home")) {
                graphics.DrawImage(bigBlueHome, rectangle);
            } else if (selectionBox1.SelectedItem.Equals("MovingCloud")) {
                graphics.DrawImage(bigBlueCloud, rectangle);
            } else if (selectionBox1.SelectedItem.Equals("Lines")) {
                Point start;
                Point finish;

                if (state.rotation[displayedRowIndex][displayedColumnIndex] == 1) {
                    start = new Point(0, 101);
                    finish = new Point(200, 101);
                } else {
                    start = new Point(100, 1);
                    finish = new Point(100, 201);
                }

                graphics.DrawLine(shapePen, start, finish);

            } else if (selectionBox1.SelectedItem.Equals("Deadzones")) {
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

            } else if (selectionBox1.SelectedItem.Equals("Checkpoints")) {
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
            } else if (selectionBox1.SelectedItem.Equals("Specials")) {
                Point[] star = { new Point(25,5),new Point(20,21), new Point(5,21),
                                         new Point(17,30),new Point(13,46),new Point(25,37),
                                         new Point(37,46),new Point(33,30),new Point(45,21), new Point(30,21)};

                for (int i = 0; i < 10; i++) {
                    star[i].X *= 4;
                    star[i].Y *= 4;
                }

                graphics.DrawPolygon(prevShapeColor, star);
                graphics.FillPolygon(prevInnerShapeBrush, star);

            }
            graphics.DrawRectangle(rectPen, rectangle);
        }

        private void deleteButton_Click(object sender, EventArgs e) {
            while (selectionBox.SelectedItems.Count != 0) {
                selectionBox.Items.Remove(selectionBox.SelectedItems[0]);
            }
            movesLeftLabel.Text = "Moves Left: " + (rolled - selectionBox.Items.Count);
        }

        private void submitButton_Click(object sender, EventArgs e) {
            bool valid = true;
            int originalDirection = direction;
            Location cloudStart = null;
            Location cloudEnd = null;
            Location currentlyOn = new Location(players[playerNumber - 1].column, players[playerNumber - 1].row);

            if (rolled == 0) {
                MessageBox.Show("You haven't even rolled -.-");
                return;
            } else if (selectionBox.Items.Count == rolled) {
                for (int i = 0; i < selectionBox.Items.Count; i++) {
                    Location l = (Location)selectionBox.Items[i];
                    if (state.values[l.row][l.column] == 0 && state.types[l.row][l.column] == 0) {
                        MessageBox.Show("You selected to move to a invalid square");
                        return;
                    }

                    if (state.types[currentlyOn.row][currentlyOn.column] == (int)SquareType.MovingCloud)
                        cloudStart = currentlyOn;

                    if (cloudStart != null && state.types[l.row][l.column] == (int)SquareType.Deadzones)
                        cloudEnd = l;

                    valid = checkDirections(currentlyOn, l);

                    if (valid == false) {
                        direction = originalDirection;
                        break;
                    }

                    currentlyOn = l;
                }
            } else if (selectionBox.Items.Count > rolled) {
                MessageBox.Show("You moved too much!");
                return;
            } else if (selectionBox.Items.Count < rolled && selectionBox.Items.Count != 1) {
                MessageBox.Show("You didn't move enough!");
                return;
            } else if (selectionBox.Items.Count == 1) {
                valid = false;
            }

            //This takes care of single click 
            if (valid == false && selectionBox.Items.Count == 1) {
                Location point = (Location)selectionBox.Items[0];
                endPositions.Clear();
                checkPossibleWays(rolled, new Location(players[playerNumber - 1].column, players[playerNumber - 1].row), new Location(players[playerNumber - 1].column, players[playerNumber - 1].row), new List<Location>());

                //Go through each path
                for (int i = 0; i < endPositions.Count; i++) {
                    List<Location> path = endPositions.ElementAt<List<Location>>(i);
                    Location endP = path.Last<Location>();
                    cloudStart = null;
                    cloudEnd = null;
                    bool bad = true;
                    direction = originalDirection;

                    //Make sure the paths are going the right direction and move the cloud
                    for (int j = 0; j < path.Count - 1; j++) {
                        currentlyOn = path.ElementAt<Location>(j);
                        Location l = path.ElementAt<Location>(j + 1);
                        if (checkDirections(currentlyOn, l) == false)
                            bad = false;

                        if (state.types[currentlyOn.row][currentlyOn.column] == (int)SquareType.MovingCloud)
                            cloudStart = currentlyOn;

                        if (cloudStart != null && state.types[l.row][l.column] == (int)SquareType.Deadzones)
                            cloudEnd = l;
                    }
                    if (endP.Equals(point) && bad) {
                        selectionBox.Items.Clear();
                        path.RemoveAt(0);
                        selectionBox.Items.AddRange(path.ToArray());
                        valid = true;
                        currentlyOn = endP;
                        break;
                    }
                }
            }

            if (valid == true) {
                int row = players[playerNumber - 1].row;
                int col = players[playerNumber - 1].column;

                //Draw a deadzone and cloud if player passed over it
                if (cloudStart != null && cloudEnd != null) {
                    prevInnerRectBrush = new SolidBrush(state.innerRectColor[cloudStart.row][cloudStart.column]);
                    prevInnerShapeBrush = new SolidBrush(state.innerShapeColor[cloudStart.row][cloudStart.column]);
                    prevRectColor = new Pen(state.rectColor[cloudStart.row][cloudStart.column], 1f);
                    prevShapeColor = new Pen(state.shapeColor[cloudStart.row][cloudStart.column], 9f);

                    state.types[cloudStart.row][cloudStart.column] = (int)SquareType.Deadzones;
                    state.types[cloudEnd.row][cloudEnd.column] = (int)SquareType.MovingCloud;

                    drawSquare(cloudStart.row, cloudStart.column, state.circleGrid[cloudStart.row][cloudStart.column], state.rectangleGrid[cloudStart.row][cloudStart.column], flowLayoutPanel1.CreateGraphics());
                    drawSquare(cloudEnd.row, cloudEnd.column, state.circleGrid[cloudEnd.row][cloudEnd.column], state.rectangleGrid[cloudEnd.row][cloudEnd.column], flowLayoutPanel1.CreateGraphics());
                }

                prevInnerRectBrush = new SolidBrush(state.innerRectColor[row][col]);
                prevInnerShapeBrush = new SolidBrush(state.innerShapeColor[row][col]);
                prevRectColor = new Pen(state.rectColor[row][col], 1f);
                prevShapeColor = new Pen(state.shapeColor[row][col], 9f);

                //Draw the Original square where the player started
                drawSquare(row, col, state.circleGrid[row][col], state.rectangleGrid[row][col], flowLayoutPanel1.CreateGraphics());

                players[playerNumber - 1].column = currentlyOn.column;
                players[playerNumber - 1].row = currentlyOn.row;
                proxy.setPlayer(playerNumber - 1, players[playerNumber - 1]);
                DrawChar(flowLayoutPanel1.CreateGraphics());

                rolled = 0;

                //Check if any of them are checkpoints or Home
                for (int i = 0; i < selectionBox.Items.Count; i++) {
                    Location l = (Location)selectionBox.Items[i];

                    //Highlight them in square
                    if (state.types[l.row][l.column] == (int)SquareType.Checkpoints) {
                        if (state.innerShapeColor[l.row][l.column] == Color.Blue && playerPanels.Controls[playerNumber - 1].Controls.Find("bluePanel", false)[0].BackColor != Color.Blue) {
                            playerPanels.Controls[playerNumber - 1].Controls.Find("bluePanel", false)[0].BackColor = Color.Blue;
                            MessageBox.Show("Checkpoint passed!  You get a card and $700!");
                            players[playerNumber - 1].cash += 700;
                        } else if (state.innerShapeColor[l.row][l.column] == Color.Green && playerPanels.Controls[playerNumber - 1].Controls.Find("greenPanel", false)[0].BackColor != Color.Green) {
                            playerPanels.Controls[playerNumber - 1].Controls.Find("greenPanel", false)[0].BackColor = Color.Green;
                            MessageBox.Show("Checkpoint passed!  You get a card and $1500!");
                            players[playerNumber - 1].cash += 1500;
                        } else if (state.innerShapeColor[l.row][l.column] == Color.Yellow && playerPanels.Controls[playerNumber - 1].Controls.Find("yellowPanel", false)[0].BackColor != Color.Yellow) {
                            playerPanels.Controls[playerNumber - 1].Controls.Find("yellowPanel", false)[0].BackColor = Color.Yellow;
                            MessageBox.Show("Checkpoint passed!  You get a card and $700!");
                            players[playerNumber - 1].cash += 700;
                        } else if (state.innerShapeColor[l.row][l.column] == Color.Red && playerPanels.Controls[playerNumber - 1].Controls.Find("redPanel", false)[0].BackColor != Color.Red) {
                            playerPanels.Controls[playerNumber - 1].Controls.Find("redPanel", false)[0].BackColor = Color.Red;
                            MessageBox.Show("Checkpoint passed!  You get a card and $700!");
                            players[playerNumber - 1].cash += 700;
                        }

                        if (cardPanel.Controls.Count < 5)
                            cardPanel.Controls.Add(CreateCard(0));

                        if (players[playerNumber - 1].checkPoints.Count < 5)
                            players[playerNumber - 1].checkPoints.Add(l);
                    } else if (state.types[l.row][l.column] == (int)SquareType.Home) {
                        //Checks if player has gone through each checkpoint
                        if (playerPanels.Controls[playerNumber - 1].Controls.Find("redPanel", false)[0].BackColor == Color.Red &&
                                                        playerPanels.Controls[playerNumber - 1].Controls.Find("greenPanel", false)[0].BackColor == Color.Green &&
                                                        playerPanels.Controls[playerNumber - 1].Controls.Find("yellowPanel", false)[0].BackColor == Color.Yellow &&
                                                        playerPanels.Controls[playerNumber - 1].Controls.Find("bluePanel", false)[0].BackColor == Color.Blue) {
                            //Property Bonus
                            int pBonus=0;
                            for (int j = 0; j < state.owned[playerNumber - 1].Count; j++) {
                                Location owned = state.owned[playerNumber - 1].ElementAt<Location>(j);
                                pBonus += state.values[owned.row][owned.column] + UpgradeCost(state.values[owned.row][owned.column], state.lvls[owned.row][owned.column]);
                            }
                            pBonus /= 2;

                            //Card Bonus
                            int cBonus = cardPanel.Controls.Count * 100;

                            //Lap Bonus
                            int lBonus = 200;
                            MessageBox.Show("Lap completed!\nProperty Bonus: " + pBonus + "\nCard Bonus: " + cBonus + "\nLap Bonus: " + lBonus);
                            players[playerNumber - 1].cash += lBonus + cBonus + pBonus;

                            playerPanels.Controls[playerNumber - 1].Controls.Find("redPanel", false)[0].BackColor = SystemColors.Control;
                            playerPanels.Controls[playerNumber - 1].Controls.Find("greenPanel", false)[0].BackColor = SystemColors.Control;
                            playerPanels.Controls[playerNumber - 1].Controls.Find("yellowPanel", false)[0].BackColor = SystemColors.Control;
                            playerPanels.Controls[playerNumber - 1].Controls.Find("bluePanel", false)[0].BackColor = SystemColors.Control;
                        }
                    }
                }

                BuyPayPass(players[playerNumber - 1].row, players[playerNumber - 1].column);
                DrawChar(flowLayoutPanel1.CreateGraphics());

                selectionBox.Items.Clear();
                //finishButton_Click(new Object(), new EventArgs());
            } else {
                direction = originalDirection;
                MessageBox.Show("Invalid Movements");
            }
        }

        public bool checkDirections(Location currentlyOn, Location l) {
            bool valid = true;
            int lineDirection = CheckForLines(currentlyOn, l);
            if (direction == (int)Direction.NONE) {
                if ((currentlyOn.row == l.row && l.column == currentlyOn.column + 1) || lineDirection == 1) {
                    direction = (int)Direction.RIGHT;
                } else if ((currentlyOn.row == l.row && l.column == currentlyOn.column - 1) || lineDirection == 2) {
                    direction = (int)Direction.LEFT;
                } else if ((currentlyOn.column == l.column && l.row == currentlyOn.row - 1) || lineDirection == 3) {
                    direction = (int)Direction.UP;
                } else if ((currentlyOn.column == l.column && l.row == currentlyOn.row + 1) || lineDirection == 4) {
                    direction = (int)Direction.DOWN;
                } else {
                    valid = false;
                    return valid;
                }
            } else if (direction == (int)Direction.LEFT) {
                if ((currentlyOn.row == l.row && l.column == currentlyOn.column - 1) || lineDirection == 2) {
                    direction = (int)Direction.LEFT;
                } else if ((currentlyOn.column == l.column && l.row == currentlyOn.row - 1) || lineDirection == 3) {
                    direction = (int)Direction.UP;
                } else if ((currentlyOn.column == l.column && l.row == currentlyOn.row + 1) || lineDirection == 4) {
                    direction = (int)Direction.DOWN;
                } else {
                    valid = false;
                    return valid;
                }
            } else if (direction == (int)Direction.RIGHT) {
                if ((currentlyOn.row == l.row && l.column == currentlyOn.column + 1) || lineDirection == 1) {
                    direction = (int)Direction.RIGHT;
                } else if ((currentlyOn.column == l.column && l.row == currentlyOn.row - 1) || lineDirection == 3) {
                    direction = (int)Direction.UP;
                } else if ((currentlyOn.column == l.column && l.row == currentlyOn.row + 1) || lineDirection == 4) {
                    direction = (int)Direction.DOWN;
                } else {
                    valid = false;
                    return valid;
                }
            } else if (direction == (int)Direction.UP) {
                if ((currentlyOn.row == l.row && l.column == currentlyOn.column + 1) || lineDirection == 1) {
                    direction = (int)Direction.RIGHT;
                } else if ((currentlyOn.row == l.row && l.column == currentlyOn.column - 1) || lineDirection == 2) {
                    direction = (int)Direction.LEFT;
                } else if ((currentlyOn.column == l.column && l.row == currentlyOn.row - 1) || lineDirection == 3) {
                    direction = (int)Direction.UP;
                } else {
                    valid = false;
                    return valid;
                }
            } else if (direction == (int)Direction.DOWN) {
                if ((currentlyOn.row == l.row && l.column == currentlyOn.column + 1) || lineDirection == 1) {
                    direction = (int)Direction.RIGHT;
                } else if ((currentlyOn.row == l.row && l.column == currentlyOn.column - 1) || lineDirection == 2) {
                    direction = (int)Direction.LEFT;
                } else if ((currentlyOn.column == l.column && l.row == currentlyOn.row + 1) || lineDirection == 4) {
                    direction = (int)Direction.DOWN;
                } else {
                    valid = false;
                    return valid;
                }
            }

            return valid;
        }

        List<List<Location>> endPositions = new List<List<Location>>();

        private void checkPossibleWays(int movesLeft, Location position, Location previousPosition, List<Location> path) {
            path.Add(position);
            if (movesLeft == 0) {
                List<Location> onePath = createPath(path);
                endPositions.Add(onePath);
            } else {
                //To the right
                if (position.column + 1 != state.gridSize.Width && (
                    (state.types[position.row][position.column + 1] == (int)SquareType.Properties && state.values[position.row][position.column + 1] > 0) ||
                    (state.types[position.row][position.column + 1] != (int)SquareType.Properties)
                    )) {
                    Location p = new Location(position.column + 1, position.row);
                    //Makes sure to not add the square where it came from
                    if (previousPosition.Equals(p) == false) {
                        //In case it's a line. 
                        if (state.types[position.row][position.column + 1] == (int)SquareType.Lines)
                            checkPossibleWays(movesLeft, p, position, path);
                        else
                            checkPossibleWays(movesLeft - 1, p, position, path);
                    }
                }

                //To the left
                if (position.column - 1 != -1 && (
                    (state.types[position.row][position.column - 1] == (int)SquareType.Properties && state.values[position.row][position.column - 1] > 0) ||
                    (state.types[position.row][position.column - 1] != (int)SquareType.Properties)
                    )) {
                    Location p = new Location(position.column - 1, position.row);
                    //Makes sure to not add the square where it came from
                    if (previousPosition.Equals(p) == false) {
                        //In case it's a line. 
                        if (state.types[position.row][position.column - 1] == (int)SquareType.Lines)
                            checkPossibleWays(movesLeft, p, position, path);
                        else
                            checkPossibleWays(movesLeft - 1, p, position, path);
                    }
                }

                //Up
                if (position.row - 1 != -1 && (
                    (state.types[position.row - 1][position.column] == (int)SquareType.Properties && state.values[position.row - 1][position.column] > 0) ||
                    (state.types[position.row - 1][position.column] != (int)SquareType.Properties)
                    )) {

                    Location p = new Location(position.column, position.row - 1);
                    //Makes sure to not add the square where it came from
                    if (previousPosition.Equals(p) == false) {
                        //In case it's a line. 
                        if (state.types[position.row - 1][position.column] == (int)SquareType.Lines)
                            checkPossibleWays(movesLeft, p, position, path);
                        else
                            checkPossibleWays(movesLeft - 1, p, position, path);
                    }
                }

                //Down
                if (position.row + 1 != state.gridSize.Height && (
                    (state.types[position.row + 1][position.column] == (int)SquareType.Properties && state.values[position.row + 1][position.column] > 0) ||
                    (state.types[position.row + 1][position.column] != (int)SquareType.Properties)
                    )) {

                    Location p = new Location(position.column, position.row + 1);
                    //Makes sure to not add the square where it came from
                    if (previousPosition.Equals(p) == false) {
                        //In case it's a line. 
                        if (state.types[position.row + 1][position.column] == (int)SquareType.Lines)
                            checkPossibleWays(movesLeft, p, position, path);
                        else
                            checkPossibleWays(movesLeft - 1, p, position, path);
                    }
                }
            }
            path.RemoveAt(path.Count - 1);
        }

        private List<Command_Board.Location> createPath(List<Command_Board.Location> path) {
            List<Location> onePath = new List<Location>();
            for (int i = 0; i < path.Count; i++) {
                onePath.Add(path.ElementAt<Location>(i));
            }

            return onePath;
        }

        private int CheckForLines(Location current, Location end) {
            int valid = -1;

            if (current.row == end.row && Math.Abs(end.column - current.column) >= 2) {

                //Takes care of lines going to the right
                if (end.column > current.column) {
                    int counter = 0;
                    for (int j = current.column + 1; j < end.column; j++) {
                        if (state.types[current.row][j] == 3)
                            counter++;
                    }

                    if (counter == end.column - current.column - 1)
                        valid = (int)Direction.RIGHT;
                } else {
                    //takes care of lines going to the left
                    int counter = 0;
                    for (int j = end.column + 1; j < current.column; j++) {
                        if (state.types[current.row][j] == 3)
                            counter++;
                    }

                    if (counter == current.column - end.column - 1)
                        valid = (int)Direction.LEFT;
                }
            } else if (current.column == end.column && Math.Abs(end.row - current.row) >= 2) {

                //Takes care of lines going to the bottom
                if (end.row > current.row) {
                    int counter = 0;
                    for (int j = current.row + 1; j < end.row; j++) {
                        if (state.types[j][current.column] == 3)
                            counter++;
                    }

                    if (counter == end.row - current.row - 1)
                        valid = (int)Direction.DOWN;
                } else {
                    //takes care of lines going to the top
                    int counter = 0;
                    for (int j = end.row + 1; j < current.row; j++) {
                        if (state.types[j][current.column] == 3)
                            counter++;
                    }

                    if (counter == current.row - end.row - 1)
                        valid = (int)Direction.UP;
                }
            }

            return valid;
        }

        private void BuyPayPass(int row, int col) {
            Location l = new Location(col, row);

            //0 is You can Buy it
            //1 you have to pay
            //2 you own it
            //3 checkpoint or home and you can upgrade whichever
            int path = 0;
            int personReceivingMoney = 0;

            if (state.types[row][col] == (int)SquareType.Home || state.types[row][col] == (int)SquareType.Checkpoints) {
                path = 3;
            } else {
                for (int i = 0; i < state.numOfPlayers; i++) {
                    if (state.owned[i].Contains(l)) {
                        if (i == playerNumber - 1) {
                            path = 1;
                        } else {
                            path = 2;
                            personReceivingMoney = i;
                        }
                    }
                }
            }


            if (path == 0 && state.values[row][col] > 0 && players[playerNumber - 1].cash >= state.values[row][col] && cardPanel.Controls.Count > 0) {
                DialogResult = MessageBox.Show("Would you like to buy this property for " + state.values[row][col] + "?", "Purchase", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (DialogResult == DialogResult.Yes) {
                    Form3 selectCard = new Form3(cardPanel);
                    selectCard.ShowDialog();
                    if (selectCard.valid && cardsToDelete[0] != null) {
                        cardPanel.Controls.RemoveAt(selectCard.index);

                        state.owned[playerNumber - 1].Add(l);
                        players[playerNumber - 1].cash -= state.values[row][col];
                        prevInnerShapeBrush = new SolidBrush(propertyBackground[playerNumber - 1]);
                        prevInnerRectBrush = new SolidBrush(state.innerRectColor[row][col]);
                        prevRectColor = new Pen(state.rectColor[row][col], 1f);
                        prevShapeColor = new Pen(state.shapeColor[row][col], 9f);

                        state.innerShapeColor[row][col] = propertyBackground[playerNumber - 1];
                        drawSquare(row, col, state.circleGrid[row][col], state.rectangleGrid[row][col], flowLayoutPanel1.CreateGraphics());
                        cardsToDelete[0] = null;
                    }
                }
                finishButton_Click(new object(), new EventArgs());
                //Update Total Values method
            } else if (path == 2) {
                players[playerNumber - 1].cash -= TollAmount(state.values[row][col], state.lvls[row][col]);
                players[personReceivingMoney].cash += TollAmount(state.values[row][col], state.lvls[row][col]);
                //Update Total Values method

                if (players[playerNumber - 1].cash < 0 && state.owned[playerNumber - 1].Count > 0) {
                    selling = true;
                    MessageBox.Show("You must sell property to be above 0 in cash");
                } else {
                    finishButton_Click(new object(), new EventArgs());
                }
            } else if (path == 1) {
                DialogResult = MessageBox.Show("Would you like to upgrade this specific Property?", "Upgrade?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (DialogResult == DialogResult.Yes) {
                    mustUpgradeColumn = col;
                    mustUpgradeRow = row;
                    upgradeAllowed = true;
                } else {
                    finishButton_Click(new object(), new EventArgs());
                }
            } else if (path == 3) {
                DialogResult = MessageBox.Show("Would you like to upgrade any Property? If yes select which.", "Upgrade?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (DialogResult == DialogResult.Yes) {
                    upgradeAllowed = true;
                } else {
                    finishButton_Click(new object(), new EventArgs());
                }
            }
        }

        private void levelPanel_Paint(object sender, PaintEventArgs e) {

        }

        private void finishButton_Click(object sender, EventArgs e) {
            upgradeAllowed = false;
            rolled = 0;
            isRolling = 0;
            mustUpgradeColumn = -1;
            mustUpgradeRow = -1;
            turns++;
            proxy.setTurns(turns);
            proxy.setPlayer(playerNumber - 1, players[playerNumber - 1]);
            proxy.setState(state);
        }

        private void upgradeButton_Click(object sender, EventArgs e) {
            if (upgradeAllowed) {
                Location l;
                if (mustUpgradeRow == -1) {
                    l = new Location(displayedColumnIndex, displayedRowIndex);
                } else if (mustUpgradeRow != displayedRowIndex || mustUpgradeColumn != displayedColumnIndex) {
                    MessageBox.Show("You can only upgrade the property you landed on");
                    return;
                } else
                    l = new Location(displayedColumnIndex, displayedRowIndex);

                if (state.owned[playerNumber - 1].Contains(l) == true) {
                    if (radioButton0.Checked && state.lvls[displayedRowIndex][displayedColumnIndex] < 0) {
                        players[playerNumber - 1].cash -= UpgradeCost(state.values[displayedRowIndex][displayedColumnIndex], 0);
                        state.lvls[displayedRowIndex][displayedColumnIndex] = 0;
                    } else if (radioButton1.Checked && state.lvls[displayedRowIndex][displayedColumnIndex] < 1) {
                        players[playerNumber - 1].cash -= UpgradeCost(state.values[displayedRowIndex][displayedColumnIndex], 1) - UpgradeCost(state.values[displayedRowIndex][displayedColumnIndex], state.lvls[displayedRowIndex][displayedColumnIndex]);
                        state.lvls[displayedRowIndex][displayedColumnIndex] = 1;
                    } else if (radioButton2.Checked && state.lvls[displayedRowIndex][displayedColumnIndex] < 2) {
                        players[playerNumber - 1].cash -= UpgradeCost(state.values[displayedRowIndex][displayedColumnIndex], 2) - UpgradeCost(state.values[displayedRowIndex][displayedColumnIndex], state.lvls[displayedRowIndex][displayedColumnIndex]);
                        state.lvls[displayedRowIndex][displayedColumnIndex] = 2;
                    } else if (radioButton3.Checked && state.lvls[displayedRowIndex][displayedColumnIndex] < 3) {
                        players[playerNumber - 1].cash -= UpgradeCost(state.values[displayedRowIndex][displayedColumnIndex], 3) - UpgradeCost(state.values[displayedRowIndex][displayedColumnIndex], state.lvls[displayedRowIndex][displayedColumnIndex]);
                        state.lvls[displayedRowIndex][displayedColumnIndex] = 3;
                    } else if (radioButton4.Checked && state.lvls[displayedRowIndex][displayedColumnIndex] < 4) {
                        players[playerNumber - 1].cash -= UpgradeCost(state.values[displayedRowIndex][displayedColumnIndex], 4) - UpgradeCost(state.values[displayedRowIndex][displayedColumnIndex], state.lvls[displayedRowIndex][displayedColumnIndex]);
                        state.lvls[displayedRowIndex][displayedColumnIndex] = 4;
                    } else {
                        MessageBox.Show("You can't upgrade to the same level -_-\"");
                        return;
                    }

                    tollLabel.Text = "Toll : " + TollAmount(state.values[displayedRowIndex][displayedColumnIndex], state.lvls[displayedRowIndex][displayedColumnIndex]);
                    levelLabel.Text = "Current Level: " + state.lvls[displayedRowIndex][displayedColumnIndex];
                    int totalValueAlready = UpgradeCost(state.values[displayedRowIndex][displayedColumnIndex], state.lvls[displayedRowIndex][displayedColumnIndex]);
                    for (int i = 0; i < 5; i++) {
                        ((RadioButton)radioPanel.Controls[i]).Checked = false;
                        if (state.lvls[displayedRowIndex][displayedColumnIndex] >= i)
                            radioPanel.Controls[i].Text = String.Format("Level {0}. {1,5} | {2,5}", i, 0, TollAmount(state.values[displayedRowIndex][displayedColumnIndex], i));
                        else
                            radioPanel.Controls[i].Text = String.Format("Level {0}. {1,5} | {2,5}", i, UpgradeCost(state.values[displayedRowIndex][displayedColumnIndex], i) - totalValueAlready, TollAmount(state.values[displayedRowIndex][displayedColumnIndex], i));

                        if (players[playerNumber - 1].cash < UpgradeCost(state.values[displayedRowIndex][displayedColumnIndex], i) - totalValueAlready)
                            radioPanel.Controls[i].Enabled = false;
                        else
                            radioPanel.Controls[i].Enabled = true;
                    }

                    MessageBox.Show("Upgraded!");
                    upgradeAllowed = false;
                    mustUpgradeColumn = -1;
                    mustUpgradeRow = -1;
                    finishButton_Click(new Object(), new EventArgs());
                } else {
                    MessageBox.Show("You don't own this property");
                    return;
                }
            } else {
                MessageBox.Show("You can't upgrade properties unless you've landed on it, home, or a checkpoint.");
            }

        }

        Random randomCard = new Random();
        private Panel CreateCard(int i) {
            Panel p = new Panel();
            Label l = new Label();

            int randomNum = randomCard.Next(100);

            p.Location = new System.Drawing.Point(0 + 67 * (i - 1), 0);
            p.MaximumSize = new System.Drawing.Size(67, 102);
            p.AutoSize = false;
            p.Size = new System.Drawing.Size(67, 102);
            p.TabIndex = 0;
            p.BorderStyle = BorderStyle.FixedSingle;
            p.MouseClick += new System.Windows.Forms.MouseEventHandler(cardClick);
            p.Margin = new Padding(0);

            // 
            // l1
            // 
            l.AutoSize = true;
            l.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            l.ForeColor = System.Drawing.Color.Yellow;
            l.Location = new System.Drawing.Point(14, 10);
            l.Name = "typeLabel";
            l.Size = new System.Drawing.Size(67, 24);
            l.TabIndex = 0;

            //Random
            if (randomNum < 10) {
                p.BackColor = Color.Black;
                p.Name = "Black";
                cardsPossesed.Add(0);
                //King
            } else if (randomNum >= 10 && randomNum < 30) {
                p.BackColor = Color.Yellow;
                p.Name = "Yellow";
                cardsPossesed.Add(1);
                //Sword
            } else if (randomNum >= 30 && randomNum < 60) {
                p.BackColor = Color.Red;
                p.Name = "Red";
                cardsPossesed.Add(2);
                //Magic
            } else {
                p.BackColor = Color.Blue;
                p.Name = "Blue";
                cardsPossesed.Add(3);
            }

            return p;
        }

        Panel[] cardsToDelete = new Panel[5];
        private void cardClick(object sender, MouseEventArgs e) {
            cardsToDelete[0] = (Panel)sender;
        }

        private void updateButton_Click(object sender, EventArgs e) {
            state = proxy.getState();
            for (int i = 0; i < state.numOfPlayers; i++) {
                players[i] = proxy.getPlayer(i);
            }
            turns = proxy.getTurns();
        }
    }

    [Serializable]
    public class Location : IEquatable<Location> {
        public int column { set; get; }
        public int row { set; get; }

        public Location(int x, int y) {
            column = x;
            row = y;

        }

        override public string ToString() {
            return "Row " + ((char)((int)'A' + row)).ToString() + " and Column " + column;
        }

        public bool Equals(Location other) {
            if (this.row == other.row && this.column == other.column)
                return true;
            else
                return false;
        }
    }
}

public class CustomFlowLayoutPanel : FlowLayoutPanel {

    public CustomFlowLayoutPanel() {             // Set the value of the double-buffering style bits to true. 
        SetStyle(ControlStyles.DoubleBuffer |
           ControlStyles.UserPaint |
           ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer,
           true);
        UpdateStyles();
    }
}

public static class converter {
    public static T[,] ToMultiD<T>(this T[][] jArray) {
        int i = jArray.Count();
        int j = jArray.Select(x => x.Count()).Aggregate(0, (current, c) => (current > c) ? current : c);


        var mArray = new T[i, j];

        for (int ii = 0; ii < i; ii++) {
            for (int jj = 0; jj < j; jj++) {
                mArray[ii, jj] = jArray[ii][jj];
            }
        }

        return mArray;
    }

    public static T[][] ToJagged<T>(this T[,] mArray) {
        var cols = mArray.GetLength(0);
        var rows = mArray.GetLength(1);
        var jArray = new T[cols][];
        for (int i = 0; i < cols; i++) {
            jArray[i] = new T[rows];
            for (int j = 0; j < rows; j++) {
                jArray[i][j] = mArray[i, j];
            }
        }
        return jArray;
    }

}

enum Direction : int {
    NONE = 0,
    RIGHT,
    LEFT,
    UP,
    DOWN
};

enum SquareType : int {
    Properties,
    Specials,
    Checkpoints,
    Lines,
    Home,
    Deadzones,
    MovingCloud
};


