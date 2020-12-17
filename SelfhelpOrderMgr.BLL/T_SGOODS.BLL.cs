using System; 
using System.Text;
using System.Collections.Generic; 
using System.Data;
using SelfhelpOrderMgr.Model;
namespace SelfhelpOrderMgr.BLL {
	 	//T_SGOODS
		public partial class T_SGOODSBLL
	{
   		     
		private readonly SelfhelpOrderMgr.DAL.T_SGOODSDAL dal=new SelfhelpOrderMgr.DAL.T_SGOODSDAL();
		public T_SGOODSBLL()
		{}
		
		#region  Method


		/// <summary>
		/// 增加一条数据
		/// </summary>
		public void  Add(SelfhelpOrderMgr.Model.T_SGOODS model)
		{
						dal.Add(model);
						
		}

		/// <summary>
		/// 更新一条数据
		/// </summary>
		public bool Update(SelfhelpOrderMgr.Model.T_SGOODS model)
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
		public SelfhelpOrderMgr.Model.T_SGOODS GetModel()
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
		public List<SelfhelpOrderMgr.Model.T_SGOODS> GetModelList(string strWhere)
		{
			DataSet ds = dal.GetList(strWhere);
			return DataTableToList(ds.Tables[0]);
		}
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public List<SelfhelpOrderMgr.Model.T_SGOODS> DataTableToList(DataTable dt)
		{
			List<SelfhelpOrderMgr.Model.T_SGOODS> modelList = new List<SelfhelpOrderMgr.Model.T_SGOODS>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				SelfhelpOrderMgr.Model.T_SGOODS model;
				for (int n = 0; n < rowsCount; n++)
				{
					model = new SelfhelpOrderMgr.Model.T_SGOODS();					
																	model.GCODE= dt.Rows[n]["GCODE"].ToString();
																																model.GNAME= dt.Rows[n]["GNAME"].ToString();
																																model.GTYPE= dt.Rows[n]["GTYPE"].ToString();
																																model.GUnit= dt.Rows[n]["GUnit"].ToString();
																												if(dt.Rows[n]["GDJ"].ToString()!="")
				{
					model.GDJ=decimal.Parse(dt.Rows[n]["GDJ"].ToString());
				}
																																				model.ACTIVE= dt.Rows[n]["ACTIVE"].ToString();
																												if(dt.Rows[n]["Ffreeflag"].ToString()!="")
				{
					model.Ffreeflag=int.Parse(dt.Rows[n]["Ffreeflag"].ToString());
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