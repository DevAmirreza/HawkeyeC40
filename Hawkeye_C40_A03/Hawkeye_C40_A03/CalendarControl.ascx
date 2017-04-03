<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CalendarControl.ascx.cs" Inherits="AYadollahibastani_C40A02.CalendarControl" %>
<div class="calendarFixed"><asp:TextBox  name="txtDate" runat="server" id="txtDate" class="form-control datepickerUC" />
<asp:ImageButton ID="btnDate" runat="server" Height="25px" Width="25px" OnClick="btnStartDate_Click" ImageUrl="~/images/Calendar.png" CssClass="calButtons" CausesValidation="False" ImageAlign="AbsMiddle" />

<asp:Calendar ID="calControl" runat="server" OnSelectionChanged="calControl_SelectionChanged" BackColor="transparent" BorderColor="transparent" BorderWidth="1px" Font-Names="Verdana" Font-Size="9pt" ForeColor="Black" Height="190px" NextPrevFormat="FullMonth" Width="232px" OnVisibleMonthChanged="calControl_VisibleMonthChanged">
                <DayHeaderStyle Font-Bold="True" Font-Size="8pt" />
                <NextPrevStyle Font-Bold="True" Font-Size="8pt" ForeColor="#9b7874" VerticalAlign="Bottom" />
                <OtherMonthDayStyle ForeColor="#9b7874" />
                <SelectedDayStyle BackColor="#9b7874" ForeColor="black" />
                <TitleStyle BackColor="transparent" BorderColor="Black" BorderWidth="4px" Font-Bold="True" Font-Size="12pt" ForeColor="#9b7874" />
                <TodayDayStyle BackColor="#CCCCCC" />
            </asp:Calendar></div>