using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CommandBoardServiceLibrary
{
    using System;
    using System.ServiceModel;

    interface ICommandBoardCallback
    {
        [OperationContract(IsOneWay = true)]
        void NextPlayer(string message, DateTime timestamp);
    }
}
