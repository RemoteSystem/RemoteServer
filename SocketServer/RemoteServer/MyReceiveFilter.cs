using SuperSocket.Facility.Protocol;
using SuperSocket.SocketBase.Protocol;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RemoteServer
{
    class MyReceiveFilter : BeginEndMarkReceiveFilter<MyRequestInfo>
    {
        //开始和结束标记也可以是两个或两个以上的字节
        private readonly static byte[] BeginMark = new byte[] { (byte)0xaa, (byte)0x55 };
        private readonly static byte[] EndMark = new byte[] { (byte)0x1c, (byte)0x1d };

        public MyReceiveFilter()
            : base(BeginMark, EndMark) //传入开始标记和结束标记
        {

        }

        protected override MyRequestInfo ProcessMatchedRequest(byte[] readBuffer, int offset, int length)
        {
            //TODO: 通过解析到的数据来构造请求实例，并返回
            byte[] buffer = new byte[readBuffer.Length - BeginMark.Length - EndMark.Length - 5];//减去1字节功能位，和4字节的长度位
            Buffer.BlockCopy(readBuffer, BeginMark.Length + 5, buffer, 0, buffer.Length);//将功能位和长度位偏移过

            //byte[]转成string：
            string str = System.Text.Encoding.Default.GetString(buffer);
            MyRequestInfo info = new MyRequestInfo();
            info.JsonInfo = str;
            return info;
        }
    }
}
