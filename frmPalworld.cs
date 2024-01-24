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

        private readonly Dictionary<string, Control> labelControlMap = new Dictionary<string, Control>();
        private bool isFirstLoad = true;

        private void CreateDisplay(Panel container)
        {
            int y = 10;
            int maxLabelWidth = 0;

            if (!string.IsNullOrEmpty(txtServLoc.Text))
            {
                var str = File.ReadLines(txtServLoc.Text)
                              .SkipWhile(line => !line.Contains("OptionSettings"))
                              .Take(1)
                              .FirstOrDefault();

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
                        string label = value.Split('=')[0].Trim();
                        Control control;

                        if (!labelControlMap.ContainsKey(label))
                        {
                            // Create a control only if it does not exist in the map
                            control = CreateControl(container, value.Trim(), y, maxLabelWidth);
                            labelControlMap.Add(label, control);
                        }
                        else
                        {
                            // Retrieve the existing control from the map
                            control = labelControlMap[label];
                        }

                        // Update the value of the control
                        UpdateControlValue(control, value.Split('=')[1].Trim());

                        y += 30; // Adjust spacing as needed
                    }
                }
            }
        }

        private Control CreateControl(Panel container, string value, int y, int maxLabelWidth)
        {
            // Create a label
            Label label = new Label();
            label.Text = value.Split('=')[0].Trim();
            label.AutoSize = true;
            label.Location = new System.Drawing.Point(10, y);

            // Create a control (textbox or dropdown)
            Control control = CreateControlBasedOnValue(value.Split('=')[1].Trim());
            control.Name = label.Text; // Set a unique name for the control
            control.Location = new System.Drawing.Point(20 + maxLabelWidth, y);
            control.Width = 100;

            // Add the label and control to the Panel
            container.Controls.Add(label);
            container.Controls.Add(control);

            return control;
        }

        private Control CreateControlBasedOnValue(string value)
        {
            // Check if the value is true or false
            if (value.Equals("True", StringComparison.OrdinalIgnoreCase) || value.Equals("False", StringComparison.OrdinalIgnoreCase))
            {
                // Create a dropdown for true/false
                ComboBox dropdown = new ComboBox();
                dropdown.DropDownStyle = ComboBoxStyle.DropDownList;
                dropdown.Items.Add("True");
                dropdown.Items.Add("False");
                dropdown.SelectedIndex = value.Equals("True", StringComparison.OrdinalIgnoreCase) ? 0 : 1;
                return dropdown;
            }
            else
            {
                // Create a textbox
                TextBox textBox = new TextBox();
                return textBox;
            }
        }

        private void UpdateControlValue(Control control, string value)
        {
            // Update the value of the control based on its type
            if (control is TextBox textBox)
            {
                textBox.Text = value;
            }
            else if (control is ComboBox dropdown)
            {
                dropdown.SelectedIndex = value.Equals("True", StringComparison.OrdinalIgnoreCase) ? 0 : 1;
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            string filePath = DlgLoad.FileName;
            string content = File.ReadAllText(filePath, System.Text.Encoding.UTF8); // Explicitly specify UTF-8 encoding
            int startIndex = content.IndexOf("OptionSettings = (");
            int endIndex = content.IndexOf(")", startIndex);

            string concatenatedString = "";

            foreach (var entry in labelControlMap)
            {
                string label = entry.Key;
                Control control = entry.Value;
                concatenatedString += $"{label} = {GetControlValue(control)}, ";
            }

            concatenatedString = concatenatedString.TrimEnd(',', ' ');
            content = content.Remove(startIndex + "OptionSettings = (".Length, endIndex - startIndex - "OptionSettings = (".Length)
                        .Insert(startIndex + "OptionSettings = (".Length, concatenatedString);

            File.WriteAllText(filePath, content, System.Text.Encoding.UTF8); // Explicitly specify UTF-8 encoding

            MessageBox.Show("Settings saved successfully.");
        }

        private string GetControlValue(Control control)
        {
            // Return the value of the control based on its type
            if (control is TextBox textBox)
            {
                return textBox.Text.Trim();
            }
            else if (control is ComboBox dropdown)
            {
                return dropdown.SelectedItem.ToString();
            }
            else
            {
                return string.Empty;
            }
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            rep = DlgLoad.ShowDialog();

            if (rep == DialogResult.OK)
            {
                if (DlgLoad.FileName.EndsWith("PalWorldSettings.ini"))
                {
                    txtServLoc.Text = DlgLoad.FileName;

                    if (isFirstLoad)
                    {
                        // Clear existing controls in the Panel on the first load
                        Pnl1.Controls.Clear();
                    }

                    LoadAndDisplaySettings();
                    MessageBox.Show("Server settings loaded.");

                    isFirstLoad = false;
                }
                else
                {
                    MessageBox.Show("Wrong file loaded.");
                }
            }
            else if (rep == DialogResult.Cancel)
            {
                MessageBox.Show("No file loaded.");
            }
        }

        private void LoadAndDisplaySettings()
        {
            if (File.ReadAllText(txtServLoc.Text).Length == 0)
            {
                File.WriteAllText(txtServLoc.Text, defaultSettings, System.Text.Encoding.UTF8); // Explicitly specify UTF-8 encoding
            }

            CreateDisplay(Pnl1);
        }
    }
}