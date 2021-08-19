<%@ Page Title="Employee Management" Language="C#" MasterPageFile="~/Common.Master" AutoEventWireup="true" CodeBehind="EmployeeMasterlist.aspx.cs" Inherits="EmpManagement.EmployeeMasterlist" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
        <ContentTemplate>
            <asp:MultiView ID="mvMain" runat="server">
                <asp:View ID="v1" runat="server">
                    <asp:ImageButton ID="imgPop" runat="server" Style="display: none;" ImageUrl="~/images/spacer.png" />
                    <asp:ModalPopupExtender ID="imgPop_modalPop" runat="server"
                        BackgroundCssClass="pg-ld" CancelControlID="pnlModalPopClose" Enabled="True" PopupControlID="pnlModalPop"
                        TargetControlID="imgPop">
                    </asp:ModalPopupExtender>
                    <div class="card">
                        <div class="card-header">
                            <h4 class="page-header"><%: Title %></h4>
                        </div>
                        <div class="card-body">
                            <div class="row">
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>Search In</label>
                                        <asp:DropDownList ID="ddlSearchIn" runat="server" class="form-control" Enabled="true">
                                            <asp:ListItem Text="Select" Value="" Selected="True"></asp:ListItem>
                                            <asp:ListItem Text="Employee ID" Value="1"></asp:ListItem>
                                            <asp:ListItem Text="Name" Value="2"></asp:ListItem>
                                            <asp:ListItem Text="Email" Value="3"></asp:ListItem>
                                            <asp:ListItem Text="Designation" Value="4"></asp:ListItem>
                                            <asp:ListItem Text="Mobile" Value="5"></asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>Search By</label>
                                        <asp:TextBox ID="txtSearchKey" runat="server" class="form-control"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <asp:Button ID="btnLoadEmp" runat="server" Text="Load List" class="btn btn-primary btn-block mt-px-30" OnClick="btnLoadEmp_Click" />
                                </div>
                                <div class="col-md-3">
                                    <asp:Button ID="btnAddEmp" runat="server" Text="Add Employee" class="btn btn-primary btn-block float-right mt-px-30" OnClick="btnAddEmp_Click" />
                                </div>
                            </div>
                            <hr />
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="table-responsive">
                                        <div style="min-height: 400px">
                                            <asp:GridView ID="gdvEmployees" runat="server" CssClass="table table-bordered" AutoGenerateColumns="False" DataKeyNames="EmpId" OnSelectedIndexChanging="gdvEmployees_SelectedIndexChanging">
                                                <Columns>
                                                    <asp:CommandField SelectText="Details" ShowSelectButton="True" HeaderStyle-Width="5%" />
                                                    <asp:BoundField DataField="Name" HeaderText="NAME" HeaderStyle-Width="30%" />
                                                    <asp:BoundField DataField="Email" HeaderText="EMAIL" HeaderStyle-Width="25%" />
                                                    <asp:BoundField DataField="Designation" HeaderText="DESIGNATION" HeaderStyle-Width="20%" />
                                                    <asp:BoundField DataField="Mobile" HeaderText="MOBILE" HeaderStyle-Width="20%" />
                                                </Columns>
                                            </asp:GridView>
                                            <asp:GridView ID="gdvEmployees_Empty" CssClass="table table-bordered" runat="server" AutoGenerateColumns="False" DataKeyNames="EmpId">
                                                <Columns>
                                                    <asp:BoundField DataField="Name" HeaderText="NAME" HeaderStyle-Width="35%" />
                                                    <asp:BoundField DataField="Email" HeaderText="EMAIL" HeaderStyle-Width="25%" />
                                                    <asp:BoundField DataField="Designation" HeaderText="DESIGNATION" HeaderStyle-Width="20%" />
                                                    <asp:BoundField DataField="Mobile" HeaderText="MOBILE" HeaderStyle-Width="20%" />
                                                </Columns>
                                            </asp:GridView>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>                    
                </asp:View>
            </asp:MultiView>
            <asp:Panel ID="pnlModalPop" runat="server" Style="display: none">
                        <div class="modal in" tabindex="-1" role="dialog" aria-hidden="false" style="display: block">
                            <asp:Panel ID="pnlModalPop_Size" runat="server" class="modal-dialog def">
                                <div class="modal-content">
                                    <div class="modal-header">
                                        <h4 class="modal-title">
                                            <asp:TextBox ID="txtKeyValue" runat="server" Visible="false" ReadOnly="true"></asp:TextBox>
                                            <asp:Label ID="lblDataHeader" runat="server"></asp:Label>
                                        </h4>
                                        <button type="button" data-dismiss="modal" id="pnlModalPopClose" runat="server" aria-label="Close" class="close"><span aria-hidden="true">×</span></button>
                                    </div>
                                    <div class="modal-content">
                                        <asp:MultiView ID="mvPop" runat="server">
                                            <asp:View ID="vw_add_edit_employee" runat="server">
                                                <div class="card-body">
                                                    <div class="row">
                                                        <div class="col-md-12 grid-margin stretch-card">
                                                            <div class="card">
                                                                <div class="card-body dashboard-tabs p-0">
                                                                    <asp:TabContainer ID="tcPopEmp" runat="server" ActiveTabIndex="0" Width="100%">
                                                                        <asp:TabPanel ID="tbpBasic" runat="server" HeaderText="Basic Info">
                                                                            <ContentTemplate>
                                                                                <div class="row">
                                                                                    <div class="col-md-12">
                                                                                        <div class="form-group">
                                                                                            <label>Employee ID</label>
                                                                                            <asp:TextBox ID="txtEmpId" runat="server" class="form-control" ReadOnly="true"></asp:TextBox>
                                                                                        </div>
                                                                                    </div>
                                                                                </div>
                                                                                <div class="row">
                                                                                    <div class="col-md-6">
                                                                                        <div class="form-group">
                                                                                            <label>Name</label>
                                                                                            <asp:TextBox ID="txtName" runat="server" class="form-control"></asp:TextBox>
                                                                                        </div>
                                                                                    </div>
                                                                                    <div class="col-md-6">
                                                                                        <div class="form-group">
                                                                                            <label>Email</label>
                                                                                            <asp:TextBox ID="txtEmail" runat="server" class="form-control"></asp:TextBox>
                                                                                        </div>
                                                                                    </div>
                                                                                </div>
                                                                                <div class="row">
                                                                                    <div class="col-md-6">
                                                                                        <div class="form-group">
                                                                                            <label>Designation</label>
                                                                                            <asp:TextBox ID="txtDesignation" runat="server" class="form-control"></asp:TextBox>
                                                                                        </div>
                                                                                    </div>
                                                                                    <div class="col-md-6">
                                                                                        <div class="form-group">
                                                                                            <label>Mobile</label>
                                                                                            <asp:TextBox ID="txtMobile" runat="server" class="form-control"></asp:TextBox>
                                                                                        </div>
                                                                                    </div>
                                                                                </div>
                                                                                <hr />
                                                                                <div class="card-body">
                                                                                    <div class="row">
                                                                                        <div class="col-md-3">
                                                                                            <asp:Button ID="btnDeleteEmp" runat="server" Text="Delete" class="btn btn-danger btn-block" OnClick="btnDeleteEmp_Click" />
                                                                                            <asp:ConfirmButtonExtender ID="btnDeleteEmp_confirm" runat="server" TargetControlID="btnDeleteEmp" ConfirmText="Confirm to proceed"></asp:ConfirmButtonExtender>
                                                                                        </div>
                                                                                        <div class="col-md-6"></div>
                                                                                        <div class="col-md-3">
                                                                                            <asp:Button ID="btnUpdateEmp" runat="server" Text="Add" class="btn btn-primary btn-block" OnClick="btnUpdateEmp_Click" />
                                                                                            <asp:ConfirmButtonExtender ID="btnUpdateEmp_confirm" runat="server" TargetControlID="btnUpdateEmp" ConfirmText="Confirm to proceed"></asp:ConfirmButtonExtender>
                                                                                        </div>
                                                                                    </div>
                                                                                </div>
                                                                            </ContentTemplate>
                                                                        </asp:TabPanel>
                                                                        <asp:TabPanel ID="tbpDocuments" runat="server" HeaderText="Documents">
                                                                            <ContentTemplate>
                                                                                <div class="row">
                                                                                    <div class="col-md-4">
                                                                                        <div class="form-group">
                                                                                            <label>File Description</label>
                                                                                            <asp:TextBox ID="txtFileDesc" runat="server" class="form-control"></asp:TextBox>
                                                                                        </div>
                                                                                    </div>
                                                                                    <div class="col-md-4">
                                                                                        <div class="form-group">
                                                                                            <label>Choose File</label>
                                                                                            <asp:FileUpload ID="fupDocFile" runat="server"></asp:FileUpload>
                                                                                        </div>
                                                                                    </div>
                                                                                    <div class="col-md-4">
                                                                                        <asp:Button ID="btnUploadDoc" runat="server" Text="Upload Doc" class="btn btn-primary btn-block mt-px-30" OnClick="btnUploadDoc_Click" />
                                                                                    </div>
                                                                                </div>
                                                                                <hr />
                                                                                <div class="row">
                                                                                    <div class="col-md-12">
                                                                                        <div class="table-responsive">
                                                                                            <asp:Panel ID="pnlDocHeight" runat="server" Height="200" ScrollBars="Auto">
                                                                                                <asp:GridView ID="gdvEmployeesDocuments" runat="server" CssClass="table table-bordered" AutoGenerateColumns="False" DataKeyNames="DocId" OnSelectedIndexChanging="gdvEmployeesDocuments_SelectedIndexChanging" OnRowDeleting ="gdvEmployeesDocuments_RowDeleting"> 
                                                                                                    <Columns>
                                                                                                        <asp:CommandField SelectText="Download" ShowSelectButton="True" HeaderStyle-Width="5%" />
                                                                                                        <asp:CommandField DeleteText="Delete" ShowDeleteButton="True" HeaderStyle-Width="5%" />
                                                                                                        <asp:BoundField DataField="FileName" HeaderText="FILENAME" HeaderStyle-Width="35%" />
                                                                                                        <asp:BoundField DataField="FileDesc" HeaderText="DESCRIPTION" HeaderStyle-Width="35%" />
                                                                                                        <asp:BoundField DataField="CreatedDate" HeaderText="CREATED DATE" HeaderStyle-Width="25%" />                                                                                                        
                                                                                                    </Columns>
                                                                                                </asp:GridView>
                                                                                                <asp:GridView ID="gdvEmployeesDocuments_Empty" CssClass="table table-bordered" runat="server" AutoGenerateColumns="False" DataKeyNames="DocId">
                                                                                                    <Columns>
                                                                                                        <asp:BoundField DataField="FileName" HeaderText="NAME" HeaderStyle-Width="40%" />
                                                                                                        <asp:BoundField DataField="FileDesc" HeaderText="DESCRIPTION" HeaderStyle-Width="35%" />
                                                                                                        <asp:BoundField DataField="CreatedDate" HeaderText="CREATED DATE" HeaderStyle-Width="25%" />                                                                                                        
                                                                                                    </Columns>
                                                                                                </asp:GridView>
                                                                                            </asp:Panel>
                                                                                        </div>
                                                                                    </div>
                                                                                </div>
                                                                            </ContentTemplate>
                                                                        </asp:TabPanel>
                                                                    </asp:TabContainer>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </asp:View>
                                        </asp:MultiView>
                                        <div class="card-body">
                                            <div class="form-group">
                                                <asp:Panel ID="pnlPopError" runat="server"></asp:Panel>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </asp:Panel>
                        </div>
                    </asp:Panel>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
