using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;
using DAL;
using System.Web;

namespace BLL
{
   public class QiandaoBLL
    {
        #region 通过时间获取对像 
        public Qiandao GetObjByTime()
        {
            return new QiandaoDAL().GetObjByTime();

        }
        #endregion

        #region 添加
        public int Add(QiandaoN obj)
        {
            return new QiandaoDAL().Add(obj);
        }
        #endregion
    }
}
