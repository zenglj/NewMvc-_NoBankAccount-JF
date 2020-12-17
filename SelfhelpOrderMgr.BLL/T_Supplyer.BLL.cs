using System; 
using System.Text;
using System.Collections.Generic; 
using System.Data;
using SelfhelpOrderMgr.Model;
namespace SelfhelpOrderMgr.BLL {
	 	//T_Supplyer
		public partial class T_SupplyerBLL
	{
   		     
		private readonly SelfhelpOrderMgr.DAL.T_SupplyerDAL dal=new SelfhelpOrderMgr.DAL.T_SupplyerDAL();
		public T_SupplyerBLL()
		{}
		
		#region  Method


		/// <summary>
		/// 增加一条数据
		/// </summary>
		public void  Add(SelfhelpOrderMgr.Model.T_Supplyer model)
		{
						dal.Add(model);
						
		}

		/// <summary>
		/// 更新一条数据
		/// </summary>
		public bool Update(SelfhelpOrderMgr.Model.T_Supplyer model)
		{
			return dal.Update(model);
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public bool Delete(string scode)
		{
			
			return dal.Delete(scode);
		}
		
		/// <summary>
		/// 得到一个对象实体
		/// </summary>
        public SelfhelpOrderMgr.Model.T_Supplyer GetModel(string scode)
		{
			
			return dal.GetModel(scode);
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
		public List<SelfhelpOrderMgr.Model.T_Supplyer> GetModelList(string strWhere)
		{
			DataSet ds = dal.GetList(strWhere);
			return DataTableToList(ds.Tables[0]);
		}
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public List<SelfhelpOrderMgr.Model.T_Supplyer> DataTableToList(DataTable dt)
		{
			List<SelfhelpOrderMgr.Model.T_Supplyer> modelList = new List<SelfhelpOrderMgr.Model.T_Supplyer>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				SelfhelpOrderMgr.Model.T_Supplyer model;
				for (int n = 0; n < rowsCount; n++)
				{
					model = new SelfhelpOrderMgr.Model.T_Supplyer();					
																	model.scode= dt.Rows[n]["scode"].ToString();
																																model.sName= dt.Rows[n]["sName"].ToString();
																																model.sAddress= dt.Rows[n]["sAddress"].ToString();
																																model.sTel= dt.Rows[n]["sTel"].ToString();
																																model.sFax= dt.Rows[n]["sFax"].ToString();
																																model.sAtten= dt.Rows[n]["sAtten"].ToString();
																																model.sAccountNo= dt.Rows[n]["sAccountNo"].ToString();
																																model.sBank= dt.Rows[n]["sBank"].ToString();
																						
				
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