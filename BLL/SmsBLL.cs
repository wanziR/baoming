using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;
using DAL;

namespace BLL
{
    /// <summary>
    /// 
    /// </summary>
    public class SmsBLL
    {
        #region 添加手验证码到数据库
        public int AddCoreInfo(Sms obj)
        {
            return new SmsDAL().AddCoreInfo(obj);

        }
        #endregion
    }

}

