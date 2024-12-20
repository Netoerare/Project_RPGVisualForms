using GAMECHARACTER01.Models;
using System;
using System.Media;
using System.Windows.Forms;

namespace GAMECHARACTER01
{
    public partial class Form6 : Form
    {
        private Timer animationTimer; // Timer for controlling C1 attack visibility for 1 second
        private int animationStep = 0; // Step to manage the attack animation state
        private SoundPlayer player;    // Declare SoundPlayer as a class-level variable
        private Warrior warrior;
        private bool isFormLoaded = false;
        public Form6()
        {
            InitializeComponent();
            this.Load += Form6_Load;
            this.FormClosing += Form6_FormClosing; // Corrected the event handler reference
        }

        private void Form6_Load(object sender, EventArgs e)
        {
            if (!isFormLoaded)
            {
                // Initially, set C1 idle and Firewolf idle visible
                pictureBox4.Visible = true;  // c1_idle
            pictureBox3.Visible = true;  // firewolf_idle
            pictureBox5.Visible = false; // c1_atk (initially hidden)
            pictureBox1.Visible = false;
            // firewolf_attack (initially hidden)

            // Initialize the animation timer to manage the attack visibility
            animationTimer = new Timer();
            animationTimer.Interval = 1000; // 1 second delay for C1's attack animation
            animationTimer.Tick += AnimationTimer_Tick;

            // Initialize the SoundPlayer and play music
            player = new SoundPlayer(Properties.Resources.ost);
            player.PlayLooping();  // Play the sound in a loop
            warrior = new Warrior("Inosuke", 1, 100, 10);
            richTextBox1.AppendText(warrior.ToString() + "\n");
                isFormLoaded = true;
            }

        }

        private void Form6_FormClosing(object sender, FormClosingEventArgs e)
        {
            // Stop the music when the form is closed
            player.Stop();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // When the attack button is clicked, show C1 attack for 1 second
            pictureBox4.Visible = false;  // Hide C1 idle
            pictureBox5.Visible = true;   // Show C1 attack
            pictureBox3.Visible = true;   // Keep Firewolf idle visible
            pictureBox1.Visible = true;

            int baseDamage = warrior.Strength * 2;
            Random rnd = new Random();
            bool criticalHit = rnd.Next(100) < 20;
            int finalDamage = criticalHit ? baseDamage * 2 : baseDamage;
            warrior.Attack();
            string attackMessage = $"{warrior.Name} attacks and deals {finalDamage} damage{(criticalHit ? " with a critical hit!" : ".")}";
            richTextBox1.AppendText(attackMessage + "\n");
            // Start the animation timer to reset after 1 second
            animationTimer.Start();
        }

        private void AnimationTimer_Tick(object sender, EventArgs e)
        {
            // After 1 second, return to idle images
            pictureBox5.Visible = false; // Hide C1 attack
            pictureBox4.Visible = true;  // Show C1 idle
            pictureBox3.Visible = true;  // Show Firewolf idle
            pictureBox1 .Visible = false;

            // Stop the timer after it has completed
            animationTimer.Stop();
        }

        // If you don't need the following click events, you can remove them.
        private void pictureBox1_Click(object sender, EventArgs e)
        {
            // Implement any necessary actions for this click if needed
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            // Implement any necessary actions for this click if needed
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            // Implement any necessary actions for this click if needed
        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            // Implement any necessary actions for this click if needed
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            // Implement any necessary actions for this click if needed
        }

        private void pictureBox1_Click_1(object sender, EventArgs e)
        {

        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {
            richTextBox1.SelectionStart = richTextBox1.Text.Length;
            richTextBox1.ScrollToCaret();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            warrior.LevelUp();

            richTextBox1.AppendText($"{warrior.Name} has leveled up! New Level: {warrior.Level}, New Strength: {warrior.Strength}, New Health: {warrior.Health}, New Armor: {warrior.Armor}\n");

        }
    }
}
