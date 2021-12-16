using System;
using System.Collections.Generic;
using System.Text;

namespace AFS.Core.Response
{
    public class ResultStatus
    {
        public bool Result { get; set; }
        public object Object { get; set; }
        public string FeedBack { get; set; }
    }
}
