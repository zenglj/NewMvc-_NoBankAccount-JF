using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;

namespace SelfhelpOrderMgr.DAL
{
    public partial class T_Goods_ChangeListDAL
    {
        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(SelfhelpOrderMgr.Model.T_Goods_ChangeList model,int flag)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into T_Goods_ChangeList(");
            strSql.Append("CrtBy,CrtDate,Remark,GCode,GName,GTXM,ChangeType,ChangeTypeName,OldPrice,NewPrice,ChangeInfo");
            strSql.Append(") values (");
            strSql.Append("@CrtBy,@CrtDate,@Remark,@GCode,@GName,@GTXM,@ChangeType,@ChangeTypeName,@OldPrice,@NewPrice,@ChangeInfo");
            strSql.Append(") ");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
			            new SqlParameter("@CrtBy", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@CrtDate", SqlDbType.DateTime) ,            
                        //new SqlParameter("@AuditBy", SqlDbType.VarChar,50) ,            
                        //new SqlParameter("@AuditArea", SqlDbType.VarChar,50) ,            
                        //new SqlParameter("@AuditDate", SqlDbType.DateTime) ,            
                        //new SqlParameter("@AuditInfo", SqlDbType.VarChar,100) ,            
                        //new SqlParameter("@AuditFlag", SqlDbType.Int,4) ,            
                        new SqlParameter("@Remark", SqlDbType.VarChar,100) ,            
                        new SqlParameter("@GCode", SqlDbType.VarChar,20) ,            
                        new SqlParameter("@GName", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@GTXM", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@ChangeType", SqlDbType.VarChar,20) ,            
                        new SqlParameter("@ChangeTypeName", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@OldPrice", SqlDbType.Decimal,9) ,            
                        new SqlParameter("@NewPrice", SqlDbType.Decimal,9) ,            
                        new SqlParameter("@ChangeInfo", SqlDbType.VarChar,100)             
              
            };

            parameters[0].Value = model.CrtBy;
            parameters[1].Value = model.CrtDate;
            //parameters[2].Value = model.AuditBy;
            //parameters[3].Value = model.AuditArea;
            //parameters[4].Value = model.AuditDate;
            //parameters[5].Value = model.AuditInfo;
            //parameters[6].Value = model.AuditFlag;
            parameters[2].Value = model.Remark;
            parameters[3].Value = model.GCode;
            parameters[4].Value = model.GName;
            parameters[5].Value = model.GTXM;
            parameters[6].Value = model.ChangeType;
            parameters[7].Value = model.ChangeTypeName;
            parameters[8].Value = model.OldPrice;
            parameters[9].Value = model.NewPrice;
            parameters[10].Value = model.ChangeInfo;

            object obj = SqlHelper.GetSingle(strSql.ToString(), parameters);
            if (obj == null)
            {
                return 0;
            }
            else
            {

                return Convert.ToInt32(obj);

            }

        }

    }
}