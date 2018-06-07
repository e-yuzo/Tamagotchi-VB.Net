Imports MongoDB.Bson
Imports MongoDB.Driver
Imports MongoDB.Driver.Builders
Imports System.Threading
Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports System.Threading.Tasks
Imports System.Web.Security


Public Class TamagotchiPage
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If (Session("Auth") Is Nothing Or Session("Auth") = "") Then
            Response.Redirect("Login.aspx", False)
        End If
        Dim petname = Page.RouteData.Values("petname")
        Dim collection = New DatabaseConnection().GetPetCollection()
        Dim petQuery = Query.And(Query.EQ("PlayerName", Session("Auth").ToString()), Query.EQ("MonsterName", petname.ToString()))
        Dim doc = collection.FindOne(petQuery)
        Dim _pet = New Monster(doc("_id"), doc("PlayerName"), doc("MonsterName"), doc("State"), doc("Hunger"), doc("Happyness"), doc("Health"), doc("LastTimeState"), doc("LastSleep"), doc("LastShower"), doc("InitialTime"), doc("WokeUp"), doc("DropOff"))
        If (Not Page.IsPostBack) Then
            Disable_GameButtons(True)
            closemodal.Attributes("disabled") = "disabled"
            Timer2.Enabled = False
            Try
                Update_Panels(_pet)
                _pet.Update(True)
                collection.Save(New Utils().MonsterClassToBson(_pet))
            Catch ex As Exception
                MsgBox("Error : " & ex.Message)
            End Try
        End If
        If (_pet.GetState = "Dead") Then
            Disable_ActionButtons(True)
            Timer1.Enabled = False
            topImage.Attributes.Remove("src")
            topImage.Attributes.Remove("alt")
        End If
        'src="Images/Normal.jpg"
        belowimage.Attributes("src") = "Images/" + _pet.GetState.ToString() + ".jpg"
        'test if pet is dead Here STOP TIMER
    End Sub

    Protected Sub Timer1_Tick(ByVal sender As Object, ByVal e As System.EventArgs) Handles Timer1.Tick
        Try
            Dim petname = Page.RouteData.Values("petname")
            Dim collection = New DatabaseConnection().GetPetCollection()
            Dim petQuery = Query.And(Query.EQ("PlayerName", Session("Auth").ToString()), Query.EQ("MonsterName", petname.ToString()))
            Dim doc = collection.FindOne(petQuery)
            Dim _pet = New Monster(doc("_id"), doc("PlayerName"), doc("MonsterName"), doc("State"), doc("Hunger"), doc("Happyness"), doc("Health"), doc("LastTimeState"), doc("LastSleep"), doc("LastShower"), doc("InitialTime"), doc("WokeUp"), doc("DropOff"))
            _pet.Update(False)

            Update_Panels(_pet)

            collection.Save(New Utils().MonsterClassToBson(_pet))
        Catch ex As Exception
            MsgBox("Error : " & ex.Message)
        End Try
        'Timer1.Enabled = True
    End Sub

    Protected Sub DeletePet_Event(ByVal sender As Object, ByVal e As System.EventArgs)
        'Timer1.Enabled = False
        Try
            Dim petname = Page.RouteData.Values("petname")
            Dim collection = New DatabaseConnection().GetPetCollection()
            Dim minigameCollection = New DatabaseConnection().GetMinigameCollection()
            Dim petQuery = Query.And(Query.EQ("PlayerName", Session("Auth").ToString()), Query.EQ("MonsterName", petname.ToString()))
            'Dim _monster = collection.FindOne(petQuery)
            'Dim _minigame = minigameCollection.FindOne(petQuery)
            collection.Remove(petQuery)
            minigameCollection.Remove(petQuery)
            Response.Redirect("AccountInfo.aspx", False)
        Catch ex As Exception
            MsgBox("Error : " & ex.Message)
        End Try
    End Sub

    Protected Sub SignOut_Event(ByVal sender As Object, ByVal e As System.EventArgs)
        Session("Auth") = ""
        Response.Redirect("Login.aspx", False)
    End Sub

    'Game Buttons
    Protected Sub Game_Tick(ByVal sender As Object, ByVal e As System.EventArgs) Handles Timer2.Tick
        'MsgBox("4")
        Disable_GameButtons(True)
        Timer2.Enabled = False
        'MsgBox(Session("Sequence").ToString())
        Dim count As Integer = Int(Session("Count"))
        Dim score As Integer = Int(Session("Score"))
        Dim sequence As String = Session("Sequence").ToString()
        If (count < score) Then
            'MsgBox(sequence(count).ToString())
            If (sequence(count).ToString() = "1") Then
                bluebutton.Attributes.Remove("disabled")
                'bluebutton.Attributes("style") = "float:right;margin-top:9%;background-color:#0097FF;"
                'bluebutton.Attributes("style") = "float:right;margin-top:9%;"
            ElseIf (sequence(count).ToString() = "2") Then
                redbutton.Attributes.Remove("disabled")
                'redbutton.Attributes("style") = "margin-top:-10%;background-color:#FF0000;"
                'redbutton.Attributes("style") = "margin-top:-10%;"
            ElseIf (sequence(count).ToString() = "3") Then
                yellowbutton.Attributes.Remove("disabled")
                'yellowbutton.Attributes("style") = "margin-bottom:-10%;background-color:#D8FF00;"
                ' yellowbutton.Attributes("style") = "margin-bottom:-10%;"
            ElseIf (sequence(count).ToString() = "4") Then
                greenbutton.Attributes.Remove("disabled")
                'greenbutton.Attributes("style") = "margin-top:9%;background-color:#00FF1F;"
                'greenbutton.Attributes("style") = "margin-top:9%;"
            End If
            Session("Count") = (Int(Session("Count")) + 1).ToString()
            Timer2.Enabled = True
        Else
            If (Session("Sequence").ToString() = "") Then
                Disable_GameButtons(True)
            Else
                Disable_GameButtons(False)
            End If
        End If
    End Sub
    'bleu red yellow green
    Protected Sub YellowButton_Event(ByVal sender As Object, ByVal e As System.EventArgs) Handles yellowbutton.Click
        Dim sequence As String = Session("Sequence").ToString()
        Session("UserSequence") = Session("UserSequence").ToString() + "3"

        Dim userSequence As String = Session("UserSequence")
        If (sequence.Length() <= userSequence.Length()) Then
            If (sequence <> userSequence) Then
                scorelabel.Text = "You Lost."
                Disable_GameButtons(True)
            ElseIf (sequence = userSequence) Then
                Next_Stage()
            End If
        End If
    End Sub

    Protected Sub BlueButton_Event(ByVal sender As Object, ByVal e As System.EventArgs) Handles bluebutton.Click
        Dim sequence As String = Session("Sequence").ToString()
        Session("UserSequence") = Session("UserSequence").ToString() + "1"
        Dim userSequence As String = Session("UserSequence")
        If (sequence.Length() <= userSequence.Length()) Then
            If (sequence <> userSequence) Then
                scorelabel.Text = "You Lost."
                Disable_GameButtons(True)
            ElseIf (sequence = userSequence) Then
                Next_Stage()
            End If
        End If
    End Sub

    Protected Sub RedButton_Event(ByVal sender As Object, ByVal e As System.EventArgs) Handles redbutton.Click
        Dim sequence As String = Session("Sequence").ToString()
        Session("UserSequence") = Session("UserSequence").ToString() + "2"
        Dim userSequence As String = Session("UserSequence")
        If (sequence.Length() <= userSequence.Length()) Then
            If (sequence <> userSequence) Then
                scorelabel.Text = "You Lost."
                Disable_GameButtons(True)
            ElseIf (sequence = userSequence) Then
                Next_Stage()
            End If
        End If
    End Sub

    Protected Sub GreenButton_Event(ByVal sender As Object, ByVal e As System.EventArgs) Handles greenbutton.Click
        Dim sequence As String = Session("Sequence").ToString()
        Session("UserSequence") = Session("UserSequence").ToString() + "4"
        Dim userSequence As String = Session("UserSequence")
        If (sequence.Length() <= userSequence.Length()) Then
            If (sequence <> userSequence) Then
                scorelabel.Text = "You Lost."
                Disable_GameButtons(True)
            ElseIf (sequence = userSequence) Then
                Next_Stage()
            End If
        End If
    End Sub

    Protected Sub Game_Event(ByVal sender As Object, ByVal e As System.EventArgs) Handles gamebutton.Click
        closemodal.Attributes("disabled") = "disabled"
        endgame.Attributes.Remove("disabled")
        Session("Score") = "0"
        Session("Sequence") = ""
        Session("UserSequence") = ""
        Session("Count") = "0"
        Next_Stage()
    End Sub

    Protected Sub EndGame_Event(ByVal sender As Object, ByVal e As System.EventArgs) Handles endgame.Click
        endgame.Attributes("disabled") = "disabled"
        Dim score = Int(Session("Score"))
        Session("Score") = "0"
        Session("Sequence") = ""
        Session("UserSequence") = ""
        Session("Count") = "0"
        Dim _pet As Monster = GetQueriedPet()
        Dim _minigame As Minigame = GetQueriedMinigame()
        _pet.Game_Action(score)
        _minigame.SetMaxScore(Int(scorelabel.Text))
        _minigame.SetNumberOfGames()
        Dim collection = New DatabaseConnection().GetPetCollection()
        Dim minigameCollection = New DatabaseConnection().GetMinigameCollection()
        collection.Save(New Utils().MonsterClassToBson(_pet))
        minigameCollection.Save(New Utils().MinigameClassToBson(_minigame))
        closemodal.Attributes.Remove("disabled")
        Disable_GameButtons(True)
    End Sub

    Private Sub Next_Stage()
        Disable_GameButtons(True)
        Generate_RandomColorSequence()
        Session("Count") = "0"
        Session("UserSequence") = ""
        scorelabel.Text = Session("Score").ToString()
        Session("Score") = (Int(Session("Score")) + 1).ToString()
        Timer2.Enabled = True
    End Sub

    Private Sub Generate_RandomColorSequence()
        Dim random As Integer = New System.Random().Next(1, 5)
        Dim sequence As String = Session("Sequence").ToString()
        If (sequence.Length() <> 0) Then
            'MsgBox(Int(Session("Score")).ToString())
            'MsgBox((sequence(Int(Session("Score")) - 1)).ToString())
            Dim lastRandom As String = sequence(Int(Session("Score")) - 1)
            While (random.ToString() = lastRandom)
                random = New System.Random().Next(1, 5)
            End While
        End If
        Session("Sequence") = sequence + random.ToString()
    End Sub

    Private Sub Disable_GameButtons(state As Boolean)
        If (state = True) Then
            yellowbutton.Attributes("disabled") = "disabled"
            greenbutton.Attributes("disabled") = "disabled"
            redbutton.Attributes("disabled") = "disabled"
            bluebutton.Attributes("disabled") = "disabled"
        Else
            yellowbutton.Attributes.Remove("disabled")
            greenbutton.Attributes.Remove("disabled")
            redbutton.Attributes.Remove("disabled")
            bluebutton.Attributes.Remove("disabled")
        End If
    End Sub

    'Action Buttons
    Protected Sub CureButton_Event(ByVal sender As Object, ByVal e As System.EventArgs) Handles curebutton.Click
        Dim _pet = GetQueriedPet()
        _pet.Cure_Action()
        Dim collection = New DatabaseConnection().GetPetCollection()
        collection.Save(New Utils().MonsterClassToBson(_pet))
    End Sub

    Protected Sub BathButton_Event(ByVal sender As Object, ByVal e As System.EventArgs) Handles bathbutton.Click
        Dim _pet = GetQueriedPet()
        _pet.Bath_Action()
        Dim collection = New DatabaseConnection().GetPetCollection()
        collection.Save(New Utils().MonsterClassToBson(_pet))
    End Sub

    Protected Sub LightButton_Event(ByVal sender As Object, ByVal e As System.EventArgs) Handles lightsbutton.Click
        Dim _pet = GetQueriedPet()
        _pet.Sleep_Action()
        If (_pet.GetState <> "Sleeping") Then
            'Disable_ActionButtons(False)
            lightsbutton.Attributes("style") = "background-image: url('Images/light-on.png'); background-size: 45px 45px; background-repeat: no-repeat; background-position: center;"
            lightsbutton.Attributes("class") = "btn btn-warning btn-circle btn-xl"
        Else
            'Disable_ActionButtons(True)
            'lightsbutton.Attributes.Remove("disabled")
            lightsbutton.Attributes("style") = "background-image: url('Images/light-off.png'); background-size: 45px 45px; background-repeat: no-repeat; background-position: center;"
            lightsbutton.Attributes("class") = "btn btn-circle btn-xl"
        End If
        Dim collection = New DatabaseConnection().GetPetCollection()
        collection.Save(New Utils().MonsterClassToBson(_pet))
    End Sub

    Protected Sub EatButton_Event(ByVal sender As Object, ByVal e As System.EventArgs) Handles eatbutton.Click
        Dim _pet As Monster = GetQueriedPet()
        _pet.Eat_Action()
        Dim collection = New DatabaseConnection().GetPetCollection()
        collection.Save(New Utils().MonsterClassToBson(_pet)) 'h########################################################
    End Sub

    Private Sub Disable_ActionButtons(bool As Boolean)
        If bool = True Then
            eatbutton.Attributes("disabled") = "disabled"
            lightsbutton.Attributes("disabled") = "disabled"
            curebutton.Attributes("disabled") = "disabled"
            gamebutton.Attributes("disabled") = "disabled"
            bathbutton.Attributes("disabled") = "disabled"
        Else
            eatbutton.Attributes.Remove("disabled")
            lightsbutton.Attributes.Remove("disabled")
            curebutton.Attributes.Remove("disabled")
            gamebutton.Attributes.Remove("disabled")
            bathbutton.Attributes.Remove("disabled")
        End If
    End Sub

    Private Function GetQueriedPet() As Monster
        Dim petname = Page.RouteData.Values("petname")
        Dim collection = New DatabaseConnection().GetPetCollection()
        Dim petQuery = Query.And(Query.EQ("PlayerName", Session("Auth").ToString()), Query.EQ("MonsterName", petname.ToString()))
        Dim doc = collection.FindOne(petQuery)
        Dim _pet = New Monster(doc("_id"), doc("PlayerName"), doc("MonsterName"), doc("State"), doc("Hunger"), doc("Happyness"), doc("Health"), doc("LastTimeState"), doc("LastSleep"), doc("LastShower"), doc("InitialTime"), doc("WokeUp"), doc("DropOff"))
        Return _pet
    End Function

    Private Function GetQueriedMinigame() As Minigame
        Dim petname = Page.RouteData.Values("petname")
        Dim collection = New DatabaseConnection().GetMinigameCollection()
        Dim minigameQuery = Query.And(Query.EQ("PlayerName", Session("Auth").ToString()), Query.EQ("MonsterName", petname.ToString()))
        Dim doc = collection.FindOne(minigameQuery)
        Dim _minigame = New Minigame(doc("_id"), doc("PlayerName"), doc("MonsterName"), doc("NumberOfGames"), doc("MaxScore"))
        Return _minigame
    End Function

    Private Sub Update_Panels(_pet As Monster)
        hungerBar.Attributes("style") = "width:" + (Int(_pet.GetHunger).ToString()) + "%;"
        hungerlabel.Text = _pet.GetHunger.ToString()
        healthBar.Attributes("style") = "width:" + (Int(_pet.GetHealth).ToString()) + "%;"
        healthlabel.Text = _pet.GetHealth.ToString()
        happynessBar.Attributes("style") = "width:" + (Int(_pet.GetHappyness).ToString()) + "%;"
        happylabel.Text = _pet.GetHappyness.ToString()
        statelabel.Text = _pet.GetState
        Label1.Text = DateTime.Now.ToLongTimeString()
        Change_Bars_Warnings(_pet.GetHealth, _pet.GetHunger, _pet.GetHappyness)
        'MsgBox("first")
    End Sub

    Private Sub Change_Bars_Warnings(health As Double, hunger As Double, happyness As Double)
        If (health < 15) Then
            healthBar.Attributes("class") = "progress-bar bg-danger"
        ElseIf (health < 60) Then
            healthBar.Attributes("class") = "progress-bar bg-warning"
        Else
            healthBar.Attributes("class") = "progress-bar bg-success"
        End If

        If (hunger > 85) Then
            hungerBar.Attributes("class") = "progress-bar bg-danger"
        ElseIf (hunger > 40) Then
            hungerBar.Attributes("class") = "progress-bar bg-warning"
        Else
            hungerBar.Attributes("class") = "progress-bar bg-success"
        End If

        If (happyness < 15) Then
            happynessBar.Attributes("class") = "progress-bar bg-danger"
        ElseIf (happyness < 60) Then
            happynessBar.Attributes("class") = "progress-bar bg-warning"
        Else
            happynessBar.Attributes("class") = "progress-bar bg-success"
        End If
    End Sub

End Class