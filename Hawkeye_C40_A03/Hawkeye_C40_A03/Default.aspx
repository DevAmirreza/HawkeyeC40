<%@ Page Title="" Language="C#" MasterPageFile="~/HomePage.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="AYadollahibastani_C40A02.Login" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="cover cover2">
        <div class="cover_content">

            <div class="col-md-11 centerit">
                <div class="col-md-6">
                    
                    <div class="banner align-left">
                                                <asp:Button CssClass="btn btn-default" ID="btnClerk" runat="server" Text="Clerk" OnClick="btnClerk_Click" />
                        <asp:Button CssClass="btn btn-default" ID="btnCustomer" runat="server" Text="Customer" OnClick="btnCustomer_Click" />
                                                <asp:Panel ID="clerkLogin" runat="server">
                                                    <h4>Login Clerk</h4>
                        <asp:Label ID="Label1" runat="server" Text="Email or Phone Number" CssClass="label-control"></asp:Label>
                        <asp:TextBox ID="TextBox1" runat="server" CssClass="form-control"></asp:TextBox>
                        <asp:Label ID="Label2" runat="server" Text="Password" CssClass="label-control"></asp:Label>
                        <asp:TextBox ID="TextBox2" runat="server" CssClass="form-control"></asp:TextBox>
                        <asp:CheckBox ID="CheckBox1" runat="server" Text="keep me login" />
                        <br />

                        </asp:Panel>

                        <asp:Panel ID="customerLogin" runat="server">
                                                    <h4>Login Customer</h4>
                        <asp:Label ID="lblUsername" runat="server" Text="Email or Phone Number" CssClass="label-control"></asp:Label>
                        <asp:TextBox ID="txtUsername" runat="server" CssClass="form-control"></asp:TextBox>
                        <asp:Label ID="lblPass" runat="server" Text="Password" CssClass="label-control"></asp:Label>
                        <asp:TextBox ID="txtPass" runat="server" CssClass="form-control"></asp:TextBox>
                        <asp:CheckBox ID="chKeepLogin" runat="server" Text="keep me login" />
                        <br />

                        </asp:Panel>
                        <asp:Button CssClass="btn btn-primary" ID="btnLogin" runat="server" Text="Login" />
                    </div>

                </div>
                <div class="col-md-6">
                    <div class="banner align-left">
                        <p>
                            New Customer ? 
                                Please click here if you wish to book a reservation right now ! 
                            <br />
                            <asp:Button CssClass="btn btn-primary" ID="btnBookNow" runat="server" Text="Book Now" />
                        </p>
                    </div>
                </div>
            </div>
        </div>
    </div>
            <span class="glyphicon glyphicon-option-horizontal continue"></span>   

    <div class="container">
        <h2>FAQ</h2>
        <p>You have problem loging to your account <br /> Please contact our customer service at 1-819-456-5678</p>
    </div>
</asp:Content>
