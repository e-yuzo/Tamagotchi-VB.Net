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
    'Public Property Id As ObjectId
    'Public Property PlayerName As String
    'Public Property MonsterName As String
    'Public Property State As String
    'Public Property Hunger As Integer
    'Public Property Happyness As Integer
    'Public Property Health As Integer
    Protected Sub Create_Pet(ByVal sender As Object, ByVal e As System.EventArgs) Handles button1.Click
        Try
            Dim collection = New DatabaseConnection().GetPetCollection()
            Dim minigameCollection = New DatabaseConnection().GetMinigameCollection()
            Dim petQuery = Query.And(Query.EQ("PlayerName", Session("Auth").ToString()), Query.EQ("MonsterName", petname.Text))
            Dim doc = collection.FindOne(petQuery)
            'Dim _monsterDB As Monster = Nothing
            'If (doc IsNot Nothing) Then
            '    _monsterDB = New Monster(doc("_id"), doc("PlayerName"), doc("MonsterName"), doc("State"), doc("Hunger"), doc("Happyness"), doc("Health"), doc("LastTimeState"), doc("LastSleep"), doc("LastShower"), doc("InitialTime"), doc("WokeUp"), doc("DropOff"))
            'End If
            Dim time = DateTime.UtcNow
            Dim _monster = New Monster(New ObjectId(), Session("Auth").ToString(), petname.Text, "Normal", 0, 100, 100, time, time, time, time, time, time)
            'With {.PlayerName = Session("Auth").ToString(), .MonsterName = petname.Text, .State = "Normal", .LastTimeState = DateTime.UtcNow, .Happyness = 100, .Health = 100, .Hunger = 0}
            'Dim _monster As new Monster(parameters) //constructor and setters and getters and private attributes.
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

    Protected Sub Image_Event(ByVal sender As Object, ByVal e As System.EventArgs)
        'Response.Redirect("TamagotchiPage.aspx", False)
    End Sub

    Protected Function Available_Pets2() As MongoCursor(Of BsonDocument)
        Dim collection = New DatabaseConnection().GetPetCollection()
        Dim _monster = collection.FindAll()
        Return _monster
    End Function
End Class