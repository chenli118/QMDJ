using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.SQLite;

namespace QMDJ
{
    public partial class Form1 : Form
    {
        string yearGZ = string.Empty;
        string monthGZ = string.Empty;
        string dayGZ = string.Empty;
        string hourGZ = string.Empty;
        string jieQi = string.Empty;
        string JU = string.Empty;
        string zhimen = string.Empty;
        string zhifu = string.Empty;
        string[] gz60 = new string[60];               
        string[] doorArr = new string[] { "杜", "景", "死", "伤", "", "惊", "生", "休", "开" };
        string[] godArr = new string[]  { "合", "虎", "武", "阴", "", "地", "蛇", "符", "天" };
        string[] xgodArr = new string[] { "武", "虎", "合", "地", "", "阴", "天", "符", "蛇" };
        string[] guaArr = new string[]  { "巽", "离", "坤", "震", "", "酉", "艮", "坎", "乾" };
        string[] yiqiArr = new string[] { "戊", "己", "庚", "辛", "壬", "癸", "丁", "丙", "乙" };
        string[] starArr = new string[] { "辅", "英", "芮", "冲", "禽", "柱", "任", "蓬", "心" };
        string[] ganArr = new string[]  { "甲", "乙", "丙", "丁", "戊", "己", "庚", "辛", "壬", "癸" };
        string[] zhiArr = new string[]  { "子", "丑", "寅", "卯", "辰", "巳", "午", "未", "申", "酉", "戌", "亥" };
        public Form1()
        {
            InitializeComponent();            
            this.ClientSize = new Size (639,347);
            this.FormBorderStyle = FormBorderStyle.None;//双击Label才能关闭界面
            this.TopMost = true;
            Start();
        }
 
        private void Start()
        {
            //1，准备数据
            InitStaticData();
            GetAllGZ();
            //2,根据日干和节气求局数
            int ju = GetJu(dayGZ);          
            //3,根据局数飞布定地盘;
            string[] dipan = OrderDIPan(ju);           
            string[,] dealDiPan = DyadicArrayHelper.InitNineGong(dipan, false);             
            Dictionary<int, string> dicDP = GetDPDic(dealDiPan);
            //4,根据值使和地盘求天盘;
            hourGZ = GetShiGZ(dayGZ);//根据日求时
            string shiG = hourGZ.Substring(0, 1);           
            zhifu  = GetZF(dicDP,shiG);
            string zhiyi = GetDiPan6Yi(hourGZ);
            string zhishi = zhiyi;// GetZHISHI(GetFuTou(hourGZ, true));//根据时求符首 
            if (zhishi == dealDiPan[1, 1]) zhishi = dealDiPan[2, 0];//特例中宫落二宫
            if (shiG == dealDiPan[1, 1]) shiG = dealDiPan[2, 0];//特例中宫落二宫
            int step = DyadicArrayHelper.GetOrderStep(dealDiPan, zhishi, shiG);//偏移量
            if (shiG == ganArr[0]) step = 0;//元帅特例
            string[,] tianPan = OrderTianPan(dealDiPan, step);
            //5,九星盘
            string[,] starPan = OrderStarPan(dealDiPan, zhishi, shiG);
            //6,八神盘
            bool order = ju > 0;
            string[,] godPan = OrderGodPan(dealDiPan, zhishi, shiG, order);
            //7,八门盘
            string shiZ = hourGZ.Substring(1, 1);
            string xunZ = GetFuTou(hourGZ, true);
            string[,] doorPan = OrderDoorPan(dealDiPan, zhishi, xunZ, shiG, shiZ, order);

            //以下调试===========================================================================================================
            if (order) JU = "阳遁： " + ju + "局"; else JU = "阴遁： " + ju + "局";
            if (zhimen.Trim().Length == 0) zhimen = "死五"; 
            string msg = "时间： " + yearGZ + monthGZ +  dayGZ + "日" + hourGZ + "时" 
                + System.Environment.NewLine + "节气： " + jieQi 
                + System.Environment.NewLine + JU  
                + System.Environment.NewLine + "旬首： 甲" + xunZ  + ( zhiyi!=zhishi? zhiyi+"转 " + zhishi: zhiyi)
                + System.Environment.NewLine + "直使： " + zhimen+"门" 
                + System.Environment.NewLine + "直符： 落" + zhifu +shiG+"宫";
            //MessageBox.Show(msg);
            label1.Text = msg;

            DataTable dt = DyadicArrayHelper.ArrayToTable(dealDiPan);            
            subGrid1.Location = new System.Drawing.Point(13, 27);
            subGrid1.Size = new System.Drawing.Size(303, 72);           
            subGrid1.DataSource = dt;
            int[] focusCell = DyadicArrayHelper.GetPanSeq(dealDiPan, zhiyi);
            subGrid1.focusColmunIndex = focusCell[0];
            subGrid1.focusRowIndex = focusCell[1]; 

            DataTable dt4 = DyadicArrayHelper.ArrayToTable(godPan);
            subGrid4.Location = new System.Drawing.Point(13, 101);
            subGrid4.Size = new System.Drawing.Size(303, 72);
            subGrid4.DataSource = dt4;
            focusCell = DyadicArrayHelper.GetPanSeq(godPan, godArr[7]);
            subGrid4.focusColmunIndex = focusCell[0];
            subGrid4.focusRowIndex = focusCell[1];

            DataTable dt6 = DyadicArrayHelper.ArrayToTable(DyadicArrayHelper.InitNineGong(guaArr, true));
            subGrid6.Location = new System.Drawing.Point(13, 175);
            subGrid6.Size = new System.Drawing.Size(303, 72);
            subGrid6.DataSource = dt6;
            focusCell = DyadicArrayHelper.GetPanSeq(dealDiPan, yiqiArr[0]);
            subGrid6.focusColmunIndex = focusCell[0];
            subGrid6.focusRowIndex = focusCell[1];

            DataTable dt2 = DyadicArrayHelper.ArrayToTable(tianPan);
            subGrid2.Location = new System.Drawing.Point(326, 27);
            subGrid2.Size = new System.Drawing.Size(303, 72);
            subGrid2.DataSource = dt2;
            focusCell = DyadicArrayHelper.GetPanSeq(tianPan, shiG);
            subGrid2.focusColmunIndex = focusCell[0];
            subGrid2.focusRowIndex = focusCell[1];

            DataTable dt3 = DyadicArrayHelper.ArrayToTable(starPan);
            subGrid3.Location = new System.Drawing.Point(326, 101);
            subGrid3.Size = new System.Drawing.Size(303, 72);
            subGrid3.DataSource = dt3;
            focusCell = DyadicArrayHelper.GetPanSeq(starPan, shiG);
            subGrid3.focusColmunIndex = focusCell[0];
            subGrid3.focusRowIndex = focusCell[1];

            DataTable dt5 = DyadicArrayHelper.ArrayToTable(doorPan);
            subGrid5.Location = new System.Drawing.Point(326, 175);
            subGrid5.Size = new System.Drawing.Size(303, 72);
            subGrid5.DataSource = dt5;
            focusCell = DyadicArrayHelper.GetPanSeq(doorPan, zhimen);
            subGrid5.focusColmunIndex = focusCell[0];
            subGrid5.focusRowIndex = focusCell[1];

        }

        private void InitStaticData()
        {
            string[] g60 = new string[60];
            string[] z60 = new string[60];
            ganArr.CopyTo(g60, 0);
            ganArr.CopyTo(g60, 10);
            ganArr.CopyTo(g60, 20);
            ganArr.CopyTo(g60, 30);
            ganArr.CopyTo(g60, 40);
            ganArr.CopyTo(g60, 50);
            zhiArr.CopyTo(z60, 0);
            zhiArr.CopyTo(z60, 12);
            zhiArr.CopyTo(z60, 24);
            zhiArr.CopyTo(z60, 36);
            zhiArr.CopyTo(z60, 48);
            for (int i = 0; i < 60; i++)
            {
                gz60[i] = g60[i] + z60[i];
            }
        }

        private string GetZF(Dictionary<int, string> dicDP, string shiG)
        {
            foreach (var v in dicDP.Keys)
            {
                if (dicDP[v] == shiG) return v.ToString();
            }
            return string.Empty;
        }

        private Dictionary<int, string> GetDPDic(string[,] pan)
        {
            Dictionary<int, string> keyValues = new Dictionary<int, string>();
            keyValues.Add(1, pan[1, 2]);
            keyValues.Add(2, pan[2, 0]);
            keyValues.Add(3, pan[0, 1]);
            keyValues.Add(4, pan[0, 0]);
            keyValues.Add(5, pan[1, 1]);
            keyValues.Add(6, pan[2, 2]);
            keyValues.Add(7, pan[2, 1]);
            keyValues.Add(8, pan[0, 2]);
            keyValues.Add(9, pan[1, 0]);
            return keyValues;
        }
        private int Diffz(string startz,string nearz)
        {
            int d = 0;
            for (int i = 0; i < gz60.Length; i++)
            {
                if (gz60[i].EndsWith(startz))
                    d++;
                else if (d > 0) { d++;}
                if ( gz60[i].EndsWith(nearz)) break;                
            }
            return d-1;
        }
        private string[,] OrderDoorPan(string[,] dipan, string zhishi, string xunz, string hg, string hz, bool order)
        {
            zhishi = GetZHISHI(GetFuTou(hourGZ, true));
            string[,] panSeqSrc = DyadicArrayHelper.InitNineGong(doorArr, true);
            int[] fromSeq = DyadicArrayHelper.GetPanSeq(dipan, zhishi);//值坐标
            
            zhimen = panSeqSrc[fromSeq[0],fromSeq[1]];
            //if (zhimen == "") //落中宫,寄坤宫 记五数
            //{
            //    fromSeq[0] = 2;
            //    fromSeq[1] = 0;
            //}
            int[] toSeq = DyadicArrayHelper.GetNineGongSeq(fromSeq,Diffz(xunz,hz) , xunz, hz, order);//旬支飞时支的坐标          
            string from = panSeqSrc[fromSeq[0], fromSeq[1]];
            if (from.Trim() == string.Empty) from = panSeqSrc[2, 0];
            string to = panSeqSrc[toSeq[0], toSeq[1]];
            if (to.Trim() == string.Empty) to = panSeqSrc[2, 0];
            int step = 0;
            if (from == to)
            {
                return panSeqSrc;
            }
            else
            {
                step = DyadicArrayHelper.GetOrderStep(panSeqSrc, from, to);
            }
            string[,] newPan = DyadicArrayHelper.DyadicNineGongToSideEigthGong(panSeqSrc, step, true);
            return newPan;
        }
        private string[,] OrderGodPan(string[,] dipan, string zhishi, string hg, bool order)
        {
            string[,] panSeqSrc = new string[3, 3];
            if (!order)
            {                 
                panSeqSrc = DyadicArrayHelper.InitNineGong(xgodArr, true);
            }
            else
            {
                panSeqSrc = DyadicArrayHelper.InitNineGong(godArr, true);
            }
            int[] toSeq = DyadicArrayHelper.GetPanSeq(dipan, hg); 
            int step = 0;
            step = DyadicArrayHelper.GetOrderStep(panSeqSrc, godArr[7], panSeqSrc[toSeq[0], toSeq[1]]);
            string[,] newPan = DyadicArrayHelper.DyadicNineGongToSideEigthGong(panSeqSrc, step, true);
            return newPan;
        }
        private string[,] OrderStarPan(string[,] dipan, string zhishi, string hg)
        {
            string[,] panSeqSrc = DyadicArrayHelper.InitNineGong(starArr, true);    
            string[] fromto = GetPanSeqStep(starArr, dipan, zhishi, hg);
            int step = 0;
            //if (fromSeq[0] == toSeq[0] && fromSeq[1] == toSeq[1])
            //{
            //    step = DyadicArrayHelper.GetOrderStep(panSeqSrc, srcArr[7], panSeqSrc[toSeq[0], toSeq[1]]);
            //}
            //else
            {
                step = DyadicArrayHelper.GetOrderStep(panSeqSrc, fromto[0], fromto[1]);
            }

            string[,] newPan = DyadicArrayHelper.DyadicNineGongToSideEigthGong(panSeqSrc, step, true);
            return newPan;
            ;
        }
        private string[,] OrderTianPan(string[,] dipan, int step)
        {
            string[,] tian = DyadicArrayHelper.DyadicNineGongToSideEigthGong(dipan, step, true);
            return tian;
        }
        private string[] GetPanSeqStep(string[] srcArr, string[,] dipan, string zhishi, string hg)
        {
            string[,] panSeqSrc = DyadicArrayHelper.InitNineGong(srcArr, true);
            int[] fromSeq = DyadicArrayHelper.GetPanSeq(dipan, zhishi);
            int[] toSeq = DyadicArrayHelper.GetPanSeq(dipan, hg);
            return new string[] { panSeqSrc[fromSeq[0], fromSeq[1]], panSeqSrc[toSeq[0], toSeq[1]] };
        }

        //符首：时所在旬的仪
        private string GetZHISHI(string xunzhi)
        {
            string yi = string.Empty;
            if (xunzhi == zhiArr[0])
            {
                yi = ganArr[4];
            }
            if (xunzhi == zhiArr[2])
            {
                yi = ganArr[9];
            }
            if (xunzhi == zhiArr[4])
            {
                yi = ganArr[8];
            }
            if (xunzhi == zhiArr[6])
            {
                yi = ganArr[7];
            }
            if (xunzhi == zhiArr[8])
            {
                yi = ganArr[6];
            }
            if (xunzhi == zhiArr[10])
            {
                yi = ganArr[5];
            }
            return yi;
        }

        private string GetFuTou(string gz, bool isJiaXun)
        {
            int gfalg = -1;
            int zflag = -1;
            int length = 0;
            string futou = string.Empty;
            string dayG = gz.Trim().Substring(0, 1);
            string dayZ = gz.Trim().Substring(1, 1);
            for (int i = 9; i >= 0; i--)
            {
                if (ganArr[i] == dayG)
                {
                    gfalg = i; break;
                }
            }
            if (isJiaXun)
            {
                length = gfalg;
            }
            else
            {
                if (gfalg > 5) length = gfalg - 5; else length = gfalg;
            }
            for (int i = 11; i >= 0; i--)
            {
                if (zhiArr[i] == dayZ)
                {
                    zflag = i; break;
                }
            }
            if ((zflag - length) < 0)
            {
                zflag = 12 + (zflag - length);
                return zhiArr[zflag];
            }
            return zhiArr[zflag - length];
        }
        private int GetJu(string gz)
        {
            int ju = -1;
            int x = 0;
            int yuan = -1;
            for (int i = 0; i < 60; i++)
            {
                if (gz60[i] == gz)
                {
                    x = x + 1;
                    if (i > 15)
                    {
                        x = i % 15;
                    }
                    if (x > 0 && x <= 5) yuan = 1;
                    else if (x / 5 <= 2) yuan = 2;
                    else yuan = 3;
                    break;                          
                }
            }
            ju = JieQiHelper.GetJuByJieQi(jieQi.Trim().Substring(0, 2), yuan);
            return ju;
        }

        private string GetDiPan6Yi(string gz)
        {
            for (int i=0; i < 60;i++)
            {
                if(gz60[i]==gz)
                {
                    if (i < 10) return yiqiArr[0];
                    else if (i < 20) return yiqiArr[1];
                    else if (i < 30) return yiqiArr[2];
                    else  if (i < 40) return yiqiArr[3];
                    else  if (i < 50) return yiqiArr[4];
                    else if (i < 60) return yiqiArr[5];
                }
            }
            return "";
        }

        private string[] OrderDIPan(int ganWu)
        {
            bool direction = ganWu > 0;
            ganWu = Math.Abs(ganWu);
            string[] order = new string[yiqiArr.Length];
            ganWu = ganWu - 1;
            for (int i = 0; i < yiqiArr.Length; i++)
            {
                if (direction)
                {
                    if (ganWu >= 9) order[ganWu - 9] = yiqiArr[i]; else order[ganWu] = yiqiArr[i];
                    ganWu++;
                }
                else
                {
                    if (ganWu < 0) order[9 + ganWu] = yiqiArr[i]; else order[ganWu] = yiqiArr[i];
                    ganWu--;
                }

            }
            return order;
        }
        private string GetShiGZ(string dayGZ)
        {
            string retV = string.Empty;
            int sG;
            string dayG = dayGZ.Trim().Substring(0, 1);
            int h = DateTime.Now.Hour;
            if (dayG == ganArr[0] || dayG == ganArr[5])
            {
                sG = 0;
                retV = GetShiGZ(sG, h);
            }
            if (dayG == ganArr[1] || dayG == ganArr[6])
            {
                sG = 2;
                retV = GetShiGZ(sG, h);

            } if (dayG == ganArr[2] || dayG == ganArr[7])
            {
                sG = 4;
                retV = GetShiGZ(sG, h);

            } if (dayG == ganArr[3] || dayG == ganArr[8])
            {
                sG = 6;
                retV = GetShiGZ(sG, h);

            } if (dayG == ganArr[4] || dayG == ganArr[9])
            {
                sG = 8;
                retV = GetShiGZ(sG, h);

            }
            return retV;
        }
        private string GetShiGZ(int gFlag, int h)
        {
            int zFlag = -1;
            int p = 0;
            if (h >= 23) zFlag = 0;
            else
            {
                for (int i = 0; i < 24; i += 2)
                {
                    int x = i + 1;
                    int y = i - 1;
                    if (h < x && h >= y)
                    {
                        zFlag = p;
                        break;
                    }
                    p++;

                }
            }
            for (int i = 0; i < zhiArr.Length; i++)
            {
                if (i == zFlag)
                {
                    if (gFlag >= 10) gFlag = gFlag - 10;
                    return ganArr[gFlag] + zhiArr[zFlag];
                }
                gFlag++;
            }
            return string.Empty;
        }
        public static class SelectJieQI
        {
            static string datasource = Application.StartupPath + "/JieQI.db";
            public static SQLiteDataReader GetReadData(int myDiffday)
            {               
                string dts = string.Format("{0:G}", DateTime.Now);
                string mydiffdate = string.Empty;
                if (myDiffday > -1) mydiffdate = "-" + myDiffday + " days";
                else mydiffdate = "+" + Math.Abs(myDiffday) + " days";
                string sql = "SELECT * FROM [ChinaJiQiTable] where strftime('%Y-%m-%d',DateValue) < strftime('%Y-%m-%d',strftime('%Y-%m-%d', 'now', '" + mydiffdate + "')) and strftime('%Y-%m-%d',DateValue) > strftime('%Y-%m-%d',strftime('%Y-%m-%d', 'now', '-30 days')) ORDER BY DateValue DESC Limit 1 ";//;'now') ";//and strftime('%m',DateValue)>strftime('%m','now') and  strftime('%d',DateValue)>strftime('%d','now') ";//DateValue > '" + dts + "' Limit 1 Offset 0";
                return  SqlLiteHelper.ExecuteDataReader(sql, datasource); 
            }
        }
        private void GetAllGZ()
        {
            //if (1==2)
            {
                //string sql = "select  [gzDay],[gzMonth],[gzYear],DateValue=([DateValue])+' '+ substring(JieQi,4,5),[weekDay],[constellation],JieQi,[nlMonth],[nlDay]  from [ChineseTenThousandCalendar] where left(ltrim(JieQi),2) in (" + JieQiHelper.GetInJieQis() + ")";
                //string datasource = Application.StartupPath + "/JieQi.db";
                //SqlLiteHelper.SqlServerToSqlLite(datasource, sql);
            }

            int myDiffday = 0;
            string jd = string.Empty;
            string jm = string.Empty;
            string jy = string.Empty;
            string jnd = string.Empty;
            string jnm = string.Empty;
            int day = 0;
            System.Data.SQLite.SQLiteDataReader reader = SelectJieQI.GetReadData(myDiffday);
            if (reader != null)
            {
                while (reader.Read())
                {
                    jieQi = reader["jieQi"].ToString().Trim() + " " + reader["DayGZ"].ToString().Trim() + "日" + reader["DateValue"].ToString();
                     jd = reader["DayGZ"].ToString();
                     jm = reader["MonthGZ"].ToString();
                     jy = reader["YearGZ"].ToString();
                     jnd = reader["NongLiDay"].ToString();
                     jnm = reader["NongLiMonth"].ToString(); ;
                    DateTime nowdate = DateTime.Now.AddDays(myDiffday);
                    DateTime jqt = DateTime.Parse(reader["DateValue"].ToString());
                    TimeSpan ts =  nowdate - jqt;
                    day = ts.Days;                   
                }
                reader.Close();
                reader.Dispose();
            }            
            CalCurrentAllGZ(day, jd, jm, jy, jnd, jnm);
        }
        private void CalCurrentAllGZ(int day, string jd, string jm, string jy, string jnd, string jnm)
        {
            int g = DyadicArrayHelper.GetArrayIndexByValue(ganArr, jd.Substring(0, 1));
            int z = DyadicArrayHelper.GetArrayIndexByValue(zhiArr, jd.Substring(1, 1));
            day = day - 1;//约一个记时日，因交节气时间不可能是24：00，时家奇门，如果有刻奇门，或分奇门理论上要比时家奇门要精确。
            for (int i =0 ; i <= day; i++)
            {
                g++;
                if (g > 9) g = g-10 ;
                z++;
                if (z > 11) z = z- 12 ;
            }
            dayGZ = ganArr[g] + zhiArr[z];//得到日干支
            yearGZ = jy;
            monthGZ = jm;
            int monthIndex = DyadicArrayHelper.GetArrayIndexByValue(JieQiHelper.Days(true).ToArray(), jnd.Replace("日",""));
            int mdays = 30;
            if (jnm.IndexOf("小") > 0) mdays = 29;
            if (monthIndex + day > mdays) //跨月
            {
               SQLiteDataReader reader =  SelectJieQI.GetReadData((-1)*day+10);//因节气在上个月，时间回本月，取节气前一个月的月干支，年干支
               if (reader != null)
               {
                   while (reader.Read())
                   {
                       monthGZ = reader["MonthGZ"].ToString();
                       yearGZ = reader["YearGZ"].ToString();
                   }
               }
               reader.Close();
               reader.Dispose();
            }
        }
        private string GetJieQiByID(string p)
        {
            string jieqi = string.Empty;
            string sql = " SELECT DayGZ,DateValue,jieQi FROM [GanZiTable] where id > (" + p + " -30)  and id < " + p + " order by ID Desc ";
            SQLiteDataReader reader = SqlLiteHelper.ExecuteDataReader(sql, Application.StartupPath + "/JieQi.db");
            if (reader != null)
            {
                while (reader.Read())
                {
                    List<string> jieQis = JieQiHelper.GetTwentyFourJieQi();
                    if ((reader["jieQi"] != null && reader["jieQi"].ToString().Trim().Length > 2 && jieQis.Contains(reader["jieQi"].ToString().Trim().Substring(0, 2))))
                    {
                        jieqi = reader["jieQi"].ToString().Trim() + " " + reader["DayGZ"].ToString().Trim() + "日" + reader["DateValue"].ToString();
                        return jieqi;
                    }
                }
                reader.Close();
            }
            return jieqi;

        }

        private void Label1_DoubleClick(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
