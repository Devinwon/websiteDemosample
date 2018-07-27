<%@ Page Language="C#" AutoEventWireup="true" CodeFile="plancourse.aspx.cs" Inherits="plancourse" StylesheetTheme="Blue" %>
<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title></title>
    <link href="../App_Themes/StyleSheet.css" rel="stylesheet" />
    <style type="text/css">
        .auto-style1 {
            height: 40px;
            width: 497px;
        }
        .auto-style2 {
            height: 155px;
            width: 497px;
        }
        .auto-style3 {
            height: 53px;
            width: 497px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table id="tablecenter">
            <tr>
                  <td style="text-align: center; " class="auto-style1">
                    <span class="auto-captionstyle">安排上课教师</span>
                </td>       
            </tr>
            <tr>
                <td class="auto-style2">
                    <asp:GridView ID="GridView1" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                        BackColor="LightGoldenrodYellow" BorderColor="Tan" BorderWidth="1px" CellPadding="2"
                        Font-Bold="True" Font-Size="10pt" ForeColor="Black" GridLines="None" OnPageIndexChanging="GridView1_PageIndexChanging"
                        OnRowEditing="GridView1_RowEditing" Width="493px">
                        <FooterStyle BackColor="Tan" />
                        <Columns>
                            <asp:BoundField DataField="cno" HeaderText="课程号">
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="cname" HeaderText="课程名">
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="ctime" HeaderText="上课学期">
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="cplace" HeaderText="上课地点">
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="tno" HeaderText="上课教师编号">
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="tname" HeaderText="上课教师">
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:CommandField HeaderText="操作" ShowEditButton="True" EditText="安排/更改教师">
                                <ItemStyle ForeColor="Blue" HorizontalAlign="Center" />
                            </asp:CommandField>
                        </Columns>
                        <SelectedRowStyle BackColor="DarkSlateBlue" ForeColor="GhostWhite" />
                        <PagerStyle BackColor="PaleGoldenrod" ForeColor="DarkSlateBlue" HorizontalAlign="Center" />
                        <HeaderStyle BackColor="Tan" Font-Bold="True" />
                        <AlternatingRowStyle BackColor="PaleGoldenrod" />
                    </asp:GridView>
                </td>
            </tr>
            <tr>
                 <td style="text-align:center" class="auto-style3">      
                    <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="退出课程安排" />
                 </td>
            </tr>
        </table>
    
    </div>
    </form>
</body>
</html>
