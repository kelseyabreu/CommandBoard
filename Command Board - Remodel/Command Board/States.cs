using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Runtime.Serialization;

namespace Command_Board
{    [DataContract]
    public class States
    {
        [DataMember]
        public int[][] rotation { set; get; }

        [DataMember]
        public int[][] types { set; get; }

        [DataMember]
        public int[][] lvls { set; get; }

        [DataMember]
        public List<Location>[] owned { set; get; }

        [DataMember]
        public System.Drawing.Rectangle[][] rectangleGrid { set; get; }

        [DataMember]
        public System.Drawing.Rectangle[][] circleGrid { set; get; }

        [DataMember]
        public int[][] values { set; get; }

        [DataMember]
        public Size gridSize { set; get; }

        [DataMember]
        public Color[][] rectColor { set; get; }

        [DataMember]
        public Color[][] shapeColor { set; get; }

        [DataMember]
        public Color[][] innerRectColor { set; get; }

        [DataMember]
        public Color[][] innerShapeColor { set; get; }

        [DataMember]
        public int numOfPlayers { set; get; }

        [DataMember]
        public int totalValue { set; get; }


    }

}
