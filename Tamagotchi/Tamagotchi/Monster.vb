Imports MongoDB.Bson

Public Class Monster
    Public Property Id As ObjectId
    Public Property PlayerName As String
    Public Property MonsterName As String
    Public Property State As String
    Public Property Hunger As Double
    Public Property Happyness As Double
    Public Property Health As Double
    Public Property LastTimeState As DateTime
    Private Property LastSleep As DateTime
    Private Property LastRest As DateTime
    Private Property LastShower As DateTime
    Public Property InitialTime As DateTime
    Public Property WokeUp As DateTime
    Public Property DropOff As DateTime

    'Public Sub New(id As ObjectId, playerName As String, monsterName As String, state As String, hunger As Double, happyness As Double, health As Double, lastTimeState As Date)
    '    Me.Id = id
    '    Me.PlayerName = playerName
    '    Me.MonsterName = monsterName
    '    Me.State = state
    '    Me.Hunger = hunger
    '    Me.Happyness = happyness
    '    Me.Health = health
    '    Me.LastTimeState = lastTimeState
    'End Sub

    'Public Property PlayerName() As String
    '    Get
    '        Return PlayerName
    '    End Get
    '    Set(ByVal value As String)
    '        PlayerName = value
    '    End Set
    'End Property

    Public Sub Update(Page_Reloaded As Boolean)
        Dim currentTime = DateTime.UtcNow
        Dim timeDiff = Get_Clock_Difference(currentTime, LastTimeState)
        Dim deltaTime = timeDiff / 1000000.0
        If Page_Reloaded = True Then
            InitialTime.AddMilliseconds(timeDiff / 2)
            Dim hungerRate As Integer = 1
            Dim healthRate As Integer = 1
            Dim happynessRate As Integer = 1

            Dim random As System.Random = New System.Random()

            Dim luck = random.Next(800, 1200) / 1000.0
            Hunger = Hunger + ((hungerRate * luck) * deltaTime)
            luck = random.Next(800, 1200) / 1000.0
            Health = Health - ((healthRate * luck) * deltaTime)
            luck = random.Next(800, 1200) / 1000.0
            Happyness = Happyness - ((happynessRate * luck) * deltaTime)
            Value_Limit()
            Change_State()
        Else
            If State = "Normal" Then
                Dim hungerRate As Integer = 5
                Dim healthRate As Integer = 5
                Dim happynessRate As Integer = 5

                Dim random As System.Random = New System.Random()

                Dim luck = random.Next(800, 1200) / 1000.0
                Hunger = Hunger + ((hungerRate * luck) * deltaTime)
                luck = random.Next(800, 1200) / 1000.0
                Health = Health - ((healthRate * luck) * deltaTime)
                luck = random.Next(800, 1200) / 1000.0
                Happyness = Happyness - ((happynessRate * luck) * deltaTime)

                Value_Limit()
                Change_State()

            ElseIf State = "Sick" Then
                Dim hungerRate As Integer = 20
                Dim healthRate As Integer = 20
                Dim happynessRate As Integer = 20

                Dim random As System.Random = New System.Random()

                Dim luck = random.Next(800, 1200) / 1000.0
                Hunger = Hunger + ((hungerRate * luck) * deltaTime)
                luck = random.Next(800, 1200) / 1000.0
                Health = Health - ((healthRate * luck) * deltaTime)
                luck = random.Next(800, 1200) / 1000.0
                Happyness = Happyness - ((happynessRate * luck) * deltaTime)

                Value_Limit()
                Change_State()

            ElseIf State = "Sleepy" Then
                Dim hungerRate As Integer = 10
                Dim healthRate As Integer = 15
                Dim happynessRate As Integer = 20

                Dim random As System.Random = New System.Random()

                Dim luck = random.Next(800, 1200) / 1000.0
                Hunger = Hunger + ((hungerRate * luck) * deltaTime)
                luck = random.Next(800, 1200) / 1000.0
                Health = Health - ((healthRate * luck) * deltaTime)
                luck = random.Next(800, 1200) / 1000.0
                Happyness = Happyness - ((happynessRate * luck) * deltaTime)

                Value_Limit()
                Change_State()

            ElseIf State = "Sleeping" Then

                Dim hungerRate As Integer = 2
                Dim healthRate As Integer = -10
                Dim happynessRate As Integer = 1

                Dim random As System.Random = New System.Random()

                Dim luck = random.Next(800, 1200) / 1000.0
                Hunger = Hunger + ((hungerRate * luck) * deltaTime)
                luck = random.Next(800, 1200) / 1000.0
                Health = Health - ((healthRate * luck) * deltaTime)
                luck = random.Next(800, 1200) / 1000.0
                Happyness = Happyness - ((happynessRate * luck) * deltaTime)

                Value_Limit()

            ElseIf State = "Sad" Then
                Dim hungerRate As Integer = 10
                Dim healthRate As Integer = 20
                Dim happynessRate As Integer = 30

                Dim random As System.Random = New System.Random()

                Dim luck = random.Next(800, 1200) / 1000.0
                Hunger = Hunger + ((hungerRate * luck) * deltaTime)
                luck = random.Next(800, 1200) / 1000.0
                Health = Health - ((healthRate * luck) * deltaTime)
                luck = random.Next(800, 1200) / 1000.0
                Happyness = Happyness - ((happynessRate * luck) * deltaTime)

                Value_Limit()
                Change_State()

            ElseIf State = "Dirty" Then
                Dim hungerRate As Integer = 10
                Dim healthRate As Integer = 25
                Dim happynessRate As Integer = 8

                If (Math.Abs((DateTime.UtcNow - LastShower).TotalMinutes) / (New System.Random().Next(1, 100)) > 30) Then
                    If (Health > 30) Then
                        Health = 30
                    End If
                End If

                Dim random As System.Random = New System.Random()

                Dim luck = random.Next(800, 1200) / 1000.0
                Hunger = Hunger + ((hungerRate * luck) * deltaTime)
                luck = random.Next(800, 1200) / 1000.0
                Health = Health - ((healthRate * luck) * deltaTime)
                luck = random.Next(800, 1200) / 1000.0
                Happyness = Happyness - ((happynessRate * luck) * deltaTime)

                Value_Limit()
                Change_State()
            End If
        End If
        LastTimeState = currentTime
    End Sub

    Public Sub Sleep_Action()
        If (State = "Sleeping") Then 'quando esta dormindo o LastSleep representa a hora que foi dormir
            Dim minutesToSleep = Math.Abs((DateTime.UtcNow - WokeUp).TotalMinutes) / 3
            Dim minutesSlept = Math.Abs((DateTime.UtcNow - DropOff).TotalMinutes)
            InitialTime.AddMinutes(minutesSlept / 3)
            If (minutesSlept >= minutesToSleep) Then 'dormiu o suficiente
                WokeUp = DateTime.UtcNow
            Else 'nao dormiu o suficiente, considera o que dormiu
                WokeUp.AddMinutes(minutesToSleep - minutesSlept)
            End If
            Change_State()
        Else
            DropOff = DateTime.UtcNow
            State = "Sleeping"
        End If
    End Sub

    Public Sub Bath_Action() 'can get sick if no bath for too long
        If (State <> "Sleeping") Then
            If (State = "Dirty") Then
                LastShower = DateTime.UtcNow
                Health = Health + 15
                Change_State()
            End If
        End If
    End Sub

    Public Sub Cure_Action()
        If (State <> "Sleeping") Then
            If (State = "Sick") Then
                Dim random As System.Random = New System.Random()
                Health = (Health + 30) - (Hunger * (random.Next(Int(Hunger), Int(Hunger + 20)) / 100))
            Else
                Health = (Health - 10)
            End If
            Change_State()
            Value_Limit()
        End If
    End Sub

    Public Sub Eat_Action()
        If (State <> "Sleeping") Then
            Hunger = (Hunger - 20) + (Health / 10.0)
            Change_State()
            Value_Limit()
        End If

    End Sub

    Private Sub Change_State()
        If (Happyness <= 0 And Health <= 0 And Hunger >= 100) Then
            State = "Dead"
        ElseIf (Health < 30) Then
            State = "Sick"
        ElseIf (Happyness < 40) Then
            State = "Sad"
        ElseIf (Math.Abs((DateTime.UtcNow - LastSleep).TotalHours) >= 3) Then '
            State = "Sleepy"
        ElseIf (Math.Abs((DateTime.UtcNow - LastShower).TotalHours) >= 6) Then
            State = "Dirty"
        Else
            State = "Normal"
        End If
    End Sub

    Private Function Get_Clock_Difference(time1 As Date, time2 As Date) As Double
        Dim diff As Double = Math.Abs((time1 - time2).TotalMilliseconds)
        Return diff
    End Function

    Private Sub Value_Limit()
        If (Happyness < 0) Then
            Happyness = 0
        ElseIf (Happyness > 100) Then
            Happyness = 100
        End If

        If (Hunger > 100) Then
            Hunger = 100
        ElseIf (Hunger < 0) Then
            If (Math.Abs(Hunger) / (New System.Random().Next(1, 100)) < 0.125) Then
                If (Health > 30) Then
                    Health = 30
                End If
            End If
            Hunger = 0
        End If

        If (Health < 0) Then
            Health = 0
        ElseIf (Health > 100) Then
            Health = 100
        End If
    End Sub

    Public Function IsPet_Dead() As Boolean
        If (State = "Dead") Then
            Return True
        Else
            Return False
        End If
    End Function

End Class
