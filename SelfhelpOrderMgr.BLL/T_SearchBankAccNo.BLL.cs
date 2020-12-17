using System; 
using System.Text;
using System.Collections.Generic; 
using System.Data;
using SelfhelpOrderMgr.Model;
namespace SelfhelpOrderMgr.BLL {
	 	//T_SearchBankAccNo
		public partial class T_SearchBankAccNoBLL
	{
   		     
		private readonly SelfhelpOrderMgr.DAL.T_SearchBankAccNoDAL dal=new SelfhelpOrderMgr.DAL.T_SearchBankAccNoDAL();
		public T_SearchBankAccNoBLL()
		{}
		
		#region  Method


		/// <summary>
		/// 增加一条数据
		/// </summary>
		public long  Add(SelfhelpOrderMgr.Model.T_SearchBankAccNo model)
		{
						return dal.Add(model);
						
		}

		/// <summary>
		/// 更新一条数据
		/// </summary>
		public bool Update(SelfhelpOrderMgr.Model.T_SearchBankAccNo model)
		{
			return dal.Update(model);
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public bool Delete(long keyId)
		{
			
			return dal.Delete(keyId);
		}
				/// <summary>
		/// 批量删除一批数据
		/// </summary>
		public bool DeleteList(string keyIdlist )
		{
			return dal.DeleteList(keyIdlist );
		}
		
		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public SelfhelpOrderMgr.Model.T_SearchBankAccNo GetModel(long keyId)
		{
			
			return dal.GetModel(keyId);
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
		public List<SelfhelpOrderMgr.Model.T_SearchBankAccNo> GetModelList(string strWhere)
		{
			DataSet ds = dal.GetList(strWhere);
			return DataTableToList(ds.Tables[0]);
		}
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public List<SelfhelpOrderMgr.Model.T_SearchBankAccNo> DataTableToList(DataTable dt)
		{
			List<SelfhelpOrderMgr.Model.T_SearchBankAccNo> modelList = new List<SelfhelpOrderMgr.Model.T_SearchBankAccNo>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				SelfhelpOrderMgr.Model.T_SearchBankAccNo model;
				for (int n = 0; n < rowsCount; n++)
				{
					model = new SelfhelpOrderMgr.Model.T_SearchBankAccNo();					
													if(dt.Rows[n]["keyId"].ToString()!="")
				{
					model.keyId=long.Parse(dt.Rows[n]["keyId"].ToString());
				}
																																				model.fcrimecode= dt.Rows[n]["fcrimecode"].ToString();
																																model.FName= dt.Rows[n]["FName"].ToString();
																																model.BankAccNo= dt.Rows[n]["BankAccNo"].ToString();
																						
				
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