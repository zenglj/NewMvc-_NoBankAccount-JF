using System; 
using System.Text;
using System.Collections.Generic; 
using System.Data;
using SelfhelpOrderMgr.Model;
namespace SelfhelpOrderMgr.BLL {
	 	//t_TreeMeun
		public partial class t_TreeMeunBLL
	{
   		     
		private readonly SelfhelpOrderMgr.DAL.t_TreeMeunDAL dal=new SelfhelpOrderMgr.DAL.t_TreeMeunDAL();
		public t_TreeMeunBLL()
		{}
		
		#region  Method


		/// <summary>
		/// 增加一条数据
		/// </summary>
		public void  Add(SelfhelpOrderMgr.Model.t_TreeMeun model)
		{
						dal.Add(model);
						
		}

		/// <summary>
		/// 更新一条数据
		/// </summary>
		public bool Update(SelfhelpOrderMgr.Model.t_TreeMeun model)
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
		public SelfhelpOrderMgr.Model.t_TreeMeun GetModel(int treeId)
		{
			
			return dal.GetModel(treeId);
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
		public List<SelfhelpOrderMgr.Model.t_TreeMeun> GetModelList(string strWhere)
		{
			DataSet ds = dal.GetList(strWhere);
			return DataTableToList(ds.Tables[0]);
		}
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public List<SelfhelpOrderMgr.Model.t_TreeMeun> DataTableToList(DataTable dt)
		{
			List<SelfhelpOrderMgr.Model.t_TreeMeun> modelList = new List<SelfhelpOrderMgr.Model.t_TreeMeun>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				SelfhelpOrderMgr.Model.t_TreeMeun model;
				for (int n = 0; n < rowsCount; n++)
				{
					model = new SelfhelpOrderMgr.Model.t_TreeMeun();					
													if(dt.Rows[n]["id"].ToString()!="")
				{
					model.id=int.Parse(dt.Rows[n]["id"].ToString());
				}
																																				model.fcode= dt.Rows[n]["fcode"].ToString();
																												if(dt.Rows[n]["flag"].ToString()!="")
				{
					model.flag=int.Parse(dt.Rows[n]["flag"].ToString());
				}
																																				model.Text= dt.Rows[n]["Text"].ToString();
																												if(dt.Rows[n]["FId"].ToString()!="")
				{
					model.FId=int.Parse(dt.Rows[n]["FId"].ToString());
				}
																																				model.URL= dt.Rows[n]["URL"].ToString();
																						
				
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