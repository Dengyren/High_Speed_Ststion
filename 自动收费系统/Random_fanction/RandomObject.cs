using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace 自动收费系统.Random_fanction
{
    public class RandomObject
    {
        char[] arrChar1 = new char[] { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' };
        char[] arrChar = new char[]{
           '0','1','2','3','4','5','6','7','8','9',
           'A','B','C','D','E','F','G','H','I','J','K','L','M','N','Q','P','R','T','S','V','U','W','X','Y','Z'
          };
        #region 数字随机数
        /// <param name="n">生成长度
        public string RandNum(int n)
        {

            StringBuilder num = new StringBuilder();

            for (int i = 0; i < n; i++)
            {
                Random rnd = new Random();
                generateMines(arrChar1.Length, arrChar1);
                num.Append(arrChar[rnd.Next(0, 9)].ToString());

            }

            return num.ToString();
        }
        #endregion

        #region 数字和字母随机数
        /// <param name="n">生成长度
        public string RandCode(int n)
        {


            StringBuilder num = new StringBuilder();


            for (int i = 0; i < n; i++)
            {
                Random rnd = new Random();
                generateMines(arrChar.Length, arrChar);
                num.Append(arrChar[rnd.Next(0, arrChar.Length)].ToString());

            }

            return num.ToString();
        }
        #endregion

        #region 字母随机数
        /// <param name="n">生成长度
        public string RandLetter(int n)
        {
            char[] arrChar = new char[]{
            'a','b','d','c','e','f','g','h','i','j','k','l','m','n','p','r','q','s','t','u','v','w','z','y','x',
            '_',
           'A','B','C','D','E','F','G','H','I','J','K','L','M','N','Q','P','R','T','S','V','U','W','X','Y','Z'
          };

            StringBuilder num = new StringBuilder();

            Random rnd = new Random(DateTime.Now.Millisecond);
            for (int i = 0; i < n; i++)
            {
                generateMines(arrChar.Length, arrChar);
                num.Append(arrChar[rnd.Next(0, arrChar.Length)].ToString());

            }

            return num.ToString();
        }
        #endregion

        private void swap(int x1, int x2, char[] m)//交换两个坐标
        {
            char t = m[x1];
            m[x1] = m[x2];
            m[x2] = t;
        }
        private void generateMines(int n, char[] m)//Fisher-Yates洗牌算法随机生成雷区
        {
            Random rd = new Random();
            for (int i = n - 1; i > 0; i--)
            {
                int index = (rd.Next(0, n));
                index = index % i;
                swap(i, index, m);
            }
        }
        
    }
}

