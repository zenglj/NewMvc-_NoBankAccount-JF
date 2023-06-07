using SelfhelpOrderMgr.BLL;
using SelfhelpOrderMgr.Common;
using SelfhelpOrderMgr.Model;
using SelfhelpOrderMgr.YuZhengJieKou.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SelfhelpOrderMgr.YuZhengJieKou
{


    class ServiceInterface
    {
        private string token = "";
        //public string _url="http://localhost:8080/api/jwt/login?password=cs&username=cs";
        private string _baseurl = "http://localhost:8080/";
        private string _zfzt = "";
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="username">用户名</param>
        /// <param name="password">密码</param>
        public ServiceInterface(string username,string password, string baseurl,string zfzt)
        {
            this._baseurl = baseurl;
            this._zfzt = zfzt;
            this.UserLogin(username, password);
        }
        public ResultInfo startService() {
            ResultInfo rs = new ResultInfo();

            //获取银企直连配置参数
            //var setting = new ConfigHelper().GetYinQiSetting();
            //string strCommand = "b2e0012";
            object _obj = new
            {
                unitCode = "FJJXYY08",
                fCode = "3512027398",
                remark = ""
            };
            string _res = HttpHelper.HttpPostByJson("http://39.98.197.192:8090/api/admin/Auth/GetQueryCriminalBankInfo", Newtonsoft.Json.JsonConvert.SerializeObject(_obj),token);

            if (_res.StartsWith("Err|"))
            {
                Console.WriteLine("失败:"+_res);

                rs.ReMsg = "失败";
                rs.Flag = false;
                rs.DataInfo = _res;
            }
            else
            {
                rs.ReMsg = "成功";
                rs.Flag = true;
                rs.DataInfo = _res;
            }
                       
            return rs;
        }



        /// <summary>
        /// 用户登录并获取token
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public ResultInfo UserLogin(string username,string password)
        {
            string subUrl = $"api/jwt/login?password={password}&username={username}";
            ResultInfo rs = new ResultInfo();

            //获取银企直连配置参数
            //var setting = new ConfigHelper().GetYinQiSetting();

            //object _obj = new
            //{
            //    unitCode = "FJJXYY08",
            //    fCode = "3512027398",
            //    remark = ""
            //};
            try
            {
                //string _res = HttpHelper.HttpPostByJson("http://39.98.197.192:8090/api/admin/Auth/GetQueryCriminalBankInfo", Newtonsoft.Json.JsonConvert.SerializeObject(_obj), token);
                string _res = HttpHelper.HttpPostByJson(_baseurl+subUrl, "", "");

                if (_res.StartsWith("Err|"))
                {
                    Console.WriteLine("失败:" + _res);

                    rs.ReMsg = "失败";
                    rs.Flag = false;
                    rs.DataInfo = _res;
                    return rs;
                }
                

                TokenMsg tokenMsg = Newtonsoft.Json.JsonConvert.DeserializeObject<TokenMsg>(_res);
                if (tokenMsg.code == 0)
                {
                    token = tokenMsg.token;
                }
                rs.ReMsg = "获取Token成功";
                rs.Flag = true;
                rs.DataInfo = tokenMsg.token;
            }
            catch (Exception e)
            {
                rs.ReMsg="Err|"+e.Message;
            }
            Console.WriteLine(rs.ReMsg);
            return rs;
        }

        /// <summary>
        /// 获取组织信息
        /// </summary>
        /// <returns></returns>
        public ResultInfo GetDeptList()
        {
            Console.WriteLine("正在队别基本信息......");
            string subUrl = $"api/getDeptList";
            ResultInfo rs = new ResultInfo();

            //获取银企直连配置参数
            //var setting = new ConfigHelper().GetYinQiSetting();

            try
            {
                string _res = HttpHelper.HttpPostByJson(_baseurl + subUrl, "", token);


                ReqMsg<SysDept> _msg = Newtonsoft.Json.JsonConvert.DeserializeObject<ReqMsg<SysDept>>(_res);
                if (_msg.code == 0)
                {
                    if (_msg.data.Count > 0)
                    {
                        string sql = @"IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[YzglTempDept]') AND type in (N'U'))
                            DROP TABLE [dbo].[YzglTempDept]                            

                            CREATE TABLE [dbo].[YzglTempDept](
	                            [FCode] [varchar](50) NOT NULL,
	                            [FName] [varchar](60) NULL,
	                            [ID] [varchar](50) NULL,
	                            [FID] [varchar](50) NULL,
	                            [URL] [varchar](50) NULL,
	                            [FTZSP_Money] [numeric](18, 2) NULL,
	                            [SaleCloseFlag] [int] NULL,
                             CONSTRAINT [PK_YzglTempDept] PRIMARY KEY CLUSTERED 
                            (
	                            [FCode] ASC
                            )WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
                            ) ON [PRIMARY]                            
                            ";

                        new CommTableInfoBLL().ExecSql(sql);//创建部门临时表

                        //string YzglTempDept = "";
                        //string strPrec = "insert into YzglTempDept (FCode,FName) values(,)";
                        StringBuilder strInsertInfo = new StringBuilder();
                        foreach (var item in _msg.data)
                        {
                            string strTemp = $"insert into YzglTempDept (FCode,FName,FID) values('{item.deptId}','{item.deptName}','{item.parentId}');";
                            strInsertInfo.Append(strTemp);
                        }
                        new CommTableInfoBLL().ExecSql(strInsertInfo.ToString());//创建部门临时表

                        rs.ReMsg = "插入部门记录成功";
                        rs.Flag = true;
                        rs.DataInfo = "";
                    }
                    else
                    {
                        rs.ReMsg = "Err|记录数为0";
                    }
                }               
            }
            catch (Exception e)
            {
                rs.ReMsg = "Err|" + e.Message;
            }

            return rs;
        }


        /// <summary>
        /// 获取字典信息
        /// </summary>
        /// <returns></returns>
        public ResultInfo GetDictDataList(string dicts)
        {
            Console.WriteLine("正在字典信息......");

            string subUrl = $"api/getDictDataList";
            int pageSize = 50000;
            int pageNum = 1;

            ResultInfo rs = new ResultInfo();

            char ee = (char)44;//逗号分隔
            string[] _subDicts = dicts.Split(ee);

            //获取银企直连配置参数
            //var setting = new ConfigHelper().GetYinQiSetting();

            //删除现有字典数据表
            string sql = @"IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[YzglTempDictCode]') AND type in (N'U'))
                            DROP TABLE [dbo].YzglTempDictCode                            

                            CREATE TABLE [dbo].[YzglTempDictCode](
	                            [dictLabel] [varchar](100) NOT NULL,
	                            [dictValue] [varchar](50) NULL,
	                            [dictType] [varchar](20) NULL,
	                            [parentCode] [varchar](50) NULL
                            ) ON [PRIMARY]                           
                            ";

            new CommTableInfoBLL().ExecSql(sql);//创建部门临时表

            //循环
            foreach (var subDict in _subDicts)
            {
                try
                {
                    subUrl = $"api/getDictDataList?pageNum={pageNum}&pageSize={pageSize}&dictType={subDict}";

                    string _res = HttpHelper.HttpPostByJson(_baseurl + subUrl, "", token);

                    ReqMsg<SysDictData> _msg = Newtonsoft.Json.JsonConvert.DeserializeObject<ReqMsg<SysDictData>>(_res);
                    if (_msg.code == 0)
                    {
                        if (_msg.data.Count > 0)
                        {
                            StringBuilder strInsertInfo = new StringBuilder();
                            foreach (var item in _msg.data)
                            {
                                string strTemp = $"insert into YzglTempDictCode (dictLabel,dictValue,dictType,parentCode) values('{item.dictLabel}','{item.dictValue}','{item.dictType}','{item.parentCode}');";
                                strInsertInfo.Append(strTemp);
                            }
                            new CommTableInfoBLL().ExecSql(strInsertInfo.ToString());//创建部门临时表

                            rs.ReMsg = $"插入字典{subDict}代码记录成功";
                            rs.Flag = true;
                            rs.DataInfo = "";
                        }
                        else
                        {
                            rs.ReMsg = "Err|记录数为0";
                        }
                    }
                }
                catch (Exception e)
                {
                    rs.ReMsg = "Err|" + e.Message;
                }
                //显示执行字典的情况
                Console.WriteLine(rs.ReMsg);
            }

            

            return rs;
        }

        #region 获取罪犯信息
        /// <summary>
        /// 获取罪犯信息
        /// </summary>
        /// <returns></returns>
        public ResultInfo GetJbxxList(string zfzt)
        {
            Console.WriteLine("正在获取人员基本信息......");

            ResultInfo rs = new ResultInfo();


            char ee = (char)44;//逗号分隔
            string[] _subZfzts = zfzt.Split(ee);



            //if (!string.IsNullOrWhiteSpace(token))
            if (true)
            {
                string subUrl = $"api/getJbxxList";

                int pageSize = 3000;
                int pageNum = 1;
                //获取银企直连配置参数
                //var setting = new ConfigHelper().GetYinQiSetting();

                try
                {
                    string sql = @"IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[YzglTempJbxx]') AND type in (N'U'))
	                        Begin
		                        DROP TABLE [YzglTempJbxx]
	                        end
	                        ";
                    new CommTableInfoBLL().ExecSql(sql);//删除人员临时表

                    sql = @"CREATE TABLE [YzglTempJbxx](
	                            [FCode] [varchar](20) NOT NULL,
	                            [FName] [varchar](50) NULL,
	                            [FIdenNo] [varchar](20) NULL,
	                            [FAge] [int] NULL,
	                            [FSex] [varchar](8) NULL,
	                            [FAddr] [varchar](256) NULL,
	                            [FCrimeCode] [varchar](60) NULL,
	                            [FCYCode] [varchar](10) NULL,
	                            [FTerm] [varchar](20) NULL,
	                            [FInDate] [datetime] NULL,
	                            [FOuDate] [datetime] NULL,
	                            [FAreaCode] [varchar](100) NULL,
	                            [gz] [varchar](100) NULL,
	                            [gw] [varchar](100) NULL,
								[zfZt] [varchar](20) NULL,
								[jtqh] [varchar](20) NULL,
                             CONSTRAINT [PK_YzglTempJbxx] PRIMARY KEY CLUSTERED 
                            (
	                            [FCode] ASC
                            )WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
                            ) ON [PRIMARY]                            
                            ";

                    new CommTableInfoBLL().ExecSql(sql);//创建人员临时表

                    foreach (var subZfzt in _subZfzts)
                    {
                        //先查询一次
                        ReqResultInfo<YzglzfdkJbxx> _result = PostGetJbxx(ref subUrl, rs, pageSize, pageNum, subZfzt);

                        //接着根据总条数，进行循环，直到全部获取完成
                        while (rs.Flag == true && _result.total > pageNum * pageSize)
                        {
                            pageNum++;
                            PostGetJbxx(ref subUrl, rs, pageSize, pageNum, subZfzt);
                        }
                    }

                    //更新地址信息
                    sql = @"update YzglTempJbxx set FAddr= (select top 1 dictLabel from [dbo].[YzglTempDictCode] where dictType='sys_gjxzqh' and dictValue=a.jtqh)+ a.FAddr
                        FROM [dbo].[YzglTempJbxx] a,YzglTempDictCode b where a.jtqh=b.dictValue and b.dictType='sys_gjxzqh' and isnull(a.jtqh,'')<>''";
                    new CommTableInfoBLL().ExecSql(sql);//更新社会关系家庭地址临时表
                }
                catch (Exception e)
                {
                    rs.ReMsg = "Err|" + e.Message;
                    //Log4NetHelper.logger.Error(rs.ReMsg);

                    Console.WriteLine(rs.ReMsg);
                }
            }
            else
            {
                rs.ReMsg = "token不能为空";
            }

            Console.WriteLine(rs.ReMsg);
            return rs;

        }

        private ReqResultInfo<YzglzfdkJbxx> PostGetJbxx(ref string subUrl, ResultInfo rs, int pageSize, int pageNum,string zfzt="")
        {
            subUrl = $"api/getJbxxList?pageNum={pageNum}&pageSize={pageSize}&zfzt={zfzt}";
            string _res = HttpHelper.HttpPostByJson(_baseurl + subUrl, "", token);

            ReqResultInfo<YzglzfdkJbxx> _result = Newtonsoft.Json.JsonConvert.DeserializeObject<ReqResultInfo<YzglzfdkJbxx>>(_res);

            var sst = _result.rows.Select(o => o.zfxm).ToList();
            string tempstr = "";
            foreach (var item in sst)
            {
                tempstr += item + "\r\n";
            }
            //string YzglTempDept = "";
            //string strPrec = "insert into YzglTempDept (FCode,FName) values(,)";
            StringBuilder strInsertInfo = new StringBuilder();
            if (_result.rows.Count > 0)
            {
                foreach (var item in _result.rows)
                {
                    string sex = "男";
                    if (item.xb == "2")
                    {
                        sex = "女";
                    }
                    string strTemp = $"insert into YzglTempJbxx (FCode,FName,FIdenNo,FAge,FSex,FAddr,FCrimeCode,FCYCode,FTerm,FInDate,FOuDate,FAreaCode, gz , gw,zfZt,jtqh) values('{item.zfbh}','{item.zfxm}','{item.zjHm}','{0}','{sex}','{item.jtmx}','{item.syzm}','{item.fgdj}','{item.xq}','{item.rjrq}','{item.xqzr}','{item.deptId}', '{item.gz}' , '{item.gw}', '{item.zfZt}','{item.jtqh}');";
                    strInsertInfo.Append(strTemp);
                }
                new CommTableInfoBLL().ExecSql(strInsertInfo.ToString());//创建部门临时表

                rs.ReMsg = "插入人员记录成功";
                rs.Flag = true;
                rs.DataInfo = "";
            }
            else
            {
                rs.ReMsg = "Err|记录数为0";
            }

            return _result;
        }

        #endregion




        #region 获取社会关系信息
        /// <summary>
        /// 获取罪犯信息
        /// </summary>
        /// <returns></returns>
        public ResultInfo GetShgxList()
        {
            Console.WriteLine("正在获取社会关系信息......");

            ResultInfo rs = new ResultInfo();
                        
            //if (!string.IsNullOrWhiteSpace(token))
            if (true)
            {
                string subUrl = $"api/getShgx";

                int pageSize = 3000;
                int pageNum = 1;
                //获取银企直连配置参数
                //var setting = new ConfigHelper().GetYinQiSetting();

                try
                {
                    string sql = @"IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[YzglTempShgx]') AND type in (N'U'))
	                        Begin
		                        DROP TABLE [YzglTempShgx]
	                        end
	                        ";
                    new CommTableInfoBLL().ExecSql(sql);//删除人员临时表

                    sql = @"CREATE TABLE [YzglTempShgx](
                                [id] [varchar](20) NOT NULL,
	                            [zfbh] [varchar](20) NOT NULL,
	                            [FamilyName] [varchar](50) NULL,
	                            [FSex] [varchar](8) NULL,
                                [FRelation] [varchar](100) NULL,
	                            [FIdenNo] [varchar](20) NULL,
	                            [FBirthday] [varchar](20) NULL,	                            
	                            [FAddr] [varchar](256) NULL,
                                [jtqh] [varchar](50) NULL,
                                [FTel] [varchar](50) NULL
                            ) ON [PRIMARY]                            
                            ";

                    new CommTableInfoBLL().ExecSql(sql);//创建人员临时表

                    //先查询一次
                    ReqResultInfo<YzglzfdkShgx> _result = PostGetShgx(ref subUrl, rs, pageSize, pageNum);

                    //接着根据总条数，进行循环，直到全部获取完成
                    while (rs.Flag == true && _result.total > pageNum * pageSize)
                    {
                        pageNum++;
                        PostGetShgx(ref subUrl, rs, pageSize, pageNum);
                    }

                    //更新地址信息
                    sql = @"update YzglTempShgx set FAddr= (select top 1 dictLabel from [dbo].[YzglTempDictCode] where dictType='sys_gjxzqh' and dictValue=a.jtqh)+ a.FAddr
                        FROM [dbo].[YzglTempShgx] a,YzglTempDictCode b where a.jtqh=b.dictValue and b.dictType='sys_gjxzqh' and isnull(a.jtqh,'')<>''";
                    new CommTableInfoBLL().ExecSql(sql);//更新社会关系家庭地址临时表

                }
                catch (Exception e)
                {
                    rs.ReMsg = "Err|" + e.Message;
                    //Log4NetHelper.logger.Error(rs.ReMsg);

                    Console.WriteLine(rs.ReMsg);
                }
            }
            else
            {
                rs.ReMsg = "token不能为空";
            }

            Console.WriteLine(rs.ReMsg);
            return rs;

        }

        private ReqResultInfo<YzglzfdkShgx> PostGetShgx(ref string subUrl, ResultInfo rs, int pageSize, int pageNum)
        {
            subUrl = $"api/getShgx?pageNum={pageNum}&pageSize={pageSize}";
            string _res = HttpHelper.HttpPostByJson(_baseurl + subUrl, "", token);

            ReqResultInfo<YzglzfdkShgx> _result = Newtonsoft.Json.JsonConvert.DeserializeObject<ReqResultInfo<YzglzfdkShgx>>(_res);

            var sst = _result.rows.Select(o => o.zfxm).ToList();
            string tempstr = "";
            foreach (var item in sst)
            {
                tempstr += item + "\r\n";
            }
            //string YzglTempDept = "";
            //string strPrec = "insert into YzglTempDept (FCode,FName) values(,)";
            StringBuilder strInsertInfo = new StringBuilder();
            if (_result.rows.Count > 0)
            {
                foreach (var item in _result.rows)
                {
                    string sex = "男";
                    if (item.xb == "2")
                    {
                        sex = "女";
                    }
                    string strTemp = $"insert into YzglTempShgx (id,zfbh,FamilyName,FSex,FRelation,FIdenNo,FBirthday,FAddr,jtqh,FTel) values('{item.id}','{item.zfbh}','{item.xm}','{sex}','{item.gxlb}','{item.zjhm}','{item.csrq}','{item.jtmx}','{item.jtqh}','{item.dh}');";
                    strInsertInfo.Append(strTemp);
                }
                new CommTableInfoBLL().ExecSql(strInsertInfo.ToString());//创建部门临时表

                

                rs.ReMsg = "插入社会关系记录成功";
                rs.Flag = true;
                rs.DataInfo = "";
            }
            else
            {
                rs.ReMsg = "Err|记录数为0";
            }

            return _result;
        }

        #endregion


        /// <summary>
        /// 获取历次判决对象
        /// </summary>
        /// <returns></returns>
        public ResultInfo GetZuifanLcpjList()
        {
            string subUrl = $"api/getFjjnList";
            ResultInfo rs = new ResultInfo();
            int pageSize = 100;
            int pageNum = 1;
            //获取银企直连配置参数
            //var setting = new ConfigHelper().GetYinQiSetting();

            try
            {


                string sql = @"IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[YzglTempJbxx]') AND type in (N'U'))
                            DROP TABLE [dbo].[YzglTempJbxx]
                            GO

                            CREATE TABLE [dbo].[YzglTempJbxx](
	                            [FCode] [varchar](20) NOT NULL,
	                            [FName] [varchar](50) NULL,
	                            [FIdenNo] [varchar](20) NULL,
	                            [FAge] [int] NULL,
	                            [FSex] [varchar](8) NULL,
	                            [FAddr] [varchar](128) NULL,
	                            [FCrimeCode] [varchar](10) NULL,
	                            [FCYCode] [varchar](4) NULL,
	                            [FTerm] [varchar](20) NULL,
	                            [FInDate] [datetime] NULL,
	                            [FOuDate] [datetime] NULL,
	                            [FAreaCode] [varchar](10) NULL,
	                            [gz] [varchar](100) NULL,
	                            [gw] [varchar](100) NULL,
                             CONSTRAINT [PK_YzglTempJbxx] PRIMARY KEY CLUSTERED 
                            (
	                            [FCode] ASC
                            )WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
                            ) ON [PRIMARY]
                            ";

                new CommTableInfoBLL().ExecSql(sql);//创建人员临时表


                //先查询一次
                ReqResultInfo<YzglzfdkJbxx> _result = PostGetLcpj(ref subUrl, rs, pageSize, pageNum);
                //接着根据总条数，进行循环，直到全部获取完成
                while (rs.Flag == true && _result.total > pageNum * pageSize)
                {
                    pageNum++;
                    PostGetJbxx(ref subUrl, rs, pageSize, pageNum);
                }

            }
            catch (Exception e)
            {
                rs.ReMsg = "Err|" + e.Message;
            }

            return rs;
        }

        private ReqResultInfo<YzglzfdkJbxx> PostGetLcpj(ref string subUrl, ResultInfo rs, int pageSize, int pageNum)
        {
            subUrl = $"{subUrl}?pageNum={pageNum}&pageSize={pageSize}";
            string _res = HttpHelper.HttpPostByJson(_baseurl + subUrl, "", token);

            ReqResultInfo<YzglzfdkJbxx> _result = Newtonsoft.Json.JsonConvert.DeserializeObject<ReqResultInfo<YzglzfdkJbxx>>(_res);

            //string YzglTempDept = "";
            //string strPrec = "insert into YzglTempDept (FCode,FName) values(,)";
            StringBuilder strInsertInfo = new StringBuilder();
            if (_result.rows.Count > 0)
            {
                foreach (var item in _result.rows)
                {
                    string sex = "男";
                    if (item.xb == "1")
                    {
                        sex = "女";
                    }
                    string strTemp = $"insert into YzglTempJbxx (FCode,FName,FIdenNo,FAge,FSex,FAddr,FCrimeCode,FCYCode,FTerm,FInDate,FOuDate,FAreaCode, gz , gw) values({item.zfbh},{item.zfxm},{item.zjHm},{0},{sex},{item.jtmx},{item.syzm},{item.fgdj},{item.xq},{item.rjrq},{item.xqzr},{item.deptId}, {item.gz} , {item.gw});";
                    strInsertInfo.Append(strTemp);
                }
                new CommTableInfoBLL().ExecSql(strInsertInfo.ToString());//创建部门临时表

                rs.ReMsg = "插入人员记录成功";
                rs.Flag = true;
                rs.DataInfo = "";
            }
            else
            {
                rs.ReMsg = "Err|记录数为0";
            }

            return _result;
        }
    }
}
