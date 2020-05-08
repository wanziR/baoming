using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Models
{
    [Serializable()]
   public class UserInfo
    {
        //userId, userName, userCompany, userPost, userPhone, userAddress, taxName, taxNumber, taxBank, taxType
        public int userId { set; get; }
        [DisplayName("姓名")]
        [Required(ErrorMessage ="{0}不得为空")]
        public string userName { set; get; }
        [DisplayName("公司")]
        [Required(ErrorMessage = "{0}不得为空")]
        public string userCompany { set; get; }
        public string userPost { set; get; }
        [DisplayName("电话号")]
        [Required(ErrorMessage = "{0}不得为空")]
        [StringLength(11, MinimumLength = 7, ErrorMessage = "{0}长度不合要求")]
        public string userPhone { set; get; }
        public string userAddress { set; get; }
        public string jxJiaoyu { set; get; }
        public string taxName { set; get; }
        public string taxNumber { set; get; }
        public string taxBank { set; get; }
        public int taxType { set; get; }
        public int taxIsok { set; get; }
        public int feiIsOk { set; get; }
        public int Num { set; get; }
        public int feiNum { set; get; }
        public DateTime userAddtime { get; set; }
        public string taxPhone { set; get; }
        public string taxAddress { set; get; }
        public int taxIsPu { set; get; }
        public int Code { set; get; }


    }
}
