using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace H1___Campingpladsen
{
    public partial class BookSpot : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            int id = 1;
            DateTime date = DateTime.Now;
            
            // Get data from url

            if (!string.IsNullOrEmpty(Request.QueryString["id"]))
                id = Convert.ToInt32(Request.QueryString["id"]);

            if (!string.IsNullOrEmpty(Request.QueryString["date"]))
                date = Convert.ToDateTime(Request.QueryString["date"]);

            // Get spot from id

            Spot s = Spot.GetSpotData(id);

            // Find current price

            DateTime compareable = new DateTime(DateTime.Now.Year, date.Month, date.Day);
            DateTime peakSeasonStart = new DateTime(DateTime.Now.Year, 6, 14);
            DateTime peakSeasonEnd = new DateTime(DateTime.Now.Year, 8, 15);

            decimal price;

            if (compareable > peakSeasonStart && compareable < peakSeasonEnd)
            {
                price = s.PricePeakSeason;
            }
            else
            {
                price = s.PriceOffSeason;
            }

            // Bind data to view

            DataTable table = new DataTable();


            table.Columns.Add("spot_id");
            table.Columns.Add("spot_name");
            table.Columns.Add("spot_type");
            table.Columns.Add("spot_price");

            table.Rows.Add(s.Id, s.Name, s.Type, price);

            datalist_spotToBook.DataSource = table;
            datalist_spotToBook.DataBind();
        }
    }
}