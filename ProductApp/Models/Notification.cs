using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductApp.Models
{
    public class Notification
    {
        public Notification(String Type, String Message)
        {
            if (Type.ToUpper() == "ERROR")
            {
                TypeRender = "alert alert-danger";
                MessageRender = Message;
            }
            else if (Type.ToUpper() == "WARNING")
            {
                TypeRender = "alert alert-warn";
                MessageRender = Message;
            }
            if (Type.ToUpper() == "SUCCESS")
            {
                TypeRender = "alert alert-success";
                MessageRender = Message;
            }

        }


        public String TypeRender { get; set; }
        public String MessageRender { get; set; }
    }
}
