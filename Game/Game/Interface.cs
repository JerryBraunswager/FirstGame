using System;
using System.Drawing;
using System.Media;
using System.Windows.Forms;

namespace Game
{
    public class Interface
    {
        public static Form f = Form.ActiveForm;
        public static SoundPlayer[] sound = new SoundPlayer[1];
        public static Image[] res = new Image[16];
        public static PictureBox character_panel;
        public static Timer timer;
        public static int pc_width = Screen.PrimaryScreen.Bounds.Width;
        public static int pc_height = Screen.PrimaryScreen.Bounds.Height;
        public static bool p_game = false;
        public static void CreateWindow()
        {
            timer = new Timer();
            timer.Interval = 1;
            timer.Tick += new EventHandler(Engine._update);
            timer.Start();
            f.Location = new Point((pc_width - Engine._width)/2, (pc_height - Engine._height)/2);
            f.Size = new Size(Engine._width, Engine._height);
            f.Text = "Game";
            f.FormBorderStyle = FormBorderStyle.FixedSingle;
            f.MaximizeBox = false;
            f.BackColor = Color.LightBlue;
        }
        public static void newButton(int width, int height, string caption, int x, int y, string b_name)
        {
            Button b = new Button();
            b.Name = b_name;
            b.Size = new Size(width, height);
            b.Location = new Point(x, y);
            b.Font = new Font("Arial", 16f, FontStyle.Bold, GraphicsUnit.Point, ((byte)(204)));
            b.Text = caption;
            b.Click += btn_click;
            f.Controls.Add(b);
        }
        public static void newLabel(string caption, int x, int y, Color color, int text_size, string name)
        {
            Label l = new Label();
            l.Name = name;
            l.AutoSize = true;
            l.Location = new Point(x, y);
            l.Font = new Font("Arial", text_size * 1f, FontStyle.Bold, GraphicsUnit.Point, ((byte)(204)));
            l.ForeColor = color;
            l.Text = caption;
            l.BackColor = Color.Transparent;
            f.Controls.Add(l);
        }
        public static void btn_click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            string b_name = btn.Name;
            switch (b_name)
            {
                case "b_play":
                    Scenes("s_play");
                    break;

                case "b_continue":
                    Scenes("s_cplay");
                    break;

                case "b_exit":
                    DialogResult result = MessageBox.Show("Вы точно хотите выйти?", "Сообщение", MessageBoxButtons.OKCancel);
                    if (result == DialogResult.OK)
                        Application.Exit();
                    break;

                case "b_options":
                    Scenes("s_options");
                    break;

                case "b_back":
                    Scenes("s_menu");
                    break;
            }
        }
        public static void Scenes(string s_name)
        {
            switch (s_name)
            {
                case "s_menu":
                    p_game = false;
                    f.Controls.Clear();
                    newLabel("Menu", 370, 20, Color.Black, 14, "l_menu");
                    if (Save.save)
                    {
                        newButton(150, 75, "Continue", 325, 60, "b_continue");
                        newButton(150, 75, "Play", 325, 160, "b_play");
                        newButton(150, 75, "Options", 325, 260, "b_options");
                        newButton(150, 75, "Exit", 325, 360, "b_exit");
                    }
                    else
                    {
                        newButton(150, 75, "Play", 325, 60, "b_play");
                        newButton(150, 75, "Options", 325, 160, "b_options");
                        newButton(150, 75, "Exit", 325, 260, "b_exit");
                    }
                    break;

                case "s_options":
                    f.Controls.Clear();
                    newLabel("Not working", 335, 300, Color.Black, 16, "l_nowork");
                    newButton(100, 50, "Back", 25, 20, "b_back");
                    break;

                case "s_play":
                    p_game = true;
                    Save.save = false;
                    Engine.InGame();
                    break;

                case "s_cplay":
                    p_game = true;
                    Engine.InGame();
                    break;

                case "s_interface":
                    Engine.l_hp = new Label();
                    Engine.l_hp.Text = "Здоровье: " + Engine.character_hp;
                    Engine.l_hp.AutoSize = true;
                    Engine.l_hp.Location = new Point(10, 10);
                    Engine.l_hp.Font = new Font("Arial", 10f, FontStyle.Bold, GraphicsUnit.Point, ((byte)(204)));
                    Engine.l_hp.BackColor = Color.Snow;
                    f.Controls.Add(Engine.l_hp);
                    Engine.l_exp = new Label();
                    Engine.l_exp.Text = "Опыт: " + (int) Engine.exp;
                    Engine.l_exp.AutoSize = true;
                    Engine.l_exp.Location = new Point(130, 10);
                    Engine.l_exp.Font = new Font("Arial", 10f, FontStyle.Bold, GraphicsUnit.Point, ((byte)(204)));
                    Engine.l_exp.BackColor = Color.Snow;
                    f.Controls.Add(Engine.l_exp);
                    Engine.l_exp_n = new Label();
                    Engine.l_exp_n.Text = "Осталось опыта для получения уровня: " + (int) (Engine.exp_n - Engine.exp);
                    Engine.l_exp_n.AutoSize = true;
                    Engine.l_exp_n.Location = new Point(230, 10);
                    Engine.l_exp_n.Font = new Font("Arial", 10f, FontStyle.Bold, GraphicsUnit.Point, ((byte)(204)));
                    Engine.l_exp_n.BackColor = Color.Snow;
                    f.Controls.Add(Engine.l_exp_n);
                    Engine.l_lvl = new Label();
                    Engine.l_lvl.Text = "Уровень: " + Engine.lvl;
                    Engine.l_lvl.AutoSize = true;
                    Engine.l_lvl.Location = new Point(560, 10);
                    Engine.l_lvl.Font = new Font("Arial", 10f, FontStyle.Bold, GraphicsUnit.Point, ((byte)(204)));
                    Engine.l_lvl.BackColor = Color.Snow;
                    f.Controls.Add(Engine.l_lvl);
                    // Char_panel
                    character_panel = new PictureBox();
                    character_panel.Location = new Point(0, 0);
                    character_panel.Size = new Size(Engine._width, 40);
                    character_panel.BackColor = Color.Snow;
                    f.Controls.Add(character_panel);
                    //Entry
                    //Right
                    Engine.entry[0] = new PictureBox();
                    Engine.entry[0].BackColor = Color.Transparent;
                    Engine.entry[0].Location = new Point(0, Engine._height / 2 - 20);
                    Engine.entry[0].Size = new Size(4, 40);
                    f.Controls.Add(Engine.entry[0]);
                    //Left
                    Engine.entry[1] = new PictureBox();
                    Engine.entry[1].BackColor = Color.Transparent;
                    Engine.entry[1].Location = new Point(Engine._width - 18, Engine._height / 2 - 20);
                    Engine.entry[1].Size = new Size(4, 40);
                    f.Controls.Add(Engine.entry[1]);
                    //Up
                    Engine.entry[2] = new PictureBox();
                    Engine.entry[2].BackColor = Color.Transparent;
                    Engine.entry[2].Location = new Point((Engine._width - 18) / 2 - 20, 40);
                    Engine.entry[2].Size = new Size(40, 4);
                    f.Controls.Add(Engine.entry[2]);
                    //Down
                    Engine.entry[3] = new PictureBox();
                    Engine.entry[3].BackColor = Color.Transparent;
                    Engine.entry[3].Location = new Point((Engine._width - 18) / 2 - 20, Engine._height - 41);
                    Engine.entry[3].Size = new Size(40, 4);
                    f.Controls.Add(Engine.entry[3]);
                    break;

                case "s_inventory":
                    f.BackColor = Color.YellowGreen;
                    // Horizontal
                    for (int i = 0; i <= 15; i++)
                    {
                        PictureBox pic = new PictureBox();
                        pic.BackColor = Color.Black;
                        pic.Location = new Point(0, 30 * i);
                        pic.Size = new Size(Engine._width - 200, 1);
                        f.Controls.Add(pic);
                    }
                    // Vertical
                    for (int i = 0; i <= 20; i++)
                    {
                        PictureBox pic = new PictureBox();
                        pic.BackColor = Color.Black;
                        pic.Location = new Point(30 * i, 0);
                        pic.Size = new Size(1, Engine._width - 349);
                        f.Controls.Add(pic);
                    }
                    Inventory._Items();
                    Inventory.cursor = new PictureBox();
                    Inventory.cursor.BackColor = Color.Black;
                    Inventory.cursor.Location = new Point(0, 0);
                    Inventory.cursor.Size = new Size(30, 30);
                    f.Controls.Add(Inventory.cursor);
                    //HP, HP_max, lvl, str, spd, damage
                    Engine.l_hp = new Label();
                    Engine.l_hp.Text = "Здоровье: " + Engine.character_hp;
                    Engine.l_hp.AutoSize = true;
                    Engine.l_hp.Location = new Point(640, 10);
                    Engine.l_hp.Font = new Font("Arial", 9f, FontStyle.Bold, GraphicsUnit.Point, ((byte)(204)));
                    Engine.l_hp.BackColor = Color.Transparent;
                    f.Controls.Add(Engine.l_hp);
                    Engine.l_hp_max = new Label();
                    Engine.l_hp_max.Text = "Максимальное здоровье: " + Engine.hp_max;
                    Engine.l_hp_max.AutoSize = true;
                    Engine.l_hp_max.Location = new Point(610, 30);
                    Engine.l_hp_max.Font = new Font("Arial", 9f, FontStyle.Bold, GraphicsUnit.Point, ((byte)(204)));
                    Engine.l_hp_max.BackColor = Color.Transparent;
                    f.Controls.Add(Engine.l_hp_max);
                    Engine.l_lvl = new Label();
                    Engine.l_lvl.Text = "Уровень: " + Engine.lvl;
                    Engine.l_lvl.AutoSize = true;
                    Engine.l_lvl.Location = new Point(640, 50);
                    Engine.l_lvl.Font = new Font("Arial", 9f, FontStyle.Bold, GraphicsUnit.Point, ((byte)(204)));
                    Engine.l_lvl.BackColor = Color.Transparent;
                    f.Controls.Add(Engine.l_lvl);
                    Engine.l_str = new Label();
                    Engine.l_str.Text = "Сила: " + Engine.str;
                    Engine.l_str.AutoSize = true;
                    Engine.l_str.Location = new Point(640, 70);
                    Engine.l_str.Font = new Font("Arial", 9f, FontStyle.Bold, GraphicsUnit.Point, ((byte)(204)));
                    Engine.l_str.BackColor = Color.Transparent;
                    f.Controls.Add(Engine.l_str);
                    Engine.l_spd = new Label();
                    Engine.l_spd.Text = "Скорость: " + (int) Engine.spd;
                    Engine.l_spd.AutoSize = true;
                    Engine.l_spd.Location = new Point(640, 90);
                    Engine.l_spd.Font = new Font("Arial", 9f, FontStyle.Bold, GraphicsUnit.Point, ((byte)(204)));
                    Engine.l_spd.BackColor = Color.Transparent;
                    f.Controls.Add(Engine.l_spd);
                    Engine.l_dmg = new Label();
                    Engine.l_dmg.Text = "Урон: " + Engine.char_dmg;
                    Engine.l_dmg.AutoSize = true;
                    Engine.l_dmg.Location = new Point(640, 110);
                    Engine.l_dmg.Font = new Font("Arial", 9f, FontStyle.Bold, GraphicsUnit.Point, ((byte)(204)));
                    Engine.l_dmg.BackColor = Color.Transparent;
                    f.Controls.Add(Engine.l_dmg);
                    Inventory.l_text = new Label();
                    Inventory.l_text.AutoSize = true;
                    Inventory.l_text.Location = new Point(0, 450);
                    Inventory.l_text.Font = new Font("Arial", 9f, FontStyle.Bold, GraphicsUnit.Point, ((byte)(204)));
                    Inventory.l_text.BackColor = Color.Transparent;
                    f.Controls.Add(Inventory.l_text);
                    break;

                case "s_minigame":
                    MiniGame.minigame = true;
                    p_game = false;
                    MiniGame._miniGame();
                    break;
            }
        }
        public static void resload()
        {
            sound[0] = new SoundPlayer(@"Materials/fone.wav");
            res[0] = Image.FromFile(@"Materials/Character.png");
            res[1] = Image.FromFile(@"Materials/Fruit10.png");
            res[2] = Image.FromFile(@"Materials/Fruit20.png");
            res[3] = Image.FromFile(@"Materials/Fruit30.png");
            res[4] = Image.FromFile(@"Materials/Enemy30.png");
            res[5] = Image.FromFile(@"Materials/Enemy31.png");
            res[6] = Image.FromFile(@"Materials/Enemy32.png");
            res[7] = Image.FromFile(@"Materials/Enemy33.png");
            res[8] = Image.FromFile(@"Materials/Enemy34.png");
            res[9] = Image.FromFile(@"Materials/Enemy35.png");
            res[10] = Image.FromFile(@"Materials/Enemy36.png");
            res[11] = Image.FromFile(@"Materials/Enemy37.png");
            res[12] = Image.FromFile(@"Materials/Enemy38.png");
            res[13] = Image.FromFile(@"Materials/Enemy39.png");
            res[14] = Image.FromFile(@"Materials/Enemy40.png");
            res[15] = Image.FromFile(@"Materials/background grass.png");
        }
    }
}
