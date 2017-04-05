<%@ Page Title="Home" Language="C#" MasterPageFile="~/Application.Master" AutoEventWireup="true" CodeBehind="home.aspx.cs" Inherits="AYadollahibastani_C40A02.homePage" %>

<%@ Register Src="~/CalendarControl.ascx" TagPrefix="uc1" TagName="CalendarControl" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
    <div class="page_title">Home Page - Welcome </div>
    <div class="container">
        <div class="row">
            <asp:Panel ID="searchPanel" runat="server">
                <div class="form-inline search ">
                    <div class="form-group">
                    </div>
                    <div class="form-group ">
                        <input type="text" class="form-control" id="txtSearchPhone" placeholder="Phone Number " />
                        Or
    <input type="text" class="form-control" id="txtSearchEmail" placeholder="CustomerEmail" />
                        <asp:Button ID="btnSearch" CausesValidation="false" runat="server" Text="Search" CssClass="btn btn-primary" />
                        <br />

                    </div>

                </div>
            </asp:Panel>
        </div>
 
        <h3>Reservations<asp:Button OnClick="btnBookNow_Click" ID="btnBookNow" CssClass="loginNewOwner btn btn-success" Text="Book New Reservation" runat="server" /></h3>
        <asp:GridView OnRowCommand="gvReservations_RowCommand" CssClass="table table-striped" ID="gvReservations" runat="server" AutoGenerateColumns="false">
            <Columns>
                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:Button ID="btn1" CssClass="btn btn-default" Text="Select" runat="server" CausesValidation="false" CommandName="selectReservation" CommandArgument='<%# Eval("reservationId") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField HeaderText="Pet Names" DataField="PetNames" />
                <asp:BoundField HeaderText="Start Date" DataField="StartDate" />
                <asp:BoundField HeaderText="End Date" DataField="EndDate" />
                <asp:TemplateField>
                    <HeaderTemplate>
                        Vaccinations Valid
                    </HeaderTemplate>
                    <ItemTemplate>
                        <asp:CheckBox runat="server" Enabled="false" Checked='<%# Convert.ToBoolean(Eval("ValidVaccinations")) %>' /> 
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
            <EmptyDataTemplate>
                No Reservations Found
            </EmptyDataTemplate>
        </asp:GridView>

        <asp:GridView ID="gvCustomer" runat="server" CellPadding="4" ForeColor="#333333" GridLines="None">
            <AlternatingRowStyle BackColor="White" />
            <EditRowStyle BackColor="#7C6F57" />
            <FooterStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
            <HeaderStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
            <PagerStyle BackColor="#666666" ForeColor="White" HorizontalAlign="Center" />
            <RowStyle BackColor="#E3EAEB" />
            <SelectedRowStyle BackColor="#C5BBAF" Font-Bold="True" ForeColor="#333333" />
            <SortedAscendingCellStyle BackColor="#F8FAFA" />
            <SortedAscendingHeaderStyle BackColor="#246B61" />
            <SortedDescendingCellStyle BackColor="#D4DFE1" />
            <SortedDescendingHeaderStyle BackColor="#15524A" />
        </asp:GridView>
        

        
    </div>
    <div class="row ">
            <asp:Panel runat="server" ID="detailPanel">

        <div class="container whiteBackground">

            <asp:Panel ID="customerPanel" runat="server">
                <div class="clientDisplay">
                    <div class="col-sm-6">
                        <h4>Notifications</h4>
                        <br />
                        <br />
                        <%--                        Content Goes Here--%>
                        <br />
                        <br />
                    </div>
                    <div class="col-sm-6">
                        <h4>Kennel Log</h4>
                        <br />
                        <br />
                        <%--                        Content Goes Here--%>
                        <br />
                        <br />
                    </div>

                </div>
            </asp:Panel>
            <asp:Panel runat="server" ID="clerkPanel">
                <div class="form-group">
                    <h4 class="subtitle">Reservation Details</h4>
                    <div class="row">
                        <div class="col-sm-6">
                            <label class="label-control col-sm-4">
                                Start Date
                            </label>
                            <uc1:CalendarControl runat="server" id="UCstartDate" />
                            <div class="error_msg label-control col-sm-6 oneSixity">
                                &nbsp;&nbsp;&nbsp;
                            <asp:RequiredFieldValidator ID="valRequired" runat="server" ControlToValidate="UCstartDate$txtDate" Display="Dynamic" ErrorMessage="Please select a start date "></asp:RequiredFieldValidator>
                            </div>
                        </div>
                        <div class="col-sm-6">
                            <label class="label-control col-sm-4">
                                End Date
                            </label>
                            <uc1:CalendarControl runat="server" ID="UCendDate" Name="UCendDate"/>
                            <div class="error_msg label-control col-sm-6 oneSixity">
                                &nbsp;&nbsp;&nbsp;
                           <asp:RequiredFieldValidator ID="valRequired2" runat="server" ControlToValidate="UCendDate$txtDate" ErrorMessage="Please select an end date"></asp:RequiredFieldValidator>
                                <asp:CustomValidator ID="valEndDate" runat="server" ErrorMessage="Your end must be after your start date" ControlToValidate="UCendDate$txtDate"></asp:CustomValidator>
                            </div>

                        </div>
                        <div class="col-sm-6 center">
                            <br />

                            <br />
                            <label id="lblChooseRun" runat="server" class="label-control col-sm-4">Assigned Run</label>
                            <asp:DropDownList ID="ddlChooseRun" runat="server" CssClass="form-control short"></asp:DropDownList>


                            <div class="error_msg label-control col-sm-6 oneSixity">
                                &nbsp;&nbsp;&nbsp;
                           <asp:CustomValidator ID="valPetExists" runat="server" ErrorMessage="Pet is already on reservation list ! "></asp:CustomValidator>
                            </div>
                        </div>
                        <br />
                        <div class="col-sm-6 center">
                            <label id="lblCurrentPet" runat="server" class="label-control col-sm-4">Current Pets</label>
                            <asp:ListBox ID="lbCurrentPets" runat="server" AutoPostBack="true"></asp:ListBox>
                        </div>
                    </div>
                    <br />
                    <h4 class="subtitle">Pet Services -
                        <asp:Label ID="lblSelectedPet" runat="server" Text="[Pet's Name]"></asp:Label></h4>
                    <div class="row">
                        <div class="col-md-6 ">
                            <label class="label-control col-sm-4">Services</label>
                            <!-- To be recieve from DB -->
                            <div class="col-sm-8 center push-left">
                                <div class="col-sm-6">
                                    <asp:CheckBox ID="chWalk" runat="server" Text="Daily Walk" CssClass="" /><br />
                                    <asp:CheckBox ID="chPalytime" runat="server" Text="Daily Playtime" CssClass="" /><br />
                                    <br />
                                </div>
                            </div>
                        </div>
                        
                        
                        <div class="col-md-6">
                            <label class="label-control col-sm-4">Reservation Note</label>
                            <textarea id="txtResNote" runat="server" cols="20" rows="2" class=" form-control"></textarea>
                        </div>
                        <br />
                    </div>



                </div>
                

            </asp:Panel>
            </div>
                </asp:Panel>
        <br />
        <div class="container">
     <div class="col-sm-6">
                        <h4>Notifications</h4>
                        <br />
                        <br />
                        <%--                        Content Goes Here--%>
                        <br />
                        <br />
                    </div>
                    <div class="col-sm-6">
                        <h4>Kennel Log</h4>
                        <br />
                        <br />
                        <%--                        Content Goes Here--%>
                        <br />
                        <br />
                    </div>
</div>
        </div>
                
        


</asp:Content>
