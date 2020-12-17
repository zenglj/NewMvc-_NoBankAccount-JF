using System; 
using System.Text;
using System.Collections.Generic; 
using System.Data;
using SelfhelpOrderMgr.Model;
namespace SelfhelpOrderMgr.BLL {
	 	//T_BALANCE
		public partial class T_BALANCEBLL
	{
   		     
		private readonly SelfhelpOrderMgr.DAL.T_BALANCEDAL dal=new SelfhelpOrderMgr.DAL.T_BALANCEDAL();
		public T_BALANCEBLL()
		{}
		
		#region  Method


		/// <summary>
		/// 增加一条数据
		/// </summary>
		public void  Add(SelfhelpOrderMgr.Model.T_BALANCE model)
		{
						dal.Add(model);
						
		}

		/// <summary>
		/// 更新一条数据
		/// </summary>
		public bool Update(SelfhelpOrderMgr.Model.T_BALANCE model)
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
		public SelfhelpOrderMgr.Model.T_BALANCE GetModel()
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
		public List<SelfhelpOrderMgr.Model.T_BALANCE> GetModelList(string strWhere)
		{
			DataSet ds = dal.GetList(strWhere);
			return DataTableToList(ds.Tables[0]);
		}
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public List<SelfhelpOrderMgr.Model.T_BALANCE> DataTableToList(DataTable dt)
		{
			List<SelfhelpOrderMgr.Model.T_BALANCE> modelList = new List<SelfhelpOrderMgr.Model.T_BALANCE>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				SelfhelpOrderMgr.Model.T_BALANCE model;
				for (int n = 0; n < rowsCount; n++)
				{
					model = new SelfhelpOrderMgr.Model.T_BALANCE();					
													if(dt.Rows[n]["seqNo"].ToString()!="")
				{
					model.seqNo=int.Parse(dt.Rows[n]["seqNo"].ToString());
				}
																																				model.cardcode= dt.Rows[n]["cardcode"].ToString();
																																model.fcrimecode= dt.Rows[n]["fcrimecode"].ToString();
																												if(dt.Rows[n]["amount"].ToString()!="")
				{
					model.amount=decimal.Parse(dt.Rows[n]["amount"].ToString());
				}
																																if(dt.Rows[n]["CRTDATE"].ToString()!="")
				{
					model.CRTDATE=DateTime.Parse(dt.Rows[n]["CRTDATE"].ToString());
				}
																																				model.BTYPE= dt.Rows[n]["BTYPE"].ToString();
																																model.REMARK= dt.Rows[n]["REMARK"].ToString();
																						
				
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