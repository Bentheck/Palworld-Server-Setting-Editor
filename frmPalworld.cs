using System;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace PalWorld_Server_Edit
{
    public partial class frmPalworld : Form
    {
        public DialogResult rep;
        private string defaultSettings = "[/Script/Pal.PalGameWorldSettings]\n" +
                                        "OptionSettings = (Difficulty = None, DayTimeSpeedRate = 1.000000, " +
                                        "NightTimeSpeedRate = 1.000000, ExpRate = 1.000000, PalCaptureRate = 1.000000, " +
                                        "PalSpawnNumRate = 1.000000, PalDamageRateAttack = 1.000000, PalDamageRateDefense = 1.000000, " +
                                        "PlayerDamageRateAttack = 1.000000, PlayerDamageRateDefense = 1.000000, " +
                                        "PlayerStomachDecreaseRate = 1.000000, PlayerStaminaDecreaseRate = 1.000000, " +
                                        "PlayerAutoHPRegenRate = 1.000000, PlayerAutoHpRegenRateInSleep = 1.000000, " +
                                        "PalStomachDecreaseRate = 1.000000, PalStaminaDecreaseRate = 1.000000, " +
                                        "PalAutoHPRegenRate = 1.000000, PalAutoHpRegenRateInSleep = 1.000000, " +
                                        "BuildObjectDamageRate = 1.000000, BuildObjectDeteriorationDamageRate = 1.000000, " +
                                        "CollectionDropRate = 1.000000, CollectionObjectHpRate = 1.000000, " +
                                        "CollectionObjectRespawnSpeedRate = 1.000000, EnemyDropItemRate = 1.000000, " +
                                        "DeathPenalty = All, bEnablePlayerToPlayerDamage = False, bEnableFriendlyFire = False, " +
                                        "bEnableInvaderEnemy = True, bActiveUNKO = False, bEnableAimAssistPad = True, " +
                                        "bEnableAimAssistKeyboard = False, DropItemMaxNum = 3000, DropItemMaxNum_UNKO = 100, " +
                                        "BaseCampMaxNum = 128, BaseCampWorkerMaxNum = 40, DropItemAliveMaxHours = 1.000000, " +
                                        "bAutoResetGuildNoOnlinePlayers = False, AutoResetGuildTimeNoOnlinePlayers = 72.000000, " +
                                        "GuildPlayerMaxNum = 20, PalEggDefaultHatchingTime = 72.000000, WorkSpeedRate = 1.000000, " +
                                        "bIsMultiplay = False, bIsPvP = False, bCanPickupOtherGuildDeathPenaltyDrop = False, " +
                                        "bEnableNonLoginPenalty = True, bEnableFastTravel = True, bIsStartLocationSelectByMap = True, " +
                                        "bExistPlayerAfterLogout = False, bEnableDefenseOtherGuildPlayer = False, CoopPlayerMaxNum = 4, " +
                                        "ServerPlayerMaxNum = 4, ServerName = \"My PalWorld server\", ServerDescription = \"\", " +
                                        "AdminPassword = \"\", ServerPassword = \"\", PublicPort = 8211, PublicIP = \"\", " +
                                        "RCONEnabled = False, RCONPort = 25575, Region = \"\", bUseAuth = True, " +
                                        "BanListURL = \"https://api.palworldgame.com/api/banlist.txt\")";

        public frmPalworld()
        {
            InitializeComponent();
        }

        private readonly Dictionary<string, TextBox> labelTextBoxMap = new Dictionary<string, TextBox>();

        private void CreateDisplay(Panel container)
        {
            int y = 10;
            int maxLabelWidth = 0;

            // Check if the file path is not empty
            if (!string.IsNullOrEmpty(txtServLoc.Text))
            {
                var str = File.ReadLines(txtServLoc.Text)
                              .SkipWhile(line => !line.Contains("OptionSettings"))
                              .Take(1)
                              .FirstOrDefault();

                // Assuming the settings are enclosed in parentheses
                if (str != null)
                {
                    str = str.Substring(str.IndexOf('(') + 1, str.LastIndexOf(')') - str.IndexOf('(') - 1);
                    string[] values = str.Split(',');

                    foreach (var value in values)
                    {
                        var labelWidth = TextRenderer.MeasureText(value.Split('=')[0].Trim(), container.Font).Width;
                        maxLabelWidth = Math.Max(maxLabelWidth, labelWidth);
                    }

                    foreach (var value in values)
                    {
                        TextBox textBox = CreateControl(container, value.Trim(), y, maxLabelWidth);
                        labelTextBoxMap.Add(value.Split('=')[0].Trim(), textBox);
                        y += 30; // Adjust spacing as needed
                    }
                }
            }
        }

        TextBox CreateControl(Panel container, string value, int y, int maxLabelWidth)
        {
            // Create a label
            Label label = new Label();
            label.Text = value.Split('=')[0].Trim();
            label.AutoSize = true;
            label.Location = new System.Drawing.Point(10, y);

            // Create a textbox
            TextBox textBox = new TextBox();
            textBox.Name = label.Text; // Set a unique name for the textbox
            textBox.Text = value.Split('=')[1].Trim();
            textBox.Location = new System.Drawing.Point(20 + maxLabelWidth, y);
            textBox.Width = 100;

            // Add the label and textbox to the Panel
            container.Controls.Add(label);
            container.Controls.Add(textBox);

            return textBox;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            // Get the current file path
            string filePath = DlgLoad.FileName;

            // Read the existing content
            string content = File.ReadAllText(filePath);

            // Find the start and end indexes of OptionSettings
            int startIndex = content.IndexOf("OptionSettings = (");
            int endIndex = content.IndexOf(")", startIndex);

            // Create a string to concatenate labels and text boxes
            string concatenatedString = "";

            // Update the content based on the text boxes
            foreach (var entry in labelTextBoxMap)
            {
                string label = entry.Key;
                TextBox textBox = entry.Value;

                // Concatenate label and text box values
                concatenatedString += $"{label} = {textBox.Text.Trim()}, ";
            }

            // Remove the trailing comma and space
            concatenatedString = concatenatedString.TrimEnd(',', ' ');

            // Replace the value in the content
            content = content.Remove(startIndex + "OptionSettings = (".Length, endIndex - startIndex - "OptionSettings = (".Length)
                        .Insert(startIndex + "OptionSettings = (".Length, concatenatedString);

            // Write the updated content back to the file without clearing it
            File.WriteAllText(filePath, content);

            MessageBox.Show("Settings saved successfully.");
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            bool x = false;
            bool valide = false;

            do
            {
                rep = DlgLoad.ShowDialog();
                if (DlgLoad.FileName.EndsWith("PalWorldSettings.ini"))
                {
                    x = true;
                    txtServLoc.Text = DlgLoad.FileName.ToString();
                }
                if (rep == DialogResult.OK)
                {
                    if (x == true)
                    {
                        MessageBox.Show("Server settings loaded.");
                        valide = false;
                        if (File.ReadAllText(txtServLoc.Text).Length == 0)
                        {
                            File.WriteAllText(txtServLoc.Text, defaultSettings);
                        }

                        // Clear existing controls in the Panel
                        Pnl1.Controls.Clear();

                        // Pass the Panel as the container
                        CreateDisplay(Pnl1);
                    }
                    else
                    {
                        MessageBox.Show("Wrong file loaded.");
                        valide = true;
                    }
                }
                else if (rep == DialogResult.Cancel)
                {
                    MessageBox.Show("No file loaded.");
                    break;
                }
            } while (valide == true);
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            // Clear existing controls in the Panel
            Pnl1.Controls.Clear();

            // Reset the labelTextBoxMap
            labelTextBoxMap.Clear();
        }
    }
}