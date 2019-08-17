using System;
using System.Collections.Generic;
using System.Text;

namespace QMDJ
{
    static class JieQiHelper
    {
        public static List<string> GetTwentyFourJieQi()
        {
            List<string> jieQI = new List<string>();
            jieQI.Add("冬至");
            jieQI.Add("小寒");
            jieQI.Add("大寒");
            //2
            jieQI.Add("立秋");
            jieQI.Add("处暑");
            jieQI.Add("白露");
            //3
            jieQI.Add("春分");
            jieQI.Add("清明");
            jieQI.Add("谷雨");
            //4
            jieQI.Add("立夏");
            jieQI.Add("小满");
            jieQI.Add("芒种");
            //6
            jieQI.Add("立冬");
            jieQI.Add("小雪");
            jieQI.Add("大雪");
            //7
            jieQI.Add("秋分");
            jieQI.Add("寒露");
            jieQI.Add("霜降");
            //8
            jieQI.Add("立春");
            jieQI.Add("雨水");
            jieQI.Add("惊蛰");
            //9
            jieQI.Add("夏至");
            jieQI.Add("小暑");
            jieQI.Add("大暑");


            return jieQI;
        }
        public static Dictionary<string, int[]> GetJuDic()
        {
            Dictionary<string, int[]> ret = new Dictionary<string, int[]>();
            ret.Add("冬至",new int[] { 1, 7, 4 });
            ret.Add("小寒",new int[] { 1+1, 8, 6-1 });
            ret.Add("大寒", new int[] { 3, 9, 6 });
            ret.Add("立秋", new int[] { 1+1, 6-1, 8 });
            ret.Add("处暑", new int[] { 1, 3+1, 7 });
            ret.Add("白露", new int[] { 9, 3, 6 });
            ret.Add("春分", new int[] { 3,9, 6 });
            ret.Add("清明", new int[] { 1+3, 1, 7 });
            ret.Add("谷雨", new int[] { 6-1, 1+1, 8 });
            ret.Add("立夏", new int[] { 1+3, 1, 7 });
            ret.Add("小满", new int[] { 6-1, 1+1, 8 });
            ret.Add("芒种", new int[] { 6, 3, 9 });
            ret.Add("立冬", new int[] { 6, 9, 3 });
            ret.Add("小雪", new int[] { 6-1, 8, 1+1 });
            ret.Add("大雪", new int[] { 1+3, 7, 1 });
            ret.Add("秋分", new int[] { 7, 1, 1+3 });
            ret.Add("寒露", new int[] { 6, 9, 3 });
            ret.Add("霜降", new int[] { 6-1, 8, 1+1 });
            ret.Add("立春", new int[] { 8, 6-1, 1+1 });
            ret.Add("雨水", new int[] { 9, 6, 3 });
            ret.Add("惊蛰", new int[] { 1, 7, 3+1 });
            ret.Add("夏至", new int[] { 9, 3, 6 });
            ret.Add("小暑", new int[] { 8, 1+1, 6-1 });
            ret.Add("大暑", new int[] { 7, 1, 3-1 });
            return ret;
        }
        public static List<string> Days(bool isBigMonth)
        {
            List<string> days = new List<string>();
            days.Add("初一");
            days.Add("初二");
            days.Add("初三");
            days.Add("初四");
            days.Add("初五");
            days.Add("初六");
            days.Add("初七");
            days.Add("初八");
            days.Add("初九");
            days.Add("初十");
            days.Add("十一");
            days.Add("十二");
            days.Add("十三");
            days.Add("十四");
            days.Add("十五");
            days.Add("十六");
            days.Add("十七");
            days.Add("十八");
            days.Add("十九");
            days.Add("二十");
            days.Add("廿一");
            days.Add("廿二");
            days.Add("廿三");
            days.Add("廿四");
            days.Add("廿五");
            days.Add("廿六");
            days.Add("廿七");
            days.Add("廿八");
            days.Add("廿九");
            if (isBigMonth)
            {
                days.Add("三十");         
            }
            return days;
        }

        public static int GetJuByJieQi(string jieQi, int szxYuan)
        {
            int ju = -1;
            bool p = false;
            Dictionary<int, string[]> dicJie = FillJieQiToGua();
            foreach (int k  in dicJie.Keys)
            {
                string[] ss = dicJie[k];               
                
                for (int t = 0; t < ss.Length; t++)
                {
                    if (ss[t] == jieQi)
                        {
                        #region
                        /*
                        if (k > 0) yy = true;
                        ju = Math.Abs(ju);
                        if (szxYuan == 1)//上元
                        {
                            if (k > 0)
                            {
                                ju = k + t;
                                if (ju > 9)
                                    ju = ju - 9;
                            }
                            else
                            {
                                ju = k - t;
                                if (ju < 1) 
                                    ju = 9 - ju;
                            }
                        }
                        if (szxYuan == 2)//中元
                        {
                            if (k > 0)
                            {
                                ju = k + t;
                                if (ju > 9)
                                    ju = ju - 9;
                                ju = ju + 6;
                                if (ju > 9)
                                    ju = ju - 9;
                            }
                            else
                            {
                                if(t==0)

                                ju = k - t;
                                if (ju < 1) 
                                    ju = 9 - ju;
                                ju = ju - 6;
                                if (ju < 1)
                                    ju = 9 - ju;
                            }

                        }
                        if (szxYuan == 3)//下元
                        {
                            if (k > 0)
                            {
                                ju = k + t;
                                if (ju > 9)
                                    ju = ju - 9;
                                ju = ju + 6;
                                if (ju > 9)
                                    ju = ju - 9;
                                ju = ju + 6;
                                if (ju > 9)
                                    ju = ju - 9;
                            }
                            else
                            {
                                ju = k - t;
                                if (ju < 1)
                                    ju = 9 - ju;
                                ju = ju - 6;
                                if (ju < 1)
                                    ju = 9 - ju;
                                ju = ju - 6;
                                if (ju < 1)
                                    ju = 9 - ju;
                            }
                           
                        }*/
                        #endregion
                        if (k > 0) ju = 1;
                        ju= GetJuDic()[jieQi][szxYuan - 1] * ju;
                        p = true;
                        break;
                       
                    }                    
                }
                if (p) break;                

            }
           // if (!yy) ju = -ju;           
            return ju;
        }
        private static Dictionary<int, string[]> FillJieQiToGua()
        {
            int x = 1;//九宫数
            int y = 0;//一卦内三节气索引
            int z = 1;//取反
            string[] guaS = new string[3];
            string[] jqs = GetTwentyFourJieQi().ToArray();
            Dictionary<int, string[]> dicJq = new Dictionary<int, string[]>();
            for (int i = 0; i < jqs.Length; i++)
            {
                if (i % 3 == 0 && i > 0)
                {
                    guaS = new string[3];
                    y = 0;
                    x++;
                    if (x % 5 == 0)
                        x = x + 1;//八卦数没有中宫数

                }
                guaS[y] = jqs[i];
                y++;
                if (x == 2 || x == 6 || x == 7 || x == 9) z = -1 * x; else z = Math.Abs(x); //阳顺阴逆
                if (dicJq.ContainsKey(z))
                {
                    dicJq[z] = guaS;
                }
                else
                {
                    dicJq.Add(z, guaS);
                }
            }
            return dicJq;
        }

        internal static string GetInJieQis()
        {
            List<string> jie = GetTwentyFourJieQi();
            StringBuilder sb = new StringBuilder();
            foreach (string s in jie)
            {
                sb.Append("'" + s + "',");
            
            }
            return sb.ToString().Substring(0,sb.Length-1);
        }
    }
}
