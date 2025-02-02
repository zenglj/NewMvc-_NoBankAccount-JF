﻿using System; 
using System.Text;
using System.Collections.Generic; 
using System.Data;
using SelfhelpOrderMgr.Model;
namespace SelfhelpOrderMgr.BLL {
	 	//t_TreeRole_Menu
		public partial class t_TreeRole_MenuBLL
	{
   		     
		private readonly SelfhelpOrderMgr.DAL.t_TreeRole_MenuDAL dal=new SelfhelpOrderMgr.DAL.t_TreeRole_MenuDAL();
		public t_TreeRole_MenuBLL()
		{}
		
		#region  Method


		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int  Add(SelfhelpOrderMgr.Model.t_TreeRole_Menu model)
		{
						return dal.Add(model);
						
		}

		/// <summary>
		/// 更新一条数据
		/// </summary>
		public bool Update(SelfhelpOrderMgr.Model.t_TreeRole_Menu model)
		{
			return dal.Update(model);
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public bool Delete(int id)
		{
			
			return dal.Delete(id);
		}
				/// <summary>
		/// 批量删除一批数据
		/// </summary>
		public bool DeleteList(string idlist )
		{
			return dal.DeleteList(idlist );
		}
		
		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public SelfhelpOrderMgr.Model.t_TreeRole_Menu GetModel(int id)
		{
			
			return dal.GetModel(id);
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
		public List<SelfhelpOrderMgr.Model.t_TreeRole_Menu> GetModelList(string strWhere)
		{
			DataSet ds = dal.GetList(strWhere);
			return DataTableToList(ds.Tables[0]);
		}
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public List<SelfhelpOrderMgr.Model.t_TreeRole_Menu> DataTableToList(DataTable dt)
		{
			List<SelfhelpOrderMgr.Model.t_TreeRole_Menu> modelList = new List<SelfhelpOrderMgr.Model.t_TreeRole_Menu>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				SelfhelpOrderMgr.Model.t_TreeRole_Menu model;
				for (int n = 0; n < rowsCount; n++)
				{
					model = new SelfhelpOrderMgr.Model.t_TreeRole_Menu();					
				if(dt.Rows[n]["id"].ToString()!="")
				{
					model.id=int.Parse(dt.Rows[n]["id"].ToString());
				}
				if(dt.Rows[n]["RoleID"].ToString()!="")
				{
					model.RoleID=int.Parse(dt.Rows[n]["RoleID"].ToString());
				}
                if (dt.Rows[n]["TreeId"].ToString() != "")
                {
                    model.TreeId = int.Parse(dt.Rows[n]["TreeId"].ToString());
                    //List<t_TreeMeun> menus = new t_TreeMeunBLL().GetModelList("Id=" + model.TreeId.ToString());
                    //if (menus.Count > 0)
                    //{
                    //    model.Menu = menus[0];
                    //}
                }
				if(dt.Rows[n]["flag"].ToString()!="")
				{
					model.flag=int.Parse(dt.Rows[n]["flag"].ToString());
				}
                if (dt.Rows[n]["FID"].ToString() != "")
                {
                    model.FId = int.Parse(dt.Rows[n]["FID"].ToString());
                }
                model.URL = dt.Rows[n]["url"].ToString();
                model.Text = dt.Rows[n]["Text"].ToString();

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