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
            
            if (!string.IsNullOrEmpty(Request.QueryString["id"]))
            {
                id = Convert.ToInt32(Request.QueryString["id"]);
            }

            if (!string.IsNullOrEmpty(Request.QueryString["start_date"]))
            {

            }

            Spot s = Spot.GetSpotData(id);

            DataTable table = new DataTable();

            table.Columns.Add("spot_id");
            table.Columns.Add("spot_name");
            table.Columns.Add("spot_type");
            table.Columns.Add("spot_price_high");
            table.Columns.Add("spot_price_low");

            table.Rows.Add(s.Id, s.Name, s.Type, s.PricePeakSeason, s.PriceOffSeason);

            datalist_spotToBook.DataSource = table;
            datalist_spotToBook.DataBind();
        }
    }
}