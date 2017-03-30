<%@ Page Title="" Language="C#" MasterPageFile="~/HomePage.Master" AutoEventWireup="true" CodeBehind="main.aspx.cs" Inherits="AYadollahibastani_C40A02.masterTest" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="cover">
        <div class="conver_content">
            <div class="col-md-6">
                <h2 class="cover_title">Happy Valley Kennel</h2>
            </div>
            <div class="col-md-6">
                <div class="banner">
                    <p>
                        " We offer dog boarding, 
                        dog day care and cat boarding 
                        services at our clean, spacious and modern
                         Ottawa kennel facility. "
                    </p>
                    <asp:Button ID="btnBookFromHomePage" runat="server" Text="Book Now" CssClass="btn btn-primary" />

                </div>
            </div>
        </div>
    </div>

    <span class="glyphicon glyphicon-option-horizontal continue"></span>

    <div class="container">
        <div class="row">
            <div class="col-md-6">
                <p class="message">Massa. Donec id leo enim. Maecenas sed congue quam. Aenean et sodales nunc, vitae tempus nulla. Vivamus rutrum accumsan sagittis. Praesent vitae mollis ante. Suspendisse aliquet ornare bibendum. Pellentesque pretium sem sit amet risus dictum, in iaculis urna consequat. Donec ut purus quis dui dignissim euismod. Phasellus ut enim massa. Cras consectetur orci id sem ornare venenatis.</p>
            </div>
            <div class="col-md-6 detail">
                <span class="glyphicon glyphicon-home continue"></span>
                <p>200 Available Run</p>
                <span class="glyphicon glyphicon-time continue"></span>
                <p>Flexible Hours</p>
                <span class="glyphicon glyphicon-earphone continue"></span>
                <p>
                    Call Us Today
                    <br />
                    1-819-340-4567
                </p>
            </div>

        </div>
        <br />
        <div class="row">
            <div class="col-md-6">

                <iframe src="https://www.google.com/maps/embed?pb=!1m18!1m12!1m3!1d2800.2590629446963!2d-75.73594918509225!3d45.424278844128565!2m3!1f0!2f0!3f0!3m2!1i1024!2i768!4f13.1!3m3!1m2!1s0x4cce047cd2444bbb%3A0x77912ef68ec6f274!2s11+Boulevard+Saint-Joseph%2C+Gatineau%2C+QC+J8Y+3V6!5e0!3m2!1sen!2sca!4v1487480802129" width="100%" height="250" frameborder="0" style="border:0" allowfullscreen></iframe>
            </div>
            <div class="col-md-6">
                    <h2>Address:</h2>
                <p>11 Boulevard Saint-Joseph, Gatineau, QC J8Y 3V6
</p>            
            </div>

        </div>
    </div>

</asp:Content>
