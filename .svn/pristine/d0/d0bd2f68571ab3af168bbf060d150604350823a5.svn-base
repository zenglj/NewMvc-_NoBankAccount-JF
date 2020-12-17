using System; 
using System.Text;
using System.Collections.Generic; 
using System.Data;
using SelfhelpOrderMgr.Model;
using SelfhelpOrderMgr.DAL;

namespace SelfhelpOrderMgr.BLL {
	 	//T_SHO_ManagerSet
		public partial class T_SHO_ManagerSetBLL
	{
   		     
		private readonly SelfhelpOrderMgr.DAL.T_SHO_ManagerSetDAL dal=new SelfhelpOrderMgr.DAL.T_SHO_ManagerSetDAL();
		public T_SHO_ManagerSetBLL()
		{}
		
		#region  Method
		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(string KeyName)
		{
			return dal.Exists(KeyName);
		}

		/// <summary>
		/// 增加一条数据
		/// </summary>
		public void  Add(SelfhelpOrderMgr.Model.T_SHO_ManagerSet model)
		{
						dal.Add(model);
						
		}

		/// <summary>
		/// 更新一条数据
		/// </summary>
		public bool Update(SelfhelpOrderMgr.Model.T_SHO_ManagerSet model)
		{
			return dal.Update(model);
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public bool Delete(string KeyName)
		{
			
			return dal.Delete(KeyName);
		}
		
		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public SelfhelpOrderMgr.Model.T_SHO_ManagerSet GetModel(string KeyName)
		{
			
			return dal.GetModel(KeyName);
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
		public List<SelfhelpOrderMgr.Model.T_SHO_ManagerSet> GetModelList(string strWhere)
		{
			DataSet ds = dal.GetList(strWhere);
			return DataTableToList(ds.Tables[0]);
		}
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public List<SelfhelpOrderMgr.Model.T_SHO_ManagerSet> DataTableToList(DataTable dt)
		{
			List<SelfhelpOrderMgr.Model.T_SHO_ManagerSet> modelList = new List<SelfhelpOrderMgr.Model.T_SHO_ManagerSet>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				SelfhelpOrderMgr.Model.T_SHO_ManagerSet model;
				for (int n = 0; n < rowsCount; n++)
				{
					model = new SelfhelpOrderMgr.Model.T_SHO_ManagerSet();					
																	model.KeyName= dt.Rows[n]["KeyName"].ToString();
																												if(dt.Rows[n]["KeyMode"].ToString()!="")
				{
					model.KeyMode=int.Parse(dt.Rows[n]["KeyMode"].ToString());
				}
																																				model.MgrName= dt.Rows[n]["MgrName"].ToString();
																																model.MgrValue= dt.Rows[n]["MgrValue"].ToString();
																												if(dt.Rows[n]["StartTime"].ToString()!="")
				{
					model.StartTime=DateTime.Parse(dt.Rows[n]["StartTime"].ToString());
				}
																																				model.Remark= dt.Rows[n]["Remark"].ToString();
																						
				
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