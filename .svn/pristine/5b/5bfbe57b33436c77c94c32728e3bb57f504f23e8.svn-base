using System; 
using System.Text;
using System.Collections.Generic; 
using System.Data;
using SelfhelpOrderMgr.Model;
namespace SelfhelpOrderMgr.BLL {
	 	//T_Vcrd_error
		public partial class T_Vcrd_errorBLL
	{
   		     
		private readonly SelfhelpOrderMgr.DAL.T_Vcrd_errorDAL dal=new SelfhelpOrderMgr.DAL.T_Vcrd_errorDAL();
		public T_Vcrd_errorBLL()
		{}
		
		#region  Method


		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int  Add(SelfhelpOrderMgr.Model.T_Vcrd_error model)
		{
						return dal.Add(model);
						
		}

		/// <summary>
		/// 更新一条数据
		/// </summary>
		public bool Update(SelfhelpOrderMgr.Model.T_Vcrd_error model)
		{
			return dal.Update(model);
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public bool Delete(int seqno)
		{
			
			return dal.Delete(seqno);
		}
				/// <summary>
		/// 批量删除一批数据
		/// </summary>
		public bool DeleteList(string seqnolist )
		{
			return dal.DeleteList(seqnolist );
		}
		
		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public SelfhelpOrderMgr.Model.T_Vcrd_error GetModel(int seqno)
		{
			
			return dal.GetModel(seqno);
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
		public List<SelfhelpOrderMgr.Model.T_Vcrd_error> GetModelList(string strWhere)
		{
			DataSet ds = dal.GetList(strWhere);
			return DataTableToList(ds.Tables[0]);
		}
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public List<SelfhelpOrderMgr.Model.T_Vcrd_error> DataTableToList(DataTable dt)
		{
			List<SelfhelpOrderMgr.Model.T_Vcrd_error> modelList = new List<SelfhelpOrderMgr.Model.T_Vcrd_error>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				SelfhelpOrderMgr.Model.T_Vcrd_error model;
				for (int n = 0; n < rowsCount; n++)
				{
					model = new SelfhelpOrderMgr.Model.T_Vcrd_error();					
													if(dt.Rows[n]["seqno"].ToString()!="")
				{
					model.seqno=int.Parse(dt.Rows[n]["seqno"].ToString());
				}
																																				model.fcrimecode= dt.Rows[n]["fcrimecode"].ToString();
																																model.fcriminal= dt.Rows[n]["fcriminal"].ToString();
																												if(dt.Rows[n]["famount"].ToString()!="")
				{
					model.famount=decimal.Parse(dt.Rows[n]["famount"].ToString());
				}
																																if(dt.Rows[n]["famounta"].ToString()!="")
				{
					model.famounta=decimal.Parse(dt.Rows[n]["famounta"].ToString());
				}
																																if(dt.Rows[n]["famountb"].ToString()!="")
				{
					model.famountb=decimal.Parse(dt.Rows[n]["famountb"].ToString());
				}
																																				model.Remark= dt.Rows[n]["Remark"].ToString();
																																model.Crtby= dt.Rows[n]["Crtby"].ToString();
																												if(dt.Rows[n]["crtdate"].ToString()!="")
				{
					model.crtdate=DateTime.Parse(dt.Rows[n]["crtdate"].ToString());
				}
																																if(dt.Rows[n]["pc"].ToString()!="")
				{
					model.pc=int.Parse(dt.Rows[n]["pc"].ToString());
				}
																																if(dt.Rows[n]["typeflag"].ToString()!="")
				{
					model.typeflag=int.Parse(dt.Rows[n]["typeflag"].ToString());
				}
																																if(dt.Rows[n]["acctype"].ToString()!="")
				{
					model.acctype=int.Parse(dt.Rows[n]["acctype"].ToString());
				}
																																				model.notes= dt.Rows[n]["notes"].ToString();
																						
				
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