using System; 
using System.Text;
using System.Collections.Generic; 
using System.Data;
using SelfhelpOrderMgr.Model;
namespace SelfhelpOrderMgr.BLL {
	 	//T_CARD_LIST
		public partial class T_CARD_LISTBLL
	{
   		     
		private readonly SelfhelpOrderMgr.DAL.T_CARD_LISTDAL dal=new SelfhelpOrderMgr.DAL.T_CARD_LISTDAL();
		public T_CARD_LISTBLL()
		{}
		
		#region  Method


		/// <summary>
		/// 增加一条数据
		/// </summary>
		public void  Add(SelfhelpOrderMgr.Model.T_CARD_LIST model)
		{
						dal.Add(model);
						
		}

		/// <summary>
		/// 更新一条数据
		/// </summary>
		public bool Update(SelfhelpOrderMgr.Model.T_CARD_LIST model)
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
		public SelfhelpOrderMgr.Model.T_CARD_LIST GetModel()
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
		public List<SelfhelpOrderMgr.Model.T_CARD_LIST> GetModelList(string strWhere)
		{
			DataSet ds = dal.GetList(strWhere);
			return DataTableToList(ds.Tables[0]);
		}
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public List<SelfhelpOrderMgr.Model.T_CARD_LIST> DataTableToList(DataTable dt)
		{
			List<SelfhelpOrderMgr.Model.T_CARD_LIST> modelList = new List<SelfhelpOrderMgr.Model.T_CARD_LIST>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				SelfhelpOrderMgr.Model.T_CARD_LIST model;
				for (int n = 0; n < rowsCount; n++)
				{
					model = new SelfhelpOrderMgr.Model.T_CARD_LIST();					
													if(dt.Rows[n]["FSN"].ToString()!="")
				{
					model.FSN=int.Parse(dt.Rows[n]["FSN"].ToString());
				}
																																				model.FCardCode= dt.Rows[n]["FCardCode"].ToString();
																																model.FCrimeCode= dt.Rows[n]["FCrimeCode"].ToString();
																												if(dt.Rows[n]["FRDate"].ToString()!="")
				{
					model.FRDate=DateTime.Parse(dt.Rows[n]["FRDate"].ToString());
				}
																																if(dt.Rows[n]["FFlag"].ToString()!="")
				{
					model.FFlag=int.Parse(dt.Rows[n]["FFlag"].ToString());
				}
																																				model.FCzy= dt.Rows[n]["FCzy"].ToString();
																						
				
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