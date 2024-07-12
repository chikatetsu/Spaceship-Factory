using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceshipFactory.Piece
{
    public class AddedStockInfo
    {
        public uint Quantity { get; set; }
        public string Type { get; set; }

        public AddedStockInfo(uint quantity, string type)
        {
            Quantity = quantity;
            Type = type;
        }
    }
}
