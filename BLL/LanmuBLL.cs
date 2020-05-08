using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using Models;

namespace BLL
{
   public class LanmuBLL
    {
        #region 通过ID获取栏目对像详细
        public LanmuInfo GetLanmuObjById(int id)
        {
            return new LanmuDAL().GetObjById(id);
        }
        #endregion

        #region 编辑对像 
        public int Edit(LanmuInfo obj)
        {
            return new LanmuDAL().Edit(obj);

        }
        #endregion
    }
}
