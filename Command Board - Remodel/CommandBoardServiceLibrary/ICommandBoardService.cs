using CommandBoard_Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace CommandBoardServiceLibrary
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "ICommandBoardService" in both code and config file together.
    [ServiceContract(SessionMode=SessionMode.Allowed)]
    public interface ICommandBoardService
    {
        [OperationContract]
        void Hello();

        [OperationContract]
        States getState();

        [OperationContract]
        void setState(States s);

        [OperationContract]
        string setConnected(int i);

        [OperationContract]
        int getConnected();

        [OperationContract]
        void setTurns(int i);

        [OperationContract]
        int getTurns();

        [OperationContract]
        void setPlayer(int index, Player player);

        [OperationContract]
        Player getPlayer(int index);

    }
}
