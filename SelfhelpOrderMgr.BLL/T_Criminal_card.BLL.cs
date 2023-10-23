using SelfhelpOrderMgr.DAL;
using SelfhelpOrderMgr.Model;
using System;
using System.Collections.Generic;
using System.Data;
namespace SelfhelpOrderMgr.BLL
{
	public partial class T_Criminal_cardBLL
	{
		private readonly T_Criminal_cardDAL dal = new T_Criminal_cardDAL();

		public bool Exists(string fcrimecode)
		{
			return this.dal.Exists(fcrimecode);
		}
		public void Add(T_Criminal_card model)
		{
			this.dal.Add(model);
		}
		public bool Update(T_Criminal_card model)
		{
			return this.dal.Update(model);
		}
		public bool Delete(string fcrimecode)
		{
			return this.dal.Delete(fcrimecode);
		}
		public T_Criminal_card GetModel(string fcrimecode)
		{
			return this.dal.GetModel(fcrimecode);
		}
		public DataSet GetList(string strWhere)
		{
			return this.dal.GetList(strWhere);
		}
		public DataSet GetList(int Top, string strWhere, string filedOrder)
		{
			return this.dal.GetList(Top, strWhere, filedOrder);
		}
		public List<T_Criminal_card> GetModelList(string strWhere)
		{
			return this.dal.GetModelList(strWhere);
		}

		public DataSet GetAllList()
		{
			return this.GetList("");
		}
	}
}
