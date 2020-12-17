using System; 
using System.Text;
using System.Collections.Generic; 
using System.Data;
using SelfhelpOrderMgr.Model;
namespace SelfhelpOrderMgr.BLL {
	 	//T_CommonTypeTab
		public partial class T_CommonTypeTabBLL
	{
   		     
		private readonly SelfhelpOrderMgr.DAL.T_CommonTypeTabDAL dal=new SelfhelpOrderMgr.DAL.T_CommonTypeTabDAL();
		public T_CommonTypeTabBLL()
		{}
		
		#region  Method


		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int  Add(SelfhelpOrderMgr.Model.T_CommonTypeTab model)
		{
						return dal.Add(model);
						
		}

		/// <summary>
		/// 更新一条数据
		/// </summary>
		public bool Update(SelfhelpOrderMgr.Model.T_CommonTypeTab model)
		{
			return dal.Update(model);
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public bool Delete(int ID)
		{
			
			return dal.Delete(ID);
		}
				/// <summary>
		/// 批量删除一批数据
		/// </summary>
		public bool DeleteList(string IDlist )
		{
			return dal.DeleteList(IDlist );
		}
		
		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public SelfhelpOrderMgr.Model.T_CommonTypeTab GetModel(int ID)
		{
			
			return dal.GetModel(ID);
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
		public List<SelfhelpOrderMgr.Model.T_CommonTypeTab> GetModelList(string strWhere)
		{
			DataSet ds = dal.GetList(strWhere);
			return DataTableToList(ds.Tables[0]);
		}
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public List<SelfhelpOrderMgr.Model.T_CommonTypeTab> DataTableToList(DataTable dt)
		{
			List<SelfhelpOrderMgr.Model.T_CommonTypeTab> modelList = new List<SelfhelpOrderMgr.Model.T_CommonTypeTab>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				SelfhelpOrderMgr.Model.T_CommonTypeTab model;
				for (int n = 0; n < rowsCount; n++)
				{
					model = new SelfhelpOrderMgr.Model.T_CommonTypeTab();					
													if(dt.Rows[n]["ID"].ToString()!="")
				{
					model.ID=int.Parse(dt.Rows[n]["ID"].ToString());
				}
																																				model.FType= dt.Rows[n]["FType"].ToString();
																																model.FCode= dt.Rows[n]["FCode"].ToString();
																																model.FName= dt.Rows[n]["FName"].ToString();
																																model.FRemark= dt.Rows[n]["FRemark"].ToString();
																						
				
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