using System; 
using System.Text;
using System.Collections.Generic; 
using System.Data;
using SelfhelpOrderMgr.Model;
namespace SelfhelpOrderMgr.BLL {
	 	//T_FAMILY_LIST
		public partial class T_FAMILY_LISTBLL
	{
   		     
		private readonly SelfhelpOrderMgr.DAL.T_FAMILY_LISTDAL dal=new SelfhelpOrderMgr.DAL.T_FAMILY_LISTDAL();
		public T_FAMILY_LISTBLL()
		{}
		
		#region  Method


		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int  Add(SelfhelpOrderMgr.Model.T_FAMILY_LIST model)
		{
						return dal.Add(model);
						
		}

		/// <summary>
		/// 更新一条数据
		/// </summary>
		public bool Update(SelfhelpOrderMgr.Model.T_FAMILY_LIST model)
		{
			return dal.Update(model);
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public bool Delete(int FCode)
		{
			
			return dal.Delete(FCode);
		}
				/// <summary>
		/// 批量删除一批数据
		/// </summary>
		public bool DeleteList(string FCodelist )
		{
			return dal.DeleteList(FCodelist );
		}
		
		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public SelfhelpOrderMgr.Model.T_FAMILY_LIST GetModel(int FCode)
		{
			
			return dal.GetModel(FCode);
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
		public List<SelfhelpOrderMgr.Model.T_FAMILY_LIST> GetModelList(string strWhere)
		{
			DataSet ds = dal.GetList(strWhere);
			return DataTableToList(ds.Tables[0]);
		}
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public List<SelfhelpOrderMgr.Model.T_FAMILY_LIST> DataTableToList(DataTable dt)
		{
			List<SelfhelpOrderMgr.Model.T_FAMILY_LIST> modelList = new List<SelfhelpOrderMgr.Model.T_FAMILY_LIST>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				SelfhelpOrderMgr.Model.T_FAMILY_LIST model;
				for (int n = 0; n < rowsCount; n++)
				{
					model = new SelfhelpOrderMgr.Model.T_FAMILY_LIST();					
													if(dt.Rows[n]["FCode"].ToString()!="")
				{
					model.FCode=int.Parse(dt.Rows[n]["FCode"].ToString());
				}
																																				model.FCrimeCode= dt.Rows[n]["FCrimeCode"].ToString();
																																model.FName= dt.Rows[n]["FName"].ToString();
																																model.FIdenNo= dt.Rows[n]["FIdenNo"].ToString();
																																model.FSex= dt.Rows[n]["FSex"].ToString();
																																model.FAddr= dt.Rows[n]["FAddr"].ToString();
																																model.FRelation= dt.Rows[n]["FRelation"].ToString();
																																model.FDesc= dt.Rows[n]["FDesc"].ToString();
																												if(dt.Rows[n]["FStatus"].ToString()!="")
				{
					model.FStatus=int.Parse(dt.Rows[n]["FStatus"].ToString());
				}
																																				model.FAddr_tmp= dt.Rows[n]["FAddr_tmp"].ToString();
																																				if(dt.Rows[n]["PICDATA"].ToString()!="")
				{
					model.PICDATA= (byte[])dt.Rows[n]["PICDATA"];
				}
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