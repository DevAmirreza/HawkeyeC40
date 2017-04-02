<%@ Page Title="ManageReservation" Language="C#" MasterPageFile="~/Application.Master" AutoEventWireup="true" CodeBehind="manageReservation.aspx.cs" Inherits="AYadollahibastani_C40A02.manageReservation" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
    <div class="page_title">
        <p>Manage Reservation</p>
    </div>
    <div class="container">
        <asp:Panel ID="searchPanel" runat="server">
            <h3>Please Choose Your Customer</h3>
            
        </asp:Panel>

                <asp:Panel ID="noReservationPanel" runat="server">
                    <h4>There are curently no reservation booked</h4>

                </asp:Panel>
        <asp:Panel ID="editPanel" runat="server">
        <div class="row">
            <div class="col-md-6">
                <asp:Button CssClass="btn btn-default" ID="btnEdit" Text="Edit" runat="server" OnClick="btnEdit_Click" CausesValidation="False" />
                <asp:Button CssClass="btn btn-default" ID="btnNewReservation" Text="Book A Reservation" runat="server" CausesValidation="false" OnClick="btnNewReservation_Click"  />
          <br />
                  </div>
            <div class="col-md-6">
                <div class="error_box alert-danger ">
<%--                    <span class="glyphicon glyphicon-warning-sign"></span>--%>
                    <asp:ValidationSummary ID="ValidationSummary1" runat="server"  />
                </div>
            </div>
        </div>
        <asp:Panel runat="server" ID="reservationPanel">
            <div class="form-group">
                <h4 class="subtitle">Reservation Details</h4>
                <div class="row">
                    <div class="col-sm-6">
                        <label class="label-control col-sm-4">
                            Start Date
                        </label>
                        <input name="startDate" runat="server" id="txtStartDate" class="form-control datepicker"  />
                        <div class="error_msg label-control col-sm-6 oneSixity">
                            &nbsp;&nbsp;&nbsp;
                            <asp:RequiredFieldValidator ID="valRequired" runat="server" ControlToValidate="txtStartDate" Display="Dynamic" ErrorMessage="Please select a start date "></asp:RequiredFieldValidator>
                        </div>
                    </div>
                    <div class="col-sm-6">
                        <label class="label-control col-sm-4">
                            End Date
                        </label>
                        <input name="endDate" runat="server" id="txtEndDate" class="form-control datepicker" />
                        <div class="error_msg label-control col-sm-6 oneSixity">
                            &nbsp;&nbsp;&nbsp;
                           <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtEndDate" ErrorMessage="Please select an end date"></asp:RequiredFieldValidator>
                            <asp:CustomValidator ID="valEndDate" runat="server" ErrorMessage="Your end must be after your start date" ControlToValidate="txtEndDate"></asp:CustomValidator>
                        </div>

                    </div>
                    <div class="col-sm-6 center">
                        <br />
                        <label class="label-control col-sm-4">Add Your Pet + </label>
                        <asp:DropDownList ID="ddlChoosePet" runat="server" CssClass="form-control" AutoPostBack="True" OnTextChanged="ddlChoosePet_TextChanged" >
                            <asp:ListItem> Select Your Pet</asp:ListItem>
                        </asp:DropDownList>
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


                        <div class="col-sm-6 center">
                        <label id="lblCurrentPet" runat="server" class="label-control col-sm-4">Current Pets</label>
                            <asp:ListBox ID="lbCurrentPets" runat="server" OnSelectedIndexChanged="lbCurrentPets_SelectedIndexChanged" AutoPostBack="true"></asp:ListBox>
                    </div>

                </div>
                <br />
                <h4 class="subtitle">Pet Services - <asp:Label ID="lblSelectedPet" runat="server" Text="[Pet's Name]"></asp:Label></h4>
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
                   
                    <div class="col-md-6">
                        <label class="label-control col-sm-4">Reservation Note</label>
                        <textarea id="txtResNote" runat="server" cols="20" rows="2" class=" form-control"></textarea>
                    </div>
                    <br />
                </div>
                <div class="pull-left col-md-4 buttons">
                    <asp:Button ID="btnBook" runat="server" Text="Book Now" CssClass="btn btn-primary" OnClick="btnBook_Click" />
                    <asp:LinkButton ID="btnClear" runat="server">Cancel</asp:LinkButton>

                </div>


            </div>
        </asp:Panel>
            </asp:Panel>
    </div>
    





</asp:Content>
