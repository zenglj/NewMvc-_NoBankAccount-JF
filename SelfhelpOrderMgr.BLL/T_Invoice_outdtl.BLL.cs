﻿using System; 
using System.Text;
using System.Collections.Generic; 
using System.Data;
using SelfhelpOrderMgr.Model;
namespace SelfhelpOrderMgr.BLL {
	 	//T_Invoice_outdtl
		public partial class T_Invoice_outdtlBLL
	{
   		     
		private readonly SelfhelpOrderMgr.DAL.T_Invoice_outdtlDAL dal=new SelfhelpOrderMgr.DAL.T_Invoice_outdtlDAL();
		public T_Invoice_outdtlBLL()
		{}
		
		#region  Method


		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int  Add(SelfhelpOrderMgr.Model.T_Invoice_outdtl model)
		{
						return dal.Add(model);
						
		}

		/// <summary>
		/// 更新一条数据
		/// </summary>
		public bool Update(SelfhelpOrderMgr.Model.T_Invoice_outdtl model)
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
		public SelfhelpOrderMgr.Model.T_Invoice_outdtl GetModel(int seqno)
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
		public List<SelfhelpOrderMgr.Model.T_Invoice_outdtl> GetModelList(string strWhere)
		{
			DataSet ds = dal.GetList(strWhere);
			return DataTableToList(ds.Tables[0]);
		}
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public List<SelfhelpOrderMgr.Model.T_Invoice_outdtl> DataTableToList(DataTable dt)
		{
			List<SelfhelpOrderMgr.Model.T_Invoice_outdtl> modelList = new List<SelfhelpOrderMgr.Model.T_Invoice_outdtl>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				SelfhelpOrderMgr.Model.T_Invoice_outdtl model;
				for (int n = 0; n < rowsCount; n++)
				{
					model = new SelfhelpOrderMgr.Model.T_Invoice_outdtl();					
													if(dt.Rows[n]["seqno"].ToString()!="")
				{
					model.seqno=int.Parse(dt.Rows[n]["seqno"].ToString());
				}
																																				model.fsn= dt.Rows[n]["fsn"].ToString();
																																model.InvoiceNo= dt.Rows[n]["InvoiceNo"].ToString();
																																model.Fcrimecode= dt.Rows[n]["Fcrimecode"].ToString();
																																model.fcriminal= dt.Rows[n]["fcriminal"].ToString();
																																model.fareaName= dt.Rows[n]["fareaName"].ToString();
																												if(dt.Rows[n]["OrderDate"].ToString()!="")
				{
					model.OrderDate=DateTime.Parse(dt.Rows[n]["OrderDate"].ToString());
				}
																																if(dt.Rows[n]["Amount"].ToString()!="")
				{
					model.Amount=decimal.Parse(dt.Rows[n]["Amount"].ToString());
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