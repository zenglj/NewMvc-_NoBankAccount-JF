using System; 
using System.Text;
using System.Collections.Generic; 
using System.Data;
using SelfhelpOrderMgr.Model;
namespace SelfhelpOrderMgr.BLL {
	 	//T_SHO_AreaGoodMaxCount
		public partial class T_SHO_AreaGoodMaxCountBLL
	{
   		     
		private readonly SelfhelpOrderMgr.DAL.T_SHO_AreaGoodMaxCountDAL dal=new SelfhelpOrderMgr.DAL.T_SHO_AreaGoodMaxCountDAL();
		public T_SHO_AreaGoodMaxCountBLL()
		{}
		
		#region  Method
		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(string FAreaCode,string FGtxm)
		{
			return dal.Exists(FAreaCode,FGtxm);
		}

		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int  Add(SelfhelpOrderMgr.Model.T_SHO_AreaGoodMaxCount model)
		{
						return dal.Add(model);
						
		}

		/// <summary>
		/// 更新一条数据
		/// </summary>
		public bool Update(SelfhelpOrderMgr.Model.T_SHO_AreaGoodMaxCount model)
		{
			return dal.Update(model);
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public bool Delete(int Id)
		{
			
			return dal.Delete(Id);
		}
				/// <summary>
		/// 批量删除一批数据
		/// </summary>
		public bool DeleteList(string Idlist )
		{
			return dal.DeleteList(Idlist );
		}
		
		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public SelfhelpOrderMgr.Model.T_SHO_AreaGoodMaxCount GetModel(int Id)
		{
			
			return dal.GetModel(Id);
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
		public List<SelfhelpOrderMgr.Model.T_SHO_AreaGoodMaxCount> GetModelList(string strWhere)
		{
			DataSet ds = dal.GetList(strWhere);
			return DataTableToList(ds.Tables[0]);
		}
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public List<SelfhelpOrderMgr.Model.T_SHO_AreaGoodMaxCount> DataTableToList(DataTable dt)
		{
			List<SelfhelpOrderMgr.Model.T_SHO_AreaGoodMaxCount> modelList = new List<SelfhelpOrderMgr.Model.T_SHO_AreaGoodMaxCount>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				SelfhelpOrderMgr.Model.T_SHO_AreaGoodMaxCount model;
				for (int n = 0; n < rowsCount; n++)
				{
					model = new SelfhelpOrderMgr.Model.T_SHO_AreaGoodMaxCount();					
													if(dt.Rows[n]["Id"].ToString()!="")
				{
					model.Id=int.Parse(dt.Rows[n]["Id"].ToString());
				}
																																				model.FAreaCode= dt.Rows[n]["FAreaCode"].ToString();
																																model.FAreaName= dt.Rows[n]["FAreaName"].ToString();
																																model.FGtxm= dt.Rows[n]["FGtxm"].ToString();
																																model.FGoodName= dt.Rows[n]["FGoodName"].ToString();
																																model.FGoodType= dt.Rows[n]["FGoodType"].ToString();
																												if(dt.Rows[n]["FGoodMaxCount"].ToString()!="")
				{
					model.FGoodMaxCount=int.Parse(dt.Rows[n]["FGoodMaxCount"].ToString());
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