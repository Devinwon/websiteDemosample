<%@ Page Language="C#" AutoEventWireup="true" CodeFile="editstudent2.aspx.cs" Inherits="editstudent2" StylesheetTheme="Blue" %>
<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title></title>
    <link href="../App_Themes/StyleSheet.css" rel="stylesheet" /
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table id="tablecenter" style="width:400px">
            <tr>
                 <td colspan="2" style="text-align: center; height: 40px;">
                    <span class="auto-captionstyle">编辑学生记录</span>
                </td>
            </tr>
            <tr>
                <td class="auto-stringstyle" style="width:150px">学 号</td>
                <td style="width:250px">
                    <asp:TextBox ID="TextBox1" runat="server" Enabled="False" style="width:100px"></asp:TextBox>              
                </td>         
            </tr>
            <tr>
                <td class="auto-stringstyle">姓 名</td>
                <td>
                    <asp:TextBox ID="TextBox2" runat="server" style="width:100px"></asp:TextBox>
                    &nbsp;
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="TextBox2"
                        ErrorMessage="姓名不能为空"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td class="auto-stringstyle">性 别
                </td>
                <td >
                    <asp:RadioButton ID="RadioButton1" runat="server" GroupName="ssex" Text="男" />
                    &nbsp;
                    <asp:RadioButton ID="RadioButton2" runat="server" GroupName="ssex" Text="女" />
                </td>
            </tr>
            <tr>
                <td class="auto-stringstyle">民 族</td>
                <td style="width: 236px; height: 24px">
                    <asp:DropDownList ID="DropDownList1" runat="server">
                        <asp:ListItem>汉族</asp:ListItem>
                        <asp:ListItem>回族</asp:ListItem>
                        <asp:ListItem>满族</asp:ListItem>
                        <asp:ListItem>朝鲜族</asp:ListItem>
                        <asp:ListItem>土家族</asp:ListItem>
                        <asp:ListItem>其他</asp:ListItem>
                    </asp:DropDownList></td>
            </tr>
            <tr>
                <td class="auto-stringstyle">班 号</td>
                <td>
                    <asp:TextBox ID="TextBox3" runat="server" style="width:100px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td colspan="2" style="height: 53px;text-align:center">  
                    <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="提交" />
                    &nbsp;
                    <input id="Reset1" runat="server" text="重置" type="reset" value="重置" class="auto-resettyle" /></td>
            </tr>
        </table>    
    </div>
    </form>
</body>
</html>
