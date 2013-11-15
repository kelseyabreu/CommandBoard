using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace CommandBoardServiceLibrary
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "CommandBoardService" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select CommandBoardService.svc or CommandBoardService.svc.cs at the Solution Explorer and start debugging.
    [ServiceBehavior(InstanceContextMode=InstanceContextMode.Single)]
    public class CommandBoardService : ICommandBoardService
    {
        [DataMember]
        public Command_Board.States state;

        [DataMember]
        public Command_Board.Player[] players = new Command_Board.Player[4];

        [DataMember]
        public int connected = 0;

        [DataMember]
        public int turns = 0;

        //private static readonly List<ICommandBoardCallback> subscribers = new List<ICommandBoardCallback>();

        //void NextPlayer(string message, DateTime timestamp)
        //{
        //    subscribers.ForEach(delegate(ICommandBoardCallback callback)
        //    {
        //        if (((ICommunicationObject)callback).State == CommunicationState.Opened)
        //        {
        //            callback.NextPlayer(message, DateTime.Now);
        //        }
        //        else
        //        {
        //            subscribers.Remove(callback);
        //        }
        //    });
        //}

        public void Hello()
        {
        }
        
        public Command_Board.States getState()
        {
            return state;
        }

        public void setState(Command_Board.States s)
        {
            state = s;
        }
        
        public string setConnected(int i){

            connected += i;
            return "Player "+connected+" Connected";
        }

        public int getConnected(){
            return connected;
        }

        public void setTurns(int i)
        {
            turns += i;
        }

        public int getTurns()
        {
            return turns;
        }

        public void setPlayer(int index, Command_Board.Player player)
        {
            players[index] = player;
        }

        public Command_Board.Player getPlayer(int index)
        {
            return players[index];
        }

    }
}
