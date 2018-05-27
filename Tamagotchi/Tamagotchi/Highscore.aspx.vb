Imports MongoDB.Bson
Imports MongoDB.Driver
Imports MongoDB.Driver.Builders
Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports System.Threading.Tasks
Imports System.Web.Security
Imports System.Web.Routing

Public Class Highscore
    Inherits System.Web.UI.Page

    Protected WithEvents tableData As System.Web.UI.WebControls.Literal

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

    'Protected Sub Construct_Table(ByVal sender As Object, ByVal e As System.EventArgs)

    'End Sub
    Public Function getAllPets() As MongoCursor(Of Monster)
        Dim collection = New DatabaseConnection().GetPetCollection()
        Dim _monster = collection.FindAll()
        Return _monster
    End Function

    Protected Sub Leave_Event(ByVal sender As Object, ByVal e As System.EventArgs)
        If (Session("Auth") Is Nothing Or Session("Auth") = "") Then
            Response.Redirect("Login.aspx", False)
        Else
            Response.Redirect("AccountInfo.aspx", False)
        End If
    End Sub
End Class