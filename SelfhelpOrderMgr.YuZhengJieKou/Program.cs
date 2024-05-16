using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SelfhelpOrderMgr.YuZhengJieKou
{
    class Program
    {

        static void Main(string[] args)
        {

            //string username = "fzjyhjglxt";
            //string password = "47C62260928D48B283E683EE27C93D00";
            //string _baseUrl = "http://29.211.1.1/";

            string username = AppLinkHelper.GetUser();
            string password = AppLinkHelper.GetPwd();
            string _baseUrl = AppLinkHelper.GetUrl();
            string _zfzt = AppLinkHelper.GetZfzt();
            string _searchDicts = AppLinkHelper.GetDicts();
            string _pageSize = AppLinkHelper.GetPageSize();

            ServiceInterface srv = new ServiceInterface(username,password,_baseUrl,_zfzt);

            //var rs = srv.startService();

            if(srv.loginFlag)
            {
                //获取字典信息
                var rs = srv.GetDictDataList(_searchDicts);
                Console.WriteLine(Newtonsoft.Json.JsonConvert.SerializeObject(rs));

                //获取队别信息
                rs = srv.GetDeptList();
                Console.WriteLine(Newtonsoft.Json.JsonConvert.SerializeObject(rs));

                //获取人员基本信息
                rs = srv.GetJbxxList(_zfzt,Convert.ToInt32(_pageSize));
                Console.WriteLine(Newtonsoft.Json.JsonConvert.SerializeObject(rs));


                //获取社会关系
                rs = srv.GetShgxList( Convert.ToInt32(_pageSize));
                Console.WriteLine(Newtonsoft.Json.JsonConvert.SerializeObject(rs));

                Console.WriteLine("恭喜！信息已经同步完成。");
            }
            else
            {
                Console.WriteLine("错误！请检查用户信息或网络是否正常。");
            }
            

            Thread.Sleep(10000);
            //Console.ReadKey();


        }
    }
}
