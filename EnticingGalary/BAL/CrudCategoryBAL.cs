using EnticingGalary.DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace EnticingGalary.BAL
{
    public class CrudCategoryBAL
    {

        //For Home Index        
        public DataTable GetWallpaperAlbums()
        {
            CommonConnection con = new CommonConnection();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "sp_crud_category";
            cmd.Connection = con.conobj;
            if (con.conobj.State != ConnectionState.Open)
            {
                con.conobj.Open();
            }
            cmd.Parameters.AddWithValue("@action", "getwallpaperalbums");
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            DataTable dt = ds.Tables[0];
            con.conobj.Close();
            return dt;
        }
        public DataTable GetMostViewWallpaperAlbums()
        {
            CommonConnection con = new CommonConnection();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "sp_crud_category";
            cmd.Connection = con.conobj;
            if (con.conobj.State != ConnectionState.Open)
            {
                con.conobj.Open();
            }
            cmd.Parameters.AddWithValue("@action", "getmostviewwallpaperalbums");
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            DataTable dt = ds.Tables[0];
            con.conobj.Close();
            return dt;
        }
        

        public DataTable GetSubCategoyGroupById(long? maincategoryid)
        {
            CommonConnection con = new CommonConnection();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "sp_crud_category";
            cmd.Connection = con.conobj;
            if (con.conobj.State != ConnectionState.Open)
            {
                con.conobj.Open();
            }
            cmd.Parameters.AddWithValue("@action", "getsubcategorygroupbyid");
            cmd.Parameters.AddWithValue("@MainCategoryId", maincategoryid);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            DataTable dt = ds.Tables[0];
            con.conobj.Close();
            return dt;
        }
        public int UpdateCategoryTypeById(long? CategoryTypeId, string CategoryTypeName, string SEOCategoryTypeName)
        {
            int i = 0;
            CommonConnection con = new CommonConnection();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "sp_crud_category";
            cmd.Connection = con.conobj;
            if (con.conobj.State != ConnectionState.Open)
            {
                con.conobj.Open();
            }
            cmd.Parameters.AddWithValue("@action", "updatecategorytypebyId");
            cmd.Parameters.AddWithValue("@CategoryTypeId", CategoryTypeId);
            cmd.Parameters.AddWithValue("@CategoryTypeName", CategoryTypeName);
            cmd.Parameters.AddWithValue("@SEOCategoryTypeName", SEOCategoryTypeName);
            i = cmd.ExecuteNonQuery();
            return i;
        }

        public int UpdateCategoryById(long? CategoryId, string CategoryName, string SEOCategoryName)
        {
            int i = 0;
            CommonConnection con = new CommonConnection();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "sp_crud_category";
            cmd.Connection = con.conobj;
            if (con.conobj.State != ConnectionState.Open)
            {
                con.conobj.Open();
            }
            cmd.Parameters.AddWithValue("@action", "updatecategorybyid");
            cmd.Parameters.AddWithValue("@CategoryId", CategoryId);
            cmd.Parameters.AddWithValue("@CategoryName", CategoryName);
            cmd.Parameters.AddWithValue("@SEOCategoryName", SEOCategoryName);
            i = cmd.ExecuteNonQuery();
            return i;
        }

        public int DeleteCategoryTypeById(long? CategoryTypeId)
        {
            int i = 0;
            CommonConnection con = new CommonConnection();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "sp_crud_category";
            cmd.Connection = con.conobj;
            if (con.conobj.State != ConnectionState.Open)
            {
                con.conobj.Open();
            }
            cmd.Parameters.AddWithValue("@action", "deletecategorytypebyid");
            cmd.Parameters.AddWithValue("@CategoryTypeId", CategoryTypeId);
            i = cmd.ExecuteNonQuery();
            return i;
        }
        public int DeleteCategoryById(long? CategoryId)
        {
            int i = 0;
            CommonConnection con = new CommonConnection();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "sp_crud_category";
            cmd.Connection = con.conobj;
            if (con.conobj.State != ConnectionState.Open)
            {
                con.conobj.Open();
            }
            cmd.Parameters.AddWithValue("@action", "deletecategorybyid");
            cmd.Parameters.AddWithValue("@CategoryId", CategoryId);
            i = cmd.ExecuteNonQuery();
            return i;
        }


        public int UpdateReviewBySEOCategoryName(string SEOCategoryName)
        {
            int i = 0;
            CommonConnection con = new CommonConnection();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "sp_crud_category";
            cmd.Connection = con.conobj;
            if (con.conobj.State != ConnectionState.Open)
            {
                con.conobj.Open();
            }
            cmd.Parameters.AddWithValue("@action", "updatereviewbyseocategoryname");
            cmd.Parameters.AddWithValue("@SEOCategoryName", SEOCategoryName);
            i = cmd.ExecuteNonQuery();
            return i;
        }


        //Get Global Like
        public DataTable GetSubCategoyGroupLikeById(string sEOMainCategoryName)
        {
            CommonConnection con = new CommonConnection();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "sp_crud_category";
            cmd.Connection = con.conobj;
            if (con.conobj.State != ConnectionState.Open)
            {
                con.conobj.Open();
            }
            cmd.Parameters.AddWithValue("@action", "getsubcategorygrouplikeglobal");
            cmd.Parameters.AddWithValue("@SEOCategoryTypeName", sEOMainCategoryName);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            DataTable dt = ds.Tables[0];
            con.conobj.Close();
            return dt;
        }


    }
}