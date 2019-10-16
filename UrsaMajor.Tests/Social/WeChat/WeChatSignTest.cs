using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using UrsaMajor.Social.WeChat;

namespace UrsaMajor.Tests.Social.WeChat
{
    [TestClass]
    public class WeChatSignTest
    {
        [TestMethod]
        public void BasicTest()
        {
            const string key = "0123456789ABCDEF";
            var objSign = new WeChatSign(key);
            objSign.add("mchid", "012345679");
            objSign.add("total_fee", "100");
            objSign.add("out_trade_no", "M2401C1234560258");
            objSign.add("type", null);
            objSign.add("body", "优惠码");

            string signed = objSign.calc();
            Assert.IsNotNull(signed);
        }

        [TestMethod]
        public async Task RemoteTest()
        {
            var dictArgs = new Dictionary<string, string>();
            dictArgs.Add("mchid", "1534793921");
            dictArgs.Add("total_fee", "1");
            dictArgs.Add("out_trade_no", "M1234C123456790");
            dictArgs.Add("type", String.Empty);
            dictArgs.Add("body", "优惠码");
            dictArgs.Add("attach", "MARK001");

            const string key = "8JGf15JuTWEyUa57";
            var objSign = new WeChatSign(key);
            foreach (var x in dictArgs) {
                objSign.add(x.Key, x.Value);
            }
            string sign = objSign.calc();
            dictArgs.Add("sign", sign);
            FormUrlEncodedContent body = new FormUrlEncodedContent(dictArgs);
            
            HttpClient httpClient = new HttpClient();
            var post = await httpClient.PostAsync("https://payjs.cn/api/native", body);
            var data = await post.Content.ReadAsStringAsync();

            Assert.IsNotNull(data);

        }
    }
}
