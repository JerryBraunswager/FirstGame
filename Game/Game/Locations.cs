using System;
using System.Drawing;
using System.Windows.Forms;

namespace Game
{
    class Locations
    {
        public static Form f = Form.ActiveForm;
        public static int numhow_up, numhow_down, numhow_right, numhow_left, allow_right, allow_left, allow_up, allow_down;
        public static int numwhere_up, numwhere_down, numwhere_right, numwhere_left;
        public static int[] n_entry = { 1, 1, 1, 1 };
        public static bool move_left, move_right, move_up, move_down;

        //Castle, forest, lake, river

        public static void _notMove()
        {
            switch(allow_right)
            {
                case 0:
                    Engine.entry[0].BackColor = Color.Black;
                    break;
                case 1:
                    Engine.entry[0].BackColor = Color.Transparent;
                    break;
            }
            switch(allow_left)
            {
                case 0:
                    Engine.entry[1].BackColor = Color.Black;
                    break;
                case 1:
                    Engine.entry[1].BackColor = Color.Transparent;
                    break;
            }
            switch (allow_up)
            {
                case 0:
                    Engine.entry[2].BackColor = Color.Black;
                    break;
                case 1:
                    Engine.entry[2].BackColor = Color.Transparent;
                    break;
            }
            switch (allow_down)
            {
                case 0:
                    Engine.entry[3].BackColor = Color.Black;
                    break;
                case 1:
                    Engine.entry[3].BackColor = Color.Transparent;
                    break;
            }
            if (allow_right == 0)
            {
                move_right = false;
                if (Engine.character.Location.X < 0) //Right
                    Engine.character.Location = new Point(0, Engine.character.Location.Y);
            }
            if(allow_left == 0)
            {
                move_left = false;
                if (Engine.character.Location.X + 40 > Engine._width - 18) //Left
                    Engine.character.Location = new Point(Engine._width - 58, Engine.character.Location.Y);
            }
            if(allow_up == 0)
            {
                move_up = false;
                if (Engine.character.Location.Y < 40) //Up
                    Engine.character.Location = new Point(Engine.character.Location.X, 41);
            }
            if(allow_down == 0)
            {
                move_down = false;
                if (Engine.character.Location.Y + 40 > Engine._height - 41) //Down
                    Engine.character.Location = new Point(Engine.character.Location.X, Engine._height - 80);
            }
        }

        public static void _moveAllow()
        {
            move_left = true;
            move_right = true;
            move_up = true;
            move_down = true;
            Random r = new Random();
            for (int i = 0; i < n_entry.Length; i++)
            {
                n_entry[i] = r.Next(0, 2);
                switch (i)
                {
                    case 0:                                 //Right
                        if (numwhere_left == 1)
                            allow_right = 1;
                        else
                            allow_right = n_entry[i];       
                        break;
                    case 1:                                 //Left
                        if (numwhere_right == 1)
                            allow_left = 1;
                        else
                            allow_left = n_entry[i];        
                        break;
                    case 2:                                 //Up
                        if (numwhere_down == 1)
                            allow_up = 1;
                        else
                            allow_up = n_entry[i];       
                        break;
                    case 3:                                 //Down
                        if (numwhere_up == 1)
                            allow_down = 1;
                        else
                            allow_down = n_entry[i];        
                        break;
                }
            }
        }
        public static void Castle()
        {
            numhow_right = 0;
            Engine._enemyRemove(1);
            f.BackColor = Color.SlateGray;
            _moveAllow();
        }
    }
}
