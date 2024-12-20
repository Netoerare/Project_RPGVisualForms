using GAMECHARACTER01.Models;
using System;
using System.Media;
using System.Windows.Forms;

namespace GAMECHARACTER01
{
    public partial class Form7 : Form
    {
        private Timer animationTimer; // Timer for controlling C1 attack visibility for 1 second
        private int animationStep = 0; // Step to manage the attack animation state
        private SoundPlayer player;    // Declare SoundPlayer as a class-level variable
        private Mage mage;
        private bool isFormLoaded = false;

        public Form7()
        {
            InitializeComponent();
            this.Load += Form7_Load;
            this.FormClosing += Form7_FormClosing; // Added FormClosing event handler
        }

        private void Form7_Load(object sender, EventArgs e)
        {
            if (!isFormLoaded)
            {
                // Initially, set C1 idle and Firewolf idle visible
                pictureBox4.Visible = true;  // c1_idle
            pictureBox1.Visible = true;  // firewolf_idle
            pictureBox5.Visible = false; // c1_atk (initially hidden)
            pictureBox2.Visible = false;                              // firewolf_attack (initially hidden)

            // Initialize the animation timer to manage the attack visibility
            animationTimer = new Timer();
            animationTimer.Interval = 1000; // 1 second delay for C1's attack animation
            animationTimer.Tick += AnimationTimer_Tick;

            // Initialize the SoundPlayer and play music
            player = new SoundPlayer(Properties.Resources.ost);
            player.PlayLooping();  // Play the sound in a loop
            mage = new Mage("Natsu", 1, 100, 50, 15);
            richTextBox1.AppendText(mage.ToString() + "\n");
                isFormLoaded = true;
            }
        }

        private void Form7_FormClosing(object sender, FormClosingEventArgs e)
        {
            // Stop the music when the form is closed
            player.Stop();
        }

        private void AnimationTimer_Tick(object sender, EventArgs e)
        {
            // After 1 second, return to idle images
            animationStep++;
            if (animationStep == 1)
            {
                pictureBox5.Visible = false; // Hide C1 attack
                // Hide Firewolf attack
                pictureBox4.Visible = true;  // Show C1 idle
                pictureBox1.Visible = true;  // Show Firewolf idle
                pictureBox2.Visible = false;
                // Stop the timer after it has completed
                animationTimer.Stop();
                animationStep = 0;  // Reset the animation step
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // When the attack button is clicked, show C1 attack for 1 second
            pictureBox4.Visible = false;  // Hide C1 idle
            pictureBox5.Visible = true;   // Show C1 attack
            pictureBox1.Visible = true;   // Keep Firewolf idle visible
            pictureBox2.Visible = true;
            mage.Attack();
            int baseMagicDamage = mage.Intelligence * 3 + mage.SpellPower;

            // Randomize the burning effect (25% chance)
            Random rnd = new Random();
            bool burningEffect = rnd.Next(100) < 25;

            // Calculate final damage and apply burning effect using ternary operator
            int finalDamage = baseMagicDamage;
            string attackMessage = $"{mage.Name} casts a spell and deals {finalDamage} magic damage" +
                                   (burningEffect ? ", applying a burning effect!" : ".");

            // Display the attack message in the rich text box
            richTextBox1.AppendText(attackMessage + "\n");

            animationTimer.Start();
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            // Implement any necessary actions for enemy attack
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            // Implement any necessary actions for C1 idle (if needed)
        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            // Implement any necessary actions for C1 attack (if needed)
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            // Implement any necessary actions for enemy (if needed)
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {
            richTextBox1.SelectionStart = richTextBox1.Text.Length;
            richTextBox1.ScrollToCaret();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            mage.LevelUp();
            // Display updated mage stats in the RichTextBox
            richTextBox1.AppendText($"{mage.Name} has leveled up! New Level: {mage.Level}, New Intelligence: {mage.Intelligence}, New Mana: {mage.Mana}, New SpellPower: {mage.SpellPower}\n");
        }
    }
}
