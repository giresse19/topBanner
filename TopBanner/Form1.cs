using System;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Automation;
using System.Windows.Forms;
using UITimer = System.Windows.Forms.Timer;

namespace TopBanner {
    public partial class Form1 : Form
    {
        private LinkLabel linkLabel;
        private UITimer timer;

        public Form1()
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.None;
            this.TopMost = true;
            this.StartPosition = FormStartPosition.Manual;
            this.Width = Screen.PrimaryScreen.WorkingArea.Width;
            this.Height = 50;
            this.BackColor = Color.SkyBlue;
            this.Location = new Point(0, 0);

            SetupBanner();
            StartUrlPolling();
        }

        private void SetupBanner()
        {
            linkLabel = new LinkLabel
            {
                Name = "Paperfree",
                Text = "",
                Font = new Font("Arial", 12, FontStyle.Bold),
                AutoSize = true,
                LinkColor = Color.Navy,
                BackColor = Color.Transparent,
                LinkBehavior = LinkBehavior.HoverUnderline
            };

            linkLabel.Click += LinkLabel_Click;

            this.Controls.Add(linkLabel);

            CenterLinkLabel();
            
            this.PerformLayout();
            this.Refresh();
        }

        private void StartUrlPolling()
        {
            timer = new UITimer { Interval = 5000 };
            timer.Tick += async (sender, e) => await UpdateChromeUrlAsync();
            timer.Start();
        }

        private string UpdateChromeUrl() {
            
            Process[] procsChrome = Process.GetProcessesByName("chrome");
            
            foreach (Process chrome in procsChrome) {
               
                if (chrome.MainWindowHandle == IntPtr.Zero)
                    continue;

                AutomationElement element = AutomationElement.FromHandle(chrome.MainWindowHandle);
                if (element == null)
                {
                    Console.WriteLine("Could not find a valid main window handle for Chrome.");
                    return "";
                }
                
                Condition conditions = new AndCondition(
                    new PropertyCondition(AutomationElement.ProcessIdProperty, chrome.Id),
                    new PropertyCondition(AutomationElement.IsControlElementProperty, true),
                    new PropertyCondition(AutomationElement.IsContentElementProperty, true),
                    new PropertyCondition(AutomationElement.ControlTypeProperty, ControlType.Edit));

                AutomationElement windowElement = element.FindFirst(TreeScope.Descendants, conditions);
                return ((ValuePattern)windowElement.GetCurrentPattern(ValuePattern.Pattern)).Current.Value as string;
            }

            return "";
        }


        private async Task UpdateChromeUrlAsync() {
           string newUrl =  UpdateChromeUrl();
           Console.WriteLine("URL gotten: " + newUrl);
            
            if (!string.IsNullOrEmpty(newUrl) && !newUrl.ToLowerInvariant().Contains("paperfree"))
            {
                linkLabel.Text = "https://www.seb.ee";
                // CenterLinkLabel();
            }
        }

        private void CenterLinkLabel() {
            linkLabel.Location = new Point(
                (this.ClientSize.Width - linkLabel.Width) / 2,
                (this.ClientSize.Height - linkLabel.Height) / 2
            );
        }

        protected override void OnLoad(EventArgs e) {
            base.OnLoad(e);
            CenterLinkLabel();
            this.PerformLayout();
            this.Refresh();
        }

        private void LinkLabel_Click(object sender, EventArgs e) {
            if (!string.IsNullOrEmpty(linkLabel.Text))
            {
                Process.Start(new ProcessStartInfo(linkLabel.Text) { UseShellExecute = true });
            }
        }

        protected override void OnFormClosing(FormClosingEventArgs e) {
            e.Cancel = true; // Prevent form closing
        }
    }
}
