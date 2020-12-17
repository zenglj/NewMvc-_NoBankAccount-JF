using System; 
using System.Text;
using System.Collections.Generic; 
using System.Data;
using SelfhelpOrderMgr.Model;
namespace SelfhelpOrderMgr.BLL {
	 	//T_RECORD_INFO
		public partial class T_RECORD_INFOBLL
	{
   		     
		private readonly SelfhelpOrderMgr.DAL.T_RECORD_INFODAL dal=new SelfhelpOrderMgr.DAL.T_RECORD_INFODAL();
		public T_RECORD_INFOBLL()
		{}
		
		#region  Method


		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int  Add(SelfhelpOrderMgr.Model.T_RECORD_INFO model)
		{
						return dal.Add(model);
						
		}

		/// <summary>
		/// 更新一条数据
		/// </summary>
		public bool Update(SelfhelpOrderMgr.Model.T_RECORD_INFO model)
		{
			return dal.Update(model);
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public bool Delete(int SEQ)
		{
			
			return dal.Delete(SEQ);
		}
				/// <summary>
		/// 批量删除一批数据
		/// </summary>
		public bool DeleteList(string SEQlist )
		{
			return dal.DeleteList(SEQlist );
		}
		
		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public SelfhelpOrderMgr.Model.T_RECORD_INFO GetModel(int SEQ)
		{
			
			return dal.GetModel(SEQ);
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
		public List<SelfhelpOrderMgr.Model.T_RECORD_INFO> GetModelList(string strWhere)
		{
			DataSet ds = dal.GetList(strWhere);
			return DataTableToList(ds.Tables[0]);
		}
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public List<SelfhelpOrderMgr.Model.T_RECORD_INFO> DataTableToList(DataTable dt)
		{
			List<SelfhelpOrderMgr.Model.T_RECORD_INFO> modelList = new List<SelfhelpOrderMgr.Model.T_RECORD_INFO>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				SelfhelpOrderMgr.Model.T_RECORD_INFO model;
				for (int n = 0; n < rowsCount; n++)
				{
					model = new SelfhelpOrderMgr.Model.T_RECORD_INFO();					
													if(dt.Rows[n]["SEQ"].ToString()!="")
				{
					model.SEQ=int.Parse(dt.Rows[n]["SEQ"].ToString());
				}
																																				model.FCode= dt.Rows[n]["FCode"].ToString();
																																model.BILLNO= dt.Rows[n]["BILLNO"].ToString();
																																model.FILEPATH= dt.Rows[n]["FILEPATH"].ToString();
																																model.PNO= dt.Rows[n]["PNO"].ToString();
																												if(dt.Rows[n]["WNO"].ToString()!="")
				{
					model.WNO=int.Parse(dt.Rows[n]["WNO"].ToString());
				}
																																if(dt.Rows[n]["TYPE"].ToString()!="")
				{
					model.TYPE=int.Parse(dt.Rows[n]["TYPE"].ToString());
				}
																																if(dt.Rows[n]["DUAL"].ToString()!="")
				{
					model.DUAL=int.Parse(dt.Rows[n]["DUAL"].ToString());
				}
																																if(dt.Rows[n]["STARTTIME"].ToString()!="")
				{
					model.STARTTIME=DateTime.Parse(dt.Rows[n]["STARTTIME"].ToString());
				}
																																if(dt.Rows[n]["STARTDATE"].ToString()!="")
				{
					model.STARTDATE=DateTime.Parse(dt.Rows[n]["STARTDATE"].ToString());
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