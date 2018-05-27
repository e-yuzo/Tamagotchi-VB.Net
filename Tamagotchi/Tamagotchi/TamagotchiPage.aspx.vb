Imports MongoDB.Bson
Imports MongoDB.Driver
Imports MongoDB.Driver.Builders
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
        If (Not Page.IsPostBack) Then
            Try
                Dim petname = Page.RouteData.Values("petname")

                Dim collection = New DatabaseConnection().GetPetCollection()
                Dim petQuery = Query.And(Query.EQ("PlayerName", Session("Auth").ToString()), Query.EQ("MonsterName", petname.ToString()))
                Dim _pet = collection.FindOne(petQuery)

                Update_Panels(_pet)

                _pet.Update(True)

                collection.Save(_pet)
            Catch ex As Exception
                MsgBox("Error : " & ex.Message)
            End Try
        End If
    End Sub

    Protected Sub Timer1_Tick(ByVal sender As Object, ByVal e As System.EventArgs) Handles Timer1.Tick
        Try
            Dim petname = Page.RouteData.Values("petname")
            Dim collection = New DatabaseConnection().GetPetCollection()
            Dim petQuery = Query.And(Query.EQ("PlayerName", Session("Auth").ToString()), Query.EQ("MonsterName", petname.ToString()))
            Dim _pet = collection.FindOne(petQuery)

            _pet.Update(False)

            Update_Panels(_pet)

            collection.Save(_pet)
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
            Dim petQuery = Query.And(Query.EQ("PlayerName", Session("Auth").ToString()), Query.EQ("MonsterName", petname.ToString()))
            Dim _monster = collection.FindOne(petQuery)
            collection.Remove(petQuery)
            Response.Redirect("AccountInfo.aspx", False)
        Catch ex As Exception
            MsgBox("Error : " & ex.Message)
        End Try
    End Sub

    Protected Sub SignOut_Event(ByVal sender As Object, ByVal e As System.EventArgs)
        Session("Auth") = ""
        Response.Redirect("Login.aspx", False)
    End Sub

    Protected Sub GameButton_Event(ByVal sender As Object, ByVal e As System.EventArgs) Handles gamebutton.Click
        'wuts gon' happen
    End Sub

    Protected Sub CureButton_Event(ByVal sender As Object, ByVal e As System.EventArgs) Handles curebutton.Click
        Dim _pet = GetQueriedPet()
        _pet.Cure_Action()
        Dim collection = New DatabaseConnection().GetPetCollection()
        collection.Save(_pet)
    End Sub

    Protected Sub SleepButton_Event(ByVal sender As Object, ByVal e As System.EventArgs) Handles bedbutton.Click
        Dim _pet = GetQueriedPet()
        _pet.Sleep_Action()
        Dim collection = New DatabaseConnection().GetPetCollection()
        collection.Save(_pet)
    End Sub

    Protected Sub BathButton_Event(ByVal sender As Object, ByVal e As System.EventArgs) Handles bathbutton.Click
        Dim _pet = GetQueriedPet()
        _pet.Bath_Action()
        Dim collection = New DatabaseConnection().GetPetCollection()
        collection.Save(_pet)
    End Sub
    'Style="background-image: url('Images/light-on.png'); background-size: 45px 45px; background-repeat: no-repeat; background-position: center;
    Protected Sub LightButton_Event(ByVal sender As Object, ByVal e As System.EventArgs) Handles lightsbutton.Click
        Dim _pet = GetQueriedPet()
        _pet.Lights_Action()
        If (_pet.State <> "Sleeping") Then
            lightsbutton.Attributes("style") = "background-image: url('Images/light-on.png'); background-size: 45px 45px; background-repeat: no-repeat; background-position: center;"
            lightsbutton.Attributes("class") = "btn btn-warning btn-circle btn-xl"
        Else
            lightsbutton.Attributes("style") = "background-image: url('Images/light-off.png'); background-size: 45px 45px; background-repeat: no-repeat; background-position: center;"
            lightsbutton.Attributes("class") = "btn btn-circle btn-xl"
        End If
        Dim collection = New DatabaseConnection().GetPetCollection()
        collection.Save(_pet)
    End Sub

    Protected Sub EatButton_Event(ByVal sender As Object, ByVal e As System.EventArgs) Handles eatbutton.Click
        Dim _pet As Monster = GetQueriedPet()
        _pet.Eat_Action()
        Dim collection = New DatabaseConnection().GetPetCollection()
        collection.Save(_pet)
    End Sub

    Protected Function GetQueriedPet() As Monster
        Dim petname = Page.RouteData.Values("petname")
        Dim collection = New DatabaseConnection().GetPetCollection()
        Dim petQuery = Query.And(Query.EQ("PlayerName", Session("Auth").ToString()), Query.EQ("MonsterName", petname.ToString()))
        Dim _pet = collection.FindOne(petQuery)
        Return _pet
    End Function

    Protected Sub Update_Panels(_pet As Monster)
        hungerBar.Attributes("style") = "width:" + (Int(_pet.Hunger).ToString()) + "%;"
        hungerlabel.Text = _pet.Hunger.ToString()
        healthBar.Attributes("style") = "width:" + (Int(_pet.Health).ToString()) + "%;"
        healthlabel.Text = _pet.Health.ToString()
        happynessBar.Attributes("style") = "width:" + (Int(_pet.Happyness).ToString()) + "%;"
        happylabel.Text = _pet.Happyness.ToString()
        statelabel.Text = _pet.State
        Label1.Text = DateTime.Now.ToLongTimeString()
        Change_Bars_Warnings(_pet.Health, _pet.Hunger, _pet.Happyness)
        'MsgBox("first")
    End Sub

    Protected Sub Change_Bars_Warnings(health As Double, hunger As Double, happyness As Double)
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

    Private Sub PlaySound(ByVal file As String)
        Dim Mytone As New System.Media.SoundPlayer
        Mytone.SoundLocation = file
        Mytone.Load()
        Mytone.Play()
    End Sub
End Class