using System; 
using System.Text;
using System.Collections.Generic; 
using System.Data;
using SelfhelpOrderMgr.Model;
namespace SelfhelpOrderMgr.BLL {
	 	//T_TempAmount_Card
		public partial class T_TempAmount_CardBLL
	{
   		     
		private readonly SelfhelpOrderMgr.DAL.T_TempAmount_CardDAL dal=new SelfhelpOrderMgr.DAL.T_TempAmount_CardDAL();
		public T_TempAmount_CardBLL()
		{}
		
		#region  Method


		/// <summary>
		/// 增加一条数据
		/// </summary>
		public void  Add(SelfhelpOrderMgr.Model.T_TempAmount_Card model)
		{
						dal.Add(model);
						
		}

		/// <summary>
		/// 更新一条数据
		/// </summary>
		public bool Update(SelfhelpOrderMgr.Model.T_TempAmount_Card model)
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
		public SelfhelpOrderMgr.Model.T_TempAmount_Card GetModel()
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
		public List<SelfhelpOrderMgr.Model.T_TempAmount_Card> GetModelList(string strWhere)
		{
			DataSet ds = dal.GetList(strWhere);
			return DataTableToList(ds.Tables[0]);
		}
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public List<SelfhelpOrderMgr.Model.T_TempAmount_Card> DataTableToList(DataTable dt)
		{
			List<SelfhelpOrderMgr.Model.T_TempAmount_Card> modelList = new List<SelfhelpOrderMgr.Model.T_TempAmount_Card>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				SelfhelpOrderMgr.Model.T_TempAmount_Card model;
				for (int n = 0; n < rowsCount; n++)
				{
					model = new SelfhelpOrderMgr.Model.T_TempAmount_Card();					
																	model.fcrimecode= dt.Rows[n]["fcrimecode"].ToString();
																																model.fname= dt.Rows[n]["fname"].ToString();
																																model.fareaName= dt.Rows[n]["fareaName"].ToString();
																																model.BankAccNo= dt.Rows[n]["BankAccNo"].ToString();
																												if(dt.Rows[n]["amounta"].ToString()!="")
				{
					model.amounta=decimal.Parse(dt.Rows[n]["amounta"].ToString());
				}
																																if(dt.Rows[n]["amountb"].ToString()!="")
				{
					model.amountb=decimal.Parse(dt.Rows[n]["amountb"].ToString());
				}
																																if(dt.Rows[n]["amountc"].ToString()!="")
				{
					model.amountc=decimal.Parse(dt.Rows[n]["amountc"].ToString());
				}
																																if(dt.Rows[n]["fmoney"].ToString()!="")
				{
					model.fmoney=decimal.Parse(dt.Rows[n]["fmoney"].ToString());
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