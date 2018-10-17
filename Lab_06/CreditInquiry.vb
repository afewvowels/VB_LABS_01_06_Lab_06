'Project: StreamReader
'Author: Anthony DePinto 
'Date: Fall 2014 
'Description: 
' Read a file sequentially and display contents based on
' account type specified by user (credit, debit or zero balances).

' Author: Keith Smith
' Date: 17 October 2018

Option Explicit On
Option Strict On

Imports System.IO ' using classes from this namespace

Public Class CreditInquiry
    ' Declare variables
    Dim FileNameString As String
    ' Declare enumerables
    Enum AccountType
        CREDIT
        DEBIT
        ZERO
    End Enum

    Private Sub debitBalancesButton_Click(sender As Object, e As EventArgs) Handles debitBalancesButton.Click

    End Sub

    Private Sub creditBalancesButton_Click(sender As Object, e As EventArgs) Handles creditBalancesButton.Click

    End Sub

    Private Sub zeroBalancesButton_Click(sender As Object, e As EventArgs) Handles zeroBalancesButton.Click

    End Sub

    Private Sub OpenToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles OpenToolStripMenuItem.Click
        ' Declare objects
        Dim FileDialogResult As DialogResult ' Store what button clicked

        ' Show file open dialog block
        Using FileOpenDialogResult As New OpenFileDialog
            ' Show file open dialog
            FileDialogResult = FileOpenDialogResult.ShowDialog()
            ' Get file name from dialog selection
            FileNameString = FileOpenDialogResult.FileName
        End Using

        If FileDialogResult <> Windows.Forms.DialogResult.Cancel Then
        End If

        ' Enable buttons
        debitBalancesButton.Enabled = True
        creditBalancesButton.Enabled = True
        zeroBalancesButton.Enabled = True
    End Sub

    Private Sub ExitToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ExitToolStripMenuItem.Click
        ' Exit Application
        Me.Close()
    End Sub
End Class ' Credit Inquiry

