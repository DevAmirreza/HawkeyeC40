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
                <br />
            </div>
            <div class="col-sm-12">
                    <asp:GridView ID="gdOwner" runat="server" AutoGenerateColumns="False" BackColor="#DEBA84" BorderColor="#DEBA84" BorderStyle="None" BorderWidth="1px" CellPadding="3" CellSpacing="2" Height="141px" Width="692px">

                        <FooterStyle BackColor="#F7DFB5" ForeColor="#8C4510" />
                        <HeaderStyle BackColor="#A55129" Font-Bold="True" ForeColor="White" />
                        <PagerStyle ForeColor="#8C4510" HorizontalAlign="Center" />
                        <RowStyle BackColor="#FFF7E7" ForeColor="#8C4510" />
                        <SelectedRowStyle BackColor="#738A9C" Font-Bold="True" ForeColor="White" />
                        <SortedAscendingCellStyle BackColor="#FFF1D4" />
                        <SortedAscendingHeaderStyle BackColor="#B95C30" />
                        <SortedDescendingCellStyle BackColor="#F1E5CE" />
                        <SortedDescendingHeaderStyle BackColor="#93451F" />

                </asp:GridView>
<%--                <table class="table table-striped">
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
                </table>--%>
             
            
                
                
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
