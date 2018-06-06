<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="Highscore.aspx.vb" Inherits="Tamagotchi.Highscore" %>

<%@ Import Namespace="MongoDB.Driver.Builders" %>
<%@ Import Namespace="MongoDB.Driver" %>
<%@ Import Namespace="MongoDB.Bson" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <link rel="stylesheet" href="Content/bootstrap.min.css" />
    <script src="//maxcdn.bootstrapcdn.com/bootstrap/3.3.0/js/bootstrap.min.js"></script>
    <script src="//code.jquery.com/jquery-1.11.1.min.js"></script>
    <title>HighScore</title>
</head>
<body>
    <form id="form1" runat="server">
        <nav class="navbar navbar-expand-lg navbar-light bg-light">
            <button class="navbar-toggler" type="button" data-toggle="collapse" data-target="#navbarTogglerDemo01" aria-controls="navbarTogglerDemo01" aria-expanded="false" aria-label="Toggle navigation">
                <span class="navbar-toggler-icon"></span>
            </button>
            <div class="collapse navbar-collapse" id="navbarTogglerDemo01">
                <a class="navbar-brand" href="AccountInfo.aspx">Tamagotchi</a>
                <ul class="navbar-nav mr-auto mt-2 mt-lg-0">
                    <li class="nav-item active">
                        <a class="nav-link" href="Highscore.aspx">HighScores <span class="sr-only">(current)</span></a>
                    </li>
                </ul>
                <asp:LinkButton ID="leave" Text="Leave" OnClick="Leave_Event" class="navbar-brand" runat="server" />
            </div>
        </nav>
        <div class="container">
            <div class="row" style="margin-top: 5%;">
                <div class="col-md-3"></div>
                <div class="col-md-4">
                    <table class="table">
                        <thead class="thead-dark">
                            <tr>
                                <th scope="col">Player Name</th>
                                <th scope="col">Pet Name</th>
                                <th scope="col">Total Played</th>
                                <th scope="col">Max Score</th>
                                <th scope="col">Date of Death</th>
                                <th scope="col">Total Points</th>
                            </tr>
                        </thead>
                        <tbody>
                            <% Dim cursorPet = getAllPets() %>
                            <% Dim cursorMinigame = getAllMinigames() %>
                            <% Dim count As Integer = 0 %>
                            <% If cursorPet IsNot Nothing Then %>
                            <% For each pet In cursorPet %>
                            <% For each minigame In cursorMinigame %>
                            <% If (pet("PlayerName") = minigame("PlayerName")) And (pet("MonsterName") = minigame("MonsterName")) Then %>
                            <% count = count + 1 %>

                            <tr>
                                <td>
                                    <%= pet("PlayerName") %>
                                </td>
                                <td>
                                    <%= pet("MonsterName") %>
                                </td>
                                <td>
                                    <%= minigame("NumberOfGames") %>
                                </td>
                                <td>
                                    <%= minigame("MaxScore") %>
                                </td>
                                <td>
                                    <%If (pet("State") = "Dead") Then %>
                                    <%= Convert.ToDateTime(pet("LastTimeState")).ToString("dd/MM/yyyy HH:mm:ss") %>
                                    <%Else%>
                                    ---
                                    <%End if %>
                                </td>
                                <td>
                                    <%Dim last As DateTime = Convert.ToDateTime(pet("LastTimeState")) %>
                                    <%Dim init As DateTime = Convert.ToDateTime(pet("InitialTime")) %>
                                    <%Dim diff As Double = Math.Abs((last - init).TotalSeconds) %>
                                    <%= diff  %>
                                </td>
                            </tr>
                            <% End If %>
                            <% Next %>
                            <% Next %>
                            <% End If %>
                            <% If (count = 0) Then %>

                            <% End If %>
                        </tbody>
                    </table>
                </div>
            </div>
        </div>
    </form>
    <script src="Content/jquery-3.3.1.slim.min.js"></script>
    <script src="Content/popper.min.js"></script>
    <script src="Content/bootstrap.min.js"></script>
</body>
</html>
