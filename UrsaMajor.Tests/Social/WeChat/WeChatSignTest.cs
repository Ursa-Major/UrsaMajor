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
            objSign.add("attach", String.Empty);

            string signed = objSign.calc();
            string expected = "3FC302E58E6A1DEE8D2505BCE325D20B";
            Assert.AreEqual(expected, signed);
        }
    }
}
