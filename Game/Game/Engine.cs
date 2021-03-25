using System;
using System.Drawing;
using System.Windows.Forms;

namespace Game
{
    public class Engine
    {
        public static Form f = Form.ActiveForm;
        public static int number = 0;
        public static Random r = new Random();
        public static PictureBox character, fruit, character_panel;
        public static PictureBox[] entry = new PictureBox[4];
        public static PictureBox[] enemy = new PictureBox[number];
        public static Label l_hp, l_exp, l_exp_n, l_lvl, l_str, l_hp_max, l_spd, l_dmg;
        public static bool die = false, pause = false, key = false, fight = false, e_dead = false;
        public static double char_dmg, spd, exp_n, exp, character_hp, hp_max, enemy_dmg, enemy_hp;
        public static double[] e_hp = new double[number];
        public static int str, lvl, enemy_lvl, m_x, m_y, hp_count, tempri, ri, temprj, rj, size, e_count, e_size;
        public static int hp_amount, char_move, M_counter = 0, M_m_counter = 4, e_counter = 0, m_e_counter = 1000; 
        public static int neg_move = -8, posi_move = 8, _height = 600, _width = 800; //Y, X
        public static int[] e_move = new int[number];
        //Debug
        public static bool debug = true, immortal = false, e_cmove;

        public static void _start()
        {
            char_move = 1;
            m_x = 0; 
            m_y = 0;
            ri = 0;
            rj = 0;
            e_size = 30;
            e_count = 0;
            char_dmg = 2;
            enemy_dmg = 1;
            spd = 1;
            exp_n = 10;
            exp = 0;
            str = 5;
            enemy_lvl = 1;
            lvl = 1;
            character_hp = 10;
            hp_max = 10;
            enemy_hp = 7;
            Locations.allow_right = 1;
            Locations.allow_left = 1;
            Locations.allow_up = 1;
            Locations.allow_down = 1;
            Locations.move_left = true;
            Locations.move_right = true;
            Locations.move_up = true; 
            Locations.move_down = true;
            die = false;
        }

        public static void InGame()
        {
            f.Controls.Clear();
            f.BackColor = Color.LightGreen;
            //f.BackgroundImage = Interface.res[15];
            if (!Save.save)
                _start();
            Interface.Scenes("s_interface");
            if (!Save.save)
            {
                character = new PictureBox();
                character.Location = new Point(20, _height / 2 - 20);
                character.Size = new Size(40, 40);
                character.Image = Interface.res[0];
                f.Controls.Add(character);
            }
            fruit = new PictureBox();
            fruit.Location = new Point(_width + 50, 10);
            fruit.Size = new Size(10, 10);
            fruit.Image = Interface.res[1];
            f.Controls.Add(fruit);
            if (Save.save)
                Save._Unload();
            if (!Save.save)
            {
                _enemySpawn("spawn");
                _enemySpawn("spawn");
            }
            else
                _enemySpawn("update");
            if (!key)
            {
                Interface.sound[0].PlayLooping();
                f.KeyDown += new KeyEventHandler(OKP);
                key = true;
            }
        }

        public static void _randNumbs(string name)
        {
            switch(name)
            {
                case "enemy":
                    size = e_size;
                    break;
                case "fruit":
                    if (hp_amount == 1)
                        size = 10;
                    if (hp_amount == 2)
                        size = 20;
                    if (hp_amount == 3)
                        size = 30;
                    break;
            }
            ri = r.Next(0, _width - size - 17);
            tempri = ri % size;
            ri -= tempri;
            rj = r.Next(41, _height - size - 40);
            temprj = rj % size;
            rj -= temprj;
        }

        public static void _fruitSpawn()
        {
            Random hp_c = new Random();
            hp_count = hp_c.Next(1, 3);
            if (hp_count == 1)
            {
                hp_amount = 1;
                fruit.Size = new Size(hp_amount * 10, hp_amount * 10);
                fruit.Image = Interface.res[1];
            }
            if (hp_count == 2)
            {
                hp_amount = 2;
                fruit.Size = new Size(hp_amount * 10, hp_amount * 10);
                fruit.Image = Interface.res[2];
            }
            if (hp_count == 3)
            {
                hp_amount = 3;
                fruit.Size = new Size(hp_amount * 10, hp_amount * 10);
                fruit.Image = Interface.res[3];
            }
            _randNumbs("fruit");              
            fruit.Location = new Point(ri, rj);
        }

        public static void _enemyImage(int op)
        {
            switch(e_size)
            {
                case 30:
                    enemy[op].Image = Interface.res[4];
                    break;
                case 31:
                    enemy[op].Image = Interface.res[5];
                    break;
                case 32:
                    enemy[op].Image = Interface.res[6];
                    break;
                case 33:
                    enemy[op].Image = Interface.res[7];
                    break;
                case 34:
                    enemy[op].Image = Interface.res[8];
                    break;
                case 35:
                    enemy[op].Image = Interface.res[9];
                    break;
                case 36:
                    enemy[op].Image = Interface.res[10];
                    break;
                case 37:
                    enemy[op].Image = Interface.res[11];
                    break;
                case 38:
                    enemy[op].Image = Interface.res[12];
                    break;
                case 39:
                    enemy[op].Image = Interface.res[13];
                    break;
                case 40:
                    enemy[op].Image = Interface.res[14];
                    break;
            }
        }

        public static void _enemyArr()
        {
            number++;
            PictureBox[] _enemy = new PictureBox[enemy.Length + 1];
            double[] _e_hp = new double[e_hp.Length + 1];
            int[] _e_move = new int[e_move.Length + 1];
            for (int i = 0; i < enemy.Length; i++)
            {
                _enemy[i] = enemy[i];
                _e_hp[i] = e_hp[i];
                _e_move[i] = e_move[i];
            }
            enemy = _enemy;
            e_hp = _e_hp;
            e_move = _e_move;
        }

        public static void _enemySpawn(string stage, int op = -1)
        {
            if (op == -1)
            {
                switch (stage)
                {
                    case "spawn":
                        {
                            _enemyArr();
                            enemy[e_count] = new PictureBox();
                            e_hp[e_count] = enemy_hp;
                            enemy[e_count].Size = new Size(e_size, e_size);
                            _enemyImage(e_count);
                            _randNumbs("enemy");
                            enemy[e_count].Location = new Point(ri, rj);
                            f.Controls.Add(enemy[e_count]);
                            e_move[e_count] = 1;
                            e_count++;
                            break;
                        }
                    case "update":
                        {
                            int count = 0;
                            for (int i = 0; i < e_count * 2; i = i + 2)
                            {
                                enemy[count] = new PictureBox();
                                enemy[count].Size = new Size(e_size, e_size);
                                _enemyImage(count);
                                enemy[count].Location = new Point(Save.s_enemy[i], Save.s_enemy[i + 1]);
                                f.Controls.Add(enemy[count]);
                                e_move[count] = 1;
                                count++;
                            }
                            break;
                        }
                }
            }
            else
            {
                e_hp[op] = enemy_hp;
                enemy[op].Size = new Size(e_size, e_size);
                _enemyImage(op);
                _randNumbs("enemy");
                enemy[op].Location = new Point(ri, rj);
                f.Controls.Add(enemy[op]);
                e_move[op] = 1;
            }
        }

        public static void _update(object MyObject, EventArgs eventArgs)
        {
            if (!die)
            {
                if(Inventory.inv)
                {
                    l_hp.Text = "Здоровье: " + (int) character_hp;
                    l_hp_max.Text = "Максимальное здоровье: " + hp_max;
                    l_lvl.Text = "Уровень: " + lvl;
                    l_str.Text = "Сила: " + str;
                    l_spd.Text = "Скорость: " + (int) spd;
                    l_dmg.Text = "Урон: " + char_dmg;
                    Inventory._itemText(); 
                }
                if (MiniGame.minigame)
                {
                    MiniGame.l_char_health.Text = "Здоровье: " + (int) Save.s_character_hp;
                    MiniGame.l_enemy_health.Text = "Здоровье: " + (int) Save.s_e_hp[MiniGame.e_count];
                    MiniGame.l_shield.Text = "Щит: " + (int) MiniGame.shield;
                    MiniGame._destroyTiles();
                }
                if (MiniGame.minigame || Interface.p_game)
                {
                    _dead();
                    _eDie();
                    _lvl();
                }
                if (Interface.p_game && !pause)
                {
                    l_hp.Text = "Здоровье: " + (int) character_hp;
                    l_exp.Text = "Опыт: " + (int) exp;
                    l_exp_n.Text = "Осталось опыта для получения уровня: " + (int) (exp_n - exp);
                    _CheckBorders();
                    _nullCollision();
                    for (int i = 0; i < e_count; i++)
                    {
                        _Collision(i);
                        _CheckBordersEnemy(i);
                    }
                    if (M_counter >= M_m_counter)
                    {
                        if (!e_cmove)
                        {
                            //_enemyMove();
                            _enemyMovesm();
                        }
                        M_counter = 0;
                    }
                    else
                        M_counter++;
                    //_lvl();
                    _fruitEat();
                    Locations._notMove();
                    //if (Locations.numhow_right == 3)
                    //    Locations.Castle();
                }
            }
        }

        public static void _eDie()
        {
            if(MiniGame.minigame)
            {
                if (Save.s_e_hp[MiniGame.e_count] <= 0 || (int)Save.s_e_hp[MiniGame.e_count] <= 0)
                {
                    Save.s_exp = Save.s_exp + Save.s_enemy_lvl;
                    MiniGame.attack = 0;
                    MiniGame.health = 0;
                    MiniGame.combo = 0;
                    MiniGame.shield = 0;
                    MiniGame.icount = 0;
                    MiniGame.jcount = 0;
                    pause = false;
                    MiniGame.minigame = false;
                    Interface.p_game = true;
                    InGame();
                }
            }
            if (Interface.p_game)
            {
                for (int i = 0; i < e_count; i++)
                {
                    if ((int)e_hp[i] <= 0)
                    {
                        e_hp[i] = enemy_hp;
                        _fruitSpawn();
                        e_dead = true;
                        _enemySpawn("spawn", i);
                    }
                }
            }
        }

        public static void _fruitEat()
        {
            if (character.Bounds.IntersectsWith(fruit.Bounds))
            {
                if(character_hp == hp_max)
                {
                    Inventory._getItem(1, hp_amount);
                }
                fruit.Location = new Point(_width + 50, 10);
                character_hp = character_hp + hp_amount * lvl;
                double hp_temp = character_hp - hp_max;
                if (hp_temp > 0)
                    character_hp = character_hp - hp_temp; 
            }
        }

        public static void _dead()
        {
            if (character_hp <= 0)
            {
                if (!die)
                {
                    f.Controls.Clear();
                    Save.save = false;
                    die = true;
                    DialogResult result = MessageBox.Show("Вы умерли", "Смерть", MessageBoxButtons.RetryCancel);
                    if (result == DialogResult.Retry)
                        InGame();
                    if (result == DialogResult.Cancel)
                    {
                        f.BackColor = Color.LightBlue;
                        Interface.Scenes("s_menu");
                    }
                }
            }
        }

        public static void _enemyRemove(int x = 1)
        {
            int temp_count = e_count;
            e_count = 0;
            for (int i = 0; i < temp_count; i++)
            {
                f.Controls.Remove(enemy[i]);
                if (x == 1)
                    _enemySpawn("spawn");
            }
        }

        public static void _lvl()
        {
            if (exp >= exp_n)
            {
                lvl++;
                str++;
                char_dmg = str / 2.5;
                character_hp = str * 2;
                hp_max = str * 2;
                spd = spd + 0.2;
                //Enemy
                enemy_lvl++;
                enemy_hp = enemy_hp + 2;
                if(e_size <= 40)
                    e_size++;
                enemy_dmg = enemy_dmg + 0.5;
                exp = 0;
                exp_n = exp_n + exp_n * 1.2;
            }   
        }
        // Moving
        public static void _characterMove()
        {
            character.Location = new Point(character.Location.X + m_x, character.Location.Y + m_y);
            m_x = 0;
            m_y = 0;
        }

        public static void _enemyMove()
        {
            for (int i = 0; i < e_count; i++)
            {
                int dest;
                Random d = new Random();
                dest = d.Next(1, 4);
                if (dest == 1) //Up
                    enemy[i].Location = new Point(enemy[i].Location.X, enemy[i].Location.Y - 10);
                if (dest == 2) //Left
                    enemy[i].Location = new Point(enemy[i].Location.X - 10, enemy[i].Location.Y);
                if (dest == 3) //Down
                    enemy[i].Location = new Point(enemy[i].Location.X, enemy[i].Location.Y + 10);
                if (dest == 4) //Right
                    enemy[i].Location = new Point(enemy[i].Location.X + 10, enemy[i].Location.Y);
            }
        }

        public static void _enemyMovesm()
        {
            
            for (int i = 0; i < e_count; i++)
            {
                int distX = character.Location.X - enemy[i].Location.X;
                int distY = character.Location.Y - enemy[i].Location.Y;
                if (Math.Abs(distX) > Math.Abs(distY))
                {
                    if (distX > 0)
                    {
                        if (e_move[i] == 1)
                            enemy[i].Location = new Point(enemy[i].Location.X + 3, enemy[i].Location.Y);
                    }
                    else
                    {
                        if (e_move[i] == 1)
                            enemy[i].Location = new Point(enemy[i].Location.X - 3, enemy[i].Location.Y);
                    }
                }
                else
                {
                    if (distY > 0)
                    {
                        if (e_move[i] == 1)
                            enemy[i].Location = new Point(enemy[i].Location.X, enemy[i].Location.Y + 3);
                    }
                    else
                    {
                        if (e_move[i] == 1)
                            enemy[i].Location = new Point(enemy[i].Location.X, enemy[i].Location.Y - 3);
                    }
                }
            }
        }

        public static void OKP(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode.ToString())
            {
                case "W":   //Up
                    if (!pause && !die && !MiniGame.minigame)
                    {
                        if (char_move != 0)
                        {
                            m_x = 0;
                            m_y = neg_move * (int) spd;
                            _characterMove();
                        }
                    }
                    break;
                case "A":   //Left
                    if (!pause && !die && !MiniGame.minigame)
                    {
                        if (char_move != 0)
                        {
                            m_x = neg_move * (int) spd;
                            m_y = 0;
                            _characterMove();
                        }
                    }
                    break;
                case "S":   //Down
                    if (!pause && !die && !MiniGame.minigame)
                    {
                        if (char_move != 0)
                        {
                            m_x = 0;
                            m_y = posi_move * (int) spd;
                            _characterMove();
                        }
                    }
                    break;
                case "D":   //Right
                    if (!pause && !die && !MiniGame.minigame)
                    {
                        if (char_move != 0)
                        {
                            m_x = posi_move * (int) spd;
                            m_y = 0;
                            _characterMove();
                        }
                    }
                    break;
                case "Escape":
                    if (Interface.p_game && !MiniGame.minigame)
                    {
                        pause = true;
                        DialogResult result = MessageBox.Show
                            ("Да - выход из игры, Нет - выход в меню", "Выход", MessageBoxButtons.YesNoCancel);
                        if (result == DialogResult.Yes)
                            Application.Exit();
                        if (result == DialogResult.No)
                        {
                            pause = false;
                            Save._Upload();
                            Interface.Scenes("s_menu");
                        }
                        if (result == DialogResult.Cancel)
                            pause = false;
                    }
                    else
                        Application.Exit();
                    break;
                case "I":
                    if (!die && !MiniGame.minigame)
                    {
                        if (!Inventory.inv && !pause)
                        {
                            if (!Inventory.key)
                            {
                                f.KeyDown += new KeyEventHandler(Inventory.OKP_inv);
                                Inventory.key = true;
                            }
                            pause = true;
                            Inventory.inv = true;
                            Save._Upload();
                            _enemyRemove(0);
                            f.Controls.Clear();
                            Interface.Scenes("s_inventory");
                        }
                        else
                        {
                            pause = false;
                            Inventory.inv = false;
                            InGame();
                        }
                    }
                    break;
                //Debug
                case "Z":
                    if(debug)
                        _fruitSpawn();
                    break;
                case "C":
                    if(debug)
                        character_hp = character_hp - 1;
                    break;
                case "X":
                    if (debug)
                    {
                        if (!e_cmove)
                            e_cmove = true;
                        else
                            e_cmove = false;
                    }
                    break;
                case "H":
                    if (debug)
                        immortal = true;
                    break;
                case "O":
                    if (debug && !MiniGame.minigame)
                    {
                        exp = exp_n - 1;
                    }
                    break;
                case "P":
                    if (debug)
                        if (!pause)
                        {
                            pause = true;
                        }
                        else
                        {
                            pause = false;
                        }
                    break;
            }
        }

        private static void _CheckBorders()
        {
            if (Locations.move_right)
            {
                if (character.Location.X < 0) //Right
                {
                    int y = character.Location.Y;
                    f.Controls.Clear();
                    _enemyRemove();
                    Interface.Scenes("s_interface");
                    Locations.numhow_right++;
                    Locations.numwhere_right = 1;
                    Locations.numwhere_left = 0;
                    Locations.numwhere_up = 0;
                    Locations.numwhere_down = 0;
                    character = new PictureBox();
                    character.Location = new Point(_width - 58, y);
                    character.Size = new Size(40, 40);
                    character.Image = Interface.res[0];
                    f.Controls.Add(character);
                    Locations._moveAllow();
                }
            }
            if (Locations.move_left)
            {
                if (character.Location.X > _width - 18) //Left
                {
                    int y = character.Location.Y;
                    f.Controls.Clear();
                    _enemyRemove();
                    Interface.Scenes("s_interface");
                    Locations.numhow_left++;
                    Locations.numwhere_right = 0;
                    Locations.numwhere_left = 1;
                    Locations.numwhere_up = 0;
                    Locations.numwhere_down = 0;
                    character = new PictureBox();
                    character.Location = new Point(0, y);
                    character.Size = new Size(40, 40);
                    character.Image = Interface.res[0];
                    f.Controls.Add(character);
                    Locations._moveAllow();
                }
            }
            if (Locations.move_up)
            {
                if (character.Location.Y < 40) //Up
                {
                    int x = character.Location.X;
                    f.Controls.Clear();
                    _enemyRemove();
                    Interface.Scenes("s_interface");
                    Locations.numhow_up++;
                    Locations.numwhere_right = 0;
                    Locations.numwhere_left = 0;
                    Locations.numwhere_up = 1;
                    Locations.numwhere_down = 0;
                    character = new PictureBox();
                    character.Location = new Point(x, _height - 80);
                    character.Size = new Size(40, 40);
                    character.Image = Interface.res[0];
                    f.Controls.Add(character);
                    Locations._moveAllow();
                }
            }
            if (Locations.move_down)
            {
                if (character.Location.Y > _height - 41) //Down
                {
                    int x = character.Location.X;
                    f.Controls.Clear();
                    _enemyRemove();
                    Interface.Scenes("s_interface");
                    Locations.numhow_down++;
                    Locations.numwhere_right = 0;
                    Locations.numwhere_left = 0;
                    Locations.numwhere_up = 0;
                    Locations.numwhere_down = 1;
                    character = new PictureBox();
                    character.Location = new Point(x, 41);
                    character.Size = new Size(40, 40);
                    character.Image = Interface.res[0];
                    f.Controls.Add(character);
                    Locations._moveAllow();
                }
            }
        }
        private static void _CheckBordersEnemy(int i)
        {
                if (enemy[i].Location.X < 0) //Right
                    enemy[i].Location = new Point(0, enemy[i].Location.Y);
                if (enemy[i].Location.X > _width - 18) //Left
                    enemy[i].Location = new Point(_width - 58, enemy[i].Location.Y);
                if (enemy[i].Location.Y < 40) //Up
                    enemy[i].Location = new Point(enemy[i].Location.X, 41);
                if (enemy[i].Location.Y > _height - 41) //Down
                    enemy[i].Location = new Point(enemy[i].Location.X, _height - 80);
        }
        private static void _Collision(int i)
        {
            //Game
            if (enemy[i].Bounds.IntersectsWith(character.Bounds))
            {
                pause = true;
                fight = true;
                e_move[i] = 0;
                MiniGame.e_count = i;
                char_move = 0;
                Save._Upload();
                _enemyRemove(0);
                f.Controls.Clear();
                Interface.Scenes("s_minigame");
            }
            for (int j = 0; j < e_count; j++)
            {
                if (i != j)
                {
                    if (enemy[i].Bounds.IntersectsWith(enemy[j].Bounds))
                    {
                        e_move[j] = 0;
                        e_move[i] = 1;
                    }
                }
            }
        }
        private static void _nullCollision()
        {
            char_move = 1;
            for (int i = 0; i < e_count; i++)
            {
                e_move[i] = 1;
            }
        }
    }
}
