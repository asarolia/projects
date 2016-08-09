<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Comments.aspx.cs" Inherits="RunBoard.Comments" ValidateRequest="false"%>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
<style type="text/css">
.tableBackground
{
	background-color:silver;
	opacity:0.7;
}

  </style>
  
</asp:Content>


<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<p> Date: <asp:TextBox ID="dtlv3" runat="server" Enabled="false"></asp:TextBox>
        &nbsp;&nbsp;&nbsp;&nbsp;
       <%-- <asp:Button ID="Button1" runat="server" onclick="Button1_Click" Text="&lt;&lt;" 
            Width="90px" />--%>
            <asp:LinkButton ID="LinkButton1" runat="server" onclick="Button1_Click">&lt;&lt;</asp:LinkButton>
</p>
    <p> &nbsp;</p>
     <p> &nbsp;</p>
     <h1> Comments : <b> <asp:Label ID="commentheader" runat="server"></asp:Label>  </b></h1>
      <p> &nbsp;</p>
      <asp:ToolkitScriptManager ID="ScriptManager1" runat="server" CombineScripts="true" >
                  
</asp:ToolkitScriptManager>
            <div id="VGrid">
            
        <asp:GridView ID="GridView1" allowpaging="true"  runat="server" BackColor="White"  AutoGenerateColumns="False" BorderStyle="Solid" HorizontalAlign="Center" style="margin-left: 0px" Width="850px"> 
            <Columns>
                <%-- <asp:CommandField ButtonType ="Button" ShowEditButton="True" ShowDeleteButton ="True" HeaderText="Action" ItemStyle-Width="100"></asp:CommandField>--%> 
                <asp:TemplateField HeaderText="Edit">
	                <ItemTemplate>
		                
                        <asp:LinkButton ID="lnkEdit" Text="Edit"  OnClick="lnkEdit_Click" runat="server"></asp:LinkButton>
                        <asp:LinkButton ID="lnkDelete" Text="Delete"  OnClick="lnkDelete_Click" runat="server"></asp:LinkButton>
	                </ItemTemplate>
	            </asp:TemplateField>
                <asp:TemplateField HeaderText="Comment Text"  SortExpression="CommentTitle" ItemStyle-HorizontalAlign ="Center" >
                    <ItemTemplate>
                        <asp:Label ID="CommentText"  Columns="50" Width="500px"  runat="Server" Text='<%# Eval("CommentText")%>' TextMode="Multiline" Autosize="true" Style="word-wrap: normal; word-break: break-all;"  ItemStyle-VerticalAlign ="Middle"></asp:Label>                   
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="CommentRecordDt" DataFormatString="{0:yyyy-MM-dd HH:mm:ss.fff}"  HeaderText="Comment Entered Date/Time" SortExpression="CommentRecordDt" ItemStyle-HorizontalAlign ="Center" ItemStyle-VerticalAlign ="Middle" />
                <asp:BoundField DataField="RACFID" HeaderText="RACFID" SortExpression="RACFID" ItemStyle-HorizontalAlign ="Center" ItemStyle-VerticalAlign ="Middle" /> 
            </Columns>
            <EditRowStyle HorizontalAlign="Center" VerticalAlign="Middle" />
            <FooterStyle BackColor="#C6C3C6" ForeColor="Black" />
            <HeaderStyle Font-Bold="True" BackColor="#33CC33" ForeColor="#0033CC" />
            <PagerStyle BackColor="#C6C3C6" ForeColor="Black" HorizontalAlign="Center" />
            <RowStyle BackColor="#FFFF00" ForeColor="Black" />
            <SelectedRowStyle BackColor="#008A8C" Font-Bold="True" ForeColor="White" />
            <SortedAscendingCellStyle BackColor="#F1F1F1" />
            <SortedAscendingHeaderStyle BackColor="#0000A9" />
            <SortedDescendingCellStyle BackColor="#CAC9C9" />
            <SortedDescendingHeaderStyle BackColor="#000065" />
            <%--<EditRowStyle HorizontalAlign="Center" VerticalAlign="Middle" />
            <HeaderStyle Font-Bold="True" BackColor="#993300" ForeColor="White" />--%>
        </asp:GridView>
        <p> &nbsp;</p>
         <asp:Button ID="LinkAddComment" CommandName="Add" runat="server" Text="Add Comment" onclick="lnkAdd_Click" />
         <p> &nbsp;</p>
        <asp:Label ID="lblmsg" runat="server"/>
       
        <asp:Button ID="modelPopup1" runat="server" style="display:none" />
        <asp:ModalPopupExtender 
        ID="ModalPopupExtender1" runat="server" TargetControlID="modelPopup1" PopupControlID="updatePanel" CancelControlID="btnCancel" BackgroundCssClass="tableBackground">
        </asp:ModalPopupExtender>
       <asp:Panel ID="updatePanel" runat="server" BackColor="White" Height="250px" Width="600px" style="display:none">
            <table width="100%" cellspacing="4">
	            <tr style="background-color:#0066FF;Font:Bold;color:#FFFFFF">
	                <td colspan="2"  align="center">Edit Comment of:  <%: this.commentheader.Text%></td>
                   
	            </tr>
	            
	            <tr>
		            <td align="right">
		                RAG Date:
		            </td>
		            <td>
		                 <%: this.dtlv3.Text %>
		            </td>
	            </tr>
                <tr>
		            <td align="right">
		                Comment Entered Date/Time:
		            </td>
		            <td>
		                 <asp:Label ID="CommentRecordDt_Text" runat="server"/>
		            </td>
	            </tr>
                <tr>
		            <td align="right">
		                RACFID:
		            </td>
		            <td>
		                <asp:TextBox ID="racfid_text"  Maxlength ='8' runat="server"/> (Mandatory, Max of 8 Characters)
		            </td>
	            </tr>
	            
	            
                <tr>
		             <td align="right">
		                Comment Text:
		            </td>
		            <td>
		                <asp:TextBox ID="CommentText_text" TextMode="multiline" Columns="52" Rows="5"  runat="server"  ClientIDMode="Static" onkeyup="return charcount1()"/>
                        <br/>
                        Max 500 characters allowed <br/>
                     
                          
		                
                    </td>
                             
	            </tr>
                   
	            
	            <tr>
		            <td align="Left" >
		                <asp:Button ID="btnUpdate" CommandName="Update" runat="server" Text="Update"  OnClientClick=" return clientvalidate1()"  onclick="btnModity_Click"/>
		            </td>
		            <td align="Right">
		                <%-- <asp:Button ID="btnCancel"  CommandName="Cancel" runat="server" Text="Cancel" onclick="btnCancel_Click"/>--%>
                        <asp:Button ID="btnCancel"  CommandName="Cancel" runat="server" Text="Cancel" />
		            </td>
	            </tr>   
        </table>
       
    </asp:Panel>
    <script language="javascript" type="text/javascript">
        
        function clientvalidate1() 
        {
            var maxCommentLength = 500;
            var CommentText = document.getElementById("<%=CommentText_text.ClientID%>").value;
            var CommentLength = CommentText.length;
            if (CommentLength > maxCommentLength) 
               {

                   alert("Sorry, only " + maxCommentLength + " characters are allowed");
                   return false;
                }
               var maxRACFIDlength = 8;
               var RACFIDtext = document.getElementById("<%=racfid_text.ClientID%>").value;
               if (RACFIDtext == "")
               {
                   alert("RACFID cannot be blank");
                   return false;
               }

               return true;
           }
           function charcount1() {
               var maxCommentLength = 500;
               var CommentText = document.getElementById("<%=CommentText_text.ClientID%>").value;
               var CommentLength = CommentText.length;
               if (CommentLength > maxCommentLength) 
               {

                   alert("Sorry, only " + maxCommentLength + " characters are allowed");
                   return false;
               }
               else 
               {
                   return true;
               }
              
           }
           
    </script> 
     

    <asp:Button ID="modelPopup2" runat="server" style="display:none" />
        <asp:ModalPopupExtender 
        ID="ModalPopupExtender2" runat="server" TargetControlID="modelPopup2" PopupControlID="deletePanel" CancelControlID="btnCancel" BackgroundCssClass="tableBackground">
        </asp:ModalPopupExtender>
       <asp:Panel ID="deletepanel" runat="server" BackColor="White" Height="300px" Width="550px" style="display:none">
            <table width="100%" cellspacing="4">
	            <tr style="background-color:#0066FF;Font:Bold;color:#FFFFFF">
	                <td colspan="2"  align="center">Delete Comment of:  <%: this.commentheader.Text%></td>
                   
	            </tr>
	            
	            <tr>
		            <td align="right">
		                RAG Date:
		            </td>
		            <td>
		                 <%: this.dtlv3.Text %>
		            </td>
	            </tr>
                <tr>
		            <td align="right">
		                Comment Entered Date/Time:
		            </td>
		            <td>
		                 <asp:Label ID="CommentRecordDt_Text_delete" runat="server"/>
		            </td>
	            </tr>
                <tr>
		            <td align="right">
		                RACFID:
		            </td>
		            <td>
		                <asp:Label ID="racfid_text_delete"  runat="server"/>
		            </td>
	            </tr>
	           
	            <tr>
		             <td align="right">
		                Comment Text:
		            </td>
		            <td>
		            <asp:Label ID="CommentText_text_delete" TextMode="multiline" Columns="50" Rows="5" MaxLength='500' Width="400px" Height="100px" Style="overflow:scroll;word-wrap:break-word" runat="server"/>
                    </td>
	            </tr>
               <tr style="background-color:#33CC33;Font:Bold;color:#0033CC">
	                <td colspan="2"  align="center"> Are you sure you want to delete the comment? Press Delete to confirm or Cancel to Return</td>
                   
	            </tr>
	            
	            <tr>
		            <td align="Left" >
		                <asp:Button ID="deletebutton" CommandName="Delete" runat="server" Text="Delete" onclick="btnDelete_Click"/>
		            </td>
		            <td align="Right">
		                <asp:Button ID="cancelfordelete"  CommandName="Cancel" runat="server" Text="Cancel" />
		            </td>
	            </tr>   
        </table>
    </asp:Panel>

    <asp:Button ID="modelpopup3" runat="server" style="display:none" />
        <asp:ModalPopupExtender 
        ID="ModalPopupExtender3" runat="server" TargetControlID="modelPopup3" PopupControlID="addPanel" CancelControlID="btnCancel" BackgroundCssClass="tableBackground">
        </asp:ModalPopupExtender>
       <asp:Panel ID="addpanel" runat="server" BackColor="White" Height="250px" Width="550px" style="display:none">
            <table width="100%" cellspacing="4">
	            <tr style="background-color:#0066FF;Font:Bold;color:#FFFFFF">
	                <td colspan="2"  align="center">Add Comments for:  <%: this.commentheader.Text%></td>
                   
	            </tr>
	            
	            <tr>
		            <td align="right">
		                RAG Date:
		            </td>
		            <td>
		                 <%: this.dtlv3.Text %>
		            </td>
	            </tr>
              
                <tr>
		            <td align="right">
		                RACFID:
		            </td>
		            <td>
		                <asp:TextBox ID="racfid_text_add"   Maxlength ='8' runat="server"/> (Mandatory, Max of 8 Characters)
		            </td>
	            </tr>
	            
	            <tr>
		             <td align="right">
		                Comment Text:
		            </td>
		            <td>
		                
                        <asp:TextBox ID="CommentText_text_add" TextMode="multiline" Columns="52" Rows="5" onkeyup="return charcount2()" ClientIDMode="Static" runat="server"/>
		            <br/>
                        Max 500 characters allowed <br/>
                    </td>
	            </tr>
               
	            <tr>
		            <td align="Left" >
		                <asp:Button ID="AddComment" CommandName="Add" runat="server" Text="Add Comment" onclick="btnAdd_Click" OnClientClick=" return clientvalidate2()" />
		            </td>
		            <td align="Right">
		                <asp:Button ID="CancelAdd"  CommandName="Cancel" runat="server" Text="Cancel" />
		            </td>
	            </tr>   
        </table>
    </asp:Panel>
      </div>

      <script language="javascript" type="text/javascript">

          function clientvalidate2() {
              var maxCommentLength = 500;
              var CommentText = document.getElementById("<%=CommentText_text_add.ClientID%>").value;
              var CommentLength = CommentText.length;
              if (CommentLength > maxCommentLength) {

                  alert("Sorry, only " + maxCommentLength + " characters are allowed");
                  return false;
              }
              var maxRACFIDlength = 8;
              var RACFIDtext = document.getElementById("<%=racfid_text_add.ClientID%>").value;
              if (RACFIDtext == "") {
                  alert("RACFID cannot be blank");
                  return false;
              }

              return true;
          }
          function charcount2() {
              var maxCommentLength = 500;
              var CommentText = document.getElementById("<%=CommentText_text_add.ClientID%>").value;
              var CommentLength = CommentText.length;
              if (CommentLength > maxCommentLength) {

                  alert("Sorry, only " + maxCommentLength + " characters are allowed");
                  return false;
              }
              else {
                  return true;
              }

          }
           
    </script> 
     

      <asp:Button ID="modelpopup4" runat="server" style="display:none" />
        <asp:ModalPopupExtender 
        ID="ModalPopupExtender4" runat="server" TargetControlID="modelPopup4" PopupControlID="viewPanel" CancelControlID="btnCancel" BackgroundCssClass="tableBackground">
        </asp:ModalPopupExtender>
       <asp:Panel ID="viewpanel" runat="server" BackColor="White" Height="300px" Width="550px" style="display:none">
            <table width="100%" cellspacing="4">
	            <tr style="background-color:#0066FF;Font:Bold;color:#FFFFFF">
	                <td colspan="2"  align="center">View Comments for:  <%: this.commentheader.Text%></td>
                   
	            </tr>
	            
	            <tr>
		            <td align="right">
		                RAG Date:
		            </td>
		            <td>
		                 <%: this.dtlv3.Text %>
		            </td>
	            </tr>
              <tr>
		            <td align="right">
		                Comment Entered Date/Time:
		            </td>
		            <td>
		                 <asp:Label ID="CommentRecordDt_Text_view" runat="server"/>
		            </td>
	            </tr>
                <tr>
		            <td align="right">
		                RACFID:
		            </td>
		            <td>
		                <asp:Label ID="racfid_text_view"  runat="server"/>
		            </td>
	            </tr>
	             
	            <tr>
		             <td align="right">
		                Comment Text:
		            </td>
		            <td>
		                <asp:Label ID="CommentText_text_view" TextMode="multiline" Columns="50" Rows="5" MaxLength='500' Width="400px" Height="100px" Style="overflow:scroll;word-wrap:break-word" runat="server"/>
		            </td>
	            </tr>
               
	            <tr>
		            
		            <td align="Center" colspan="2">
		                <asp:Button ID="CancelView"  CommandName="Cancel" runat="server" Text="Back" />
		            </td>
	            </tr>   
        </table>
    </asp:Panel>

       <%--<asp:SqlDataSource ID="SqlDataSource1" runat="server" 
            ConnectionString="<%$ ConnectionStrings:rundashboardConnectionString %>" 

            SelectCommand="SELECT CommentText, CommentRecordDt FROM CommentTable Where RecordDt = '2014-11-04' and Type = 'PRINT' ORDER BY CommentTable.CommentRecordDt DESC" >
            <SelectParameters>
                <asp:SessionParameter Name="Type" SessionField="detail" />
                 <asp:SessionParameter Name="Date" SessionField="date" />
            </SelectParameters>
        </asp:SqlDataSource>--%>
    
</asp:Content>

