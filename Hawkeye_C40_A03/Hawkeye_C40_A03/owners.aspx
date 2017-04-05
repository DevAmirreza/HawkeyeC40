<%@ Page Title="" Language="C#" MasterPageFile="~/Application.Master" AutoEventWireup="true" CodeBehind="owners.aspx.cs" Inherits="AYadollahibastani_C40A02.owners" %>

<%@ Register Src="~/listPets.ascx" TagPrefix="uc1" TagName="listPets" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
    <div class="page_title">
        Find a New Customer
    </div>
    <div class="row">
        <div class="container">
            <asp:LinkButton ID="btnAddNew" runat="server" CssClass="btn btn-default" OnClick="btnAddNew_Click">Add a new customer </asp:LinkButton><br />
            <h4>Search</h4>
            <div class="col-sm-12">
                <asp:TextBox ID="txtEmail" Name="email" runat="server" CssClass="form-control col-sm-6"></asp:TextBox>
                <asp:Button ID="btnSearch" runat="server" CssClass=" btn btn-primary" Text="Search"  />
                <br />
                <asp:ObjectDataSource ID="odFullOwner" runat="server" SelectMethod="getFullOwner" TypeName="HawkeyehvkBLL.Owner" OldValuesParameterFormatString="original_{0}">
                    <SelectParameters>
                        <asp:ControlParameter ControlID="txtEmail" DefaultValue="OWNER_EMAIL" Name="email" PropertyName="Text" Type="String" />
                    </SelectParameters>
                </asp:ObjectDataSource>
                <br />
                <br />
            </div>
            <div class="col-sm-12">
                    <asp:GridView ID="gdOwner"  OnRowCommand="gdOwner_RowCommand" CssClass="table table-hover " runat="server" AutoGenerateColumns="False" DataSourceID="odFullOwner">
                        <Columns>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:Button  CssClass="btn btn-default" runat="server" Text="View Details"   CommandArgument='<%# Eval("ownerNumber") %>' CausesValidation="false" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Customer Name" SortExpression="firstName">
                                <ItemTemplate>
                                    <asp:Label ID="Label4" runat="server" Text='<%# Eval("lastname").ToString()+ " " + Eval("firstName").ToString()%>' ></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Email" SortExpression="email">
                                <EditItemTemplate>
                                    <asp:TextBox ID="TextBox2" runat="server" Text='<%# Bind("email") %>'></asp:TextBox>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="Label2" runat="server" Text='<%# Bind("email") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Phone Number" SortExpression="phoneNumber">
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtEditPhone" runat="server" Text='<%# Bind("phoneNumber") %>'></asp:TextBox>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="gdlblPhone" runat="server" Text='<%# Bind("phoneNumber") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                 
                            

                        </Columns>


                </asp:GridView>
                
                   <asp:Panel runat="server" ID="editDisplay">
                    <asp:LinkButton ID="btnBookNewReservation" runat="server" CssClass="btn btn-default" OnClick="btnBookNewReservation_Click">Book a new reservation </asp:LinkButton>
                    <asp:LinkButton ID="btnAddPet" href="/managePet.aspx" runat="server" CssClass="btn btn-default">Add a new Pet </asp:LinkButton>
                    <asp:LinkButton ID="btnEditCustomer" href="/manageCustomer.aspx" runat="server" CssClass="btn btn-default">Manage Customer </asp:LinkButton>
                    <asp:Panel runat="server" ID="viewPet">
                        <div class="col-sm-6">
                            <br />
                            Reservation History
                        </div>
                        <div class="col-sm-6">
                            <uc1:listPets runat="server" ID="listPets" />
                        </div>
                    </asp:Panel>
                </asp:Panel>



            </div>
        </div>
    </div>


</asp:Content>
