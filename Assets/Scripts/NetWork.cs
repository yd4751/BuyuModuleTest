using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LitJson;
using System.Runtime.InteropServices;
namespace CNetWork
{
    public static class NetWork
    {

        public enum ProtocolType
        {
            PROTO_TYPE_NULL = 0,
            PROTO_TYPE_JSON = 1,
            PROTO_TYPE_PB = 2,
            PROTO_TYPE_MAX = 3,
        }

        //定义回掉
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void cbEventHandler(int nClientID, int nCmd, int nMsgLength, string msgBuf, ProtocolType type);

        public const string RelativeDir = "" + "libnetcore";
        [DllImport(RelativeDir, CallingConvention = CallingConvention.Cdecl)]
        public static extern void Config(int port);
        [DllImport(RelativeDir, CallingConvention = CallingConvention.Cdecl)]
        public static extern void RegisterEvnetHandler(cbEventHandler handler);
        [DllImport(RelativeDir, CallingConvention = CallingConvention.Cdecl)]
        public static extern int Connect(string ip, int port);
        [DllImport(RelativeDir, CallingConvention = CallingConvention.Cdecl)]
        public static extern bool SendData(int id, int cmd, string data, int length, ProtocolType type);
        [DllImport(RelativeDir, CallingConvention = CallingConvention.Cdecl)]
        public static extern bool Start();
        [DllImport(RelativeDir, CallingConvention = CallingConvention.Cdecl)]
        public static extern void Stop();

        static int m_nServerID = -1;
        static bool m_bInit = false;
        public static void OnNetworkEvent(int nClientID, int nCmd, int nMsgLength, string msgBuf, ProtocolType type)
        {
            Debug.Log("[Recv]: Cmd:" + nCmd);
            //解析
            //JsonData reply = JsonMapper.ToObject(msgBuf);
            //Console.WriteLine(reply.ToJson());
        }

        public static void Init()
        {
            if (m_bInit) return;

            Config(0);
            Start();
            m_bInit = true;
        }
        public static void ReconnectServer()
        {
            Init();
            if (IsServerInline()) return;

            m_nServerID = Connect("127.0.0.1", 9999);
            if (m_nServerID > 0)
            {
                Debug.Log("Connect server success!");
            }
        }
        public static bool IsServerInline()
        {
            return m_nServerID > 0 ? true : false;
        }
        public static void SendDataToServer(int nCmd, string data)
        {
            ReconnectServer();
            if (!IsServerInline())
            {
                Debug.Log("send error , server is offline!");
                return;
            }
            if (!SendData(m_nServerID, nCmd, data, data.Length, ProtocolType.PROTO_TYPE_JSON))
            {
                Debug.Log("SendDataToServer failed!");
            }
        }


        //登录请求
        public static void SendLoginRequest(string account, string password)
        {
            //5004 
            JsonData data = new JsonData();
            data["account"] = account;
            data["password"] = password;
            string strSend = data.ToJson();
            SendDataToServer(5004, strSend);
        }
        //
        public static void SendFishDie()
        {
            //1234
            JsonData data = new JsonData();
            data["operate"] = "fishdie";
            string strSend = data.ToJson();
            SendDataToServer(1234, strSend);
        }
        public static void SendFire()
        {
            //1235
            JsonData data = new JsonData();
            data["operate"] = "fire";
            string strSend = data.ToJson();
            SendDataToServer(1234, strSend);
        }
    }
}