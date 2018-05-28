<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="TamagotchiPage.aspx.vb" Inherits="Tamagotchi.TamagotchiPage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <link rel="stylesheet" href="Content/bootstrap.min.css" />
    <link rel="stylesheet" href="Style/circlebutton.css" />
    <title>Tamagotchi</title>
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
                <asp:LinkButton ID="deletepet" Text="Shoo Away!" Style="color: white;" OnClick="DeletePet_Event" class="navbar-brand btn btn-danger" runat="server" />
                <asp:LinkButton ID="signout" Text="Sign Out" OnClick="SignOut_Event" class="navbar-brand" runat="server" />
            </div>
        </nav>

        <br />
        <div class="row">
            <div class="col-md-9">
                <asp:ScriptManager ID="ScriptManager1" runat="server" />
                <div>
                    <asp:Timer ID="Timer1" OnTick="Timer1_Tick" runat="server" Interval="2000">
                    </asp:Timer>
                </div>
                <asp:UpdatePanel ID="UpdatePanel1" UpdateMode="Conditional" runat="server">
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="Timer1" EventName="Tick" />
                    </Triggers>
                    <ContentTemplate>
                        <div class="container">
                            <h6>Hunger:
                                <asp:Label ID="hungerlabel" Text="text" runat="server" />%</h6>
                            <div class="progress">
                                <div class="progress-bar bg-warning" id="hungerBar" runat="server"></div>
                            </div>
                        </div>
                        <br />
                        <div class="container">
                            <h6>Health:
                                <asp:Label ID="healthlabel" Text="text" runat="server" />%</h6>
                            <div class="progress">
                                <div class="progress-bar bg-success" id="healthBar" runat="server"></div>
                            </div>
                        </div>
                        <br />
                        <div class="container">
                            <h6>Happyness:
                                <asp:Label ID="happylabel" Text="text" runat="server" />%</h6>
                            <div class="progress">
                                <div class="progress-bar bg-success" id="happynessBar" runat="server"></div>
                            </div>
                        </div>
                        <br />
                        <div class="container">
                            <h6>State:
                                <asp:Label ID="statelabel" Text="text" runat="server" /></h6>
                        </div>
                        <div class="container">
                            <img runat="server" src="Images/Fox.gif" alt="Kuriten" />
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>

            </div>
            <%--          <div class="col-md-1"></div>--%>
            <div class="col-md-2">
                <div class="container">
                    <asp:UpdatePanel ID="UpdatePanel2" UpdateMode="Conditional" runat="server">
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="Timer1" EventName="Tick" />
                        </Triggers>
                        <ContentTemplate>
                            <div class="panel panel-default" style="float: right;">
                                <h5>Time:
                                <asp:Label ID="Label1" runat="server" Text=""></asp:Label>
                                </h5>
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                    <div class="panel panel-default" style="float: right;">
                        <div class="panel-body">
                            <asp:UpdatePanel ID="UpdatePanel4" runat="server" UpdateMode="Conditional">
                                <ContentTemplate>
                                    <asp:Button runat="server" ID="eatbutton" class="btn btn-warning btn-circle btn-xl" Style="background-image: url('Images/sushi.png'); background-size: 45px 45px; background-repeat: no-repeat; background-position: center;"></asp:Button>
                                </ContentTemplate>
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="eatbutton" EventName="Click" />
                                </Triggers>
                            </asp:UpdatePanel>
                            <br />
                            <asp:UpdatePanel ID="UpdatePanel3" runat="server" UpdateMode="Conditional">
                                <ContentTemplate>
                                    <asp:Button runat="server" ID="curebutton" class="btn btn-warning btn-circle btn-xl" Style="background-image: url('Images/syringes.png'); background-size: 45px 45px; background-repeat: no-repeat; background-position: center;"></asp:Button>
                                </ContentTemplate>
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="curebutton" EventName="Click" />
                                </Triggers>
                            </asp:UpdatePanel>
                            <br />
                            <asp:UpdatePanel ID="UpdatePanel5" runat="server" UpdateMode="Conditional">
                                <ContentTemplate>
                                    <asp:Button runat="server" ID="bathbutton" class="btn btn-warning btn-circle btn-xl" Style="background-image: url('Images/bathtub.png'); background-size: 45px 45px; background-repeat: no-repeat; background-position: center;"></asp:Button>
                                </ContentTemplate>
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="bathbutton" EventName="Click" />
                                </Triggers>
                            </asp:UpdatePanel>
                            <br />
                            <%--                            <asp:UpdatePanel ID="UpdatePanel6" runat="server" UpdateMode="Conditional">
                                <ContentTemplate>
                                    <asp:Button runat="server" ID="bedbutton" class="btn btn-warning btn-circle btn-xl" Style="background-image: url('Images/bed.png'); background-size: 45px 45px; background-repeat: no-repeat; background-position: center;"></asp:Button>
                                </ContentTemplate>
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="bedbutton" EventName="Click" />
                                </Triggers>
                            </asp:UpdatePanel>--%>
                            <br />
                            <asp:UpdatePanel ID="UpdatePanel7" runat="server" UpdateMode="Conditional">
                                <ContentTemplate>
                                    <asp:Button runat="server" ID="lightsbutton" class="btn btn-warning btn-circle btn-xl" Style="background-image: url('Images/light-on.png'); background-size: 45px 45px; background-repeat: no-repeat; background-position: center;"></asp:Button>
                                </ContentTemplate>
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="lightsbutton" EventName="Click" />
                                </Triggers>
                            </asp:UpdatePanel>
                            <br />
                            <asp:UpdatePanel ID="UpdatePanel8" runat="server" UpdateMode="Conditional">
                                <ContentTemplate>
                                    <asp:Button runat="server" ID="gamebutton" class="btn btn-warning btn-circle btn-xl" Style="background-image: url('Images/gameboy.png'); background-size: 45px 45px; background-repeat: no-repeat; background-position: center;" data-toggle="modal" data-target="#modalGame"></asp:Button>
                                </ContentTemplate>
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="gamebutton" EventName="Click" />
                                </Triggers>
                            </asp:UpdatePanel>
                            <%--Modal--%>
                            <div runat="server" class="modal" id="modalGame" tabindex="-1" role="dialog" aria-labelledby="Simon Says" aria-hidden="true" data-keyboard="false" data-backdrop="static">
                                <div class="modal-dialog modal-dialog-centered" role="document">
                                    <div class="modal-content">
                                        <div class="modal-header">
                                            <h5 class="modal-title" id="exampleModalCenterTitle">Simon Says</h5>
                                            <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                                <span aria-hidden="true">&times;</span>
                                            </button>
                                        </div>
                                        <div class="modal-body">
                                            <div class="row">
                                                <div class="col-md-4">
                                                    <asp:UpdatePanel ID="UpdatePanel6" runat="server" UpdateMode="Conditional">
                                                        <ContentTemplate>
                                                            <asp:Button runat="server" ID="bluebutton" class="btn btn-primary btn-circle btn-xl" Style="float: right; margin-top: 9%;"></asp:Button>
                                                        </ContentTemplate>
                                                        <Triggers>
                                                            <asp:AsyncPostBackTrigger ControlID="bluebutton" EventName="Click" />
                                                        </Triggers>
                                                    </asp:UpdatePanel>
                                                </div>
                                                <div class="col-md-4" style="text-align: center;">
                                                    <asp:UpdatePanel ID="UpdatePanel9" runat="server" UpdateMode="Conditional">
                                                        <ContentTemplate>
                                                            <asp:Button runat="server" ID="redbutton" class="btn btn-danger btn-circle btn-xl" Style="margin-top: -10%;"></asp:Button>
                                                        </ContentTemplate>
                                                        <Triggers>
                                                            <asp:AsyncPostBackTrigger ControlID="redbutton" EventName="Click" />
                                                        </Triggers>
                                                    </asp:UpdatePanel>

                                                    <asp:UpdatePanel ID="UpdatePanel10" runat="server" UpdateMode="Conditional">
                                                        <ContentTemplate>
                                                            <asp:Button runat="server" ID="yellowbutton" class="btn btn-warning btn-circle btn-xl" Style="margin-bottom: -10%;"></asp:Button>
                                                        </ContentTemplate>
                                                        <Triggers>
                                                            <asp:AsyncPostBackTrigger ControlID="yellowbutton" EventName="Click" />
                                                        </Triggers>
                                                    </asp:UpdatePanel>
                                                </div>
                                                <div class="col-md-4">
                                                    <asp:UpdatePanel ID="UpdatePanel11" runat="server" UpdateMode="Conditional">
                                                        <ContentTemplate>
                                                            <asp:Button runat="server" ID="greenbutton" class="btn btn-success btn-circle btn-xl" Style="margin-top: 9%;"></asp:Button>
                                                        </ContentTemplate>
                                                        <Triggers>
                                                            <asp:AsyncPostBackTrigger ControlID="greenbutton" EventName="Click" />
                                                        </Triggers>
                                                    </asp:UpdatePanel>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="modal-footer">
                                            <h5>Score: 
                                                <asp:Label Text="0" runat="server" /></h5>
                                        </div>
                                    </div>
                                    <%--Modal--%>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="col-md-1"></div>
        </div>
    </form>
    <script src="Content/jquery-3.3.1.slim.min.js"></script>
    <script src="Content/popper.min.js"></script>
    <script src="Content/bootstrap.min.js"></script>
</body>
</html>

