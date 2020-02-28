<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Main.master" CodeBehind="Refunds.aspx.cs" Inherits="ProductReviews.Admin.Refunds" %>

<asp:Content ID="Content" ContentPlaceHolderID="MainContent" runat="server">
    Varience to use for possible products: <dx:ASPxTextBox ID="txtVariance" runat="server" Text="15"></dx:ASPxTextBox>
    <dx:ASPxButton ID="btnChangeVariance" runat="server" CssClass="btn btn-success" Text="Change Variance" OnClick="btnChangeVariance_Click"></dx:ASPxButton>
    <br />
    <dx:ASPxButton ID="btnClearFilter" runat="server" CssClass="btn btn-warning" Text="Clear Filter" OnClick="btnClearFilter_Click"></dx:ASPxButton>
    <br />
    <h2>Refunds</h2>
    <div> 
        <dx:ASPxGridView ID="gvRefunds" ClientInstanceName="gridRefunds" runat="server" AutoGenerateColumns="False" KeyFieldName="RefundID">
            <Styles>
                <Header BackColor="#5cb246" ForeColor="#dedede" Font-Bold="true">
                </Header>
            </Styles> 
            <Settings ShowFilterRow="False" EnableFilterControlPopupMenuScrolling="True" ShowFilterBar="Visible" ShowHeaderFilterButton="True" ShowPreview="True" ShowGroupedColumns="True" VerticalScrollBarMode="Hidden" />
            <SettingsDataSecurity AllowDelete="False" AllowEdit="False" AllowInsert="False" />
            <SettingsSearchPanel Visible="False" /> 
            <SettingsPager PageSize="10">
                <PageSizeItemSettings Visible="true" />
            </SettingsPager>
            <Columns>
                <dx:GridViewDataColumn Caption="Refund ID" FieldName="RefundID">
                    <HeaderStyle Wrap="True" HorizontalAlign="Center" />
                    <DataItemTemplate>
                        <dx:ASPxHyperLink ID="hyperLink" runat="server" OnInit="hyperLink_Init"/>
                    </DataItemTemplate>
                </dx:GridViewDataColumn>  
                <dx:GridViewDataColumn Caption="Product ID" FieldName="ProductID">
                    <HeaderStyle Wrap="True" HorizontalAlign="Center" />
                </dx:GridViewDataColumn>
                <dx:GridViewDataColumn Caption="Refund Type" FieldName="RefundType">
                    <HeaderStyle Wrap="True" HorizontalAlign="Center" />
                </dx:GridViewDataColumn>
                <dx:GridViewDataColumn Caption="Refund Date" FieldName="RefundDate">
                    <HeaderStyle Wrap="True" HorizontalAlign="Center" />
                </dx:GridViewDataColumn>
                <dx:GridViewDataColumn Caption="PP Transaction ID" FieldName="PPTransactionID">
                    <HeaderStyle Wrap="True" HorizontalAlign="Center" />
                </dx:GridViewDataColumn>
                <dx:GridViewDataColumn Caption="Amount" FieldName="Amount">
                    <HeaderStyle Wrap="True" HorizontalAlign="Center" />
                </dx:GridViewDataColumn>
                <dx:GridViewDataColumn Caption="PP Fees" FieldName="PPFees">
                    <HeaderStyle Wrap="True" HorizontalAlign="Center" />
                </dx:GridViewDataColumn> 
                <dx:GridViewDataColumn Caption="Net Amount" FieldName="NetAmount">
                    <HeaderStyle Wrap="True" HorizontalAlign="Center" />
                </dx:GridViewDataColumn>
                <dx:GridViewDataColumn Caption="Gift Card Type" FieldName="GiftCardType">
                    <HeaderStyle Wrap="True" HorizontalAlign="Center" />
                </dx:GridViewDataColumn>
                <dx:GridViewDataColumn Caption="Gift Card Number" FieldName="GiftCardNumber">
                    <HeaderStyle Wrap="True" HorizontalAlign="Center" />
                </dx:GridViewDataColumn>
                <dx:GridViewDataColumn Caption="Refund Notes" FieldName="RefundNotes">
                    <HeaderStyle Wrap="True" HorizontalAlign="Center" />
                </dx:GridViewDataColumn>
                <dx:GridViewDataColumn Caption="Last Modified" FieldName="LastModified">
                    <HeaderStyle Wrap="True" HorizontalAlign="Center" />
                </dx:GridViewDataColumn>
                <dx:GridViewDataColumn Caption="Modified By" FieldName="ModifiedBy">
                    <HeaderStyle Wrap="True" HorizontalAlign="Center" />
                </dx:GridViewDataColumn> 
            </Columns>
        </dx:ASPxGridView>  
     </div>
    <h2>Possible Products</h2>
    <div>  
        <dx:ASPxGridView ID="gvPossibleProducts" ClientInstanceName="gridRefunds" runat="server" AutoGenerateColumns="False" KeyFieldName="ProductID" >
            <Styles>
                <Header BackColor="#5cb246" ForeColor="#dedede" Font-Bold="true">
                </Header>
            </Styles> 
            <Settings ShowFilterRow="False" EnableFilterControlPopupMenuScrolling="True" ShowFilterBar="Visible" ShowHeaderFilterButton="True" ShowPreview="True" ShowGroupedColumns="True" VerticalScrollBarMode="Hidden" />
            <SettingsDataSecurity AllowDelete="False" AllowEdit="False" AllowInsert="False" />
            <SettingsSearchPanel Visible="False" /> 
            <SettingsPager PageSize="100">
                <PageSizeItemSettings Visible="true" />
            </SettingsPager>
            <Columns>
                <dx:GridViewDataColumn Caption="Product ID" FieldName="ProductID">
                    <HeaderStyle Wrap="True" HorizontalAlign="Center" />
                    <DataItemTemplate>
                        <dx:ASPxHyperLink ID="hyperLink" runat="server" OnInit="hyperLinkProductID_Init"/>
                    </DataItemTemplate>
                </dx:GridViewDataColumn>  
                <dx:GridViewDataColumn Caption="Messenger Link" FieldName="MessengerLink">
                    <HeaderStyle Wrap="True" HorizontalAlign="Center" />
                    <DataItemTemplate>
                        <dx:ASPxHyperLink ID="hyperLinkMessenger" runat="server" OnInit="hyperLinkMessenger_Init" />
                    </DataItemTemplate>
                </dx:GridViewDataColumn>  
                <dx:GridViewDataColumn Caption="Agent ID" FieldName="AgentID">
                    <HeaderStyle Wrap="True" HorizontalAlign="Center" />
                </dx:GridViewDataColumn>
                <dx:GridViewDataColumn Caption="Product Title" FieldName="ProductTitle">
                    <HeaderStyle Wrap="True" HorizontalAlign="Center" />
                </dx:GridViewDataColumn> 
                <dx:GridViewDataColumn Caption="Item Subtotal" FieldName="ItemSubtotal">
                    <HeaderStyle Wrap="True" HorizontalAlign="Center" />
                </dx:GridViewDataColumn> 
                <dx:GridViewDataColumn Caption="Order Total" FieldName="Total">
                    <HeaderStyle Wrap="True" HorizontalAlign="Center" />
                </dx:GridViewDataColumn> 
            </Columns>
        </dx:ASPxGridView>  
     </div>
</asp:Content>