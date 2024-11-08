
using System.Diagnostics;

namespace TopBanner;

public partial class Form1 : Form
{
    public Form1()
    { 
        InitializeComponent();
        
     // Set form properties
     this.FormBorderStyle = FormBorderStyle.None;
     this.TopMost = true;
     this.StartPosition = FormStartPosition.Manual;
     this.Width = Screen.PrimaryScreen.WorkingArea.Width;
     this.Height = 10;
     this.BackColor = Color.SkyBlue;
     this.Location = new Point(0, 0);  // Position at the top of the screen
    }
    
    private void BannerForm_Load(object sender, EventArgs e)
    {
        LinkLabel linkLabel = new LinkLabel();
               
        linkLabel.BackColor = Color.Red; 
        linkLabel.Text = "I am a Dynamic LinkLabel"; 
        linkLabel.Name = "DynamicLinkLabel";
        linkLabel.LinkColor = Color.Red;
        linkLabel.AutoSize = false;
        linkLabel.Font = new Font("Georgia", 16); 
        
       // Center the LinkLabel horizontally within the banner
      linkLabel.Location = new Point((this.Width - linkLabel.Width) / 2, (this.Height - linkLabel.Height) / 2);
       linkLabel.Click += LinkLabel_Click;
       
       this.Controls.Add(linkLabel);
    }
    
    // Open a URL when the link label is clicked
    private void LinkLabel_Click(object sender, EventArgs e)
    {
        string url = "https://www.seb.ee"; // Replace with your desired URL
        Process.Start(new ProcessStartInfo(url) { UseShellExecute = true });
    }
    
    // Prevent closing of form
    protected override void OnFormClosing(FormClosingEventArgs e)
    {
        e.Cancel = true; // Prevent the form from being closed
    }
    
}