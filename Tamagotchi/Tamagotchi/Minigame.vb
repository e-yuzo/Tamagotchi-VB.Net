Imports MongoDB.Bson

Public Class Minigame
    Private Property Id As ObjectId
    Private Property PlayerName As String
    Private Property MonsterName As String
    Private Property NumberOfGames As Integer
    Private Property MaxScore As Integer

    Public Sub New(id As ObjectId, playerName As String, monsterName As String, numberOfGames As Integer, maxScore As Integer)
        Me.Id = id
        Me.PlayerName = playerName
        Me.MonsterName = monsterName
        Me.NumberOfGames = numberOfGames
        Me.MaxScore = maxScore
    End Sub

    Public Function GetId() As ObjectId
        Return Id
    End Function

    Public Function GetPlayerName() As String
        Return PlayerName
    End Function

    Public Function GetMonsterName() As String
        Return MonsterName
    End Function

    Public Function GetMaxScore() As Integer
        Return MaxScore
    End Function

    Public Sub SetMaxScore(score As Integer)
        If (score > MaxScore) Then
            Me.MaxScore = score
        End If
    End Sub

    Public Function GetNumberOfGames() As Integer
        Return NumberOfGames
    End Function

    Public Sub SetNumberOfGames()
        Me.NumberOfGames = NumberOfGames + 1
    End Sub

End Class
