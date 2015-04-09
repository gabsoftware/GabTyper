Imports GabSoftware.Utils

Public Class frmMain

    ''' <summary>
    ''' objet qui capture les touches du clavier
    ''' </summary>
    ''' <remarks></remarks>
    Friend WithEvents keyHook As GabKeyboardHook

    Private average As Integer = 87
    Private variation As Integer = 175
    Private minimum As Integer = 10
    Private position As Integer = 0

    Private Sub frmMain_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        Me.keyHook.Stop(True, False)
    End Sub

    Private Sub frmMain_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'On capture toutes les touches même si on a pas le focus
        Me.keyHook = New GabKeyboardHook(True)

        Randomize()
    End Sub

    Private Sub keyHook_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles keyHook.KeyDown
        Me.SetKeyboardActions(e, Me.ContainsFocus)
    End Sub

    Private Sub SetKeyboardActions(ByVal touche As System.Windows.Forms.KeyEventArgs, ByVal hasFocus As Boolean)
        If touche.KeyCode = Keys.T And touche.Shift And touche.Control Then

            If mainTimer.Enabled Then
                mainTimer.Stop()
            Else
                Me.position = 0
                mainTimer.Start()
            End If


        End If
    End Sub

    Private Sub mainTimer_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mainTimer.Tick

        Dim tmp As Integer
        tmp = average + ((Rnd() * variation) - (variation / 2))
        If tmp < minimum Then
            tmp = minimum
        End If
        mainTimer.Interval = tmp

        If position >= txtText.Text.Length Then
            mainTimer.Stop()
            Exit Sub
        End If

        SendKeys.Send(txtText.Text(position))

        If txtText.Text(position) = vbCr Then
            position += 1
        End If
        position += 1


        'txtText.Text &= mainTimer.Interval.ToString() & vbNewLine

    End Sub
End Class
