
using System.Diagnostics;

namespace TopBanner;

public partial class Form1 : Form {
    private LinkLabel linkLabel;
   // private Timer timer;
    
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
    // StartUrlPolling();
    }
    
    private void SetupBanner() { 
        linkLabel = new LinkLabel();
               
        linkLabel.Name = "Paperfree";
        linkLabel.Text = "Return to Paperfree"; 
        linkLabel.Font = new Font("Arial", 12, FontStyle.Bold); 
        linkLabel.AutoSize = true;
        linkLabel.LinkColor = Color.Navy;
        linkLabel.BackColor = Color.Transparent;
        linkLabel.LinkBehavior = LinkBehavior.HoverUnderline;
        
        linkLabel.Click += LinkLabel_Click ;
          
        this.Controls.Add(linkLabel);
        
        CenterLinkLabel();
        
        this.PerformLayout();
        this.Refresh();
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
        string url = "https://www.example.com"; // Replace with your desired URL
        Process.Start(new ProcessStartInfo(url) { UseShellExecute = true });
    }
    
    protected override void OnFormClosing(FormClosingEventArgs e) {
        e.Cancel = true; // Prevent form closing
    }
    
}