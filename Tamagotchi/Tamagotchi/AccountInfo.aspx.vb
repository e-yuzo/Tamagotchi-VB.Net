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
            Dim petQuery = Query.And(Query.EQ("PlayerName", Session("Auth").ToString()), Query.EQ("MonsterName", petname.Text))
            Dim _monsterDB = collection.FindOne(petQuery)
            Dim _monster = New Monster() With {.PlayerName = Session("Auth").ToString(), .MonsterName = petname.Text, .State = "Normal", .LastTimeState = DateTime.UtcNow, .Happyness = 100, .Health = 100, .Hunger = 0}
            'Dim _monster As new Monster(parameters) //constructor and setters and getters and private attributes.
            If _monsterDB Is Nothing Then
                collection.Insert(_monster)
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

    Public Function Available_Pets2() As MongoCursor(Of Monster)
        Dim collection = New DatabaseConnection().GetPetCollection()
        Dim _monster = collection.FindAll()
        Return _monster
    End Function
End Class