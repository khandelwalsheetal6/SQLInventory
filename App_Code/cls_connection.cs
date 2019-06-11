using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Web;

public class cls_connection
{
    SqlConnection con = new SqlConnection();
    SqlDataAdapter ad = null;
    SqlCommand cmd = null;
    int result = 0;

    public cls_connection()
    {
        try
        {
            con.ConnectionString = ConfigurationManager.ConnectionStrings["ConStr"].ConnectionString;
        }
        catch (Exception ex)
        {
            //  RMG.Functions.MsgBox(ex.Message);       
        }

        // TODO: Add constructor logic here

    }

    //  Function for open & close the connection
    #region connection_check

    public void open_connection()
    {
        try
        {
            if (con.State == ConnectionState.Closed)
                con.Open();
        }
        catch (Exception ex)
        {
            //  RMG.Functions.MsgBox(ex.Message);
        }
    }

    public void close_connection()
    {
        try
        {
            if (con.State == ConnectionState.Open)
                con.Close();
        }

        catch (Exception ex)
        {
            //  RMG.Functions.MsgBox(ex.Message);
        }
    }

    #endregion

    //  Function for fetch the data 
    #region data_functions

    public DataTable select_data_dt(string str)
    {

        open_connection();
        DataTable dt = new DataTable();
        ad = new SqlDataAdapter(str, con);
        ad.Fill(dt);
        close_connection();
        return dt;

    }

    public int select_data_scalar_int(string str)
    {

        open_connection();
        cmd = new SqlCommand(str, con);

        if (cmd.ExecuteScalar() != DBNull.Value)
            result = Convert.ToInt32(cmd.ExecuteScalar());
        else
            result = 0;

        close_connection();
        return result;

    }

    public double select_data_scalar_double(string str)
    {
        double result1;
        open_connection();
        cmd = new SqlCommand(str, con);
        if (cmd.ExecuteScalar() != DBNull.Value)
            result1 = Convert.ToDouble(cmd.ExecuteScalar());
        else
            result1 = 0;

        close_connection();
        return result1;

    }


    public string select_data_scalar_string(string str)
    {
        string result1;
        open_connection();
        cmd = new SqlCommand(str, con);
        if (cmd.ExecuteScalar() != DBNull.Value)
            result1 = Convert.ToString(cmd.ExecuteScalar());
        else
            result1 = "";

        close_connection();
        return result1;

    }

    public int insert_data(string str)
    {

        open_connection();
        cmd = new SqlCommand(str, con);
        int x = cmd.ExecuteNonQuery();
        cmd.CommandTimeout = 3600;
        close_connection();
        return x;
    }

    public int delete_data(string str)
    {
        open_connection();
        cmd = new SqlCommand(str, con);
        int x = cmd.ExecuteNonQuery();
        close_connection();
        return x;

    }

    public int update_data(string str)
    {

        open_connection();
        cmd = new SqlCommand(str, con);
        int x = cmd.ExecuteNonQuery();
        cmd.CommandTimeout = 3600;
        close_connection();
        return x;

    }
    #endregion
    public string GetAppPath()
    {
        if (HttpContext.Current.Request.ApplicationPath == "/")
        {
            if (HttpContext.Current.Request.Url.Port > 0)
            {
                return HttpContext.Current.Request.Url.Scheme + "://" + HttpContext.Current.Request.Url.Host + ":" + HttpContext.Current.Request.Url.Port;
            }
            else
            {
                return HttpContext.Current.Request.Url.Scheme + "://" + HttpContext.Current.Request.Url.Host;
            }
        }
        else
        if (HttpContext.Current.Request.Url.Port > 0)
        {
            return HttpContext.Current.Request.Url.Scheme + "://" + HttpContext.Current.Request.Url.Host + ":" + HttpContext.Current.Request.Url.Port + HttpContext.Current.Request.ApplicationPath;
        }
        else
        {
            return HttpContext.Current.Request.Url.Scheme + "://" + HttpContext.Current.Request.Url.Host + HttpContext.Current.Request.ApplicationPath;
        }
    }

}