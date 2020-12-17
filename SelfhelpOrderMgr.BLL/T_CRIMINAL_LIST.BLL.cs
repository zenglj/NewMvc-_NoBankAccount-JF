using System; 
using System.Text;
using System.Collections.Generic; 
using System.Data;
using SelfhelpOrderMgr.Model;
namespace SelfhelpOrderMgr.BLL {
	 	//T_CRIMINAL_LIST
		public partial class T_CRIMINAL_LISTBLL
	{
   		     
		private readonly SelfhelpOrderMgr.DAL.T_CRIMINAL_LISTDAL dal=new SelfhelpOrderMgr.DAL.T_CRIMINAL_LISTDAL();
		public T_CRIMINAL_LISTBLL()
		{}
		
		#region  Method


		/// <summary>
		/// 增加一条数据
		/// </summary>
		public void  Add(SelfhelpOrderMgr.Model.T_CRIMINAL_LIST model)
		{
						dal.Add(model);
						
		}

		/// <summary>
		/// 更新一条数据
		/// </summary>
		public bool Update(SelfhelpOrderMgr.Model.T_CRIMINAL_LIST model)
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
		public SelfhelpOrderMgr.Model.T_CRIMINAL_LIST GetModel()
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
		public List<SelfhelpOrderMgr.Model.T_CRIMINAL_LIST> GetModelList(string strWhere)
		{
			DataSet ds = dal.GetList(strWhere);
			return DataTableToList(ds.Tables[0]);
		}
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public List<SelfhelpOrderMgr.Model.T_CRIMINAL_LIST> DataTableToList(DataTable dt)
		{
			List<SelfhelpOrderMgr.Model.T_CRIMINAL_LIST> modelList = new List<SelfhelpOrderMgr.Model.T_CRIMINAL_LIST>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				SelfhelpOrderMgr.Model.T_CRIMINAL_LIST model;
				for (int n = 0; n < rowsCount; n++)
				{
					model = new SelfhelpOrderMgr.Model.T_CRIMINAL_LIST();					
																	model.FCode= dt.Rows[n]["FCode"].ToString();
																																model.FName= dt.Rows[n]["FName"].ToString();
																																model.FIdenNo= dt.Rows[n]["FIdenNo"].ToString();
																												if(dt.Rows[n]["FAge"].ToString()!="")
				{
					model.FAge=int.Parse(dt.Rows[n]["FAge"].ToString());
				}
																																				model.FSex= dt.Rows[n]["FSex"].ToString();
																																model.FAddr= dt.Rows[n]["FAddr"].ToString();
																																model.FCrimeCode= dt.Rows[n]["FCrimeCode"].ToString();
																																model.FCYCode= dt.Rows[n]["FCYCode"].ToString();
																																model.FTerm= dt.Rows[n]["FTerm"].ToString();
																												if(dt.Rows[n]["FInDate"].ToString()!="")
				{
					model.FInDate=DateTime.Parse(dt.Rows[n]["FInDate"].ToString());
				}
																																if(dt.Rows[n]["FOuDate"].ToString()!="")
				{
					model.FOuDate=DateTime.Parse(dt.Rows[n]["FOuDate"].ToString());
				}
																																				model.FAreaCode= dt.Rows[n]["FAreaCode"].ToString();
																																model.FSubArea= dt.Rows[n]["FSubArea"].ToString();
																																model.FDesc= dt.Rows[n]["FDesc"].ToString();
																												if(dt.Rows[n]["FStatus"].ToString()!="")
				{
					model.FStatus=int.Parse(dt.Rows[n]["FStatus"].ToString());
				}
																																if(dt.Rows[n]["FStatus2"].ToString()!="")
				{
					model.FStatus2=int.Parse(dt.Rows[n]["FStatus2"].ToString());
				}
																																				model.FAddr_tmp= dt.Rows[n]["FAddr_tmp"].ToString();
																																model.fczy= dt.Rows[n]["fczy"].ToString();
																												if(dt.Rows[n]["FModDate"].ToString()!="")
				{
					model.FModDate=DateTime.Parse(dt.Rows[n]["FModDate"].ToString());
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