<%@ Page Title="" Language="C#" MasterPageFile="~/Template.Master" AutoEventWireup="true" CodeBehind="CalculatedElements.aspx.cs" Inherits="UserManagement.Parameter.Others.CalculatedElements" %>
<asp:Content ID="Content1" ContentPlaceHolderID="BodyContent" runat="server">
    <div id="content" runat="server">
        <div class="form-group row mt-3">
            <label for="indemnityInput" class="col-sm-2 col-form-label">Elements Calculés</label>
            <div class="col-sm-8">
                <input name="indemnityInput" class="form-control" id="indemnityInput" placeholder="Ex: déplacement;logement;Cnaps ou % pour tous">
            </div>
        </div>
        <div class="form-group row">
            <div class="col-sm-2">Type</div>
            <div class="col-sm-10">
                <asp:ListView runat="server" ID="Indemnities" ItemType="UserManagement.Model.IndemnityType">
                    <LayoutTemplate>
                        <asp:PlaceHolder runat="server" ID="itemPlaceholder"></asp:PlaceHolder>
                    </LayoutTemplate>
                    <ItemTemplate>
                        <div class="form-check">
                            <input class="form-check-input" type="radio" id="<%#: Item.Name %>" name="IndemnityType" value="<%#: Item.Id %>">
                            <label class="form-check-label" for="formule">
                                <%#: Item.Name %>
                            </label>
                        </div>
                    </ItemTemplate>
                </asp:ListView>
            </div>
        </div>
        <div class="form-group row">
            <label for="formulaInput" class="col-sm-2 col-form-label">Formule/Valeur</label>
            <div class="col-sm-8">
                <input class="form-control" id="formulaInput" name="formulaInput">
            </div>
        </div>
        <div class="form-group row">
            <div class="col-sm-2">Signe</div>
            <div class="col-sm-10">
                <div class="form-check">
                    <input class="form-check-input" type="radio" id="positive" name="sign" value="1">
                    <label class="form-check-label" for="positive">
                        +
                    </label>
                </div>
                <div class="form-check">
                    <input class="form-check-input" type="radio" id="negative" name="sign" value="-1">
                    <label class="form-check-label" for="negative">
                        -
                    </label>
                </div>
            </div>
        </div>
        <div class="form-group row mt-3">
            <label for="usersInput" class="col-sm-2 col-form-label">Id personnes</label>
            <div class="col-sm-8">
                <input class="form-control" name="usersInput" id="usersInput" placeholder="Utilisateur1;Utilisateur2 ou % pour tous">
            </div>
        </div>
        <div class="form-group row mt-3">
            <label for="beginningDate" class="col-sm-2 col-form-label">Date début d'application</label>
            <div class="col-sm-8">
                <input type="date" class="form-control" id="beginningDate" name="beginningDate">
            </div>
        </div>
        <div class="form-group row mt-3">
            <label for="endingDate" class="col-sm-2 col-form-label">Date fin d'application</label>
            <div class="col-sm-8">
                <input type="date" class="form-control" id="endingDate" name="endingDate">
            </div>
        </div>
        <div class="form-group row mt-3">
            <label for="monthsInput" class="col-sm-2 col-form-label">Mois d'application</label>
            <div class="col-sm-8">
                <input name="monthsInput" class="form-control" id="monthsInput" placeholder="Ex: 1;3 pour Janvier et Mars, 1-3 pour Janvier à Mars">
            </div>
        </div>
        <div class="form-group row mt-3">
            <label for="yearsInput" class="col-sm-2 col-form-label">Années d'application</label>
            <div class="col-sm-8">
                <input name="yearsInput" class="form-control" id="yearsInput" placeholder="Ex: 2018;2020 pour 2018 et 2018, 2018-2020 pour 2018 à 2020">
            </div>
        </div>
        <div class="form-group row">
            <div class="col-sm-10">
                <button type="submit" class="btn btn-primary">Mettre à jour</button>
            </div>
        </div>
    </div>
    <div id="hiddenContent" runat="server"><h1 runat="server" id="message" class="text-danger"></h1></div>
</asp:Content>
