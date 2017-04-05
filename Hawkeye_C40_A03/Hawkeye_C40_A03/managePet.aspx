<%@ Page Title="Manage Pet" Language="C#" MasterPageFile="~/Application.Master" AutoEventWireup="true" CodeBehind="managePet.aspx.cs" Inherits="AYadollahibastani_C40A02.managePet" %>
<%@ Register Src="~/CalendarControl.ascx" TagPrefix="uc1" TagName="CalendarControl" %>
<%@ Register Src="~/listPets.ascx" TagPrefix="uc1" TagName="listPets" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">

    <div class="page_title">
        <p>
            Manage Pet <span>
                <asp:Label ID="lblDisplay" runat="server" Text=""></asp:Label></span>
        </p>
    </div>
    <div class="container">
        <div class="message">
            <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
        </div>
        <asp:Button CssClass="btn btn-default" ID="btnEdit" Text="Edit" OnClick="btnEdit_Click" runat="server" CausesValidation="False" UseSubmitBehavior="False" />

        <div class="editDisplay" id="editDisplay" runat="Server">
            <asp:Panel runat="server" ID="editPanel">
                <div class="col-md-6  item2">

                    <div class="form-group">
                        <div class="col-sm-12">
                            <label class="label-control col-sm-4 txtArea">Special Notes</label>
                            <textarea class="txtArea" runat="server" id="txtSpecialNote" cols="50" rows="2"></textarea>

                        </div>
                    </div>

                       <div class="form-group">
                        <div class="col-sm-12">
                            <label class="label-control col-sm-4">Choose Your Vaccination</label>
                            <asp:DropDownList CssClass="form-control medium" ID="ddlVacc" runat="server" DataSourceID="odsVaccinesNotHad" DataTextField="name" DataValueField="vaccinationNumber">

                            </asp:DropDownList>
                            <asp:Button ID="btnAddVaccine" CssClass="btn btn-primary" runat="server" CausesValidation="False" OnClick="btnAddVaccine_Click" Text="Add Vaccine" />
                        </div>
                        <div class="col-sm-12" style="font-family: sans-serif">

                            <label class="label-control col-sm-2">Expiry Date</label>
                            
                            <uc1:CalendarControl runat="server" ID="UCexpDate" />
                            
                        </div>
                        <div class="error_msg col-sm-6 label-control block">
                            <asp:CustomValidator ID="valCheckDate" runat="server" ControlToValidate="UCexpDate$txtDate" ErrorMessage="Please enter a valid date"></asp:CustomValidator>
                            <asp:CustomValidator ID="valVacDate" runat="server" ControlToValidate="UCexpDate$txtDate" ErrorMessage="Please enter your expiry date"></asp:CustomValidator>
                        </div>

                       


                    </div>
                     <div class="col-sm-12" style="font-family: sans-serif">



                            <asp:ObjectDataSource ID="odsPetVaccinations" runat="server" SelectMethod="listVaccinations" TypeName="HawkeyehvkBLL.PetVaccination" OldValuesParameterFormatString="original_{0}">
                                <SelectParameters>
                                    <asp:SessionParameter Name="petNum" SessionField="PetID" Type="Int32" />
                                </SelectParameters>
                            </asp:ObjectDataSource>
                            <label class="label-control col-sm-2">Current Vaccines</label>
                            <asp:GridView ID="gvPetVaccination" CssClass="table table-responsive table-hover table-striped" runat="server" AutoGenerateColumns="False" DataSourceID="odsPetVaccinations">
                                <Columns>
                                    <asp:TemplateField ShowHeader="False">
                                        <EditItemTemplate>
                                            <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="false" CommandName="Cancel" Text="Update"></asp:LinkButton>
                                            &nbsp<asp:LinkButton ID="LinkButton2" runat="server" CausesValidation="False" CommandName="Cancel" Text="Cancel"></asp:LinkButton>
                                        </EditItemTemplate>
                                        <ItemTemplate>
                                            <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="False" CommandName="Edit" Text="Edit"></asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField ConvertEmptyStringToNull="False" HeaderText="Vaccine">
                                        <ItemTemplate>
                                            <%# DataBinder.Eval(Container.DataItem, "vaccination.name")  %>
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:TextBox ID="txtVacName" runat="server" Text=''></asp:TextBox>
                                        </EditItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Expiration Date" SortExpression="expirationDate">
                                        <EditItemTemplate>
                                            <asp:Calendar ID="Calendar1" runat="server" SelectedDate='<%# Bind("expirationDate") %>'></asp:Calendar>
                                        </EditItemTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="Label1" runat="server" Text='<%# Bind("expirationDate", "{0:MMM dd, yyyy}") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Validated?">
                                        <EditItemTemplate>
                                            <asp:DropDownList ID="DropDownList1" runat="server" SelectedValue='<%# Bind("isValidated") %>'>
                                                <asp:ListItem Value="Y">Valid</asp:ListItem>
                                                <asp:ListItem Value="N">Invalid</asp:ListItem>
                                            </asp:DropDownList>
                                        </EditItemTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="Label2" runat="server" Text='<%# (Eval("isValidated").ToString() == "N" ? "Invalid" : "Valid") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </div>


                </div>
                <div class="col-md-6 item1">
                    <div class="form-group">
                        <div class="col-sm-12">
                            <label class="label-control col-sm-2 ">
                                Pet Name
                            </label>
                            <asp:TextBox CssClass="form-control" ID="txtPetName" runat="server" MaxLength="25"></asp:TextBox>
                            <div class="label-control col-sm-6 error_msg">
                                <asp:RequiredFieldValidator ID="valPetName" runat="server" ControlToValidate="txtPetName" Display="Dynamic" ErrorMessage="Please enter your pet name"></asp:RequiredFieldValidator>
                            </div>
                        </div>
                    </div>
                    <div class="form-group">
                        <div class="col-sm-12">
                            <label class="label-control col-sm-2">
                                Breed
                            </label>
                            <asp:TextBox CssClass="form-control medium" ID="txtBreed" runat="server" MaxLength="50"></asp:TextBox>
                        </div>
                        <div class="error_msg label-control col-sm-6">
                            &nbsp;&nbsp;&nbsp;     
                           <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="txtBreed" ErrorMessage="Please enter a valid breed" ValidationExpression="^[a-zA-Z'.\s]{1,40}$"></asp:RegularExpressionValidator>
                        </div>
                    </div>

                    <div class="form-group">
                        <div class="col-sm-12">
                            <label class="label-control col-sm-2">Gender</label>
                            <asp:RadioButtonList ID="rdGender" CssClass="btnRadio" runat="server">
                                <asp:ListItem Value="M">Male</asp:ListItem>
                                <asp:ListItem Value="F">Female</asp:ListItem>

                            </asp:RadioButtonList>
                        </div>

                        <div class="error_msg label-control col-sm-6">
                            &nbsp;&nbsp;&nbsp;
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Please select a gender" ControlToValidate="rdGender"></asp:RequiredFieldValidator>

                        </div>


                    </div>
                    <div class="form-group">
                        <div class="col-sm-12">
                            <label class="label-control col-sm-2">
                                Pet Size
                            </label>

                            <asp:RadioButtonList ID="rdlPetSize" CssClass="btnRadio" runat="server">
                                <asp:ListItem Value="Small">Small</asp:ListItem>
                                <asp:ListItem Value="Medium">Medium</asp:ListItem>
                                <asp:ListItem>Large</asp:ListItem>
                            </asp:RadioButtonList>
                            <div class="error_msg label-control col-sm-6">
                                <asp:RequiredFieldValidator ID="valReqPetSize" runat="server" ControlToValidate="rdlPetSize" Display="Dynamic" ErrorMessage="Please select your pet's size"></asp:RequiredFieldValidator>
                            </div>
                        </div>
                    </div>
                    <asp:SqlDataSource ID="dsVaccine" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>" ProviderName="<%$ ConnectionStrings:ConnectionString.ProviderName %>" SelectCommand="SELECT &quot;VACCINATION_NUMBER&quot;, &quot;VACCINATION_NAME&quot; FROM &quot;HVK_VACCINATION&quot;"></asp:SqlDataSource>
                    <asp:ObjectDataSource ID="odsVaccinesNotHad" runat="server" SelectMethod="listNonPetVaccinations" TypeName="HawkeyehvkBLL.Vaccination">
                        <SelectParameters>
                            <asp:SessionParameter Name="petNum" SessionField="PetID" Type="Int32" />
                        </SelectParameters>
                    </asp:ObjectDataSource>
                 
                </div>

                <!--Buttons-->
                <div class="form-group">
                    <div class="col-sm-offset-2 col-sm-12">
                        <asp:Button CssClass="btn btn-primary pet-save" ID="btnSave" runat="server" Text="Save" OnClick="btnSave_Click1" />
                    </div>
                </div>

            </asp:Panel>


            <!--
                    It requires researh on how to  add a div programmatically 
                    to the page - to be implemented later 
                    -->
            <!--Pet List gets repeated for list of all pets -->
            <h2>List Of All Pets</h2>
         
            <uc1:listPets runat="server" ID="listPets" />
            <br />
            <asp:Button CausesValidation="false" CssClass="btn btn-default" ID="btnAdd" Text="Add A New Pet + " OnClick="btnAdd_Click" runat="server" />

        </div>
        <!--To be implemented in future -->
        <div class="addDisplay" id="addDisplay" runat="Server">
        </div>

        <!--To be implemented in future -->
        <div class="viewDisplay" id="viewDisplay" runat="Server">
        </div>


    </div>



</asp:Content>
