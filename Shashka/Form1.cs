using System.Runtime.InteropServices;
namespace Shashka
{
    public partial class Form1 : Form
    {
        Button[,] katak = new Button[8, 8];
        int[,] xarita = new int[8, 8];
        int navbat = 1;
        Bitmap oq = new Bitmap("1.png");
        Bitmap qora = new Bitmap("2.png");
        Bitmap oqd = new Bitmap("1d.png");
        Bitmap qorad = new Bitmap("2d.png");
        public Form1()
        {
            InitializeComponent();
        }
        bool play = false;
        private void Form1_Activated(object sender, EventArgs e)
        {
            if (!play)
            {
                Form2 obj = new Form2();
                obj.ShowDialog();
                if (!Class1.esc)
                {
                    play = true;
                    yaratishX();
                }
            }
        }
        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;
        [DllImportAttribute("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [DllImportAttribute("user32.dll")]
        public static extern bool ReleaseCapture();
        private void Form1_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }
        void yaratishX()
        {
            if (Class1.oq)
            {
                xarita = new int[8, 8]
                {
                {2,0,2,0,2,0,2,0 },
                {0,2,0,2,0,2,0,2 },
                {2,0,2,0,2,0,2,0 },
                {0,0,0,0,0,0,0,0 },
                {0,0,0,0,0,0,0,0 },
                {0,1,0,1,0,1,0,1 },
                {1,0,1,0,1,0,1,0 },
                {0,1,0,1,0,1,0,1 }
                };
            }
            else
            {
                xarita = new int[8, 8]
                {
                {1,0,1,0,1,0,1,0 },
                {0,1,0,1,0,1,0,1 },
                {1,0,1,0,1,0,1,0 },
                {0,0,0,0,0,0,0,0 },
                {0,0,0,0,0,0,0,0 },
                {0,2,0,2,0,2,0,2 },
                {2,0,2,0,2,0,2,0 },
                {0,2,0,2,0,2,0,2 }
                };
            }
            yaratishK();
            yurish();
            if(Class1.dastur) boshqaruv();
        }
        void yaratishK()
        {
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    katak[i, j] = new Button();
                    katak[i, j].FlatStyle = FlatStyle.Flat;
                    katak[i, j].Size = new Size(80,80);
                    katak[i, j].Location = new Point(j * 80, i * 80);
                    katak[i, j].FlatAppearance.BorderSize = 0;
                    katak[i, j].Font = new Font("Segue UI", 30);
                    katak[i, j].BackgroundImageLayout = ImageLayout.Center;
                    if (!Class1.dastur) katak[i, j].Click += new EventHandler(insonClick);
                    if (Class1.dastur) katak[i, j].Click += new EventHandler(dasturClick);
                    katak[i, j].Name = i + "" + j;
                    katak[i, j].ForeColor = Color.White;
                    katak[i, j].FlatAppearance.BorderColor = Color.FromArgb(34, 27, 19);
                    if (xarita[i, j] == 2)
                    {
                        katak[i, j].Image = (Image)qora;

                    }
                    if (xarita[i, j] == 1)
                    {
                        katak[i, j].Image = (Image)oq;

                    }
                    panel2.Controls.Add(katak[i, j]);
                }
            }
            fon();
        }
        int x = 0, y = 0, a = 0, b = 0;
        void fon()
        {
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    if (i % 2 == 0 && j % 2 == 0 || i % 2 == 1 && j % 2 == 1)
                    {
                        katak[i, j].BackColor = Color.FromArgb(34, 27, 19);
                        katak[i, j].FlatAppearance.BorderColor = Color.White;
                        katak[i, j].FlatAppearance.MouseDownBackColor = Color.FromArgb(34, 27, 19);
                        katak[i, j].FlatAppearance.BorderSize = 0;
                    }
                    else
                    {
                        katak[i, j].BackColor = Color.FromArgb(126, 103, 80);
                        katak[i, j].FlatAppearance.BorderColor = Color.White;
                        katak[i, j].FlatAppearance.MouseDownBackColor = Color.FromArgb(126, 103, 80);
                        katak[i, j].FlatAppearance.MouseOverBackColor = Color.FromArgb(126, 103, 80);
                    }
                }
            }
        }
        bool inson = false;
        private void dasturClick(object sender, EventArgs e)
        {
            Button bosilgan = (Button)sender;
            border();
            if (bosilgan.ForeColor == Color.White && !tugadi)
            {
                if (bosilgan.Image == (Image)qora || bosilgan.Image == (Image)oq)
                {
                    for (int i = 0; i < 8; i++) for (int j = 0; j < 8; j++) if (bosilgan.Name == katak[i, j].Name) { x = i; y = j; break; }
                    fon();
                    tekshir(x, y);
                    if (!t) yurish();
                    if (bosilgan.FlatAppearance.BorderColor == Color.Orange)
                    {
                        bosilgan.FlatAppearance.BorderColor = Color.Green;
                        bosilgan.FlatAppearance.BorderSize = 3;
                        olish(x, y);
                        if (!t) harakat(x, y);
                        inson = true;
                    }
                }
                if (bosilgan.Image == (Image)qorad || bosilgan.Image == (Image)oqd)
                {
                    for (int i = 0; i < 8; i++)
                    {
                        for (int j = 0; j < 8; j++)
                        {
                            if (katak[i, j].Name == bosilgan.Name)
                            {
                                x = i; y = j;
                            }
                        }
                    }
                    fon();
                    yurishDama();
                    if (bosilgan.FlatAppearance.BorderColor == Color.Orange)
                    {
                        olishDama(x, y);
                        if (!t)
                        {
                            bosilgan.FlatAppearance.BorderColor = Color.Green;
                            bosilgan.FlatAppearance.BorderSize = 3;
                            harakatDama(x, y);
                        }
                        inson = true;
                    }
                }
            }
            if (bosilgan.FlatAppearance.BorderColor == Color.FromArgb(180, 150, 0) && inson)
            {
                bosilgan.Image = katak[x, y].Image;
                bosilgan.ForeColor = katak[x, y].ForeColor;
                katak[x, y].Image = null;
                for (int i = 0; i < 8; i++) for (int j = 0; j < 8; j++) if (bosilgan.Name == katak[i, j].Name) { a = i; b = j; break; }
                int d = xarita[a, b];
                xarita[a, b] = xarita[x, y];
                xarita[x, y] = d;
                if (x > a && y > b)
                {
                    for (int i = x; i > a; i--)
                    {
                        for (int j = y; j > b; j--)
                        {
                            if (katak[i, j].FlatAppearance.BorderColor == Color.Red)
                            {
                                katak[i, j].Image = null;
                                xarita[i, j] = 0;
                            }
                        }
                    }
                }
                else if (x > a && y < b)
                {
                    for (int i = x; i > a; i--)
                    {
                        for (int j = y; j < b; j++)
                        {
                            if (katak[i, j].FlatAppearance.BorderColor == Color.Red)
                            {
                                katak[i, j].Image = null;
                                xarita[i, j] = 0;
                            }
                        }
                    }
                }
                else if (x < a && y < b)
                {
                    for (int i = x; i < a; i++)
                    {
                        for (int j = y; j < b; j++)
                        {
                            if (katak[i, j].FlatAppearance.BorderColor == Color.Red)
                            {
                                katak[i, j].Image = null;
                                xarita[i, j] = 0;
                            }
                        }
                    }
                }
                else if (x < a && y > b)
                {
                    for (int i = x; i < a; i++)
                    {
                        for (int j = y; j > b; j--)
                        {
                            if (katak[i, j].FlatAppearance.BorderColor == Color.Red)
                            {
                                katak[i, j].Image = null;
                                xarita[i, j] = 0;
                            }
                        }
                    }
                }
                fon();
                for (int i = 0; i < 8; i++)
                {
                    if (Class1.oq)
                    {
                        if (xarita[7, i] == 2)
                        {
                            katak[7, i].FlatAppearance.BorderColor = Color.White;
                            katak[7, i].Image = (Image)qorad;

                        }
                        if (xarita[0, i] == 1)
                        {
                            katak[0, i].FlatAppearance.BorderColor = Color.White;
                            katak[0, i].Image = (Image)oqd;
                        }
                    }
                    else
                    {
                        if (xarita[7, i] == 1)
                        {
                            katak[7, i].FlatAppearance.BorderColor = Color.White;
                            katak[7, i].Image = (Image)oqd;
                        }
                        if (xarita[0, i] == 2)
                        {
                            katak[0, i].FlatAppearance.BorderColor = Color.White;
                            katak[0, i].Image = (Image)qorad;
                        }
                    }
                }
                if (t)
                {
                    t = false;
                    olish(a, b);
                    if (katak[a, b].Image == (Image)qorad || katak[a, b].Image == (Image)oqd) olishDama(a, b);
                    x = a; y = b;
                }
                if (!t)
                {
                    almashtirish();
                    inson = false;
                    yurish();
                    yurishDama();
                    for (int i = 0; i < 8; i++)
                    {
                        for (int j = 0; j < 8; j++)
                        {
                            if (katak[i, j].FlatAppearance.BorderColor == Color.Orange) tekshir(i, j);
                            if (katak[i, j].Image == (Image)qorad || katak[i, j].Image == (Image)oqd) tekshirDama(i, j);
                        }
                    }
                }
            }
            boshqaruv();

        }
        void boshqaruv()
        {
            if (Class1.oq)
            {
                if (navbat == 2 && !tugadi)
                {
                    while (navbat == 2)
                    {
                        dastur();
                        for (int i = 0; i < 8; i++)
                        {
                            for (int j = 0; j < 8; j++)
                            {
                                if (katak[i, j].FlatAppearance.BorderColor == Color.Red && xarita[i, j] != 0)
                                {
                                    katak[i, j].FlatAppearance.BorderColor = Color.FromArgb(126, 103, 80);
                                    katak[i, j].FlatAppearance.BorderSize = 3;
                                }
                                if (katak[i, j].FlatAppearance.BorderColor == Color.FromArgb(180, 150, 0) && xarita[i, j] == 0)
                                {
                                    katak[i, j].FlatAppearance.BorderColor = Color.FromArgb(126, 103, 80);
                                    katak[i, j].FlatAppearance.BorderSize = 3;
                                }
                            }
                        }
                    }
                    
                }

            }
            else
            {
                if (navbat == 1 && !tugadi)
                {
                    
                    {
                        dastur();
                        for (int i = 0; i < 8; i++)
                        {
                            for (int j = 0; j < 8; j++)
                            {
                                if (katak[i, j].FlatAppearance.BorderColor == Color.Red && xarita[i, j] != 0)
                                {
                                    katak[i, j].FlatAppearance.BorderColor = Color.FromArgb(126, 103, 80);
                                    katak[i, j].FlatAppearance.BorderSize = 3;
                                }
                                if (katak[i, j].FlatAppearance.BorderColor == Color.FromArgb(180, 150, 0) && xarita[i, j] == 0)
                                {
                                    katak[i, j].FlatAppearance.BorderColor = Color.FromArgb(126, 103, 80);
                                    katak[i, j].FlatAppearance.BorderSize = 3;
                                }
                            }
                        }
                    }
                    
                }
            }
        }
        void dastur()
        {

            Random random = new Random();
            List<string> names = new List<string>();
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    if (katak[i, j].FlatAppearance.BorderColor == Color.Orange || katak[i, j].BackColor == Color.Orange) names.Add(katak[i, j].Name);
                }
            }
            try
            {
                bool p = false;
                foreach (string name in names)
                {
                    if (!p)
                    {
                        t = false;
                        int i = int.Parse(name[0].ToString());
                        int j = int.Parse(name[1].ToString());
                        if (katak[i, j].Image == (Image)oq || katak[i, j].Image == (Image)qora) olish(i, j);
                        else if (katak[i, j].Image == (Image)oqd || katak[i, j].Image == (Image)qorad) olishDama(i, j);
                        if (t)
                        {
                            t = false;
                            p = true;
                            x = i; y = j;
                        }
                    }
                }
                int l = random.Next(0, names.Count);
                if (!p)
                {
                    int i = int.Parse(names[l][0].ToString());
                    int j = int.Parse(names[l][1].ToString());
                    katak[i, j].FlatAppearance.BorderColor = Color.Green;
                    katak[i, j].FlatAppearance.BorderSize = 3;
                    if (katak[i, j].Image == (Image)oq || katak[i, j].Image == (Image)qora) harakat(i, j);
                    else if (katak[i, j].Image == (Image)oqd || katak[i, j].Image == (Image)qorad) harakatDama(i, j);
                    x = i; y = j;
                }
                List<string> yur = new List<string>();
                for (int i = 0; i < 8; i++)
                {
                    for (int j = 0; j < 8; j++)
                    {
                        if (katak[i, j].FlatAppearance.BorderColor == Color.FromArgb(180, 150, 0))
                        {
                            yur.Add(katak[i, j].Name);
                        }
                    }
                }
                try
                {
                    int k = random.Next(0, yur.Count);
                    bool f = false;
                    int a = int.Parse(yur[k][0].ToString());
                    int b = int.Parse(yur[k][1].ToString());
                    katak[a, b].Image = katak[x, y].Image;
                    katak[a, b].Text = katak[x, y].Text;
                    katak[a, b].ForeColor = katak[x, y].ForeColor;
                    katak[x, y].Image = null;
                    katak[x, y].Text = "";
                    int d = xarita[a, b];
                    xarita[a, b] = xarita[x, y];
                    xarita[x, y] = d;
                    if (x > a && y > b)
                    {
                        for (int i = x; i > a; i--)
                        {
                            for (int j = y; j > b; j--)
                            {
                                if (katak[i, j].FlatAppearance.BorderColor == Color.Red)
                                {
                                    f = true;
                                    katak[x, y].FlatAppearance.BorderColor = Color.Green;
                                    katak[x, y].FlatAppearance.BorderSize = 3;
                                    katak[i, j].FlatAppearance.BorderColor = Color.Red;
                                    katak[i, j].FlatAppearance.BorderSize = 3;
                                    katak[a, b].FlatAppearance.BorderColor = Color.FromArgb(180, 150, 0);
                                    katak[a, b].FlatAppearance.BorderSize = 3;
                                    katak[i, j].Image = null;
                                    katak[i, j].Text = "";
                                    xarita[i, j] = 0;
                                }
                            }
                        }
                    }
                    else if (x > a && y < b)
                    {
                        for (int i = x; i > a; i--)
                        {
                            for (int j = y; j < b; j++)
                            {
                                if (katak[i, j].FlatAppearance.BorderColor == Color.Red)
                                {
                                    f = true;
                                    katak[x, y].FlatAppearance.BorderColor = Color.Green;
                                    katak[x, y].FlatAppearance.BorderSize = 3;
                                    katak[i, j].FlatAppearance.BorderColor = Color.Red;
                                    katak[i, j].FlatAppearance.BorderSize = 3;
                                    katak[a, b].FlatAppearance.BorderColor = Color.FromArgb(180, 150, 0);
                                    katak[a, b].FlatAppearance.BorderSize = 3;
                                    katak[i, j].Image = null;
                                    katak[i, j].Text = "";
                                    xarita[i, j] = 0;
                                }
                            }
                        }
                    }
                    else if (x < a && y < b)
                    {
                        for (int i = x; i < a; i++)
                        {
                            for (int j = y; j < b; j++)
                            {
                                if (katak[i, j].FlatAppearance.BorderColor == Color.Red)
                                {
                                    f = true;
                                    katak[x, y].FlatAppearance.BorderColor = Color.Green;
                                    katak[x, y].FlatAppearance.BorderSize = 3;
                                    katak[i, j].FlatAppearance.BorderColor = Color.Red;
                                    katak[i, j].FlatAppearance.BorderSize = 3;
                                    katak[a, b].FlatAppearance.BorderColor = Color.FromArgb(180, 150, 0);
                                    katak[a, b].FlatAppearance.BorderSize = 3;
                                    katak[i, j].Image = null;
                                    katak[i, j].Text = "";
                                    xarita[i, j] = 0;
                                }
                            }
                        }
                    }
                    else if (x < a && y > b)
                    {
                        for (int i = x; i < a; i++)
                        {
                            for (int j = y; j > b; j--)
                            {
                                if (katak[i, j].FlatAppearance.BorderColor == Color.Red)
                                {
                                    f = true;
                                    katak[x, y].FlatAppearance.BorderColor = Color.Green;
                                    katak[x, y].FlatAppearance.BorderSize = 3;
                                    katak[i, j].FlatAppearance.BorderColor = Color.Red;
                                    katak[i, j].FlatAppearance.BorderSize = 3;
                                    katak[a, b].FlatAppearance.BorderColor = Color.FromArgb(180, 150, 0);
                                    katak[a, b].FlatAppearance.BorderSize = 3;
                                    katak[i, j].Image = null;
                                    katak[i, j].Text = "";
                                    xarita[i, j] = 0;
                                }
                            }
                        }
                    }
                    if (!f && !t)
                    {
                        fon();
                        katak[x, y].FlatAppearance.BorderColor = Color.Green;
                        katak[x, y].FlatAppearance.BorderSize = 3;
                        katak[a, b].FlatAppearance.BorderColor = Color.FromArgb(180, 150, 0);
                        katak[a, b].FlatAppearance.BorderSize = 3;
                    }
                    if (f)
                    {
                        t = false;
                        olish(a, b);
                        if (katak[a, b].Image == (Image)qorad || katak[a, b].Image == (Image)oqd) olishDama(a, b);
                        if (t)
                        {
                            katak[a, b].FlatAppearance.BorderColor = Color.FromArgb(180, 150, 0);
                            katak[a, b].FlatAppearance.BorderSize = 3;
                            x = a; y = b;
                            p = false;
                        }
                    }
                    if (!t)
                    {
                        almashtirish();
                        yurish();
                        for (int i = 0; i < 8; i++)
                        {
                            for (int j = 0; j < 8; j++)
                            {
                                if (katak[i, j].FlatAppearance.BorderColor == Color.Orange) tekshir(i, j);
                                if (katak[i, j].Image == (Image)qorad || katak[i, j].Image == (Image)oqd) tekshirDama(i, j);
                            }
                        }
                    }
                }
                catch { }
                for (int i = 0; i < 8; i++)
                {
                    if (Class1.oq)
                    {
                        if (xarita[7, i] == 2)
                        {
                            katak[7, i].FlatAppearance.BorderColor = Color.White;
                            katak[7, i].Image = (Image)qorad;

                        }
                        if (xarita[0, i] == 1)
                        {
                            katak[0, i].FlatAppearance.BorderColor = Color.White;
                            katak[0, i].Image = (Image)oqd;
                        }
                    }
                    else
                    {
                        if (xarita[7, i] == 1)
                        {
                            katak[7, i].FlatAppearance.BorderColor = Color.White;
                            katak[7, i].Image = (Image)oqd;

                        }
                        if (xarita[0, i] == 2)
                        {
                            katak[0, i].FlatAppearance.BorderColor = Color.White;
                            katak[0, i].Image = (Image)qorad;
                        }
                    }

                }
            }
            catch { tugadi = true; almashtirish(); }

        }
        void yurishDama()
        {
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    if (katak[i, j].Image == (Image)qorad || katak[i, j].Image == (Image)oqd)
                    {
                        if (xarita[i, j] == navbat)
                        {
                            try { if (xarita[i - 1, j + 1] == 0) katak[i, j].FlatAppearance.BorderColor = Color.Orange; } catch { }
                            try { if (xarita[i - 1, j - 1] == 0) katak[i, j].FlatAppearance.BorderColor = Color.Orange; } catch { }
                            try { if (xarita[i - 1, j + 1] != navbat && xarita[i - 2, j + 2] == 0) katak[i, j].FlatAppearance.BorderColor = Color.Orange; } catch { }
                            try { if (xarita[i - 1, j - 1] != navbat && xarita[i - 2, j - 2] == 0) katak[i, j].FlatAppearance.BorderColor = Color.Orange; } catch { }
                            try { if (xarita[i + 1, j - 1] == 0) katak[i, j].FlatAppearance.BorderColor = Color.Orange; } catch { }
                            try { if (xarita[i + 1, j + 1] == 0) katak[i, j].FlatAppearance.BorderColor = Color.Orange; } catch { }
                            try { if (xarita[i + 1, j - 1] != navbat && xarita[i + 2, j - 2] == 0) katak[i, j].FlatAppearance.BorderColor = Color.Orange; } catch { }
                            try { if (xarita[i + 1, j + 1] != navbat && xarita[i + 2, j + 2] == 0) katak[i, j].FlatAppearance.BorderColor = Color.Orange; } catch { }
                        }
                    }
                }
            }
        }
        private void insonClick(object sender, EventArgs e)
        {
            Button bosilgan = (Button)sender;
            border();
            if (bosilgan.Image == (Image)qora || bosilgan.Image == (Image)oq)
            {
                for (int i = 0; i < 8; i++) for (int j = 0; j < 8; j++) if (bosilgan.Name == katak[i, j].Name) { x = i; y = j; break; }
                fon();
                tekshir(x, y);
                if (!t) yurish();
                if (bosilgan.FlatAppearance.BorderColor == Color.Orange)
                {
                    bosilgan.FlatAppearance.BorderColor = Color.Green;
                    bosilgan.FlatAppearance.BorderSize = 3;
                    olish(x, y);
                    if (!t) harakat(x, y);
                    inson = true;
                }
            }
            if (bosilgan.Image == (Image)qorad || bosilgan.Image == (Image)oqd)
            {
                for (int i = 0; i < 8; i++)
                {
                    for (int j = 0; j < 8; j++)
                    {
                        if (katak[i, j].Name == bosilgan.Name)
                        {
                            x = i; y = j;
                        }
                    }
                }
                fon();
                yurishDama();
                if (bosilgan.FlatAppearance.BorderColor == Color.Orange)
                {
                    olishDama(x, y);
                    if (!t)
                    {
                        bosilgan.FlatAppearance.BorderColor = Color.Green;
                        bosilgan.FlatAppearance.BorderSize = 3;
                        harakatDama(x, y);
                    }
                    inson = true;
                }
            }
            if (bosilgan.FlatAppearance.BorderColor == Color.FromArgb(180, 150, 0))
            {
                bosilgan.Image = katak[x, y].Image;
                bosilgan.ForeColor = katak[x, y].ForeColor;
                katak[x, y].Image = null;
                for (int i = 0; i < 8; i++) for (int j = 0; j < 8; j++) if (bosilgan.Name == katak[i, j].Name) { a = i; b = j; break; }
                int d = xarita[a, b];
                xarita[a, b] = xarita[x, y];
                xarita[x, y] = d;
                if (x > a && y > b)
                {
                    for (int i = x; i > a; i--)
                    {
                        for (int j = y; j > b; j--)
                        {
                            if (katak[i, j].FlatAppearance.BorderColor == Color.Red)
                            {
                                katak[i, j].Image = null;
                                xarita[i, j] = 0;
                            }
                        }
                    }
                }
                else if (x > a && y < b)
                {
                    for (int i = x; i > a; i--)
                    {
                        for (int j = y; j < b; j++)
                        {
                            if (katak[i, j].FlatAppearance.BorderColor == Color.Red)
                            {
                                katak[i, j].Image = null;
                                xarita[i, j] = 0;
                            }
                        }
                    }
                }
                else if (x < a && y < b)
                {
                    for (int i = x; i < a; i++)
                    {
                        for (int j = y; j < b; j++)
                        {
                            if (katak[i, j].FlatAppearance.BorderColor == Color.Red)
                            {
                                katak[i, j].Image = null;
                                xarita[i, j] = 0;
                            }
                        }
                    }
                }
                else if (x < a && y > b)
                {
                    for (int i = x; i < a; i++)
                    {
                        for (int j = y; j > b; j--)
                        {
                            if (katak[i, j].FlatAppearance.BorderColor == Color.Red)
                            {
                                katak[i, j].Image = null;
                                xarita[i, j] = 0;
                            }
                        }
                    }
                }
                fon();
                for (int i = 0; i < 8; i++)
                {
                    if (Class1.oq)
                    {
                        if (xarita[7, i] == 2)
                        {
                            katak[7, i].FlatAppearance.BorderColor = Color.White;
                            katak[7, i].Image = (Image)qorad;

                        }
                        if (xarita[0, i] == 1)
                        {
                            katak[0, i].FlatAppearance.BorderColor = Color.White;
                            katak[0, i].Image = (Image)oqd;
                        }
                    }
                    else
                    {
                        if (xarita[7, i] == 1)
                        {
                            katak[7, i].FlatAppearance.BorderColor = Color.White;
                            katak[7, i].Image = (Image)oqd;

                        }
                        if (xarita[0, i] == 2)
                        {
                            katak[0, i].FlatAppearance.BorderColor = Color.White;
                            katak[0, i].Image = (Image)qorad;
                        }
                    }

                }
                if (t)
                {
                    t = false;
                    olish(a, b);
                    if (katak[a, b].Image == (Image)oqd || katak[a, b].Image == (Image)qorad) olishDama(a, b);
                    x = a; y = b;
                }
                if (!t)
                {
                    almashtirish();
                    yurish();
                    for (int i = 0; i < 8; i++)
                    {
                        for (int j = 0; j < 8; j++)
                        {
                            if (katak[i, j].FlatAppearance.BorderColor == Color.Orange) tekshir(i, j);
                            if (katak[i, j].Image == (Image)oqd || katak[i, j].Image == (Image)qorad) tekshirDama(i, j);
                        }
                    }
                }
            }
        }
        bool tugadi = false;
        void harakatDama(int i, int j)
        {
            if (navbat == xarita[i, j])
            {
                try
                {
                    bool p = false;
                    for (int z = 1; z < 8; z++)
                    {
                        if (xarita[i - z, j + z] == 0 && !p) 
                        {
                            katak[i - z, j + z].FlatAppearance.BorderColor = Color.FromArgb(180, 150, 0); 
                            katak[i - z, j + z].FlatAppearance.BorderSize = 3; 
                        }
                        try { if (xarita[i - z, j + z] != 0 && xarita[i - z - 1, j + z + 1] != 0 || xarita[i - z, j + z] == xarita[i, j]) p = true; } catch { }
                    }
                }
                catch { }
                try
                {
                    bool p = false;
                    for (int z = 1; z < 8; z++)
                    {
                        if (xarita[i - z, j - z] == 0 && !p)
                        {
                            katak[i - z, j - z].FlatAppearance.BorderColor = Color.FromArgb(180, 150, 0);
                            katak[i - z, j - z].FlatAppearance.BorderSize = 3;
                        }
                        try { if (xarita[i - z, j - z] != 0 && xarita[i - z - 1, j - z - 1] != 0 || xarita[i - z, j - z] == xarita[i, j]) p = true; } catch { }
                    }
                }
                catch { }
                try
                {
                    bool p = false;
                    for (int z = 1; z < 8; z++)
                    {
                        if (xarita[i + z, j - z] == 0 && !p)
                        {
                            katak[i + z, j - z].FlatAppearance.BorderColor = Color.FromArgb(180, 150, 0);
                            katak[i + z, j - z].FlatAppearance.BorderSize = 3;
                        }
                        try { if (xarita[i + z, j - z] != 0 && xarita[i + z + 1, j - z - 1] != 0 || xarita[i + z, j - z] == xarita[i, j]) p = true; } catch { }
                    }
                }
                catch { }
                try
                {
                    bool p = false;
                    for (int z = 1; z < 8; z++)
                    {
                        if (xarita[i + z, j + z] == 0 && !p)
                        {
                            katak[i + z, j + z].FlatAppearance.BorderColor = Color.FromArgb(180, 150, 0);
                            katak[i + z, j + z].FlatAppearance.BorderSize = 3;
                        }
                        try { if (xarita[i + z, j + z] != 0 && xarita[i + z + 1, j + z + 1] != 0 || xarita[i + z, j + z] == xarita[i, j]) p = true; } catch { }
                    }
                }
                catch { }
            }
        }
        bool t = false;
        void olishDama(int i, int j)
        {
            if (navbat == xarita[i, j])
            {
                try
                {
                    bool p = false;
                    for (int z = 1; z < 8; z++)
                    {
                        try { if (xarita[i - z, j + z] != 0 && xarita[i - z - 1, j + z + 1] != 0 || xarita[i - z, j + z] == xarita[i, j]) p = true; } catch { }
                        try
                        {
                            if (xarita[i - z, j + z] != 0 && xarita[i - z, j + z] != xarita[i, j] && !p && xarita[i - z - 1, j + z + 1] == 0)
                            {

                                katak[i, j].FlatAppearance.BorderColor = Color.Green;
                                katak[i, j].FlatAppearance.BorderSize = 3;
                                katak[i - z, j + z].FlatAppearance.BorderColor = Color.Red;
                                katak[i - z, j + z].FlatAppearance.BorderSize = 3;
                                for (int k = 1; k < 8; k++)
                                {
                                    if (xarita[i - z - k, j + z + k] == 0 && !p)
                                    {
                                        katak[i - z - k, j + z + k].FlatAppearance.BorderColor = Color.FromArgb(180, 150, 0);
                                        katak[i - z - k, j + z + k].FlatAppearance.BorderSize = 3;
                                        t = true;
                                    }
                                    else if (xarita[i - z - k, j + z + k] != 0) p = true;
                                }
                            }
                        }
                        catch { }
                    }
                }
                catch { }
                try
                {
                    bool p = false;
                    for (int z = 1; z < 8; z++)
                    {
                        try { if (xarita[i - z, j - z] != 0 && xarita[i - z - 1, j - z - 1] != 0 || xarita[i - z, j - z] == xarita[i, j]) p = true; } catch { }
                        try
                        {
                            if (xarita[i - z, j - z] != 0 && xarita[i - z, j - z] != xarita[i, j] && !p && xarita[i - z - 1, j - z - 1] == 0)
                            {

                                katak[i, j].FlatAppearance.BorderColor = Color.Green;
                                katak[i, j].FlatAppearance.BorderSize = 3;
                                katak[i - z, j - z].FlatAppearance.BorderColor = Color.Red;
                                katak[i - z, j - z].FlatAppearance.BorderSize = 3;
                                for (int k = 1; k < 8; k++)
                                {
                                    if (xarita[i - z - k, j - z - k] == 0 && !p)
                                    {
                                        katak[i - z - k, j - z - k].FlatAppearance.BorderColor = Color.FromArgb(180, 150, 0);
                                        katak[i - z - k, j - z - k].FlatAppearance.BorderSize = 3;
                                        t = true;
                                    }
                                    else if (xarita[i - z - k, j - z - k] != 0) p = true;
                                }
                            }
                        }
                        catch { }
                    }
                }
                catch { }
                try
                {
                    bool p = false;
                    for (int z = 1; z < 8; z++)
                    {
                        try { if (xarita[i + z, j - z] != 0 && xarita[i + z + 1, j - z - 1] != 0 || xarita[i + z, j - z] == xarita[i, j]) p = true; } catch { }
                        try
                        {
                            if (xarita[i + z, j - z] != 0 && xarita[i + z, j - z] != xarita[i, j] && !p && xarita[i + z + 1, j - z - 1] == 0)
                            {

                                katak[i, j].FlatAppearance.BorderColor = Color.Green;
                                katak[i, j].FlatAppearance.BorderSize = 3;
                                katak[i + z, j - z].FlatAppearance.BorderColor = Color.Red;
                                katak[i + z, j - z].FlatAppearance.BorderSize = 3;
                                for (int k = 1; k < 8; k++)
                                {
                                    if (xarita[i + z + k, j - z - k] == 0 && !p)
                                    {
                                        katak[i + z + k, j - z - k].FlatAppearance.BorderColor = Color.FromArgb(180, 150, 0);
                                        katak[i + z + k, j - z - k].FlatAppearance.BorderSize = 3;
                                        t = true;
                                    }
                                    else if (xarita[i + z + k, j - z - k] != 0) p = true;
                                }
                            }
                        }
                        catch { }
                    }
                }
                catch { }
                try
                {
                    bool p = false;
                    for (int z = 1; z < 8; z++)
                    {
                        try { if (xarita[i + z, j + z] != 0 && xarita[i + z + 1, j + z + 1] != 0 || xarita[i + z, j + z] == xarita[i, j]) p = true; } catch { }
                        try
                        {
                            if (xarita[i + z, j + z] != 0 && xarita[i + z, j + z] != xarita[i, j] && !p && xarita[i + z + 1, j + z + 1] == 0)
                            {


                                katak[i, j].FlatAppearance.BorderColor = Color.Green;
                                katak[i, j].FlatAppearance.BorderSize = 3;
                                katak[i + z, j + z].FlatAppearance.BorderColor = Color.Red;
                                katak[i + z, j + z].FlatAppearance.BorderSize = 3;
                                for (int k = 1; k < 8; k++)
                                {
                                    if (xarita[i + z + k, j + z + k] == 0 && !p)
                                    {
                                        katak[i + z + k, j + z + k].FlatAppearance.BorderColor = Color.FromArgb(180, 150, 0);
                                        katak[i + z + k, j + z + k].FlatAppearance.BorderSize = 3;
                                        t = true;
                                    }
                                    else if (xarita[i + z + k, j + z + k] != 0) p = true;
                                }
                            }
                        }
                        catch { }
                    }
                }
                catch { }
            }
        }
        void yurish()
        {
            if (navbat == 1)
            {
                for (int i = 0; i < 8; i++)
                {
                    for (int j = 0; j < 8; j++)
                    {
                        if (xarita[i, j] == 1)
                        {
                            if (Class1.oq)
                            {
                                try { if (xarita[i - 1, j + 1] == 0 && !t) katak[i, j].FlatAppearance.BorderColor = Color.Orange; } catch { }
                                try { if (xarita[i - 1, j - 1] == 0 && !t) katak[i, j].FlatAppearance.BorderColor = Color.Orange; } catch { }
                                try { if (xarita[i - 1, j + 1] == 2 && xarita[i - 2, j + 2] == 0 && !t) katak[i, j].FlatAppearance.BorderColor = Color.Orange; } catch { }
                                try { if (xarita[i - 1, j - 1] == 2 && xarita[i - 2, j - 2] == 0 && !t) katak[i, j].FlatAppearance.BorderColor = Color.Orange; } catch { }
                            }
                            else
                            {
                                try { if (xarita[i + 1, j - 1] == 0 && !t) katak[i, j].FlatAppearance.BorderColor = Color.Orange; } catch { }
                                try { if (xarita[i + 1, j + 1] == 0 && !t) katak[i, j].FlatAppearance.BorderColor = Color.Orange; } catch { }
                                try { if (xarita[i + 1, j - 1] == 2 && xarita[i + 2, j - 2] == 0 && !t) katak[i, j].FlatAppearance.BorderColor = Color.Orange; } catch { }
                                try { if (xarita[i + 1, j + 1] == 2 && xarita[i + 2, j + 2] == 0 && !t) katak[i, j].FlatAppearance.BorderColor = Color.Orange; } catch { }
                            }
                        }
                    }
                }
            }
            if (navbat == 2)
            {
                for (int i = 0; i < 8; i++)
                {
                    for (int j = 0; j < 8; j++)
                    {
                        if (xarita[i, j] == 2)
                        {
                            if (Class1.oq)
                            {
                                try { if (xarita[i + 1, j - 1] == 0 && !t) katak[i, j].FlatAppearance.BorderColor = Color.Orange; } catch { }
                                try { if (xarita[i + 1, j + 1] == 0 && !t) katak[i, j].FlatAppearance.BorderColor = Color.Orange; } catch { }
                                try { if (xarita[i + 1, j - 1] == 1 && xarita[i + 2, j - 2] == 0 && !t) katak[i, j].FlatAppearance.BorderColor = Color.Orange; } catch { }
                                try { if (xarita[i + 1, j + 1] == 1 && xarita[i + 2, j + 2] == 0 && !t) katak[i, j].FlatAppearance.BorderColor = Color.Orange; } catch { }
                            }
                            else
                            {
                                try { if (xarita[i - 1, j + 1] == 0 && !t) katak[i, j].FlatAppearance.BorderColor = Color.Orange; } catch { }
                                try { if (xarita[i - 1, j - 1] == 0 && !t) katak[i, j].FlatAppearance.BorderColor = Color.Orange; } catch { }
                                try { if (xarita[i - 1, j + 1] == 1 && xarita[i - 2, j + 2] == 0 && !t) katak[i, j].FlatAppearance.BorderColor = Color.Orange; } catch { }
                                try { if (xarita[i - 1, j - 1] == 1 && xarita[i - 2, j - 2] == 0 && !t) katak[i, j].FlatAppearance.BorderColor = Color.Orange; } catch { }
                            }
                        }
                    }
                }
            }
        }
        void almashtirish()
        {
            navbat = (navbat == 1) ? 2 : 1;
        }
        void olish(int i, int j)
        {
            if (navbat == xarita[i, j])
            {
                try
                {
                    if (xarita[i - 1, j + 1] != navbat && xarita[i - 1, j + 1] != 0 && xarita[i - 2, j + 2] == 0)
                    {
                        katak[i, j].FlatAppearance.BorderColor = Color.Green;
                        katak[i, j].FlatAppearance.BorderSize = 3;
                        katak[i - 1, j + 1].FlatAppearance.BorderColor = Color.Red;
                        katak[i - 1, j + 1].FlatAppearance.BorderSize = 3;
                        katak[i - 2, j + 2].FlatAppearance.BorderColor = Color.FromArgb(180, 150, 0);
                        katak[i - 2, j + 2].FlatAppearance.BorderSize = 3;
                        t = true;
                    }
                }
                catch { }
                try
                {
                    if (xarita[i - 1, j - 1] != navbat && xarita[i - 1, j - 1] != 0 && xarita[i - 2, j - 2] == 0)
                    {
                        katak[i, j].FlatAppearance.BorderColor = Color.Green;
                        katak[i, j].FlatAppearance.BorderSize = 3; 
                        katak[i - 1, j - 1].FlatAppearance.BorderColor = Color.Red;
                        katak[i - 1, j - 1].FlatAppearance.BorderSize = 3;
                        katak[i - 2, j - 2].FlatAppearance.BorderColor = Color.FromArgb(180, 150, 0);
                        katak[i - 2, j - 2].FlatAppearance.BorderSize = 3;
                        t = true;
                    }
                }
                catch { }
                try
                {
                    if (xarita[i + 1, j - 1] != navbat && xarita[i + 1, j - 1] != 0 && xarita[i + 2, j - 2] == 0)
                    {
                        katak[i, j].FlatAppearance.BorderColor = Color.Green;
                        katak[i, j].FlatAppearance.BorderSize = 3;
                        katak[i + 1, j - 1].FlatAppearance.BorderColor = Color.Red;
                        katak[i + 1, j - 1].FlatAppearance.BorderSize = 3;
                        katak[i + 2, j - 2].FlatAppearance.BorderColor = Color.FromArgb(180, 150, 0);
                        katak[i + 2, j - 2].FlatAppearance.BorderSize = 3;
                        t = true;
                    }
                }
                catch { }
                try
                {
                    if (xarita[i + 1, j + 1] != navbat && xarita[i + 1, j + 1] != 0 && xarita[i + 2, j + 2] == 0)
                    {
                        katak[i, j].FlatAppearance.BorderColor = Color.Green;
                        katak[i, j].FlatAppearance.BorderSize = 3;
                        katak[i + 1, j + 1].FlatAppearance.BorderColor = Color.Red;
                        katak[i + 1, j + 1].FlatAppearance.BorderSize = 3;
                        katak[i + 2, j + 2].FlatAppearance.BorderColor = Color.FromArgb(180, 150, 0);
                        katak[i + 2, j + 2].FlatAppearance.BorderSize = 3;
                        t = true;
                    }
                }
                catch { }
            }
        }
        private void button3_Click(object sender, EventArgs e)
        {
            panel2.Controls.Clear();
            t = false;
            navbat = 1;
            tugadi = false;
            yaratishX();
        }
        private void button4_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }
        private void btnPlay_Click(object sender, EventArgs e)
        {
            Form2 obj = new Form2();
            obj.ShowDialog();
            if (!Class1.esc)
            {
                play = true;
                panel2.Controls.Clear();
                t = false;
                navbat = 1;
                tugadi = false;
                yaratishX();
            }
        }
        void tekshirDama(int i, int j)
        {
            if (navbat == xarita[i, j])
            {
                for (int z = 1; z < 8; z++)
                {
                    try
                    {
                        if (xarita[i - z + 1, j + z - 1] != xarita[i - z, j + z] && xarita[i - z, j + z] != navbat && xarita[i - z, j + z] != 0 && xarita[i - z - 1, j + z + 1] == 0)
                        {
                            katak[i, j].FlatAppearance.BorderColor = Color.Orange; t = true;
                        }
                    }
                    catch { }
                    try
                    {
                        if (xarita[i - z + 1, j - z + 1] != xarita[i - z, j - z] && xarita[i - z, j - z] != navbat && xarita[i - z, j - z] != 0 && xarita[i - z - 1, j - z - 1] == 0)
                        {
                            katak[i, j].FlatAppearance.BorderColor = Color.Orange; t = true;
                        }
                    }
                    catch { }
                    try
                    {
                        if (xarita[i + z - 1, j - z + 1] != xarita[i + z, j - z] && xarita[i + z, j - z] != navbat && xarita[i + z, j - z] != 0 && xarita[i + z + 1, j - z - 1] == 0)
                        {
                            katak[i, j].FlatAppearance.BorderColor = Color.Orange; t = true;
                        }
                    }
                    catch { }
                    try
                    {
                        if (xarita[i + z - 1, j + z - 1] != xarita[i + z, j + z] && xarita[i + z, j + z] != navbat && xarita[i + z, j + z] != 0 && xarita[i + z + 1, j + z + 1] == 0)
                        {
                            katak[i, j].FlatAppearance.BorderColor = Color.Orange; t = true;
                        }
                    }
                    catch { }
                }
            }
        }
        void border()
        {
            
        }
        private void lu_Click(object sender, EventArgs e)
        {
        }

        void tekshir(int i, int j)
        {
            if (navbat == xarita[i, j])
            {
                try
                {
                    if (xarita[i - 1, j + 1] != navbat && xarita[i - 1, j + 1] != 0 && xarita[i - 2, j + 2] == 0)
                    {
                        katak[i, j].FlatAppearance.BorderColor = Color.Orange;
                        t = true;
                    }
                }
                catch { }
                try
                {
                    if (xarita[i - 1, j - 1] != navbat && xarita[i - 1, j - 1] != 0 && xarita[i - 2, j - 2] == 0)
                    {
                        katak[i, j].FlatAppearance.BorderColor = Color.Orange;
                        t = true;
                    }
                }
                catch { }
                try
                {
                    if (xarita[i + 1, j - 1] != navbat && xarita[i + 1, j - 1] != 0 && xarita[i + 2, j - 2] == 0)
                    {
                        katak[i, j].FlatAppearance.BorderColor = Color.Orange;
                        t = true;
                    }
                }
                catch { }
                try
                {
                    if (xarita[i + 1, j + 1] != navbat && xarita[i + 1, j + 1] != 0 && xarita[i + 2, j + 2] == 0)
                    {
                        katak[i, j].FlatAppearance.BorderColor = Color.Orange;
                        t = true;
                    }
                }
                catch { }
            }
        }
        void harakat(int i, int j)
        {
            if (navbat == 1 && xarita[i, j] == 1)
            {
                if (Class1.oq)
                {
                    try { if (xarita[i - 1, j + 1] == 0)
                        {
                            katak[i - 1, j + 1].FlatAppearance.BorderColor = Color.FromArgb(180, 150, 0);
                            katak[i - 1, j + 1].FlatAppearance.BorderSize = 3;
                        }
                    } catch { }
                    try { if (xarita[i - 1, j - 1] == 0)
                        {
                            katak[i - 1, j - 1].FlatAppearance.BorderColor = Color.FromArgb(180, 150, 0);
                            katak[i - 1, j - 1].FlatAppearance.BorderSize = 3;
                        }
                    } catch { }
                }
                else
                {
                    try { if (xarita[i + 1, j - 1] == 0) 
                        {
                            katak[i + 1, j - 1].FlatAppearance.BorderColor = Color.FromArgb(180, 150, 0); 
                            katak[i + 1, j - 1].FlatAppearance.BorderSize = 3; 
                        }
                    } catch { }
                    try { if (xarita[i + 1, j + 1] == 0)
                        {
                            katak[i + 1, j + 1].FlatAppearance.BorderColor = Color.FromArgb(180, 150, 0);
                            katak[i + 1, j + 1].FlatAppearance.BorderSize = 3;
                        }
                    } catch { }
                }
            }
            if (navbat == 2 && xarita[i, j] == 2)
            {
                if (Class1.oq)
                {
                    try
                    {
                        if (xarita[i + 1, j - 1] == 0)
                        {
                            katak[i + 1, j - 1].FlatAppearance.BorderColor = Color.FromArgb(180, 150, 0);
                            katak[i + 1, j - 1].FlatAppearance.BorderSize = 3;
                        }
                    }
                    catch { }
                    try
                    {
                        if (xarita[i + 1, j + 1] == 0)
                        {
                            katak[i + 1, j + 1].FlatAppearance.BorderColor = Color.FromArgb(180, 150, 0);
                            katak[i + 1, j + 1].FlatAppearance.BorderSize = 3;
                        }
                    }
                    catch { }
                }
                else
                {
                    try
                    {
                        if (xarita[i - 1, j + 1] == 0)
                        {
                            katak[i - 1, j + 1].FlatAppearance.BorderColor = Color.FromArgb(180, 150, 0);
                            katak[i - 1, j + 1].FlatAppearance.BorderSize = 3;
                        }
                    }
                    catch { }
                    try
                    {
                        if (xarita[i - 1, j - 1] == 0)
                        {
                            katak[i - 1, j - 1].FlatAppearance.BorderColor = Color.FromArgb(180, 150, 0);
                            katak[i - 1, j - 1].FlatAppearance.BorderSize = 3;
                        }
                    }
                    catch { }
                }
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}