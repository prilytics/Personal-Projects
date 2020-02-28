<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Main.master" CodeBehind="Default.aspx.cs" Inherits="ProductReviews._Default" %>
<asp:Content ID="Content" ContentPlaceHolderID="MainContent" runat="server">
    <dx:ASPxGridView ID="gvOfferStatus" runat="server" AutoGenerateColumns="False" KeyFieldName="OfferStatusID" ClientInstanceName="grid"> 
        <Styles>
            <Header BackColor="#5cb246" ForeColor="#dedede" Font-Bold="true">
            </Header>
        </Styles>
        <SettingsPager Visible="False" Mode="ShowAllRecords">
            <PageSizeItemSettings Visible="true" ShowAllItem="true" AllItemText="All Records" />
        </SettingsPager>
        <Settings ShowFilterRow="True" EnableFilterControlPopupMenuScrolling="True" ShowFilterBar="Visible" ShowHeaderFilterButton="True" ShowPreview="True" ShowGroupedColumns="True" VerticalScrollBarMode="Hidden" />
        <SettingsBehavior AutoExpandAllGroups="True" />
        <SettingsDataSecurity AllowDelete="False" AllowEdit="False" AllowInsert="False" />
        <SettingsSearchPanel Visible="True" />
        <Columns>   
            <dx:GridViewDataTextColumn FieldName="OfferStatusID" Caption="Offer Status ID" ReadOnly="True" Visible="false" VisibleIndex="0">
                <HeaderStyle Wrap="True" />
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn FieldName="OfferDate" Caption="Offer Date" ReadOnly="True" Visible="true" VisibleIndex="1">
                <HeaderStyle Wrap="True" />
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn FieldName="AgentID" Caption="Agent ID" ReadOnly="True" VisibleIndex="2">
                <HeaderStyle Wrap="True" />
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn FieldName="ProductID" Caption="Product ID" ReadOnly="true" Visible="true" VisibleIndex="3">
                <HeaderStyle Wrap="True" />
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn FieldName="OfferType" Caption="Offer Type" ReadOnly="True" VisibleIndex="4">
                <HeaderStyle Wrap="True" />
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn FieldName="OrderDate" Caption="Order Date" ReadOnly="True" VisibleIndex="5">
                <HeaderStyle Wrap="True" />
            </dx:GridViewDataTextColumn> 
            <dx:GridViewDataTextColumn FieldName="ReceivedDate" Caption="Received Date" VisibleIndex="6">
                <HeaderStyle Wrap="True" />
            </dx:GridViewDataTextColumn> 
            <dx:GridViewDataTextColumn FieldName="ReviewSubmittedDate" Caption="Review Submitted Date" VisibleIndex="6">
                <HeaderStyle Wrap="True" />
            </dx:GridViewDataTextColumn> 
            <dx:GridViewDataTextColumn FieldName="ReviewLiveDate" Caption="Review Live Date" VisibleIndex="6">
                <HeaderStyle Wrap="True" />
            </dx:GridViewDataTextColumn> 
            <dx:GridViewDataTextColumn FieldName="RefundRequestedDate" Caption="Refund Requested Date" VisibleIndex="6">
                <HeaderStyle Wrap="True" />
            </dx:GridViewDataTextColumn> 
            <dx:GridViewDataTextColumn FieldName="ProductCost" Caption="Product Cost" VisibleIndex="6">
                <HeaderStyle Wrap="True" />
            </dx:GridViewDataTextColumn> 
            <dx:GridViewDataTextColumn FieldName="RefundedAmount" Caption="RefundedAmount" VisibleIndex="6">
                <HeaderStyle Wrap="True" />
            </dx:GridViewDataTextColumn>  
        </Columns>    
        <SettingsPager Mode="ShowAllRecords" />
        <SettingsPopup>
            <EditForm Width="600" Modal="true" >
                <SettingsAdaptivity Mode="OnWindowInnerWidth" SwitchAtWindowInnerWidth="768" />
            </EditForm>
        </SettingsPopup>
        <Templates>
            <EditForm>
                <div style="padding: 4px 3px 4px">
                    <dx:ASPxPageControl runat="server" ID="pageControl" Width="100%">
                        <TabPages>
                            <dx:TabPage Text="Info" Visible="true">
                                <ContentCollection>
                                    <dx:ContentControl runat="server">
                                        <dx:ASPxGridViewTemplateReplacement ID="Editors" ReplacementType="EditFormEditors"
                                            runat="server">
                                        </dx:ASPxGridViewTemplateReplacement>
                                    </dx:ContentControl>
                                </ContentCollection>
                            </dx:TabPage>
                            <dx:TabPage Text="Notes" Visible="true">
                                <ContentCollection>
                                    <dx:ContentControl runat="server">
                                        <dx:ASPxMemo runat="server" ID="notesEditor" Text='<%# Eval("OfferNotes")%>' Width="100%" Height="93px" />
                                    </dx:ContentControl>
                                </ContentCollection>
                            </dx:TabPage>
                        </TabPages>
                    </dx:ASPxPageControl>
                </div>
                <div style="text-align: right; padding: 2px">
                    <dx:ASPxGridViewTemplateReplacement ID="UpdateButton" ReplacementType="EditFormUpdateButton"
                        runat="server">
                    </dx:ASPxGridViewTemplateReplacement>
                    <dx:ASPxGridViewTemplateReplacement ID="CancelButton" ReplacementType="EditFormCancelButton"
                        runat="server">
                    </dx:ASPxGridViewTemplateReplacement>
                </div>
            </EditForm>
        </Templates>
    </dx:ASPxGridView>   


</asp:Content>