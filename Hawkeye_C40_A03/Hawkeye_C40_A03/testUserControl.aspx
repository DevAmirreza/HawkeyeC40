<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="testUserControl.aspx.cs" Inherits="AYadollahibastani_C40A02.testUserControl" %>

<%@ Register Src="~/CalendarControl.ascx" TagPrefix="uc1" TagName="CalendarControl" %>


<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <uc1:CalendarControl runat="server" ID="CalendarControl" />
        <asp:Label ID="lbl" runat="server" Text="Label"></asp:Label>
    </div>
    </form>
</body>
</html>
