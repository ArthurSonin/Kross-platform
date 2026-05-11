using System.Drawing;
using System.Windows.Forms;

namespace WinFormsApp7
{
    public partial class Form1 : Form
    {
        // TASK 1
        bool isDrawing = false;
        Point lastPoint;
        Color currentColor = Color.Black;
        Bitmap? map;
        Graphics? g;
        // TASK 2
        Graphics? g2;
        Image? originalImage = null; // Початкове зображення
        Image? rotatedImage = null;  // Повернуте зображення
        int currentAngle; // Поточний кут повороту
        // TASK 3
        Graphics? g3;
        List<Shape> shapesList = new List<Shape>();
        Random rnd = new Random();
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            map = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            g = Graphics.FromImage(map);
            g.Clear(Color.White);
            pictureBox1.Image = map;
            map = new Bitmap(pictureBox2.Width, pictureBox2.Height);
            g2 = Graphics.FromImage(map);
            g2.Clear(Color.White);
            pictureBox2.Image = map;
            map = new Bitmap(pictureBox3.Width, pictureBox3.Height);
            g3 = Graphics.FromImage(map);
            g3.Clear(Color.White);
            pictureBox3.Image = map;
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton1.Checked) currentColor = Color.Red;
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton2.Checked) currentColor = Color.Blue;
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton3.Checked) currentColor = Color.Green;
        }

        private void radioButton4_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton4.Checked) currentColor = Color.Black;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            g?.Clear(Color.White);
            pictureBox1.Invalidate();
        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            isDrawing = true;
            lastPoint = e.Location; // Запам'ятовуємо, де почали
        }

        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            isDrawing = false;
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            if (isDrawing)
            {
                Pen pen = new Pen(currentColor, 5f);

                g?.DrawLine(pen, lastPoint, e.Location);

                lastPoint = e.Location;

                pictureBox1.Invalidate();
            }
        }

        // TASK 2

        private Bitmap? RotateImage(Image img, float angle)
        {
            if (img == null) return null;

            Bitmap rotatedBmp = new Bitmap(img.Width, img.Height);
            rotatedBmp.SetResolution(img.HorizontalResolution, img.VerticalResolution);

            using (Graphics g = Graphics.FromImage(rotatedBmp))
            {
                g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;

                g.TranslateTransform((float)img.Width / 2, (float)img.Height / 2);


                g.RotateTransform(angle);


                g.TranslateTransform(-(float)img.Width / 2, -(float)img.Height / 2);

                g.DrawImage(img, new Point(0, 0));
            }
            return rotatedBmp;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "HI (*.bmp;*.jpg;*.png)|*.bmp;*.jpg;*.png";
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                rotatedImage = null;
                originalImage = Image.FromFile(openFileDialog1.FileName);
                pictureBox2.BackColor = Color.White;
                pictureBox2.Image = originalImage;
                currentAngle = 0;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (rotatedImage != null)
            {
                rotatedImage = RotateImage(rotatedImage, 45f);
                pictureBox2.Image = rotatedImage;
            }
            else if (originalImage != null)
            {
                rotatedImage = RotateImage(originalImage, 45f);
                pictureBox2.Image = rotatedImage;
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (pictureBox2.Image != null)
            {
                saveFileDialog1.Filter = "PNG Image|*.png|JPEG Image|*.jpg|Bitmap Image|*.bmp";
                saveFileDialog1.Title = "Зберегти зображення";

                if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    string fileName = saveFileDialog1.FileName;
                    pictureBox2.Image.Save(fileName);
                }
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            shapesList.Clear();

            int count = textBox1.Text != "" ? int.Parse(textBox1.Text) : 5;

            for (int i = 0; i < count; i++)
            {
                Shape newShape;
                int type = rnd.Next(0, 3);

                if (type == 0) newShape = new MyRectangle();
                else if (type == 1) newShape = new MyTriangle();
                else newShape = new MyPentagon();

                pictureBox3.BackColor = Color.White;
                newShape.X = rnd.Next(50, pictureBox3.Width - 50);
                newShape.Y = rnd.Next(50, pictureBox3.Height - 50);
                newShape.Radius = rnd.Next(20, 60);
                newShape.Color = Color.FromArgb(rnd.Next(256), rnd.Next(256), rnd.Next(256));

                shapesList.Add(newShape);
            }

            pictureBox3.Invalidate();
        }

        private void pictureBox3_Paint(object sender, PaintEventArgs e)
        {
            foreach (var shape in shapesList)
            {
                shape.Draw(e.Graphics);
            }
        }
    }
}
