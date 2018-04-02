using Newtonsoft.Json;
using RemoteModel;
using System;

namespace ServerController
{
    public class ServerService
    {
        public ResultInfo SendCommand(string sessionId)
        {
            ResultInfo result = new ResultInfo();

            return result;
        }

        public ResultInfo updateBloodParas(string sessionId, BloodSettings obj)
        {
            ResultInfo result = new ResultInfo();
            try
            {
                string msg = JsonConvert.SerializeObject(obj, Formatting.Indented, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });

                result = SocketServer.SocketServer.sendMsg(sessionId, msg);
            }
            catch (Exception e)
            {
                result.code = 101;
                result.msg = "发送出错." + e.Message;
            }

            return result;
        }

    }
}
