using H1___Campingpladsen.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace H1___Campingpladsen
{
    public enum StoredProcedures {
        GetAllSpots,
        GetAllSpotsOfType,
        GetSpotModel,
        GetAllReservations,
        GetCurrentReservations,
        GetReservationModel,
        InsertReservation,
        GetAllSpotTypes
    }

    public class DbController
    {
        public static string ConnectionString = "Server=localhost; Database=Campingpladsen; User Id=campingpladsen_rw; Password=Kode1234!";

        public static Spot GetSpot(int id)
        {
            Spot s = new Spot();

            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand(StoredProcedures.GetSpotModel.ToString(), con)
                {
                    CommandType = System.Data.CommandType.StoredProcedure
                })
                {
                    cmd.Parameters.AddWithValue("@id", id);

                    con.Open();

                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        s = ReadSpot(reader);
                    }

                    con.Close();
                }
            }

            return s;
        }

        public static List<Spot> GetAllSpots()
        {
            List<Spot> spots = new List<Spot>();

            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand(StoredProcedures.GetAllSpots.ToString(), con)
                {
                    CommandType = System.Data.CommandType.StoredProcedure
                })
                {
                    con.Open();

                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        Spot s = ReadSpot(reader);
                        spots.Add(s);
                    }

                    con.Close();
                }
            }

            return spots;
        }

        public static List<Spot> GetAllSpotsOfType(int typeId)
        {
            List<Spot> spots = new List<Spot>();

            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand(StoredProcedures.GetAllSpotsOfType.ToString(), con)
                {
                    CommandType = System.Data.CommandType.StoredProcedure
                })
                {

                    cmd.Parameters.AddWithValue("@type_id", typeId);

                    con.Open();

                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        Spot s = ReadSpot(reader);
                        spots.Add(s);
                    }

                    con.Close();
                }
            }

            return spots;
        }

        public static Spot ReadSpot(SqlDataReader reader)
        {
            int id = (int)reader["id"];
            string name = (string)reader["spot_name"];
            string type = (string)reader["spot_type_name"];
            decimal pricePeakSeason = (decimal)reader["price_high"];
            decimal priceOffSeason = (decimal)reader["price_low"];

            Spot s = new Spot(id, name, type, pricePeakSeason, priceOffSeason);

            return s;
        }

        public static Reservation GetReservation(int id)
        {
            Customer c = new Customer();
            Spot s = new Spot();
            Reservation r = new Reservation();

            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand(StoredProcedures.GetReservationModel.ToString(), con)
                {
                    CommandType = System.Data.CommandType.StoredProcedure
                })
                {
                    cmd.Parameters.AddWithValue("@id", id);

                    con.Open();

                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        // Get customer information
                        int customerId = (int)reader["Kunde ID"];
                        string customerName = (string)reader["Navn"];
                        string customerAddress = (string)reader["Adresse"];
                        string customerPhone = (string)reader["Telefonnummer"];
                        string customerEmail = (string)reader["Email"];

                        // Get the spot
                        string spotName = (string)reader["Plads"];
                        string spotType = (string)reader["Pladstype"];

                        // Get the rest of the reservation
                        DateTime startDate = (DateTime)reader["Udrejsedato"];
                        DateTime endDate = (DateTime)reader["Hjemrejsedato"];
                        int adults = (int)reader["Antal voksne"];
                        int children = (int)reader["Antal børn"];
                        int dogs = (int)reader["Antal hunde"];
                        bool bedLinen = Convert.ToBoolean(reader["Leje af sengelinned"]);
                        bool exitCleaning = Convert.ToBoolean(reader["Betalt for slutrengøring"]);
                        int bikeRental = (int)reader["Total cykeludlejning"];
                        int adultWaterParkPasses = (int)reader["Antal badebilletter (voksne)"];
                        int childWaterParkPasses = (int)reader["Antal badebilletter (børn)"];


                        c = new Customer(customerId, customerName, customerAddress, customerPhone, customerEmail);
                        s = new Spot(spotName, spotType);
                        r = new Reservation(c, s, startDate, endDate, adults, children, dogs, bedLinen, exitCleaning, bikeRental, adultWaterParkPasses, childWaterParkPasses);
                    }

                    con.Close();
                }
            }

            return r;
        }

        public static bool InsertReservation(Reservation reservation)
        {
            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand(StoredProcedures.InsertReservation.ToString(), con)
                {
                    CommandType = System.Data.CommandType.StoredProcedure
                })
                {
                    cmd.Parameters.AddWithValue("@customer_full_name", reservation.Customer.Name);
                    cmd.Parameters.AddWithValue("@customer_home_address", reservation.Customer.Address);
                    cmd.Parameters.AddWithValue("@customer_phone", reservation.Customer.Phone);
                    cmd.Parameters.AddWithValue("@customer_email", reservation.Customer.Email);

                    cmd.Parameters.AddWithValue("@resduration_spot_id", reservation.Spot.Id);
                    cmd.Parameters.AddWithValue("@resduration_start_date", reservation.StartDate.ToString("yyyy'-'MM'-'dd'T'13:00:00"));
                    cmd.Parameters.AddWithValue("@resduration_end_date", reservation.EndDate.ToString("yyyy'-'MM'-'dd'T'11:00:00"));

                    cmd.Parameters.AddWithValue("@reservation_adults", reservation.NumberOfAdults);
                    cmd.Parameters.AddWithValue("@reservation_children", reservation.NumberOfChildren);
                    cmd.Parameters.AddWithValue("@reservation_dogs", reservation.NumberOfDogs);

                    cmd.Parameters.AddWithValue("@resextra_qty_bedlinen", Convert.ToInt32(reservation.RequiresBedLinen));
                    cmd.Parameters.AddWithValue("@resextra_qty_exit_cleaning", Convert.ToInt32(reservation.RequiresExitCleaning));
                    cmd.Parameters.AddWithValue("@resextra_qty_bike_rental", reservation.BikeRentalTotal);
                    cmd.Parameters.AddWithValue("@resextra_qty_waterpark_adults", reservation.NumberOfAdultWaterParkPasses);
                    cmd.Parameters.AddWithValue("@resextra_qty_waterpark_children", reservation.NumberOfChildrensWaterParkPasses);

                    con.Open();

                    cmd.ExecuteNonQuery();

                    //try
                    //{
                    //    cmd.ExecuteNonQuery();
                    //}
                    //catch
                    //{
                    //    con.Close();
                    //    return false;
                    //}

                        con.Close();
                }
            }

            return true;
        }

        public static List<string[]> GetAllSpotTypes()
        {
            List<string[]> spotTypes = new List<string[]>();

            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand(StoredProcedures.GetAllSpotTypes.ToString(), con)
                {
                    CommandType = System.Data.CommandType.StoredProcedure
                })
                {

                    con.Open();

                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        object[] spotTypeObjects = new object[reader.FieldCount];
                        string[] spotType = new string[reader.FieldCount];

                        reader.GetValues(spotTypeObjects);

                        for (int i = 0; i < spotType.Length; i++)
                        {
                            spotType[i] = spotTypeObjects[i].ToString();
                        }

                        spotTypes.Add(spotType);
                    }

                    con.Close();
                }
            }

            return spotTypes;
        }
    }
}