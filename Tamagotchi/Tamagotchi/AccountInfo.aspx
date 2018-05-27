<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="AccountInfo.aspx.vb" Inherits="Tamagotchi.AccountInfo" %>
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
    <title>Account Information</title>
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
                    <%--                <li class="nav-item">
                    <a class="nav-link" href="#">Link</a>
                </li>--%>
                </ul>
                <asp:LinkButton ID="signout" Text="Sign Out" OnClick="SignOut_Event" class="navbar-brand" runat="server" />
                <%-- <a ID="signout" runat="server" class="navbar-brand">Sign Out</a>--%>
            </div>
        </nav>

        <div class="container">
            <div class="row">
                <div class="col-md-6">
                    <div class="container">
                        <h3 style="float: right;">Create Your Pet. Or... </h3>
                    </div>
                    <div class="container" style="margin-top: 20%;">
                        <%--<form runat="server" id="form2" method="post" role="form" style="display: block;">--%>
                        <div class="form-group">
                            <asp:TextBox runat="server" type="text" name="petname" ID="petname" class="form-control" placeholder="Name Your Pet!" required="true" />
                        </div>
                        <div class="form-group">
                            <asp:Button Text="Create Pet" runat="server" type="submit" name="login-submit" ID="button1" class="form-control btn btn-danger" />
                        </div>
                        <%--</form>--%>
                    </div>
                </div>
                <div class="col-md-6">
                    <div class="row">
                        <div class="col-md-12"></div>
                        <div class="container">
                            <h3 style="float: left;">...Manage Your Pets.</h3>
                        </div>
                        <div class="row" style="margin-top: 11.5%;">
                            <% Dim cursor = Available_Pets2() %>
                            <% Dim count As Integer = 0 %>
                            <% If cursor IsNot Nothing Then %>
                            <% For each pet In cursor %>
                            <% If (pet.PlayerName = Session("Auth").ToString()) Then %>
                            <% count = count + 1 %>

                            <div class="container col-md-3">
                                <div class="card" style="width: 8rem;">
                                    <a href='<%= pet.MonsterName %>'><img class="card-img-top" src="Images/Kuriten.gif" alt="Kuriten" /></a>
                                    <div class="card-body">
                                        <h5 class="card-title" style="text-align: center;"><%=pet.MonsterName%></h5>
                                    </div>
                                </div>
                            </div>

                            <% End If %>
                            <% Next %>
                            <% End If %>
                            <% If (count = 0) Then %>
                            <div class="container">
                            <h3>
                                <asp:Label ID="Registeredpets" class="badge badge-danger" Text="You Don't Have Any Pets!" runat="server" />
                            </h3>
                            </div>
                            <% End If %>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </form>
</body>
</html>
