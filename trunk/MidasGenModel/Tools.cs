using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Collections;

namespace MidasGenModel.Tools
{
    /// <summary>
    /// 选择集
    /// </summary>
    public class SelectCollection
    {
        private SortedList<int, string> _Nodes;
        private SortedList<int, string> _Elements;

        #region 属性
        /// <summary>
        /// 节点集合
        /// </summary>
        public List<int> Nodes
        {
            get
            {
                List<int> res = new List<int>();
                foreach (int temp in _Nodes.Keys)
                {
                    res.Add(temp);
                }
                return res;
            }
        }
        /// <summary>
        /// 单元集合
        /// </summary>
        public List<int> Elements
        {
            get
            {
                List<int> res = new List<int>();
                foreach (int temp in _Elements.Keys)
                {
                    res.Add(temp);
                }
                return res;
            }
        }

        #endregion

        #region 构造函数
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="S_node">节点集合，带to</param>
        /// <param name="S_elem">单元集合字符串，带to</param>
        public SelectCollection(string S_node, string S_elem)
        {
            _Nodes = new SortedList<int, string>();
            _Elements = new SortedList<int, string>();

            List<int> ns = StringToList(S_node);
            List<int> es = StringToList(S_elem);

            foreach (int temp in ns)
            {
                _Nodes.Add(temp, temp.ToString());
            }

            foreach (int temp in es)
            {
                _Elements.Add(temp, temp.ToString());
            }
        }
        #endregion
        #region 方法
        /// <summary>
        /// 将带to的字符串转成整数集
        /// </summary>
        /// <param name="ss">简化字符串</param>
        /// <returns>整数集</returns>
        static public List<int> StringToList(string ss)
        {
            List<int> res = new List<int>();

            if (ss == null || ss.Trim().Length == 0)
            {
                return res;
            }

            string[] temp = ss.Trim().Split(' ');
            foreach (string str in temp)
            {
                if (str.Contains("to") == false)
                {
                    res.Add(Convert.ToInt32(str));
                }
                else
                {
                    string[] tt = str.Split(new string []{"to","by"},StringSplitOptions.RemoveEmptyEntries);

                    string from = tt[0];
                    string to = tt[1];
                    string by = "1";
                    if (tt.Length > 2)
                    {
                        by = tt[2];
                    }

                    int from_num = Convert.ToInt32(from);
                    int to_num = Convert.ToInt32(to);
                    int by_num = Convert.ToInt32(by);

                    for (int i = from_num; i <= to_num; i+=by_num)
                    {
                        res.Add(i);
                    }
                }
            }

            return res;//返回结果
        }
        /// <summary>
        /// 将整数集转成字符串
        /// 未实现
        /// </summary>
        /// <param name="ss">整数集</param>
        /// <returns>简化字符串</returns>
        static public string ListToString(List<int> ss)
        {
            string res=null;

            return res;
        }
        #endregion
    }
}