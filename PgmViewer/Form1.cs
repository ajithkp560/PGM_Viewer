using System;
using System.Drawing;
using System.Windows.Forms;
using System.IO;
using System.Drawing.Imaging;
namespace PgmViewer
{
    public partial class Form1 : Form
    {
        string fname;
        FileStream input;
        StreamReader reader;
        BinaryReader br;
        public Form1()
        {
            InitializeComponent();
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult res;
            using(OpenFileDialog ofd = new OpenFileDialog())
            {
                res = ofd.ShowDialog();
                fname = ofd.FileName;
            }
            if (res == DialogResult.OK)
            {
                if (fname == string.Empty)
                {
                    MessageBox.Show("Invalid file name", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    SetPicture(fname);
                }   
            }
        }

        public void SetPicture(String fname)
        {
            input = new FileStream(fname, FileMode.Open, FileAccess.Read);
            reader = new StreamReader(input);
            br = new BinaryReader(input);
            byte b = 0;
            while (b != 10)
            {
                b = br.ReadByte();
            }
            b = 0;
            string line = "";
            while (b != 10)
            {
                b = br.ReadByte();
                char c = (char)b;
                line += c;
            }
            string[] xxx = line.Split(' ');
            int width = int.Parse(xxx[0]);
            int height = int.Parse(xxx[1]);
            b = 0;
            line = "";
            while (b != 10)
            {
                b = br.ReadByte();
                char c = (char)b;
                line += c;
            }
            int max = int.Parse(line);
            pictureBox1.Width = width;
            pictureBox1.Height = height;
            Bitmap bitmap = new Bitmap(width, height);
            try
            {
                int x = 0;
                for (Int32 i = 0; i < height; i++)
                {
                    for (Int32 j = 0; j < width; j++)
                    {
                        b = br.ReadByte();
                        int val = b;
                        Color c = Color.FromArgb(val, val, val);
                        bitmap.SetPixel(j, i, c);
                    }
                }
                pictureBox1.Image = bitmap;
                pictureBox1.Dock = DockStyle.Top|DockStyle.Left;
                pictureBox1.Refresh();
            }
            catch (Exception err)
            {
                MessageBox.Show(err.ToString(), "Error");
            }
            br.Close();
            reader.Close();
        }

        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "Images|*.png;*.bmp;*.jpg";
            ImageFormat format = ImageFormat.Png;
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                string ext = System.IO.Path.GetExtension(sfd.FileName);
                switch (ext)
                {
                    case ".jpg":
                        format = ImageFormat.Jpeg;
                        break;
                    case ".bmp":
                        format = ImageFormat.Bmp;
                        break;
                }
                pictureBox1.Image.Save(sfd.FileName, format);
            }
        }
    }
}
