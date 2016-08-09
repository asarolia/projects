<%@ Page Language="C#" MasterPageFile="~/MasterPages/MasterPage.master" AutoEventWireup="true" CodeFile="Welcome.aspx.cs" Inherits="Welcome" Title="Exceed Data Customisation" %>

<asp:Content ID="Content2" ContentPlaceHolderID="Head" Runat="Server">
    <script language="javascript" type="text/javascript">
        $(document).ready(function(){
        $('#aHome').addClass('selected');
        $('#navigation').hide();
        });
    </script>
    <title>Product Clone utitlity</title>
</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="Body" Runat="Server">
<div id="main" class="nosidebar">
    <div id="sidebar"></div>
    <div id="content">
        <h2>Overview</h2>
        Use <b>Clone Proposition</b> to clone existing proposition into a new proposition.
        <br />
        <span class="breadcrumb">This process is sensitive to scheme code, product code & master company number while selecting list of support table for download </span>
        <br />
        <br />
        Use <b>Download Proposition</b> to download all the proposition data into Excel sheet for reviewing.
        <br />
        <span class="breadcrumb">This process is sensitive to scheme code, product code & master company number while selecting list of support table for download </span>
        <br />
        <br />
        Use <b>Browse Module</b> to browse host cobol module
        <br />
        <span class="breadcrumb">This is process currently only works for cobol modules.  Cobol modules are formatter for better readability</span>
        <br />
        <br />
        Use <b>RDX </b> (work in progress)to RDX policies from one region to another
        <br />
        <span class="breadcrumb">This will help setup / copy data from live to dev regions</span>
    </div>
</div>
</asp:Content>

