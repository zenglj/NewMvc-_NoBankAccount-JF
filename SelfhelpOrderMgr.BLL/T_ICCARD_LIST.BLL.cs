﻿using System; 
using System.Text;
using System.Collections.Generic; 
using System.Data;
using SelfhelpOrderMgr.Model;
namespace SelfhelpOrderMgr.BLL {
	 	//T_ICCARD_LIST
		public partial class T_ICCARD_LISTBLL
	{
   		     
		private readonly SelfhelpOrderMgr.DAL.T_ICCARD_LISTDAL dal=new SelfhelpOrderMgr.DAL.T_ICCARD_LISTDAL();
		public T_ICCARD_LISTBLL()
		{}
		
		#region  Method


		/// <summary>
		/// 增加一条数据
		/// </summary>
		public void  Add(SelfhelpOrderMgr.Model.T_ICCARD_LIST model)
		{
						dal.Add(model);
						
		}

		/// <summary>
		/// 更新一条数据
		/// </summary>
		public bool Update(SelfhelpOrderMgr.Model.T_ICCARD_LIST model)
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
		public SelfhelpOrderMgr.Model.T_ICCARD_LIST GetModel()
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
		public List<SelfhelpOrderMgr.Model.T_ICCARD_LIST> GetModelList(string strWhere)
		{
			DataSet ds = dal.GetList(strWhere);
			return DataTableToList(ds.Tables[0]);
		}
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public List<SelfhelpOrderMgr.Model.T_ICCARD_LIST> DataTableToList(DataTable dt)
		{
			List<SelfhelpOrderMgr.Model.T_ICCARD_LIST> modelList = new List<SelfhelpOrderMgr.Model.T_ICCARD_LIST>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				SelfhelpOrderMgr.Model.T_ICCARD_LIST model;
				for (int n = 0; n < rowsCount; n++)
				{
					model = new SelfhelpOrderMgr.Model.T_ICCARD_LIST();					
				    if(dt.Rows[n]["SEQNO"].ToString()!="")
				    {
					    model.SEQNO=int.Parse(dt.Rows[n]["SEQNO"].ToString());
				    }
				    model.CardCode= dt.Rows[n]["CardCode"].ToString();
				    model.FPWD= dt.Rows[n]["FPWD"].ToString();
				    model.FCrimeCode= dt.Rows[n]["FCrimeCode"].ToString();
				    model.FRCZY= dt.Rows[n]["FRCZY"].ToString();
				    if(dt.Rows[n]["FRDate"].ToString()!="")
				    {
					    model.FRDate=DateTime.Parse(dt.Rows[n]["FRDate"].ToString());
				    }
                    else
                    {
                        model.FRDate = DateTime.Parse("1990-01-01");
                    }
				    model.FDELCZY= dt.Rows[n]["FDELCZY"].ToString();
				    if(dt.Rows[n]["FDelDate"].ToString()!="")
				    {
					    model.FDelDate=DateTime.Parse(dt.Rows[n]["FDelDate"].ToString());
                    }
                    else
                    {
                        model.FDelDate = DateTime.Parse("1990-01-01");
                    }
				    if(dt.Rows[n]["Amount"].ToString()!="")
				    {
					    model.Amount=decimal.Parse(dt.Rows[n]["Amount"].ToString());
				    }
				    if(dt.Rows[n]["FFlag"].ToString()!="")
				    {
					    model.FFlag=int.Parse(dt.Rows[n]["FFlag"].ToString());
				    }
					model.fareacode= dt.Rows[n]["fareacode"].ToString();
					model.fareaName= dt.Rows[n]["fareaName"].ToString();
					model.fcriminal= dt.Rows[n]["fcriminal"].ToString();
					model.Frealareacode= dt.Rows[n]["Frealareacode"].ToString();
					model.FrealAreaName= dt.Rows[n]["FrealAreaName"].ToString();
					if(dt.Rows[n]["cardtype"].ToString()!="")
				    {
					    model.cardtype=int.Parse(dt.Rows[n]["cardtype"].ToString());
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