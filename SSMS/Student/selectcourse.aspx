<%@ Page Language="C#" AutoEventWireup="true" CodeFile="selectcourse.aspx.cs" Inherits="selectcourse" StylesheetTheme="Blue" %>
<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title></title>
    <link href="../App_Themes/StyleSheet.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server" >
    <div>
        <table id="tablecenter">
            <tr>
                <td style="width: 498px; height: 40px; text-align: center">
                    <span class="auto-captionstyle">学生选修或取消课程</span>
                </td>
            </tr>
            <tr>
                <td style="width: 498px; height: 139px">
                    <asp:GridView ID="GridView1" runat="server" AllowPaging="True" AutoGenerateColumns="False" 
                        BackColor="LightGoldenrodYellow" BorderColor="Tan" BorderWidth="1px" CellPadding="2" Font-Bold="True" Font-Size="10pt"
                        ForeColor="Black" GridLines="None" Width="569px"  OnPageIndexChanging="GridView1_PageIndexChanging"  >
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
                            <asp:BoundField DataField="tname" HeaderText="任课教师">
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="sel" HeaderText="选修否" >
                                <ItemStyle ForeColor="#0000C0" HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:Button ID="Button2" runat="server" OnClick="Button2_Click" 
                                        Text="选修" style="font-size: 12px; color: #FF0000; font-weight: 700; 
                                        font-family: 黑体" CommandArgument='<%# DataBinder.Eval(Container.DataItem,"cno") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:Button ID="Button3" runat="server" OnClick="Button3_Click" 
                                        Text="取消" style="font-size: 12px; color: #FF0000; font-weight: 700; 
                                        font-family: 黑体" CommandArgument='<%# DataBinder.Eval(Container.DataItem,"cno") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                        <SelectedRowStyle BackColor="DarkSlateBlue" ForeColor="GhostWhite" />
                        <PagerStyle BackColor="PaleGoldenrod" ForeColor="DarkSlateBlue" HorizontalAlign="Center" />
                        <HeaderStyle BackColor="Tan" Font-Bold="True" />
                        <AlternatingRowStyle BackColor="PaleGoldenrod" />
                    </asp:GridView>
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td style="text-align:center; width: 498px; height: 33px">
                    <asp:Button ID="Button1" runat="server" Text="提交选修课程"  OnClick="Button1_Click" />
                </td>
            </tr>
        </table>
    
    </div>       
    </form>
</body>
</html>
