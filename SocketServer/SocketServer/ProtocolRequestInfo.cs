using SuperSocket.SocketBase.Protocol;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocketServer
{
    public class ProtocolRequestInfo : IRequestInfo
    {
        /// <summary>
        /// [不使用]
        /// </summary>
        public string Key { get; set; }

        /// <summary>
        /// 开始标志位
        /// </summary>
        public string Start { get; set; }

        /// <summary>
        /// 协议类型
        /// </summary>
        public string Control { get; set; }

        /// <summary>
        /// 数据长度
        /// </summary>
        public int Length { get; set; }

        /// <summary>
        /// 数据域
        /// </summary>
        public string Data { get; set; }

        /// <summary>
        /// 校验位
        /// </summary>
        public string Verification { get; set; }

    }
}
