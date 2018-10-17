'Project: StreamReader
'Author: Anthony DePinto 
'Date: Fall 2014 
'Description: 
' Read a file sequentially and display contents based on
' account type specified by user (credit, debit or zero balances).

' Student information
' -------------------
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
        DEBIT
        CREDIT
        ZERO
    End Enum

    ' Button subroutines
    Private Sub debitBalancesButton_Click(sender As Object, e As EventArgs) Handles debitBalancesButton.Click
        DisplayAccounts(AccountType.DEBIT)
    End Sub

    Private Sub creditBalancesButton_Click(sender As Object, e As EventArgs) Handles creditBalancesButton.Click
        DisplayAccounts(AccountType.CREDIT)
    End Sub

    Private Sub zeroBalancesButton_Click(sender As Object, e As EventArgs) Handles zeroBalancesButton.Click
        DisplayAccounts(AccountType.ZERO)
    End Sub

    ' Menu subroutines
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
            ' Enable buttons
            debitBalancesButton.Enabled = True
            creditBalancesButton.Enabled = True
            zeroBalancesButton.Enabled = True

            ' Could load into a data structure here to be used
            ' in multiple other functions/subroutines
            ' Example: when searching for something, want to stop reading file
            ' once searched item is found
        End If
    End Sub

    Private Sub AboutToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles AboutToolStripMenuItem.Click
        ' Display the about screen
        AboutBoxForm.Show()
    End Sub

    Private Sub ExitToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ExitToolStripMenuItem.Click
        ' Exit Application
        Me.Close()
    End Sub

    ' Subroutines and functions
    Private Sub DisplayAccounts(ByVal inAccountType As AccountType)
        ' Declare stream reader
        Dim AccountStreamReader As StreamReader

        ' Try to open the file
        Try
            ' Create new stream reader object
            AccountStreamReader = New StreamReader(FileNameString, True)

            ' Clear text box in anticipation of writing new data
            ' (don't clear if file open fails)
            accountsTextBox.Clear()

            ' Create header for text box before account information is added
            ' to text box
            accountsTextBox.Text &= "The relevant accounts are:" & vbCrLf

            ' Read and display account information
            ' Alt:
            ' Do While Not AccountStreamReader.EndOfStream
            Do Until AccountStreamReader.EndOfStream
                ' Local variable declaractions
                Dim LineString As String = AccountStreamReader.ReadLine
                Dim TempAccount() As String = LineString.Split(CChar(","))
                Dim TempAccountValue As Decimal

                ' Try to parse account balance data from line
                ' read in from the file
                Try
                    TempAccountValue = Convert.ToDecimal(TempAccount(3))
                Catch ex As Exception
                    MessageBox.Show("Error converting account balance",
                                    "Balance conversion error",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Exclamation)
                End Try

                ' Test read account balance information to
                ' determine if the account should be displayed
                ' to the end user
                If ShouldDisplay(TempAccountValue, inAccountType) Then
                    ' Format temporary account information and append to accounts text box
                    accountsTextBox.Text &= String.Format("{0}{5}{1}{5}{2}{5}{3:c}{4}",
                                                         TempAccount(0),
                                                         TempAccount(1),
                                                         TempAccount(2),
                                                         TempAccountValue,
                                                         vbCrLf,
                                                         vbTab)
                End If
            Loop

        Catch ex As IOException
            ' Display error message if IOException occurs
            MessageBox.Show("Error reading file",
                            "IO Error",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Exclamation)
        Finally
            ' Try to close file
            Try
                AccountStreamReader.Close()
            Catch ex As NullReferenceException
                MessageBox.Show("Error closing file",
                                "IO Error",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Exclamation)
            End Try
        End Try
    End Sub

    Function ShouldDisplay(ByVal inAccountBalance As Decimal, ByVal _inAccountType As AccountType) As Boolean
        ' Logic to return true/false based on account type and account balance value
        If _inAccountType = AccountType.DEBIT AndAlso inAccountBalance > 0D Then
            Return True
        ElseIf _inAccountType = AccountType.CREDIT AndAlso inAccountBalance < 0D Then
            Return True
        ElseIf _inAccountType = AccountType.ZERO AndAlso inAccountBalance = 0D Then
            Return True
        Else
            Return False
        End If
    End Function
End Class ' Credit Inquiry

