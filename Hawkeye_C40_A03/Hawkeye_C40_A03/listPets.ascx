<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="listPets.ascx.cs" Inherits="AYadollahibastani_C40A02.listPets" %>
       <div class="listOfAllPets">
    <%--            <div class="petList">
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
                </div>--%>


      

            <asp:GridView CssClass="table table-striped petTable" ID="gvPetList" runat="server" OnRowCommand="gvPetList_RowCommand1" AutoGenerateColumns="False" Height="300px" Width="600px"  >
              <Columns>
                  <asp:TemplateField HeaderText="Pet Name">
                      <EditItemTemplate>
                          <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("name") %>'></asp:TextBox>
                      </EditItemTemplate>
                      <ItemTemplate>
                          <asp:Label ID="Label1" runat="server" Text='<%# Bind("name") %>'></asp:Label>
                      </ItemTemplate>
                  </asp:TemplateField>
                  <asp:TemplateField HeaderText="Gender">
                      <EditItemTemplate>
                          <asp:TextBox ID="TextBox2" runat="server" Text='<%# Bind("gender") %>'></asp:TextBox>
                      </EditItemTemplate>
                      <ItemTemplate>
                          <asp:Label ID="Label2" runat="server" Text='<%# Bind("gender") %>'></asp:Label>
                      </ItemTemplate>
                  </asp:TemplateField>
                  <asp:TemplateField HeaderText="Birthday">
                      <EditItemTemplate>
                          <asp:TextBox ID="TextBox3" runat="server" Text='<%# Bind("birthday") %>'></asp:TextBox>
                      </EditItemTemplate>
                      <ItemTemplate>
                          <asp:Label ID="Label3" runat="server" Text='<%# Bind("birthday") %>'></asp:Label>
                      </ItemTemplate>
                  </asp:TemplateField>
                  <asp:TemplateField>
                      <ItemTemplate>
                        <img src="images/profile.jpg" class="petProfileImage" />
                      </ItemTemplate>
                  </asp:TemplateField>
                             <asp:TemplateField>
                      <ItemTemplate>
                          <asp:Button  CommandArgument='<%# Eval("petNumber") %>' runat="server" Text="Edit" CssClass="btn btn-default"  CausesValidation="true"    />
                      </ItemTemplate>   
                  </asp:TemplateField>
              </Columns>
           </asp:GridView>






            </div>