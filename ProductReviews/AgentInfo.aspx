<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Main.master" CodeBehind="AgentInfo.aspx.cs" Inherits="ProductReviews.AgentInfo" %>

<asp:Content ID="Content" ContentPlaceHolderID="MainContent" runat="server"> 
    <dx:ASPxGridView ID="gvAgent" runat="server" AutoGenerateColumns="False" KeyFieldName="AgentID" ClientInstanceName="gridAgent" OnCellEditorInitialize="gvAgent_CellEditorInitialize"
        OnRowInserted="gvAgent_RowInserted"> 
        <Styles>
            <Header BackColor="#5cb246" ForeColor="#dedede" Font-Bold="true">
            </Header>
        </Styles>
        <SettingsPager Visible="False" Mode="ShowAllRecords">
            <PageSizeItemSettings Visible="true" ShowAllItem="true" AllItemText="All Records" />
        </SettingsPager>
        <Settings ShowFilterRow="True" EnableFilterControlPopupMenuScrolling="True" ShowFilterBar="Visible" ShowHeaderFilterButton="True" ShowPreview="True" ShowGroupedColumns="True" VerticalScrollBarMode="Hidden" />
        <SettingsBehavior AutoExpandAllGroups="True" />
        <SettingsDataSecurity AllowDelete="False" AllowEdit="True" AllowInsert="True" />
        <SettingsSearchPanel Visible="True" />
        <Columns>   
            <dx:GridViewCommandColumn ShowNewButtonInHeader="true" ShowEditButton="true" VisibleIndex="0" />
            <dx:GridViewDataColumn UnboundType="String" Caption="Edit Agent" Width="75" HeaderStyle-Wrap="True" CellStyle-CssClass="agent-bg" CellStyle-HorizontalAlign="Center">
                <dataitemtemplate>
                    <asp:ImageButton  ID="btnEditAgent" runat="server" EnableViewState="false" CssClass="agent-imgbtn" ImageUrl="/Images/edit_icon.png" CommandName="EditAgent" CommandArgument='<%# Container.ItemIndex %>' OnCommand="btnEditAgent_Command" >
                    </asp:ImageButton> 
                </dataitemtemplate>
                <EditFormSettings Visible="false" />
            </dx:GridViewDataColumn>
            <dx:GridViewDataTextColumn FieldName="AgentID" Caption="Agent ID" ReadOnly="True" Visible="false" VisibleIndex="0">
                <HeaderStyle Wrap="True" />
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn FieldName="MessengerLink" Caption="Messenger Link" Visible="true" ReadOnly="false" VisibleIndex="1">
                <HeaderStyle Wrap="True" />
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn FieldName="FullName" Caption="Full Name" ReadOnly="false" VisibleIndex="2">
                <HeaderStyle Wrap="True" />
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn FieldName="WhatsAppID" Caption="Whats App ID" Visible="true" ReadOnly="false" VisibleIndex="3">
                <HeaderStyle Wrap="True" />
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn FieldName="AgentEmail" Caption="Agent Email" ReadOnly="false" VisibleIndex="4">
                <HeaderStyle Wrap="True" />
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn FieldName="AgentPhone" Caption="Agent Phone" ReadOnly="false" VisibleIndex="5">
                <HeaderStyle Wrap="True" />
            </dx:GridViewDataTextColumn> 
            <dx:GridViewDataTextColumn FieldName="AgentNotes" Caption="Agent Notes" ReadOnly="false" VisibleIndex="6">
                <HeaderStyle Wrap="True" />
            </dx:GridViewDataTextColumn> 
            <dx:GridViewDataDateColumn  FieldName="LastModified" Caption="Last Modified" ReadOnly="true" VisibleIndex="6">
                <HeaderStyle Wrap="True" />
            </dx:GridViewDataDateColumn> 
            <dx:GridViewDataTextColumn FieldName="ModifiedBy" Caption="Modified By" ReadOnly="true" VisibleIndex="6">
                <HeaderStyle Wrap="True" />
            </dx:GridViewDataTextColumn>  
        </Columns>     
        <SettingsEditing EditFormColumnCount="3" Mode="PopupEditForm" />
        <SettingsPager Mode="ShowAllRecords" />  
    </dx:ASPxGridView>   


</asp:Content>