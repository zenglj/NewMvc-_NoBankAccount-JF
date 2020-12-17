using System; 
using System.Text;
using System.Collections.Generic; 
using System.Data;
using SelfhelpOrderMgr.Model;
namespace SelfhelpOrderMgr.BLL {
	 	//T_EdiDetail
		public partial class T_EdiDetailBLL
	{
   		     
		private readonly SelfhelpOrderMgr.DAL.T_EdiDetailDAL dal=new SelfhelpOrderMgr.DAL.T_EdiDetailDAL();
		public T_EdiDetailBLL()
		{}
		
		#region  Method


		/// <summary>
		/// 增加一条数据
		/// </summary>
		public long  Add(SelfhelpOrderMgr.Model.T_EdiDetail model)
		{
						return dal.Add(model);
						
		}

		/// <summary>
		/// 更新一条数据
		/// </summary>
		public bool Update(SelfhelpOrderMgr.Model.T_EdiDetail model)
		{
			return dal.Update(model);
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public bool Delete(long ID)
		{
			
			return dal.Delete(ID);
		}
				/// <summary>
		/// 批量删除一批数据
		/// </summary>
		public bool DeleteList(string IDlist )
		{
			return dal.DeleteList(IDlist );
		}
		
		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public SelfhelpOrderMgr.Model.T_EdiDetail GetModel(long ID)
		{
			
			return dal.GetModel(ID);
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
		public List<SelfhelpOrderMgr.Model.T_EdiDetail> GetModelList(string strWhere)
		{
			DataSet ds = dal.GetList(strWhere);
			return DataTableToList(ds.Tables[0]);
		}
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public List<SelfhelpOrderMgr.Model.T_EdiDetail> DataTableToList(DataTable dt)
		{
			List<SelfhelpOrderMgr.Model.T_EdiDetail> modelList = new List<SelfhelpOrderMgr.Model.T_EdiDetail>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				SelfhelpOrderMgr.Model.T_EdiDetail model;
				for (int n = 0; n < rowsCount; n++)
				{
					model = new SelfhelpOrderMgr.Model.T_EdiDetail();					
													if(dt.Rows[n]["ID"].ToString()!="")
				{
					model.ID=long.Parse(dt.Rows[n]["ID"].ToString());
				}
																																if(dt.Rows[n]["seqno"].ToString()!="")
				{
					model.seqno=int.Parse(dt.Rows[n]["seqno"].ToString());
				}
																																if(dt.Rows[n]["Mainseqno"].ToString()!="")
				{
					model.Mainseqno=int.Parse(dt.Rows[n]["Mainseqno"].ToString());
				}
																																				model.vouno= dt.Rows[n]["vouno"].ToString();
																												if(dt.Rows[n]["DAMOUNT"].ToString()!="")
				{
					model.DAMOUNT=decimal.Parse(dt.Rows[n]["DAMOUNT"].ToString());
				}
																																if(dt.Rows[n]["CAMOUNT"].ToString()!="")
				{
					model.CAMOUNT=decimal.Parse(dt.Rows[n]["CAMOUNT"].ToString());
				}
																																				model.FCRIMECODE= dt.Rows[n]["FCRIMECODE"].ToString();
																												if(dt.Rows[n]["TYPEFLAG"].ToString()!="")
				{
					model.TYPEFLAG=int.Parse(dt.Rows[n]["TYPEFLAG"].ToString());
				}
																																if(dt.Rows[n]["SubTypeflag"].ToString()!="")
				{
					model.SubTypeflag=int.Parse(dt.Rows[n]["SubTypeflag"].ToString());
				}
																																				model.AccCode= dt.Rows[n]["AccCode"].ToString();
																																model.origid= dt.Rows[n]["origid"].ToString();
																												if(dt.Rows[n]["SuccFlag"].ToString()!="")
				{
					model.SuccFlag=int.Parse(dt.Rows[n]["SuccFlag"].ToString());
				}
																																if(dt.Rows[n]["vcrdseqno"].ToString()!="")
				{
					model.vcrdseqno=int.Parse(dt.Rows[n]["vcrdseqno"].ToString());
				}
																																				model.remark= dt.Rows[n]["remark"].ToString();
																																model.fareacode= dt.Rows[n]["fareacode"].ToString();
																						
				
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