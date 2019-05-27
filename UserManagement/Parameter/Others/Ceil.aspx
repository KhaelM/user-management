<%@ Page Title="" Language="C#" MasterPageFile="~/Template.Master" AutoEventWireup="true" CodeBehind="Ceil.aspx.cs" Inherits="UserManagement.Parameter.Others.Ceil" %>
<asp:Content ID="Content1" ContentPlaceHolderID="BodyContent" runat="server">
    <div id="content" runat="server">
        <div class="form-group row mt-4">
            <label for="staticEmail" class="col-sm-2 col-form-label">Arrondi actuel</label>
            <div class="col-sm-10">
                <input type="text" readonly class="form-control-plaintext" value="<%= ActiveParameter.Name %>">
            </div>
        </div>
        <div class="form-group row">
            <asp:ListView ID="ListView1" runat="server" DataKeyNames="Id" ItemType="UserManagement.Model.CeilParameter">
                <LayoutTemplate>
                    <div class="col-sm-2">Ordre de l'arrondi</div>
                    <div class="col-sm-10">
                        <asp:PlaceHolder ID="itemPlaceHolder" runat="server"></asp:PlaceHolder>
                    </div>
                </LayoutTemplate>
                <ItemTemplate>
                    <div class="form-check">
                        <input class="form-check-input" type="radio" id="<%#: Item.Id %>" name="ceil" value="<%#: Item.Id %>">
                        <label class="form-check-label" for="<%#: Item.Id %>">
                            <%#: Item.Name %>
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