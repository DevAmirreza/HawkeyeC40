<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="listPets.ascx.cs" Inherits="AYadollahibastani_C40A02.listPets" %>
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