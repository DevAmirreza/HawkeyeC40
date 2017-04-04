<%@ Page Title="ManageCustomer" Language="C#" MasterPageFile="~/Application.Master" AutoEventWireup="true" CodeBehind="ManageCustomer.aspx.cs" Inherits="AYadollahibastani_C40A02.ManageCustomer" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
    <div class="page_title">
        <p>Manage Customer</p>
    </div>


<%--    
    Work in progress / will be added at the end
    <asp:Wizard ID="wzRegistery" runat="server">
        <WizardSteps>
            <asp:WizardStep ID="Personalnformation" runat="server" Title="Personal Information"></asp:WizardStep>
            <asp:WizardStep ID="PersonalContact" runat="server" Title="Personal Contact"></asp:WizardStep>
            <asp:WizardStep ID="EmergencyContact" runat="server" Title="Emergency Contact"></asp:WizardStep>
        </WizardSteps>
    </asp:Wizard>
    <br />
    <br />
    <br />--%>
    <div class="container">
        <div class="message">
            <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
        </div>
        <div class="centerit">
            <asp:Button CssClass="btn btnEdit btn-danger" ID="btnEdit" runat="server" Text="Edit" OnClick="btnEdit_Click" CausesValidation="False" />
            <asp:Button CssClass="btn btnEdit btn-danger" ID="btnAdd" runat="server" Text="Add New Customer" OnClick="btnAdd_Click" CausesValidation="False" />
            &nbsp;&nbsp;&nbsp;
            <br />
            <%--    <div class="error_box">
                <asp:ValidationSummary ID="valSumary" runat="server" CssClass="error_style" />
            </div>--%>
            <asp:Panel runat="server" ID="formPanel">
                <h4 class="subtitle">Personal Information</h4>
                <div class="form-group">
                    <div class="col-sm-12">
                        <label class="label-control col-sm-2">
                            First Name
                        </label>
                        <asp:TextBox placeholder="Your first name" CssClass="form-control fname" ID="txtfName" runat="server" MaxLength="25"></asp:TextBox>
                        <div class="error_msg label-control col-sm-6">
                            &nbsp;&nbsp;&nbsp;
                            <asp:RequiredFieldValidator ID="valFname" runat="server" ControlToValidate="txtfName" Display="Dynamic" ErrorMessage="Please enter your first name"></asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtfName" Display="Dynamic" ErrorMessage="Please enter a valid first name" ValidationExpression="^[a-zA-Z'.\s]{1,40}$"></asp:RegularExpressionValidator>
                        </div>
                    </div>
                </div>
                <div class="form-group">
                    <div class="col-sm-12">
                        <label class="label-control col-sm-2">
                            Last Name 
                           
                        </label>
                        <asp:TextBox placeholder="Your last name" CssClass="form-control lname" ID="txtlName" runat="server" MaxLength="25"></asp:TextBox>
                        <div class="error_msg label-control col-sm-6">
                            &nbsp;&nbsp;&nbsp;
                            <asp:RequiredFieldValidator ID="valLastName" runat="server" ControlToValidate="txtlName" Display="Dynamic" ErrorMessage="Please enter your last name "></asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="txtlName" ErrorMessage="Please enter a valid last name" ValidationExpression="^[a-zA-Z'.\s]{1,40}$"></asp:RegularExpressionValidator>
                        </div>
                    </div>
                </div>
                <div class="form-group">
                    <div class="col-sm-12">
                        <label class="label-control col-sm-2">
                            Email 
                        </label>
                        <asp:TextBox placeholder="Your email address" CssClass="form-control" ID="txtEmail" runat="server"></asp:TextBox>
                        <div class="error_msg  label-control col-sm-6">
                            &nbsp;&nbsp;&nbsp;
                            <%--                            to be fixed
                            <asp:RegularExpressionValidator ID="valEmail" runat="server" ControlToValidate="txtEmail" Display="Dynamic" ErrorMessage="Please enter a valid email address" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator>--%>
                            <asp:RequiredFieldValidator ID="valReqEmail" ControlToValidate="txtEmail" runat="server" ErrorMessage="You must enter your email address"></asp:RequiredFieldValidator>
                        </div>
                    </div>
                </div>
                <h4 class="subtitle">Personal Contact </h4>
                <div class="form-group">
                    <div class="col-sm-12">
                        <label class="label-control col-sm-2">
                            Home Phone Number
                        </label>
                        <asp:TextBox placeholder="Phone Number" CssClass="form-control phone" ID="txtHomePhone" runat="server"></asp:TextBox>
                        <div class="error_msg label-control col-sm-6 block ">
                            <asp:RequiredFieldValidator ID="valHomePhone" runat="server" ControlToValidate="txtHomePhone" Display="static" ErrorMessage="You must enter your home phone number"></asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ControlToValidate="txtHomePhone" Display="Static" ErrorMessage="Please enter a valid phone number" ValidationExpression="^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$"></asp:RegularExpressionValidator>
                        </div>
                    </div>
                </div>
                <!--Address-->
                <div class="form-group">
                    <div class="col-sm-12">
                        <label class="label-control col-sm-2">
                            Address
                        </label>
                        <asp:TextBox placeholder="Your residential address" CssClass="form-control" ID="txtaddress" runat="server" MaxLength="40"></asp:TextBox>
                        <div class="error_msg label-control col-sm-6">
                            &nbsp;&nbsp;&nbsp;
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtaddress" Display="Dynamic" ErrorMessage="Please enter your street address "></asp:RequiredFieldValidator>
                            <asp:CustomValidator ID="valAdreessInvalid" runat="server" ErrorMessage="Please enter a valid address"></asp:CustomValidator>
                        </div>
                    </div>
                </div>
                <div class="form-group">
                    <div class="col-sm-12">
                        <label class="label-control col-sm-2 ">
                            City
                        </label>
                        <asp:TextBox placeholder="Current City" CssClass="form-control city" ID="txtCity" runat="server" MaxLength="25"></asp:TextBox>
                        <div class="error_msg label-control col-sm-6">
                            &nbsp;&nbsp;&nbsp;
                            <asp:RequiredFieldValidator ID="valCity" runat="server" ControlToValidate="txtCity" Display="Dynamic" ErrorMessage="Please enter your city "></asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator6" runat="server" ControlToValidate="txtCity" ErrorMessage="Please enter a valid city" ValidationExpression="^[a-zA-Z'.\s]{1,40}$"></asp:RegularExpressionValidator>
                        </div>
                    </div>
                </div>
                <div class="form-group">
                    <div class="col-sm-12">
                        <label class="label-control col-sm-2">
                            Province 
                            <%--                            <asp:RequiredFieldValidator ID="valProvince" runat="server" ControlToValidate="DropDownProvince" Display="Dynamic" ErrorMessage="Please select your province">*</asp:RequiredFieldValidator>--%>
                        </label>
                        <asp:DropDownList placeholder="Your Province" CssClass="form-control prov" ID="DropDownProvince" runat="server">
                            <asp:ListItem>--Select Province--</asp:ListItem>
                            <asp:ListItem>Ontario</asp:ListItem>
                            <asp:ListItem>Quebec</asp:ListItem>
                        </asp:DropDownList>
                        <br />
                    </div>
                </div>
                <div class="form-group">
                    <div class="col-sm-12">
                        <label class="label-control col-sm-2">
                            Postal Code 
                        </label>
                        <asp:TextBox placeholder="Your Postal Code" CssClass="form-control postal" ID="txtPostal" runat="server"></asp:TextBox>
                        &nbsp;&nbsp;&nbsp;
                        <div class="error_msg label-control col-sm-6 ">
                            <asp:RequiredFieldValidator ID="valReqPostalCode" runat="server" ErrorMessage="Please enter your postal code" ControlToValidate="txtPostal"></asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="valPostalExpress" runat="server" ErrorMessage="Please enter a valid postal code" ControlToValidate="txtPostal" ValidationExpression="[ABCEGHJKLMNPRSTVXY][0-9][ABCEGHJKLMNPRSTVWXYZ] ?[0-9][ABCEGHJKLMNPRSTVWXYZ][0-9]"></asp:RegularExpressionValidator>
                        </div>
                    </div>
                </div>



                <h4 class="subtitle">Emergency Contact </h4>
                <div class="form-group">
                    <div class="col-sm-12">
                        <label class="label-control col-sm-2">Emergency First Name</label>
                        <asp:TextBox placeholder="Emergency Contact First Name" CssClass="form-control fname" ID="txtEmrgfName" runat="server"></asp:TextBox>
                    </div>
                </div>
                <div class="form-group">
                    <div class="col-sm-12">
                        <label class="label-control col-sm-2">Emergency Last Name </label>
                        <asp:TextBox placeholder="Emergency Contact Last Name" CssClass="form-control" ID="txtEmrglName" runat="server"></asp:TextBox>
                    </div>
                </div>
                <div class="form-group">
                    <div class="col-sm-12">
                        <label class="label-control col-sm-2">
                            Emergency Phone Number
                        </label>
                        <asp:TextBox placeholder="Phone Number" CssClass="form-control phone" ID="txtEmrgPhone" runat="server"></asp:TextBox>
                        <div class="error_msg label-control col-sm-6 block">
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator4" runat="server" ControlToValidate="txtEmrgPhone" ErrorMessage="Please enter a valid phone number" ValidationExpression="^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$"></asp:RegularExpressionValidator>
                            <asp:CustomValidator ID="valRequiredEmg" runat="server" ErrorMessage="Please enter your emergency phone number"></asp:CustomValidator>
                        </div>
                    </div>
                </div>

                <!--Buttons-->
                <div class="form-group">
                    <div class="col-sm-offset-2 col-sm-12">
                        <asp:Button CssClass="btn btn-primary" ID="btnSave" runat="server" Text="Save" OnClick="btnSave_Click" />
                        <asp:LinkButton ID="lbtnClear" runat="server" OnClick="lbtnClear_Click" CausesValidation="false">Clear</asp:LinkButton>
                        <div class="right-wrap">
                            <asp:Button CssClass="btn btn-default" ID="btnPassdEdit" runat="server" Text="Edit My Password" />
                            

                        </div>

                    </div>
                </div>



            </asp:Panel>

        </div>
    </div>

</asp:Content>
<%--  --%>