using System; 
using System.Text;
using System.Collections.Generic; 
using System.Data;
using SelfhelpOrderMgr.Model;
namespace SelfhelpOrderMgr.BLL {
	 	//t_bank_dealList
		public partial class t_bank_dealListBLL
	{
   		     
		private readonly SelfhelpOrderMgr.DAL.t_bank_dealListDAL dal=new SelfhelpOrderMgr.DAL.t_bank_dealListDAL();
		public t_bank_dealListBLL()
		{}
		
		#region  Method


		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int  Add(SelfhelpOrderMgr.Model.t_bank_dealList model)
		{
						return dal.Add(model);
						
		}

		/// <summary>
		/// 更新一条数据
		/// </summary>
		public bool Update(SelfhelpOrderMgr.Model.t_bank_dealList model)
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
		public SelfhelpOrderMgr.Model.t_bank_dealList GetModel(int seqno)
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
		public List<SelfhelpOrderMgr.Model.t_bank_dealList> GetModelList(string strWhere)
		{
			DataSet ds = dal.GetList(strWhere);
			return DataTableToList(ds.Tables[0]);
		}
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public List<SelfhelpOrderMgr.Model.t_bank_dealList> DataTableToList(DataTable dt)
		{
			List<SelfhelpOrderMgr.Model.t_bank_dealList> modelList = new List<SelfhelpOrderMgr.Model.t_bank_dealList>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				SelfhelpOrderMgr.Model.t_bank_dealList model;
				for (int n = 0; n < rowsCount; n++)
				{
					model = new SelfhelpOrderMgr.Model.t_bank_dealList();					
													if(dt.Rows[n]["seqno"].ToString()!="")
				{
					model.seqno=int.Parse(dt.Rows[n]["seqno"].ToString());
				}
																																				model.AccNo= dt.Rows[n]["AccNo"].ToString();
																																model.fcrimecode= dt.Rows[n]["fcrimecode"].ToString();
																																model.fName= dt.Rows[n]["fName"].ToString();
																												if(dt.Rows[n]["dcflag"].ToString()!="")
				{
					model.dcflag=int.Parse(dt.Rows[n]["dcflag"].ToString());
				}
																																if(dt.Rows[n]["amount"].ToString()!="")
				{
					model.amount=decimal.Parse(dt.Rows[n]["amount"].ToString());
				}
																																if(dt.Rows[n]["BalAmount"].ToString()!="")
				{
					model.BalAmount=decimal.Parse(dt.Rows[n]["BalAmount"].ToString());
				}
																																				model.Remark= dt.Rows[n]["Remark"].ToString();
																																model.BankSeqno= dt.Rows[n]["BankSeqno"].ToString();
																																model.BankDealCode= dt.Rows[n]["BankDealCode"].ToString();
																												if(dt.Rows[n]["DealDate"].ToString()!="")
				{
					model.DealDate=DateTime.Parse(dt.Rows[n]["DealDate"].ToString());
				}
																																if(dt.Rows[n]["LoadDate"].ToString()!="")
				{
					model.LoadDate=DateTime.Parse(dt.Rows[n]["LoadDate"].ToString());
				}
																																if(dt.Rows[n]["flag"].ToString()!="")
				{
					model.flag=int.Parse(dt.Rows[n]["flag"].ToString());
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