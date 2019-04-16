#region << 版 本 注 释 >>
/*----------------------------------------------------------------
* 项目名称 ：WS.Text
* 项目描述 ：
* 类 名 称 ：StringExtension
* 类 描 述 ：
* 所在的域 ：DESKTOP-KA4M82K
* 命名空间 ：WS.Text
* 机器名称 ：DESKTOP-KA4M82K 
* CLR 版本 ：4.0.30319.42000
* 作    者 ：wagsn
* 创建时间 ：2018/11/27 19:36:44
* 更新时间 ：2018/11/27 19:36:44
* 版 本 号 ：v1.0.0.0
//----------------------------------------------------------------*/
#endregion

using System.Linq;

namespace WS.Text
{
    /// <summary>
    /// 原生String扩展
    /// </summary>
    public static class StringExtension
    {
        /// <summary>
        /// 去除首尾空格，再合并连续空格
        /// </summary>
        /// <param name="src">原始字符串</param>
        /// <returns></returns>
        public static string NormalSpace(this string src)
        {
            return System.Text.RegularExpressions.Regex.Replace(src.Trim(), "\\s{2,}", " ");
        }

        /// <summary>
        /// 获取两个字符串的相似度
        /// 原理：本次所用到的相似度计算公式是 相似度=Kq*q/(Kq*q+Kr*r+Ks*s) (Kq > 0 , Kr>=0,Ka>=0) 其中，q是字符串1和字符串2中都存在的单词的总数，s是字符串1中存在，字符串2中不存在的单词总数，r是字符串2中存在，字符串1中不存在的单词总数.Kq,Kr和ka分别是q,r,s的权重，根据实际的计算情况，我们设Kq=2，Kr=Ks=1.
        /// 来源：http://www.cnblogs.com/lcq529/archive/2018/03/21/8618287.html
        /// </summary>
        /// <param name=”sourceString”>第一个字符串</param>
        /// <param name=”str”>第二个字符串</param>
        /// <returns>相似度为小数</returns>
        public static decimal GetSimilarityWith(this string sourceString, string str)
        {
            decimal Kq = 2;
            decimal Kr = 1;
            decimal Ks = 1;

            char[] ss = sourceString.ToCharArray();
            char[] st = str.ToCharArray();

            //获取交集数量
            int q = ss.Intersect(st).Count();
            int s = ss.Length - q;
            int r = st.Length - q;

            return Kq * q / (Kq * q + Kr * r + Ks * s);
        }

        ///// <summary>
        ///// 使文件路径标准化（首先剔除掉文件路径不能包含的特殊字符）
        ///// </summary>
        ///// <param name="path"></param>
        ///// <returns></returns>
        //public static string NormalPath(this string path)
        //{
        //    return string.Empty;
        //}

        ///// <summary>
        ///// 是否是路径，TODO
        ///// </summary>
        ///// <param name="src"></param>
        ///// <returns></returns>
        //public static bool IsPath(this string src)
        //{
        //    return false;
        //}
    }
}
