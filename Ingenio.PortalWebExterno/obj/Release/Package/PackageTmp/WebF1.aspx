<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebF1.aspx.cs" Inherits="Ingenio.PortalWebExterno.WebF1" %>

<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=12.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <br />
    
    </div>
        <rsweb:ReportViewer ID="ReportViewer1" runat="server" Font-Names="Verdana" Font-Size="8pt" Height="1042px" WaitMessageFont-Names="Verdana" WaitMessageFont-Size="14pt" Width="852px">
            <LocalReport ReportPath="Certifica.rdlc">
                <DataSources>
                    <rsweb:ReportDataSource DataSourceId="ObjectDataSource3" Name="DataSet1" />
                    <rsweb:ReportDataSource DataSourceId="ObjectDataSource2" Name="DataSet2" />
                    <rsweb:ReportDataSource DataSourceId="ObjectDataSource1" Name="DataSet3" />
                    <rsweb:ReportDataSource DataSourceId="ObjectDataSource4" Name="DataSet4" />
                    <rsweb:ReportDataSource DataSourceId="ObjectDataSource5" Name="CREDITOS" />
                    <rsweb:ReportDataSource DataSourceId="ObjectDataSource7" Name="RENDIMIENTOS" />
                    <rsweb:ReportDataSource DataSourceId="ObjectDataSource8" Name="INTERESES" />
                    <rsweb:ReportDataSource DataSourceId="ObjectDataSource9" Name="RETENCIONES" />
                    <rsweb:ReportDataSource DataSourceId="ObjectDataSource10" Name="IVA" />
                    <rsweb:ReportDataSource DataSourceId="ObjectDataSource12" Name="ICA" />
                    <rsweb:ReportDataSource DataSourceId="ObjectDataSource14" Name="Cre_vivienda" />
                </DataSources>
            </LocalReport>
        </rsweb:ReportViewer>
        <asp:ObjectDataSource ID="ObjectDataSource14" runat="server" OldValuesParameterFormatString="original_{0}" SelectMethod="GetData" TypeName="Ingenio.PortalWebExterno.PortaWebTableAdapters.P_EXTRACTO_CREDITOSVIVITableAdapter">
            <SelectParameters>
                <asp:QueryStringParameter Name="CEDULA" QueryStringField="CEDULA" Type="Int64" />
                <asp:QueryStringParameter Name="TIPO" QueryStringField="TIPO" Type="Int32" />
                <asp:QueryStringParameter Name="FECHAINICIAL" QueryStringField="FECHAINICIAL" Type="DateTime" />
                <asp:QueryStringParameter Name="FECHAFINAL" QueryStringField="FECHAFINAL" Type="DateTime" />
            </SelectParameters>
        </asp:ObjectDataSource>
        <asp:ObjectDataSource ID="ObjectDataSource12" runat="server" OldValuesParameterFormatString="original_{0}" SelectMethod="GetData" TypeName="Ingenio.PortalWebExterno.PortaWebTableAdapters.P_EXTRACTO_ICATableAdapter">
            <SelectParameters>
                <asp:QueryStringParameter Name="cedulasociado" QueryStringField="cedulasociado" Type="Int64" />
                <asp:QueryStringParameter Name="fechainicio" QueryStringField="fechainicio" Type="DateTime" />
                <asp:QueryStringParameter Name="fechafinal" QueryStringField="fechafinal" Type="DateTime" />
            </SelectParameters>
        </asp:ObjectDataSource>
        <asp:ObjectDataSource ID="ObjectDataSource11" runat="server"></asp:ObjectDataSource>
        <asp:ObjectDataSource ID="ObjectDataSource10" runat="server" OldValuesParameterFormatString="original_{0}" SelectMethod="GetData" TypeName="Ingenio.PortalWebExterno.PortaWebTableAdapters.P_EXTRACTO_IVATableAdapter">
            <SelectParameters>
                <asp:QueryStringParameter Name="CEDULA" QueryStringField="CEDULA" Type="Int64" />
                <asp:QueryStringParameter Name="TIPO" QueryStringField="TIPO" Type="Int32" />
                <asp:QueryStringParameter Name="FECHAINICIAL" QueryStringField="FECHAINICIAL" Type="DateTime" />
                <asp:QueryStringParameter Name="FECHAFINAL" QueryStringField="FECHAFINAL" Type="DateTime" />
            </SelectParameters>
        </asp:ObjectDataSource>
        <asp:ObjectDataSource ID="ObjectDataSource9" runat="server" OldValuesParameterFormatString="original_{0}" SelectMethod="GetData" TypeName="Ingenio.PortalWebExterno.PortaWebTableAdapters.P_EXTRACTO_RETENCIONESTableAdapter">
            <SelectParameters>
                <asp:QueryStringParameter Name="CEDULA" QueryStringField="CEDULA" Type="Int64" />
                <asp:QueryStringParameter Name="TIPO" QueryStringField="TIPO" Type="Int32" />
                <asp:QueryStringParameter Name="FECHAINICIAL" QueryStringField="FECHAINICIAL" Type="DateTime" />
                <asp:QueryStringParameter Name="FECHAFINAL" QueryStringField="FECHAFINAL" Type="DateTime" />
            </SelectParameters>
        </asp:ObjectDataSource>
        <asp:ObjectDataSource ID="ObjectDataSource8" runat="server" OldValuesParameterFormatString="original_{0}" SelectMethod="GetData" TypeName="Ingenio.PortalWebExterno.PortaWebTableAdapters.P_EXTRACTO_INTERESESTableAdapter">
            <SelectParameters>
                <asp:QueryStringParameter Name="CEDULA" QueryStringField="CEDULA" Type="Int64" />
                <asp:QueryStringParameter Name="TIPO" QueryStringField="TIPO" Type="Int32" />
                <asp:QueryStringParameter Name="FECHAINICIAL" QueryStringField="FECHAINICIAL" Type="DateTime" />
                <asp:QueryStringParameter Name="FECHAFINAL" QueryStringField="FECHAFINAL" Type="DateTime" />
            </SelectParameters>
        </asp:ObjectDataSource>
        <asp:ObjectDataSource ID="ObjectDataSource7" runat="server" OldValuesParameterFormatString="original_{0}" SelectMethod="GetData" TypeName="Ingenio.PortalWebExterno.PortaWebTableAdapters.P_EXTRACTO_RENDIMIENTOSTableAdapter">
            <SelectParameters>
                <asp:QueryStringParameter Name="CEDULA" QueryStringField="CEDULA" Type="Int64" />
                <asp:QueryStringParameter Name="TIPO" QueryStringField="TIPO" Type="Int32" />
                <asp:QueryStringParameter Name="FECHAINICIAL" QueryStringField="FECHAINICIAL" Type="DateTime" />
                <asp:QueryStringParameter Name="FECHAFINAL" QueryStringField="FECHAFINAL" Type="DateTime" />
            </SelectParameters>
        </asp:ObjectDataSource>
        <asp:ObjectDataSource ID="ObjectDataSource5" runat="server" OldValuesParameterFormatString="original_{0}" SelectMethod="GetData" TypeName="Ingenio.PortalWebExterno.PortaWebTableAdapters.P_EXTRACTO_CREDITOSTableAdapter">
            <SelectParameters>
                <asp:QueryStringParameter Name="CEDULA" QueryStringField="CEDULA" Type="Int64" />
                <asp:QueryStringParameter Name="TIPO" QueryStringField="TIPO" Type="Int32" />
                <asp:QueryStringParameter Name="FECHAINICIAL" QueryStringField="FECHAINICIAL" Type="DateTime" />
                <asp:QueryStringParameter Name="FECHAFINAL" QueryStringField="FECHAFINAL" Type="DateTime" />
            </SelectParameters>
        </asp:ObjectDataSource>
        <asp:ObjectDataSource ID="ObjectDataSource4" runat="server" OldValuesParameterFormatString="original_{0}" SelectMethod="GetData" TypeName="Ingenio.PortalWebExterno.PortaWebTableAdapters.P_EXTRACTO_APORTESTableAdapter">
            <SelectParameters>
                <asp:QueryStringParameter Name="CEDULA" QueryStringField="CEDULA" Type="Int64" />
                <asp:QueryStringParameter Name="TIPO" QueryStringField="TIPO" Type="Int32" />
                <asp:QueryStringParameter Name="FECHAINICIAL" QueryStringField="FECHAINICIAL" Type="DateTime" />
                <asp:QueryStringParameter Name="FECHAFINAL" QueryStringField="FECHAFINAL" Type="DateTime" />
            </SelectParameters>
        </asp:ObjectDataSource>
        <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" OldValuesParameterFormatString="original_{0}" SelectMethod="GetData" TypeName="Ingenio.PortalWebExterno.PortaWebTableAdapters.P_EXTRACTOTableAdapter">
            <SelectParameters>
                <asp:QueryStringParameter Name="CEDULA" QueryStringField="CEDULA" Type="Int64" />
                <asp:QueryStringParameter Name="TIPO" QueryStringField="TIPO" Type="Int32" />
                <asp:QueryStringParameter Name="FECHAINICIAL" QueryStringField="FECHAINICIAL" Type="DateTime" />
                <asp:QueryStringParameter Name="FECHAFINAL" QueryStringField="FECHAFINAL" Type="DateTime" />
            </SelectParameters>
        </asp:ObjectDataSource>
        <asp:ObjectDataSource ID="ObjectDataSource3" runat="server" OldValuesParameterFormatString="original_{0}" SelectMethod="GetData" TypeName="Ingenio.PortalWebExterno.PortaWebTableAdapters.P_CALCULO_RFTETableAdapter">
            <SelectParameters>
                <asp:QueryStringParameter Name="cedulasociado" QueryStringField="cedulasociado" Type="Int64" />
                <asp:QueryStringParameter Name="fechainicio" QueryStringField="fechainicio" Type="DateTime" />
                <asp:QueryStringParameter Name="fechafinal" QueryStringField="fechafinal" Type="DateTime" />
            </SelectParameters>
        </asp:ObjectDataSource>
        <asp:ObjectDataSource ID="ObjectDataSource2" runat="server" OldValuesParameterFormatString="original_{0}" SelectMethod="GetData" TypeName="Ingenio.PortalWebExterno.PortaWebTableAdapters.P_CERTIFICA_RFTETableAdapter">
            <SelectParameters>
                <asp:QueryStringParameter Name="cedulasociado" QueryStringField="cedulasociado" Type="Int64" />
                <asp:QueryStringParameter Name="fechainicio" QueryStringField="fechainicio" Type="DateTime" />
                <asp:QueryStringParameter Name="fechafinal" QueryStringField="fechafinal" Type="DateTime" />
            </SelectParameters>
        </asp:ObjectDataSource>
    </form>
</body>
</html>
