﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs
{
    public class OrderReturnDto
    {       
        public int OrderId { get; set; }
        public int? OrderSum { get; set; }

        public int? UserId { get; set; }



    }
}
