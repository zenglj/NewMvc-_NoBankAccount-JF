using System; 
using System.Text;
using System.Collections.Generic; 
using System.Data;
using SelfhelpOrderMgr.Model;
namespace SelfhelpOrderMgr.BLL {
	 	//T_Vcrd_old
		public partial class T_Vcrd_oldBLL
	{
   		     
		private readonly SelfhelpOrderMgr.DAL.T_Vcrd_oldDAL dal=new SelfhelpOrderMgr.DAL.T_Vcrd_oldDAL();
		public T_Vcrd_oldBLL()
		{}
		
		#region  Method


		/// <summary>
		/// 增加一条数据
		/// </summary>
		public void  Add(SelfhelpOrderMgr.Model.T_Vcrd_old model)
		{
						dal.Add(model);
						
		}

		/// <summary>
		/// 更新一条数据
		/// </summary>
		public bool Update(SelfhelpOrderMgr.Model.T_Vcrd_old model)
		{
			return dal.Update(model);
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public bool Delete()
		{
			
			return dal.Delete();
		}
		
		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public SelfhelpOrderMgr.Model.T_Vcrd_old GetModel()
		{
			
			return dal.GetModel();
		}


		/// <summary>
		/// 获得数据列表
		/// </summary>
		public DataSet GetList(string strWhere)
		{
			return dal.GetList(strWhere);
		}
		/// <summary>
		/// 获得前几行数据
		/// </summary>
		public DataSet GetList(int Top,string strWhere,string filedOrder)
		{
			return dal.GetList(Top,strWhere,filedOrder);
		}
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public List<SelfhelpOrderMgr.Model.T_Vcrd_old> GetModelList(string strWhere)
		{
			DataSet ds = dal.GetList(strWhere);
			return DataTableToList(ds.Tables[0]);
		}
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public List<SelfhelpOrderMgr.Model.T_Vcrd_old> DataTableToList(DataTable dt)
		{
			List<SelfhelpOrderMgr.Model.T_Vcrd_old> modelList = new List<SelfhelpOrderMgr.Model.T_Vcrd_old>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				SelfhelpOrderMgr.Model.T_Vcrd_old model;
				for (int n = 0; n < rowsCount; n++)
				{
					model = new SelfhelpOrderMgr.Model.T_Vcrd_old();					
																	model.VOUNO= dt.Rows[n]["VOUNO"].ToString();
																																model.cardcode= dt.Rows[n]["cardcode"].ToString();
																																model.fcrimecode= dt.Rows[n]["fcrimecode"].ToString();
																												if(dt.Rows[n]["DAMOUNT"].ToString()!="")
				{
					model.DAMOUNT=decimal.Parse(dt.Rows[n]["DAMOUNT"].ToString());
				}
																																if(dt.Rows[n]["CAMOUNT"].ToString()!="")
				{
					model.CAMOUNT=decimal.Parse(dt.Rows[n]["CAMOUNT"].ToString());
				}
																																				model.crtBy= dt.Rows[n]["crtBy"].ToString();
																												if(dt.Rows[n]["CRTDATE"].ToString()!="")
				{
					model.CRTDATE=DateTime.Parse(dt.Rows[n]["CRTDATE"].ToString());
				}
																																				model.DTYPE= dt.Rows[n]["DTYPE"].ToString();
																																model.DEPOSITER= dt.Rows[n]["DEPOSITER"].ToString();
																																model.REMARK= dt.Rows[n]["REMARK"].ToString();
																												if(dt.Rows[n]["flag"].ToString()!="")
				{
					model.flag=int.Parse(dt.Rows[n]["flag"].ToString());
				}
																																				model.delby= dt.Rows[n]["delby"].ToString();
																												if(dt.Rows[n]["deldate"].ToString()!="")
				{
					model.deldate=DateTime.Parse(dt.Rows[n]["deldate"].ToString());
				}
																																				model.fareacode= dt.Rows[n]["fareacode"].ToString();
																																model.fareaName= dt.Rows[n]["fareaName"].ToString();
																																model.fcriminal= dt.Rows[n]["fcriminal"].ToString();
																																model.Frealareacode= dt.Rows[n]["Frealareacode"].ToString();
																																model.FrealAreaName= dt.Rows[n]["FrealAreaName"].ToString();
																																model.ptype= dt.Rows[n]["ptype"].ToString();
																												if(dt.Rows[n]["udate"].ToString()!="")
				{
					model.udate=DateTime.Parse(dt.Rows[n]["udate"].ToString());
				}
																																				model.origid= dt.Rows[n]["origid"].ToString();
																												if(dt.Rows[n]["cardtype"].ToString()!="")
				{
					model.cardtype=int.Parse(dt.Rows[n]["cardtype"].ToString());
				}
																																if(dt.Rows[n]["TYPEFLAG"].ToString()!="")
				{
					model.TYPEFLAG=int.Parse(dt.Rows[n]["TYPEFLAG"].ToString());
				}
																																if(dt.Rows[n]["acctype"].ToString()!="")
				{
					model.acctype=int.Parse(dt.Rows[n]["acctype"].ToString());
				}
																																if(dt.Rows[n]["senddate"].ToString()!="")
				{
					model.senddate=DateTime.Parse(dt.Rows[n]["senddate"].ToString());
				}
																																if(dt.Rows[n]["Bankflag"].ToString()!="")
				{
					model.Bankflag=int.Parse(dt.Rows[n]["Bankflag"].ToString());
				}
																																if(dt.Rows[n]["checkflag"].ToString()!="")
				{
					model.checkflag=int.Parse(dt.Rows[n]["checkflag"].ToString());
				}
																																if(dt.Rows[n]["checkdate"].ToString()!="")
				{
					model.checkdate=DateTime.Parse(dt.Rows[n]["checkdate"].ToString());
				}
																																				model.checkby= dt.Rows[n]["checkby"].ToString();
																												if(dt.Rows[n]["pc"].ToString()!="")
				{
					model.pc=int.Parse(dt.Rows[n]["pc"].ToString());
				}
																																if(dt.Rows[n]["seqno"].ToString()!="")
				{
					model.seqno=int.Parse(dt.Rows[n]["seqno"].ToString());
				}
																																if(dt.Rows[n]["subtypeflag"].ToString()!="")
				{
					model.subtypeflag=int.Parse(dt.Rows[n]["subtypeflag"].ToString());
				}
																																if(dt.Rows[n]["rcvdate"].ToString()!="")
				{
					model.rcvdate=DateTime.Parse(dt.Rows[n]["rcvdate"].ToString());
				}
																										
				
					modelList.Add(model);
				}
			}
			return modelList;
		}

		/// <summary>
		/// 获得数据列表
		/// </summary>
		public DataSet GetAllList()
		{
			return GetList("");
		}
#endregion
   
	}
}