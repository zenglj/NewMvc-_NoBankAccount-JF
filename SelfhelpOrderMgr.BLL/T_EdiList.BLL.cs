using System; 
using System.Text;
using System.Collections.Generic; 
using System.Data;
using SelfhelpOrderMgr.Model;
namespace SelfhelpOrderMgr.BLL {
	 	//T_EdiList
		public partial class T_EdiListBLL
	{
   		     
		private readonly SelfhelpOrderMgr.DAL.T_EdiListDAL dal=new SelfhelpOrderMgr.DAL.T_EdiListDAL();
		public T_EdiListBLL()
		{}
		
		#region  Method


		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int  Add(SelfhelpOrderMgr.Model.T_EdiList model)
		{
						return dal.Add(model);
						
		}

		/// <summary>
		/// 更新一条数据
		/// </summary>
		public bool Update(SelfhelpOrderMgr.Model.T_EdiList model)
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
		public SelfhelpOrderMgr.Model.T_EdiList GetModel(int seqno)
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
		public List<SelfhelpOrderMgr.Model.T_EdiList> GetModelList(string strWhere)
		{
			DataSet ds = dal.GetList(strWhere);
			return DataTableToList(ds.Tables[0]);
		}
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public List<SelfhelpOrderMgr.Model.T_EdiList> DataTableToList(DataTable dt)
		{
			List<SelfhelpOrderMgr.Model.T_EdiList> modelList = new List<SelfhelpOrderMgr.Model.T_EdiList>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				SelfhelpOrderMgr.Model.T_EdiList model;
				for (int n = 0; n < rowsCount; n++)
				{
					model = new SelfhelpOrderMgr.Model.T_EdiList();					
													if(dt.Rows[n]["seqno"].ToString()!="")
				{
					model.seqno=int.Parse(dt.Rows[n]["seqno"].ToString());
				}
																																				model.Code= dt.Rows[n]["Code"].ToString();
																																model.sfile= dt.Rows[n]["sfile"].ToString();
																												if(dt.Rows[n]["UpLoadDate"].ToString()!="")
				{
					model.UpLoadDate=DateTime.Parse(dt.Rows[n]["UpLoadDate"].ToString());
				}
																																if(dt.Rows[n]["DownLoadDate"].ToString()!="")
				{
					model.DownLoadDate=DateTime.Parse(dt.Rows[n]["DownLoadDate"].ToString());
				}
																																if(dt.Rows[n]["UploadFlag"].ToString()!="")
				{
					model.UploadFlag=int.Parse(dt.Rows[n]["UploadFlag"].ToString());
				}
																																if(dt.Rows[n]["DownLoadFlag"].ToString()!="")
				{
					model.DownLoadFlag=int.Parse(dt.Rows[n]["DownLoadFlag"].ToString());
				}
																																if(dt.Rows[n]["InOutFlag"].ToString()!="")
				{
					model.InOutFlag=int.Parse(dt.Rows[n]["InOutFlag"].ToString());
				}
																																if(dt.Rows[n]["MainFlag"].ToString()!="")
				{
					model.MainFlag=int.Parse(dt.Rows[n]["MainFlag"].ToString());
				}
																																				model.DetailFile= dt.Rows[n]["DetailFile"].ToString();
																												if(dt.Rows[n]["Mainseqno"].ToString()!="")
				{
					model.Mainseqno=int.Parse(dt.Rows[n]["Mainseqno"].ToString());
				}
																																if(dt.Rows[n]["DetailUploadflag"].ToString()!="")
				{
					model.DetailUploadflag=int.Parse(dt.Rows[n]["DetailUploadflag"].ToString());
				}
																																if(dt.Rows[n]["DetailDownloadFlag"].ToString()!="")
				{
					model.DetailDownloadFlag=int.Parse(dt.Rows[n]["DetailDownloadFlag"].ToString());
				}
																																				model.DCFLAG= dt.Rows[n]["DCFLAG"].ToString();
																																model.AccCode= dt.Rows[n]["AccCode"].ToString();
																												if(dt.Rows[n]["Succflag"].ToString()!="")
				{
					model.Succflag=int.Parse(dt.Rows[n]["Succflag"].ToString());
				}
																																if(dt.Rows[n]["DetailUploadDate"].ToString()!="")
				{
					model.DetailUploadDate=DateTime.Parse(dt.Rows[n]["DetailUploadDate"].ToString());
				}
																																if(dt.Rows[n]["DetailDownLoadDate"].ToString()!="")
				{
					model.DetailDownLoadDate=DateTime.Parse(dt.Rows[n]["DetailDownLoadDate"].ToString());
				}
																																				model.modecode= dt.Rows[n]["modecode"].ToString();
																												if(dt.Rows[n]["datadate"].ToString()!="")
				{
					model.datadate=DateTime.Parse(dt.Rows[n]["datadate"].ToString());
				}
																																if(dt.Rows[n]["resetflag"].ToString()!="")
				{
					model.resetflag=int.Parse(dt.Rows[n]["resetflag"].ToString());
				}
																																				model.feecode= dt.Rows[n]["feecode"].ToString();
																																model.remark= dt.Rows[n]["remark"].ToString();
																												if(dt.Rows[n]["crtdate"].ToString()!="")
				{
					model.crtdate=DateTime.Parse(dt.Rows[n]["crtdate"].ToString());
				}
																																if(dt.Rows[n]["typeflag"].ToString()!="")
				{
					model.typeflag=int.Parse(dt.Rows[n]["typeflag"].ToString());
				}
																																if(dt.Rows[n]["subTypeFlag"].ToString()!="")
				{
					model.subTypeFlag=int.Parse(dt.Rows[n]["subTypeFlag"].ToString());
				}
																																if(dt.Rows[n]["balflag"].ToString()!="")
				{
					model.balflag=int.Parse(dt.Rows[n]["balflag"].ToString());
				}
																																if(dt.Rows[n]["rseqno"].ToString()!="")
				{
					model.rseqno=int.Parse(dt.Rows[n]["rseqno"].ToString());
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