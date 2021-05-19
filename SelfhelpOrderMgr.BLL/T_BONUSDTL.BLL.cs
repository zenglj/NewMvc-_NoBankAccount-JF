using System;
using System.Text;
using System.Collections.Generic;
using System.Data;

namespace SelfhelpOrderMgr.BLL
{
	//T_BONUSDTL
	public partial class T_BONUSDTLBLL
	{

		private readonly SelfhelpOrderMgr.DAL.T_BONUSDTLDAL dal = new SelfhelpOrderMgr.DAL.T_BONUSDTLDAL();
		public T_BONUSDTLBLL()
		{ }

		#region  Method
		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(int seqno)
		{
			return dal.Exists(seqno);
		}

		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int Add(SelfhelpOrderMgr.Model.T_BONUSDTL model)
		{
			return dal.Add(model);

		}

		/// <summary>
		/// 更新一条数据
		/// </summary>
		public bool Update(SelfhelpOrderMgr.Model.T_BONUSDTL model)
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
		public bool DeleteList(string seqnolist)
		{
			return dal.DeleteList(seqnolist);
		}

		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public SelfhelpOrderMgr.Model.T_BONUSDTL GetModel(int seqno)
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
		public DataSet GetList(int Top, string strWhere, string filedOrder)
		{
			return dal.GetList(Top, strWhere, filedOrder);
		}
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public List<SelfhelpOrderMgr.Model.T_BONUSDTL> GetModelList(string strWhere)
		{
			DataSet ds = dal.GetList(strWhere);
			return DataTableToList(ds.Tables[0]);
		}
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public List<SelfhelpOrderMgr.Model.T_BONUSDTL> DataTableToList(DataTable dt)
		{
			List<SelfhelpOrderMgr.Model.T_BONUSDTL> modelList = new List<SelfhelpOrderMgr.Model.T_BONUSDTL>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				SelfhelpOrderMgr.Model.T_BONUSDTL model;
				for (int n = 0; n < rowsCount; n++)
				{
					model = new SelfhelpOrderMgr.Model.T_BONUSDTL();
					if (dt.Rows[n]["seqno"].ToString() != "")
					{
						model.seqno = int.Parse(dt.Rows[n]["seqno"].ToString());
					}
					model.vouno = dt.Rows[n]["vouno"].ToString();
					model.Frealareacode = dt.Rows[n]["Frealareacode"].ToString();
					model.FrealAreaName = dt.Rows[n]["FrealAreaName"].ToString();
					model.remark = dt.Rows[n]["remark"].ToString();
					model.ptype = dt.Rows[n]["ptype"].ToString();
					if (dt.Rows[n]["udate"].ToString() != "")
					{
						model.udate = DateTime.Parse(dt.Rows[n]["udate"].ToString());
					}
					model.crtby = dt.Rows[n]["crtby"].ToString();
					if (dt.Rows[n]["crtdt"].ToString() != "")
					{
						model.crtdt = DateTime.Parse(dt.Rows[n]["crtdt"].ToString());
					}
					model.applyby = dt.Rows[n]["applyby"].ToString();
					if (dt.Rows[n]["acctype"].ToString() != "")
					{
						model.acctype = int.Parse(dt.Rows[n]["acctype"].ToString());
					}
					model.BID = dt.Rows[n]["BID"].ToString();
					if (dt.Rows[n]["cardtype"].ToString() != "")
					{
						model.cardtype = int.Parse(dt.Rows[n]["cardtype"].ToString());
					}
					if (dt.Rows[n]["AmountC"].ToString() != "")
					{
						model.AmountC = decimal.Parse(dt.Rows[n]["AmountC"].ToString());
					}
					if (dt.Rows[n]["cqbt"].ToString() != "")
					{
						model.cqbt = decimal.Parse(dt.Rows[n]["cqbt"].ToString());
					}
					if (dt.Rows[n]["gwjt"].ToString() != "")
					{
						model.gwjt = decimal.Parse(dt.Rows[n]["gwjt"].ToString());
					}
					if (dt.Rows[n]["ldjx"].ToString() != "")
					{
						model.ldjx = decimal.Parse(dt.Rows[n]["ldjx"].ToString());
					}
					if (dt.Rows[n]["tbbz"].ToString() != "")
					{
						model.tbbz = decimal.Parse(dt.Rows[n]["tbbz"].ToString());
					}
					if (dt.Rows[n]["grkj"].ToString() != "")
					{
						model.grkj = decimal.Parse(dt.Rows[n]["grkj"].ToString());
					}
					if (dt.Rows[n]["AmountA"].ToString() != "")
					{
						model.AmountA = decimal.Parse(dt.Rows[n]["AmountA"].ToString());
					}
					if (dt.Rows[n]["AmountB"].ToString() != "")
					{
						model.AmountB = decimal.Parse(dt.Rows[n]["AmountB"].ToString());
					}
					model.FCRIMECODE = dt.Rows[n]["FCRIMECODE"].ToString();
					model.CARDCODE = dt.Rows[n]["CARDCODE"].ToString();
					if (dt.Rows[n]["FAMOUNT"].ToString() != "")
					{
						model.FAMOUNT = decimal.Parse(dt.Rows[n]["FAMOUNT"].ToString());
					}
					if (dt.Rows[n]["FLAG"].ToString() != "")
					{
						model.FLAG = int.Parse(dt.Rows[n]["FLAG"].ToString());
					}
					model.fareacode = dt.Rows[n]["fareacode"].ToString();
					model.fareaName = dt.Rows[n]["fareaName"].ToString();
					model.fcriminal = dt.Rows[n]["fcriminal"].ToString();


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