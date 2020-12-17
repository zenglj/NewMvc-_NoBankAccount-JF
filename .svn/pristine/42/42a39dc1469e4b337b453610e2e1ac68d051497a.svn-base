using System; 
using System.Text;
using System.Collections.Generic; 
using System.Data;
using SelfhelpOrderMgr.Model;
namespace SelfhelpOrderMgr.BLL {
	 	//t_FeeList
		public partial class t_FeeListBLL
	{
   		     
		private readonly SelfhelpOrderMgr.DAL.t_FeeListDAL dal=new SelfhelpOrderMgr.DAL.t_FeeListDAL();
		public t_FeeListBLL()
		{}
		
		#region  Method


		/// <summary>
		/// 增加一条数据
		/// </summary>
		public void  Add(SelfhelpOrderMgr.Model.t_FeeList model)
		{
						dal.Add(model);
						
		}

		/// <summary>
		/// 更新一条数据
		/// </summary>
		public bool Update(SelfhelpOrderMgr.Model.t_FeeList model)
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
		public SelfhelpOrderMgr.Model.t_FeeList GetModel()
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
		public List<SelfhelpOrderMgr.Model.t_FeeList> GetModelList(string strWhere)
		{
			DataSet ds = dal.GetList(strWhere);
			return DataTableToList(ds.Tables[0]);
		}
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public List<SelfhelpOrderMgr.Model.t_FeeList> DataTableToList(DataTable dt)
		{
			List<SelfhelpOrderMgr.Model.t_FeeList> modelList = new List<SelfhelpOrderMgr.Model.t_FeeList>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				SelfhelpOrderMgr.Model.t_FeeList model;
				for (int n = 0; n < rowsCount; n++)
				{
					model = new SelfhelpOrderMgr.Model.t_FeeList();					
													if(dt.Rows[n]["seqno"].ToString()!="")
				{
					model.seqno=int.Parse(dt.Rows[n]["seqno"].ToString());
				}
																																if(dt.Rows[n]["code"].ToString()!="")
				{
					model.code=int.Parse(dt.Rows[n]["code"].ToString());
				}
																																if(dt.Rows[n]["subcode"].ToString()!="")
				{
					model.subcode=int.Parse(dt.Rows[n]["subcode"].ToString());
				}
																																				model.fname= dt.Rows[n]["fname"].ToString();
																												if(dt.Rows[n]["DCFLAG"].ToString()!="")
				{
					model.DCFLAG=int.Parse(dt.Rows[n]["DCFLAG"].ToString());
				}
																																				model.Subflag= dt.Rows[n]["Subflag"].ToString();
																												if(dt.Rows[n]["levelid"].ToString()!="")
				{
					model.levelid=int.Parse(dt.Rows[n]["levelid"].ToString());
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