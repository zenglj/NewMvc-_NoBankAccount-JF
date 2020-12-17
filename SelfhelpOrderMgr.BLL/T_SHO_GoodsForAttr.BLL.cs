using System; 
using System.Text;
using System.Collections.Generic; 
using System.Data;
using SelfhelpOrderMgr.Model;
using SelfhelpOrderMgr.DAL;

namespace SelfhelpOrderMgr.BLL {
	 	//T_SHO_GoodsForAttr
		public partial class T_SHO_GoodsForAttrBLL
	{
   		     
		private readonly SelfhelpOrderMgr.DAL.T_SHO_GoodsForAttrDAL dal=new SelfhelpOrderMgr.DAL.T_SHO_GoodsForAttrDAL();
		public T_SHO_GoodsForAttrBLL()
		{}
		
		#region  Method
		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(int ID)
		{
			return dal.Exists(ID);
		}

		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int  Add(SelfhelpOrderMgr.Model.T_SHO_GoodsForAttr model)
		{
						return dal.Add(model);
						
		}

		/// <summary>
		/// 更新一条数据
		/// </summary>
		public bool Update(SelfhelpOrderMgr.Model.T_SHO_GoodsForAttr model)
		{
			return dal.Update(model);
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public bool Delete(int ID)
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
		public SelfhelpOrderMgr.Model.T_SHO_GoodsForAttr GetModel(int ID)
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
        public List<SelfhelpOrderMgr.Model.T_SHO_GoodsForAttr> GetModelList(string strWhere, string filedOrder)
		{
            DataSet ds = dal.GetList(100,strWhere,filedOrder);
			return DataTableToList(ds.Tables[0]);
		}
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public List<SelfhelpOrderMgr.Model.T_SHO_GoodsForAttr> DataTableToList(DataTable dt)
		{
			List<SelfhelpOrderMgr.Model.T_SHO_GoodsForAttr> modelList = new List<SelfhelpOrderMgr.Model.T_SHO_GoodsForAttr>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				SelfhelpOrderMgr.Model.T_SHO_GoodsForAttr model;
				for (int n = 0; n < rowsCount; n++)
				{
					model = new SelfhelpOrderMgr.Model.T_SHO_GoodsForAttr();					
													if(dt.Rows[n]["ID"].ToString()!="")
				{
					model.ID=int.Parse(dt.Rows[n]["ID"].ToString());
				}
																																				model.GCode= dt.Rows[n]["GCode"].ToString();
																												if(dt.Rows[n]["GoodsAttrId"].ToString()!="")
				{
					model.GoodsAttrId=int.Parse(dt.Rows[n]["GoodsAttrId"].ToString());
				}
																																				model.AttrInfo= dt.Rows[n]["AttrInfo"].ToString();
																						
				
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