<%@ Page Title="Login" Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="UserManagement.Account.Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link type="text/css" rel="stylesheet" href="~/Content/signin.css" />
    <link type="text/css" rel="stylesheet" href="~/Content/bootstrap.css" />
    <title><%: Page.Title %></title>
</head>
<body>
        <form runat="server" class="form-signin">
            <h1 class="h3 mb-3 font-weight-normal">Veuillez vous connecter</h1>
            <label for="inputEmail" class="sr-only">Nom d'utilisateur</label>
            <input type="text" id="inputUsername" name="inputUsername" class="form-control" placeholder="Nom d'utilisateur" required="required" autofocus="autofocus" />
            <label for="inputPassword" class="sr-only">Mot de passe</label>
            <input type="password" id="inputPassword" name="inputPassword" class="form-control" placeholder="Mot de passe" required="required" />
            <button class="btn btn-lg btn-primary btn-block" type="submit">Connexion</button>
            <asp:Label ID="Message" runat="server"></asp:Label>
        </form>
</body>
</html>
