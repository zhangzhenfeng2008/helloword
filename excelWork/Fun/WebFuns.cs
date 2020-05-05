using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HtmlAgilityPack;

namespace excelWork.Fun
{
    public struct Address
    {
        public string Url;
        public string City;
        public string SF;
        public List<string> qy;
    }

   
    public class WebFuns
    {
        private static List<Address> Addlist;
        private static int page = 1;
        private static int listindex = 0;
        public WebFuns()
        {
            iniAddlist();
        }

        private void iniAddlist()
        {
            if (Addlist != null) return;
            Addlist = new List<Address>();
            Address add = new Address();
            add.City = "上海市";
            add.SF = "";
            add.Url = "http://sh.city8.com/canyinfuwu/poi1-{0}.html";
            List<string> qylist = new List<string>();
            add.qy = qylist;
            Addlist.Add(add);

            add = new Address();
            add.City = "广州市";
            add.SF = "广东省";
            add.Url = "http://gz.city8.com/canyinfuwu/poi1-{0}.html";
            add.qy = qylist;
            Addlist.Add(add);

            add = new Address();
            add.City = "深圳市";
            add.SF = "";
            add.Url = "http://sz.city8.com/canyinfuwu/poi1-{0}.html";
            add.qy = qylist;
            Addlist.Add(add);


            add = new Address();
            add.City = "武汉市";
            add.SF = "湖北省";
            add.Url = "http://wh.city8.com/canyinfuwu/poi1-{0}.html";
            add.qy = qylist;
            Addlist.Add(add);

            add = new Address();
            add.City = "成都市";
            add.SF = "四川省";
            add.Url = "http://cd.city8.com/canyinfuwu/poi1-{0}.html";
            add.qy = qylist;
            Addlist.Add(add);

            add = new Address();
            add.City = "海口市";
            add.SF = "海南省";
            add.Url = "http://hk.city8.com/canyinfuwu/poi1-{0}.html";
            add.qy = qylist;
            Addlist.Add(add);

            add = new Address();
            add.City = "三亚市";
            add.SF = "海南省";
            add.Url = "http://sy.city8.com/canyinfuwu/poi1-{0}.html";
            add.qy = qylist;
            Addlist.Add(add);

            add = new Address();
            add.City = "丽江市";
            add.SF = "云南省";
            add.Url = "http://lj.city8.com/canyinfuwu/poi1-{0}.html";
            add.qy = qylist;
            Addlist.Add(add);

            add = new Address();
            add.City = "长沙市";
            add.SF = "湖南省";
            add.Url = "http://changsha.city8.com/canyinfuwu/poi1-{0}.html";
            add.qy = qylist;
            Addlist.Add(add);

            add = new Address();
            add.City = "南京市";
            add.SF = "江苏省";
            add.Url = "http://nanjing.city8.com/canyinfuwu/poi1-{0}.html";
            add.qy = qylist;
            Addlist.Add(add);

            add = new Address();
            add.City = "韶关市";
            add.SF = "广东省";
            add.Url = "http://shaoguan.city8.com/canyinfuwu/poi1-{0}.html";
            add.qy = qylist;
            Addlist.Add(add);

            add = new Address();
            add.City = "郴州市";
            add.SF = "湖南省";
            add.Url = "http://chenzhou.city8.com/canyinfuwu/poi1-{0}.html";
            add.qy = qylist;
            Addlist.Add(add);

            add = new Address();
            add.City = "桂林市";
            add.SF = "广西省";
            add.Url = "http://gl.city8.com/canyinfuwu/poi1-{0}.html";
            add.qy = qylist;
            Addlist.Add(add);

            add = new Address();
            add.City = "清远市";
            add.SF = "广东省";
            add.Url = "http://qingyuan.city8.com/canyinfuwu/poi1-{0}.html";
            add.qy = qylist;
            Addlist.Add(add);


            add = new Address();
            add.City = "东莞市";
            add.SF = "广东省";
            add.Url = "http://dg.city8.com/canyinfuwu/poi1-{0}.html";
            add.qy = qylist;
            Addlist.Add(add);


            add = new Address();
            add.City = "杭州市";
            add.SF = "浙江省";
            add.Url = "http://hz.city8.com/canyinfuwu/poi1-{0}.html";
            add.qy = qylist;
            Addlist.Add(add);

            add = new Address();
            add.City = "惠州市";
            add.SF = "广东省";
            add.Url = "http://huizhou.city8.com/canyinfuwu/poi1-{0}.html";
            add.qy = qylist;
            Addlist.Add(add);

            add = new Address();
            add.City = "佛山市";
            add.SF = "广东省";
            add.Url = "http://fs.city8.com/canyinfuwu/poi1-{0}.html";
            add.qy = qylist;
            Addlist.Add(add);


            add = new Address();
            add.City = "南宁市";
            add.SF = "广西省";
            add.Url = "http://nn.city8.com/canyinfuwu/poi1-{0}.html";
            add.qy = qylist;
            Addlist.Add(add);

            add = new Address();
            add.City = "南充市";
            add.SF = "四川省";
            add.Url = "http://nanchong.city8.com/canyinfuwu/poi1-{0}.html";
            add.qy = qylist;
            Addlist.Add(add);


            add = new Address();
            add.City = "南昌市";
            add.SF = "江西省";
            add.Url = "http://nc.city8.com/canyinfuwu/poi1-{0}.html";
            add.qy = qylist;
            Addlist.Add(add);

            add = new Address();
            add.City = "赣州市";
            add.SF = "江西省";
            add.Url = "http://ganzhou.city8.com/canyinfuwu/poi1-{0}.html";
            add.qy = qylist;
            Addlist.Add(add);

            add = new Address();
            add.City = "郑州市";
            add.SF = "河南省";
            add.Url = "http://zz.city8.com/canyinfuwu/poi1-{0}.html";
            add.qy = qylist;
            Addlist.Add(add);
        }

        private Address GetAddress()
        {
             if (listindex >= Addlist.Count)
             {
                 listindex = 0;
                 page++;
                 if (page > 11)
                 {
                     page = 1;
                 }
             }
             Address add = Addlist[listindex];
             listindex++;
             return add;
        }


        public List<string> GetUserAddress()
        {
            List<string> values = new List<string>();
            Address add = GetAddress();
            HtmlWeb hw = new HtmlWeb();
            HtmlDocument doc = hw.Load(string.Format(add.Url, page));
            HtmlNodeCollection cols = doc.DocumentNode.SelectNodes("//div[@class='v2synei']//a");
            foreach (HtmlNode node in cols)
            {
                string item = node.InnerText.Replace("附近", "");
                if (item.Contains("【")) continue;
                if (item.Contains("区") || item.Contains("号"))
                {
                    int index = item.IndexOf("省");
                    if(index>0)
                    {
                        item=item.Substring(index+1);
                    }
                    index = item.IndexOf("区");
                    if (index > 0)
                    {
                        item = item.Substring(index + 1);
                    }
                    item = item.Replace(add.City, "");
                    index = item.IndexOf("号");
                    if (index > 0)
                    {
                        item = item.Substring(0, index+1);
                    }
                    if (item.Contains("号") && item.Contains("路"))
                    {
                        if (values.Contains(item)) continue;
                        values.Add(item);
                    }
                }
            }
            return values;
        }
        private List<string> Shops;

        public void IniShops()
        {
            Shops = new List<string>();
            HtmlWeb hw = new HtmlWeb();
            HtmlDocument doc = hw.Load("http://sh.city8.com/shangjia/");
            HtmlNodeCollection cols = doc.DocumentNode.SelectNodes("//div[@id='sj_other']//a");
            foreach (HtmlNode node in cols)
            {
                if (node.InnerText.Contains("商户"))
                {
                    Shops.Add("http:"+ node.GetAttributeValue("href", "//sz.city8.com/shangjia/"));
                }
            }

        }

        public Stack<string> GetCompany()
        {
            Stack<string> list = new Stack<string>();
            //if (Shops == null)
            //{
            //    IniShops();
            //}
            //HtmlWeb hw = new HtmlWeb();
            //HtmlDocument doc = hw.Load(Shops.Pop());
            //HtmlNodeCollection cols = doc.DocumentNode.SelectNodes("//div[@class='v2synei']//a");
            //foreach (HtmlNode node in cols)
            //{
            //    string company = node.InnerText.Trim();
            //    if (company.Contains("公司"))
            //    {
            //        if (list.Contains(company)) continue;
            //        list.Push(company);
            //    }
            //}

            return list;

        }


        public List<string> GetCompany(string Url)
        {
            List<string> list = new List<string>();
            foreach (string s in Shops)
            {
                HtmlWeb hw = new HtmlWeb();
                HtmlDocument doc = hw.Load(s);
                HtmlNodeCollection cols = doc.DocumentNode.SelectNodes("//div[@class='v2synei']//a");
                foreach (HtmlNode node in cols)
                {
                    string company = node.InnerText.Trim();
                    if (company.Contains("公司"))
                    {
                        if (list.Contains(company)) continue;
                        list.Add(company);
                    }
                }
            }

            return list;

        }
    }
}
