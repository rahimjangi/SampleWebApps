using DataLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVC.Models
{
    public class OrderDisplayModel
    {
        public OrderModel Order{ get; set; }
        public string ItemPurchased { get; set; }
    }
}
