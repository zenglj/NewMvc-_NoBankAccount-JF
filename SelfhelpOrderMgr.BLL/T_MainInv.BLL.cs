using System; 
using System.Text;
using System.Collections.Generic; 
using System.Data;
using SelfhelpOrderMgr.Model;
namespace SelfhelpOrderMgr.BLL {
	 	//T_MainInv
		public partial class T_MainInvBLL
	{
   		     
		private readonly SelfhelpOrderMgr.DAL.T_MainInvDAL dal=new SelfhelpOrderMgr.DAL.T_MainInvDAL();
		public T_MainInvBLL()
		{}
		
		#region  Method


		/// <summary>
		/// 增加一条数据
		/// </summary>
		public void  Add(SelfhelpOrderMgr.Model.T_MainInv model)
		{
						dal.Add(model);
						
		}

		/// <summary>
		/// 更新一条数据
		/// </summary>
		public bool Update(SelfhelpOrderMgr.Model.T_MainInv model)
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
		public SelfhelpOrderMgr.Model.T_MainInv GetModel()
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
		public List<SelfhelpOrderMgr.Model.T_MainInv> GetModelList(string strWhere)
		{
			DataSet ds = dal.GetList(strWhere);
			return DataTableToList(ds.Tables[0]);
		}
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public List<SelfhelpOrderMgr.Model.T_MainInv> DataTableToList(DataTable dt)
		{
			List<SelfhelpOrderMgr.Model.T_MainInv> modelList = new List<SelfhelpOrderMgr.Model.T_MainInv>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				SelfhelpOrderMgr.Model.T_MainInv model;
				for (int n = 0; n < rowsCount; n++)
				{
					model = new SelfhelpOrderMgr.Model.T_MainInv();					
																	model.fsn= dt.Rows[n]["fsn"].ToString();
																																model.fareacode= dt.Rows[n]["fareacode"].ToString();
																																model.fsubareacode= dt.Rows[n]["fsubareacode"].ToString();
																																model.fAgent= dt.Rows[n]["fAgent"].ToString();
																												if(dt.Rows[n]["fOrderDate"].ToString()!="")
				{
					model.fOrderDate=DateTime.Parse(dt.Rows[n]["fOrderDate"].ToString());
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