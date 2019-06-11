using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Collections;
using System.Data.SqlClient;
using System.Configuration;
using System.IO;
using System.Globalization;

public partial class BasicDataMethods
{
    // Department
    public static List<cls_InstanceInventory> GetInstanceInventory()
    {
        string query = "SELECT * FROM [dbo].[INSTANCE_INVENTORY]";
        System.Data.SqlClient.SqlDataReader dr = SqlHelper.ExecuteReader(System.Data.CommandType.Text, query);
        List<cls_InstanceInventory> lstDept = new List<cls_InstanceInventory>().FromDataReader(dr).ToList<cls_InstanceInventory>();
        return lstDept;
    }
    
    public static string SqlClean(string value)
    {
        if (value == "")
        {
            return value;
        }
        else
        {
            value = value.Replace("'", "''");
            return value;
        }
    }
}