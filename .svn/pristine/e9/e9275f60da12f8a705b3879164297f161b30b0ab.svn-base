using System; 
using System.Text;
using System.Collections.Generic; 
using System.Data;
using SelfhelpOrderMgr.Model;
namespace SelfhelpOrderMgr.BLL {
	 	//t_bankinfo
		public partial class t_bankinfoBLL
	{
   		     
		private readonly SelfhelpOrderMgr.DAL.t_bankinfoDAL dal=new SelfhelpOrderMgr.DAL.t_bankinfoDAL();
		public t_bankinfoBLL()
		{}
		
		#region  Method


		/// <summary>
		/// 增加一条数据
		/// </summary>
		public void  Add(SelfhelpOrderMgr.Model.t_bankinfo model)
		{
						dal.Add(model);
						
		}

		/// <summary>
		/// 更新一条数据
		/// </summary>
		public bool Update(SelfhelpOrderMgr.Model.t_bankinfo model)
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
		public SelfhelpOrderMgr.Model.t_bankinfo GetModel()
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
		public List<SelfhelpOrderMgr.Model.t_bankinfo> GetModelList(string strWhere)
		{
			DataSet ds = dal.GetList(strWhere);
			return DataTableToList(ds.Tables[0]);
		}
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public List<SelfhelpOrderMgr.Model.t_bankinfo> DataTableToList(DataTable dt)
		{
			List<SelfhelpOrderMgr.Model.t_bankinfo> modelList = new List<SelfhelpOrderMgr.Model.t_bankinfo>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				SelfhelpOrderMgr.Model.t_bankinfo model;
				for (int n = 0; n < rowsCount; n++)
				{
					model = new SelfhelpOrderMgr.Model.t_bankinfo();					
																	model.AccCode= dt.Rows[n]["AccCode"].ToString();
																																model.BankName= dt.Rows[n]["BankName"].ToString();
																																model.TradeCode= dt.Rows[n]["TradeCode"].ToString();
																																model.TradeBankNo= dt.Rows[n]["TradeBankNo"].ToString();
																												if(dt.Rows[n]["inoutflag"].ToString()!="")
				{
					model.inoutflag=int.Parse(dt.Rows[n]["inoutflag"].ToString());
				}
																																				model.TradeTerminal= dt.Rows[n]["TradeTerminal"].ToString();
																																model.TradeBy= dt.Rows[n]["TradeBy"].ToString();
																																model.compcode= dt.Rows[n]["compcode"].ToString();
																												if(dt.Rows[n]["portcode"].ToString()!="")
				{
					model.portcode=int.Parse(dt.Rows[n]["portcode"].ToString());
				}
																																if(dt.Rows[n]["AgentPort"].ToString()!="")
				{
					model.AgentPort=int.Parse(dt.Rows[n]["AgentPort"].ToString());
				}
																																				model.Custid_type= dt.Rows[n]["Custid_type"].ToString();
																																model.cust_id= dt.Rows[n]["cust_id"].ToString();
																																model.FeeCode= dt.Rows[n]["FeeCode"].ToString();
																																model.MainFeeCode= dt.Rows[n]["MainFeeCode"].ToString();
																						
				
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