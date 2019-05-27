<%@ Page Title="Droit Utilisateurs" Language="C#" MasterPageFile="~/Template.Master" AutoEventWireup="true" CodeBehind="Users.aspx.cs" Inherits="UserManagement.Parameter.Users" %>

<asp:Content ID="Content1" ContentPlaceHolderID="BodyContent" runat="server">
    <div runat="server" id="content">
        <div class="form-group row mt-3">
            <label for="profiles" class="col-sm-2 col-form-label">Employés</label>
            <div class="col-sm-8">
                <input runat="server" class="form-control" id="users" placeholder="Ex: 1;2 ou % pour tous">
            </div>
        </div>
        <div class="form-group row">
            <label for="tables" class="col-sm-2 col-form-label">Tables</label>
            <div class="col-sm-8">
                <input runat="server" class="form-control" id="tables" placeholder="Ex: Salaire;Profil ou % pour tous">
            </div>
        </div>

        <div class="form-group row">
            <asp:ListView ID="ListView1" runat="server" DataKeyNames="Id" ItemType="UserManagement.Model.Checkbox">
                <LayoutTemplate>
                    <div class="col-sm-2">Droits</div>
                    <div class="col-sm-10">
                        <asp:PlaceHolder ID="itemPlaceHolder" runat="server"></asp:PlaceHolder>
                    </div>
                </LayoutTemplate>
                <ItemTemplate>
                    <div class="form-check">
                        <input class="form-check-input" type="checkbox" id="<%#: Item.Id %>" name="<%#: Item.Name %>" <%#: Item.Checked %> value="<%#: Item.Value %>">
                        <label class="form-check-label" for="<%#: Item.Id %>">
                            <%#: Item.Label %>
                        </label>
                    </div>
                </ItemTemplate>
            </asp:ListView>
        </div>
        <div class="form-group row">
            <div class="col-sm-10">
                <button type="submit" class="btn btn-primary">Mettre à jour</button>
            </div>
        </div>
    </div>
    <div runat="server" id="exception" class="text-danger" visible="false"></div>
    <div id="hiddenContent" runat="server" visible="false">
        <h1 runat="server" id="message" class="text-danger"></h1>
    </div>
</asp:Content>
