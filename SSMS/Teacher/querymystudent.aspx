<%@ Page Language="C#" AutoEventWireup="true" CodeFile="querymystudent.aspx.cs" Inherits="querymystudent" StylesheetTheme="Blue" %>
<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title></title>
    <link href="../App_Themes/StyleSheet.css" rel="stylesheet" />    
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table id="tablecenter" style="width:440px">
            <tr>
                <td class="auto-captionstyle" style="width:100%">列我的上课学生</td>
            </tr>
            <tr>
                <td style="text-align:center">
                    <strong class="auto-stringstyle">课程: </strong>
                    <asp:DropDownList ID="DropDownList1" runat="server" >
                    </asp:DropDownList>&nbsp;
                    <asp:Button ID="Button1" runat="server" Text="确定" Font-Size="10pt" OnClick="Button1_Click" />
                </td>
            </tr>
            <tr>
                <td>
                    <asp:GridView ID="GridView1" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                        BackColor="LightGoldenrodYellow" BorderColor="Tan" BorderWidth="1px" CellPadding="2"
                        Font-Bold="True" Font-Size="10pt" ForeColor="Black" GridLines="None" Height="5px"
                        OnPageIndexChanging="GridView1_PageIndexChanging" Width="450px" PageSize="20">
                        <FooterStyle BackColor="Tan" />
                        <Columns>
                            <asp:BoundField DataField="sno" HeaderText="学号">
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="sname" HeaderText="姓名">
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="cno" HeaderText="课程号">
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="cname" HeaderText="课程名">
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                        </Columns>
                        <SelectedRowStyle BackColor="DarkSlateBlue" ForeColor="GhostWhite" />
                        <PagerStyle BackColor="PaleGoldenrod" ForeColor="DarkSlateBlue" HorizontalAlign="Center" />
                        <HeaderStyle BackColor="Tan" Font-Bold="True" />
                        <AlternatingRowStyle BackColor="PaleGoldenrod" />
                    </asp:GridView>
                </td>
            </tr>
        </table>
    
    </div>
    </form>
</body>
</html>
