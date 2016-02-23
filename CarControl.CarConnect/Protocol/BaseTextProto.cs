using System;
using System.Text;
using CarConnect.Model;
using CarControl.CarConnect.CommandsCommon;
using CarControl.CarConnect.InputCommands;
using CarControl.CarConnect.Server;

namespace CarControl.CarConnect.Protocol
{
    public class BaseTextProto : BaseCarProtocol
    {
        private readonly StringBuilder _cmd = new StringBuilder();

        public BaseTextProto(ITcpConnection connection, ICommandFactory commandFactory, int id)
            : base(connection, commandFactory, id)
        {
        }

        public BaseTextProto() { }

        public override void Receive(byte[] bufBytes)
        {
            for (var i = 0; i < bufBytes.Length; i++)
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
            base.Send(Encoding.UTF8.GetBytes(cmd));
        }

        public virtual void CommandReceived(string cmd)
        {
            throw new NotImplementedException();
        }

    }
}
