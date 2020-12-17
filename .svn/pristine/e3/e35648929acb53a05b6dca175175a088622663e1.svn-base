using System; 
using System.Text;
using System.Collections.Generic; 
using System.Data;
using SelfhelpOrderMgr.Model;
namespace SelfhelpOrderMgr.BLL {
	 	//T_TempLilun
		public partial class T_TempLilunBLL
	{
   		     
		private readonly SelfhelpOrderMgr.DAL.T_TempLilunDAL dal=new SelfhelpOrderMgr.DAL.T_TempLilunDAL();
		public T_TempLilunBLL()
		{}
		
		#region  Method


		/// <summary>
		/// 增加一条数据
		/// </summary>
		public void  Add(SelfhelpOrderMgr.Model.T_TempLilun model)
		{
						dal.Add(model);
						
		}

		/// <summary>
		/// 更新一条数据
		/// </summary>
		public bool Update(SelfhelpOrderMgr.Model.T_TempLilun model)
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
		public SelfhelpOrderMgr.Model.T_TempLilun GetModel()
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
		public List<SelfhelpOrderMgr.Model.T_TempLilun> GetModelList(string strWhere)
		{
			DataSet ds = dal.GetList(strWhere);
			return DataTableToList(ds.Tables[0]);
		}
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public List<SelfhelpOrderMgr.Model.T_TempLilun> DataTableToList(DataTable dt)
		{
			List<SelfhelpOrderMgr.Model.T_TempLilun> modelList = new List<SelfhelpOrderMgr.Model.T_TempLilun>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				SelfhelpOrderMgr.Model.T_TempLilun model;
				for (int n = 0; n < rowsCount; n++)
				{
					model = new SelfhelpOrderMgr.Model.T_TempLilun();					
																	model.gcode= dt.Rows[n]["gcode"].ToString();
																																model.gname= dt.Rows[n]["gname"].ToString();
																												if(dt.Rows[n]["price"].ToString()!="")
				{
					model.price=decimal.Parse(dt.Rows[n]["price"].ToString());
				}
																																if(dt.Rows[n]["gnum"].ToString()!="")
				{
					model.gnum=decimal.Parse(dt.Rows[n]["gnum"].ToString());
				}
																																if(dt.Rows[n]["gCurMonthPrice"].ToString()!="")
				{
					model.gCurMonthPrice=decimal.Parse(dt.Rows[n]["gCurMonthPrice"].ToString());
				}
																																if(dt.Rows[n]["lilun"].ToString()!="")
				{
					model.lilun=decimal.Parse(dt.Rows[n]["lilun"].ToString());
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