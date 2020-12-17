using System; 
using System.Text;
using System.Collections.Generic; 
using System.Data;
using SelfhelpOrderMgr.Model;
namespace SelfhelpOrderMgr.BLL {
	 	//T_Importgoods
		public partial class T_ImportgoodsBLL
	{
   		     
		private readonly SelfhelpOrderMgr.DAL.T_ImportgoodsDAL dal=new SelfhelpOrderMgr.DAL.T_ImportgoodsDAL();
		public T_ImportgoodsBLL()
		{}
		
		#region  Method


		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int  Add(SelfhelpOrderMgr.Model.T_Importgoods model)
		{
						return dal.Add(model);
						
		}

		/// <summary>
		/// 更新一条数据
		/// </summary>
		public bool Update(SelfhelpOrderMgr.Model.T_Importgoods model)
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
		public SelfhelpOrderMgr.Model.T_Importgoods GetModel(int seqno)
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
		public List<SelfhelpOrderMgr.Model.T_Importgoods> GetModelList(string strWhere)
		{
			DataSet ds = dal.GetList(strWhere);
			return DataTableToList(ds.Tables[0]);
		}
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public List<SelfhelpOrderMgr.Model.T_Importgoods> DataTableToList(DataTable dt)
		{
			List<SelfhelpOrderMgr.Model.T_Importgoods> modelList = new List<SelfhelpOrderMgr.Model.T_Importgoods>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				SelfhelpOrderMgr.Model.T_Importgoods model;
				for (int n = 0; n < rowsCount; n++)
				{
					model = new SelfhelpOrderMgr.Model.T_Importgoods();					
													if(dt.Rows[n]["seqno"].ToString()!="")
				{
					model.seqno=int.Parse(dt.Rows[n]["seqno"].ToString());
				}
																																if(dt.Rows[n]["pc"].ToString()!="")
				{
					model.pc=int.Parse(dt.Rows[n]["pc"].ToString());
				}
																																				model.gtypename= dt.Rows[n]["gtypename"].ToString();
																																model.gname= dt.Rows[n]["gname"].ToString();
																																model.gtxm= dt.Rows[n]["gtxm"].ToString();
																																model.Remark= dt.Rows[n]["Remark"].ToString();
																												if(dt.Rows[n]["crtdate"].ToString()!="")
				{
					model.crtdate=DateTime.Parse(dt.Rows[n]["crtdate"].ToString());
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