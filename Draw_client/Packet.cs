using System;
using System.Collections.Generic;
using System.Text;

namespace Client
{
    public enum DataIdentifier
    {
        Message,
        Null
    }

    public class Packet
    {
        #region Private Members
        private DataIdentifier dataIdentifier;
        private string message;
        #endregion

        #region Public Properties
        public DataIdentifier ChatDataIdentifier
        {
            get { return dataIdentifier; }
            set { dataIdentifier = value; }
        }

        public string ChatMessage
        {
            get { return message; }
            set { message = value; }
        }
        #endregion

        #region Methods

        public Packet()
        {
            this.dataIdentifier = DataIdentifier.Null;
            this.message = null;
        }

        public Packet(byte[] dataStream)
        {
            this.dataIdentifier = (DataIdentifier)BitConverter.ToInt32(dataStream, 0);

            int msgLength = BitConverter.ToInt32(dataStream, 4);

            if (msgLength > 0)
                this.message = Encoding.UTF8.GetString(dataStream, 8, msgLength);
            else
                this.message = null;
        }

        public byte[] GetDataStream()
        {
            List<byte> dataStream = new List<byte>();

            dataStream.AddRange(BitConverter.GetBytes((int)this.dataIdentifier));

            if (this.message != null)
                dataStream.AddRange(BitConverter.GetBytes(this.message.Length));
            else
                dataStream.AddRange(BitConverter.GetBytes(0));

            if (this.message != null)
                dataStream.AddRange(Encoding.UTF8.GetBytes(this.message));

            return dataStream.ToArray();
        }

        #endregion
    }
}
