<%@ Page Title="ManageReservation" Language="C#" MasterPageFile="~/Application.Master" AutoEventWireup="true" CodeBehind="manageReservation.aspx.cs" Inherits="AYadollahibastani_C40A02.manageReservation" %>

<%@ Register Src="~/CalendarControl.ascx" TagPrefix="uc1" TagName="CalendarControl" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
    <div class="page_title">
        <p>Manage Reservation</p>
    </div>
    <div class="container">

        
        <div class="row">
                        
            <div class="col-md-6">
                <div class="error_box alert-danger ">
<%--                    <span class="glyphicon glyphicon-warning-sign"></span>--%>
                    <asp:ValidationSummary ID="ValidationSummary1" runat="server"  />
                </div>
            </div>
        </div>
        <asp:Panel runat="server" ID="reservationPanel">
            <div class="form-group">
                <h4 class="subtitle" id="pageTitle" runat="server">Reservation Details</h4>
                <div class="row">
                    <div class="col-sm-6">
                        <label class="label-control col-sm-4">
                            Start Date
                        </label>
                        <uc1:CalendarControl runat="server" ID="UCstartDate" />
                        <div class="error_msg label-control col-sm-6 oneSixity">
                            &nbsp;&nbsp;&nbsp;
                            <asp:RequiredFieldValidator ID="valRequired" runat="server" ControlToValidate="UCstartDate$txtDate" Display="Dynamic" ErrorMessage="Please select a start date "></asp:RequiredFieldValidator>
                        </div>
                    </div>
                    <div class="col-sm-6">
                        <label class="label-control col-sm-4">
                            End Date
                        </label>
                        <uc1:CalendarControl runat="server" ID="UCendDate" />
                        <div class="error_msg label-control col-sm-6 oneSixity">
                            &nbsp;&nbsp;&nbsp;
                           <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="UCendDate$txtDate" ErrorMessage="Please select an end date"></asp:RequiredFieldValidator>
                            <asp:CustomValidator ID="valEndDate" runat="server" ErrorMessage="Your end must be after your start date" ControlToValidate="UCendDate$txtDate"></asp:CustomValidator>
                        </div>

                    </div>
                    <div class="col-sm-6 center">
                        <br />
                        <label class="label-control col-sm-4">Add Your Pet + </label>
                        <asp:DropDownList ID="ddlAddPet" runat="server" CssClass="form-control inlineDdl" AutoPostBack="True" >
                            <asp:ListItem> Select Your Pet</asp:ListItem>
                        </asp:DropDownList>
                        <asp:Button ID="btnAddDog" runat="server" Text="Add" CssClass="btn btn-default" OnClick="btnAddDog_Click"/>

                       <div class="error_msg label-control col-sm-6 oneSixity">
                            &nbsp;&nbsp;&nbsp;
                           <asp:CustomValidator ID="valPetExists" runat="server" ErrorMessage="Pet is already on reservation list ! " ></asp:CustomValidator>
                        </div>
                    </div>
                    <br />
                    <div class="col-sm-6 center">
                        <br />
                        <label id="lblChooseRun" runat="server" class="label-control col-sm-4">Choose Run</label>
                        <asp:DropDownList ID="ddlChooseRun" runat="server" CssClass="form-control short" ></asp:DropDownList>

                    </div>


                        

                </div>
                <br />
                <hr />
                <h4 class="subtitleInline">Pets in Reservation -  </h4><asp:DropDownList ID="ddlPetsInRes" runat="server" CssClass="form-control short inlineDdl" OnSelectedIndexChanged="ddlPetsInRes_SelectedIndexChanged"></asp:DropDownList>
                <asp:Button ID="btnRemovePet" runat="server" Text="Remove" CssClass="btn btn-default" OnClick="btnRemovePet_Click"/>
                <div class="row">
                    <div class="col-md-6 ">
                        <label class="label-control col-sm-4">Services</label>
                        <!-- To be recieve from DB -->
                        <div class="col-sm-8 center push-left">
                            <div class="col-sm-6">
                            <asp:CheckBox ID="chWalk" runat="server" Text="Daily Walk" CssClass="" /><br />
                            <asp:CheckBox ID="chPalytime" runat="server" Text="Daily Playtime" CssClass="" /><br  />
                                <br />
                            </div>
                        </div>
                    </div>
                    </div>
                   
                    
                    <br />
                </div>
            </asp:Panel>
                <div class="pull-left col-md-4 buttons">
                    <asp:Button ID="btnBook" runat="server" Text="Book Now" CssClass="btn btn-primary"/>
                    <asp:Button CssClass="btn btn-default" ID="btnEdit" Text="Edit" runat="server" CausesValidation="False" OnClick="btnEdit_Click1" />
                    <asp:LinkButton ID="btnClear" runat="server">Cancel</asp:LinkButton>

                </div>

            </asp:Panel>
            </div>
    





</asp:Content>
