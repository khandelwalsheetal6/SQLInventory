<%@ Page Title="" Language="C#" MasterPageFile="~/Front.master" AutoEventWireup="true" CodeFile="Inventory.aspx.cs" Inherits="Inventory" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <!-- page content -->
    <div class="right_col" role="main">
        <div class="page-title">
            <div class="title_left">
                <h3>Inventory</h3>
            </div>

            <div class="title_right">
                <div class="col-md-5 col-sm-5 col-xs-12 form-group pull-right top_search">
                    <div class="input-group">
                    </div>
                </div>
            </div>
        </div>

        <div class="clearfix"></div>

        <div class="row">
            <div class="col-md-12 col-sm-12 col-xs-12">
                <div class="x_panel">
                    <div class="x_content">
                        <table id="datatable-responsive" class="table table-striped table-bordered dt-responsive nowrap" cellspacing="0" width="100%">
                            <thead>
                                <tr>
                                    <th>Server Name</th>
                                    <th>Building Number</th>
                                    <th>Database Type</th>
                                    <th>Instance Name</th>
                                    <th>Connect String</th>
                                    <th>Description</th>
                                    <th>Create Date</th>
                                    <th>Obsolete Date</th>
                                    <th>Listener Port No</th>
                                    <th>Admin CheckList Created</th>
                                    <th>Active</th>
                                    <th>CPU Count</th>
                                    <th>License Model</th>
                                    <th>Major Version</th>
                                    <th>Minor Version</th>
                                    <th>Build Version</th>
                                    <th>Manufacturer</th>
                                    <th>ID</th>
                                    <th>IS Index Maint Installed</th>
                                    <th>DBA Team Managed</th>
                                    <th>Number of Physical cpus</th>
                                    <th>Number of Cores per cpu</th>
                                    <th>Total Number of cores</th>
                                    <th>Number of Virtual cpus</th>
                                    <th>Edition</th>
                                    <th>Last Inventory Date</th>
                                    <th>Service Pack</th>
                                    <th>HyperThread Ratio</th>
                                </tr>
                            </thead>
                            <tbody>
                                <asp:Repeater ID="rptInventory" runat="server">
                                    <ItemTemplate>
                                        <tr>
                                            <td><%#Eval("SERVER_NAME")%></td>
                                            <td><%#Eval("BUILDING_NUMBER")%></td>
                                            <td><%#Eval("DATABASE_TYPE")%></td>
                                            <td><%#Eval("INSTANCE_NAME")%></td>
                                            <td><%#Eval("CONNECT_STRING")%></td>
                                            <td><%#Eval("DESCRIPTION")%></td>
                                            <td><%#Eval("CREATE_DATE")%></td>
                                            <td><%#Eval("OBSOLETE_DATE")%></td>
                                            <td><%#Eval("LISTENER_PORT_NO")%></td>
                                            <td><%#Eval("ADMIN_CHECKLIST_CREATED")%></td>
                                            <td><%#Eval("ACTIVE")%></td>
                                            <td><%#Eval("CPU_COUNT")%></td>
                                            <td><%#Eval("LICENSE_MODEL")%></td>
                                            <td><%#Eval("MAJORVERSION")%></td>
                                            <td><%#Eval("MINORVERSION")%></td>
                                            <td><%#Eval("BUILDVERSION")%></td>
                                            <td><%#Eval("MANUFACTURER")%></td>
                                            <td><%#Eval("ID")%></td>
                                            <td><%#Eval("IS_INDEX_MAINT_INSTALLED")%></td>
                                            <td><%#Eval("DBA_TEAM_MANAGED")%></td>
                                            <td><%#Eval("number_of_physical_cpus")%></td>
                                            <td><%#Eval("number_of_cores_per_cpu")%></td>
                                            <td><%#Eval("total_number_of_cores")%></td>
                                            <td><%#Eval("number_of_virtual_cpus")%></td>
                                            <td><%#Eval("edition")%></td>
                                            <td><%#Eval("LAST_INVENTORY_DATE")%></td>
                                            <td><%#Eval("SERVICEPACK")%></td>
                                            <td><%#Eval("HYPERTHREAD_RATIO")%></td>
                                        </tr>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </tbody>
                        </table>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <!-- /page content -->
</asp:Content>

