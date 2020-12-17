using System; 
using System.Text;
using System.Collections.Generic; 
using System.Data;
using SelfhelpOrderMgr.Model;
namespace SelfhelpOrderMgr.BLL {
	 	//t_bank_regList
		public partial class t_bank_regListBLL
	{
   		     
		private readonly SelfhelpOrderMgr.DAL.t_bank_regListDAL dal=new SelfhelpOrderMgr.DAL.t_bank_regListDAL();
		public t_bank_regListBLL()
		{}
		
		#region  Method


		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int  Add(SelfhelpOrderMgr.Model.t_bank_regList model)
		{
						return dal.Add(model);
						
		}

		/// <summary>
		/// 更新一条数据
		/// </summary>
		public bool Update(SelfhelpOrderMgr.Model.t_bank_regList model)
		{
			return dal.Update(model);
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public bool Delete(int seqno)
		{
			
			return dal.Delete(seqno);
		}
				/// <summary>
		/// 批量删除一批数据
		/// </summary>
		public bool DeleteList(string seqnolist )
		{
			return dal.DeleteList(seqnolist );
		}
		
		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public SelfhelpOrderMgr.Model.t_bank_regList GetModel(int seqno)
		{
			
			return dal.GetModel(seqno);
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
		public List<SelfhelpOrderMgr.Model.t_bank_regList> GetModelList(string strWhere)
		{
			DataSet ds = dal.GetList(strWhere);
			return DataTableToList(ds.Tables[0]);
		}
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public List<SelfhelpOrderMgr.Model.t_bank_regList> DataTableToList(DataTable dt)
		{
			List<SelfhelpOrderMgr.Model.t_bank_regList> modelList = new List<SelfhelpOrderMgr.Model.t_bank_regList>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				SelfhelpOrderMgr.Model.t_bank_regList model;
				for (int n = 0; n < rowsCount; n++)
				{
					model = new SelfhelpOrderMgr.Model.t_bank_regList();					
													if(dt.Rows[n]["seqno"].ToString()!="")
				{
					model.seqno=int.Parse(dt.Rows[n]["seqno"].ToString());
				}
																																				model.AccNo= dt.Rows[n]["AccNo"].ToString();
																																model.fcrimecode= dt.Rows[n]["fcrimecode"].ToString();
																																model.fname= dt.Rows[n]["fname"].ToString();
																																model.PrisonNo= dt.Rows[n]["PrisonNo"].ToString();
																												if(dt.Rows[n]["Flag"].ToString()!="")
				{
					model.Flag=int.Parse(dt.Rows[n]["Flag"].ToString());
				}
																																if(dt.Rows[n]["LoadDate"].ToString()!="")
				{
					model.LoadDate=DateTime.Parse(dt.Rows[n]["LoadDate"].ToString());
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