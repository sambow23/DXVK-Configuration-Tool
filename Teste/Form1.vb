Public Class Form1

    Dim _mouseDown As Boolean
    Dim startPt As Point



    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        _mouseDown = False



        ' detect settings
        If My.Settings.fakeso = True Then
            CheckBox5.CheckState = CheckState.Checked
            If My.Settings.fakeso = False Then
                CheckBox5.CheckState = CheckState.Unchecked
            End If
        End If
        If My.Settings.vsync = True Then
            CheckBox3.CheckState = CheckState.Checked
            If My.Settings.vsync = False Then
                CheckBox3.CheckState = CheckState.Unchecked
            End If
        End If
        If My.Settings.d3d10 = True Then
            CheckBox4.CheckState = CheckState.Checked
            If My.Settings.d3d10 = False Then
                CheckBox4.CheckState = CheckState.Unchecked
            End If
        End If
        ComboBox1.SelectedItem = My.Settings.anisitropic

    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Me.WindowState = FormWindowState.Minimized
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        'warning
        MsgBox("Select the .exe game path")

        'getting path to save

        FolderBrowserDialog1.ShowDialog()
        If FolderBrowserDialog1.ShowDialog = Windows.Forms.DialogResult.OK Then

            'show selected folder
            My.Settings.path = (FolderBrowserDialog1.SelectedPath)
            My.Settings.Save()
        End If

        'delete file
        Dim FileToDelete As String
        FileToDelete = (My.Settings.path + "\dxvk.config")

        If System.IO.File.Exists(FileToDelete) = True Then
            System.IO.File.Delete(FileToDelete)
        End If

        'write on selected path dxvk.config file
        Dim file As System.IO.StreamWriter
        file = My.Computer.FileSystem.OpenTextFileWriter(My.Settings.path + "\dxvk.config", True)

        'fake stream output
        If My.Settings.fakeso = "True" Then
            file.WriteLine("d3d11.fakeStreamOutSupport = True")
        End If
        If My.Settings.fakeso = "False" Then
            file.WriteLine("d3d11.fakeStreamOutSupport = False")
        End If

        'vsync
        If My.Settings.vsync = "True" Then
            file.WriteLine("dxgi.syncInterval = 0")
        End If
        If My.Settings.vsync = "False" Then
            file.WriteLine("dxgi.syncInterval = 1")
        End If

        'd3d10 support
        If My.Settings.d3d10 = "True" Then
            file.WriteLine("d3d10.enable = True")
        End If
        If My.Settings.d3d10 = "False" Then
            file.WriteLine("d3d10.enable = False")
        End If

        'anisitropic
        file.WriteLine("d3d11.samplerAnisotropy = " + My.Settings.anisitropic)

        'AMD spoof
        If My.Settings.AMD = "True" Then
            file.WriteLine("dxgi.customDeviceId = e366")
            file.WriteLine("dxgi.customVendorId = 1002")
        End If

        'NVIDIA spoof
        If My.Settings.NVIDIA = "True" Then
            file.WriteLine("dxgi.customVendorId = 10de")
        End If

        file.Close()
        My.Settings.Save()

    End Sub

    Private Sub CheckBox5_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox5.CheckedChanged

    End Sub

    Private Sub CheckBox5_CheckStateChanged(sender As Object, e As EventArgs) Handles CheckBox5.CheckStateChanged
        If CheckBox5.CheckState = CheckState.Checked Then
            My.Settings.fakeso = "True"
        End If
        If CheckBox5.CheckState = CheckState.Unchecked Then
            My.Settings.fakeso = "False"
        End If
    End Sub

    Private Sub CheckBox3_CheckStateChanged(sender As Object, e As EventArgs) Handles CheckBox3.CheckStateChanged
        If CheckBox3.CheckState = CheckState.Checked Then
            My.Settings.vsync = "True"
        End If
        If CheckBox3.CheckState = CheckState.Unchecked Then
            My.Settings.vsync = "False"
        End If
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Me.Close()
    End Sub

    Private Sub CheckBox4_CheckStateChanged(sender As Object, e As EventArgs) Handles CheckBox4.CheckStateChanged
        If CheckBox4.CheckState = CheckState.Checked Then
            My.Settings.d3d10 = "True"
        End If
        If CheckBox4.CheckState = CheckState.Unchecked Then
            My.Settings.d3d10 = "False"
        End If
    End Sub

    Private Sub CheckBox4_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox4.CheckedChanged

    End Sub

    Private Sub Panel2_MouseDown(sender As Object, e As MouseEventArgs) Handles Panel2.MouseDown
        _mouseDown = True
        startPt = e.Location
    End Sub

    Private Sub Panel2_MouseMove(sender As Object, e As MouseEventArgs) Handles Panel2.MouseMove
        If _mouseDown = True Then
            Dim currpos As Point = PointToScreen(e.Location)
            Location = New Point(currpos.X - startPt.X, currpos.Y - startPt.Y)
        End If
    End Sub

    Private Sub Panel2_MouseUp(sender As Object, e As MouseEventArgs) Handles Panel2.MouseUp
        _mouseDown = False
    End Sub

    Private Sub ComboBox1_SelectedValueChanged(sender As Object, e As EventArgs) Handles ComboBox1.SelectedValueChanged
        My.Settings.anisitropic = ComboBox1.SelectedItem
    End Sub

    Private Sub CheckBox1_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox1.CheckedChanged

    End Sub

    Private Sub CheckBox1_CheckStateChanged(sender As Object, e As EventArgs) Handles CheckBox1.CheckStateChanged
        If CheckBox1.CheckState = CheckState.Checked Then
            My.Settings.AMD = "True"
            My.Settings.NVIDIA = "False"
            CheckBox2.CheckState = CheckState.Unchecked
        End If
        If CheckBox1.CheckState = CheckState.Unchecked Then
            My.Settings.AMD = "False"
        End If
    End Sub

    Private Sub CheckBox2_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox2.CheckedChanged

    End Sub

    Private Sub CheckBox2_CheckStateChanged(sender As Object, e As EventArgs) Handles CheckBox2.CheckStateChanged
        If CheckBox2.CheckState = CheckState.Checked Then
            My.Settings.NVIDIA = "True"
            My.Settings.AMD = "False"
            CheckBox1.CheckState = CheckState.Unchecked
        End If
        If CheckBox2.CheckState = CheckState.Unchecked Then
            My.Settings.NVIDIA = "False"
        End If
    End Sub
End Class
