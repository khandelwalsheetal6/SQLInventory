using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Reflection;


public class Reflection
{
    public void FillObjectWithProperty(ref object objectTo, string propertyName, object propertyValue)
    {


        Type tOb2 = objectTo.GetType();


        BindingFlags flags = BindingFlags.Public | BindingFlags.NonPublic |
            BindingFlags.Static | BindingFlags.Instance |
                BindingFlags.DeclaredOnly;


        if (propertyValue == DBNull.Value)
        {
            tOb2.GetProperty(propertyName, flags).SetValue(objectTo, null);
        }
        else
        {
            tOb2.GetProperty(propertyName, flags).SetValue(objectTo, propertyValue);
        }
    }
}