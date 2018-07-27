<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="studentmenu.aspx.cs" 
    Inherits="studentmenu" Title="欢迎使用学生成绩管理系统" Theme="Blue" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <link href="App_Themes/StyleSheet.css" rel="stylesheet" />
    <table  style="width: 100%; height: 100%;align-content: center;" >
        <tr>
            <td colspan="2" style="height: 22px">
                <asp:Label ID="Label1" runat="server" ></asp:Label>
            </td>
        </tr>
        <tr>
            <td style="width: 20%; background-color: aliceblue;height:100%">
                <asp:TreeView ID="TreeView1" runat="server" style="font-family:宋体;">
                    <Nodes >
                        <asp:TreeNode Text="选课管理" Value="选课管理" NavigateUrl="dispinfo.aspx?info=欢迎使用本系统" Target="Iframe1">
                            <asp:TreeNode Text="管理课程" Value="选修课程/取消课程" NavigateUrl="~/Student/selectcourse.aspx" Target="Iframe1"></asp:TreeNode>
                            <asp:TreeNode Text="查询选课" Value="列选课单" NavigateUrl="~/Student/listcourse.aspx" Target="Iframe1"></asp:TreeNode>
                        </asp:TreeNode>
                        <asp:TreeNode Text="成绩管理" Value="成绩管理" NavigateUrl="dispinfo.aspx?info=欢迎使用本系统" Target="Iframe1">
                            <asp:TreeNode Text="查询成绩" Value="我的成绩单" NavigateUrl="~/Student/listscore.aspx" Target="Iframe1"></asp:TreeNode>
                        </asp:TreeNode>
                        <asp:TreeNode Text="密码管理" Value="密码管理" NavigateUrl="dispinfo.aspx?info=欢迎使用本系统" Target="Iframe1">
                            <asp:TreeNode Text="更改密码" Value="更改密码" NavigateUrl="~/Student/updatestudentpass.aspx" Target="Iframe1"></asp:TreeNode>
                        </asp:TreeNode>
                    </Nodes>
                </asp:TreeView>
                <br />
                <asp:HyperLink ID="HyperLink1" runat="server" style="font-family:黑体; font-weight:bold;font-size:16px;color:gray"                    
                    NavigateUrl="~/Default.aspx" Target="_self">退出系统</asp:HyperLink>
            </td>
            <td style="width: 99%; height: 300px;">
               <div style="text-align:center">
                   <iframe name = "Iframe1" style=" height:100%; width:99%;text-align:center" id = "Iframe1" src="dispinfo.aspx?info=欢迎使用本系统">               
                   </iframe>
               </div>
            </td>
        </tr>
    </table>
</asp:Content>

