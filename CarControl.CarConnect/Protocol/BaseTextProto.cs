using System;
using System.Text;
using CarControl.CarConnect.Commands;
using CarControl.CarConnect.Server;

namespace CarControl.CarConnect.Protocol
{
    public class BaseTextProto : BaseCarProtocol
    {
        private readonly StringBuilder _cmd = new StringBuilder();

        public BaseTextProto(ITcpConnection connection, ICommandFactory commandFactory, int id, int carId)
            : base(connection, commandFactory, id, carId)
        {
        }

        public BaseTextProto() { }

        public override void Receive(byte[] bufBytes)
        {
            for (int i = 0; i < bufBytes.Length; i++)
            {
                switch (bufBytes[i])
                {
                    case 13:
                        break;
                    case 10:
                        try
                        {
                            CommandReceived(_cmd.ToString());
                        }
                        catch (InvalidOperationException)
                        {
                        }
                        catch (Exception e)
                        {
                            Send(e.GetType().Name);
                        }
                        _cmd.Clear();
                        break;
                    default:
                        _cmd.Append(Encoding.UTF8.GetString(bufBytes, i, 1));
                        break;
                }
            }
        }

        public override void Send(string cmd)
        {
            cmd += "\r\n";
            Send(Encoding.UTF8.GetBytes(cmd));
        }

        public virtual void CommandReceived(string cmd)
        {
            throw new NotImplementedException();
        }

    }
}
