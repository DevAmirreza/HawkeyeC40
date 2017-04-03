<%@ Page Title="" Language="C#" MasterPageFile="~/Application.Master" AutoEventWireup="true" CodeBehind="owners.aspx.cs" Inherits="AYadollahibastani_C40A02.owner" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
    <div class="page_title">
        <%--Find a New Customer--%>
    </div>
    <div class="row">
        <div class="container">
            <asp:LinkButton ID="btnAddNew" href="/manageCustomer.aspx" runat="server" CssClass="btn btn-default">Add a new customer </asp:LinkButton>
            <h3>Search</h3>
            <div class="col-sm-12">
                <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control col-sm-6"></asp:TextBox>
                <asp:TextBox ID="txtPhone" runat="server" CssClass="form-control col-sm-6"></asp:TextBox>
                <asp:Button ID="btnSearch" runat="server" CssClass=" btn btn-primary" Text="Search" />
                <br />
                <br />
            </div>
            <div class="col-sm-12">
                <table class="table table-striped">
                    <thead>
                        <tr>
                            <th>Firstname</th>
                            <th>Lastname</th>
                            <th>Email</th>
                            <th>Phone#</th>

                        </tr>
                    </thead>
                    <tbody>
                        <tr>
                            <td>John</td>
                            <td>Doe</td>
                            <td>john@example.com</td>
                            <td>(438)990-6065</td>
                            <td>
                                <asp:LinkButton ID="customerEdit" runat="server" OnClick="customerEdit_Click">Edit</asp:LinkButton>
                            </td>

                        </tr>


                        <tr>
                            <td>John</td>
                            <td>Doe</td>
                            <td>john@example.com</td>
                            <td>(438)990-6065</td>
                            <td>
                                <asp:LinkButton ID="LinkButton1" runat="server" OnClick="customerEdit_Click">Edit</asp:LinkButton>
                            </td>

                        </tr>

                        <tr>
                            <td>John</td>
                            <td>Doe</td>
                            <td>john@example.com</td>
                            <td>(438)990-6065</td>
                            <td>
                                <asp:LinkButton ID="LinkButton2" runat="server" OnClick="customerEdit_Click">Edit</asp:LinkButton>
                            </td>

                        </tr>


                        <tr>
                            <td>John</td>
                            <td>Doe</td>
                            <td>john@example.com</td>
                            <td>(438)990-6065</td>
                            <td>
                                <asp:LinkButton ID="LinkButton3" runat="server" OnClick="customerEdit_Click">Edit</asp:LinkButton>
                            </td>

                        </tr>


                    </tbody>
                </table>
            </div>
        </div>
    </div>


</asp:Content>
