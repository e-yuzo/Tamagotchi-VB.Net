Imports MongoDB.Bson

Public Class Monster
    Private Property Id As ObjectId
    Private Property PlayerName As String
    Private Property MonsterName As String
    Private Property State As String
    Private Property Hunger As Double
    Private Property Happyness As Double
    Private Property Health As Double
    Private Property LastTimeState As DateTime
    Private Property LastSleep As DateTime
    Private Property LastShower As DateTime
    Private Property InitialTime As DateTime
    Private Property WokeUp As DateTime
    Private Property DropOff As DateTime


    Public Sub New(id As ObjectId, playerName As String, monsterName As String, state As String, hunger As Double, happyness As Double, health As Double, lastTimeState As Date, lastSleep As Date, lastShower As Date, initialTime As Date, wokeUp As Date, dropOff As Date)
        Me.Id = id
        Me.PlayerName = playerName
        Me.MonsterName = monsterName
        Me.State = state
        Me.Hunger = hunger
        Me.Happyness = happyness
        Me.Health = health
        Me.LastTimeState = lastTimeState
        Me.LastSleep = lastSleep
        Me.LastShower = lastShower
        Me.InitialTime = initialTime
        Me.WokeUp = wokeUp
        Me.DropOff = dropOff
    End Sub

    Public Sub SetId(id As ObjectId)
        Me.Id = id
    End Sub

    Public Function GetId() As ObjectId
        Return Id
    End Function

    Public Sub SetPlayerName(name As String)
        Me.PlayerName = name
    End Sub

    Public Function GetPlayerName() As String
        Return PlayerName
    End Function

    Public Sub SetMonsterName(name As String)
        Me.MonsterName = name
    End Sub

    Public Function GetMonsterName() As String
        Return MonsterName
    End Function

    Public Sub SetState(state As String)
        Me.State = state
    End Sub

    Public Function GetState() As String
        Return State
    End Function

    Public Sub SetHunger(hunger As Double)
        Me.Hunger = hunger
    End Sub

    Public Function GetHunger() As Double
        Return Hunger
    End Function

    Public Sub SetHappyness(happy As Double)
        Me.Happyness = happy
    End Sub

    Public Function GetHappyness() As Double
        Return Happyness
    End Function

    Public Sub SetHealth(health As Double)
        Me.Health = health
    End Sub

    Public Function GetHealth() As Double
        Return Health
    End Function

    Public Sub SetLastTimeState(time As DateTime)
        Me.LastTimeState = time
    End Sub

    Public Function GetLastTimeState() As DateTime
        Return LastTimeState
    End Function

    Public Sub SetLastSleep(time As DateTime)
        Me.LastSleep = time
    End Sub

    Public Function GetLastSleep() As DateTime
        Return LastSleep
    End Function

    Public Sub SetLastShower(time As DateTime)
        Me.LastShower = time
    End Sub

    Public Function GetLastShower() As DateTime
        Return LastShower
    End Function

    Public Sub SetInitialTime(time As DateTime)
        Me.InitialTime = time
    End Sub

    Public Function GetInitialTime() As DateTime
        Return InitialTime
    End Function

    Public Sub SetWokeUp(time As DateTime)
        Me.WokeUp = time
    End Sub

    Public Function GetWokeUp() As DateTime
        Return WokeUp
    End Function

    Public Sub SetDropOff(time As DateTime)
        Me.DropOff = time
    End Sub

    Public Function GetDropOff() As DateTime
        Return DropOff
    End Function

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

            SetValues_RateBased(hungerRate, healthRate, happynessRate, deltaTime)
            Value_Limit()
            If (State <> "Sleeping") Then
                Change_State()
            End If
        Else
            If State = "Normal" Then
                Dim hungerRate As Integer = 5
                Dim healthRate As Integer = 5
                Dim happynessRate As Integer = 5

                SetValues_RateBased(hungerRate, healthRate, happynessRate, deltaTime)

                Value_Limit()
                Change_State()

            ElseIf State = "Sick" Then
                Dim hungerRate As Integer = 20
                Dim healthRate As Integer = 20
                Dim happynessRate As Integer = 20

                SetValues_RateBased(hungerRate, healthRate, happynessRate, deltaTime)

                Value_Limit()
                Change_State()

            ElseIf State = "Sleepy" Then
                Dim hungerRate As Integer = 10
                Dim healthRate As Integer = 15
                Dim happynessRate As Integer = 20

                SetValues_RateBased(hungerRate, healthRate, happynessRate, deltaTime)

                Value_Limit()
                Change_State()

            ElseIf State = "Sleeping" Then

                Dim hungerRate As Integer = 2
                Dim healthRate As Integer = -10
                Dim happynessRate As Integer = 1

                SetValues_RateBased(hungerRate, healthRate, happynessRate, deltaTime)

                Value_Limit()

            ElseIf State = "Sad" Then
                Dim hungerRate As Integer = 10
                Dim healthRate As Integer = 20
                Dim happynessRate As Integer = 30

                SetValues_RateBased(hungerRate, healthRate, happynessRate, deltaTime)

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

                SetValues_RateBased(hungerRate, healthRate, happynessRate, deltaTime)

                Value_Limit()
                Change_State()
            End If
        End If
        LastTimeState = currentTime
    End Sub

    Private Sub SetValues_RateBased(hungerRate As Integer, healthRate As Integer, happynessRate As Integer, deltaTime As Double)
        Dim random As System.Random = New System.Random()
        Dim luck = random.Next(800, 1200) / 1000.0
        Hunger = Hunger + ((hungerRate * luck) * deltaTime)
        luck = random.Next(800, 1200) / 1000.0
        Health = Health - ((healthRate * luck) * deltaTime)
        luck = random.Next(800, 1200) / 1000.0
        Happyness = Happyness - ((happynessRate * luck) * deltaTime)
    End Sub

    Public Sub Sleep_Action()
        If (State = "Sleeping") Then
            Dim play = New Utils()
            play.PlaySound("C:\Users\Yuso\Desktop\Sounds\lights.wav")
            Dim minutesToSleep = Math.Abs((DateTime.UtcNow - WokeUp).TotalMinutes) / 3
            Dim minutesSlept = Math.Abs((DateTime.UtcNow - DropOff).TotalMinutes)
            InitialTime.AddMinutes(minutesSlept / 3)
            If (minutesSlept >= minutesToSleep) Then
                WokeUp = DateTime.UtcNow
            Else
                WokeUp.AddMinutes(minutesToSleep - minutesSlept)
            End If
            Change_State()
        Else
            Dim play = New Utils()
            play.PlaySound("C:\Users\Yuso\Desktop\Sounds\lights.wav")
            DropOff = DateTime.UtcNow
            State = "Sleeping"
        End If
    End Sub

    Public Sub Bath_Action()
        If (State <> "Sleeping") Then
            Dim play = New Utils()
            play.PlaySound("C:\Users\Yuso\Desktop\Sounds\cure.wav")
            If (State = "Dirty") Then
                LastShower = DateTime.UtcNow
                Health = Health + 15
                Change_State()
            End If
        Else
            Dim play = New Utils()
            play.PlaySound("C:\Users\Yuso\Desktop\Sounds\deny.wav")
        End If
    End Sub

    Public Sub Cure_Action()
        If (State <> "Sleeping") Then
            Dim play = New Utils()
            play.PlaySound("C:\Users\Yuso\Desktop\Sounds\cure.wav")
            If (State = "Sick") Then
                Dim random As System.Random = New System.Random()
                Health = (Health + 30) - (Hunger * (random.Next(Int(Hunger), Int(Hunger + 20)) / 1000))
            Else
                Health = (Health - 10)
            End If
            Change_State()
            Value_Limit()
        Else
            Dim play = New Utils()
            play.PlaySound("C:\Users\Yuso\Desktop\Sounds\deny.wav")
        End If
    End Sub

    Public Sub Eat_Action()
        If (State <> "Sleeping") Then
            Dim play = New Utils()
            play.PlaySound("C:\Users\Yuso\Desktop\Sounds\feed.wav")
            Hunger = (Hunger - 20) + (Health / 10.0)
            Change_State()
            Value_Limit()
        Else
            Dim play = New Utils()
            play.PlaySound("C:\Users\Yuso\Desktop\Sounds\deny.wav")
        End If
    End Sub

    Public Sub Game_Action(score As Integer)
        Dim play = New Utils()
        play.PlaySound("C:\Users\Yuso\Desktop\Sounds\deny.wav")
        If (State <> "Sleeping") Then
            Hunger = Hunger + score
            Happyness = Happyness + (score * 3)
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
        ElseIf (Math.Abs((DateTime.UtcNow - WokeUp).TotalHours) >= 9) Then '
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
