using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace Game
{
    class Save
    {
        public static Form f = Form.ActiveForm;
        public static double s_char_dmg, s_enemy_dmg, s_spd, s_exp_n, s_exp, s_character_hp, s_hp_max;
        public static double[] s_e_hp = new double[Engine.number];
        public static int[] s_enemy;
        public static int s_str, s_lvl, s_enemy_lvl, s_e_count, s_e_size, s_hp_amount, r_count = 0;
        public static int s_allow_right, s_allow_left, s_allow_up, s_allow_down, s_character_x, s_character_y, s_fruit_x, s_fruit_y;
        public static bool s_move_left, s_move_right, s_move_up, s_move_down, save;
        public static void _Upload()
        {
            save = true;
            r_count = 0;
            s_e_hp = Engine.e_hp;
            s_e_size = Engine.e_size;
            s_e_count = Engine.e_count;
            s_char_dmg = Engine.char_dmg;
            s_enemy_dmg = Engine.enemy_dmg;
            s_spd = Engine.spd;
            s_exp_n = Engine.exp_n;
            s_exp = Engine.exp;
            s_str = Engine.str;
            s_enemy_lvl = Engine.enemy_lvl;
            s_lvl = Engine.lvl;
            s_character_hp = Engine.character_hp;
            s_hp_max = Engine.hp_max;
            s_allow_right = Locations.allow_right;
            s_allow_left = Locations.allow_left;
            s_allow_up = Locations.allow_up;
            s_allow_down = Locations.allow_down;
            s_move_left = Locations.move_left;
            s_move_right = Locations.move_right;
            s_move_up = Locations.move_up;
            s_move_down = Locations.move_down;
            s_character_x = Engine.character.Location.X;
            s_character_y = Engine.character.Location.Y;
            s_fruit_x = Engine.fruit.Location.X;
            s_fruit_y = Engine.fruit.Location.Y;
            s_hp_amount = Engine.hp_amount;
            s_enemy = new int[Engine.number * 2];
            for(int i = 0; i < Engine.e_count * 2; i = i + 2)
            {
                s_enemy[i] = Engine.enemy[r_count].Location.X;
                s_enemy[i + 1] = Engine.enemy[r_count++].Location.Y;
            }
            
        }
        public static void _Unload()
        {
            Engine.e_hp = s_e_hp;
            Engine.e_size = s_e_size;
            Engine.e_count = s_e_count;
            Engine.char_dmg = s_char_dmg;
            Engine.enemy_dmg = s_enemy_dmg;
            Engine.spd = s_spd;
            Engine.exp_n = s_exp_n;
            Engine.exp = s_exp;
            Engine.str = s_str;
            Engine.enemy_lvl = s_enemy_lvl;
            Engine.lvl = s_lvl;
            Engine.character_hp = s_character_hp;
            Engine.hp_max = s_hp_max;
            Locations.allow_right = s_allow_right;
            Locations.allow_left = s_allow_left;
            Locations.allow_up = s_allow_up;
            Locations.allow_down = s_allow_down;
            Locations.move_left = s_move_left;
            Locations.move_right = s_move_right;
            Locations.move_up = s_move_up;
            Locations.move_down = s_move_down;
            Engine.character = new PictureBox();
            Engine.character.Location = new Point(s_character_x, s_character_y);
            Engine.character.Size = new Size(40, 40);
            Engine.character.Image = Interface.res[0];
            f.Controls.Add(Engine.character);
            Engine.fruit.Location = new Point(s_fruit_x, s_fruit_y);
            Engine.fruit.Size = new Size(s_hp_amount * 10, s_hp_amount * 10);  
        }
    }
}
