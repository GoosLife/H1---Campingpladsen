using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace H1___Campingpladsen
{
    public class Spot
    {
        private int id;

        public int Id
        {
            get { return id; }
            set { id = value; }
        }

        private string name;

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        private string spotType;

        public string SpotType
        {
            get { return spotType; }
            set { spotType = value; }
        }

        private decimal price;

        public decimal Price
        {
            get { return price; }
            set { price = value; }
        }


        public Spot(int id, string name, string spotType, decimal price)
        {
            this.id = id;
            this.name = name;
            this.spotType = spotType;
            this.price = price;
        }
    }
}