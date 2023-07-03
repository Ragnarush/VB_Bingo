Public Class Cartes
    Private labelCompteur As Byte = 0
    Private labelPosX As Integer = 9
    Private labelPosY As Integer = 19

    Private textboxID As Byte = 1
    Private textboxTI As Byte = 1
    Private tbPosX As Integer = 6
    Private tbPosY As Integer = 37

    Private myGrpBoxID As Byte = 1
    Private myGrpBoxTabIndex = 1
    Public groupboxPosX As Integer = 27
    Public groupboxPosY As Integer = 81

    Public myCard As New GroupBox
    Public myCardNumbers As New List(Of Integer)
    Private numberList As New List(Of Integer) ' gros test ici
    Private stringNumberList As New List(Of String) ' gros test ici
    Public textBoxList As New List(Of TextBox)

    Public Property Controls As Object

    Public Sub New()
        Me.myCard = MyGroupBox(myGrpBoxID, myGrpBoxTabIndex, groupboxPosX, groupboxPosY)
        'Me.myCardNumbers = numberList
    End Sub

    Public Function MyGroupBox(gbID As Integer, gbTI As Integer, gbPosX As Integer, gbPosY As Integer)
        Dim myGrpBox As New GroupBox
        Dim myGB_ID As String = gbID.ToString()
        Dim myGB_TI As String = gbTI.ToString()

        myGrpBox.SuspendLayout()
        myGrpBox.BackColor = Color.ForestGreen

        myGrpBox.Location = New Point(gbPosX, gbPosY)
        myGrpBox.Name = "groupBox_" & myGB_ID
        myGrpBox.Size = New Size(142, 182)
        myGrpBox.TabStop = False
        myGrpBox.TabIndex = myGB_TI
        myGrpBox.Text = "Carte Bingo " & myGB_ID
        myGrpBox.Enabled = False

        For i As Byte = 1 To 5 Step 1
            myGrpBox.Controls.Add(MyLabel())
        Next

        Dim number As Integer
        Dim rndA As Integer = 15
        Dim rndB As Integer = 1
        numberList.Clear()

        For i As Byte = 1 To 5 Step 1
            For j As Byte = 1 To 5 Step 1
                Do
                    Dim rnd = New Random
                    number = rnd.Next(rndB, rndA)
                Loop While (numberList.Contains(number))
                numberList.Add(number)
                Dim strNumber As String
                strNumber = StringNumberFormat(number.ToString())
                stringNumberList.Add(strNumber)

                Dim myBox = MyTextBox(textboxID, textboxTI, tbPosX, tbPosY, strNumber)
                If myBox.Name = "txtBox_13" Then
                    myBox.Text = "X"
                    myBox.Backcolor = Color.Orange
                End If
                textBoxList.Add(myBox)
                myGrpBox.Controls.Add(myBox)
                tbPosY += 29
                textboxID += 1
                textboxTI += 1
            Next
            tbPosX += 27
            tbPosY = 37
            rndA += 15
            rndB += 15
        Next

        Return myGrpBox
    End Function
    Public Sub ItererGroupboxCompteurs()
        myGrpBoxID += 1
        myGrpBoxTabIndex += 1
        groupboxPosX += 148
        groupboxPosY += 0
    End Sub

    Public Function MyTextBox(tbID As Integer, tbTI As Integer, X As Integer, Y As Integer, strNumber As String) ' as integer
        Dim myTB As New TextBox
        Dim myTB_ID As String = tbID
        Dim myTB_TI As String = tbTI

        myTB.Location = New Point(X, Y)
        myTB.Name = "txtBox_" & myTB_ID
        myTB.Size = New Size(21, 23)
        myTB.TabStop = False
        myTB.TextAlign = HorizontalAlignment.Center
        myTB.Text = strNumber
        myTB.TabIndex = myTB_TI
        myTB.Enabled = False

        Return myTB
    End Function

    Public Sub ItererTextboxCompteurs()
        textboxID += 1
        textboxTI += 1
        tbPosX += 27
        tbPosY = 37
    End Sub

    Public Sub AssignerFreeCase()

    End Sub

    Public Function MyLabel()
        Dim label As New Label
        Dim labelID As String = GetLabelChar(labelCompteur)

        label.AutoSize = True
        label.Location = New Point(labelPosX, labelPosY)
        label.Name = "lbl" & labelID
        label.Size = New Size(14, 15)
        label.Text = labelID
        label.TabStop = False
        label.Enabled = False

        ItererLabelCompteurs()

        Return label

    End Function

    Function GetLabelChar(labelCompteur As Byte)
        Dim list As New List(Of Char)(New Char() {"B", "I", "N", "G", "O"})
        Dim id As Char = list(labelCompteur)
        Return id
    End Function

    Private Sub ItererLabelCompteurs()
        labelCompteur += 1
        labelPosX += 27
        labelPosY += 0
    End Sub

    Private Function StringNumberFormat(number As String)
        Dim strNumber As String
        If number = "0" Or "1" Or "2" Or "3" Or "4" Or "5" Or "6" Or "7" Or "8" Or "9" Then
            strNumber = number.ToString().PadLeft(2, "0")   'String.Format("{0:0#}", number)
            Return strNumber
        Else
            Return number
        End If
    End Function

End Class