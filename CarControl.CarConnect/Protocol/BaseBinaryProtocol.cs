using System;
using CarControl.CarConnect.Commands;
using CarControl.CarConnect.Server;

namespace CarControl.CarConnect.Protocol
{
    public class BaseBinaryProtocol : BaseCarProtocol
    {
        public const byte CommandMode = 1;
        public const byte Temp1 = 2;

        private byte[] _buf;
        private int _receivedBytes = 0;
        private int _totalBytes = 0;

        public BaseBinaryProtocol(ITcpConnection connection, ICommandFactory commandFactory, int id, int carId)
            : base(connection, commandFactory, id, carId)
        {
        }

        public BaseBinaryProtocol() { }

        public override void Receive(byte[] bufBytes)
        {
            // Первый пакет с указанием общей длины
            if (_receivedBytes == 0)
            {
                _receivedBytes = bufBytes.Length;
                _totalBytes = bufBytes[0];
                if ((_totalBytes < 1) || (_totalBytes > 255))
                {
                    Connection.Close();
                    return;
                }
                _receivedBytes = bufBytes.Length;
                _buf = new byte[_totalBytes - 1];
                // копируем во внутренний буфер, пропуская длину
                Array.Copy(bufBytes, 1, _buf, 0, _receivedBytes - 1);
            }
            // новый пакет с продолжением команды
            else
            {
                var dataLength = bufBytes.Length;
                if ((dataLength < 1) || (dataLength > 255) || (_totalBytes - _receivedBytes - dataLength < 0))
                {
                    Connection.Close();
                    return;
                }
                Array.Copy(bufBytes, 0, _buf, _receivedBytes - 1, dataLength);
                _receivedBytes += dataLength;
            }
            if (_receivedBytes == _totalBytes)
            {
                try
                {
                    CommandReceived(_buf);
                }
                catch 
                {
                    Send(new byte[] {1, 0});
                }
                _totalBytes = 0;
                _receivedBytes = 0;
                _buf = null;
            }
        }

        public override void Send(byte[] bufBytes)
        {
            if (bufBytes.Length > 254) return;
            var buf = new byte[bufBytes.Length + 1];
            buf[0] = (byte) (bufBytes.Length + 1);
            Array.Copy(bufBytes, 0, buf, 1, bufBytes.Length);
            base.Send(buf);
        }

        public virtual void CommandReceived(byte[] bufBytes)
        {
            throw new NotImplementedException();
        }

    }
}
