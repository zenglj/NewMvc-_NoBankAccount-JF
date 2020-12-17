using SelfhelpOrderMgr.DAL;
using SelfhelpOrderMgr.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SelfhelpOrderMgr.BLL
{
    public partial class t_TreeRole_MenuBLL
    {
        public t_TreeRole_Menu Insert(t_TreeRole_Menu op)
        {
            return new t_TreeRole_MenuDAL().Insert(op);
        }

        public int InsertRoleMenu(int roleId)
        {
            return new t_TreeRole_MenuDAL().InsertRoleMenu(roleId); ;
        }
        public int DeleteById(int id)
        {
            return new t_TreeRole_MenuDAL().DeleteById(id);
        }

        public int DeleteByRoleId(int roleId)
        {
            return new t_TreeRole_MenuDAL().DeleteByRoleId(roleId);
        }
        /// <summary>
        /// 根据用户'Id'Update用户数据
        /// </summary>
        /// <param name="op"></param>
        public int UpdateByID(t_TreeRole_Menu op)
        {
            return new t_TreeRole_MenuDAL().UpdateByID(op);
        }

        public int UpdateByRoleMenu(int flag, string strRoleID, string NodesSql)
        {
            return new t_TreeRole_MenuDAL().UpdateByRoleMenu(flag, strRoleID, NodesSql);
        }

        /// 按Id查找用户记录
        public t_TreeRole_Menu GetTableById(int id)
        {
            return new t_TreeRole_MenuDAL().GetTableById(id);
        }

        public IEnumerable<t_TreeRole_Menu> GetTableByRoleId(string RoleId, params int[] flags)
        {
            return new t_TreeRole_MenuDAL().GetTableByRoleId(RoleId, flags);
        }
    }
}