using System; 
using System.Text;
using System.Collections.Generic; 
using System.Data;
using SelfhelpOrderMgr.Model;
namespace SelfhelpOrderMgr.BLL {
	 	//t_bankFeeList
		public partial class t_bankFeeListBLL
	{
   		     
		private readonly SelfhelpOrderMgr.DAL.t_bankFeeListDAL dal=new SelfhelpOrderMgr.DAL.t_bankFeeListDAL();
		public t_bankFeeListBLL()
		{}
		
		#region  Method


		/// <summary>
		/// 增加一条数据
		/// </summary>
		public void  Add(SelfhelpOrderMgr.Model.t_bankFeeList model)
		{
						dal.Add(model);
						
		}

		/// <summary>
		/// 更新一条数据
		/// </summary>
		public bool Update(SelfhelpOrderMgr.Model.t_bankFeeList model)
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
		public SelfhelpOrderMgr.Model.t_bankFeeList GetModel()
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
		public List<SelfhelpOrderMgr.Model.t_bankFeeList> GetModelList(string strWhere)
		{
			DataSet ds = dal.GetList(strWhere);
			return DataTableToList(ds.Tables[0]);
		}
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public List<SelfhelpOrderMgr.Model.t_bankFeeList> DataTableToList(DataTable dt)
		{
			List<SelfhelpOrderMgr.Model.t_bankFeeList> modelList = new List<SelfhelpOrderMgr.Model.t_bankFeeList>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				SelfhelpOrderMgr.Model.t_bankFeeList model;
				for (int n = 0; n < rowsCount; n++)
				{
					model = new SelfhelpOrderMgr.Model.t_bankFeeList();					
																	model.AccCode= dt.Rows[n]["AccCode"].ToString();
																												if(dt.Rows[n]["typeid"].ToString()!="")
				{
					model.typeid=int.Parse(dt.Rows[n]["typeid"].ToString());
				}
																																				model.typename= dt.Rows[n]["typename"].ToString();
																												if(dt.Rows[n]["subtypeid"].ToString()!="")
				{
					model.subtypeid=int.Parse(dt.Rows[n]["subtypeid"].ToString());
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