using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace JcService
{
    public partial class aJcService : ServiceBase
    {
        System.Timers.Timer timer1;  //计时器
        int iHour = 00;
        int iMinute = 00;
        int iSecond = 05;//每天00.00.05 执行
        public aJcService()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            timer1 = new System.Timers.Timer();


            timer1.Interval = 1000;  //设置计时器事件间隔执行时间


            timer1.Elapsed += new System.Timers.ElapsedEventHandler(timer1_Elapsed);


            timer1.Enabled = true;
        }

        protected override void OnStop()
        {
            this.timer1.Enabled = false;
            EventLog.WriteEntry("OnStop", "任务结束");
        }
        private void timer1_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            try
            {
                int intHour = e.SignalTime.Hour;
                int intMinute = e.SignalTime.Minute;
                int intSecond = e.SignalTime.Second;
                // 设置　每天 00：00：05开始执行程序  
                if (intHour == iHour && intMinute == iMinute && intSecond == iSecond)
                {
                    List<string> urlList = new List<string>();
                    string resoultData = HttpUtil.HttpGet2("http://www.caoam.cn/JcService/GetMonitorUrl");
                    Rdata myData = JsonConvert.DeserializeObject<Rdata>(resoultData);
                    if (myData.msg == "操作成功")
                    {
                        urlList.AddRange(myData.urlList);
                    }
                    foreach (var ur in urlList)
                    {
                        List<long> jzItemList = new List<long>();
                        resoultData = HttpUtil.HttpGet2($"{ur}JcService/GetMonitorJzItem");
                        myData = JsonConvert.DeserializeObject<Rdata>(resoultData);
                        if (myData.msg == "操作成功")
                        {
                            foreach (var item in myData.data)
                            {
                                jzItemList.Add(Convert.ToInt64(item));
                            }
                        }
                        foreach (var item in jzItemList)
                        {
                            string url = "https://trade-acs.m.taobao.com/gw/mtop.taobao.detail.getdetail/6.0/?data=itemNumId%22%3A%22{0}%22&callback=__jp5";
                            string result = HttpUtil.HttpGet2(string.Format(url, item));
                            var newdata = new { jzitem = item, result = result };
                            string data = Newtonsoft.Json.JsonConvert.SerializeObject(newdata);
                            resoultData = HttpUtil.PostData($"{ur}JcService/MonitorJzItemChanges", data);
                            myData = JsonConvert.DeserializeObject<Rdata>(resoultData);
                            if (myData.msg == "操作成功")
                            {
                                WriteLogs(0, $"{item}");
                            }
                            else
                            {
                                WriteLogs(1, $"竞品{item}-{myData.msg}");
                            }
                            Thread.Sleep(3000);
                        }
                    }
                    WriteLogs(0, "全部竞品");
                }
            }
            catch (Exception ex)
            {
                WriteLogs(1, $"我异常了:{ex.Message}");
            }


            //// 设置　每个小时的３０分钟开始执行  
            //if (intMinute == iMinute && intSecond == iSecond)
            //{
            //    Console.WriteLine("每个小时的３０分钟开始执行一次！");
            //     write("每个小时的３０分钟开始执行一次！\n");
            //}
            // 设置　每天 00：00：00开始执行程序  



            //if (intHour == iHour && intMinute == iMinute && intSecond == iSecond)
            //{
            //    string resoultData = HttpUtil.HttpGet2("http://www.bk.caoam.cn/JCApiCore/GetJDKeyWordDetial");
            //    dynamic myData = JsonConvert.DeserializeObject<dynamic>(resoultData);
            //    if (myData.msg == "操作成功")
            //    {
            //        WriteLogs(0);
            //    }
            //    else
            //    {
            //        WriteLogs(1);
            //    }
            //}
        }
        public static void WriteLogs(int type, string error)
        {
            string path = AppDomain.CurrentDomain.BaseDirectory;
            if (!string.IsNullOrEmpty(path))
            {
                path = AppDomain.CurrentDomain.BaseDirectory + "ServiceLog";
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                path = path + "\\" + DateTime.Now.ToString("yyyyMMdd") + ".txt";
                string delPath = AppDomain.CurrentDomain.BaseDirectory + "ServiceLog" + "\\" + DateTime.Now.AddDays(-5).ToString("yyyyMMdd") + ".txt";//只保留五天的
                if (!File.Exists(path))
                {
                    FileStream fs = File.Create(path);
                    fs.Close();
                    if (File.Exists(delPath))
                    {
                        File.Delete(delPath);
                    }
                }
                if (File.Exists(path))
                {
                    StreamWriter sw = new StreamWriter(path, true, System.Text.Encoding.Default);
                    if (type == 0)
                    {
                        sw.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff") + " >>> 竞品" + error + ":执行成功");
                        sw.WriteLine("-----------------华丽的分割线-----------------------");
                        sw.Close();

                    }
                    else
                    {
                        sw.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff") + " >>> " + "执行失败:" + error);
                        sw.WriteLine("-----------------华丽的分割线-----------------------");
                        sw.Close();
                    }

                }
            }
        }
    }
}
