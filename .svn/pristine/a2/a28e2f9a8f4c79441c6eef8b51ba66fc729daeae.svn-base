using System; 
using System.Text;
using System.Collections.Generic; 
using System.Data;
using SelfhelpOrderMgr.Model;
namespace SelfhelpOrderMgr.BLL {
	 	//T_Invoice_out
		public partial class T_Invoice_outBLL
	{
   		     
		private readonly SelfhelpOrderMgr.DAL.T_Invoice_outDAL dal=new SelfhelpOrderMgr.DAL.T_Invoice_outDAL();
		public T_Invoice_outBLL()
		{}
		
		#region  Method


		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int  Add(SelfhelpOrderMgr.Model.T_Invoice_out model)
		{
						return dal.Add(model);
						
		}

		/// <summary>
		/// 更新一条数据
		/// </summary>
		public bool Update(SelfhelpOrderMgr.Model.T_Invoice_out model)
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
		public SelfhelpOrderMgr.Model.T_Invoice_out GetModel(int seqno)
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
		public List<SelfhelpOrderMgr.Model.T_Invoice_out> GetModelList(string strWhere)
		{
			DataSet ds = dal.GetList(strWhere);
			return DataTableToList(ds.Tables[0]);
		}
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public List<SelfhelpOrderMgr.Model.T_Invoice_out> DataTableToList(DataTable dt)
		{
			List<SelfhelpOrderMgr.Model.T_Invoice_out> modelList = new List<SelfhelpOrderMgr.Model.T_Invoice_out>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				SelfhelpOrderMgr.Model.T_Invoice_out model;
				for (int n = 0; n < rowsCount; n++)
				{
					model = new SelfhelpOrderMgr.Model.T_Invoice_out();					
													if(dt.Rows[n]["seqno"].ToString()!="")
				{
					model.seqno=int.Parse(dt.Rows[n]["seqno"].ToString());
				}
																																				model.fsn= dt.Rows[n]["fsn"].ToString();
																												if(dt.Rows[n]["Amount"].ToString()!="")
				{
					model.Amount=decimal.Parse(dt.Rows[n]["Amount"].ToString());
				}
																																				model.CrtBy= dt.Rows[n]["CrtBy"].ToString();
																												if(dt.Rows[n]["crtdt"].ToString()!="")
				{
					model.crtdt=DateTime.Parse(dt.Rows[n]["crtdt"].ToString());
				}
																																				model.RcvBy= dt.Rows[n]["RcvBy"].ToString();
																												if(dt.Rows[n]["Flag"].ToString()!="")
				{
					model.Flag=int.Parse(dt.Rows[n]["Flag"].ToString());
				}
																																				model.remark= dt.Rows[n]["remark"].ToString();
																												if(dt.Rows[n]["typeflag"].ToString()!="")
				{
					model.typeflag=int.Parse(dt.Rows[n]["typeflag"].ToString());
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