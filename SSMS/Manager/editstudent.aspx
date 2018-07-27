<%@ Page Language="C#" AutoEventWireup="true" CodeFile="editstudent.aspx.cs" Inherits="editstudent" StylesheetTheme="Blue" %>
<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title></title>
    <link href="../App_Themes/StyleSheet.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table id="tablecenter" style="width:300px">
            <tr>
                 <td colspan="2" style="text-align: center; height: 40px;">
                    <span class="auto-captionstyle">查找要编辑的学生记录</span>
                </td>
            </tr>
            <tr>
                <td class="auto-stringstyle" >学 号</td>
                <td >
                    <asp:TextBox ID="TextBox1" runat="server" style="width:100px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="auto-stringstyle">
                   姓 名
                </td>
                <td>
                    <asp:TextBox ID="TextBox2" runat="server" style="width:100px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="auto-stringstyle">
                    性 别
                </td>
                <td> 
                    <asp:RadioButton ID="RadioButton1" runat="server" Text="男" />&nbsp;
                    <asp:RadioButton ID="RadioButton2" runat="server" Text="女" />
                </td>
            </tr>
            <tr>
                <td class="auto-stringstyle">
                    民 族
                </td>
                <td >
                    <asp:TextBox ID="TextBox3" runat="server" style="width:100px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="auto-stringstyle">
                   班 号
                </td>
                <td>
                    <asp:TextBox ID="TextBox4" runat="server" style="width:100px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td colspan="2" style="height: 53px;text-align:center">          
                    <asp:Button ID="Button1" runat="server" Text="确定" OnClick="Button1_Click" />
                    &nbsp; &nbsp; 
                    <input type="reset" ID="Button2" runat="server" Text="重置" class="auto-resettyle" /></td>
            </tr>
        </table>    
    </div>
    </form>
</body>
</html>
