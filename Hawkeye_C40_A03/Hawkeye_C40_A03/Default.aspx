<%@ Page Title="" Language="C#" MasterPageFile="~/HomePage.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="AYadollahibastani_C40A02.Login" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="cover cover2">
        <div class="cover_content">

            <div class="col-md-11 centerit">
                <div class="col-md-6">
                    
                    <div class="banner align-left">
                         
                        <asp:Panel ID="customerLogin" runat="server">
                                                    <h2>Login </h2>
                            <asp:Label ID="lblErrors" runat="server" Text=""></asp:Label><br />
                        <asp:Label ID="lblUsername" runat="server" Text="Email" CssClass="label-control"></asp:Label>
                        <asp:TextBox ID="txtUsername" runat="server" CssClass="form-control"></asp:TextBox>
                        <asp:Label ID="lblPass" runat="server" Text="Password" CssClass="label-control"></asp:Label>
                        <asp:TextBox ID="txtPass" runat="server" CssClass="form-control" TextMode="Password"></asp:TextBox>
                        <asp:CheckBox ID="chKeepLogin" runat="server" Text="&nbsp Keep me logged in" />
                        <br />

                        </asp:Panel>
                        <asp:Button CssClass="btn btn-primary" ID="btnLogin" runat="server" Text="Login" OnClick="btnLogin_Click" />
                    </div>

                </div>
                <div class="col-md-6">
                    <div class="banner align-left">
                        <p>
                            New Customer ? 
                                Please click here if you wish to book a reservation right now ! 
                            <br />
                            <asp:Button CssClass="btn btn-primary" ID="btnBookNow" runat="server" Text="Book Now" OnClick="btnBookNow_Click" />
                        </p>
                    </div>
                </div>
            </div>
        </div>
    </div>
            <span class="glyphicon glyphicon-option-horizontal continue"></span>   

    <div class="container">
        <h2>Help</h2>
        <p>You have problem loging to your account <br /> Please contact our customer service at 1-819-456-5678
            
        </p>
    </div>
    <asp:SqlDataSource ID="dsOwnerEmails" runat="server" 
        ConnectionString="<%$ ConnectionStrings:ConnectionString %>" 
        ProviderName="<%$ ConnectionStrings:ConnectionString.ProviderName %>" 
        SelectCommand="SELECT OWNER_EMAIL FROM HVK_OWNER"></asp:SqlDataSource>
</asp:Content>
