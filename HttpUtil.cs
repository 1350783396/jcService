using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace JcService
{
    public class HttpUtil
    {
        public static string HttpGet(string url)
        {
            try
            {
                var myRequest = (HttpWebRequest)WebRequest.Create(url);
                myRequest.UserAgent = "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.1; SV1; .NET CLR 1.1.4322)";
                myRequest.Method = "GET";
                using (WebResponse wr = myRequest.GetResponse())
                {

                    string content = new StreamReader(wr.GetResponseStream(), Encoding.UTF8).ReadToEnd();

                    return content;
                }
            }
            catch (Exception EX)
            {
                throw new Exception(EX.Message);
            }
        }
        public static string HttpGet2(string url)
        {
            try
            {
                var myRequest = (HttpWebRequest)WebRequest.Create(url);
                myRequest.UserAgent = "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.1; SV1; .NET CLR 1.1.4322)";
                myRequest.Method = "GET";
                myRequest.Timeout = 60000;
                myRequest.Headers.Add("cookie", "t=2f7c01b5d5395063931ec64ce22f72dd; thw=cn; enc=uWanIMlZlDlI46k14%2B0DKJQA1mtwWtn03J1Kf2C1R%2FVBOgtqbo8YyVQlxTTr0MuVMWdlXqu31Ek6qeUGEhSSlg%3D%3D; hng=CN%7Czh-CN%7CCNY%7C156; tk_trace=oTRxOWSBNwn9dPyorMJE%2FoPdY8zfvmw%2Fq5horoBWw%2BmTqaVO5o9kjLSekto5Br5dYkRIcOIsxmMRNynsVJFf%2B3m9xrdWtOAuRhInpUhePpTKhQnezvUEGdNa5g5G9PzSQwsxfsrdbCFKkDx%2BiMrADFf8m3xL4u4d5SLFQU6onc4zcxH2QJPjPJr50hhFlznKZyvtOP%2F%2BdlVV7sHbh%2B2gkc7Y8GhP4GEDuyJbaZMe3jVo2Kh2BXQvCLPPpJDfmRj%2Bd97rNChGVas%2FauvjSnBLpilzZ8eu; lLtC1_=1; cookie2=116dd0330edfb6fb1827dcfaa46226f5; _tb_token_=53e54d65ee9ae; _samesite_flag_=true; cna=B7g7F2OKxGECAXozjj/Ca2Ez; unb=2201271445225; lgc=tb269821864; cookie17=UUphy%2FA%2B%2BxGbefhdqA%3D%3D; dnk=tb269821864; tracknick=tb269821864; _l_g_=Ug%3D%3D; sg=451; _nk_=tb269821864; cookie1=Uojci8VbkHqLnX%2FmZcRcQjB8INhHtgTV7W6LCs6Fulc%3D; mt=ci=34_1; sgcookie=E2%2Bs5xnPhO4pL37U4LcQ4; uc1=existShop=false&cookie16=WqG3DMC9UpAPBHGz5QBErFxlCA%3D%3D&cookie14=UoTV6ysMiKKBHw%3D%3D&cookie21=VT5L2FSpdA%3D%3D&cookie15=URm48syIIVrSKA%3D%3D&pas=0; uc3=vt3=F8dBxG2oaGtj0QL6SKs%3D&id2=UUphy%2FA%2B%2BxGbefhdqA%3D%3D&lg2=W5iHLLyFOGW7aA%3D%3D&nk2=F5RHpx25WfNJJUc%3D; csg=4d91ae72; skt=4eb28dfb5a725a13; existShop=MTU5NzIxODg0NQ%3D%3D; uc4=id4=0%40U2grEJfL55F%2FpjxChIRE9RkjFJYrbcm7&nk4=0%40FY4MtaLARrGSWw1QuLMhglglcoZMWw%3D%3D; _cc_=URm48syIZQ%3D%3D; v=0; _m_h5_tk=782f34f25b7d20fae06e3f86ea2e756b_1597226047470; _m_h5_tk_enc=48604ead0ea3457d46f86f81eda83788; isg=BICAeQbzy3AW_bd0Cgzev5_ZUQ5SCWTTsGET0voRQxsudSCfohufYi1Hjd21RRyr; l=eBM0hE6qO_f6ZRHbBOfanurza77OSIRYYuPzaNbMiOCP_r5B5xx5WZucnMY6C3GVh6beR3RgZBHyBeYBqQAonxvtIosM_Ckmn; tfstk=czECBJb0ZBACipbyYJ6ZTNjXZQiRwcJjFwG3dPl5vH8njj1mDnlXXcakwh3xC");
                using (WebResponse wr = myRequest.GetResponse())
                {

                    string content = new StreamReader(wr.GetResponseStream(), Encoding.UTF8).ReadToEnd();

                    return content;
                }
            }
            catch (WebException ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public static String PostDataSim(string url, string data, CookieContainer cookie)
        {
            HttpWebResponse response = null;
            try
            {
                // throw new Exception(data);
                byte[] postdata = Encoding.UTF8.GetBytes(data);

                var myRequest = (HttpWebRequest)WebRequest.Create(url);
                myRequest.Method = "POST";
                myRequest.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/69.0.3497.92 Safari/537.36";
                myRequest.ContentType = "application/x-www-form-urlencoded; charset=UTF-8";
                myRequest.CookieContainer = cookie;
                myRequest.Headers.Add("x-requested-with", "XMLHttpRequest");
                myRequest.Referer = "https://subway.simba.taobao.com/";
                myRequest.Timeout = 60000;
                Stream newStream = myRequest.GetRequestStream();

                newStream.Write(postdata, 0, postdata.Length);
                newStream.Close();
                // Get response
                response = (HttpWebResponse)myRequest.GetResponse();


                using (StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.UTF8))
                {
                    return reader.ReadToEnd();
                }
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (response != null)
                    response.Close();
            }


        }
        public static String PostData(string url, string data)
        {
            HttpWebResponse response = null;
            try
            {
                // throw new Exception(data);
                byte[] postdata = Encoding.UTF8.GetBytes(data);

                var myRequest = (HttpWebRequest)WebRequest.Create(url);
                myRequest.Method = "POST";
                myRequest.ContentType = "application/json";
                myRequest.Timeout = 60000;
                Stream newStream = myRequest.GetRequestStream();

                newStream.Write(postdata, 0, postdata.Length);
                newStream.Close();
                // Get response
                response = (HttpWebResponse)myRequest.GetResponse();


                using (StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.UTF8))
                {
                    return reader.ReadToEnd();
                }
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (response != null)
                    response.Close();
            }


        }
    }
}
