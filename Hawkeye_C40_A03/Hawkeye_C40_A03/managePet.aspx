<%@ Page Title="Manage Pet" Language="C#" MasterPageFile="~/Application.Master" AutoEventWireup="true" CodeBehind="managePet.aspx.cs" Inherits="AYadollahibastani_C40A02.managePet" %>

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
                            <label class="label-control col-sm-2">Food Preference</label>
                            <asp:DropDownList CssClass="form-control medium" ID="ddlFood" runat="server"></asp:DropDownList>
                        </div>
                    </div>
                    <div class="form-group">
                        <div class="col-sm-12">
                            <label class="label-control col-sm-2">
                                Pet Size
                            </label>

                            <asp:RadioButtonList ID="rdlPetSize" CssClass="btnRadio" runat="server">
                                <asp:ListItem>Small</asp:ListItem>
                                <asp:ListItem>Medium</asp:ListItem>
                                <asp:ListItem>Large</asp:ListItem>
                            </asp:RadioButtonList>
                            <div class="error_msg label-control col-sm-6">
                                <asp:RequiredFieldValidator ID="valReqPetSize" runat="server" ControlToValidate="rdlPetSize" Display="Dynamic" ErrorMessage="Please select your pet's size"></asp:RequiredFieldValidator>
                            </div>
                        </div>
                    </div>
                    <div class="form-group">
                        <div class="col-sm-12">
                            <label class="label-control col-sm-2">Vet</label>
                            <asp:DropDownList CssClass="form-control medium" ID="ddlVet" runat="server"></asp:DropDownList>
                        </div>
                    </div>

                    <div class="form-group">
                        <div class="col-sm-12">
                            <label class="label-control col-sm-2">Choose Your Vaccination</label>
                            <asp:DropDownList  AutoPostBack="true" CssClass="form-control medium" ID="ddlVacc" runat="server" OnSelectedIndexChanged="ddlVacc_SelectedIndexChanged">
                                <asp:ListItem>Vac 1</asp:ListItem>
                                <asp:ListItem>Vac 2</asp:ListItem>
                                <asp:ListItem>Vac 3</asp:ListItem>
                                <asp:ListItem>Vac 4</asp:ListItem>

                            </asp:DropDownList>
                        </div>
                        <div class="col-sm-12" style="font-family: sans-serif">

                            <label class="label-control col-sm-2">Expiry Date</label>
                            <input name="endDate" class="form-control short datepicker" runat="server" id="txtExpiry" />
                        </div>
                        <div class="error_msg col-sm-6 label-control block">
                            <asp:CustomValidator ID="valCheckDate" runat="server" ControlToValidate="txtExpiry" ErrorMessage="Please eneter a valid date"></asp:CustomValidator>
                            <asp:CustomValidator ID="valVacDate" runat="server" ControlToValidate="txtExpiry" ErrorMessage="Please enter your expiry date"></asp:CustomValidator>
                        </div>
                        <div class="col-sm-12" style="font-family: sans-serif">

                            <label class="label-control col-sm-2">Current Vaccines</label>
                            <asp:ListBox ID="lbCurrentVacc" runat="server"></asp:ListBox>
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
            <div class="listOfAllPets">
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
            </div>
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
