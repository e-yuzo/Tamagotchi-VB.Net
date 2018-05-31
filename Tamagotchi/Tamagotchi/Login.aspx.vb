Imports MongoDB.Bson
Imports MongoDB.Driver
Imports MongoDB.Driver.Builders
Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports System.Threading.Tasks
Imports System.Web.Security

Public Class Login
    Inherits System.Web.UI.Page

    Protected WithEvents username As System.Web.UI.WebControls.TextBox
    Protected WithEvents password As System.Web.UI.WebControls.TextBox
    Protected WithEvents button1 As System.Web.UI.WebControls.Button

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If (Session("Auth") IsNot Nothing And Session("Auth") <> "") Then
            Response.Redirect("AccountInfo.aspx", False)
        End If
    End Sub

    Protected Sub Login_Event(ByVal sender As Object, ByVal e As System.EventArgs) Handles button1.Click
        Try
            Dim collection = New DatabaseConnection().GetPlayerCollection()
            Dim loginQuery = Query.And(Query.EQ("Name", username.Text), Query.EQ("Password", password.Text))
            Dim doc = collection.FindOne(loginQuery)
            Dim _player As Player = Nothing
            If (doc IsNot Nothing) Then
                _player = New Player(doc("_id"), doc("Name"), doc("Password"))
            End If

            If _player Is Nothing Then
                MsgBox("Wrong Username or Password.")
            Else
                Session("Auth") = _player.GetName()
                Response.Redirect("AccountInfo.aspx", False)
            End If

        Catch ex As Exception
            MsgBox("Error : " & ex.Message)
        End Try
    End Sub
End Class