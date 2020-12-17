using System; 
using System.Text;
using System.Collections.Generic; 
using System.Data;
using SelfhelpOrderMgr.Model;
namespace SelfhelpOrderMgr.BLL {
	 	//t_BankProve
		public partial class t_BankProveBLL
	{
   		     
		private readonly SelfhelpOrderMgr.DAL.t_BankProveDAL dal=new SelfhelpOrderMgr.DAL.t_BankProveDAL();
		public t_BankProveBLL()
		{}
		
		#region  Method


		/// <summary>
		/// 增加一条数据
		/// </summary>
		public void  Add(SelfhelpOrderMgr.Model.t_BankProve model)
		{
						dal.Add(model);
						
		}

		/// <summary>
		/// 更新一条数据
		/// </summary>
		public bool Update(SelfhelpOrderMgr.Model.t_BankProve model)
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
		public SelfhelpOrderMgr.Model.t_BankProve GetModel()
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
		public List<SelfhelpOrderMgr.Model.t_BankProve> GetModelList(string strWhere)
		{
			DataSet ds = dal.GetList(strWhere);
			return DataTableToList(ds.Tables[0]);
		}
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public List<SelfhelpOrderMgr.Model.t_BankProve> DataTableToList(DataTable dt)
		{
			List<SelfhelpOrderMgr.Model.t_BankProve> modelList = new List<SelfhelpOrderMgr.Model.t_BankProve>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				SelfhelpOrderMgr.Model.t_BankProve model;
				for (int n = 0; n < rowsCount; n++)
				{
					model = new SelfhelpOrderMgr.Model.t_BankProve();					
																	model.fcode= dt.Rows[n]["fcode"].ToString();
																																model.fname= dt.Rows[n]["fname"].ToString();
																												if(dt.Rows[n]["foudate"].ToString()!="")
				{
					model.foudate=DateTime.Parse(dt.Rows[n]["foudate"].ToString());
				}
																																				model.FIdenNo= dt.Rows[n]["FIdenNo"].ToString();
																																model.fareaName= dt.Rows[n]["fareaName"].ToString();
																																model.BankCode= dt.Rows[n]["BankCode"].ToString();
																												if(dt.Rows[n]["CardMoney"].ToString()!="")
				{
					model.CardMoney=decimal.Parse(dt.Rows[n]["CardMoney"].ToString());
				}
																																if(dt.Rows[n]["crtDate"].ToString()!="")
				{
					model.crtDate=DateTime.Parse(dt.Rows[n]["crtDate"].ToString());
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