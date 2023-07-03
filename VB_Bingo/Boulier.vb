Public Class Boulier
    ' compteur de position des textbox
    Private textboxID As Integer = 1
    Private textboxTI As Integer = 1
    Private tbPosX As Integer = 6
    Private tbPosY As Integer = 59

    'compteur de position des groupbox
    Public myGrpBoxID As Byte = 1
    Public myGrpBoxTabIndex = 1
    Public groupboxPosX As Integer = 811
    Public groupboxPosY As Integer = 81

    ' instanciation et listes du boulier
    Public boulier As New GroupBox
    Public boulierList As New List(Of String)
    Public boulierListIndex As New List(Of Byte)
    Public boulierListNumbers As New List(Of String)({"b01", "b02", "b03", "b04", "b05", "b06", "b07", "b08", "b09", "b10", "b11", "b12", "b13", "b14", "b15",
                                                      "i16", "i17", "i18", "i19", "i20", "i21", "i22", "i23", "i24", "i25", "i26", "i27", "i28", "i29", "i30",
                                                      "n31", "n32", "n33", "n34", "n35", "n36", "n37", "n38", "n39", "n40", "n41", "n42", "n43", "n44", "n45",
                                                      "g46", "g47", "g48", "g49", "g50", "g51", "g52", "g53", "g54", "g55", "g56", "g57", "g58", "g59", "g60",
                                                      "o61", "o62", "o63", "o64", "o65", "o66", "o67", "o68", "o69", "o70", "o71", "o72", "o73", "o74", "o75"})

    ' mes buttons
    Public btn As Button
    Public prochaineBoule As Button

    Public Property Controls As Object ' mmm je crois cest pour les Controls

    ' Constructeur
    Public Sub New()
        Me.boulier = MyGroupBox(myGrpBoxID, myGrpBoxTabIndex, groupboxPosX, groupboxPosY)
        Me.prochaineBoule = btn
        Me.boulierList = Shuffle(boulierListNumbers)
    End Sub

    ' fabrication d'une carte bingo
    Public Function MyGroupBox(gbID As Integer, gbTI As Integer, gbPosX As Integer, gbPosY As Integer)
        Dim myGrpBox As New GroupBox
        'Dim myGB_ID As String = gbID.ToString()
        Dim myGB_TI As String = gbTI.ToString()

        myGrpBox.BackColor = Color.Orange
        myGrpBox.Location = New Point(gbPosX, gbPosY)
        myGrpBox.Name = "groupBox_" & 21 ' 21 car il y a un maximum de 20 carte, donc 20 groupbox, le boulier sera le 21e
        myGrpBox.Size = New Size(145, 785)
        myGrpBox.TabStop = False
        myGrpBox.TabIndex = myGB_TI
        myGrpBox.Text = "Boulier"
        myGrpBox.Enabled = True

        btn = MyButton(6, 22)

        myGrpBox.Controls.Add(btn)

        For i As Byte = 1 To 25 Step 1
            For j As Byte = 1 To 5 Step 1
                Dim myBox = MyTextBox(textboxID, textboxTI, tbPosX, tbPosY)
                myGrpBox.Controls.Add(myBox)
                tbPosX += 27
                textboxID += 1
                textboxTI += 1
            Next
            tbPosX = 6
            tbPosY += 29
        Next
        Return myGrpBox
    End Function

    ' fabrication d'un textbox de carte de bingo
    Public Function MyTextBox(tbID As Integer, tbTI As Integer, X As Integer, Y As Integer)
        Dim myTB As New TextBox
        Dim myTB_ID As String = tbID
        Dim myTB_TI As String = tbTI
        Dim boulierList As New List(Of String)

        myTB.Location = New Point(X, Y)
        myTB.Name = "txtBox_" & myTB_ID
        myTB.Size = New Size(25, 23)
        myTB.TabStop = False
        myTB.TextAlign = HorizontalAlignment.Center
        myTB.TabIndex = myTB_TI
        myTB.Enabled = True

        Return myTB
    End Function

    ' mon Button dynamique
    Public Function MyButton(X As Integer, Y As Integer)
        Dim myBTN As New Button

        myBTN.Location = New Point(X, Y)
        myBTN.Name = "Button_1"
        myBTN.Size = New Size(130, 30)
        myBTN.TabStop = False
        myBTN.TextAlign = HorizontalAlignment.Center
        myBTN.BackColor = Color.ForestGreen
        myBTN.Text = "Prochaine Boule"
        myBTN.TabIndex = 1
        myBTN.Enabled = True

        Return myBTN
    End Function

    ' methode pour shuffler les boules
    Function Shuffle(Of T)(collection As IEnumerable(Of T)) As List(Of T)
        Dim rnd As Random = New Random()
        Shuffle = collection.OrderBy(Function(a) rnd.Next()).ToList()
    End Function
End Class