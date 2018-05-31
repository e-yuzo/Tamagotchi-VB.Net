Imports MongoDB.Bson

Public Class Player
    Private Property Id As MongoDB.Bson.ObjectId
    Private Property Name As String
    Private Property Password As String

    Public Sub SetId(id As ObjectId)
        Me.Id = id
    End Sub

    Public Function GetId() As ObjectId
        Return Id
    End Function

    Public Sub SetName(name As String)
        Me.Name = name
    End Sub

    Public Function GetName() As String
        Return Name
    End Function

    Public Sub SetPassword(password As String)
        Me.Password = password
    End Sub

    Public Function GetPassword() As String
        Return Password
    End Function

    Public Sub New(id As ObjectId, name As String, password As String)
        Me.Id = id
        Me.Name = name
        Me.Password = password
    End Sub

End Class
