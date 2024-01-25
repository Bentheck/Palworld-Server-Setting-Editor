using System.Diagnostics;
using System.IO.Compression;
using System.IO;
using System.Net;
using System.Runtime.CompilerServices;
using Newtonsoft.Json.Linq;

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

        public string vers = "V1.2.2";

        public frmPalworld()
        {
            InitializeComponent();
            Load += frmPalworld_Load;
        }

        private async void frmPalworld_Load(object sender, EventArgs e)
        {
            await CheckForUpdates();
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
            string filePath = txtServLoc.Text;

            // Check if the file exists
            if (!File.Exists(filePath))
            {
                MessageBox.Show("File does not exist.");
                return;
            }

            // Read content from the file
            string content = File.ReadAllText(filePath, System.Text.Encoding.UTF8); // Explicitly specify UTF-8 encoding

            // Check if the content contains "OptionSettings = ("
            int startIndex = content.IndexOf("OptionSettings = (");
            if (startIndex == -1)
            {
                MessageBox.Show("Invalid file format. OptionSettings not found. Try running as administrator");
                return;
            }

            int endIndex = content.IndexOf(")", startIndex);

            string concatenatedString = "";

            foreach (var entry in labelControlMap)
            {
                string label = entry.Key;
                Control control = entry.Value;
                concatenatedString += $"{label} = {GetControlValue(control)}, ";
            }

            concatenatedString = concatenatedString.TrimEnd(',', ' ');

            try
            {
                // Write the content back to the file
                File.WriteAllText(filePath, content.Remove(startIndex + "OptionSettings = (".Length, endIndex - startIndex - "OptionSettings = (".Length)
                            .Insert(startIndex + "OptionSettings = (".Length, concatenatedString), System.Text.Encoding.UTF8); // Explicitly specify UTF-8 encoding

                MessageBox.Show("Settings saved successfully.");
            }
            catch (UnauthorizedAccessException)
            {
                MessageBox.Show("Please run the editor as administrator due to your file location.");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}");
            }
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
            string filePath = DlgLoad.FileName;

            if (rep == DialogResult.OK)
            {
                if (Path.GetFileName(filePath).Trim() == "PalWorldSettings.ini")
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
                    MessageBox.Show("Wrong file loaded. Please select a file named PalWorldSettings.ini.");
                }
            }
            else if (rep == DialogResult.Cancel)
            {
                MessageBox.Show("No file loaded.");
            }
        }

        private void LoadAndDisplaySettings()
        {
            string a = File.ReadAllText(DlgLoad.FileName).ToString();


            if ( string.IsNullOrEmpty(a))
            {
                File.WriteAllText(DlgLoad.FileName, defaultSettings, System.Text.Encoding.UTF8); // Explicitly specify UTF-8 encoding
            }

            CreateDisplay(Pnl1);
        }

        private async Task<string> GetLatestVersionFromGitHub()
        {
            string repoOwner = "Bentheck";
            string repoName = "Palworld-Server-Setting-Editor";

            using (HttpClient client = new HttpClient())
            {
                string apiUrl = $"https://api.github.com/repos/{repoOwner}/{repoName}/releases/latest";
                client.DefaultRequestHeaders.Add("User-Agent", "request");
                string json = await client.GetStringAsync(apiUrl);

                JObject releaseInfo = JObject.Parse(json);
                string latestVersion = releaseInfo["tag_name"].ToString();

                return latestVersion;
            }
        }

        private bool IsUpdateAvailable(string currentVersion, string latestVersion)
        {
            // Implement your version comparison logic here
            // Example: Compare major, minor, build, etc.
            return string.Compare(currentVersion, latestVersion) < 0;
        }


        private async Task CheckForUpdates()
        {
            string currentVersion = vers; // Replace with your actual version
            string latestVersion = await GetLatestVersionFromGitHub(); // Use await here

            if (IsUpdateAvailable(currentVersion, latestVersion))
            {
                // Ensure UI update on the UI thread
                Invoke((MethodInvoker)delegate
                {
                    lblUpdate.Visible = true;
                    btnUpdate.Visible = true;
                });
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            Process.Start(new ProcessStartInfo("cmd", $"/c start https://github.com/Bentheck/Palworld-Server-Setting-Editor/releases") { CreateNoWindow = true });
            Application.Exit();
        }
    }
}