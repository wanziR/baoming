using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;

using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Models
{
   public class SysAdmin
    {
        public int PK_Sys_Admin { set; get; }
        public string admin_name { set; get; }
        public string admin_pwd { set; get; }
        public DateTime admin_addtime { set; get; }
    }

}
