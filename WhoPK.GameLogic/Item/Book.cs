using System;
using System.Collections.Generic;
using System.Text;

namespace WhoPK.GameLogic.Item
{
    public class Book
    {
        public int PageCount { get; set; }
        public List<string> Pages { get; set; }
        public bool Blank { get; set; }
    }
}
