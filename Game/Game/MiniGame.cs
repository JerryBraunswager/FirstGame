using System;
using System.Drawing;
using System.Windows.Forms;

namespace Game
{
    class MiniGame
    {
        public static Form f = Form.ActiveForm;
        public static bool minigame = false, key = false, grab = false, e_move = true;
        public static Random r = new Random();
        public static PictureBox mini_char, mini_enemy, cursor;
        public static PictureBox[] tiles = new PictureBox[100];
        public static Label l_char_health, l_enemy_health, l_shield;
        public static double shield, attack, health, combo;
        public static int attack_n, health_n, combo_n, e_count, move = (int) Save.s_spd;
        public static int rnum, icount, jcount, ci, cj, cnum, w_cnum, a_cnum, s_cnum, d_cnum, x, y;
        public static int[,] a_game = new int[10, 10];

        //Shield, Attack, Heart, Combo

        public static void _miniGame()
        {
            if(minigame)
            {
                //Horizontal
                for (int i = 0; i <= 10; i++)
                {
                    PictureBox pic = new PictureBox();
                    pic.BackColor = Color.Black;
                    pic.Location = new Point(200, 40 * i + 160);
                    pic.Size = new Size(400, 1);
                    f.Controls.Add(pic);
                }
                // Vertical
                for (int i = 0; i <= 10; i++)
                {
                    PictureBox pic = new PictureBox();
                    pic.BackColor = Color.Black;
                    pic.Location = new Point(40 * i + 200, Engine._height - 440);
                    pic.Size = new Size(1, 400);
                    f.Controls.Add(pic);
                }
  
                l_char_health = new Label();
                l_char_health.AutoSize = true;
                l_char_health.Text = "Здоровье: " + (int) Save.s_character_hp;
                l_char_health.Font = new Font("Arial", 9f, FontStyle.Bold, GraphicsUnit.Point, ((byte)(204)));
                l_char_health.BackColor = Color.Transparent;
                l_char_health.Location = new Point(0, 0);
                f.Controls.Add(l_char_health);

                l_enemy_health = new Label();
                l_enemy_health.AutoSize = true;
                l_enemy_health.Text = "Здоровье: " + (int) Save.s_e_hp[e_count];
                l_enemy_health.Font = new Font("Arial", 9f, FontStyle.Bold, GraphicsUnit.Point, ((byte)(204)));
                l_enemy_health.Location = new Point(560, 0);
                l_enemy_health.BackColor = Color.Transparent;
                f.Controls.Add(l_enemy_health);

                l_shield = new Label();
                l_shield.AutoSize = true;
                l_shield.Text = "Щит: " + (int) shield;
                l_shield.Font = new Font("Arial", 9f, FontStyle.Bold, GraphicsUnit.Point, ((byte)(204)));
                l_shield.BackColor = Color.Transparent;
                l_shield.Location = new Point(0, 15);
                f.Controls.Add(l_shield);

                mini_char = new PictureBox();
                mini_char.Location = new Point(200, 50);
                mini_char.Size = new Size(60, 60);
                mini_char.BackColor = Color.SandyBrown;
                f.Controls.Add(mini_char);

                mini_enemy = new PictureBox();
                mini_enemy.Location = new Point(540, 50);
                mini_enemy.Size = new Size(60, 60);
                mini_enemy.BackColor = Color.Red;
                f.Controls.Add(mini_enemy);

                for (int j = 0; j < 10; j++)
                {
                    for (int i = 0; i < 10; i++)
                    {
                        rnum = r.Next(1, 5);
                        a_game[i, j] = rnum;
                        if (i > 1)
                        {
                            if (a_game[i, j] == a_game[i - 1, j])
                            {
                                do
                                {
                                    rnum = r.Next(1, 5);
                                    a_game[i, j] = rnum;
                                } while (a_game[i, j] == a_game[i - 2, j]);
                            }
                        }
                        if (j > 1)
                        {
                            if (a_game[i, j] == a_game[i, j - 1])
                            {
                                do
                                {
                                    rnum = r.Next(1, 5);
                                    a_game[i, j] = rnum;
                                } while (a_game[i, j] == a_game[i, j - 2]);
                            }
                        }
                    }
                }
                for (int i = 0; i < 10; i++)
                {
                    for (int j = 0; j < 10; j++)
                    {
                        tiles[i + j * 10] = new PictureBox();
                        tiles[i + j * 10].Location = new Point(40 * i + 202, 40 * j + 162);
                        tiles[i + j * 10].Size = new Size(37, 37);
                        f.Controls.Add(tiles[i + j * 10]);
                        switch (a_game[i, j])
                        {
                            case 1:
                                tiles[i + j * 10].BackColor = Color.Blue;
                                break;
                            case 2:
                                tiles[i + j * 10].BackColor = Color.White;
                                break;
                            case 3:
                                tiles[i + j * 10].BackColor = Color.Red;
                                break;
                            case 4:
                                tiles[i + j * 10].BackColor = Color.Yellow;
                                break;
                        }
                    }
                }
                cursor = new PictureBox();
                cursor.BackColor = Color.Black;
                cursor.Location = new Point(200, 160);
                cursor.Size = new Size(40, 40);
                f.Controls.Add(cursor);
                if (!key)
                {
                    f.KeyDown += new KeyEventHandler(OKP_mini);
                    key = true;
                }
            }
        }

        public static void _destroyTiles()
        {
            for(int j = 0; j < 10; j++)
            {
                for(int i = 0; i < 10; i++)
                {
                    //Horizontal
                    if (i < 8 && a_game[i, j] == a_game[i + 1, j] && a_game[i, j] == a_game[i + 2, j])
                    {
                        icount = 3;
                        if (i < 7 && a_game[i, j] == a_game[i + 3, j])
                        {
                            icount = 4;
                            if (i < 6 && a_game[i, j] == a_game[i + 4, j])
                                icount = 5;
                        }
                    }
                    for (int n = i; n < i + icount; n++)
                        _tileColor(n, j);
                    
                    //Vertical
                    if (j < 8 && a_game[i, j] == a_game[i, j + 1] && a_game[i, j] == a_game[i, j + 2])
                    {
                        jcount = 3;
                        if (j < 7 && a_game[i, j] == a_game[i, j + 3])
                        {
                            jcount = 4;
                            if (j < 6 && a_game[i, j] == a_game[i, j + 4])
                                jcount = 5;
                        }
                    }
                    for (int n1 = j; n1 < j + jcount; n1++)
                        _tileColor(i, n1);
                    
                    for (int n = i; n < i + icount; n++)
                        a_game[n, j] = 0;
                    for (int n1 = j; n1 < j + jcount; n1++)
                        a_game[i, n1] = 0;

                    icount = 0;
                    jcount = 0;
                    _AddArray();
                }
            }
            _fillTile();
            _Move();
        }

        public static void _tileColor(int op1, int op2)
        {
            switch (a_game[op1, op2])
            {
                case 1:                     //Shield
                    shield += 2 / 5;
                    break;
                case 2:                     //Attack
                    attack = 1 * Engine.char_dmg / 5;
                    Save.s_e_hp[e_count] = Save.s_e_hp[e_count] - attack;
                    attack_n = 0;
                    attack = 0;
                    break;
                case 3:                     //Health
                    health = 1 * Engine.hp_max / 25;
                    Save.s_character_hp = Save.s_character_hp + health;
                    double hp_temp = Save.s_character_hp - Save.s_hp_max;
                    if (hp_temp > 0)
                        Save.s_character_hp = Save.s_character_hp - hp_temp;
                    health = 0;
                    health_n = 0;
                    break;
                case 4:                     //Combo
                    combo_n++;
                    if (combo_n == 5)
                    {
                        combo = 4;
                        combo_n = 0;
                        if (combo_n == 4)
                        {
                            combo = 3;
                            combo_n = 0;
                            if (combo_n == 3)
                            {
                                combo = 2;
                                combo_n = 0;
                            }
                        }
                    }
                    break;
            }
        }

        public static void _AddArray()
        {
            for(int j = 9; j > 0; j--)
            {
                for(int i = 9; i >= 0; i--)
                {
                    if(a_game[i, j] == 0)
                    {
                        a_game[i, j] = a_game[i, j - 1];
                        a_game[i, j - 1] = 0;
                    }
                }
            }
            for(int i = 0; i < 10; i++)
            {
                if(a_game[i, 0] == 0)
                {
                    rnum = r.Next(1, 5);
                    a_game[i, 0] = rnum;
                }
            }
        }

        public static void _fillTile()
        {
            for (int j = 0; j < 10; j++)
            {
                for (int i = 0; i < 10; i++)
                {
                    switch (a_game[i, j])
                    {
                        case 1:
                            tiles[i + j * 10].BackColor = Color.Blue;
                            break;
                        case 2:
                            tiles[i + j * 10].BackColor = Color.White;
                            break;
                        case 3:
                            tiles[i + j * 10].BackColor = Color.Red;
                            break;
                        case 4:
                            tiles[i + j * 10].BackColor = Color.Yellow;
                            break;
                    }
                }
            }
        }

        public static void _Move()
        {
            if(move <= 0)
            {
                e_move = false;
                _fight();
            }
        }

        
        public static void _fight()
        {
            if (!Engine.immortal)
            {
                Save.s_character_hp = Save.s_character_hp + shield - Save.s_enemy_dmg;
                if (Save.s_character_hp > Engine.hp_max)
                {
                    shield = Save.s_character_hp - Save.s_hp_max;
                    Save.s_character_hp = Save.s_hp_max;
                }
                else
                    shield = 0;
            }
            move = (int)Save.s_spd;
            e_move = true;
        }
        public static void OKP_mini(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode.ToString())
            {
                case "W":
                    if (minigame)
                    {
                        if (e_move)
                        {
                            if (cursor.Location.Y > 160)
                            {
                                if (grab)
                                {
                                    a_game[ci, cj] = w_cnum;
                                    a_game[ci, cj - 1] = cnum;
                                    _fillTile();
                                    _destroyTiles();
                                    grab = false;
                                    move--;
                                }
                                cursor.Location = new Point(cursor.Location.X, cursor.Location.Y - 40);
                                cj--;
                            }
                        }
                    }
                    break;
                case "A":
                    if (minigame)
                    {
                        if (e_move)
                        {
                            if (cursor.Location.X > 200)
                            {
                                if (grab)
                                {
                                    a_game[ci, cj] = a_cnum;
                                    a_game[ci - 1, cj] = cnum;
                                    _fillTile();
                                    _destroyTiles();
                                    grab = false;
                                    move--;
                                }
                                cursor.Location = new Point(cursor.Location.X - 40, cursor.Location.Y);
                                ci--;
                            }
                        }
                    }
                    break;
                case "S":
                    if (minigame)
                    {
                        if (e_move)
                        {
                            if (cursor.Location.Y < 520)
                            {
                                if (grab)
                                {
                                    a_game[ci, cj] = s_cnum;
                                    a_game[ci, cj + 1] = cnum;
                                    _fillTile();
                                    _destroyTiles();
                                    grab = false;
                                    move--;
                                }
                                cursor.Location = new Point(cursor.Location.X, cursor.Location.Y + 40);
                                cj++;
                            }
                        }
                    }
                    break;
                case "D":
                    if (minigame)
                    {
                        if (e_move)
                        {
                            if (cursor.Location.X < 560)
                            {
                                if (grab)
                                {
                                    a_game[ci, cj] = d_cnum;
                                    a_game[ci + 1, cj] = cnum;
                                    _fillTile();
                                    _destroyTiles();
                                    grab = false;
                                    move--;
                                }
                                cursor.Location = new Point(cursor.Location.X + 40, cursor.Location.Y);
                                ci++;
                            }
                        }
                    }
                    break;
                case "Space":
                    if (minigame)
                    {
                        if (e_move)
                        {
                            if (!grab)
                            {
                                x = cursor.Location.X;
                                y = cursor.Location.Y;
                                ci = (x - 200) / 40;
                                cj = (y - 160) / 40;
                                cnum = a_game[ci, cj];
                                if (cj > 0)
                                    w_cnum = a_game[ci, cj - 1];
                                if (ci > 0)
                                    a_cnum = a_game[ci - 1, cj];
                                if (cj < 9)
                                    s_cnum = a_game[ci, cj + 1];
                                if (ci < 9)
                                    d_cnum = a_game[ci + 1, cj];
                                grab = true;
                            }
                            else
                                grab = false;
                        }
                    }
                    break;
                case "Esc":
                    if (minigame)
                    { }
                    break;
            }
        }
    }
}
