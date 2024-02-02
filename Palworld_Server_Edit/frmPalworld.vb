Imports System.Drawing.Text
Imports System.IO


Public Class Form1
    Private Sub btnLoadServLoc_Click(sender As Object, e As EventArgs) Handles btnLoadServLoc.Click
        Dim BaseFolder As String
        Dim Settings As String
        BaseFolder = txtServLoc.Text
        Dim newFilePath As String

        newFilePath = Settings.Replace(".txt", ".ini")
        System.IO.File.Move(Settings, newFilePath)

        If System.IO.Directory.GetFiles(BaseFolder, "PalWorldSettings.ini", IO.SearchOption.AllDirectories).Length > 0 Then
            MsgBox("Found!")
            Settings = BaseFolder + "\PalWorldSettings.ini"
        Else
            MsgBox("Not found!")
        End If

        newFilePath = Settings.Replace(".ini", ".txt")
        System.IO.File.Move(Settings, newFilePath)

        Settings = BaseFolder + "\PalWorldSettings.txt"

        Dim Basic As String = "OptionSettings=(Difficulty=None,DayTimeSpeedRate=1.000000,NightTimeSpeedRate=1.000000,ExpRate=1.000000,PalCaptureRate=1.000000,PalSpawnNumRate=1.000000,PalDamageRateAttack=1.000000,PalDamageRateDefense=1.000000,PlayerDamageRateAttack=1.000000,PlayerDamageRateDefense=1.000000,PlayerStomachDecreaceRate=1.000000,PlayerStaminaDecreaceRate=1.000000,PlayerAutoHPRegeneRate=1.000000,PlayerAutoHpRegeneRateInSleep=1.000000,PalStomachDecreaceRate=1.000000,PalStaminaDecreaceRate=1.000000,PalAutoHPRegeneRate=1.000000,PalAutoHpRegeneRateInSleep=1.000000,BuildObjectDamageRate=1.000000,BuildObjectDeteriorationDamageRate=1.000000,CollectionDropRate=1.000000,CollectionObjectHpRate=1.000000,CollectionObjectRespawnSpeedRate=1.000000,EnemyDropItemRate=1.000000,DeathPenalty=All,bEnablePlayerToPlayerDamage=False,bEnableFriendlyFire=False,bEnableInvaderEnemy=True,bActiveUNKO=False,bEnableAimAssistPad=True,bEnableAimAssistKeyboard=False,DropItemMaxNum=3000,DropItemMaxNum_UNKO=100,BaseCampMaxNum=128,BaseCampWorkerMaxNum=40,DropItemAliveMaxHours=1.000000,bAutoResetGuildNoOnlinePlayers=False,AutoResetGuildTimeNoOnlinePlayers=72.000000,GuildPlayerMaxNum=20,PalEggDefaultHatchingTime=72.000000,WorkSpeedRate=1.000000,bIsMultiplay=False,bIsPvP=False,bCanPickupOtherGuildDeathPenaltyDrop=False,bEnableNonLoginPenalty=True,bEnableFastTravel=True,bIsStartLocationSelectByMap=True,bExistPlayerAfterLogout=False,bEnableDefenseOtherGuildPlayer=False,CoopPlayerMaxNum=4,ServerPlayerMaxNum=4,ServerName=""Ben Server"",ServerDescription="",AdminPassword=""Leondine1"",ServerPassword="",PublicPort=8211,PublicIP="",RCONEnabled=False,RCONPort=25575,Region="",bUseAuth=True,BanListURL=""https://api.palworldgame.com/api/banlist.txt"")"
        Dim Path As String = Settings
        Dim sw As StreamWriter
        sw = File.AppendText(Path)

        Using r As StreamReader = New StreamReader(Path)
            Dim line As String
            line = r.ReadLine
            If File.ReadAllText(Path).Length.Equals(0) Then
                sw.WriteLine(Basic)
                MsgBox("File is empty, creating basic sever settings")
            End If
        End Using


    End Sub
End Class
