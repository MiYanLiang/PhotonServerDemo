using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Photon.SocketServer;
using ExitGames.Logging;
using ExitGames.Logging.Log4Net;
using System.IO;
using log4net.Config;

namespace MyGameServer
{
    /// <summary>
    /// 入口主类，所有的server端 主类都要继承ApplicationBase
    /// </summary>
    public class MyGameServer:ApplicationBase
    {
        public static readonly ILogger log = LogManager.GetCurrentClassLogger();

        //当一个客户端请求链接，返回一个ClientPeer表示和一个客户端的链接
        protected override PeerBase CreatePeer(InitRequest initRequest)
        {
            log.Info("有一个客户端链接了服务器");
            return new ClientPeer(initRequest); //PhotonServer管理该对象
        }
        //初始化
        protected override void Setup()
        {
            //日志初始化
            log4net.GlobalContext.Properties["Photon:ApplicationLogPath"] = Path.Combine(Path.Combine(this.ApplicationRootPath, "bin_Win64"), "log");//日志输出目录
            FileInfo configFileInfo = new FileInfo(Path.Combine(this.BinaryPath, "log4net.config"));
            if (configFileInfo.Exists)
            {
                LogManager.SetLoggerFactory(Log4NetLoggerFactory.Instance); //让phonto得知使用的是什么日志插件
                XmlConfigurator.ConfigureAndWatch(configFileInfo);          //读取日志配置文件
            }
            log.Info("服务器开启初始化完成");
        }
        //server端关闭时
        protected override void TearDown()
        {
            log.Info("服务器应用关闭");   
        }
    }
}
