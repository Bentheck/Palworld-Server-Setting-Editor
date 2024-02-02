using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PalWorld_Server_Edit
{
    public partial class frmLimits : Form
    {
        public frmLimits()
        {
            InitializeComponent();
            AddTextToTextBox();
        }

        private void AddTextToTextBox()
        {
            // Multiline TextBox to display the text
            TextBox textBox = new TextBox();
            textBox.Multiline = true;
            textBox.ReadOnly = true;
            textBox.ScrollBars = ScrollBars.Vertical;
            textBox.Dock = DockStyle.Fill; // Fill the entire form
            textBox.Font = new System.Drawing.Font("Arial", 10); // Adjust font if needed

            // Add the text to the TextBox
            textBox.Text = @"
Difficulty	Adjusts the overall difficulty of the game.
Has no affect on dedicated servers
Valid options are:
    - Casual
    - Normal
    - Hard

DayTimeSpeedRate (Modifies the speed of in-game time during the day.)
Default is 1.000000
Changing this value to 2.000000 will make the day time go twice as fast.

NightTimeSpeedRate (Modifies the speed of in-game time during the night.)
Default is 1.000000
Changing this value to 2.000000 will make the night time go twice as fast.

ExpRate	(Changes the experience gain rate for both players and creatures.)
Default is 1.000000
Changing this value to 2.000000 will mean you get twice as much as normal EXP.

PalCaptureRate (Adjusts the rate at which Pal creatures can be captured.)
Default is 1.000000
Changing this value to 2.000000 will mean you be twice as likely to catch a Pal than normal.

PalSpawnNumRate (Adjusts the rate at which Pal creatures spawn.)
Default is 1.000000
Changing this value to 2.000000 will mean twice as many Pals running around

PalDamageRateAttack (Fine-tunes Pal creature damage dealt.)
Default is 1.000000
Changing this value to 2.000000 will mean twice as much damage coming from Pals

PalDamageRateDefense (Fine-tunes Pal creature damage received.)
Default is 1.000000

PlayerDamageRateAttack (Fine-tunes player damage dealt.)
Default is 1.000000

PlayerDamageRateDefense	(Fine-tunes player damage received.)
Default is 1.000000

PlayerStomachDecreaceRate (Adjusts the rate at which the player's stomach decreases.)
Default is 1.000000

PlayerStaminaDecreaceRate (Adjusts the rate at which the player's stamina decreases.)
Default is 1.000000

PlayerAutoHPRegeneRate (Adjusts the rate of automatic player health regeneration.)
Default is 1.000000

PlayerAutoHpRegeneRateInSleep (Adjusts the rate of automatic player health regeneration during sleep.)
Default is 1.000000

PalStomachDecreaceRate (Adjusts the rate at which Pal creature stomach decreases.)
Default is 1.000000

PalStaminaDecreaceRate (Adjusts the rate at which Pal creature stamina decreases.)
Default is 1.000000

PalAutoHPRegeneRate (Adjusts the rate of automatic Pal creature health regeneration.)
Default is 1.000000

PalAutoHpRegeneRateInSleep (Adjusts the rate of automatic Pal creature health regeneration during sleep.)
Default is 1.000000

BuildObjectDamageRate (Adjusts the rate at which built objects take damage.)
Default is 1.000000

BuildObjectDeteriorationDamageRate (Adjusts the rate at which built objects deteriorate.)
Default is 1.000000

CollectionDropRate (Adjusts the drop rate of collected items.)
Default is 1.000000

CollectionObjectHpRate (Adjusts the health of collected objects.)
Default is 1.000000

CollectionObjectRespawnSpeedRate (Adjusts the respawn speed of collected objects.)
Default is 1.000000

EnemyDropItemRate (Adjusts the drop rate of items from defeated enemies.)
Default is 1.000000

DeathPenalty (Defines the penalty upon player death (e.g., All, None).)
    - None : No lost
    - Item : Lost item without equipment
    - ItemAndEquipment : Lost item and equipment
    - All : Lost All item, equipment, pal(in inventory)

GuildPlayerMaxNum (Sets the maximum number of players in a guild.)
Default is 20.000000

PalEggDefaultHatchingTime (Sets the default hatching time for Pal eggs.)
Default is 72.000000

ServerPlayerMaxNum (Maximum number of people who can join the server)
1-32 (Maxinum players is 32)

ServerName (Server name)

ServerDescription (Server description)

AdminPassword (AdminPassword in game and for RCON access)

ServerPassword (Set the server password.)

PublicPort (Public port number)

PublicIP (Public IP)

RCONEnabled (Enable RCON)

RCONPort (Port number for RCON)";

            // Add the TextBox to the form's controls
            this.Controls.Add(textBox);
        }
    }
}
