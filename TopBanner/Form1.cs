
using System.Diagnostics;
using UITimer = System.Windows.Forms.Timer;

namespace TopBanner;

public partial class Form1 : Form {
    private LinkLabel linkLabel;
    private UITimer timer;
    
    public Form1() { 
        InitializeComponent();
        this.FormBorderStyle = FormBorderStyle.None;
        this.TopMost = true;
        this.StartPosition = FormStartPosition.Manual;
        this.Width = Screen.PrimaryScreen.WorkingArea.Width;
        this.Height = 10;
        this.BackColor = Color.SkyBlue;
        this.Location = new Point(0, 0);

        SetupBanner();
        StartUrlPolling();
    }
    
    private void SetupBanner() { 
        linkLabel = new LinkLabel();
               
        linkLabel.Name = "Paperfree";
        linkLabel.Text = ""; 
        linkLabel.Font = new Font("Arial", 12, FontStyle.Bold); 
        linkLabel.AutoSize = true;
        linkLabel.LinkColor = Color.Navy;
        linkLabel.BackColor = Color.Transparent;
        linkLabel.LinkBehavior = LinkBehavior.HoverUnderline;
        
        linkLabel.Click += LinkLabel_Click;
          
        this.Controls.Add(linkLabel);
        
        CenterLinkLabel();
        
        this.PerformLayout();
        this.Refresh();
    }
    
    private void StartUrlPolling() {
        timer = new UITimer() { Interval = 5000 };
        timer.Tick += async (sender, e) => await UpdateChromeUrlAsync();
        timer.Start();
    }
    
    private async Task UpdateChromeUrlAsync() {
        string newUrl = "https://www.google.com"; // todo: get chrome url and pass here
        
        if (!string.IsNullOrEmpty(newUrl) && !newUrl.Contains("paperfree"))
        {
            linkLabel.Text = "https://www.seb.ee";
            CenterLinkLabel(); 
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
        if (!string.IsNullOrEmpty(linkLabel.Text)){
            Process.Start(new ProcessStartInfo(linkLabel.Text) { UseShellExecute = true });
        }
    }
    
    protected override void OnFormClosing(FormClosingEventArgs e) {
        e.Cancel = true; // Prevent form closing
    }
}