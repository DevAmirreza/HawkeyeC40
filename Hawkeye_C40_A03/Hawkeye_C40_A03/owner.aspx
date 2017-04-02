<%@ Page Title="" Language="C#" MasterPageFile="~/Application.Master" AutoEventWireup="true" CodeBehind="owner.aspx.cs" Inherits="AYadollahibastani_C40A02.owner" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
    <div class="page_title">
        Find a New Custoemr
    </div>
    <div class="row">
        <div class="container">
            <h3>Search</h3>
            <div class="col-sm-12">
                <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control col-sm-6"></asp:TextBox>
                <asp:TextBox ID="txtPhone" runat="server" CssClass="form-control col-sm-6"></asp:TextBox>

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
                                <asp:LinkButton ID="customerEdit" runat="server" OnClick="customerEdit_Click" >Edit</asp:LinkButton>
                            </td>

      </tr>

    </tbody>
  </table>
            </div>
        </div>
    </div>


</asp:Content>
