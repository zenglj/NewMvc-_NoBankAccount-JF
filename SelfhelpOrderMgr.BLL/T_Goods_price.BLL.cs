using System; 
using System.Text;
using System.Collections.Generic; 
using System.Data;
using SelfhelpOrderMgr.Model;
namespace SelfhelpOrderMgr.BLL {
	 	//T_Goods_price
		public partial class T_Goods_priceBLL
	{
   		     
		private readonly SelfhelpOrderMgr.DAL.T_Goods_priceDAL dal=new SelfhelpOrderMgr.DAL.T_Goods_priceDAL();
		public T_Goods_priceBLL()
		{}
		
		#region  Method


		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int  Add(SelfhelpOrderMgr.Model.T_Goods_price model)
		{
						return dal.Add(model);
						
		}

		/// <summary>
		/// 更新一条数据
		/// </summary>
		public bool Update(SelfhelpOrderMgr.Model.T_Goods_price model)
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
		public SelfhelpOrderMgr.Model.T_Goods_price GetModel(int seqno)
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
		public List<SelfhelpOrderMgr.Model.T_Goods_price> GetModelList(string strWhere)
		{
			DataSet ds = dal.GetList(strWhere);
			return DataTableToList(ds.Tables[0]);
		}
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public List<SelfhelpOrderMgr.Model.T_Goods_price> DataTableToList(DataTable dt)
		{
			List<SelfhelpOrderMgr.Model.T_Goods_price> modelList = new List<SelfhelpOrderMgr.Model.T_Goods_price>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				SelfhelpOrderMgr.Model.T_Goods_price model;
				for (int n = 0; n < rowsCount; n++)
				{
					model = new SelfhelpOrderMgr.Model.T_Goods_price();					
													if(dt.Rows[n]["seqno"].ToString()!="")
				{
					model.seqno=int.Parse(dt.Rows[n]["seqno"].ToString());
				}
																																				model.gcode= dt.Rows[n]["gcode"].ToString();
																																model.gname= dt.Rows[n]["gname"].ToString();
																												if(dt.Rows[n]["Gdj"].ToString()!="")
				{
					model.Gdj=decimal.Parse(dt.Rows[n]["Gdj"].ToString());
				}
																																if(dt.Rows[n]["BegDate"].ToString()!="")
				{
					model.BegDate=DateTime.Parse(dt.Rows[n]["BegDate"].ToString());
				}
																																				model.CRTBY= dt.Rows[n]["CRTBY"].ToString();
																												if(dt.Rows[n]["CRTDT"].ToString()!="")
				{
					model.CRTDT=DateTime.Parse(dt.Rows[n]["CRTDT"].ToString());
				}
																																				model.MODBY= dt.Rows[n]["MODBY"].ToString();
																												if(dt.Rows[n]["MODDT"].ToString()!="")
				{
					model.MODDT=DateTime.Parse(dt.Rows[n]["MODDT"].ToString());
				}
																																if(dt.Rows[n]["gorigdj"].ToString()!="")
				{
					model.gorigdj=decimal.Parse(dt.Rows[n]["gorigdj"].ToString());
				}
																																if(dt.Rows[n]["PlanPrice"].ToString()!="")
				{
					model.PlanPrice=decimal.Parse(dt.Rows[n]["PlanPrice"].ToString());
				}
																																				model.ChkBy= dt.Rows[n]["ChkBy"].ToString();
																												if(dt.Rows[n]["ChkDate"].ToString()!="")
				{
					model.ChkDate=DateTime.Parse(dt.Rows[n]["ChkDate"].ToString());
				}
																																if(dt.Rows[n]["ChkFlag"].ToString()!="")
				{
					model.ChkFlag=int.Parse(dt.Rows[n]["ChkFlag"].ToString());
				}
																																				model.Remark= dt.Rows[n]["Remark"].ToString();
																																model.ChkInfo= dt.Rows[n]["ChkInfo"].ToString();
																						
				
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