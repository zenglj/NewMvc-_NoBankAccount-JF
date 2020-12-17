using System; 
using System.Text;
using System.Collections.Generic; 
using System.Data;
using SelfhelpOrderMgr.Model;
namespace SelfhelpOrderMgr.BLL {
	 	//T_TmpRcvPay
		public partial class T_TmpRcvPayBLL
	{
   		     
		private readonly SelfhelpOrderMgr.DAL.T_TmpRcvPayDAL dal=new SelfhelpOrderMgr.DAL.T_TmpRcvPayDAL();
		public T_TmpRcvPayBLL()
		{}
		
		#region  Method


		/// <summary>
		/// 增加一条数据
		/// </summary>
		public void  Add(SelfhelpOrderMgr.Model.T_TmpRcvPay model)
		{
						dal.Add(model);
						
		}

		/// <summary>
		/// 更新一条数据
		/// </summary>
		public bool Update(SelfhelpOrderMgr.Model.T_TmpRcvPay model)
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
		public SelfhelpOrderMgr.Model.T_TmpRcvPay GetModel()
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
		public List<SelfhelpOrderMgr.Model.T_TmpRcvPay> GetModelList(string strWhere)
		{
			DataSet ds = dal.GetList(strWhere);
			return DataTableToList(ds.Tables[0]);
		}
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public List<SelfhelpOrderMgr.Model.T_TmpRcvPay> DataTableToList(DataTable dt)
		{
			List<SelfhelpOrderMgr.Model.T_TmpRcvPay> modelList = new List<SelfhelpOrderMgr.Model.T_TmpRcvPay>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				SelfhelpOrderMgr.Model.T_TmpRcvPay model;
				for (int n = 0; n < rowsCount; n++)
				{
					model = new SelfhelpOrderMgr.Model.T_TmpRcvPay();					
																	model.BankName= dt.Rows[n]["BankName"].ToString();
																																model.AccNo= dt.Rows[n]["AccNo"].ToString();
																																model.Dtype= dt.Rows[n]["Dtype"].ToString();
																												if(dt.Rows[n]["paydate"].ToString()!="")
				{
					model.paydate=DateTime.Parse(dt.Rows[n]["paydate"].ToString());
				}
																																if(dt.Rows[n]["Fmoney"].ToString()!="")
				{
					model.Fmoney=decimal.Parse(dt.Rows[n]["Fmoney"].ToString());
				}
																																if(dt.Rows[n]["SucMoney"].ToString()!="")
				{
					model.SucMoney=decimal.Parse(dt.Rows[n]["SucMoney"].ToString());
				}
																																if(dt.Rows[n]["ErrMoney"].ToString()!="")
				{
					model.ErrMoney=decimal.Parse(dt.Rows[n]["ErrMoney"].ToString());
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