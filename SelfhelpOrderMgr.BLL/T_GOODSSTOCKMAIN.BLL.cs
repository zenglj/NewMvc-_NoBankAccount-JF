using System; 
using System.Text;
using System.Collections.Generic; 
using System.Data;
using SelfhelpOrderMgr.Model;
namespace SelfhelpOrderMgr.BLL {
	 	//T_GOODSSTOCKMAIN
		public partial class T_GOODSSTOCKMAINBLL
	{
   		     
		private readonly SelfhelpOrderMgr.DAL.T_GOODSSTOCKMAINDAL dal=new SelfhelpOrderMgr.DAL.T_GOODSSTOCKMAINDAL();
		public T_GOODSSTOCKMAINBLL()
		{}
		
		#region  Method


		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int  Add(SelfhelpOrderMgr.Model.T_GOODSSTOCKMAIN model)
		{
						return dal.Add(model);
						
		}

		/// <summary>
		/// 更新一条数据
		/// </summary>
		public bool Update(SelfhelpOrderMgr.Model.T_GOODSSTOCKMAIN model)
		{
			return dal.Update(model);
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public bool Delete(int SEQNO)
		{
			
			return dal.Delete(SEQNO);
		}
				/// <summary>
		/// 批量删除一批数据
		/// </summary>
		public bool DeleteList(string SEQNOlist )
		{
			return dal.DeleteList(SEQNOlist );
		}
		
		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public SelfhelpOrderMgr.Model.T_GOODSSTOCKMAIN GetModel(int SEQNO)
		{
			
			return dal.GetModel(SEQNO);
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
		public List<SelfhelpOrderMgr.Model.T_GOODSSTOCKMAIN> GetModelList(string strWhere)
		{
			DataSet ds = dal.GetList(strWhere);
			return DataTableToList(ds.Tables[0]);
            
		}
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public List<SelfhelpOrderMgr.Model.T_GOODSSTOCKMAIN> DataTableToList(DataTable dt)
		{
			List<SelfhelpOrderMgr.Model.T_GOODSSTOCKMAIN> modelList = new List<SelfhelpOrderMgr.Model.T_GOODSSTOCKMAIN>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				SelfhelpOrderMgr.Model.T_GOODSSTOCKMAIN model;
				for (int n = 0; n < rowsCount; n++)
				{
					model = new SelfhelpOrderMgr.Model.T_GOODSSTOCKMAIN();					
													if(dt.Rows[n]["SEQNO"].ToString()!="")
				{
					model.SEQNO=int.Parse(dt.Rows[n]["SEQNO"].ToString());
				}
																																				model.GCODE= dt.Rows[n]["GCODE"].ToString();
																												if(dt.Rows[n]["BALANCE"].ToString()!="")
				{
					model.BALANCE=decimal.Parse(dt.Rows[n]["BALANCE"].ToString());
				}
																																if(dt.Rows[n]["TMPBALANCE"].ToString()!="")
				{
					model.TMPBALANCE=decimal.Parse(dt.Rows[n]["TMPBALANCE"].ToString());
				}
																																if(dt.Rows[n]["GCurMonthNum"].ToString()!="")
				{
					model.GCurMonthNum=decimal.Parse(dt.Rows[n]["GCurMonthNum"].ToString());
				}
																																if(dt.Rows[n]["GCurMonthPrice"].ToString()!="")
				{
					model.GCurMonthPrice=decimal.Parse(dt.Rows[n]["GCurMonthPrice"].ToString());
				}
																																if(dt.Rows[n]["GLastAvgNum"].ToString()!="")
				{
					model.GLastAvgNum=decimal.Parse(dt.Rows[n]["GLastAvgNum"].ToString());
				}
																																if(dt.Rows[n]["GLastAvgPrice"].ToString()!="")
				{
					model.GLastAvgPrice=decimal.Parse(dt.Rows[n]["GLastAvgPrice"].ToString());
				}
																																if(dt.Rows[n]["LastDate"].ToString()!="")
				{
					model.LastDate=DateTime.Parse(dt.Rows[n]["LastDate"].ToString());
				}
																																if(dt.Rows[n]["GJieShuanNumber"].ToString()!="")
				{
					model.GJieShuanNumber=decimal.Parse(dt.Rows[n]["GJieShuanNumber"].ToString());
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