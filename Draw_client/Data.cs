using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;

namespace Client
{
    [DataContract]
    class Data
    {
        [DataMember]
        public int DrawMode;

        [DataMember]
        public int Color;

        [DataMember]
        public int X0, Y0, X, Y;

        [DataMember]
        public int Cx, Cy;

        [DataMember]
        public int PenSize;
    }
}
