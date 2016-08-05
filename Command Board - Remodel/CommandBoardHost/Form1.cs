using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.ServiceModel;
using CommandBoardServiceLibrary;
using CommandBoard_Data;

namespace CommandBoardHost
{

    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() != DialogResult.Cancel)
            {
                BinaryFormatter bFormatter = new BinaryFormatter();

                FileStream inPut = new FileStream(openFileDialog1.FileName, FileMode.Open, FileAccess.Read);
                States state = new States();

                state.gridSize = (Size)bFormatter.Deserialize(inPut);

                state.innerRectColor = (Color[][])bFormatter.Deserialize(inPut);
                state.innerShapeColor = (Color[][])bFormatter.Deserialize(inPut);
                state.rectColor = (Color[][])bFormatter.Deserialize(inPut);
                state.shapeColor = (Color[][])bFormatter.Deserialize(inPut);
                state.types = (int[][])bFormatter.Deserialize(inPut);
                state.rotation = (int[][])bFormatter.Deserialize(inPut);
                state.values = (int[][])bFormatter.Deserialize(inPut);

                inPut.Close();

                Form2 players1 = new Form2();
                players1.ShowDialog();
                state.numOfPlayers = players1.numberOfPlayer;
                state.totalValue = players1.totalValue;

                //ICommandBoardService proxy;
                using (ServiceHost host = new ServiceHost(typeof(CommandBoardServiceLibrary.CommandBoardService)))
                {
                    host.AddServiceEndpoint(typeof(
                        CommandBoardServiceLibrary.ICommandBoardService),
                        new NetTcpBinding(),
                        "net.tcp://localhost:9000/commandboard");
                    host.Open();

                  //  ICommandBoardService proxy = ChannelFactory<ICommandBoardService>.CreateChannel(
                  //               new NetTcpBinding(),
                  //              new EndpointAddress(
                  //              "net.tcp://localhost:9001/CommandBoardEndPoint"));

                  //  proxy.setConnected(0);
                  ////  proxy.setState(state);
                  //  Label l = new Label();
                  //  l.Text = proxy.setConnected(0);
                  //  listBox1.Controls.Add(l);
                    Console.ReadLine();

                }

            }
        }
    }
}