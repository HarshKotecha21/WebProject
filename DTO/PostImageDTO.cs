using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace DTO
{
    public class PostImageDTO
    {
        public int ID { get; set; }
        public int  PostID { get; set; }
        public string ImagePath { get; set; }


    }
}
