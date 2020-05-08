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
   public class LanmuInfo
    {
        //lanmuId, lanmuName, lanmuNeirong
        public int lanmuId { set; get; }
        public string lanmuName { set; get; }
        public string lanmuNeirong { set; get; }
       

    }
}
