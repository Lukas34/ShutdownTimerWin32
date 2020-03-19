﻿using System;
using System.Windows.Forms;

namespace ShutdownTimerWin32
{
    public partial class Menu : Form
    {
        public Menu()
        {
            InitializeComponent();
        }

        private void Menu_Load(object sender, EventArgs e)
        {
            version_label.Text = "v" + Application.ProductVersion.Remove(Application.ProductVersion.LastIndexOf(".")); // Display current version 
        }

        private void Title_label_Click(object sender, EventArgs e)
        {
            MessageBox.Show("THE SOFTWARE IS PROVIDED 'AS IS', WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE." +
                    "\n\nBy using this software you agree to the above mentioned terms as this software is licensed under the MIT License. For more information visit: https://opensource.org/licenses/MIT.", "MIT License", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void Github_pb_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("https://github.com/Lukas34/ShutdownTimerWin32"); // Show GitHub page
        }

        private void Start_button_Click(object sender, EventArgs e)
        {
            if (RunChecks() == true)
            {
                // Disable controls
                start_button.Enabled = false;
                action_group.Enabled = false;
                time_group.Enabled = false;

                // Hide
                this.Hide();

                // Show countdown
                using (Countdown countdown = new Countdown
                {
                    hours = Convert.ToInt32(hours_updown.Value),
                    minutes = Convert.ToInt32(minutes_updown.Value),
                    seconds = Convert.ToInt32(seconds_updown.Value),
                    action = action_combo.Text
                })
                {
                    if (background_check.Checked == true) { countdown.UI = false; } // disables UI updates
                    countdown.Owner = this;
                    countdown.ShowDialog();
                    Application.Exit(); // Exit application after countdown is closed
                }
            }
            else
            {
                MessageBox.Show("The following error(s) occurred:\n\n" + CheckResult + "Please try to resolve the(se) problem(s) and try again.", "There seems to be a problem!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public string CheckResult;

        /// <summary>
        /// Checks user input before further processing
        /// </summary>
        /// <returns>Result of checks</returns>
        private bool RunChecks()
        {
            bool err_tracker = true; // if anything goes wrong the tracker will be set to false
            string err_message = ""; // error messages will append to this

            if (!action_combo.Items.Contains(action_combo.Text))
            {
                err_tracker = false;
                err_message += "Please select a valid action from the dropdown menu!\n\n";
            }

            if (hours_updown.Value == 0 && minutes_updown.Value == 0 && seconds_updown.Value == 0)
            {
                err_tracker = false;
                err_message += "The timer cannot start at 0!\n\n";
            }

            CheckResult = err_message;
            return err_tracker;
        }
    }
}
