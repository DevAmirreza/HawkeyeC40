<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="listPets.ascx.cs" Inherits="AYadollahibastani_C40A02.listPets" %>
       <div class="listOfAllPets">
  

            <asp:GridView CssClass="table table-striped petTable" ID="gvPetList" runat="server" OnRowCommand="gvPetList_RowCommand1" AutoGenerateColumns="False"   >
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