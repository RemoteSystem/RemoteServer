using SuperSocket.Facility.Protocol;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocketServer
{
    class ProtocolReceiveFilter : FixedHeaderReceiveFilter<ProtocolRequestInfo>
    {
        public ProtocolReceiveFilter()
            : base(1)//协议头部应该是7，但为了兼容心跳，设置为1(意思是收到1个字节，就来进行匹配)
        {
        }

        /// <summary>
        /// 获取数据域和结尾字节长度
        /// </summary>
        /// <param name="header"></param>
        /// <param name="offset"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        protected override int GetBodyLengthFromHeader(byte[] header, int offset, int length)
        {
            //心跳
            byte[] heart = new byte[1];
            Buffer.BlockCopy(header, offset, heart, 0, 1);
            if (byteToHexStr(heart).ToLower() == "66")
            {
                return 0;
            }

            if (header.Length >= offset + 2)
            {
                //读取两个字节，判断是否为消息的开始标识
                byte[] startByte = new byte[2];
                Buffer.BlockCopy(header, offset, startByte, 0, 2);
                if (byteToHexStr(startByte).ToLower() == "aa55")
                {
                    byte[] headerData = new byte[4];
                    Buffer.BlockCopy(header, offset + 3, headerData, 0, 4);
                    if (BitConverter.IsLittleEndian)//判断计算机结构的endian设置(高低位转换)
                        Array.Reverse(headerData);
                    int len = BitConverter.ToInt32(headerData, 0);

                    Console.WriteLine("解析出的长度" + (len));

                    return len + 6 + 2;//结尾有2个字节(兼容心跳的情况下，增加头部6个位置。本来是7个，已经设置了1个(名义上的头部)，剩6个。加上结尾2个)
                }
            }

            //这里有点问题，异常数据才能走到这里，但没有获取到这段数据是多长.
            //会导致多次解析（每次读取一个字节）。
            return 0;
        }

        /// <summary>
        /// 实现帧内容解析
        /// </summary>
        /// <param name="header"></param>
        /// <param name="bodyBuffer"></param>
        /// <param name="offset"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        protected override ProtocolRequestInfo ResolveRequestInfo(ArraySegment<byte> header, byte[] bodyBuffer, int offset, int length)
        {
            ProtocolRequestInfo res = new ProtocolRequestInfo();

            length -= 6;//减掉为了兼容心跳多出来的6个(头部)
            if (length > 1)
            {
                byte[] control = new byte[1];
                Buffer.BlockCopy(bodyBuffer, offset + 1, control, 0, 1);
                res.Control = byteToHexStr(control);

                byte[] dstArray = new byte[length];
                //Buffer.BlockCopy(bodyBuffer, offset, dstArray, 0, length);
                //res.Length = length - 2;
                Buffer.BlockCopy(bodyBuffer, offset + 6, dstArray, 0, length);//为了兼容心跳，偏移缩小了6，这里补回来。
                res.Length = length - 2;//减去末尾的2位就是内容的长度
                byte[] Data = new byte[res.Length];
                Buffer.BlockCopy(dstArray, 0, Data, 0, Data.Length);
                res.Data = Encoding.UTF8.GetString(Data);
                byte[] Verifi = new byte[2];//末尾2位结束标识
                Buffer.BlockCopy(dstArray, res.Length, Verifi, 0, Verifi.Length);
                res.Verification = byteToHexStr(Verifi);
            }

            return res;
        }

        /// <summary>
        /// 字节数组转16进制字符串
        /// </summary>
        /// <param name="bytes"></param>
        /// <returns></returns>
        public static string byteToHexStr(byte[] bytes)
        {
            string returnStr = "";
            if (bytes != null)
            {
                for (int i = 0; i < bytes.Length; i++)
                {
                    returnStr += bytes[i].ToString("X2");
                }
            }
            return returnStr;
        }               

    }
}
