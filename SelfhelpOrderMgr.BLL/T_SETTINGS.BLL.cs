using System; 
using System.Text;
using System.Collections.Generic; 
using System.Data;
using SelfhelpOrderMgr.Model;
namespace SelfhelpOrderMgr.BLL {
	 	//T_SETTINGS
		public partial class T_SETTINGSBLL
	{
   		     
		private readonly SelfhelpOrderMgr.DAL.T_SETTINGSDAL dal=new SelfhelpOrderMgr.DAL.T_SETTINGSDAL();
		public T_SETTINGSBLL()
		{}
		
		#region  Method


		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int  Add(SelfhelpOrderMgr.Model.T_SETTINGS model)
		{
						return dal.Add(model);
						
		}

		/// <summary>
		/// 更新一条数据
		/// </summary>
		public bool Update(SelfhelpOrderMgr.Model.T_SETTINGS model)
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
		public SelfhelpOrderMgr.Model.T_SETTINGS GetModel(int SEQ)
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
		public List<SelfhelpOrderMgr.Model.T_SETTINGS> GetModelList(string strWhere)
		{
			DataSet ds = dal.GetList(strWhere);
			return DataTableToList(ds.Tables[0]);
		}
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public List<SelfhelpOrderMgr.Model.T_SETTINGS> DataTableToList(DataTable dt)
		{
			List<SelfhelpOrderMgr.Model.T_SETTINGS> modelList = new List<SelfhelpOrderMgr.Model.T_SETTINGS>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				SelfhelpOrderMgr.Model.T_SETTINGS model;
				for (int n = 0; n < rowsCount; n++)
				{
					model = new SelfhelpOrderMgr.Model.T_SETTINGS();					
													if(dt.Rows[n]["SEQ"].ToString()!="")
				{
					model.SEQ=int.Parse(dt.Rows[n]["SEQ"].ToString());
				}
																																				model.NAME= dt.Rows[n]["NAME"].ToString();
																																model.VALUE= dt.Rows[n]["VALUE"].ToString();
																												if(dt.Rows[n]["TYPE"].ToString()!="")
				{
					model.TYPE=int.Parse(dt.Rows[n]["TYPE"].ToString());
				}
																																				model.remark= dt.Rows[n]["remark"].ToString();
																						
				
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