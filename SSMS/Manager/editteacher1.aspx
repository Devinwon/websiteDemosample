<%@ Page Language="C#" AutoEventWireup="true" CodeFile="editteacher1.aspx.cs" Inherits="editteacher1" StylesheetTheme="Blue" %>
<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title></title>
    <link href="../App_Themes/StyleSheet.css" rel="stylesheet" />
    <style type="text/css">
        .auto-style1 {
            font-family: 楷体;
            font-size: medium;
            color: #0000FF;
            font-weight: bold;
            text-align: right;
            width: 128px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server" style="width:400px">
    <div>
        <table id="tablecenter">
            <tr>
                 <td colspan="2" style="text-align: center; height: 40px;">
                    <span class="auto-captionstyle">编辑教师记录</span>
                </td>
            </tr>
            <tr>
                <td class="auto-style1">编 号</td>
                <td style="width:250px">
                    <asp:TextBox ID="TextBox1" runat="server" Enabled="False" style="width:100px"></asp:TextBox>                   
                </td>
            </tr>
            <tr>
                <td class="auto-style1">
                    姓 名
                </td>
                <td>
                    <asp:TextBox ID="TextBox2" runat="server" style="width:100px"></asp:TextBox>                    
                       &nbsp;
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="TextBox2"
                        ErrorMessage="姓名不能为空" ></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td class="auto-style1">
                    性 别
                </td>
                <td>
                    <asp:RadioButton ID="RadioButton1" runat="server" GroupName="tsex" Text="男" />
                    &nbsp;
                    <asp:RadioButton ID="RadioButton2" runat="server" GroupName="tsex" Text="女" />
                </td>
            </tr>
            <tr>
                <td class="auto-style1">
                   系 别
                </td>
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
