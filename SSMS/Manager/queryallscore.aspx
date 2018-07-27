<%@ Page Language="C#" AutoEventWireup="true" CodeFile="queryallscore.aspx.cs" Inherits="queryallscore" StylesheetTheme="Blue" %>
<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title></title>
    <link href="../App_Themes/StyleSheet.css" rel="stylesheet" />
    <style type="text/css">
        .auto-style1 {
            width: 159px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table id="tablecenter">
            <tr>
                 <td colspan="2" style="text-align: center; height: 40px;">
                    <span class="auto-captionstyle">查找学生成绩</span>
                </td>
            </tr>
            <tr>
                <td class="auto-stringstyle" >
                   学 号
                </td>
                <td class="auto-style1">
                    <asp:TextBox ID="TextBox1" runat="server" style="width:100px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="auto-stringstyle">
                   姓 名
                </td>
                <td class="auto-style1">
                   <asp:TextBox ID="TextBox2" runat="server" style="width:100px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="auto-stringstyle" >
                    课程号
                </td>
                <td class="auto-style1">
                    <asp:TextBox ID="TextBox3" runat="server" style="width:100px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="auto-stringstyle">
                   课程名
                </td>
                <td class="auto-style1">
                    <asp:TextBox ID="TextBox4" runat="server" style="width:100px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                   <td colspan="2" style="text-align:center; height: 53px">
                    <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="确定" />
                    &nbsp; &nbsp;&nbsp;
                    <input id="Button2" runat="server" text="重置" type="reset" value="重置" class="auto-resettyle" /></td>
            </tr>
        </table>
    
    </div>
    </form>
</body>
</html>
