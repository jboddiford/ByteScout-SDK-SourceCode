'*******************************************************************************************'
'                                                                                           '
' Download Free Evaluation Version From:     https://bytescout.com/download/web-installer   '
'                                                                                           '
' Also available as Web API! Free Trial Sign Up: https://secure.bytescout.com/users/sign_up '
'                                                                                           '
' Copyright © 2017-2018 ByteScout Inc. All rights reserved.                                 '
' http://www.bytescout.com                                                                  '
'                                                                                           '
'*******************************************************************************************'


Imports Bytescout.PDFExtractor

Class Program
    Friend Shared Sub Main(ByVal args As String())

        ' Create Bytescout.PDFExtractor.MultimediaExtractor instance
        Dim extractor As New MultimediaExtractor()
        extractor.RegistrationName = "demo"
        extractor.RegistrationKey = "demo"

        ' Load PDF document
        ' (!) We do not provide the sample document, please load your own.
        extractor.LoadDocumentFromFile(".\audio.pdf")

        Dim i As Integer = 0

        ' Initialize sound clips enumeration
        If extractor.GetFirstAudio() Then
            Do
                Dim outputFileName As String = "audio" & i & extractor.GetCurrentAudioExtension()

                ' Save sound clip to file
                extractor.SaveCurrentAudioToFile(outputFileName)

                i = i + 1

            Loop While extractor.GetNextAudio() ' Advance sounds enumeration
        End If

        ' Cleanup
        extractor.Dispose()

    End Sub
End Class
