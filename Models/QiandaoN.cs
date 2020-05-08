using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;


namespace Models
{
   public class QiandaoN
    {

       // qdName,userName,userPhone,qdAddress,qdTime
        public int Id { set; get; }
        public string qdName { set; get; }
        public string userName { set; get; }
        public string userPhone { set; get; }
        public string qdAddress { set; get; }
        public DateTime qdTime { get; set; }

    }
}
