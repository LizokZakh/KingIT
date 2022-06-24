using System;
using System.Drawing;
using System.IO;
using System.Windows;
using System.Windows.Media.Imaging;


namespace KingIT
{
    /// <summary>
    /// Логика взаимодействия для Captcha.xaml
    /// </summary>
    public partial class Captcha : Window
    {
        private string text;

        public Captcha()
        {
            InitializeComponent();
        }

        public Bitmap CreateImage(int Width, int Height)
        {
            Random rnd = new Random();

            //Создадим изображение
            Bitmap result = new Bitmap(Width, Height);

            //Вычислим позицию текста
            int Xpos = rnd.Next(0, Width - 90);
            int Ypos = rnd.Next(15, Height - 30);

            //Добавим различные цвета
            System.Drawing.Brush[] colors = {
            System.Drawing.Brushes.Black,
            System.Drawing.Brushes.Red,
            System.Drawing.Brushes.RoyalBlue,
            System.Drawing.Brushes.Green };

            //Укажем где рисовать
            Graphics g = Graphics.FromImage((System.Drawing.Image)result);

            //Пусть фон картинки будет серым
            g.Clear(System.Drawing.Color.Gray);

            //Сгенерируем текст
            text = String.Empty;
            string ALF = "1234567890QWERTYUIOPASDFGHJKLZXCVBNM";
            for (int i = 0; i < 5; ++i)
                text += ALF[rnd.Next(ALF.Length)];

            //Нарисуем сгенирируемый текст
            g.DrawString(text,
            new Font("Arial", 15),
            colors[rnd.Next(colors.Length)],
            new PointF(Xpos, Ypos));

            //Добавим немного помех
            //Линии из углов
            g.DrawLine(Pens.Black,
            new System.Drawing.Point(0, 0),
            new System.Drawing.Point(Width - 1, Height - 1));
            g.DrawLine(Pens.Black,
            new System.Drawing.Point(0, Height - 1),
            new System.Drawing.Point(Width - 1, 0));
            //Белые точки
            for (int i = 0; i < Width; ++i)
                for (int j = 0; j < Height; ++j)
                    if (rnd.Next() % 20 == 0)
                        result.SetPixel(i, j, System.Drawing.Color.White);

            return result;
        }
        private void GenerateImageControl()
        {
            Image a = CreateImage((int)CaptchaImage.Width - 30, (int)CaptchaImage.Height - 25);
            MemoryStream ms = new MemoryStream();
            a.Save(ms, System.Drawing.Imaging.ImageFormat.Bmp);
            BitmapImage ix = new BitmapImage();
            ix.BeginInit();
            ix.CacheOption = BitmapCacheOption.OnLoad;
            ix.StreamSource = ms;
            ix.EndInit();
            CaptchaImage.Source = ix;
        }
        private void CreateCaptcha_Click(object sender, EventArgs e)
        {
            GenerateImageControl();
        }
        private void CaptchaImage_Loaded(object sender, RoutedEventArgs e)
        {
            GenerateImageControl();
        }

        private void SaveCaptcha_Click(object sender, EventArgs e)
        {
            if (CaptchaWrite.Text == this.text)
            {
                MessageBox.Show("Верно!", "", MessageBoxButton.OK);
                this.Close();
            }
            else
                MessageBox.Show("Ошибка!", "", MessageBoxButton.OK);
        }


    }
}
