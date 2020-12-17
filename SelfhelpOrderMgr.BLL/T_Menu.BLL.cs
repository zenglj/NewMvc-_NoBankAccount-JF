using System; 
using System.Text;
using System.Collections.Generic; 
using System.Data;
using SelfhelpOrderMgr.Model;
namespace SelfhelpOrderMgr.BLL {
	 	//T_Menu
		public partial class T_MenuBLL
	{
   		     
		private readonly SelfhelpOrderMgr.DAL.T_MenuDAL dal=new SelfhelpOrderMgr.DAL.T_MenuDAL();
		public T_MenuBLL()
		{}
		
		#region  Method


		/// <summary>
		/// 增加一条数据
		/// </summary>
		public void  Add(SelfhelpOrderMgr.Model.T_Menu model)
		{
						dal.Add(model);
						
		}

		/// <summary>
		/// 更新一条数据
		/// </summary>
		public bool Update(SelfhelpOrderMgr.Model.T_Menu model)
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
		public SelfhelpOrderMgr.Model.T_Menu GetModel()
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
		public List<SelfhelpOrderMgr.Model.T_Menu> GetModelList(string strWhere)
		{
			DataSet ds = dal.GetList(strWhere);
			return DataTableToList(ds.Tables[0]);
		}
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public List<SelfhelpOrderMgr.Model.T_Menu> DataTableToList(DataTable dt)
		{
			List<SelfhelpOrderMgr.Model.T_Menu> modelList = new List<SelfhelpOrderMgr.Model.T_Menu>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				SelfhelpOrderMgr.Model.T_Menu model;
				for (int n = 0; n < rowsCount; n++)
				{
					model = new SelfhelpOrderMgr.Model.T_Menu();					
													if(dt.Rows[n]["code"].ToString()!="")
				{
					model.code=int.Parse(dt.Rows[n]["code"].ToString());
				}
																																				model.fname= dt.Rows[n]["fname"].ToString();
																												if(dt.Rows[n]["dj"].ToString()!="")
				{
					model.dj=decimal.Parse(dt.Rows[n]["dj"].ToString());
				}
																																if(dt.Rows[n]["ffreeflag"].ToString()!="")
				{
					model.ffreeflag=int.Parse(dt.Rows[n]["ffreeflag"].ToString());
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