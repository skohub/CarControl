using System;
using System.Text;
using CarControl.CarConnect.InCommands;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CarControl.CarConnect.Protocol;

namespace CarConnect.Test
{

    [TestClass]
    public class TextAuthProtoTest
    {
        [TestMethod]
        public void MustAuthenticateText()
        {
            var tcpConnectionMock = new TcpConnectionMock();
            var carServiceMock = new CarServiceMock();
            var commandFactory = new InCommandFactory(carServiceMock, new SensorServiceMock(), new SmsServiceMock());
            var authProto = new TextAuthProto(tcpConnectionMock, commandFactory, 1, 1, carServiceMock);
            authProto.Receive(Encoding.UTF8.GetBytes("123456789012345:123\r\n"));
            var expect = Encoding.UTF8.GetBytes("LOGINOK\r\n");
            Assert.IsTrue(tcpConnectionMock.LastSentBuf.Length > 8);
            var ans = new byte[9];
            Array.Copy(tcpConnectionMock.LastSentBuf, ans, 9);
            CollectionAssert.AreEqual(ans, expect);
        }

        [TestMethod]
        public void MustNotAuthenticateText()
        {
            var tcpConnectionMock = new TcpConnectionMock();
            var carServiceMock = new CarServiceMock();
            var commandFactory = new InCommandFactory(carServiceMock, new SensorServiceMock(), new SmsServiceMock());
            var authProto = new TextAuthProto(tcpConnectionMock, commandFactory, 1, 1, new CarServiceMock());
            authProto.Receive(Encoding.UTF8.GetBytes("123456789012345:12\r\n"));
            var expect = Encoding.UTF8.GetBytes("LOGINFAIL\r\n");
            CollectionAssert.AreEqual(tcpConnectionMock.LastSentBuf, expect);
        }
    }
}
