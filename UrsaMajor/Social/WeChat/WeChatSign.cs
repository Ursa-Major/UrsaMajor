using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace UrsaMajor.Social.WeChat
{
    /// <summary>
    /// 微信参数签名算法
    /// </summary>
    public class WeChatSign
    {
        /// <summary>
        /// 密钥
        /// </summary>
        private string _key;

        /// <summary>
        /// 待参加签名的参数
        /// </summary>
        private SortedDictionary<string, string> _dictArgs = new SortedDictionary<string, string>();

        /// <summary>
        /// 构造微信签名算法对象
        /// </summary>
        /// <param name="key">密钥</param>
        public WeChatSign(string key) {
            _key = key;
        }

        public void clear()
        {
            _dictArgs.Clear();
        }

        /// <summary>
        /// 追加参数
        /// </summary>
        /// <param name="key">参数键名</param>
        /// <param name="value">参数键值</param>
        public void add(string key, string value)
        {
            if(!String.IsNullOrWhiteSpace(value))
                _dictArgs.Add(key, value);
        }

        /// <summary>
        /// 计算签名
        /// </summary>
        /// <returns></returns>
        public string calc()
        {
            // 对参数按照key=value的格式，并按照参数名ASCII字典序排序
            StringBuilder sbBuf = new StringBuilder();
            foreach (var k in _dictArgs.Keys) {
                string v = _dictArgs[k];
                sbBuf.Append($"{k}={v}&");
            }

            // 拼接密钥
            sbBuf.Append($"key={_key}");

            // md5
            var buf = Encoding.UTF8.GetBytes(sbBuf.ToString());
            var md5 = MD5.Create().ComputeHash(buf);
            sbBuf.Clear();
            foreach (var b in md5) {
                sbBuf.AppendFormat("{0:X2}", b);
            }

            return sbBuf.ToString();
        }
    }
}
