<%@ Page Language="C#" AutoEventWireup="true" CodeFile="listcourse.aspx.cs" Inherits="listcourse" %>
<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>无标题页</title>
    <link href="../App_Themes/StyleSheet.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server" >
    <div>
        <table id="tablecenter" border="0px">
            <tr>
                <td style="height: 40px; text-align: center"> 
                    <span class="auto-captionstyle" style="color:Black">我的选课</span>
                </td>
            </tr>
            <tr>
                <td style="width: 100%; height: 15px;">
                    <asp:GridView ID="GridView1" runat="server" BackColor="LightGoldenrodYellow" BorderColor="Tan"
                        BorderWidth="1px" CellPadding="2" Font-Bold="True" Font-Size="10pt" ForeColor="Black"
                        GridLines="None" OnPageIndexChanging="GridView1_PageIndexChanging" Width="492px" AllowPaging="True" AutoGenerateColumns="False" Height="1px">
                        <FooterStyle BackColor="Tan" />
                        <Columns>
                            <asp:BoundField DataField="sno" HeaderText="学号" >
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="sname" HeaderText="姓名" >
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="cno" HeaderText="课程号" >
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="cname" HeaderText="课程名" >
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="tno" HeaderText="上课教师号" >
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
