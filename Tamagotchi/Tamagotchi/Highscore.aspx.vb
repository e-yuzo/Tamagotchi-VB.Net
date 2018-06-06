Imports MongoDB.Bson
Imports MongoDB.Driver

Public Class Highscore
    Inherits System.Web.UI.Page

    Protected WithEvents tableData As System.Web.UI.WebControls.Literal

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

    Protected Function getAllPets() As MongoCursor(Of BsonDocument)
        Dim collection = New DatabaseConnection().GetPetCollection()
        Dim _monster = collection.FindAll()

        Return _monster
    End Function

    Protected Function getAllMinigames() As MongoCursor(Of BsonDocument)
        Dim collection = New DatabaseConnection().GetMinigameCollection()
        Dim _minigame = collection.FindAll()
        Return _minigame
    End Function

    Protected Sub Leave_Event(ByVal sender As Object, ByVal e As System.EventArgs)
        If (Session("Auth") Is Nothing Or Session("Auth") = "") Then
            Response.Redirect("Login.aspx", False)
        Else
            Response.Redirect("AccountInfo.aspx", False)
        End If
    End Sub
End Class