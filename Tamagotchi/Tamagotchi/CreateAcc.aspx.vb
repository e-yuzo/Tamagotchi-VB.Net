Imports MongoDB.Bson
Imports MongoDB.Driver
Imports MongoDB.Driver.Builders

Public Class CreateAcc
    Inherits System.Web.UI.Page

    Protected WithEvents username As System.Web.UI.WebControls.TextBox
    Protected WithEvents password As System.Web.UI.WebControls.TextBox
    Protected WithEvents button1 As System.Web.UI.WebControls.Button

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

    Protected Sub CreateAcc_Event(ByVal sender As Object, ByVal e As System.EventArgs) Handles button1.Click
        Try
            Dim collection = New DatabaseConnection().GetPlayerCollection()
            Dim _player = New Player() With {.Name = username.Text, .Password = password.Text}
            Dim userQuery = Query.EQ("Name", username.Text)
            Dim _playerDB As Player = collection.FindOne(userQuery)
            If _playerDB Is Nothing Then
                collection.Insert(_player)
                Session("Auth") = ""
                Response.Redirect("Login.aspx", False)
            Else
                MsgBox("Username Already Exists.")
            End If
        Catch ex As Exception
            MsgBox("Error : " & ex.Message)
        End Try
    End Sub

    Protected Sub Signin_Link(ByVal sender As Object, ByVal e As System.EventArgs)
        Session("Auth") = ""
        Response.Redirect("Login.aspx", False)
    End Sub
End Class