using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using System.Windows.Forms;

namespace excelWork.Fun
{
    public class UserData
    {

        private List<string> yuyan;
        private List<string> zuocai;
        private List<string> techang;
        public UserData()
        {
            yuyan = new List<string>();
            yuyan.Add("英语");
            yuyan.Add("广东话");
            yuyan.Add("客家话");
            yuyan.Add("潮汕话");
            zuocai = new List<string>();
            zuocai.Add("月子餐");
            zuocai.Add("粤菜");
            zuocai.Add("湘菜");
            zuocai.Add("川菜");
            zuocai.Add("面食");
            techang = new List<string>();
            techang.Add("双胎");
            techang.Add("催乳");
            techang.Add("产后康复");
            techang.Add("育婴");
            techang.Add("小儿推拿");
            inicitys();
        }

        private void inicitys()
        {
            if (citys == null)
            {
                citys = new Dictionary<string, string>();
                XmlDocument doc = new XmlDocument();
                doc.Load("citys.xml");
                XmlNode nodel = doc.ChildNodes[1];
                foreach (XmlNode item in nodel.ChildNodes)
                {
                    string[] arr = item.InnerText.Split(',');
                    if (arr.Length == 2)
                    {
                        if (citys.ContainsKey(arr[0])) continue;
                        citys.Add(arr[0], arr[1]);
                    }
                }
            }
        }
        public static string GetGenderByIdCard(string idCard)
        {
            bool b = false;
            if (!string.IsNullOrWhiteSpace(idCard))
            {
                b=Convert.ToBoolean(int.Parse(idCard.Substring(16, 1)) % 2);
            }
            return b ? "男" : "女";
        }

        public static string GetBirthDayByIdCard(string idCard)
        {
            string year = idCard.Substring(6,4);
            string month = idCard.Substring(10, 2);
            string date = idCard.Substring(12, 2);
            string result = year + "-" + month + "-" + date;
            return result;
        }

        public static string get_shengxiao(string idCard)
        { //根据身份证号，自动返回对应的生肖
            if (string.IsNullOrEmpty(idCard)) return "";
            int start = 1901;
            int end = Convert.ToInt32(idCard.Substring(6, 4));
            int x = (start - end) % 12;
            string value = "";
            if (x == 1 || x == -11) { value = "鼠"; }
            if (x == 0) { value = "牛"; }
            if (x == 11 || x == -1) { value = "虎"; }
            if (x == 10 || x == -2) { value = "兔"; }
            if (x == 9 || x == -3) { value = "龙"; }
            if (x == 8 || x == -4) { value = "蛇"; }
            if (x == 7 || x == -5) { value = "马"; }
            if (x == 6 || x == -6) { value = "羊"; }
            if (x == 5 || x == -7) { value = "猴"; }
            if (x == 4 || x == -8) { value = "鸡"; }
            if (x == 3 || x == -9) { value = "狗"; }
            if (x == 2 || x == -10) { value = "猪"; }
            return value;
        }

        public static int GetAgeByIdCard(string idCard)
        {
            int age = 0;
            if (!string.IsNullOrWhiteSpace(idCard))
            {
                var subStr = string.Empty;
                if (idCard.Length == 18)
                {
                    subStr = idCard.Substring(6, 8).Insert(4, "-").Insert(7, "-");
                }
                else if (idCard.Length == 15)
                {
                    subStr = ("19" + idCard.Substring(6, 6)).Insert(4, "-").Insert(7, "-");
                }
                TimeSpan ts = DateTime.Now.Subtract(Convert.ToDateTime(subStr));
                age = ts.Days / 365;
            }
            return age;
        }



            /// <summary>
            /// 地区代码表(默认为空，需初始化：简称、全称)
            /// </summary>
            public Hashtable m_DistrictTB = new Hashtable();

            /// <summary>
            /// 初始化：地区代码：简称
            /// </summary>
            public void InitDistrictTable_Short()
            {
                m_DistrictTB.Clear();
                //11-15 京、津、冀、晋、蒙 
                m_DistrictTB.Add("11", "京");
                m_DistrictTB.Add("12", "津");
                m_DistrictTB.Add("13", "冀");
                m_DistrictTB.Add("14", "晋");
                m_DistrictTB.Add("15", "蒙");
                //21-23 辽、吉、黑 
                m_DistrictTB.Add("21", "辽");
                m_DistrictTB.Add("22", "吉");
                m_DistrictTB.Add("23", "黑");
                //31-37 沪、苏、浙、皖、闽、赣、鲁 
                m_DistrictTB.Add("31", "沪");
                m_DistrictTB.Add("32", "苏");
                m_DistrictTB.Add("33", "浙");
                m_DistrictTB.Add("34", "皖");
                m_DistrictTB.Add("35", "闽");
                m_DistrictTB.Add("36", "赣");
                m_DistrictTB.Add("37", "鲁");
                //41-46 豫、鄂、湘、粤、桂、琼 
                m_DistrictTB.Add("41", "豫");
                m_DistrictTB.Add("42", "鄂");
                m_DistrictTB.Add("43", "湘");
                m_DistrictTB.Add("44", "粤");
                m_DistrictTB.Add("45", "桂");
                m_DistrictTB.Add("46", "琼");
                //50-54 渝、川、贵、云、藏 
                m_DistrictTB.Add("50", "渝");
                m_DistrictTB.Add("51", "川");
                m_DistrictTB.Add("52", "贵");
                m_DistrictTB.Add("53", "云");
                m_DistrictTB.Add("54", "藏");
                //61-65 陕、甘、青、宁、新 
                m_DistrictTB.Add("61", "陕");
                m_DistrictTB.Add("62", "甘");
                m_DistrictTB.Add("63", "青");
                m_DistrictTB.Add("64", "宁");
                m_DistrictTB.Add("65", "新");
                //71 台湾
                m_DistrictTB.Add("71", "台");
                //81-82 港、澳 
                m_DistrictTB.Add("81", "港");
                m_DistrictTB.Add("82", "澳");
                //91 国外
                m_DistrictTB.Add("91", "外");
            }

            /// <summary>
            /// 初始化：地区代码：全称
            /// </summary>
            public void InitDistrictTable_Full()
            {
                m_DistrictTB.Clear();
                //11-15 京、津、冀、晋、蒙 
                m_DistrictTB.Add("11", "北京");
                m_DistrictTB.Add("12", "天津");
                m_DistrictTB.Add("13", "河北");
                m_DistrictTB.Add("14", "山西");
                m_DistrictTB.Add("15", "内蒙古");
                //21-23 辽、吉、黑 
                m_DistrictTB.Add("21", "辽宁");
                m_DistrictTB.Add("22", "吉林");
                m_DistrictTB.Add("23", "黑龙江");
                //31-37 沪、苏、浙、皖、闽、赣、鲁 
                m_DistrictTB.Add("31", "上海");
                m_DistrictTB.Add("32", "江苏");
                m_DistrictTB.Add("33", "浙江");
                m_DistrictTB.Add("34", "安徽");
                m_DistrictTB.Add("35", "福建");
                m_DistrictTB.Add("36", "江西");
                m_DistrictTB.Add("37", "山东");
                //41-46 豫、鄂、湘、粤、桂、琼 
                m_DistrictTB.Add("41", "河南");
                m_DistrictTB.Add("42", "湖北");
                m_DistrictTB.Add("43", "湖南");
                m_DistrictTB.Add("44", "广东");
                m_DistrictTB.Add("45", "广西");
                m_DistrictTB.Add("46", "海南");
                //50-54 渝、川、贵、云、藏 
                m_DistrictTB.Add("50", "重庆");
                m_DistrictTB.Add("51", "四川");
                m_DistrictTB.Add("52", "贵州");
                m_DistrictTB.Add("53", "云南");
                m_DistrictTB.Add("54", "西藏");
                //61-65 陕、甘、青、宁、新 
                m_DistrictTB.Add("61", "陕西");
                m_DistrictTB.Add("62", "甘肃");
                m_DistrictTB.Add("63", "青海");
                m_DistrictTB.Add("64", "宁夏");
                m_DistrictTB.Add("65", "新疆");
                //71 台湾
                m_DistrictTB.Add("71", "台湾");
                //81-82 港、澳 
                m_DistrictTB.Add("81", "香港");
                m_DistrictTB.Add("82", "澳门");
                //91 国外
                m_DistrictTB.Add("91", "国外");
            }

            /// <summary>
            /// 地区代码返回结果类型：Full(全称)、Short(简称)
            /// </summary>
            public enum DistrictResultType
            {
                /// <summary>
                /// 全称
                /// </summary>
                Full,
                /// <summary>
                /// 简称
                /// </summary>
                Short
            }

            /// <summary>
            /// 通过两位地区码得到对应的地区名称
            /// </summary>
            /// <param name="code">两位地区码</param>
            /// <param name="resType">返回类型：Full(全称)、Short(简称)</param>
            /// <returns>对应的地区名称</returns>
            public string GetDistrictCode(string code, int resType)
            {
                try
                {
                    string codeStr = "";
                    if (code.Length == 2)
                    {
                        //初始化：全称
                        if (resType == (int)DistrictResultType.Full)
                        {
                            InitDistrictTable_Full();
                        }
                        //初始化：简称
                        if (resType == (int)DistrictResultType.Short)
                        {
                            InitDistrictTable_Short();
                        }
                        //获取对应键值的结果
                        if (m_DistrictTB.ContainsKey(code))
                        {
                            codeStr = m_DistrictTB[code].ToString();
                        }
                    }
                    return codeStr;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            public string GetSF(string idCard)
            {
                return GetDistrictCode(idCard.Substring(0, 2), 0);
            }

            private static Dictionary<string, string> citys;

            /// 根据营业执照号得到地区信息(身份证号也可)
            /// </summary>
            /// <param name="BusinessCode"></param>
            /// <param name="provinceName"></param>
            /// <param name="cityName"></param>
            /// <param name="xianName"></param>
            public  void GetRegionInfo(string BusinessCode, out string provinceName, out string cityName, out string xianName)
            { 
                var province = BusinessCode.Substring(0, 2) + "0000";
                var city = BusinessCode.Substring(0, 4) + "00";
                var xian = BusinessCode.Substring(0, 6);
                provinceName = "";
                cityName = "";
                xianName = "";

                if (citys.ContainsKey(xian))
                {
                    xianName = citys[xian].ToString();
                }
                if (citys.ContainsKey(city))
                {
                    cityName = citys[city].ToString();
                }
                if (citys.ContainsKey(province))
                {
                    provinceName = citys[province].ToString();
                }
            }

            public List<string> First;
            public List<string> Mids;
            public void iniTXT()
            {
                if (First != null) return;
                First = new List<string>();
                Mids = new List<string>();
                string Filepath = Path.Combine(Application.StartupPath, "pingjia.txt");
                string txt = ReadTextFile(Filepath).Replace("\r","");
                string[] A =txt.Split('\n');
                foreach (string item in A)
                {
                    string[] B = item.Split('。');
                    if (B.Length == 1)
                    {
                        if (string.IsNullOrEmpty(B[0])) continue;
                        First.Add(B[0]); continue;
                    }
                    for (int i = 1; i < B.Length; i++)
                    {
                        string Bi = B[i].Trim();
                        if (string.IsNullOrEmpty(Bi)) continue;
                        if (Bi.Contains("本人") || Bi.Contains("我"))
                        {
                            First.Add(Bi); continue;
                        }
                        Mids.Add(Bi);
                    }
                }
      
            }

            public string GetPingjia()
            {
                iniTXT();
                Random ran = new Random(Guid.NewGuid().GetHashCode());
                int Findex = ran.Next(First.Count);
                int Mid = ran.Next(Mids.Count);
                string head = string.Empty;
                if (Findex % 2 == 0)
                {
                    head = First[Findex];
                }
                else
                {
                    Findex = ran.Next(Mids.Count);
                    while (Findex == Mid)
                    {
                        Findex = ran.Next(Mids.Count);
                    }
                    head = Mids[Findex];
                }
                return head + "。" + Mids[Mid] + "。";
            }

            public List<string> gangweilist;
            public string Getgangwei()
            {
                if (gangweilist == null)
                {
                    gangweilist = new List<string>();
                    string Filepath = Path.Combine(Application.StartupPath, "gangwei.txt");
                    string txt = ReadTextFile(Filepath).Replace("\r", "");
                    string[] gs = txt.Split('\n');
                    foreach (string item in gs)
                    {
                        string gi = item.Trim();
                        if (string.IsNullOrEmpty(gi)) continue;
                        gangweilist.Add(gi);
                    }
                }

                Random ran = new Random(Guid.NewGuid().GetHashCode());
                int index = ran.Next(gangweilist.Count);
                return gangweilist[index];
 
            }

            public List<string[]> GetCards()
            {
                List<string[]> list = new List<string[]>();
                string Filepath = Path.Combine(Application.StartupPath, "card.txt");
                string txt = ReadTextFile(Filepath).Replace("\r", "").Replace("\t", "");
                string[] cs = txt.Split('\n');
                foreach (string item in cs)
                {
                    string gi = item.Trim();
                    if (string.IsNullOrEmpty(gi)) continue;
                    string[] arr = item.Split(',');
                    if (arr.Length == 2)
                    {
                        list.Add(new string[2] { arr[0], arr[1] });
                    }
                }
                return list;
            }

            public string ReadTextFile(string logFile)
            {
                StreamReader sr = File.OpenText(logFile);
                string fileTxt = sr.ReadToEnd();
                sr.Close();
                return fileTxt;
            }

            public void WriteTextFile(string txt)
            {
                using (StreamWriter sw = new StreamWriter(@"company.txt", true))
                {
                    sw.WriteLine(txt);
                    sw.Close();
                }
            }

            public string GetWorkTime(int age)
            {
                int len = age - 18;
                if (len > 0)
                {
                    Random ran = new Random();
                    int le= ran.Next(1,len);
                    DateTime dt = DateTime.Now;
                    int year = dt.Year - le;
                    int Month = ran.Next(1, 13);
                    int day = ran.Next(1, 28);
                    int Monthend = ran.Next(1, dt.Month+1);
                    int dayend= ran.Next(1, 28);

                    return year.ToString("0000") + "/" + Month.ToString("00") + "/" + day.ToString("00") + " - " + dt.Year.ToString("0000") + "/" + Monthend.ToString("00") + "/" + dayend.ToString("00");
                }
                return string.Empty;
            }

            public List<string> GetTc()
            {
                List<string> list = new List<string>();
                Random ran = new Random();
                int index = ran.Next(yuyan.Count);
                list.Add("普通话、" + yuyan[index]);
                int count = ran.Next(2, 4);
                List<string> zclist = new List<string>();
                while (zclist.Count < count)
                {
                  index= ran.Next(zuocai.Count);
                  if (zclist.Contains(zuocai[index])) continue;
                  zclist.Add(zuocai[index]);
                }
                string zc = string.Empty;
                foreach (string item in zclist)
                {
                    zc += item + "、";
                }
                zc = zc.Substring(0, zc.Length - 1);
                list.Add(zc);


                count = ran.Next(2, 4);
                List<string> tclist = new List<string>();
                while (tclist.Count < count)
                {
                    index = ran.Next(techang.Count);
                    if (tclist.Contains(techang[index])) continue;
                    tclist.Add(techang[index]);
                }
                string tc = string.Empty;
                foreach (string item in tclist)
                {
                    tc += item + "、";
                }
                tc = tc.Substring(0, tc.Length - 1);
                list.Add(tc);

                return list;
            }

            private string GetJiGuan(string CarID)
            {
                 string provinceName,cityName,xianName;
                 GetRegionInfo(CarID, out provinceName, out cityName, out xianName);
                 return provinceName + cityName;
            }

            private string[] telStarts = "134,135,136,137,138,139,150,151,152,157,158,159,130,131,132,155,156,133,153,180,181,182,183,185,186,176,187,188,189,177,178".Split(',');
            /// <summary>
            /// 随机生成电话号码
            /// </summary>
            /// <returns></returns>
            public string getRandomTel()
            {
                Random ran = new Random();
                int n = ran.Next(10, 1000);
                int index = ran.Next(0, telStarts.Length - 1);
                string first = telStarts[index];
                string second = (ran.Next(100, 888) + 10000).ToString().Substring(1);
                string thrid = (ran.Next(1, 9100) + 10000).ToString().Substring(1);
                return first + second + thrid;
            }

            private string[] xlarr = new string[5] {"高中","中专","大专","本科","硕士" };

            private List<string> Companys = new List<string>();
            public string GetCompany()
            {
                if (Companys.Count == 0)
                {
                    string Filepath = Path.Combine(Application.StartupPath, "company.txt");
                    string txt = ReadTextFile(Filepath).Replace("\r", "").Replace("\t", "");
                    string[] cs = txt.Split('\n');
                    foreach (string item in cs)
                    {
                        string gi = item.Trim();
                        if (string.IsNullOrEmpty(gi)) continue;
                        Companys.Add(gi);
                    }
                }
                Random ran = new Random(Guid.NewGuid().GetHashCode());
                return Companys[ran.Next(Companys.Count)];

            }

            public Model GetModel(string CarID, string Name)
            {
                Model model = new Model();
                model.CardID = CarID;
                model.Name = Name;
                model.Age = GetAgeByIdCard(CarID);
                model.BirthDay = GetBirthDayByIdCard(CarID);
                model.chushengdi = GetJiGuan(CarID);
                List<string> tc= GetTc();
                model.fuwutechang = tc[2];
                model.yuyan = tc[0];
                model.zuocainengli = tc[1];
                Random rand = new Random();
                model.hunyin = rand.Next(10) % 2 == 0 ? "未婚" : "已婚";
                model.phoneNub = getRandomTel();
                model.qita = "";
                model.Sex = GetGenderByIdCard(CarID);
                model.shengao = rand.Next(160, 175).ToString() + "CM";
                model.tizhong = rand.Next(40, 65).ToString() + "KG";
                model.xueli = xlarr[rand.Next(xlarr.Length)];
                model.ziwopinjia = GetPingjia();
                model.times = GetWorkTime(model.Age);
                model.Company = GetCompany();
                model.position = Getgangwei();
                return model;

            }

           

   
    }
}
