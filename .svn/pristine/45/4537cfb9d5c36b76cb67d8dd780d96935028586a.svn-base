using System; 
using System.Text;
using System.Collections.Generic; 
using System.Data;
using SelfhelpOrderMgr.Model;
namespace SelfhelpOrderMgr.BLL {
	 	//T_GoodsStock
		public partial class T_GoodsStockBLL
	{
   		     
		private readonly SelfhelpOrderMgr.DAL.T_GoodsStockDAL dal=new SelfhelpOrderMgr.DAL.T_GoodsStockDAL();
		public T_GoodsStockBLL()
		{}
		
		#region  Method


		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int  Add(SelfhelpOrderMgr.Model.T_GoodsStock model)
		{
						return dal.Add(model);
						
		}

		/// <summary>
		/// 更新一条数据
		/// </summary>
		public bool Update(SelfhelpOrderMgr.Model.T_GoodsStock model)
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
		public SelfhelpOrderMgr.Model.T_GoodsStock GetModel(int seqno)
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
		public List<SelfhelpOrderMgr.Model.T_GoodsStock> GetModelList(string strWhere)
		{
			DataSet ds = dal.GetList(strWhere);
			return DataTableToList(ds.Tables[0]);
		}
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public List<SelfhelpOrderMgr.Model.T_GoodsStock> DataTableToList(DataTable dt)
		{
			List<SelfhelpOrderMgr.Model.T_GoodsStock> modelList = new List<SelfhelpOrderMgr.Model.T_GoodsStock>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				SelfhelpOrderMgr.Model.T_GoodsStock model;
				for (int n = 0; n < rowsCount; n++)
				{
					model = new SelfhelpOrderMgr.Model.T_GoodsStock();					
													if(dt.Rows[n]["seqno"].ToString()!="")
				{
					model.seqno=int.Parse(dt.Rows[n]["seqno"].ToString());
				}
																																				model.gcode= dt.Rows[n]["gcode"].ToString();
																																model.gtxm= dt.Rows[n]["gtxm"].ToString();
																												if(dt.Rows[n]["InDate"].ToString()!="")
				{
					model.InDate=DateTime.Parse(dt.Rows[n]["InDate"].ToString());
				}
																																if(dt.Rows[n]["Gdj"].ToString()!="")
				{
					model.Gdj=decimal.Parse(dt.Rows[n]["Gdj"].ToString());
				}
																																if(dt.Rows[n]["Balance"].ToString()!="")
				{
					model.Balance=decimal.Parse(dt.Rows[n]["Balance"].ToString());
				}
																																if(dt.Rows[n]["TmpBalance"].ToString()!="")
				{
					model.TmpBalance=decimal.Parse(dt.Rows[n]["TmpBalance"].ToString());
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