using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;


namespace Models
{
   public class Qiandao
    {
        // Id, huiyiId, userId, qdAddress, qdTime
        //public int Id { set; get; }
        //public int huiyiId { set; get; }
        //public int userId { set; get; }
        //public string qdAddress { set; get; }
        //public DateTime qdTime { get; set; }

        public int Id { set; get; }
        public string qdName { set; get; }
        public DateTime qdStarttime { get; set; }
        public DateTime qdEndttime { get; set; }
    }
}
