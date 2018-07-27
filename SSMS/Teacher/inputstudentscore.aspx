<%@ Page Language="C#" AutoEventWireup="true" CodeFile="inputstudentscore.aspx.cs" Inherits="inputstudentscore" 
    StylesheetTheme="Blue" %>
<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title></title>
    <link href="../App_Themes/StyleSheet.css" rel="stylesheet" />    
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table id="tablecenter"  style="width:440px">
            <tr>
                <td class="auto-captionstyle" style="width:100%" >输入我的学生成绩
                </td>
            </tr>
            <tr>
                <td class="auto-stringstyle" style="text-align:center">课程:
                    <asp:DropDownList ID="DropDownList1" runat="server">
                    </asp:DropDownList>&nbsp;
                    <asp:Button id="Button1" runat="server" Font-Size="10pt" OnClick="Button1_Click"
                        Text="确定" />

                </td>
            </tr>
            <tr>
                <td style="width: 456px; height: 23px">
                    <asp:GridView id="GridView1" runat="server" AutoGenerateColumns="False"
                        BackColor="LightGoldenrodYellow" BorderColor="Tan" BorderWidth="1px" CellPadding="0"
                        Font-Bold="True" Font-Size="10pt" ForeColor="Black" GridLines="None" Height="5px" Width="456px" >
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
                            <asp:TemplateField HeaderText="分数">
                                <EditItemTemplate>
                                    <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("degree") %>'></asp:TextBox>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:TextBox ID="TextBox1" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"degree") %>'
                                        Width="65px"></asp:TextBox>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                        </Columns>
                        <SelectedRowStyle BackColor="DarkSlateBlue" ForeColor="GhostWhite" />
                        <PagerStyle BackColor="PaleGoldenrod" ForeColor="DarkSlateBlue" HorizontalAlign="Center" />
                        <HeaderStyle BackColor="Tan" Font-Bold="True" />
                        <AlternatingRowStyle BackColor="PaleGoldenrod" />
                    </asp:GridView>
                    </td>
                </tr>
                <tr>
                    <td style="text-align:center">
                     <asp:Button id="Button2" runat="server" Text="保存成绩" OnClick="Button2_Click" />&nbsp;
                   <input type="reset" id="Button3" runat="server" value="取消" 
                     style="color:red;font-size: medium;font-weight: 700; font-family: 黑体" /></td>
            </tr>
        </table>    
    </div>
    </form>
</body>
</html>
