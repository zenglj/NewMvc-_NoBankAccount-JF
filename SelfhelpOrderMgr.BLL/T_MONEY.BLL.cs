using System; 
using System.Text;
using System.Collections.Generic; 
using System.Data;
using SelfhelpOrderMgr.Model;
namespace SelfhelpOrderMgr.BLL {
	 	//T_MONEY
		public partial class T_MONEYBLL
	{
   		     
		private readonly SelfhelpOrderMgr.DAL.T_MONEYDAL dal=new SelfhelpOrderMgr.DAL.T_MONEYDAL();
		public T_MONEYBLL()
		{}
		
		#region  Method


		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int  Add(SelfhelpOrderMgr.Model.T_MONEY model)
		{
						return dal.Add(model);
						
		}

		/// <summary>
		/// 更新一条数据
		/// </summary>
		public bool Update(SelfhelpOrderMgr.Model.T_MONEY model)
		{
			return dal.Update(model);
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public bool Delete(int FSeq)
		{
			
			return dal.Delete(FSeq);
		}
				/// <summary>
		/// 批量删除一批数据
		/// </summary>
		public bool DeleteList(string FSeqlist )
		{
			return dal.DeleteList(FSeqlist );
		}
		
		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public SelfhelpOrderMgr.Model.T_MONEY GetModel(int FSeq)
		{
			
			return dal.GetModel(FSeq);
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
		public List<SelfhelpOrderMgr.Model.T_MONEY> GetModelList(string strWhere)
		{
			DataSet ds = dal.GetList(strWhere);
			return DataTableToList(ds.Tables[0]);
		}
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public List<SelfhelpOrderMgr.Model.T_MONEY> DataTableToList(DataTable dt)
		{
			List<SelfhelpOrderMgr.Model.T_MONEY> modelList = new List<SelfhelpOrderMgr.Model.T_MONEY>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				SelfhelpOrderMgr.Model.T_MONEY model;
				for (int n = 0; n < rowsCount; n++)
				{
					model = new SelfhelpOrderMgr.Model.T_MONEY();					
													if(dt.Rows[n]["FSeq"].ToString()!="")
				{
					model.FSeq=int.Parse(dt.Rows[n]["FSeq"].ToString());
				}
																																				model.FIdenNo= dt.Rows[n]["FIdenNo"].ToString();
																												if(dt.Rows[n]["FMoney"].ToString()!="")
				{
					model.FMoney=decimal.Parse(dt.Rows[n]["FMoney"].ToString());
				}
																																if(dt.Rows[n]["FDate"].ToString()!="")
				{
					model.FDate=DateTime.Parse(dt.Rows[n]["FDate"].ToString());
				}
																																if(dt.Rows[n]["FFlag"].ToString()!="")
				{
					model.FFlag=int.Parse(dt.Rows[n]["FFlag"].ToString());
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