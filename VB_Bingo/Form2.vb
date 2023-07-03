Imports System.IO
Imports System.Windows.Forms.Design
Imports System.Windows.Forms.VisualStyles
Imports System.Windows.Forms.VisualStyles.VisualStyleElement
Imports System.Windows.Forms.VisualStyles.VisualStyleElement.Button

Public Class Form2
    ' compteur de position
    Private gbPosX As Integer = 27
    Private gbPosY As Integer = 81
    Private gbID As Integer = 1
    Private gbTI As Integer = 1

    ' compteur
    Public cptBtnBoulier As Integer = 1
    Public cptBoxBoulier As Integer = 1

    ' instanciation
    Public boulier As New Boulier
    Public myForm1 As Form1 = New Form1
    Public joueur As Cartes
    Public myStats As New Stats

    ' determiner les choix du joueur
    Private NombreDeJoueurs As Byte = DeterminerNombreJoueurs()
    Private ModeJeux As Byte = DeterminerModeJeux()


    Public Sub Form2_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Me.Size = New Size(1000, 950)

        ' Ici on ajoute le bon nombre de cartes des joueurs et le boulier, dans le form
        For i As Byte = 1 To NombreDeJoueurs Step 1

            joueur = New Cartes
            joueur.myCard.Location = New Point(gbPosX, gbPosY)
            joueur.myCard.Name = "groupBox_" & gbID.ToString()
            joueur.myCard.Size = New Size(142, 182)
            joueur.myCard.TabStop = False
            joueur.myCard.TabIndex = gbTI
            joueur.myCard.Text = "Carte Bingo " & gbID.ToString()

            Me.Controls.Add(joueur.myCard)

            Dim list As New List(Of Integer)
            list = joueur.myCardNumbers

            gbPosX += 148
            gbID += 1
            gbTI += 1

            If i = 5 Then
                gbPosX = 27
                gbPosY += 188
            ElseIf i = 10 Then
                gbPosX = 27
                gbPosY += 188
            ElseIf i = 15 Then
                gbPosX = 27
                gbPosY += 188
            End If
        Next

        Me.Controls.Add(boulier.boulier)
        Me.Button_1 = boulier.btn

    End Sub

    ' Ici on a un bouton creer dynamiquement par le code - jai beaucoup bucher et appris ici 
    Public Sub Button_1_Click(sender As Object, e As EventArgs) Handles Button_1.Click
        ' on itere les control du boulier et on y assigne une valeur si cest le bon textbox

        For Each ctl In Me.boulier.boulier.Controls
            Dim textBoxConcat As String = "txtBox_" + cptBoxBoulier.ToString()
            If ctl.Name = textBoxConcat Then
                ctl.Text = boulier.boulierList(cptBoxBoulier - 1)
                Dim i As Integer = boulier.boulierListNumbers.IndexOf(boulier.boulierList(cptBoxBoulier - 1))
                If myStats.statsBoulierList.Contains(boulier.boulierList(cptBoxBoulier - 1)) Then
                    myStats.statsList(i) += 1
                Else
                    myStats.statsBoulierList.Add(boulier.boulierList(cptBoxBoulier - 1))
                    myStats.statsList(i) += 1
                End If
            End If
        Next

        ' verification des carte - on itere les groupbox de la form, si cest le bon, on itere les control du groupbox
        ' si le text du control est egale a la boule sorti du boulier, son backcolor devient orange
        Dim grpBoxCpt As Integer = 1
        For Each ctr In Me.Controls
            Dim groupBoxConcat As String = "groupBox_" + grpBoxCpt.ToString()
            grpBoxCpt += 1
            If ctr.Name = groupBoxConcat Then
                'Dim txtBoxCpt As Integer = 1
                For Each sub_ctr In ctr.Controls
                    Dim numeroDeBoule As String = boulier.boulierList(cptBoxBoulier - 1).Substring(1, 2)
                    If sub_ctr.Text = numeroDeBoule Then
                        sub_ctr.Backcolor = Color.Orange
                    End If
                Next
            End If
        Next

        cptBoxBoulier += 1 ' possible jai pas besoin de celle ci et pourrais utiliser cptBtnBoulier

        cptBtnBoulier += 1 'le nombre de boule sorti, avant lincrementation
        If cptBtnBoulier = 75 Then
            MsgBox("on ne devrais pas arriver ici , un gagnant devrais se declarer avant")
        End If

        DeterminerGagnant()

    End Sub

    ' On determine le gagnant selon le mode de jeu choisi
    Private Sub DeterminerGagnant()
        Dim modeJeu As Byte = DeterminerModeJeux()
        If modeJeu = 1 Then
            DeterminerLigne()
        ElseIf modeJeu = 2 Then
            Determiner4Coins()
        Else
            DeterminerCartePleine()
        End If
    End Sub

    ' calcul des gagnant en mode 4coins
    Private Sub Determiner4Coins()
        Dim grpBoxCpt As Integer = 1
        For Each ctr In Me.Controls
            Dim groupBoxConcat As String = "groupBox_" + grpBoxCpt.ToString()
            grpBoxCpt += 1
            If ctr.Name = groupBoxConcat Then
                Dim cornerCpt As Integer = 0
                For Each sub_ctr In ctr.Controls
                    If sub_ctr.Name = "txtBox_1" And sub_ctr.Backcolor = Color.Orange Then
                        cornerCpt += 1
                    ElseIf sub_ctr.Name = "txtBox_5" And sub_ctr.Backcolor = Color.Orange Then
                        cornerCpt += 1
                    ElseIf sub_ctr.Name = "txtBox_21" And sub_ctr.Backcolor = Color.Orange Then
                        cornerCpt += 1
                    ElseIf sub_ctr.Name = "txtBox_25" And sub_ctr.Backcolor = Color.Orange Then
                        cornerCpt += 1
                    Else
                        Continue For
                    End If
                Next
                If cornerCpt = 4 Then
                    MessageGagnant(grpBoxCpt)
                End If
            End If
        Next
    End Sub

    'calcul des gagnant en mode carte pleine
    Private Sub DeterminerCartePleine()
        Dim grpBoxCpt As Integer = 1
        For Each ctr In Me.Controls
            Dim groupBoxConcat As String = "groupBox_" + grpBoxCpt.ToString()
            grpBoxCpt += 1
            If ctr.Name = groupBoxConcat Then
                Dim backColorCount As Integer = 0
                For Each sub_ctr In ctr.Controls
                    If sub_ctr.Backcolor = Color.Orange Then
                        backColorCount += 1
                    End If
                Next
                If backColorCount = 25 Then
                    MessageGagnant(grpBoxCpt)
                End If
            End If
        Next
    End Sub

    ' calcul des gagnant en mode ligne
    Private Sub DeterminerLigne()
        DeterminerLigneVerticale()
        DeterminerLigneHorizontale()
        DeterminerLigneDiagonale()
    End Sub

    Private Sub DeterminerLigneVerticale()
        Dim grpBoxCpt As Integer = 1
        For Each ctr In Me.Controls
            Dim groupBoxConcat As String = "groupBox_" + grpBoxCpt.ToString()
            grpBoxCpt += 1
            If ctr.Name = groupBoxConcat Then
                Dim colCpt1 As Integer = 0
                Dim colCpt2 As Integer = 0
                Dim colCpt3 As Integer = 0
                Dim colCpt4 As Integer = 0
                Dim colCpt5 As Integer = 0
                Dim txtBoxCpt As Integer = 1

                For Each sub_ctr In ctr.Controls
                    Dim txtBoxConcat As String = "txtBox_" + txtBoxCpt.ToString()
                    If sub_ctr.Name = txtBoxConcat Then
                        If txtBoxCpt <= 5 And sub_ctr.Backcolor = Color.Orange Then
                            colCpt1 += 1
                        ElseIf txtBoxCpt <= 10 And sub_ctr.Backcolor = Color.Orange Then
                            colCpt2 += 1
                        ElseIf txtBoxCpt <= 15 And sub_ctr.Backcolor = Color.Orange Then
                            colCpt3 += 1
                        ElseIf txtBoxCpt <= 20 And sub_ctr.Backcolor = Color.Orange Then
                            colCpt4 += 1
                        ElseIf txtBoxCpt <= 25 And sub_ctr.Backcolor = Color.Orange Then
                            colCpt5 += 1
                        End If
                    Else
                        Continue For
                    End If
                    txtBoxCpt += 1
                Next

                If colCpt1 = 5 Or colCpt2 = 5 Or colCpt3 = 5 Or colCpt4 = 5 Or colCpt5 = 5 Then
                    MessageGagnant(grpBoxCpt)
                End If

            End If
        Next
    End Sub

    Private Sub DeterminerLigneHorizontale()
        Dim grpBoxCpt As Integer = 1
        For Each ctr In Me.Controls
            Dim groupBoxConcat As String = "groupBox_" + grpBoxCpt.ToString()
            grpBoxCpt += 1
            If ctr.Name = groupBoxConcat Then
                Dim ligneCpt1 As Integer = 0
                Dim ligneCpt2 As Integer = 0
                Dim ligneCpt3 As Integer = 0
                Dim ligneCpt4 As Integer = 0
                Dim ligneCpt5 As Integer = 0
                Dim txtBoxCpt As Integer = 1

                For Each sub_ctr In ctr.Controls
                    Dim txtBoxConcat As String = "txtBox_" + txtBoxCpt.ToString()
                    If sub_ctr.Name = txtBoxConcat Then
                        If txtBoxCpt = 1 And sub_ctr.Backcolor = Color.Orange Or txtBoxCpt = 6 And sub_ctr.Backcolor = Color.Orange Or txtBoxCpt = 11 And sub_ctr.Backcolor = Color.Orange Or txtBoxCpt = 16 And sub_ctr.Backcolor = Color.Orange Or txtBoxCpt = 21 And sub_ctr.Backcolor = Color.Orange Then
                            ligneCpt1 += 1
                        ElseIf txtBoxCpt = 2 And sub_ctr.Backcolor = Color.Orange Or txtBoxCpt = 7 And sub_ctr.Backcolor = Color.Orange Or txtBoxCpt = 12 And sub_ctr.Backcolor = Color.Orange Or txtBoxCpt = 17 And sub_ctr.Backcolor = Color.Orange Or txtBoxCpt = 22 And sub_ctr.Backcolor = Color.Orange Then
                            ligneCpt2 += 1
                        ElseIf txtBoxCpt = 3 And sub_ctr.Backcolor = Color.Orange Or txtBoxCpt = 8 And sub_ctr.Backcolor = Color.Orange Or txtBoxCpt = 13 And sub_ctr.Backcolor = Color.Orange Or txtBoxCpt = 18 And sub_ctr.Backcolor = Color.Orange Or txtBoxCpt = 23 And sub_ctr.Backcolor = Color.Orange Then
                            ligneCpt3 += 1
                        ElseIf txtBoxCpt = 4 And sub_ctr.Backcolor = Color.Orange Or txtBoxCpt = 9 And sub_ctr.Backcolor = Color.Orange Or txtBoxCpt = 14 And sub_ctr.Backcolor = Color.Orange Or txtBoxCpt = 19 And sub_ctr.Backcolor = Color.Orange Or txtBoxCpt = 24 And sub_ctr.Backcolor = Color.Orange Then
                            ligneCpt4 += 1
                        ElseIf txtBoxCpt = 5 And sub_ctr.Backcolor = Color.Orange Or txtBoxCpt = 10 And sub_ctr.Backcolor = Color.Orange Or txtBoxCpt = 15 And sub_ctr.Backcolor = Color.Orange Or txtBoxCpt = 20 And sub_ctr.Backcolor = Color.Orange Or txtBoxCpt = 25 And sub_ctr.Backcolor = Color.Orange Then
                            ligneCpt5 += 1
                        End If
                    Else
                        Continue For
                    End If
                    txtBoxCpt += 1
                Next

                If ligneCpt1 = 5 Or ligneCpt2 = 5 Or ligneCpt3 = 5 Or ligneCpt4 = 5 Or ligneCpt5 = 5 Then
                    MessageGagnant(grpBoxCpt)
                End If

            End If
        Next
    End Sub

    Private Sub DeterminerLigneDiagonale()
        Dim grpBoxCpt As Integer = 1
        For Each ctr In Me.Controls
            Dim groupBoxConcat As String = "groupBox_" + grpBoxCpt.ToString()
            grpBoxCpt += 1
            If ctr.Name = groupBoxConcat Then
                Dim diaCpt1 As Byte = 0
                Dim diaCpt2 As Byte = 0
                Dim txtBoxCpt As Integer = 1

                For Each sub_ctr In ctr.Controls
                    Dim txtBoxConcat As String = "txtBox_" + txtBoxCpt.ToString()
                    If sub_ctr.Name = txtBoxConcat Then
                        If txtBoxCpt = 1 And sub_ctr.Backcolor = Color.Orange Or txtBoxCpt = 7 And sub_ctr.Backcolor = Color.Orange Or txtBoxCpt = 19 And sub_ctr.Backcolor = Color.Orange Or txtBoxCpt = 25 And sub_ctr.Backcolor = Color.Orange Then
                            diaCpt1 += 1
                        ElseIf txtBoxCpt = 5 And sub_ctr.Backcolor = Color.Orange Or txtBoxCpt = 9 And sub_ctr.Backcolor = Color.Orange Or txtBoxCpt = 17 And sub_ctr.Backcolor = Color.Orange Or txtBoxCpt = 21 And sub_ctr.Backcolor = Color.Orange Then
                            diaCpt2 += 1
                        End If
                    Else
                        Continue For
                    End If
                    txtBoxCpt += 1
                Next

                If diaCpt1 = 4 Or diaCpt2 = 4 Then
                    MessageGagnant(grpBoxCpt)
                End If

            End If
        Next
    End Sub

    ' determiner les choix du joueur
    Private Function DeterminerNombreJoueurs()
        Dim nombreJoueurs As Byte = CByte(Form1.NumericUpDown1.Value)
        Return nombreJoueurs
    End Function

    Private Function DeterminerModeJeux() As Byte
        Dim modeJeux As Byte
        If Form1.RadioButton1.Checked = True Then
            modeJeux = 1
        ElseIf Form1.RadioButton2.Checked = True Then
            modeJeux = 2
        Else
            modeJeux = 3
        End If
        Return modeJeux
    End Function

    'messages de fin de partie
    Private Sub MessageGagnant(cpt As Integer)
        Dim answer As String
        answer = MsgBox("Joueur avec la carte #" + (cpt - 1).ToString() + " est Gagnant, Felicitation !!!  -   Voulez-vous Rejouer? ", vbQuestion + vbYesNo, "User Response")
        MsgBox("Partie gagner apres " + cptBtnBoulier.ToString() + " boules")
        If answer = vbYes Then
            Rejouer()
            'CalculStats()
        Else
            CalculStats()
            MsgBox("aurevoir, a bientot")
            Application.Exit()
        End If
    End Sub

    'calcul et affichage des stats
    Public Sub CalculStats()
        Dim ind As Integer
        Dim qty As Integer
        Dim message As String
        For Each boule In myStats.statsBoulierList
            ind = boulier.boulierListNumbers.IndexOf(boule)
            qty = myStats.statsList(ind)
            message = boule & " revient " & qty.ToString() & " fois"
            If qty = 0 Then
                Continue For
            Else
                MsgBox(message)
            End If
        Next
    End Sub

    ' demande pour rejouer
    Private Sub Rejouer()
        Me.Close()
        myForm1.Button1.PerformClick()
    End Sub

    Private Sub SaveStats()
        ' etant donner on ne pouvais pas sauvegarder les stats dans une globale a cause des conflit d'instanciation
        ' on voulais faire un filestream pour sauvegarder les stats d'une partie a lautre. 
    End Sub

End Class