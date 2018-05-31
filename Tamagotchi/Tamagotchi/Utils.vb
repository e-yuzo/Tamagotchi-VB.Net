Imports MongoDB.Bson

Public Class Utils
    Public Sub PlaySound(ByVal file As String)
        Dim Mytone As New System.Media.SoundPlayer
        Mytone.SoundLocation = file
        Mytone.Load()
        Mytone.Play()
    End Sub

    Public Function PlayerClassToBson(p As Player) As BsonDocument
        Dim doc As BsonDocument = New BsonDocument()
        doc.Add("_id", p.GetId()).Add("Name", p.GetName()).Add("Password", p.GetPassword())
        Return doc
    End Function

    Public Function MonsterClassToBson(p As Monster) As BsonDocument
        Dim doc As BsonDocument = New BsonDocument()
        doc.Add("_id", p.GetId()).Add("PlayerName", p.GetPlayerName()).Add("MonsterName", p.GetMonsterName()) _
           .Add("State", p.GetState()).Add("Hunger", p.GetHunger()).Add("Happyness", p.GetHappyness()).Add("Health", p.GetHealth()) _
           .Add("LastTimeState", p.GetLastTimeState()).Add("LastSleep", p.GetLastSleep()).Add("LastShower", p.GetLastShower()).Add("InitialTime", p.GetInitialTime()) _
           .Add("WokeUp", p.GetWokeUp()).Add("DropOff", p.GetDropOff())
        Return doc
    End Function

    Public Function MinigameClassToBson(p As Minigame) As BsonDocument
        Dim doc As BsonDocument = New BsonDocument()
        doc.Add("_id", p.GetId()).Add("PlayerName", p.GetPlayerName()).Add("MonsterName", p.GetMonsterName()) _
           .Add("NumberOfGames", p.GetNumberOfGames()).Add("MaxScore", p.GetMaxScore())
        Return doc
    End Function
End Class
