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
            <asp:LinkButton ID="btnAddNew" href="/manageCustomer.aspx" runat="server" CssClass="btn btn-default" OnClick="btnAddNew_Click">Add a new customer </asp:LinkButton>
            <h4>Search</h4>
            <div class="col-sm-12">
                
                <asp:TextBox ID="txtEmail" Name="email" runat="server" CssClass="form-control col-sm-6"></asp:TextBox>
                <asp:Button ID="btnSearch" runat="server" CssClass=" btn btn-primary" Text="Search" OnClick="btnSearch_Click" />
                <br />
                <asp:ObjectDataSource ID="odFullOwner" runat="server" SelectMethod="getFullOwner" TypeName="HawkeyehvkBLL.Owner">
                    <SelectParameters>
                        <asp:ControlParameter ControlID="txtEmail" DefaultValue="bque@gmail.com" Name="email" PropertyName="Text" Type="String" />
                    </SelectParameters>
                </asp:ObjectDataSource>
                <br />
                <br />
            </div>
            <div class="col-sm-12">
                 <%--   <asp:GridView ID="gdOwner" runat="server" AutoGenerateColumns="False" BackColor="#DEBA84" BorderColor="#DEBA84" BorderStyle="None" BorderWidth="1px" CellPadding="3" CellSpacing="2" Height="141px" Width="821px" DataSourceID="odFullOwner">

                        <Columns>
                            <asp:TemplateField HeaderText="firstName" SortExpression="firstName">
                                <EditItemTemplate>
                                    <asp:TextBox ID="TextBox4" runat="server" Text='<%# Bind("firstName") %>'></asp:TextBox>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="Label4" runat="server" Text='<%# Bind("firstName") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="lastName" SortExpression="lastName">
                                <EditItemTemplate>
                                    <asp:TextBox ID="TextBox3" runat="server" Text='<%# Bind("lastName") %>'></asp:TextBox>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="Label3" runat="server" Text='<%# Bind("lastName") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="email" SortExpression="email">
                                <EditItemTemplate>
                                    <asp:TextBox ID="TextBox2" runat="server" Text='<%# Bind("email") %>'></asp:TextBox>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="Label2" runat="server" Text='<%# Bind("email") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="phoneNumber" SortExpression="phoneNumber">
                                <EditItemTemplate>
                                    <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("phoneNumber") %>'></asp:TextBox>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="Label1" runat="server" Text='<%# Bind("phoneNumber") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>

                        <FooterStyle BackColor="#F7DFB5" ForeColor="#8C4510" />
                        <HeaderStyle BackColor="#A55129" Font-Bold="True" ForeColor="White" />
                        <PagerStyle ForeColor="#8C4510" HorizontalAlign="Center" />
                        <RowStyle BackColor="#FFF7E7" ForeColor="#8C4510" />
                        <SelectedRowStyle BackColor="#738A9C" Font-Bold="True" ForeColor="White" />
                        <SortedAscendingCellStyle BackColor="#FFF1D4" />
                        <SortedAscendingHeaderStyle BackColor="#B95C30" />
                        <SortedDescendingCellStyle BackColor="#F1E5CE" />
                        <SortedDescendingHeaderStyle BackColor="#93451F" />

                </asp:GridView>--%>
                <table class="table table-striped">
                    <thead>
                        <tr>
                            <th>Select</th>
                            <th>Firstname</th>
                            <th>Lastname</th>
                            <th>Email</th>
                            <th>Phone#</th>


                        </tr>
                    </thead>
                    <tbody>
                        <tr>
                            <td>
                                <asp:CheckBox AutoPostBack="true" ID="cdOwnerSelected" runat="server" OnCheckedChanged="cdOwnerSelected_CheckedChanged" /></td>
                            <td>John</td>
                            <td>Doe</td>
                            <td>john@example.com</td>
                            <td>(438)990-6065</td>
                            <td>
                                <asp:LinkButton href="/manageCustomer.aspx" ID="customerEdit" runat="server" OnClick="customerEdit_Click">Edit</asp:LinkButton>
                            </td>

                        </tr>




                    </tbody>
                </table>
             
            
                
                
                   <asp:Panel runat="server" ID="editDisplay">
                    <asp:LinkButton ID="btnBookNewReservation" href="/managePet.aspx" runat="server" CssClass="btn btn-default">Book a new reservation </asp:LinkButton>
                    <asp:LinkButton ID="btnAddPet" href="/managePet.aspx" runat="server" CssClass="btn btn-default">Add a new Pet </asp:LinkButton>
                    <%--                    <asp:LinkButton ID="btnViewPet" runat="server" CssClass="btn btn-default" OnClick="btnViewPet_Click">Vew Pet List </asp:LinkButton>--%>
                    <asp:Panel runat="server" ID="viewPet">
                        <div class="col-sm-6">
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
