using System.Diagnostics;
using System.Drawing.Imaging;

namespace getscreenshot
{
    public partial class Form1 : Form
    {
        Dictionary<string, WindowInfo> winInfo = new Dictionary<string, WindowInfo>();
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Trace.WriteLine("shot");
            IntPtr curr = winInfo[comboBox1.Text].Hwnd;

            // 

            var image = ScreenCapture.CaptureWindow(curr);
            string fileName = DateTime.Now.ToString("yyyyMMdd-HHmmss") + ".jpg";
            image.Save(@"C:\images\" + fileName, ImageFormat.Jpeg);
            image.Dispose();
        }



        private void button2_Click(object sender, EventArgs e)
        {
            Trace.WriteLine("refresh");
            comboBox1.Items.Clear();
            winInfo.Clear();
            var windows = WindowEnumerator.FindAll();
            for (int i = 0; i < windows.Count; i++)
            {
                var window = windows[i];
                //                Trace.WriteLine($@"{i.ToString().PadLeft(3, ' ')}. {window.Title}
                //     {window.Bounds.X}, {window.Bounds.Y}, {window.Bounds.Width}, {window.Bounds.Height}");
                comboBox1.Items.Add(Convert.ToString(window.Hwnd) + " " + window.Title);
                winInfo.Add(Convert.ToString(window.Hwnd) + " " + window.Title, window);
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            Trace.WriteLine(comboBox1.Text);
            Trace.WriteLine(winInfo[comboBox1.Text].Hwnd.ToString());

        }



        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }


    }
}