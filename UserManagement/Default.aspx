<%@ Page Title="" Language="C#" MasterPageFile="~/Template.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="UserManagement.Default" %>
<asp:Content ID="Content1" ContentPlaceHolderID="BodyContent" runat="server">
    <div class="jumbotron row">
        <h1 class="display-4 col-md-12">Bienvenue <asp:Label ID="Username" runat="server"></asp:Label> !</h1>
        <p class="lead col-md-12">Vous êtes sur le site de gestion d'utilisateurs. Consultez, Modifiez, supprimez, mettez à jour des données selon vos droits d'ultilisateur.</p>
        <hr class="my-4">
        <p class="col-md-12">Cliquez ci-dessous pour avoir plus de détails sur votre profil.</p>
        <div class="col-md-12">
            <a runat="server" class="btn btn-primary btn-lg" href="~/Account/Profile.aspx" role="button">Voir profil</a>
        </div>
    </div>
</asp:Content>
