using System; 
using System.Text;
using System.Collections.Generic; 
using System.Data;
using SelfhelpOrderMgr.Model;
namespace SelfhelpOrderMgr.BLL {
	 	//t_area_change
		public partial class t_area_changeBLL
	{
   		     
		private readonly SelfhelpOrderMgr.DAL.t_area_changeDAL dal=new SelfhelpOrderMgr.DAL.t_area_changeDAL();
		public t_area_changeBLL()
		{}
		
		#region  Method


		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int  Add(SelfhelpOrderMgr.Model.t_area_change model)
		{
						return dal.Add(model);
						
		}

		/// <summary>
		/// 更新一条数据
		/// </summary>
		public bool Update(SelfhelpOrderMgr.Model.t_area_change model)
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
		public SelfhelpOrderMgr.Model.t_area_change GetModel(int seqno)
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
		public List<SelfhelpOrderMgr.Model.t_area_change> GetModelList(string strWhere)
		{
			DataSet ds = dal.GetList(strWhere);
			return DataTableToList(ds.Tables[0]);
		}
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public List<SelfhelpOrderMgr.Model.t_area_change> DataTableToList(DataTable dt)
		{
			List<SelfhelpOrderMgr.Model.t_area_change> modelList = new List<SelfhelpOrderMgr.Model.t_area_change>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				SelfhelpOrderMgr.Model.t_area_change model;
				for (int n = 0; n < rowsCount; n++)
				{
					model = new SelfhelpOrderMgr.Model.t_area_change();					
													if(dt.Rows[n]["seqno"].ToString()!="")
				{
					model.seqno=int.Parse(dt.Rows[n]["seqno"].ToString());
				}
																																				model.fcrimecode= dt.Rows[n]["fcrimecode"].ToString();
																																model.fcriminal= dt.Rows[n]["fcriminal"].ToString();
																																model.fareacode= dt.Rows[n]["fareacode"].ToString();
																																model.fareaname= dt.Rows[n]["fareaname"].ToString();
																																model.fNewAreacode= dt.Rows[n]["fNewAreacode"].ToString();
																																model.fNewAreaName= dt.Rows[n]["fNewAreaName"].ToString();
																																model.crtby= dt.Rows[n]["crtby"].ToString();
																												if(dt.Rows[n]["crtdate"].ToString()!="")
				{
					model.crtdate=DateTime.Parse(dt.Rows[n]["crtdate"].ToString());
				}
																																if(dt.Rows[n]["amount"].ToString()!="")
				{
					model.amount=decimal.Parse(dt.Rows[n]["amount"].ToString());
				}
																																				model.DVOUNO= dt.Rows[n]["DVOUNO"].ToString();
																																model.CVOUNO= dt.Rows[n]["CVOUNO"].ToString();
																						
				
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