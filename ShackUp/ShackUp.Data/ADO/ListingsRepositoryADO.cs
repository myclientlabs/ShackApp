using ShackUp.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShackUp.Models.Tables;
using System.Data.SqlClient;
using System.Data;

namespace ShackUp.Data.ADO
{
    public class ListingsRepositoryADO : IListingsRepository
    {
        public void Delete(int listingId)
        {
            using (var cn = new SqlConnection(Settings.GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand("ListingsDelete", cn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@ListingId", listingId);

                cn.Open();

                cmd.ExecuteNonQuery();
            }
        }

        public Listing GetById(int listingId)
        {
            Listing listing = null;

            using (var cn = new SqlConnection(Settings.GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand("ListingsSelect", cn);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@ListingId", listingId);

                cn.Open();

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.Read())
                    {
                        listing = new Listing();
                        listing.ListingId = (int)dr["ListingId"];
                        listing.UserId = dr["UserId"].ToString();
                        listing.StateId = dr["StateId"].ToString();
                        listing.BathroomTypeId = (int)dr["BathroomTypeId"];
                        listing.Nickname = dr["Nickname"].ToString();
                        listing.City = dr["City"].ToString();
                        listing.Rate = (decimal)dr["Rate"];
                        listing.SquareFootage = (decimal)dr["SquareFootage"];
                        listing.HasElectric = (bool)dr["HasElectric"];
                        listing.HasHeat = (bool)dr["HasHeat"];

                        if (dr["ImageFileName"] != DBNull.Value)
                            listing.ImageFileName = dr["ImageFileName"].ToString();
                    }
                }
            }

            return listing;
        }

        public void Insert(Listing listing)
        {
            using (var cn = new SqlConnection(Settings.GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand("ListingsInsert", cn);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                SqlParameter param = new SqlParameter("@ListingId", SqlDbType.Int);
                param.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(param);
                cmd.Parameters.AddWithValue("@UserId", listing.UserId);
                cmd.Parameters.AddWithValue("@StateId", listing.StateId);
                cmd.Parameters.AddWithValue("@BathroomTypeId", listing.BathroomTypeId);
                cmd.Parameters.AddWithValue("@Nickname", listing.Nickname);
                cmd.Parameters.AddWithValue("@City", listing.City);
                cmd.Parameters.AddWithValue("@Rate", listing.Rate);
                cmd.Parameters.AddWithValue("@SquareFootage", listing.SquareFootage);
                cmd.Parameters.AddWithValue("@HasElectric", listing.HasElectric);
                cmd.Parameters.AddWithValue("@HasHeat", listing.HasHeat);
                cmd.Parameters.AddWithValue("@ImageFileName", listing.ImageFileName);

                cn.Open();

                cmd.ExecuteNonQuery();

                listing.ListingId = (int)param.Value;
            }
        }

        public void Update(Listing listing)
        {
            using (var cn = new SqlConnection(Settings.GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand("ListingsUpdate", cn);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@ListingId", listing.ListingId);
                cmd.Parameters.AddWithValue("@UserId", listing.UserId);
                cmd.Parameters.AddWithValue("@StateId", listing.StateId);
                cmd.Parameters.AddWithValue("@BathroomTypeId", listing.BathroomTypeId);
                cmd.Parameters.AddWithValue("@Nickname", listing.Nickname);
                cmd.Parameters.AddWithValue("@City", listing.City);
                cmd.Parameters.AddWithValue("@Rate", listing.Rate);
                cmd.Parameters.AddWithValue("@SquareFootage", listing.SquareFootage);
                cmd.Parameters.AddWithValue("@HasElectric", listing.HasElectric);
                cmd.Parameters.AddWithValue("@HasHeat", listing.HasHeat);
                cmd.Parameters.AddWithValue("@ImageFileName", listing.ImageFileName);

                cn.Open();

                cmd.ExecuteNonQuery();
            }
        }
    }
}
