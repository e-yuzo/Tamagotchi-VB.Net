Imports MongoDB.Bson
Imports MongoDB.Driver
Imports MongoDB.Driver.Builders

Public Class AccountInfo
    Inherits System.Web.UI.Page

    Protected WithEvents petname As System.Web.UI.WebControls.TextBox
    Protected WithEvents button1 As System.Web.UI.WebControls.Button

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If (Session("Auth") Is Nothing Or Session("Auth") = "") Then
            Response.Redirect("Login.aspx", False)
        End If
    End Sub

    Protected Sub Create_Pet(ByVal sender As Object, ByVal e As System.EventArgs) Handles button1.Click
        Try
            Dim collection = New DatabaseConnection().GetPetCollection()
            Dim minigameCollection = New DatabaseConnection().GetMinigameCollection()
            Dim petQuery = Query.And(Query.EQ("PlayerName", Session("Auth").ToString()), Query.EQ("MonsterName", petname.Text))
            Dim doc = collection.FindOne(petQuery)
            Dim time = DateTime.UtcNow
            Dim _monster = New Monster(New ObjectId(), Session("Auth").ToString(), petname.Text, "Normal", 0, 100, 100, time, time, time, time, time, time)
            If doc Is Nothing Then
                Dim minidoc As Minigame = New Minigame(New ObjectId(), Session("Auth").ToString(), petname.Text, 0, 0)
                minigameCollection.Insert(New Utils().MinigameClassToBson(minidoc))
                collection.Insert(New Utils().MonsterClassToBson(_monster))
                Response.Redirect("AccountInfo.aspx", False)
            Else
                MsgBox("Pet Name Already Exists.")
            End If
        Catch ex As Exception
            MsgBox("Error : " & ex.Message)
        End Try
    End Sub

    Protected Sub SignOut_Event(ByVal sender As Object, ByVal e As System.EventArgs)
        Session("Auth") = ""
        Response.Redirect("Login.aspx", False)
    End Sub

    Protected Function Available_Pets2() As MongoCursor(Of BsonDocument)
        Dim collection = New DatabaseConnection().GetPetCollection()
        Dim _monster = collection.FindAll()
        Return _monster
    End Function
End Class