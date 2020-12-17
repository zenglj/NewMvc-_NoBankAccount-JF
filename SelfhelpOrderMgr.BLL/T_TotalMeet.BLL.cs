using System; 
using System.Text;
using System.Collections.Generic; 
using System.Data;
using SelfhelpOrderMgr.Model;
namespace SelfhelpOrderMgr.BLL {
	 	//T_TotalMeet
		public partial class T_TotalMeetBLL
	{
   		     
		private readonly SelfhelpOrderMgr.DAL.T_TotalMeetDAL dal=new SelfhelpOrderMgr.DAL.T_TotalMeetDAL();
		public T_TotalMeetBLL()
		{}
		
		#region  Method


		/// <summary>
		/// 增加一条数据
		/// </summary>
		public void  Add(SelfhelpOrderMgr.Model.T_TotalMeet model)
		{
						dal.Add(model);
						
		}

		/// <summary>
		/// 更新一条数据
		/// </summary>
		public bool Update(SelfhelpOrderMgr.Model.T_TotalMeet model)
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
		public SelfhelpOrderMgr.Model.T_TotalMeet GetModel()
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
		public List<SelfhelpOrderMgr.Model.T_TotalMeet> GetModelList(string strWhere)
		{
			DataSet ds = dal.GetList(strWhere);
			return DataTableToList(ds.Tables[0]);
		}
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public List<SelfhelpOrderMgr.Model.T_TotalMeet> DataTableToList(DataTable dt)
		{
			List<SelfhelpOrderMgr.Model.T_TotalMeet> modelList = new List<SelfhelpOrderMgr.Model.T_TotalMeet>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				SelfhelpOrderMgr.Model.T_TotalMeet model;
				for (int n = 0; n < rowsCount; n++)
				{
					model = new SelfhelpOrderMgr.Model.T_TotalMeet();					
																	model.Area= dt.Rows[n]["Area"].ToString();
																												if(dt.Rows[n]["mon1"].ToString()!="")
				{
					model.mon1=int.Parse(dt.Rows[n]["mon1"].ToString());
				}
																																if(dt.Rows[n]["mon2"].ToString()!="")
				{
					model.mon2=int.Parse(dt.Rows[n]["mon2"].ToString());
				}
																																if(dt.Rows[n]["mon3"].ToString()!="")
				{
					model.mon3=int.Parse(dt.Rows[n]["mon3"].ToString());
				}
																																if(dt.Rows[n]["mon4"].ToString()!="")
				{
					model.mon4=int.Parse(dt.Rows[n]["mon4"].ToString());
				}
																																if(dt.Rows[n]["mon5"].ToString()!="")
				{
					model.mon5=int.Parse(dt.Rows[n]["mon5"].ToString());
				}
																																if(dt.Rows[n]["mon6"].ToString()!="")
				{
					model.mon6=int.Parse(dt.Rows[n]["mon6"].ToString());
				}
																																if(dt.Rows[n]["mon7"].ToString()!="")
				{
					model.mon7=int.Parse(dt.Rows[n]["mon7"].ToString());
				}
																																if(dt.Rows[n]["mon8"].ToString()!="")
				{
					model.mon8=int.Parse(dt.Rows[n]["mon8"].ToString());
				}
																																if(dt.Rows[n]["mon9"].ToString()!="")
				{
					model.mon9=int.Parse(dt.Rows[n]["mon9"].ToString());
				}
																																if(dt.Rows[n]["mon10"].ToString()!="")
				{
					model.mon10=int.Parse(dt.Rows[n]["mon10"].ToString());
				}
																																if(dt.Rows[n]["mon11"].ToString()!="")
				{
					model.mon11=int.Parse(dt.Rows[n]["mon11"].ToString());
				}
																																if(dt.Rows[n]["mon12"].ToString()!="")
				{
					model.mon12=int.Parse(dt.Rows[n]["mon12"].ToString());
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