<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="Login.aspx.vb" Inherits="Tamagotchi.Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <link rel="stylesheet" href="Content/bootstrap.min.css" />
    <script src="//maxcdn.bootstrapcdn.com/bootstrap/3.3.0/js/bootstrap.min.js"></script>
    <script src="//code.jquery.com/jquery-1.11.1.min.js"></script>
    <title>Login Page</title>
</head>
<body>
    <form runat="server" id="form1">
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
                <a class="navbar-brand" href="CreateAcc.aspx">Create Account</a>
            </div>
        </nav>
        <div class="container" style="margin-top: 5%;">
            <div class="row">
                <div class="col-md-4"></div>
                <div class="col-md-4">
                    <div class="panel panel-login">
                        <div class="container">
                            <h2>Please Log In</h2>
                        </div>
                        <hr />
                        <div class="panel-body">
                            <div class="row">
                                <div class="col-lg-12">

                                    <div class="form-group">
                                        <asp:TextBox runat="server" type="text" name="username" ID="username" class="form-control" placeholder="Username" required="true" />
                                    </div>
                                    <div class="form-group">
                                        <asp:TextBox runat="server" type="password" name="password" ID="password" class="form-control" placeholder="Password" required="true" />
                                    </div>
                                    <div class="form-group">
                                        <div class="row">
                                            <div class="col-lg-12">
                                                <asp:Button Text="Log In" runat="server" type="submit" name="login-submit" ID="button1" class="form-control btn btn-primary" />
                                            </div>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <div class="row">
                                            <div class="col-lg-12">
                                                <div class="text-center">
                                                    <a href="CreateAcc.aspx">Create Account.</a>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="col-md-4"></div>
            </div>
        </div>
    </form>
    <script src="Content/jquery-3.3.1.slim.min.js"></script>
    <script src="Content/popper.min.js"></script>
    <script src="Content/bootstrap.min.js"></script>
</body>
</html>
