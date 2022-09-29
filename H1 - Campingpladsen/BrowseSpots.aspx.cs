﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace H1___Campingpladsen
{
    public partial class BrowseSpots : System.Web.UI.Page
    {
        static List<int> selectedIntTypes = new List<int>();

        protected void Page_Load(object sender, EventArgs e)
        {
            // Get all spots
            List<Spot> spots = DbController.GetAllSpots();

            // Create data table with spot information
            DataTable table = new DataTable();
            table.Columns.Add("spot_id");
            table.Columns.Add("spot_name");
            table.Columns.Add("spot_type");
            table.Columns.Add("start_date");

            DataTable table2 = new DataTable();

            for (int i = 0; i < spots.Count; i++)
            {
                table.Rows.Add(spots[i].Id, spots[i].Name, spots[i].Type);
            }

            string[] spotTypeValues = { "spot_type_name", "id" };
            List<object[]> spotTypes = DbController.SelectColumnsFromTable(spotTypeValues, "SpotTypes");

            for (int i = 0; i < spotTypes.Count; i++)
            {
                cbl_spotTypes.Items.Add(new ListItem((string)spotTypes[i][0], Convert.ToString(spotTypes[i][1])));
            }

            cbl_spotTypes.DataBind();

            datalist_spots.DataSource = table;
            datalist_spots.DataBind();
        }

        protected void cbl_spotTypes_SelectedIndexChanged(object sender, EventArgs e)
        {
            selectedIntTypes.Clear();

            for (int i = 0; i < cbl_spotTypes.Items.Count; i++)
            {
                if (cbl_spotTypes.Items[i].Selected)
                    selectedIntTypes.Add(Convert.ToInt32(cbl_spotTypes.Items[i].Value));
            }
        }
    }
}