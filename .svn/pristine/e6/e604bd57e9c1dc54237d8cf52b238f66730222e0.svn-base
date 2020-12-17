using System; 
using System.Text;
using System.Collections.Generic; 
using System.Data;
using SelfhelpOrderMgr.Model;
namespace SelfhelpOrderMgr.BLL {
	 	//T_MEET_AREA
		public partial class T_MEET_AREABLL
	{
   		     
		private readonly SelfhelpOrderMgr.DAL.T_MEET_AREADAL dal=new SelfhelpOrderMgr.DAL.T_MEET_AREADAL();
		public T_MEET_AREABLL()
		{}
		
		#region  Method


		/// <summary>
		/// 增加一条数据
		/// </summary>
		public void  Add(SelfhelpOrderMgr.Model.T_MEET_AREA model)
		{
						dal.Add(model);
						
		}

		/// <summary>
		/// 更新一条数据
		/// </summary>
		public bool Update(SelfhelpOrderMgr.Model.T_MEET_AREA model)
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
		public SelfhelpOrderMgr.Model.T_MEET_AREA GetModel()
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
		public List<SelfhelpOrderMgr.Model.T_MEET_AREA> GetModelList(string strWhere)
		{
			DataSet ds = dal.GetList(strWhere);
			return DataTableToList(ds.Tables[0]);
		}
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public List<SelfhelpOrderMgr.Model.T_MEET_AREA> DataTableToList(DataTable dt)
		{
			List<SelfhelpOrderMgr.Model.T_MEET_AREA> modelList = new List<SelfhelpOrderMgr.Model.T_MEET_AREA>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				SelfhelpOrderMgr.Model.T_MEET_AREA model;
				for (int n = 0; n < rowsCount; n++)
				{
					model = new SelfhelpOrderMgr.Model.T_MEET_AREA();					
																	model.FCode= dt.Rows[n]["FCode"].ToString();
																																model.FName= dt.Rows[n]["FName"].ToString();
																																model.FFlag= dt.Rows[n]["FFlag"].ToString();
																												if(dt.Rows[n]["FMain"].ToString()!="")
				{
					model.FMain=int.Parse(dt.Rows[n]["FMain"].ToString());
				}
																																if(dt.Rows[n]["FSub"].ToString()!="")
				{
					model.FSub=int.Parse(dt.Rows[n]["FSub"].ToString());
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