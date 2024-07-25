using System;
using System.Text;
using System.Collections.Generic;
using System.Data;

namespace SelfhelpOrderMgr.BLL
{
	//t_balanceList
	public partial class t_balanceListBLL
	{

		private readonly SelfhelpOrderMgr.DAL.t_balanceListDAL dal = new SelfhelpOrderMgr.DAL.t_balanceListDAL();
		public t_balanceListBLL()
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
		public int Add(SelfhelpOrderMgr.Model.t_balanceList model)
		{
			return dal.Add(model);

		}

		/// <summary>
		/// 更新一条数据
		/// </summary>
		public bool Update(SelfhelpOrderMgr.Model.t_balanceList model)
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
		public SelfhelpOrderMgr.Model.t_balanceList GetModel(int seqno)
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
		public List<SelfhelpOrderMgr.Model.t_balanceList> GetModelList(string strWhere)
		{
			DataSet ds = dal.GetList(strWhere);
			return DataTableToList(ds.Tables[0]);
		}
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public List<SelfhelpOrderMgr.Model.t_balanceList> DataTableToList(DataTable dt)
		{
			List<SelfhelpOrderMgr.Model.t_balanceList> modelList = new List<SelfhelpOrderMgr.Model.t_balanceList>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				SelfhelpOrderMgr.Model.t_balanceList model;
				for (int n = 0; n < rowsCount; n++)
				{
					model = new SelfhelpOrderMgr.Model.t_balanceList();
					if (dt.Rows[n]["seqno"].ToString() != "")
					{
						model.seqno = int.Parse(dt.Rows[n]["seqno"].ToString());
					}
					if (dt.Rows[n]["AmountB"].ToString() != "")
					{
						model.AmountB = decimal.Parse(dt.Rows[n]["AmountB"].ToString());
					}
					if (dt.Rows[n]["crtdate"].ToString() != "")
					{
						model.crtdate = DateTime.Parse(dt.Rows[n]["crtdate"].ToString());
					}
					model.crtby = dt.Rows[n]["crtby"].ToString();
					model.remark = dt.Rows[n]["remark"].ToString();
					if (dt.Rows[n]["baltype"].ToString() != "")
					{
						model.baltype = int.Parse(dt.Rows[n]["baltype"].ToString());
					}
					model.DEPOSITER = dt.Rows[n]["DEPOSITER"].ToString();
					if (dt.Rows[n]["AmountC"].ToString() != "")
					{
						model.AmountC = decimal.Parse(dt.Rows[n]["AmountC"].ToString());
					}
					if (dt.Rows[n]["bankamount"].ToString() != "")
					{
						model.bankamount = decimal.Parse(dt.Rows[n]["bankamount"].ToString());
					}
					if (dt.Rows[n]["PayMode"].ToString() != "")
					{
						model.PayMode = int.Parse(dt.Rows[n]["PayMode"].ToString());
					}
					if (dt.Rows[n]["CollectMoneyFlag"].ToString() != "")
					{
						model.CollectMoneyFlag = int.Parse(dt.Rows[n]["CollectMoneyFlag"].ToString());
					}
					model.fcrimecode = dt.Rows[n]["fcrimecode"].ToString();
					if (dt.Rows[n]["PrintCount"].ToString() != "")
					{
						model.PrintCount = int.Parse(dt.Rows[n]["PrintCount"].ToString());
					}
					model.vounoa = dt.Rows[n]["vounoa"].ToString();
					model.cardcodea = dt.Rows[n]["cardcodea"].ToString();
					if (dt.Rows[n]["typeflagA"].ToString() != "")
					{
						model.typeflagA = int.Parse(dt.Rows[n]["typeflagA"].ToString());
					}
					if (dt.Rows[n]["AmountA"].ToString() != "")
					{
						model.AmountA = decimal.Parse(dt.Rows[n]["AmountA"].ToString());
					}
					model.vounob = dt.Rows[n]["vounob"].ToString();
					model.cardcodeB = dt.Rows[n]["cardcodeB"].ToString();
					if (dt.Rows[n]["typeflagB"].ToString() != "")
					{
						model.typeflagB = int.Parse(dt.Rows[n]["typeflagB"].ToString());
					}
					if (dt.Rows[0]["AccPoints"].ToString() != "")
					{
						model.AccPoints = decimal.Parse(dt.Rows[0]["AccPoints"].ToString());
					}

					if (dt.Rows[0]["AtmLuFeiAmount"].ToString() != "")
					{
						model.AtmLuFeiAmount = decimal.Parse(dt.Rows[0]["AtmLuFeiAmount"].ToString());
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