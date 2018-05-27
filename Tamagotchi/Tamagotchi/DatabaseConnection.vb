Imports MongoDB.Bson
Imports MongoDB.Driver
Imports MongoDB.Driver.Builders

Public Class DatabaseConnection
    Public Function GetConnection() As MongoDatabase
        Dim client = New MongoClient("mongodb://localhost:27017")
        Dim server = client.GetServer()
        Dim database = server.GetDatabase("test")
        Return database
    End Function

    Public Function GetPetCollection() As MongoCollection(Of Monster)
        Dim database As MongoDatabase = GetConnection()
        Dim collection = database.GetCollection(Of Monster)("Monsters")
        Return collection
    End Function

    Public Function GetPlayerCollection() As MongoCollection(Of Player)
        Dim database As MongoDatabase = GetConnection()
        Dim collection = database.GetCollection(Of Player)("Players")
        Return collection
    End Function
End Class
