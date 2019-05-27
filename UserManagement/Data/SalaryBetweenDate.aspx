<%@ Page Title="" Language="C#" MasterPageFile="~/Template.Master" AutoEventWireup="true" CodeBehind="SalaryBetweenDate.aspx.cs" Inherits="UserManagement.Data.SalaryBetweenDate" %>

<asp:Content ID="Content1" ContentPlaceHolderID="BodyContent" runat="server">
    <div class="row form-inline col-lg-6 offset-lg-3">
        <div class="row justify-content-center mt-3">
            <div class="form-group mb-2">
                <label for="beginningDate">Début</label>
                <input runat="server" type="date" class="form-control" id="beginningDate">
            </div>
            <div class="form-group mx-sm-3 mb-2">
                <label for="endingDate">Fin</label>
                <input runat="server" type="date" class="form-control" id="endingDate">
            </div>
            <asp:Button runat="server" OnClick="Unnamed_Click" CssClass="btn btn-primary mb-2" Text="Afficher" />
        </div>
    </div>

    <div class="row">
        <div  class="accordion w-100" id="accordionExample">
            <%
                if (otherAccessibleUsers != null)
                {
                    for (int i = 0; i < otherAccessibleUsers.Length; i++)
                    {
            %>
            <div class="card">
                <div class="card-header" id="<%= otherAccessibleUsers[i].Id %>">
                    <h2 class="mb-0">
                        <button class="btn btn-link collapsed" type="button" data-toggle="collapse" data-target="#collapse<%= otherAccessibleUsers[i].Id %>" aria-expanded="false" aria-controls="collapse<%= otherAccessibleUsers[i].Id %>">
                            <%= otherAccessibleUsers[i].Name %>
                        </button>
                    </h2>
                </div>

                <div id="collapse<%= otherAccessibleUsers[i].Id %>" class="collapse" aria-labelledby="<%= otherAccessibleUsers[i].Id %>" data-parent="#accordionExample">
                    <% 
                        try
                        {
                            display = otherAccessibleUsers[i].GetSalaryBetweenDates(beginningDate.Value, endingDate.Value, allUsers, allSalaryRisings);
                            if (display.Length != 0)
                            {
                        %>
                        <div class="card-body">
                            <table class="table table-bordered">
                                <thead>
                                    <tr>
                                        <th>Mois</th>
                                        <th>Année</th>
                                        <th>Salaire</th>
                                    </tr>
                                </thead>
                                <tbody>
                                    <%
                                        for (int j = 0; j < display.Length; j++)
                                        {
                                    %>
                                    <tr>
                                        <td><%= display[j].Month %></td>
                                        <td><%= display[j].Date.Year %></td>
                                        <td><%= display[j].SalaryFormatted  %></td>
                                    </tr>
                                    <%
                                        }
                                    %>
                                </tbody>
                            </table>
                        </div>
                        <%
                            }
                        %>
                    </div>
                        <%
                            }

                            catch (Exception e)
                            {
                        %>
                        <p><%= e.Message %></p>
                        <%

                            }
                        %>
            </div>


            <%
                    }
                }
            %>
        </div>
    </div>
</asp:Content>
