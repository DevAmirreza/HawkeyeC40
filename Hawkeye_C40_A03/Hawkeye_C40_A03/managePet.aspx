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
                <div class="col-md-4 item2">
                    <div id="petProfileImage" class="petProfileImage" style="background-image: url('images/profile.jpg')"></div>
                    <asp:HyperLink ID="linkUpload" runat="server">Upload a new picture</asp:HyperLink>

                    <div class="form-group">
                        <div class="col-sm-12">
                            <label class="label-control col-sm-4 txtArea">Special Note</label>
                            <textarea class="txtArea" runat="server" id="txtSpecialNote" cols="50" rows="2"></textarea>

                        </div>
                    </div>
                </div>
                <div class="col-md-8 item1">
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
                    <div class="form-group">
                        <div class="col-sm-12">
                            <label class="label-control col-sm-2">Choose Your Vaccination</label>
                            <asp:DropDownList CssClass="form-control medium" ID="ddlVacc" runat="server" DataSourceID="dsVaccine" DataTextField="VACCINATION_NAME" DataValueField="VACCINATION_NUMBER">

                            </asp:DropDownList>
                            <asp:Button ID="btnAddVaccine" runat="server" CausesValidation="False" OnClick="btnAddVaccine_Click" Text="Add Vaccine" />
                        </div>
                        <div class="col-sm-12" style="font-family: sans-serif">

                            <label class="label-control col-sm-2">Expiry Date</label>
                            
                            <uc1:CalendarControl runat="server" ID="UCexpDate" />
                            
                        </div>
                        <div class="error_msg col-sm-6 label-control block">
                            <asp:CustomValidator ID="valCheckDate" runat="server" ControlToValidate="UCexpDate$txtDate" ErrorMessage="Please enter a valid date"></asp:CustomValidator>
                            <asp:CustomValidator ID="valVacDate" runat="server" ControlToValidate="UCexpDate$txtDate" ErrorMessage="Please enter your expiry date"></asp:CustomValidator>
                        </div>
                        <div class="col-sm-12" style="font-family: sans-serif">
                            <asp:ObjectDataSource ID="odsPetVaccinations" runat="server" SelectMethod="listVaccinations" TypeName="HawkeyehvkBLL.PetVaccination" OldValuesParameterFormatString="original_{0}" UpdateMethod="updatePetVaccinationExpiry">
                                <SelectParameters>
                                    <asp:SessionParameter Name="petNum" SessionField="PetId" Type="Int32" />
                                </SelectParameters>
                                <UpdateParameters>
                                    <asp:Parameter Name="expiryDate" Type="DateTime" />
                                    <asp:Parameter Name="vacNumber" Type="Int32" />
                                    <asp:Parameter Name="petNumber" Type="Int32" />
                                </UpdateParameters>
                            </asp:ObjectDataSource>
                            <label class="label-control col-sm-2">Current Vaccines</label>
                            <asp:ListBox ID="lbCurrentVacc" runat="server" OnSelectedIndexChanged="lbCurrentVacc_SelectedIndexChanged" SelectionMode="Multiple" AutoPostBack="True">
                                <asp:ListItem></asp:ListItem>
                            </asp:ListBox>
                            <asp:GridView ID="gvPetVaccination" runat="server" AutoGenerateColumns="False" DataSourceID="odsPetVaccinations">
                                <Columns>
                                    <asp:TemplateField ConvertEmptyStringToNull="False" HeaderText="Vaccine Id">
                                        <ItemTemplate>
                                            <%# DataBinder.Eval(Container.DataItem, "vaccination.vaccinationNumber") %>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField ConvertEmptyStringToNull="False" HeaderText="Vaccine">
                                        <ItemTemplate>
                                            <%# DataBinder.Eval(Container.DataItem, "vaccination.name")  %>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="expirationDate" HeaderText="expirationDate" SortExpression="expirationDate" />
                                    <asp:BoundField DataField="isValidated" HeaderText="isValidated" ReadOnly="True" SortExpression="isValidated" />
                                </Columns>
                            </asp:GridView>
                        </div>


                    </div>
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
            <div class="listOfAllPets" id="listOfPets">
                <div class="petList">
                    <div class="col-md-8  ">
                        <div class="form-group">
                            <div class="col-sm-12" style="font-family: sans-serif">
                                <label class="label-control col-sm-2">Pet Name : </label>
                                <asp:Label ID="lblPetName" runat="server" CssClass="label-control col-sm-2" Text="n/a"></asp:Label>
                            </div>

                            <asp:Button CssClass="btn btn-danger btnEditPet" ID="btnPetListEdit" Text="Edit" runat="server" />
                        </div>
                    </div>
                    <div class="col-md-4 ">
                        <div id="" class="petProfileImage" style="background-image: url('images/profile.jpg')"></div>
                    </div>


                </div>

                <!--Second Pet-->
                <div class="petList">
                    <div class="col-md-8  ">
                        <div class="form-group">
                            <div class="col-sm-12" style="font-family: sans-serif">
                                <label class="label-control col-sm-2">Pet Name : </label>
                                <asp:Label ID="Label1" runat="server" CssClass="label-control col-sm-2" Text="n/a"></asp:Label>
                            </div>

                            <asp:Button CssClass="btn btn-danger btnEditPet" ID="Button2" Text="Edit" runat="server" />
                        </div>
                    </div>
                    <div class="col-md-4 ">
                        <div id="" class="petProfileImage" style="background-image: url('images/profile.jpg')"></div>
                    </div>
                </div>

                <!-- Third Pet -->
                <div class="petList">
                    <div class="col-md-8  ">
                        <div class="form-group">
                            <div class="col-sm-12" style="font-family: sans-serif">
                                <label class="label-control col-sm-2">Petpiry Name : </label>
                                <asp:Label ID="Label2" runat="server" CssClass="label-control col-sm-2" Text="n/a"></asp:Label>
                            </div>
                            <asp:Button CssClass="btn btn-danger btnEditPet" ID="Button1" Text="Edit" runat="server" />
                        </div>
                    </div>
                    <div class="col-md-4 ">
                        <div id="" class="petProfileImage" style="background-image: url('images/profile.jpg')"></div>
                    </div>
                </div>
            </div>--%>

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
