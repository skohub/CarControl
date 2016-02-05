using System;
using System.Text;
using CarControl.CarConnect.Commands;
using CarControl.CarConnect.Protocol;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CarConnect.Test
{
    [TestClass]
    public class BinaryAuthProtoTest
    {
        [TestMethod]
        public void MustAuthenticate()
        {
            const string imei = "123456789012345";
            const string hash = "123";
            var imeiBytes = Encoding.UTF8.GetBytes(imei);
            var hashBytes = Encoding.UTF8.GetBytes(hash);
            var totalLength = (byte) (4 + imei.Length + hash.Length);
            var bufBytes = new byte[totalLength];
            bufBytes[0] = totalLength;
            bufBytes[1] = (byte) BinaryAuthProto.Action.Auth;
            bufBytes[2] = (byte) imei.Length;
            bufBytes[3] = (byte) hash.Length;
            Array.Copy(imeiBytes, 0, bufBytes, 4, imei.Length);
            Array.Copy(hashBytes, 0, bufBytes, 4 + imei.Length, hashBytes.Length);
            
            var tcpConnectionMock = new TcpConnectionMock();
            var carServiceMock = new CarServiceMock();
            var commandFactory = new TextCommandFactory(carServiceMock, new SensorServiceMock(), new SmsServiceMock());
            var authProto = new BinaryAuthProto(tcpConnectionMock, commandFactory, 1, 1, new CarServiceMock());
            authProto.Receive(bufBytes);
            byte[] ans = new byte[2];
            Array.Copy(tcpConnectionMock.LastSentBuf, 1, ans, 0, 2);
            byte[] expect = {1, 1};
            CollectionAssert.AreEqual(ans, expect);
        }

        [TestMethod]
        public void MustNotAuthenticateWithWrongHash()
        {
            const string imei = "123456789012345";
            const string hash = "1224";
            var imeiBytes = Encoding.UTF8.GetBytes(imei);
            var hashBytes = Encoding.UTF8.GetBytes(hash);
            var totalLength = (byte)(4 + imei.Length + hash.Length);
            var tcpConnectionMock = new TcpConnectionMock();
            var carServiceMock = new CarServiceMock();
            var commandFactory = new TextCommandFactory(carServiceMock, new SensorServiceMock(), new SmsServiceMock());
            var authProto = new BinaryAuthProto(tcpConnectionMock, commandFactory, 1, 1, new CarServiceMock());
            // для проверки отправим по частям, будто TCP разделил
            var bufBytes = new byte[] { totalLength };
            authProto.Receive(bufBytes);
            bufBytes = new byte[]
            {
                (byte) BinaryAuthProto.Action.Auth,
                (byte) imei.Length,
                (byte) hash.Length
            };
            authProto.Receive(bufBytes);
            authProto.Receive(imeiBytes);
            authProto.Receive(hashBytes);
            byte[] ans = new byte[2];
            Array.Copy(tcpConnectionMock.LastSentBuf, 1, ans, 0, 2);
            byte[] expect = { 1, 0 };
            CollectionAssert.AreEqual(ans, expect);
        }

    }
}
