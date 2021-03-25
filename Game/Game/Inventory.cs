using System;
using System.Drawing;
using System.Windows.Forms;

namespace Game
{
    class Inventory
    {
        public static Form f = Form.ActiveForm;
        public static Label l_text;
        public static bool key = false, inv; //Inventory Activation
        public static PictureBox cursor;
        public static PictureBox[,] items = new PictureBox[20, 15];
        public static int i_count = 0, j_count = 0, ci, cj, i_summ, j_summ;
        public static int[,] inv_items = new int[20, 15], inform = new int[20, 15];

        //Fruit, weapons, armors

        public static void _nullPos()
        {
            for (int j = 0; j < 15; j++)
            {
                for (int i = 0; i < 20; i++)
                {
                    if (inv_items[i, j] == 0)
                    {
                        i_count = i;
                        j_count = j;
                        i = 19;
                        j = 14;
                    }
                }
            }
        }
        public static void _getItem(int it, int inf)
        {
            _nullPos();
            if(inv_items[i_count, j_count] == 0)
            {
                switch(it)
                {
                    case 1:
                        inv_items[i_count, j_count] = 1;
                        inform[i_count, j_count] = inf;
                        break;
                    case 2:
                        inv_items[i_count, j_count] = 2;
                        inform[i_count, j_count] = inf;
                        break;
                    case 3:
                        inv_items[i_count, j_count] = 3;
                        inform[i_count, j_count] = inf;
                        break;
                }
                if(i_summ == i_count)
                i_summ++;
                if(i_count >= 20)
                {
                    i_summ = 0;
                    j_summ++;
                }
            }
        }

        public static void _itemText()
        {
            if (ci < i_summ && cj <= j_summ)
            {
                if (cursor.Location.X + 2 == items[ci, cj].Location.X &&
                    cursor.Location.Y + 2 == items[ci, cj].Location.Y)
                {
                    switch (inv_items[ci, cj])
                    {
                        case 1:
                            l_text.Text = "Fruit. Recover " + inform[ci, cj] + " HP";
                            break;
                    }
                }
                else
                    l_text.Text = "";
            }
            else
                l_text.Text = "";
        }
        public static void _Items()
        {
            ci = 0;
            cj = 0;
            for(int j = 0; j < 15; j++)
            {
                for(int i = 0; i < 20; i++)
                {
                    switch(inv_items[i, j])
                    {
                        case 1:
                            items[i, j] = new PictureBox();
                            items[i, j].BackColor = Color.Red;
                            items[i, j].Location = new Point(30 * i + 2, 30 * j + 2);
                            items[i, j].Size = new Size(27, 27);
                            f.Controls.Add(items[i, j]);
                            break;

                    }
                }
            }
        }
        public static void OKP_inv(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode.ToString())
            {
                case "W":
                    if (inv)
                    {
                        if (cursor.Location.Y > 0)
                        {
                            cursor.Location = new Point(cursor.Location.X, cursor.Location.Y - 30);
                            cj--;
                        }
                    }
                    break;
                case "A":
                    if (inv)
                    {
                        if (cursor.Location.X > 0)
                        {
                            cursor.Location = new Point(cursor.Location.X - 30, cursor.Location.Y);
                            ci--;
                        }
                    }
                    break;
                case "S":
                    if (inv)
                    {
                        if (cursor.Location.Y < 420)
                        {
                            cursor.Location = new Point(cursor.Location.X, cursor.Location.Y + 30);
                            cj++;
                        }
                    }
                    break;
                case "D":
                    if (inv)
                    {
                        if (cursor.Location.X < 570)
                        {
                            cursor.Location = new Point(cursor.Location.X + 30, cursor.Location.Y);
                            ci++;
                        }
                    }
                    break;
                case "E":
                    if (inv)
                    {
                        switch (inv_items[ci, cj])
                        {
                            case 1:
                                if(Save.s_character_hp != Save.s_hp_max)
                                {
                                    Save.s_character_hp = Save.s_character_hp + inform[ci, cj] * Save.s_lvl;
                                    double hp_temp = Save.s_character_hp - Save.s_hp_max;
                                    if (hp_temp > 0)
                                        Save.s_character_hp = Save.s_character_hp - hp_temp;
                                    inv_items[ci, cj] = 0;
                                    inform[ci, cj] = 0;
                                    f.Controls.Remove(items[ci, cj]);
                                }
                                break;
                            case 2:
                                break;
                            case 3:
                                break;
                        }
                    }
                    break;
            }
        }
    }
}
