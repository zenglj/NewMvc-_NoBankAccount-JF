using System; 
using System.Text;
using System.Collections.Generic; 
using System.Data;
using SelfhelpOrderMgr.Model;
namespace SelfhelpOrderMgr.BLL {
	 	//T_TEST_DB
		public partial class T_TEST_DBBLL
	{
   		     
		private readonly SelfhelpOrderMgr.DAL.T_TEST_DBDAL dal=new SelfhelpOrderMgr.DAL.T_TEST_DBDAL();
		public T_TEST_DBBLL()
		{}
		
		#region  Method


		/// <summary>
		/// 增加一条数据
		/// </summary>
		public void  Add(SelfhelpOrderMgr.Model.T_TEST_DB model)
		{
						dal.Add(model);
						
		}

		/// <summary>
		/// 更新一条数据
		/// </summary>
		public bool Update(SelfhelpOrderMgr.Model.T_TEST_DB model)
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
		public SelfhelpOrderMgr.Model.T_TEST_DB GetModel()
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
		public List<SelfhelpOrderMgr.Model.T_TEST_DB> GetModelList(string strWhere)
		{
			DataSet ds = dal.GetList(strWhere);
			return DataTableToList(ds.Tables[0]);
		}
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public List<SelfhelpOrderMgr.Model.T_TEST_DB> DataTableToList(DataTable dt)
		{
			List<SelfhelpOrderMgr.Model.T_TEST_DB> modelList = new List<SelfhelpOrderMgr.Model.T_TEST_DB>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				SelfhelpOrderMgr.Model.T_TEST_DB model;
				for (int n = 0; n < rowsCount; n++)
				{
					model = new SelfhelpOrderMgr.Model.T_TEST_DB();					
																	model.name= dt.Rows[n]["name"].ToString();
																																				if(dt.Rows[n]["picdata1"].ToString()!="")
				{
					model.picdata1= (byte[])dt.Rows[n]["picdata1"];
				}
																																if(dt.Rows[n]["picdata2"].ToString()!="")
				{
					model.picdata2= (byte[])dt.Rows[n]["picdata2"];
				}
																												model.picdata3= dt.Rows[n]["picdata3"].ToString();
																						
				
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