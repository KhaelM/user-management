<%@ Page Title="Liste employés" Language="C#" MasterPageFile="~/Template.Master" AutoEventWireup="true" CodeBehind="Employee.aspx.cs" Inherits="UserManagement.Data.Salary" %>
<asp:Content ID="Content1" ContentPlaceHolderID="BodyContent" runat="server">
    <asp:ListView ID="ListView1" runat="server" DataKeyNames="Id" ItemType="UserManagement.Model.User">
        <LayoutTemplate>
            <table class="table table-bordered text-right">
                <tr>
                    <th scope="col">#</th>
                    <th scope="col">Id</th>
                    <th scope="col">Nom</th>
                    <th scope="col">Salaire</th>
                </tr>
                <tbody>
                    <asp:PlaceHolder ID="itemPlaceHolder" runat="server" />
                </tbody>
            </table>
        </LayoutTemplate>
        <ItemTemplate>
            <tr>
                <th scope="row"><%# Container.DataItemIndex+1 %></th>
                <td><%#: Item.Id %></td>
                <td><%#: Item.Name %></td>
                <td><%#: Item.SalaryDisplay   %></td>
            </tr>
        </ItemTemplate> 
        <EmptyDataTemplate>
            <asp:Label runat="server" ID="exceptionMsg"></asp:Label>
        </EmptyDataTemplate>
    </asp:ListView>
    <nav>
        <% 
    if (Pagination.PagingBeginningIndex != 0)
    {
        %>
        <ul runat="server" id="pagination" class="pagination">
            <li runat="server" id="prevContainer" class="page-item">
                <a class="page-link" runat="server" id="prev" tabindex="-1" href="#">Previous</a>
            </li>

            <%
            for (int i = Pagination.PagingBeginningIndex ; i <= Pagination.PagingEndIndex; i++)
            {
            %>
               <li class="page-item <%= Pagination.CurrentPage == i ? "active" : "" %>"><a class="page-link" href="Salary.aspx?page=<%= i %>"><%= i %></a></li>
            <%
            } 
            %>

            <li runat="server" id="nextContainer" class="page-item">
                <a runat="server" id="next" class="page-link" href="#">Next</a>
            </li>
        </ul>
        <% 
    }
        %>
    </nav>
</asp:Content>
