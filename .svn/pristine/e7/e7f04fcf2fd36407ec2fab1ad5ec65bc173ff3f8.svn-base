using System; 
using System.Text;
using System.Collections.Generic; 
using System.Data;
using SelfhelpOrderMgr.Model;
namespace SelfhelpOrderMgr.BLL {
	 	//T_TreeArea
		public partial class T_TreeAreaBLL
	{
   		     
		private readonly SelfhelpOrderMgr.DAL.T_TreeAreaDAL dal=new SelfhelpOrderMgr.DAL.T_TreeAreaDAL();
		public T_TreeAreaBLL()
		{}
		
		#region  Method


		/// <summary>
		/// 增加一条数据
		/// </summary>
		public void  Add(SelfhelpOrderMgr.Model.T_TreeArea model)
		{
						dal.Add(model);
						
		}

		/// <summary>
		/// 更新一条数据
		/// </summary>
		public bool Update(SelfhelpOrderMgr.Model.T_TreeArea model)
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
		public SelfhelpOrderMgr.Model.T_TreeArea GetModel()
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
		public List<SelfhelpOrderMgr.Model.T_TreeArea> GetModelList(string strWhere)
		{
			DataSet ds = dal.GetList(strWhere);
			return DataTableToList(ds.Tables[0]);
		}
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public List<SelfhelpOrderMgr.Model.T_TreeArea> DataTableToList(DataTable dt)
		{
			List<SelfhelpOrderMgr.Model.T_TreeArea> modelList = new List<SelfhelpOrderMgr.Model.T_TreeArea>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				SelfhelpOrderMgr.Model.T_TreeArea model;
				for (int n = 0; n < rowsCount; n++)
				{
					model = new SelfhelpOrderMgr.Model.T_TreeArea();					
																	model.ID= dt.Rows[n]["ID"].ToString();
																																model.Text= dt.Rows[n]["Text"].ToString();
																																model.FID= dt.Rows[n]["FID"].ToString();
																																model.fmeetdate= dt.Rows[n]["fmeetdate"].ToString();
																																model.URL= dt.Rows[n]["URL"].ToString();
																						
				
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