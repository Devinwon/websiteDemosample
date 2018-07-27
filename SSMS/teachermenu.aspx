<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="teachermenu.aspx.cs" Inherits="teachermenu" Title="欢迎使用学生成绩管理系统" StyleSheetTheme="Blue" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server"> 
    <link href="App_Themes/StyleSheet.css" rel="stylesheet" />   
    <table style="width: 100%; height: 100%">
        <tr>
            <td colspan="2" style="height: 22px">
                <asp:Label ID="Label1" runat="server"></asp:Label></td>
        </tr>
        <tr>
            <td style="width: 20%; height:100%; background-color: aliceblue">
                <asp:TreeView ID="TreeView1" runat="server" style="font-family:宋体;">
                    <Nodes>
                        <asp:TreeNode Target="Iframe1" Text="课程管理" Value="课程管理" NavigateUrl="dispinfo.aspx?info=欢迎使用本系统">
                            <asp:TreeNode Target="Iframe1" Text="查看我的课程" Value="查看我的课程" NavigateUrl="~/Teacher/querymycourse.aspx"></asp:TreeNode>
                            <asp:TreeNode Text="查看选课学生" Value="查看选课学生" NavigateUrl="~/Teacher/querymystudent.aspx" Target="Iframe1"></asp:TreeNode>
                        </asp:TreeNode>
                        <asp:TreeNode Text="成绩管理" Value="成绩管理" Target="Iframe1" NavigateUrl="dispinfo.aspx?info=欢迎使用本系统">
                            <asp:TreeNode NavigateUrl="~/Teacher/inputstudentscore.aspx" Target="Iframe1" Text="输入学生成绩" Value="输入学生成绩">
                            </asp:TreeNode>
                            <asp:TreeNode Target="Iframe1" Text="查看学生成绩" Value="查看学生成绩" NavigateUrl="~/Teacher/querystudentscore.aspx"></asp:TreeNode>
                        </asp:TreeNode>
                        <asp:TreeNode Text="密码管理" Value="密码管理" Target="Iframe1" NavigateUrl="dispinfo.aspx?info=欢迎使用本系统">
                            <asp:TreeNode NavigateUrl="~/Teacher/updateteacherpass.aspx" Target="Iframe1" Text="更改密码"
                                Value="更改密码"></asp:TreeNode>
                        </asp:TreeNode>
                    </Nodes>
                </asp:TreeView>
                <br />
                <asp:HyperLink ID="HyperLink1" runat="server" style="font-family:黑体; font-weight:bold;font-size:16px;color:gray"                    
                    NavigateUrl="~/Default.aspx" Target="_self">退出系统</asp:HyperLink>
            <td style="width: 99%; height: 300px">
                <iframe id="Iframe1" name="Iframe1" src="dispinfo.aspx?info=欢迎使用本系统" style="width: 99%;
                    height: 99%"></iframe>
            </td>
        </tr>
    </table>
</asp:Content>

