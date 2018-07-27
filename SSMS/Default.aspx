<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" 
    Inherits="_Default" Title="欢迎使用学生成绩管理系统" StylesheetTheme="Blue" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <link href="App_Themes/StyleSheet.css" rel="stylesheet" />
    <table style="width: 100%; background-color: aliceblue; cellspacing:1;height:100%;" border="0px">
        <tr>
            <td colspan="2" style="text-align: center; height: 70px;">
                <strong><span style="font-size: 30pt; color: #12ab3b; font-family: 华文新魏">用户登录</span></strong>
            </td>
        </tr>
        <tr>
            <td class="auto-stringstyle" style="height: 20px;font-size: 20pt;color: #12ab3b"> 用户
            </td>
            <td style="width: 60%; height: 20px;">
                <asp:TextBox ID="TextBox1" runat="server" style="width:170px;height:75%"></asp:TextBox>
                &nbsp;
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="TextBox1"
                    ErrorMessage="用户不能为空" style="color:red"> </asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td class="auto-stringstyle" style="height: 20px;font-size: 20pt;color: #12ab3b">                  
                密码
            </td>
            <td style="width: 60%; height: 20px;">               
                  <asp:TextBox ID="TextBox2" runat="server" textMode="Password" style="width:170px;height:75%" ValidateRequestMode="Enabled"></asp:TextBox>
                 &nbsp;
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="TextBox2"
                    ErrorMessage="密码不能为空" style="color:red"></asp:RequiredFieldValidator>                 
            </td>
        </tr>
        <tr>
            <td class="auto-stringstyle" style="height: 20px;font-size: 20pt;color: #12ab3b">
                验证码
            </td>
            <td style="width: 60%; height: 22px">
                <asp:TextBox ID="TextBox3" runat="server" style="width:70px;height:75%"></asp:TextBox>

                <strong><span style="color: #339966; font-family: 仿宋; font-size: 22px; height:75%;width:26px"></span></strong>
                <asp:Label ID="Label1" runat="server" style="color:black;background-color:gray;width:30px;"/>                                                                                                                                  
                &nbsp;
                <asp:Button ID="Button3" runat="server" OnClick="Button3_Click" Text="刷新"
                        style="font-size: small;color:red;font-weight: bold;" />
            </td>
        </tr>

        <tr>
            <td class="auto-stringstyle" style="height: 20px;font-size: 20pt;color: #12ab3b">
                身份</td>
            <td style="width: 60%; height: 24px;">
                <asp:RadioButton ID="RadioButton1" runat="server" Text="学生" GroupName="sel" style="font-size:17px" />
                &nbsp;
                <asp:RadioButton ID="RadioButton2" runat="server" Text="教师" GroupName="sel" style="font-size:17px" />
                &nbsp; 
                <asp:RadioButton ID="RadioButton3" runat="server" Text="管理员" GroupName="sel" style="font-size:17px" />
            </td>
        </tr>
        
        <tr>
            <td colspan="2" style="text-align:center;height:56px">
                <asp:Button ID="Button1" runat="server" Text="登   录" OnClick="Button1_Click" style="font-size:25px;color:#12ab3b" />
                &nbsp; &nbsp; &nbsp;
                <input type ="reset" ID="Button2" value="重 置" class="auto-resettyle" style="font-size:25px;color:gray" />
            </td>
        </tr>
    </table>
</asp:Content>

