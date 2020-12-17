using System; 
using System.Text;
using System.Collections.Generic; 
using System.Data;
using SelfhelpOrderMgr.Model;
namespace SelfhelpOrderMgr.BLL {
	 	//T_MEET_TYPE
		public partial class T_MEET_TYPEBLL
	{
   		     
		private readonly SelfhelpOrderMgr.DAL.T_MEET_TYPEDAL dal=new SelfhelpOrderMgr.DAL.T_MEET_TYPEDAL();
		public T_MEET_TYPEBLL()
		{}
		
		#region  Method


		/// <summary>
		/// 增加一条数据
		/// </summary>
		public void  Add(SelfhelpOrderMgr.Model.T_MEET_TYPE model)
		{
						dal.Add(model);
						
		}

		/// <summary>
		/// 更新一条数据
		/// </summary>
		public bool Update(SelfhelpOrderMgr.Model.T_MEET_TYPE model)
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
		public SelfhelpOrderMgr.Model.T_MEET_TYPE GetModel()
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
		public List<SelfhelpOrderMgr.Model.T_MEET_TYPE> GetModelList(string strWhere)
		{
			DataSet ds = dal.GetList(strWhere);
			return DataTableToList(ds.Tables[0]);
		}
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public List<SelfhelpOrderMgr.Model.T_MEET_TYPE> DataTableToList(DataTable dt)
		{
			List<SelfhelpOrderMgr.Model.T_MEET_TYPE> modelList = new List<SelfhelpOrderMgr.Model.T_MEET_TYPE>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				SelfhelpOrderMgr.Model.T_MEET_TYPE model;
				for (int n = 0; n < rowsCount; n++)
				{
					model = new SelfhelpOrderMgr.Model.T_MEET_TYPE();					
																	model.FCode= dt.Rows[n]["FCode"].ToString();
																																model.FName= dt.Rows[n]["FName"].ToString();
																												if(dt.Rows[n]["FPeriord"].ToString()!="")
				{
					model.FPeriord=int.Parse(dt.Rows[n]["FPeriord"].ToString());
				}
																																if(dt.Rows[n]["FSpecial"].ToString()!="")
				{
					model.FSpecial=int.Parse(dt.Rows[n]["FSpecial"].ToString());
				}
																																				model.FAddrCode= dt.Rows[n]["FAddrCode"].ToString();
																																model.FDesc= dt.Rows[n]["FDesc"].ToString();
																						
				
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