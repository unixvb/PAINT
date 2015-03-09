using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Draw_server
{
    [DataContract]
    class Data
    {
        [DataMember]
        public int DrawMode;

        [DataMember]
        public Color Color;

        [DataMember]
        public int X0, Y0, X, Y;

        [DataMember]
        public int Cx, Cy;

        [DataMember]
        public int PenSize;
    }
}
