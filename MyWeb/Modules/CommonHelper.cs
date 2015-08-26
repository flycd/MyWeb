using Model;
using Nancy.Session;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyWeb.Modules
{
    public class CommonHelper
    {
        #region 
        /// <summary>    
        ///    
        /// </summary>    
        /// <returns>P</returns>    
        public static string GetIPAddress(string ip)
        {
            IPScanerHelper objScan = new IPScanerHelper();
            objScan.DataPath = "App_Data/qqwry.dat";
              objScan.IP = ip;
              string OWNER_address = objScan.IPLocation();
              return OWNER_address;
        }
        #endregion

        #region 过滤掉 html代码
        public static string StripHTML(string strHtml)
        {
            string[] aryReg ={ 
            @"<script[^>]*?>.*?</script>", 

            @"<(\/\s*)?!?((\w+:)?\w+)(\w+(\s*=?\s*(([""'])(\\[""'tbnr]|[^\7])*?\7|\w+)|.{0})|\s)*?(\/\s*)?>", 
            @"([\r\n])[\s]+", 
            @"&(quot|#34);", 
            @"&(amp|#38);", 
            @"&(lt|#60);", 
            @"&(gt|#62);", 
            @"&(nbsp|#160);", 
            @"&(iexcl|#161);", 
            @"&(cent|#162);", 
            @"&(pound|#163);", 
            @"&(copy|#169);", 
            @"&#(\d+);", 
            @"-->", 
            @"<!--.*\n" 
            };

            string[] aryRep = { 
            "", 
            "", 
            "", 
            "\"", 
            "&", 
            "<", 
            ">", 
            " ", 
            "\xa1",//chr(161), 
            "\xa2",//chr(162), 
            "\xa3",//chr(163), 
            "\xa9",//chr(169), 
            "", 
            "\r\n", 
            "" 
            };

            string newReg = aryReg[0];
            string strOutput = strHtml;
            for (int i = 0; i < aryReg.Length; i++)
            {
                System.Text.RegularExpressions.Regex regex = new System.Text.RegularExpressions.Regex(aryReg[i], System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                strOutput = regex.Replace(strOutput, aryRep[i]);
            }
            strOutput.Replace("<", "");
            strOutput.Replace(">", "");
            strOutput.Replace("\r\n", "");
            return strOutput;
        }
        #endregion 
    }
}