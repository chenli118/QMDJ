using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace QMDJ
{
    public static class DyadicArrayHelper
    {
        public static string[] NineGong = new string[] { "4", "9", "2", "3", "5", "7", "8", "1", "6" };
        public static string[,] InitNineGong(string[] srcNineGong,bool isPanSeq)
        {
            int x = 0;
            string[,] DyadicNineGong = new string[3, 3];
            string[] toNineGong = new string[9];
            if (isPanSeq)
            {
                toNineGong = srcNineGong;
            }
            else
            {
                toNineGong = new string[] { srcNineGong[3], srcNineGong[8], srcNineGong[1], srcNineGong[2], srcNineGong[4], srcNineGong[6], srcNineGong[7], srcNineGong[0], srcNineGong[5] };
            }
            string[] nineGong1 = toNineGong;
            for (int c = 0; c < 3; c++)
            {
                for (int r = 0; r < 3; r++)
                {
                    DyadicNineGong[r, c] = nineGong1[x];
                    x++;
                }
            }
            return DyadicNineGong;
        }

        public static string[,] DyadicNineGongToSideEigthGong(string[,] src, int step,bool order)
        {
            if (step == 0) return src;
            string[,] newNineGong = new string[3, 3];
            Dictionary<int, Dictionary<string, string>> dicMap = PanMapping(src);
            string[] toArr = new string[dicMap[step].Keys.Count];
            int x = 0; ;

            foreach (string s in dicMap[step].Keys)
            {
                if (order)
                {
                    toArr[x] = dicMap[step][s];
                }
                else
                {
                    toArr[x] = s;
                }
                x++;
            }

            int y = 0;
            for (int c = 0; c < 3; c++)
            {
                for (int r = 0; r < 3; r++)
                {
                    if (r == 1 && c == 1) newNineGong[1, 1] = src[1, 1];
                    else
                    {
                        newNineGong[r, c] = toArr[y];
                        y++;
                    }
                }
            }
           
            #region old
            /*
                    switch (step)
                    {
                        case 1:
                            newNineGong[0, 0] = dyadicNineGong[0, 1];
                            newNineGong[1, 0] = dyadicNineGong[0, 0];
                            newNineGong[2, 0] = dyadicNineGong[1, 0];
                            newNineGong[0, 1] = dyadicNineGong[0, 2];
                            newNineGong[2, 1] = dyadicNineGong[2, 0];
                            newNineGong[0, 2] = dyadicNineGong[1, 2];
                            newNineGong[1, 2] = dyadicNineGong[2, 2];
                            newNineGong[2, 2] = dyadicNineGong[2, 1];
                            break;
                        case 2:
                            newNineGong[0, 0] = dyadicNineGong[0, 2];
                            newNineGong[1, 0] = dyadicNineGong[0, 1];
                            newNineGong[2, 0] = dyadicNineGong[0, 0];
                            newNineGong[0, 1] = dyadicNineGong[1, 2];
                            newNineGong[2, 1] = dyadicNineGong[1, 0];
                            newNineGong[0, 2] = dyadicNineGong[2, 2];
                            newNineGong[1, 2] = dyadicNineGong[2, 1];
                            newNineGong[2, 2] = dyadicNineGong[2, 0];
                            break;
                        case 3:
                            newNineGong[0, 0] = dyadicNineGong[1, 2];
                            newNineGong[1, 0] = dyadicNineGong[0, 2];
                            newNineGong[2, 0] = dyadicNineGong[0, 1];
                            newNineGong[0, 1] = dyadicNineGong[2, 2];
                            newNineGong[2, 1] = dyadicNineGong[0, 0];
                            newNineGong[0, 2] = dyadicNineGong[2, 1];
                            newNineGong[1, 2] = dyadicNineGong[2, 0];
                            newNineGong[2, 2] = dyadicNineGong[1, 0];
                            break;
                        case 4:
                            newNineGong[0, 0] = dyadicNineGong[2, 2];
                            newNineGong[1, 0] = dyadicNineGong[1, 2];
                            newNineGong[2, 0] = dyadicNineGong[0, 2];
                            newNineGong[0, 1] = dyadicNineGong[2, 1];
                            newNineGong[2, 1] = dyadicNineGong[0, 1];
                            newNineGong[0, 2] = dyadicNineGong[2, 0];
                            newNineGong[1, 2] = dyadicNineGong[1, 0];
                            newNineGong[2, 2] = dyadicNineGong[0, 0];
                            break;
                        case 5:
                            newNineGong[0, 0] = dyadicNineGong[2, 1];
                            newNineGong[1, 0] = dyadicNineGong[2, 2];
                            newNineGong[2, 0] = dyadicNineGong[1, 2];
                            newNineGong[0, 1] = dyadicNineGong[2, 0];
                            newNineGong[2, 1] = dyadicNineGong[0, 2];
                            newNineGong[0, 2] = dyadicNineGong[1, 0];
                            newNineGong[1, 2] = dyadicNineGong[0, 0];
                            newNineGong[2, 2] = dyadicNineGong[0, 1];
                            break;
                        case 6:
                            newNineGong[0, 0] = dyadicNineGong[2, 0];
                            newNineGong[1, 0] = dyadicNineGong[2, 1];
                            newNineGong[2, 0] = dyadicNineGong[2, 2];
                            newNineGong[0, 1] = dyadicNineGong[1, 0];
                            newNineGong[2, 1] = dyadicNineGong[1, 2];
                            newNineGong[0, 2] = dyadicNineGong[0, 0];
                            newNineGong[1, 2] = dyadicNineGong[0, 1];
                            newNineGong[2, 2] = dyadicNineGong[0, 2];
                            break;
                        case 7:
                            newNineGong[0, 0] = dyadicNineGong[1, 0];
                            newNineGong[1, 0] = dyadicNineGong[2, 0];
                            newNineGong[2, 0] = dyadicNineGong[2, 1];
                            newNineGong[0, 1] = dyadicNineGong[0, 0];
                            newNineGong[2, 1] = dyadicNineGong[2, 2];
                            newNineGong[0, 2] = dyadicNineGong[0, 1];
                            newNineGong[1, 2] = dyadicNineGong[0, 2];
                            newNineGong[2, 2] = dyadicNineGong[1, 2];
                            break;
                    }
                     */
            #endregion
            return newNineGong;
        }
        //nineGong[1, 2] = "1";     22          21         20       10      00      01      02
        //nineGong[2, 0] = "2";     10          00         01       02      12      22      21
        //nineGong[0, 1] = "3";     02          12         22       21      20      10      00
        //nineGong[0, 0] = "4";  1  01     2    02     3   12   4   22  5   21  6   20  7   10
        //nineGong[1, 1] = "5";     11          11         11       11      11      11      11
        //nineGong[2, 1] = "7";     20          10         00       01      02      12      22
        //nineGong[2, 2] = "6";     21          20         10       00      01      02      12
        //nineGong[0, 2] = "8";     12          22         21       20      10      00      01
        //nineGong[1, 0] = "9";     00          01         02       12      22      21      20
        private static Dictionary<int, Dictionary<string, string>> PanMapping(string[,] src)
        {
            Dictionary<int, Dictionary<string, string>> dicMap = new Dictionary<int, Dictionary<string, string>>();
            Dictionary<string, string> toFrom;
            for (int i = 1; i < 8; i++)
            {
                toFrom = new Dictionary<string, string>();
                switch (i)
                {
                    case 1:
                        toFrom.Add(src[0, 0], src[0, 1]);
                        toFrom.Add(src[1, 0], src[0, 0]);
                        toFrom.Add(src[2, 0], src[1, 0]);
                        toFrom.Add(src[0, 1], src[0, 2]);
                        toFrom.Add(src[2, 1], src[2, 0]);
                        toFrom.Add(src[0, 2], src[1, 2]);
                        toFrom.Add(src[1, 2], src[2, 2]);
                        toFrom.Add(src[2, 2], src[2, 1]);
                        break;
                    case 2:
                        toFrom.Add(src[0, 0], src[0, 2]);
                        toFrom.Add(src[1, 0], src[0, 1]);
                        toFrom.Add(src[2, 0], src[0, 0]);
                        toFrom.Add(src[0, 1], src[1, 2]);
                        toFrom.Add(src[2, 1], src[1, 0]);
                        toFrom.Add(src[0, 2], src[2, 2]);
                        toFrom.Add(src[1, 2], src[2, 1]);
                        toFrom.Add(src[2, 2], src[2, 0]);
                        break;
                    case 3:
                        toFrom.Add(src[0, 0], src[1, 2]);
                        toFrom.Add(src[1, 0], src[0, 2]);
                        toFrom.Add(src[2, 0], src[0, 1]);
                        toFrom.Add(src[0, 1], src[2, 2]);
                        toFrom.Add(src[2, 1], src[0, 0]);
                        toFrom.Add(src[0, 2], src[2, 1]);
                        toFrom.Add(src[1, 2], src[2, 0]);
                        toFrom.Add(src[2, 2], src[1, 0]);
                        break;
                    case 4:
                        toFrom.Add(src[0, 0], src[2, 2]);
                        toFrom.Add(src[1, 0], src[1, 2]);
                        toFrom.Add(src[2, 0], src[0, 2]);
                        toFrom.Add(src[0, 1], src[2, 1]);
                        toFrom.Add(src[2, 1], src[0, 1]);
                        toFrom.Add(src[0, 2], src[2, 0]);
                        toFrom.Add(src[1, 2], src[1, 0]);
                        toFrom.Add(src[2, 2], src[0, 0]);
                        break;
                    case 5:
                        toFrom.Add(src[0, 0], src[2, 1]);
                        toFrom.Add(src[1, 0], src[2, 2]);
                        toFrom.Add(src[2, 0], src[1, 2]);
                        toFrom.Add(src[0, 1], src[2, 0]);
                        toFrom.Add(src[2, 1], src[0, 2]);
                        toFrom.Add(src[0, 2], src[1, 0]);
                        toFrom.Add(src[1, 2], src[0, 0]);
                        toFrom.Add(src[2, 2], src[0, 1]);
                        break;
                    case 6:
                        toFrom.Add(src[0, 0], src[2, 0]);
                        toFrom.Add(src[1, 0], src[2, 1]);
                        toFrom.Add(src[2, 0], src[2, 2]);
                        toFrom.Add(src[0, 1], src[1, 0]);
                        toFrom.Add(src[2, 1], src[1, 2]);
                        toFrom.Add(src[0, 2], src[0, 0]);
                        toFrom.Add(src[1, 2], src[0, 1]);
                        toFrom.Add(src[2, 2], src[0, 2]);
                        break;
                    case 7:
                        toFrom.Add(src[0, 0], src[1, 0]);
                        toFrom.Add(src[1, 0], src[2, 0]);
                        toFrom.Add(src[2, 0], src[2, 1]);
                        toFrom.Add(src[0, 1], src[0, 0]);
                        toFrom.Add(src[2, 1], src[2, 2]);
                        toFrom.Add(src[0, 2], src[0, 1]);
                        toFrom.Add(src[1, 2], src[0, 2]);
                        toFrom.Add(src[2, 2], src[1, 2]);
                        break;

                }
                dicMap.Add(i, toFrom);

            }
            return dicMap;
        }
        public static int GetOrderStep(string[,] src, string from, string to)
        {
            if (from == to) return 0;
            Dictionary<int, Dictionary<string, string>> dicMap = PanMapping(src);
            int step = -1;
            foreach (int i in dicMap.Keys)
            {
                foreach (string tt in dicMap[i].Keys)
                {
                    if (dicMap[i][tt] == from && tt == to)
                    {
                        step = i;
                        break;
                    }
                }
            }
            return step;
        }
        public static int GetArrayIndexByValue(string[] arr, string arrvalue)
        {
            for (int i = 0; i < arr.Length; i++)
            {
                if (arr[i] == arrvalue)
                {
                    return i;
                }
            }
            return -1;
        }
        public static DataTable ArrayToTable(string[,] arr)
        {
            DataTable dt = new DataTable();
            for (int c = 0; c < arr.GetLength(1); c++)
            {
                dt.Columns.Add("列" + c);
            }
            for (int c = 0; c < arr.GetLength(1); c++)
            {
                DataRow dr = dt.NewRow();
                for (int r = 0; r < arr.GetLength(0); r++)
                {
                    dr[r] = arr[r, c].ToString();
                }
                dt.Rows.Add(dr);
            }

            return dt;
        }
        
        /// <summary>
        /// 根据九宫的值取九宫位坐标
        /// </summary>
        /// <param name="ninegong"></param>
        /// <param name="gongvalue"></param>
        /// <returns></returns>
        internal static int[] GetPanSeq(string[,] ninegong, string gongvalue)
        {
            int[] cr= new int[2];
            for (int c = 0; c < ninegong.GetLength(1); c++)
            {
                for (int r = 0; r < ninegong.GetLength(0); r++)
                {
                    if (ninegong[r, c] == gongvalue)
                    {
                        cr[0] = r;
                        cr[1] = c;
                        return cr;
                    }
                }
            }
            return cr;
        }

        internal static int[] GetNineGongSeq(int[] fromSeq,int diffz, string from, string to, bool order)
        { 
            string[,] ng =  InitNineGong(NineGong, true);            
            string startIndex = ng[fromSeq[0], fromSeq[1]];
            if (order)
            {
                int endIndex = diffz + int.Parse(startIndex);
                if (endIndex > 9) endIndex = endIndex - 9;
                if (from != to && endIndex == 0) endIndex = 9;
                return GetPanSeq(ng, endIndex.ToString());
            }
            else
            {
                int endIndex = int.Parse(startIndex) - diffz;
                if (endIndex < 0 ) endIndex = 9 + endIndex;
                if (from != to && endIndex == 0) endIndex = 9;
                return GetPanSeq(ng, endIndex.ToString());
            }            
        }

        private static int GetDiffZ(string[] fromArr, string from, string to,bool order)
        {
            int srcfromIndex = GetArrayIndexByValue(fromArr, from);
            int srctoIndex = GetArrayIndexByValue(fromArr, to);
            if (srcfromIndex == srctoIndex) return 0;
            if (order)
            {
                if (srctoIndex > srcfromIndex) return srctoIndex - srcfromIndex;
                return 12 - srcfromIndex + srctoIndex;
            }
            else
            {
                if (srctoIndex <  srcfromIndex) return srcfromIndex - srctoIndex ;
                return 12 - srctoIndex + srcfromIndex;
            }
        }
    }
}
