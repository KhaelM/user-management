<%@ Page Title="" Language="C#" MasterPageFile="~/Template.Master" AutoEventWireup="true" CodeBehind="Profile.aspx.cs" Inherits="UserManagement.Account.Profile" %>
<asp:Content ID="Content1" ContentPlaceHolderID="BodyContent" runat="server">
    <div class="row">
        <div class="col-md-3">
            <div><strong>Id: </strong><asp:Label runat="server" ID="UserId"></asp:Label></div>
            <div><strong>Nom d'utilisateur: </strong><asp:Label runat="server" ID="Username"></asp:Label></div>
            <div><strong>Mot de passe: </strong><asp:Label runat="server" ID="Password"></asp:Label></div>
            <div><strong>Profil: </strong><asp:Label runat="server" ID="Profil"></asp:Label></div>
            <div><strong>Salaire actuel: </strong><asp:Label runat="server" ID="Salary"></asp:Label> Ariary</div>
        </div>
    </div>
</asp:Content>
