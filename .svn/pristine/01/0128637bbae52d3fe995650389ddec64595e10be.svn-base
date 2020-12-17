using System; 
using System.Text;
using System.Collections.Generic; 
using System.Data;
using SelfhelpOrderMgr.Model;
namespace SelfhelpOrderMgr.BLL {
	 	//T_Invoice_old
		public partial class T_Invoice_oldBLL
	{
   		     
		private readonly SelfhelpOrderMgr.DAL.T_Invoice_oldDAL dal=new SelfhelpOrderMgr.DAL.T_Invoice_oldDAL();
		public T_Invoice_oldBLL()
		{}
		
		#region  Method


		/// <summary>
		/// 增加一条数据
		/// </summary>
		public void  Add(SelfhelpOrderMgr.Model.T_Invoice_old model)
		{
						dal.Add(model);
						
		}

		/// <summary>
		/// 更新一条数据
		/// </summary>
		public bool Update(SelfhelpOrderMgr.Model.T_Invoice_old model)
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
		public SelfhelpOrderMgr.Model.T_Invoice_old GetModel()
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
		public List<SelfhelpOrderMgr.Model.T_Invoice_old> GetModelList(string strWhere)
		{
			DataSet ds = dal.GetList(strWhere);
			return DataTableToList(ds.Tables[0]);
		}
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public List<SelfhelpOrderMgr.Model.T_Invoice_old> DataTableToList(DataTable dt)
		{
			List<SelfhelpOrderMgr.Model.T_Invoice_old> modelList = new List<SelfhelpOrderMgr.Model.T_Invoice_old>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				SelfhelpOrderMgr.Model.T_Invoice_old model;
				for (int n = 0; n < rowsCount; n++)
				{
					model = new SelfhelpOrderMgr.Model.T_Invoice_old();					
																	model.INVOICENO= dt.Rows[n]["INVOICENO"].ToString();
																																model.cardcode= dt.Rows[n]["cardcode"].ToString();
																																model.fcrimecode= dt.Rows[n]["fcrimecode"].ToString();
																												if(dt.Rows[n]["amount"].ToString()!="")
				{
					model.amount=decimal.Parse(dt.Rows[n]["amount"].ToString());
				}
																																if(dt.Rows[n]["OrderDate"].ToString()!="")
				{
					model.OrderDate=DateTime.Parse(dt.Rows[n]["OrderDate"].ToString());
				}
																																if(dt.Rows[n]["PayDATE"].ToString()!="")
				{
					model.PayDATE=DateTime.Parse(dt.Rows[n]["PayDATE"].ToString());
				}
																																				model.PTYPE= dt.Rows[n]["PTYPE"].ToString();
																												if(dt.Rows[n]["Flag"].ToString()!="")
				{
					model.Flag=int.Parse(dt.Rows[n]["Flag"].ToString());
				}
																																				model.REMARK= dt.Rows[n]["REMARK"].ToString();
																												if(dt.Rows[n]["servamount"].ToString()!="")
				{
					model.servamount=decimal.Parse(dt.Rows[n]["servamount"].ToString());
				}
																																				model.crtby= dt.Rows[n]["crtby"].ToString();
																												if(dt.Rows[n]["crtdate"].ToString()!="")
				{
					model.crtdate=DateTime.Parse(dt.Rows[n]["crtdate"].ToString());
				}
																																				model.fsn= dt.Rows[n]["fsn"].ToString();
																																model.fareacode= dt.Rows[n]["fareacode"].ToString();
																																model.fareaName= dt.Rows[n]["fareaName"].ToString();
																																model.fcriminal= dt.Rows[n]["fcriminal"].ToString();
																																model.Frealareacode= dt.Rows[n]["Frealareacode"].ToString();
																																model.FrealAreaName= dt.Rows[n]["FrealAreaName"].ToString();
																												if(dt.Rows[n]["TYPEFLAG"].ToString()!="")
				{
					model.TYPEFLAG=int.Parse(dt.Rows[n]["TYPEFLAG"].ToString());
				}
																																if(dt.Rows[n]["CardType"].ToString()!="")
				{
					model.CardType=int.Parse(dt.Rows[n]["CardType"].ToString());
				}
																																if(dt.Rows[n]["AmountA"].ToString()!="")
				{
					model.AmountA=decimal.Parse(dt.Rows[n]["AmountA"].ToString());
				}
																																if(dt.Rows[n]["AmountB"].ToString()!="")
				{
					model.AmountB=decimal.Parse(dt.Rows[n]["AmountB"].ToString());
				}
																																if(dt.Rows[n]["fifoflag"].ToString()!="")
				{
					model.fifoflag=int.Parse(dt.Rows[n]["fifoflag"].ToString());
				}
																																if(dt.Rows[n]["FreeAmountA"].ToString()!="")
				{
					model.FreeAmountA=decimal.Parse(dt.Rows[n]["FreeAmountA"].ToString());
				}
																																if(dt.Rows[n]["FreeAmountB"].ToString()!="")
				{
					model.FreeAmountB=decimal.Parse(dt.Rows[n]["FreeAmountB"].ToString());
				}
																																if(dt.Rows[n]["checkflag"].ToString()!="")
				{
					model.checkflag=int.Parse(dt.Rows[n]["checkflag"].ToString());
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