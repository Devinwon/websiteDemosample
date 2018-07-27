<%@ Page Language="C#" AutoEventWireup="true" CodeFile="addteacher.aspx.cs" Inherits="addteacher" StylesheetTheme="Blue" %>
<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title></title>
    <link href="../App_Themes/StyleSheet.css" rel="stylesheet" />  
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table id="tablecenter" style="width:400px">
            <tr>
                 <td colspan="2" style="text-align: center; height: 40px;">
                    <span class="auto-captionstyle">添加新的教师记录</span>
                </td>
            </tr>
            <tr>
                <td class="auto-stringstyle" style="width:100px">编 号</td>
                <td style="width:300px">
                    <asp:TextBox ID="TextBox1" runat="server" style="width:100px"></asp:TextBox>
                    &nbsp; 
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="TextBox1"
                        ErrorMessage="编号不能为空"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td class="auto-stringstyle">姓 名</td>
                <td>
                    <asp:TextBox ID="TextBox2" runat="server" style="width:100px"></asp:TextBox>
                    &nbsp;
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="TextBox2"
                        ErrorMessage="姓名不能为空"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td class="auto-stringstyle">性 别</td>
                <td>
                    <asp:RadioButton ID="RadioButton1" runat="server" GroupName="tsex" Text="男" />
                    &nbsp;
                    <asp:RadioButton ID="RadioButton2" runat="server" GroupName="tsex" Text="女" />
                </td>
            </tr>
            <tr>
                <td class="auto-stringstyle">系 别
                </td>
                <td>
                    <asp:TextBox ID="TextBox3" runat="server" style="width:100px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td colspan="2" style="height: 53px;text-align:center" >
                    <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="提交" />
                    &nbsp;
                    <input id="Reset1" runat="server" text="重置" type="reset" value="重置" class="auto-resettyle" />
                </td>
            </tr>
        </table>
    
    </div>
    </form>
</body>
</html>
