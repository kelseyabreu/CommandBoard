using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Drawing;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
using CommandBoardServiceLibrary;

namespace CommandBoardConsoleHost
{
    class Program
    {
        static void Main(string[] args)
        {
            BinaryFormatter bFormatter = new BinaryFormatter();

            FileStream inPut = new FileStream("C:\\Users\\Kelsey\\Desktop\\savefile\\lolblackjagged.yup", FileMode.Open, FileAccess.Read);
            Command_Board.States state = new Command_Board.States();

            state.gridSize = (Size)bFormatter.Deserialize(inPut);

            state.innerRectColor = (Color[][])bFormatter.Deserialize(inPut);
            state.innerShapeColor = (Color[][])bFormatter.Deserialize(inPut);
            state.rectColor = (Color[][])bFormatter.Deserialize(inPut);
            state.shapeColor = (Color[][])bFormatter.Deserialize(inPut);
            state.types = (int[][])bFormatter.Deserialize(inPut);
            state.rotation = (int[][])bFormatter.Deserialize(inPut);
            state.values = (int[][])bFormatter.Deserialize(inPut);
            state.circleGrid = (Rectangle[][])bFormatter.Deserialize(inPut);
            state.rectangleGrid = (Rectangle[][])bFormatter.Deserialize(inPut);
            state.lvls = converter.ToJagged(new int[state.gridSize.Height,state.gridSize.Width]);
            state.owned = new List<Command_Board.Location>[4];

            for (int i = 0; i < state.owned.Length; i++) {
                state.owned[i] = new List<Command_Board.Location>();
            }

            inPut.Close();


            Console.WriteLine("Number of Players?");
            state.numOfPlayers = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("Total Value?");
            state.totalValue = Convert.ToInt32(Console.ReadLine());

            //ICommandBoardService proxy;
            using (ServiceHost host = new ServiceHost(typeof(CommandBoardServiceLibrary.CommandBoardService)))
            {
                NetTcpBinding ntb = new NetTcpBinding();
                NetTcpSecurity nts = new NetTcpSecurity();
                nts.Mode = SecurityMode.None;
                ntb.Security = nts;

                host.AddServiceEndpoint(typeof(
                    CommandBoardServiceLibrary.ICommandBoardService),
                    new NetTcpBinding(),
                    "net.tcp://localhost:9000/commandboard");
                host.Open();

                ICommandBoardService proxy = ChannelFactory<ICommandBoardService>.CreateChannel(
                             new NetTcpBinding(),
                            new EndpointAddress(
                            "net.tcp://localhost:9000/commandboard"));

                proxy.setState(state);

                Console.WriteLine("Good to go");
                Console.ReadLine();

            }
        }
    }
}
