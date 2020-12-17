using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;

namespace SelfhelpOrderMgr.DAL
{
    public partial class T_Criminal_ChangeListDAL
    {
        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(SelfhelpOrderMgr.Model.T_Criminal_ChangeList model,int flag)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into T_Criminal_ChangeList(");
            strSql.Append("ChangeInfo,CrtBy,CrtDate,AuditBy,AuditArea,AuditInfo,AuditFlag,Remark,FCode,FName,ChangeType,ChangeTypeName,OldCode,OldName,NewCode,NewName");
            strSql.Append(") values (");
            strSql.Append("@ChangeInfo,@CrtBy,@CrtDate,@AuditBy,@AuditArea,@AuditInfo,@AuditFlag,@Remark,@FCode,@FName,@ChangeType,@ChangeTypeName,@OldCode,@OldName,@NewCode,@NewName");
            strSql.Append(") ");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
			            new SqlParameter("@ChangeInfo", SqlDbType.VarChar,100) ,            
                        new SqlParameter("@CrtBy", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@CrtDate", SqlDbType.DateTime) ,            
                        new SqlParameter("@AuditBy", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@AuditArea", SqlDbType.VarChar,50) ,                        
                        new SqlParameter("@AuditInfo", SqlDbType.VarChar,100) ,            
                        new SqlParameter("@AuditFlag", SqlDbType.Int,4) ,            
                        new SqlParameter("@Remark", SqlDbType.VarChar,100) ,            
                        new SqlParameter("@FCode", SqlDbType.VarChar,20) ,            
                        new SqlParameter("@FName", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@ChangeType", SqlDbType.VarChar,20) ,            
                        new SqlParameter("@ChangeTypeName", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@OldCode", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@OldName", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@NewCode", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@NewName", SqlDbType.VarChar,50)             
              
            };

            parameters[0].Value = model.ChangeInfo;
            parameters[1].Value = model.CrtBy;
            parameters[2].Value = model.CrtDate;
            parameters[3].Value = model.AuditBy;
            parameters[4].Value = model.AuditArea;
            //parameters[5].Value = model.AuditDate;
            parameters[5].Value = model.AuditInfo;
            parameters[6].Value = model.AuditFlag;
            parameters[7].Value = model.Remark;
            parameters[8].Value = model.FCode;
            parameters[9].Value = model.FName;
            parameters[10].Value = model.ChangeType;
            parameters[11].Value = model.ChangeTypeName;
            parameters[12].Value = model.OldCode;
            parameters[13].Value = model.OldName;
            parameters[14].Value = model.NewCode;
            parameters[15].Value = model.NewName;

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