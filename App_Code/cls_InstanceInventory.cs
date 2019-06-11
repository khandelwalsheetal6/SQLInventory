using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

public class cls_InstanceInventory
{
    public string SERVER_NAME { get; set; }
    public string BUILDING_NUMBER { get; set; }
    public string DATABASE_TYPE { get; set; }
    public string INSTANCE_NAME { get; set; }
    public string CONNECT_STRING { get; set; }
    public string DESCRIPTION { get; set; }
    public DateTime CREATE_DATE { get; set; }
    public DateTime OBSOLETE_DATE { get; set; }
    public int LISTENER_PORT_NO { get; set; }
    public string ADMIN_CHECKLIST_CREATED { get; set; }
    public string ACTIVE { get; set; }
    public int CPU_COUNT { get; set; }
    public string LICENSE_MODEL { get; set; }
    public string MAJORVERSION { get; set; }
    public string MINORVERSION { get; set; }
    public string BUILDVERSION { get; set; }
    public string MANUFACTURER { get; set; }
    public int ID { get; set; }
    public string IS_INDEX_MAINT_INSTALLED { get; set; }
    public string DBA_TEAM_MANAGED { get; set; }
    public int number_of_physical_cpus { get; set; }
    public int number_of_cores_per_cpu { get; set; }
    public int total_number_of_cores { get; set; }
    public int number_of_virtual_cpus { get; set; }
    public string edition { get; set; }
    public DateTime LAST_INVENTORY_DATE { get; set; }
    public string SERVICEPACK { get; set; }
    public int HYPERTHREAD_RATIO { get; set; }




}
