using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Drawing;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Kursach
{
    public partial class Form1 : Form
    {
        Button ceiling_material, wall_material, floor_material;
        int count_materials=0;
        Color ceiling, wall, floor;
        string ceiling_color, wall_color, floor_color;
        string ceiling_mat, wall_mat, floor_mat;
        string poverhnost;
        string mode;
        string open_color;
        Color choosen_color;
        string cs = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source= Kursach.mdb;";
        public OleDbConnection conn;
        Bitmap bmp = null;
        List<Int32> ID_sotr = new List<int>();
        public Form1()
        {
            InitializeComponent();
            conn = new OleDbConnection(cs);
            conn.Open();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Close();
        }
        //Метод плавной смены цвета
        async public void ColorSW(int r0, int g0, int b0, int r1, int g1, int b1, int delay, Control ob,string color_type)
        {
            if (color_type == "Forecolor")
            {
                int stepR, stepG, stepB;
                if (r0 > r1)
                    stepR = (r0 - r1) / -25;
                else
                    stepR = (r1 - r0) / 25;
                if (g0 > g1)
                    stepG = (g0 - g1) / -25;
                else
                    stepG = (g1 - g0) / 25;
                if (b0 > b1)
                    stepB = (b0 - b1) / -25;
                else
                    stepB = (b1 - b0) / 25;
                if (r0 > r1)
                {
                    for (int r = r0, g = g0, b = b0; r >= r1 & g >= g1 & b >= b1; r += stepR, g += stepG, b += stepB, await Task.Delay(delay))
                    {
                        ob.ForeColor = Color.FromArgb(r, g, b);
                    }
                }
                else
                {
                    for (int r = r0, g = g0, b = b0; r <= r1 & g <= g1 & b <= b1; r += stepR, g += stepG, b += stepB, await Task.Delay(delay))
                    {
                        ob.ForeColor = Color.FromArgb(r, g, b);
                    }
                }
                ob.ForeColor = Color.FromArgb(r1, g1, b1);
            }
            if (color_type == "Backcolor")
            {
                int stepR, stepG, stepB;
                if (r0 > r1)
                    stepR = (r0 - r1) / -25;
                else
                    stepR = (r1 - r0) / 25;
                if (g0 > g1)
                    stepG = (g0 - g1) / -25;
                else
                    stepG = (g1 - g0) / 25;
                if (b0 > b1)
                    stepB = (b0 - b1) / -25;
                else
                    stepB = (b1 - b0) / 25;
                if (r0 > r1)
                {
                    for (int r = r0, g = g0, b = b0; r >= r1 & g >= g1 & b >= b1; r += stepR, g += stepG, b += stepB, await Task.Delay(delay))
                    {
                        ob.BackColor = Color.FromArgb(r, g, b);
                    }
                }
                else
                {
                    for (int r = r0, g = g0, b = b0; r <= r1 & g <= g1 & b <= b1; r += stepR, g += stepG, b += stepB, await Task.Delay(delay))
                    {
                        ob.BackColor = Color.FromArgb(r, g, b);
                    }
                }
                ob.BackColor = Color.FromArgb(r1, g1, b1);
            }
        }

        async private void Form1_Load(object sender, EventArgs e)
        {
            if (Properties.Settings.Default.startPage == "Цвет")
            {
                tabControl1.SelectTab(tabPage3);
                button_user.ForeColor = Color.FromArgb(-12799119);
                button_user_Click(this, new EventArgs());
            }
            if (Properties.Settings.Default.startPage == "Материал")
            {
                tabControl1.SelectTab(tabPage5);
                button_next_mats_Click(this, new EventArgs());
            }
            Properties.Settings.Default.startPage = null;
            Properties.Settings.Default.Save();
            ColorSW(245, 255, 250, 32, 178, 170, 80, Privetstvie, "Forecolor");

            await Task.Delay(1500);
            button_start.Visible = true;
            ColorSW(245, 255, 250, 60, 179, 113, 40, button_start, "Forecolor");
        }

        async private void button_start_Click(object sender, EventArgs e)
        {
            if (button_start.ForeColor.ToArgb() == -12799119)
            {
                ColorSW(32, 178, 170, 245, 255, 250, 40, Privetstvie,"Forecolor");
                ColorSW(60, 179, 113, 245, 255, 250, 40, button_start, "Forecolor");
                await Task.Delay(1300);
                button_start.Visible = false;
                button_razrab.Visible = true;
                button_user.Visible = true;

                ColorSW(245, 255, 250, 32, 178, 170, 60, label_kto, "Forecolor");
                ColorSW(245, 255, 250, 60, 179, 113, 60, button_razrab, "Forecolor");
                ColorSW(245, 255, 250, 60, 179, 113, 60, button_user, "Forecolor");
            }
        }
        async private void button_razrab_Click(object sender, EventArgs e)
        {
            if (button_razrab.ForeColor.ToArgb() == -12799119)
            {
                ColorSW(32, 178, 170, 245, 255, 250, 40, label_kto, "Forecolor");
                ColorSW(60, 179, 113, 245, 255, 250, 60, button_razrab, "Forecolor");
                ColorSW(60, 179, 113, 245, 255, 250, 60, button_user, "Forecolor");
                await Task.Delay(1700);
                button_razrab.Visible = false;
                button_user.Visible = false;

                label_password.Visible = true;
                textBox_password.Visible = true;
                button_pasOK.Visible = true;
                button_pasBack.Visible = true;
            }
        }
        private void button_pasOK_Click(object sender, EventArgs e)
        {
            if (textBox_password.Text == "000")
            {
                tabControl1.SelectTab(tabPage2);
                
            }
            else
            {
                label_password.Text = "Неправильный пароль";
                label_password.ForeColor = Color.Red;
            }
        }
        private void button_pasBack_Click(object sender, EventArgs e)
        {
            label_password.Visible = false;
            textBox_password.Visible = false;
            button_pasOK.Visible = false;
            button_pasBack.Visible = false;

            button_razrab.Visible = true;
            button_user.Visible = true;

            ColorSW(245, 255, 250, 32, 178, 170, 60, label_kto, "Forecolor");
            ColorSW(245, 255, 250, 60, 179, 113, 60, button_razrab, "Forecolor");
            ColorSW(245, 255, 250, 60, 179, 113, 60, button_user, "Forecolor");
        }
        //Пользовательское окно выбора цвета
        async private void button_user_Click(object sender, EventArgs e)
        {
            if (button_user.ForeColor.ToArgb() == -12799119)
            {
                if (Properties.Settings.Default.startPage != "Цвет")
                {
                    ColorSW(32, 178, 170, 245, 255, 250, 40, label_kto, "Forecolor");
                    ColorSW(60, 179, 113, 245, 255, 250, 60, button_razrab, "Forecolor");
                    ColorSW(60, 179, 113, 245, 255, 250, 60, button_user, "Forecolor");

                    await Task.Delay(1700);
                }

                button_razrab.Visible = false;
                button_user.Visible = false;
                tabControl1.SelectTab(tabPage3);
                for (int x = 8; x <= 114; x += 10, await Task.Delay(1))
                {
                    button_silver.Location = new Point(x, 283);
                }
                button_silver.Location = new Point(114, 283);
                for (int x = 8; x <= 220; x += 15, await Task.Delay(1))
                {
                    button_red.Location = new Point(x, 283);
                }
                button_red.Location = new Point(220, 283);
                for (int x = 8; x <= 326; x += 20, await Task.Delay(1))
                {
                    button_orange.Location = new Point(x, 283);
                }
                button_orange.Location = new Point(326, 283);
                for (int x = 8; x <= 432; x += 25, await Task.Delay(1))
                {
                    button_yellow.Location = new Point(x, 283);
                }
                button_yellow.Location = new Point(432, 283);
                for (int x = 8; x <= 538; x += 30, await Task.Delay(1))
                {
                    button_green.Location = new Point(x, 283);
                }
                button_green.Location = new Point(538, 283);
                for (int x = 8; x <= 644; x += 35, await Task.Delay(1))
                {
                    button_сyan.Location = new Point(x, 283);
                }
                button_сyan.Location = new Point(644, 283);
                for (int x = 8; x <= 750; x += 40, await Task.Delay(1))
                {
                    button_blue.Location = new Point(x, 283);
                }
                button_blue.Location = new Point(750, 283);
                for (int x = 8; x <= 856; x += 45, await Task.Delay(1))
                {
                    button_fuchsia.Location = new Point(x, 283);
                }
                button_fuchsia.Location = new Point(856, 283);
                for (int x = 8; x <= 962; x += 50, await Task.Delay(1))
                {
                    button_black.Location = new Point(x, 283);
                }
                button_black.Location = new Point(962, 283);

                for (byte r = 245, g = 255, b = 250; r >= 32 & g >= 178 & b >= 170; r -= 11, g -= 4, b -= 4, await Task.Delay(80))
                {
                    label_choose_color.ForeColor = Color.FromArgb(r, g, b);
                    button_color1.ForeColor = Color.FromArgb(r, g, b);
                    button_color2.ForeColor = Color.FromArgb(r, g, b);
                    button_color3.ForeColor = Color.FromArgb(r, g, b);
                }
                label_choose_color.ForeColor = Color.FromArgb(32, 178, 170);
                button_color1.ForeColor = Color.FromArgb(32, 178, 170);
                button_color2.ForeColor = Color.FromArgb(32, 178, 170);
                button_color3.ForeColor = Color.FromArgb(32, 178, 170);
            }
        }
        //Метод переключения между цветами
        async public void Sw_help(Button bu, Button bu_l, Button bu_d)
        {
            for (int l = 177, d = 389; l <= 283 & d >= 283; l += 10, d -= 10, await Task.Delay(1))
            {
                bu_l.Location = new Point(bu.Location.X, l);
                bu_d.Location = new Point(bu.Location.X, d);
            }
            bu_l.Location = new Point(bu.Location.X, 283);
            bu_d.Location = new Point(bu.Location.X, 283);
            bu_l.Visible = false;
            bu_d.Visible = false;
        }

        public void Switch()
        {
            switch (open_color)
            {
                case "silver":
                    Sw_help(button_silver, button_silver_light, button_silver_dark);
                    break;

                case "red":
                    Sw_help(button_red, button_red_light, button_red_dark);
                    break;
                case "orange":
                    Sw_help(button_orange, button_orange_light, button_orange_dark);
                    break;
                case "yellow":
                    Sw_help(button_yellow, button_yellow_light, button_yellow_dark);
                    break;
                case "green":
                    Sw_help(button_green, button_green_light, button_green_dark);
                    break;
                case "cyan":
                    Sw_help(button_сyan, button_cyan_light, button_cyan_dark);
                    break;
                case "blue":
                    Sw_help(button_blue, button_blue_light, button_blue_dark);
                    break;
                case "fuchsia":
                    Sw_help(button_fuchsia, button_fuchsia_light, button_fuchsia_dark);
                    break;

            }
        }

        public void Choose()
        {
            if ((button_color1.BackColor != choosen_color) & (button_color2.BackColor != choosen_color) & (button_color3.BackColor != choosen_color))
            {
                if ((button_color1.BackColor == Color.MintCream) || button_color1.BackColor == choosen_color)
                { button_color1.BackColor = choosen_color; button_color1.ForeColor = Color.Lime; }
                else if ((button_color2.BackColor == Color.MintCream) || button_color2.BackColor == choosen_color)
                { button_color2.BackColor = choosen_color; button_color2.ForeColor = Color.Lime; }
                else if ((button_color3.BackColor == Color.MintCream) || button_color3.BackColor == choosen_color)
                { button_color3.BackColor = choosen_color; button_color3.ForeColor = Color.Lime; }
                if ((button_color1.BackColor != Color.MintCream) & (button_color2.BackColor != Color.MintCream) & (button_color3.BackColor != Color.MintCream))
                {
                    button_next.Visible = true;
                    ColorSW(245, 255, 250, 60, 179, 113, 40, button_next, "Forecolor");
                }
            }

        }

        private void button_white_Click(object sender, EventArgs e)
        {
            Switch();
            choosen_color = button_white.BackColor;
            open_color = "white";
            Choose();
        }
        //Метод выбора одного из 3 цветов
        async public void Onefrom3(Button bu, Button bu_l, Button bu_d, string op_col)
        {
            if (bu_l.Location == bu.Location)
            {
                Switch();
                bu_l.Visible = true;
                bu_d.Visible = true;
                for (int l = 283, d = 283; l >= 177 & d <= 389; l -= 10, d += 10, await Task.Delay(1))
                {
                    bu_l.Location = new Point(bu.Location.X, l);
                    bu_d.Location = new Point(bu.Location.X, d);
                }
                bu_l.Location = new Point(bu.Location.X, 177);
                bu_d.Location = new Point(bu.Location.X, 389);
                open_color = op_col;
            }
            else
            {
                choosen_color = bu.BackColor;
                Choose();
            }
        }

        private void button_silver_Click(object sender, EventArgs e)
        {
            Onefrom3(button_silver, button_silver_light, button_silver_dark, "silver");
        }

        private void button_red_Click(object sender, EventArgs e)
        {
            Onefrom3(button_red, button_red_light, button_red_dark, "red");
        }

        private void button_orange_Click(object sender, EventArgs e)
        {
            Onefrom3(button_orange, button_orange_light, button_orange_dark, "orange");
        }

        private void button_yellow_Click(object sender, EventArgs e)
        {
            Onefrom3(button_yellow, button_yellow_light, button_yellow_dark, "yellow");
        }

        private void button_green_Click(object sender, EventArgs e)
        {
            Onefrom3(button_green, button_green_light, button_green_dark, "green");
        }

        private void button_сyan_Click(object sender, EventArgs e)
        {
            Onefrom3(button_сyan, button_cyan_light, button_cyan_dark, "cyan");
        }

        private void button_blue_Click(object sender, EventArgs e)
        {
            Onefrom3(button_blue, button_blue_light, button_blue_dark, "blue");
        }

        private void button_fuchsia_Click(object sender, EventArgs e)
        {
            Onefrom3(button_fuchsia, button_fuchsia_light, button_fuchsia_dark, "fuchsia");
        }

        private void button_black_Click(object sender, EventArgs e)
        {
            Switch();
            choosen_color = button_black.BackColor;
            open_color = "black";
            Choose();
        }

        private void button_color1_Click(object sender, EventArgs e)
        {
            button_color1.BackColor = Color.MintCream;
            button_color1.ForeColor = Color.LightSeaGreen;
            if ((button_color1.BackColor == Color.MintCream) || (button_color2.BackColor == Color.MintCream) || (button_color3.BackColor == Color.MintCream))
                button_next.Visible = false;

        }

        private void button_color2_Click(object sender, EventArgs e)
        {
            button_color2.BackColor = Color.MintCream;
            button_color2.ForeColor = Color.LightSeaGreen;
            if ((button_color1.BackColor == Color.MintCream) || (button_color2.BackColor == Color.MintCream) || (button_color3.BackColor == Color.MintCream))
                button_next.Visible = false;
        }

        private void button_color3_Click(object sender, EventArgs e)
        {
            button_color3.BackColor = Color.MintCream;
            button_color3.ForeColor = Color.LightSeaGreen;
            if ((button_color1.BackColor == Color.MintCream) || (button_color2.BackColor == Color.MintCream) || (button_color3.BackColor == Color.MintCream))
                button_next.Visible = false;
        }

        private void button_colors_Click(object sender, EventArgs e)
        {
            if (sender.Equals(button_silver_light))
            {
                choosen_color = button_silver_light.BackColor;
                Choose();
            }
            if (sender.Equals(button_silver_dark))
            { choosen_color = button_silver_dark.BackColor; Choose(); }
            if (sender.Equals(button_red_light))
            { choosen_color = button_red_light.BackColor; Choose(); }
            if (sender.Equals(button_red_dark))
            { choosen_color = button_red_dark.BackColor; Choose(); }
            if (sender.Equals(button_orange_light))
            { choosen_color = button_orange_light.BackColor; Choose(); }
            if (sender.Equals(button_orange_dark))
            { choosen_color = button_orange_dark.BackColor; Choose(); }
            if (sender.Equals(button_yellow_light))
            { choosen_color = button_yellow_light.BackColor; Choose(); }
            if (sender.Equals(button_yellow_dark))
            { choosen_color = button_yellow_dark.BackColor; Choose(); }
            if (sender.Equals(button_green_light))
            { choosen_color = button_green_light.BackColor; Choose(); }
            if (sender.Equals(button_green_dark))
            { choosen_color = button_green_dark.BackColor; Choose(); }
            if (sender.Equals(button_cyan_light))
            { choosen_color = button_cyan_light.BackColor; Choose(); }
            if (sender.Equals(button_cyan_dark))
            { choosen_color = button_cyan_dark.BackColor; Choose(); }
            if (sender.Equals(button_blue_light))
            { choosen_color = button_blue_light.BackColor; Choose(); }
            if (sender.Equals(button_blue_dark))
            { choosen_color = button_blue_dark.BackColor; Choose(); }
            if (sender.Equals(button_fuchsia_light))
            { choosen_color = button_fuchsia_light.BackColor; Choose(); }
            if (sender.Equals(button_fuchsia_dark))
            { choosen_color = button_fuchsia_dark.BackColor; Choose(); }
        }

        public void What_color(Color some_color)
        {
            String[,] col = { {"Белый" , "Color [White]"},
            {"Светло-серый" , "Color [A=255, R=224, G=224, B=224]"},
            {"Серый" , "Color [Silver]"},
            { "Темно-серый" , "Color [Gray]"},
            {"Светло-красный" , "Color [A=255, R=255, G=128, B=128]"},
            {"Красный" , "Color [Red]"},
            {"Темно-красный" , "Color [A=255, R=192, G=0, B=0]"},
            {"Светло-оранжевый" , "Color [A=255, R=255, G=192, B=128]"},
            {"Оранжевый" , "Color [A=255, R=255, G=128, B=0]"},
            {"Темно-оранжевый" , "Color [A=255, R=192, G=64, B=0]"},
            {"Светло-жёлтый" , "Color [A=255, R=255, G=255, B=128]"},
            {"Жёлтый" , "Color [Yellow]"},
            {"Темно-жёлтый" , "Color [A=255, R=192, G=192, B=0]"},
            {"Светло-зелёный" , "Color [A=255, R=128, G=255, B=128]"},
            {"Зелёный" , "Color [Lime]"},
            {"Темно-зелёный" , "Color [Green]"},
            {"Светло-бирюзовый" , "Color [A=255, R=192, G=255, B=255]"},
            {"Бирюзовый" , "Color [Cyan]"},
            {"Темно-бирюзовый" , "Color [Teal]"},
            {"Светло-синий" , "Color [A=255, R=128, G=128, B=255]"},
            {"Синий" , "Color [Blue]"},
            {"Тёмно-синий" , "Color [Navy]"},
            {"Светло-розовый" , "Color [A=255, R=255, G=128, B=255]"},
            {"Розовый" , "Color [Fuchsia]"},
            {"Тёмно-розовый" , "Color [Purple]"},
            {"Чёрный" , "Color [Black]"} };
            for (int n = 0; n <= col.GetUpperBound(0); n++) 
            {
                if (some_color == ceiling)
                {
                    if (some_color.ToString() == col[n, 1])
                    {
                        ceiling_color = col[n, 0];
                    }
                }
                if (some_color == wall)
                {
                    if (some_color.ToString() == col[n, 1])
                    {
                        wall_color = col[n, 0];
                    }
                }
                if (some_color == floor)
                {
                    if (some_color.ToString() == col[n, 1])
                    {
                        floor_color = col[n, 0];
                    }
                }
            }
        }
                                                                                                     //Форма добавления материала разработчиком
        private void comboBox_r_poverhnost_SelectedIndexChanged(object sender, EventArgs e)    //Изменение типа поверхности
        {
            if (comboBox_r_poverhnost.SelectedIndex == 0)
            {
                string[] mas_potolok = {"Натяжной ПВХ потолок", "Краска", "Штукатурка", "Обои", "Панели" };
                comboBox_r_material.Items.Clear();
                comboBox_r_material.Items.AddRange(mas_potolok);
                poverhnost = "potolok";
                load_rtab(poverhnost);
            }
            if (comboBox_r_poverhnost.SelectedIndex == 1)
            {
                string[] mas_stena = {"Краска", "Штукатурка", "Обои", "Керамическая плитка", "Панели" };
                comboBox_r_material.Items.Clear();
                comboBox_r_material.Items.AddRange(mas_stena);
                poverhnost = "stena";
                load_rtab(poverhnost);
            }
            if (comboBox_r_poverhnost.SelectedIndex == 2)
            {
                string[] mas_pol = {"Линолеум", "Паркет", "Кафель", "Наливной пол", "Ламинат"};
                comboBox_r_material.Items.Clear();
                comboBox_r_material.Items.AddRange(mas_pol);
                poverhnost = "pol";
                load_rtab(poverhnost);
            }
            clear_rboxes();
        }

        public void clear_rboxes()                                                          //Метод очистки полей формы разработчика
        {
            comboBox_r_cvet.Text = "";
            comboBox_r_material.Text = "";
            textBox_r_name.Text = "";
            richTextBox1.Text = "";
            pictureBox2.Image = global::Kursach.Properties.Resources.add_picture;
        }

        public void load_rtab(string poverhnost)                                                //Метод загрузки данных в таблицу
        {
                listView1.Items.Clear();
                ID_sotr = new List<int>();
                OleDbCommand command = new OleDbCommand("SELECT * FROM "+poverhnost, conn);
                OleDbDataReader reader = command.ExecuteReader();
                while (reader.Read() != false)
                {
                    ListViewItem item = listView1.Items.Add(new ListViewItem());
                    item.Text = reader["cvet"].ToString();
                    item.SubItems.Add(reader["material"].ToString());
                    item.SubItems.Add(reader["name"].ToString());
                    item.SubItems.Add(reader["description"].ToString());
                    ID_sotr.Add(Convert.ToInt32(reader["id"]));
                }
                reader.Close();

            button2.Text = "Добавить материал";
            mode = "add";
        }

        private void listView1_ItemActivate(object sender, EventArgs e)                     //Выделение, просмотр, изменение данных в таблице
        {
            bmp = null;
                int i = listView1.SelectedIndices[0];
                OleDbCommand cmd = new OleDbCommand("SELECT " + poverhnost + ".photo FROM " + poverhnost + " WHERE id = " + ID_sotr[i] + "", conn);
                OleDbDataReader read = cmd.ExecuteReader();
                while (read.Read())
                {
                    MemoryStream ms = (MemoryStream)read.GetStream(0);
                    bmp = new Bitmap(ms);
                    if (bmp != null)
                    {
                        pictureBox2.Image = bmp;
                    }
                }
                i = listView1.SelectedIndices[0];
                OleDbCommand command = new OleDbCommand("SELECT cvet, material, name, description FROM " + poverhnost+ " WHERE id = " + ID_sotr[i] + "", conn);
                OleDbDataReader reader = command.ExecuteReader();
                while (reader.Read() != false)
                {
                    comboBox_r_cvet.Text = reader["cvet"].ToString();
                    comboBox_r_material.Text = reader["material"].ToString();
                    textBox_r_name.Text = reader["name"].ToString();
                    richTextBox1.Text = reader["description"].ToString();
                }
                read.Close();
            button2.Text = "Изменить материал";
            mode = "change";
        }

        public void toBD_fromTab(string poverhnost)                                         //Метод добавления данных в БД (mode = "add") и изменения данных в БД (mode = "change")
        {
            if (mode == "add")
            {
                    OleDbCommand cmd = new OleDbCommand("INSERT INTO " + poverhnost + "" +
                        "(cvet, material, name, description, photo) VALUES (@cvet, @material, @name, @description, @img)", conn);
                    cmd.Parameters.Add("@cvet", OleDbType.VarChar);
                    cmd.Parameters["@cvet"].Value = comboBox_r_cvet.Text;
                    cmd.Parameters.Add("@material", OleDbType.VarChar);
                    cmd.Parameters["@material"].Value = comboBox_r_material.Text;
                    cmd.Parameters.Add("@name", OleDbType.VarChar);
                    cmd.Parameters["@name"].Value = textBox_r_name.Text;
                    cmd.Parameters.Add("@description", OleDbType.VarChar);
                    cmd.Parameters["@description"].Value = richTextBox1.Text;

                    cmd.Parameters.Add("@img", OleDbType.VarBinary);
                    MemoryStream ms = new MemoryStream();
                    if (bmp != null)
                    {
                        bmp.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                        cmd.Parameters["@img"].Value = ms.ToArray();
                        cmd.ExecuteNonQuery();
                        load_rtab(poverhnost);
                    }
                    else
                    {
                        MessageBox.Show("Нет картинки");
                    }
            }
            if (mode == "change")
            {
                    int i = listView1.SelectedIndices[0];
                    OleDbCommand cmd = new OleDbCommand("UPDATE " + poverhnost + "" +
                        " SET [cvet]=@cvet, [material]=@material, [name]=@name, " +
                        "[description]=@description, [photo]=@img WHERE id = " + ID_sotr[i] + "", conn);
                    cmd.Parameters.Add("@cvet", OleDbType.VarChar);
                    cmd.Parameters["@cvet"].Value = comboBox_r_cvet.Text;
                    cmd.Parameters.Add("@material", OleDbType.VarChar);
                    cmd.Parameters["@material"].Value = comboBox_r_material.Text;
                    cmd.Parameters.Add("@name", OleDbType.VarChar);
                    cmd.Parameters["@name"].Value = textBox_r_name.Text;
                    cmd.Parameters.Add("@description", OleDbType.VarChar);
                    cmd.Parameters["@description"].Value = richTextBox1.Text;


                    cmd.Parameters.Add("@img", OleDbType.VarBinary);
                    MemoryStream ms = new MemoryStream();
                    if (bmp != null)
                    {
                        bmp.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                        cmd.Parameters["@img"].Value = ms.ToArray();
                        cmd.ExecuteNonQuery();
                        load_rtab(poverhnost);
                    }
                    else
                    {
                        MessageBox.Show("Нет картинки");
                    }
            }
        }


        private void button2_Click(object sender, EventArgs e)
        {
            toBD_fromTab(poverhnost);
            load_rtab(poverhnost);
            clear_rboxes();
            button2.Text = "Добавить материал";
        }

        private void pictureBox2_Click(object sender, EventArgs e)                      //Загрузка изображения из файлов в pictureBox
        {
            OpenFileDialog opf = new OpenFileDialog();
            opf.Filter = "JPG|*.jpg";
            if (opf.ShowDialog() == DialogResult.OK)
            {
                bmp = new Bitmap(opf.FileName);
                pictureBox2.Image = bmp;
            }
        }

        private void listView1_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                int i = listView1.SelectedIndices[0];
                OleDbCommand cmd = new OleDbCommand("DELETE FROM " + poverhnost + " WHERE id = " + ID_sotr[i] + "", conn);
                cmd.ExecuteNonQuery();
                OleDbCommand com = new OleDbCommand("ALTER TABLE " + poverhnost + " DROP COLUMN id", conn);
                com.ExecuteNonQuery();
                OleDbCommand coma = new OleDbCommand("ALTER TABLE " + poverhnost + " ADD COLUMN id counter(1,1)", conn);
                coma.ExecuteNonQuery();
                load_rtab(poverhnost);
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            conn.Close(); //Закрытие соединения с БД при закрытии формы
        }
        async private void button_r_menu_Click(object sender, EventArgs e)
        {
            label_password.Visible = false;
            textBox_password.Visible = false;
            button_pasOK.Visible = false;
            button_pasBack.Visible = false;
            button_start.BackColor = Color.MintCream;
            button_start.Visible = true;
            tabControl1.SelectTab(tabPage1);
            ColorSW(245, 255, 250, 32, 178, 170, 80, Privetstvie, "Forecolor");

            await Task.Delay(1500);

            ColorSW(245, 255, 250, 60, 179, 113, 40, button_start, "Forecolor");
        }

        public void color_Click(string poverhnost, Color cover)
        {
            Graphics g = Graphics.FromImage(pictureBox_room.Image);
            SolidBrush cocl = new SolidBrush(cover);
            Pen blackPen = new Pen(Color.Black, 8);
            switch (poverhnost)
            {
                case "ceiling":
                    g.FillPolygon(cocl, new[] { new Point(7, 0), new Point(810, 0), new Point(552, 165), new Point(262, 165) });
                    pictureBox_room.Invalidate();
                break;

                case "wall":
                    g.FillPolygon(cocl, new[] { new Point(0, 0), new Point(260,167), new Point(260,352), new Point(0, 534) });
                    g.FillPolygon(cocl, new[] { new Point(264,169), new Point(549,169), new Point(549,351), new Point(264,351) });
                    g.FillPolygon(cocl, new[] { new Point(553,168), new Point(816,1), new Point(816,533), new Point(553, 353) });
                    g.FillRectangle(Brushes.MintCream, 366, 190, 80, 120); g.DrawRectangle(blackPen, 366, 190, 80, 120);
                    pictureBox_room.Invalidate();
                break;

                case "floor":
                    g.FillPolygon(cocl, new[] { new Point(4, 537), new Point(261,355), new Point(551, 355), new Point(814, 537) });
                    pictureBox_room.Invalidate();
                break;

                case "load":
                    Graphics v = Graphics.FromImage(panel_vorota.Image);
                    SolidBrush ceil = new SolidBrush(button_color1.BackColor);
                    SolidBrush wal = new SolidBrush(button_color2.BackColor);
                    SolidBrush floo = new SolidBrush(button_color3.BackColor);
                    v.FillPolygon(ceil, new[] { new Point(7, 0), new Point(810, 0), new Point(552, 165), new Point(262, 165) });
                    v.FillPolygon(wal, new[] { new Point(0, 0), new Point(260, 167), new Point(260, 352), new Point(0, 534) });
                    v.FillPolygon(wal, new[] { new Point(264, 169), new Point(549, 169), new Point(549, 351), new Point(264, 351) });
                    v.FillPolygon(wal, new[] { new Point(553, 168), new Point(816, 1), new Point(816, 533), new Point(553, 353) });
                    v.FillRectangle(Brushes.MintCream, 366, 190, 80, 120); v.DrawRectangle(blackPen, 366, 190, 80, 120);
                    v.FillPolygon(floo, new[] { new Point(4, 537), new Point(261, 355), new Point(551, 355), new Point(814, 537) });
                    panel_vorota.Invalidate();
                break;
            }
        }
                                                                                                                                //Кнопка перехода к подбору цвтовых сочетаний
        async private void button_next_Click(object sender, EventArgs e)
        {
            panel_vorota.Location = new Point(1075, 3);
            panel_vorota.Visible = true;
            color_Click("load", Color.White);
            for (int x = panel_vorota.Location.X; x >= 3; x -= panel_vorota.Location.X / 3, await Task.Delay(1))
            {
                panel_vorota.Location = new Point(x, panel_vorota.Location.Y);
            }
            panel_vorota.Location = new Point(3, panel_vorota.Location.Y);
            ceiling = button_color1.BackColor;
            wall = button_color2.BackColor;
            floor = button_color3.BackColor;
            color_Click("ceiling", ceiling);
            color_Click("wall", wall);
            color_Click("floor", floor);
            panel_ceiling_color.BackColor = button_color1.BackColor;
            panel_wall_color.BackColor = button_color2.BackColor;
            panel_floor_color.BackColor = button_color3.BackColor;
            panel_menu.Location = new Point(-167, 196);
            tabControl1.SelectTab(tabPage4);
            for (double x = panel_menu.Location.X; x <= -0.5; x += -panel_menu.Location.X / (10-0.5), await Task.Delay(1))
            {
                panel_menu.Location = new Point(Convert.ToInt32(x), panel_menu.Location.Y);
            }
            panel_menu.Location = new Point(0, panel_menu.Location.Y);

        }

        public Color prev_next(Color cover, string step)
        {
            switch (step)
            { 
                case "next":
                if (cover == button_color3.BackColor)
                    cover = button_color3.BackColor;
                if (cover == button_color2.BackColor)
                    cover = button_color3.BackColor;
                if (cover == button_color1.BackColor)
                    cover = button_color2.BackColor;
                break;

                case "prev":
                if (cover == button_color1.BackColor)
                    cover = button_color1.BackColor;
                if (cover == button_color2.BackColor)
                    cover = button_color1.BackColor;
                if (cover == button_color3.BackColor)
                    cover = button_color2.BackColor;
                break;
            }
            return cover;
        }

        private void button_next_wall_Click(object sender, EventArgs e)
        {
            wall = prev_next(wall, "next");
            color_Click("wall",wall);
            panel_wall_color.BackColor = wall;
        }

        private void button_prev_wall_Click(object sender, EventArgs e)
        {
            wall = prev_next(wall, "prev");
            color_Click("wall", wall);
            panel_wall_color.BackColor = wall;
        }

        private void button_next_floor_Click(object sender, EventArgs e)
        {
            floor = prev_next(floor, "next");
            color_Click("floor", floor);
            panel_floor_color.BackColor = floor;
        }

        private void button_prev_floor_Click(object sender, EventArgs e)
        {
            color_Click("floor", prev_next(floor, "prev"));
            floor = prev_next(floor, "prev");
            panel_floor_color.BackColor = floor;
        }

        public void button_prev_potolok_Click(object sender, EventArgs e)
        {
            color_Click("ceiling", prev_next(ceiling, "prev"));
            ceiling = prev_next(ceiling, "prev");
            panel_ceiling_color.BackColor = ceiling;
        }

        private void button_next_potolok_Click(object sender, EventArgs e)
        {
            color_Click("ceiling", prev_next(ceiling, "next"));
            ceiling = prev_next(ceiling, "next");
            panel_ceiling_color.BackColor = ceiling;
        }

        async private void button_next_mats_Click(object sender, EventArgs e)
        {
            if (Properties.Settings.Default.startPage != "Материал")
            {
                Graphics g = Graphics.FromImage(pictureBox_room.Image);
                Pen blackPen = new Pen(Color.Black, 8);
                for (int z = panel_menu.Location.X, x = 366, y = 190, width = 80, height = 120; z >= -1500 & x <= 3860 & y <= 1900 & width <= 1000 & height <= 1200; z -= 36, x -= 10, y -= 10, width += 20, height += 20, await Task.Delay(1))
                {
                    panel_menu.Location = new Point(z, panel_menu.Location.Y);
                    g.FillRectangle(Brushes.MintCream, x, y, width, height); g.DrawRectangle(blackPen, x, y, width, height);
                    pictureBox_room.Invalidate();
                }
                tabControl1.SelectTab(tabPage5);
            }
            else
            {
                ceiling = Properties.Settings.Default.Color_ceiling;
                wall = Properties.Settings.Default.Color_wall;
                floor = Properties.Settings.Default.Color_floor;
            }
            ColorSW(245, 255, 250, 32, 178, 170, 80, label_choose_material, "Forecolor");

            await Task.Delay(1000);
            ColorSW(245, 255, 250, 60, 179, 113, 100, label_ceiling, "Forecolor");
            ColorSW(245, 255, 250, 60, 179, 113, 100, label_wall, "Forecolor");
            ColorSW(245, 255, 250, 60, 179, 113, 100, label_floor, "Forecolor");
            button_ceiling_strech.Visible = true; 
            button_wall_paint.Visible = true; 
            button_floor_linoleum.Visible = true; 
            await Task.Delay(250);
            button_ceiling_paint.Visible = true; 
            button_wall_plaster.Visible = true; 
            button_floor_parquet.Visible = true; 
            await Task.Delay(250);
            button_ceiling_plaster.Visible = true; 
            button_wall_wallpaper.Visible = true; 
            button_floor_tile.Visible = true;
            await Task.Delay(250);
            button_ceiling_wallpaper.Visible = true; 
            button_wall_ceramic.Visible = true; 
            button_floor_selfleveling.Visible = true;
            await Task.Delay(250);
            button_ceiling_panels.Visible = true; 
            button_wall_panels.Visible = true; 
            button_floor_laminate.Visible = true; 


        }

        async private void button_back_cvet_Click(object sender, EventArgs e)
        {
            button_color1.Visible = false;
            button_color2.Visible = false;
            button_color3.Visible = false;
            Switch();
            button_next.Visible = false;
            pictureBox_gate.Location = new Point(-1072, 0);
            pictureBox_gate.Visible = true;
            for (double x = pictureBox_gate.Location.X; x <= 3; x += -pictureBox_gate.Location.X / 3, await Task.Delay(1))
            {
                pictureBox_gate.Location = new Point(Convert.ToInt32(x), pictureBox_gate.Location.Y);
                if (x >= -9)
                    x+=3;
            }
            panel_vorota.Visible = false;
            tabControl1.SelectTab(tabPage3);
            button_next.Visible = true;
            ColorSW(245, 255, 250, 60, 179, 113, 40, button_next, "Forecolor");
            pictureBox_gate.Visible = false;
            button_color3.Visible = true;
            await Task.Delay(60);
            button_color2.Visible = true;
            await Task.Delay(60);
            button_color1.Visible = true;

        }

        async public void spaace(Button kosmo)
        {
            kosmo.Location = new Point(kosmo.Location.X, kosmo.Location.Y + 2); await Task.Delay(30);
            kosmo.Location = new Point(kosmo.Location.X, kosmo.Location.Y + 2); await Task.Delay(30);
            kosmo.Location = new Point(kosmo.Location.X, kosmo.Location.Y + 2); await Task.Delay(30);
            for (int y = kosmo.Location.Y; y >= -300; y -= 30, await Task.Delay(1))
            {
                kosmo.Location = new Point(kosmo.Location.X, y);
            }
            kosmo.Location = new Point(kosmo.Location.X, -300);
        }

        async public void backfromspace(Button kosmo,int ground)
        {
            for (int y = kosmo.Location.Y; y <= ground+6; y += 30, await Task.Delay(1))
            {
                kosmo.Location = new Point(kosmo.Location.X, y);
            }
            kosmo.Location = new Point(kosmo.Location.X, ground+6);
            kosmo.Location = new Point(kosmo.Location.X, kosmo.Location.Y - 2); await Task.Delay(30);
            kosmo.Location = new Point(kosmo.Location.X, kosmo.Location.Y - 2); await Task.Delay(30);
            kosmo.Location = new Point(kosmo.Location.X, kosmo.Location.Y - 2); await Task.Delay(30);
        }

        public void choose_material(object sender, EventArgs e)
        {
            string s = ((Button)sender).Name;
            if (s.IndexOf("_ceiling_") > -1)
            {
                Button[] ceiling = new Button[] { button_ceiling_strech, button_ceiling_paint, button_ceiling_plaster, button_ceiling_wallpaper, button_ceiling_panels };
                for (int a = 0; a != 5; a++)
                {
                    if (sender != ceiling[a])
                    {
                        if (ceiling[a].Location.Y == 60)
                        {
                            spaace(ceiling[a]);
                            count_materials++;
                            ceiling_material = ((Button)sender);
                        }
                        if (ceiling[a].Location.Y == -300)
                        {
                            backfromspace(ceiling[a], 60);
                            count_materials--;
                            ceiling_material = null;
                        }
                    }
                }
            }
            if (s.IndexOf("_wall_") > -1)
            {
                Button[] wall = new Button[] { button_wall_paint, button_wall_plaster, button_wall_wallpaper, button_wall_ceramic, button_wall_panels };
                for (int b = 0; b != 5; b++)
                {
                    if (sender != wall[b])
                    {
                        if (wall[b].Location.Y == 270)
                        {
                            spaace(wall[b]);
                            count_materials++;
                            wall_material = ((Button)sender);
                        }
                        if (wall[b].Location.Y == -300)
                        {
                            backfromspace(wall[b], 270);
                            count_materials--;
                            wall_material = null;
                        }
                    }
                }
            }
            if (s.IndexOf("_floor_") > -1)
            {
                Button[] floor = new Button[] { button_floor_linoleum, button_floor_parquet, button_floor_tile, button_floor_selfleveling, button_floor_laminate };
                for (int c = 0; c != 5; c++)
                {
                    if (sender != floor[c])
                    {
                        if (floor[c].Location.Y == 480)
                        {
                            spaace(floor[c]);
                            count_materials++;
                            floor_material = ((Button)sender);
                        }
                        if (floor[c].Location.Y == -300)
                        {
                            backfromspace(floor[c], 480);
                            count_materials--;
                            floor_material = null;
                        }
                    }
                }
            }
            if (count_materials == 12)
            {
                button_next_u_bd.Enabled = true;
                button_next_u_bd.ForeColor = Color.MediumSeaGreen;
            }
            else
            {
                button_next_u_bd.Enabled = false;
                button_next_u_bd.ForeColor = Color.Gray;
            }
        }

        async private void button_next_u_bd_Click(object sender, EventArgs e)
        {
            for (int y = label_choose_material.Location.Y, x = label_ceiling.Location.X; y >= -300 & x>=-150; y -= 30, x-=30 , await Task.Delay(1))
            {
                label_choose_material.Location = new Point(label_choose_material.Location.X, y);
                button_next_u_bd.Location = new Point(label_choose_material.Location.X, y);
                label_ceiling.Location = new Point(x, label_ceiling.Location.Y);
                label_wall.Location = new Point(x, label_wall.Location.Y);
                label_floor.Location = new Point(x, label_floor.Location.Y);
            }
            Button[] who = new Button[] { button_ceiling_strech, button_ceiling_paint, button_ceiling_plaster, button_ceiling_wallpaper, button_ceiling_panels, button_wall_paint, button_wall_plaster, button_wall_wallpaper, button_wall_ceramic, button_wall_panels, button_floor_linoleum, button_floor_parquet, button_floor_tile, button_floor_selfleveling, button_floor_laminate };
            for (int x = 150, y = 150; x >= 0 & y >= 0; x -= 10, y -= 10, await Task.Delay(1))
            {
                for (int i=0;i!=15;i++)
                {
                    if (ceiling_material == who[i])
                    {
                        who[i].Size = new Size(x, y);
                        
                    }
                    if (wall_material == who[i])
                    {
                        who[i].Size = new Size(x, y);
                    }
                    if (floor_material == who[i])
                    {
                        who[i].Size = new Size(x, y);
                    }
                    if (x <= 0)
                        who[i].Visible = false;
                }
            }

            load_fromBd_toCeiling();
            load_fromBd_toWall();
            load_fromBd_toFloor();
            tabControl1.SelectTab(tabPage6);
        }

        public void What_material(String name)
        {
            String[,] mat = { {"Натяжной ПВХ потолок", "button_ceiling_strech"},
            {"Краска", "button_ceiling_paint"},
            {"Штукатурка", "button_ceiling_plaster"},
            {"Обои", "button_ceiling_wallpaper"},
            {"Панели", "button_ceiling_panels"},
            {"Краска" , "button_wall_paint"},
            {"Штукатурка" , "button_wall_plaster"},
            {"Обои" , "button_wall_wallpaper"},
            {"Керамическая плитка" , "button_wall_ceramic"},
            {"Панели" , "button_wall_panels"},
            {"Линолеум" , "button_floor_linoleum"},
            {"Паркет" , "button_floor_parquet"},
            {"Кафель" , "button_floor_tile"},
            {"Наливной пол" , "button_floor_selfleveling"},
            {"Ламинат" , "button_floor_laminate"} };
            for (int n = 0; n <= mat.GetUpperBound(0); n++)
            {
                if (name.IndexOf("_ceiling_") > -1)
                {
                    if (name == mat[n, 1])
                    {
                        ceiling_mat = mat[n, 0];
                    }
                }
                if (name.IndexOf("_wall_") > -1)
                {
                    if (name.ToString() == mat[n, 1])
                    {
                        wall_mat = mat[n, 0];
                    }
                }
                if (name.IndexOf("_floor_") > -1)
                {
                    if (name.ToString() == mat[n, 1])
                    {
                        floor_mat = mat[n, 0];
                    }
                }
            }
        }

        public Panel[] pan = new Panel[30];
        public PictureBox[] pic = new PictureBox[30];
        public Label[] lab = new Label[30];
        List<Int32> Id = new List<Int32>();

        public void load_fromBd_toCeiling()
        {
            What_material(ceiling_material.Name);
            What_material(wall_material.Name);
            What_material(floor_material.Name);
            What_color(ceiling);
            What_color(wall);
            What_color(floor);
            OleDbCommand com = conn.CreateCommand();
            com.CommandText = "SELECT count(*) FROM potolok WHERE (cvet = '" + ceiling_color + "') AND (material = '" + ceiling_mat + "')";
            OleDbCommand comm;
            OleDbDataReader reade;
            OleDbCommand cmd;
            OleDbDataReader read;
            OleDbCommand command;
            OleDbDataReader reader;
            int count = System.Convert.ToInt32(com.ExecuteScalar());
            int x = 3, y = 0;
            for (int i=0;i<=count-1;i++)
            {
                pan[i] = new Panel();
                pan[i].Location = new Point(x,y);
                pan[i].Size = new Size(200,200);
                pan[i].BackColor = Color.MintCream;
                pan[i].BorderStyle = BorderStyle.Fixed3D;
                panel_ceilings.Controls.Add(pan[i]);
                pic[i] = new PictureBox();
                pic[i].Location = new Point(0, 0);
                pic[i].Size = new Size(200, 150);
                pic[i].BackColor = Color.MintCream;
                pic[i].SizeMode = PictureBoxSizeMode.StretchImage;
                pic[i].Click += Description_ce;
                comm = new OleDbCommand("SELECT id FROM potolok WHERE (cvet = '" + ceiling_color + "') AND (material = '" + ceiling_mat + "')", conn);
                reade = comm.ExecuteReader();
                while (reade.Read())
                {
                    Id.Add(reade.GetInt32(0));
                }
                reade.Close();
                cmd = new OleDbCommand("SELECT potolok.photo FROM potolok WHERE (id = " + Id[i] + ") AND (cvet = '" + ceiling_color + "') AND (material = '" + ceiling_mat + "')", conn);
                read = cmd.ExecuteReader();
                while (read.Read())
                {
                    MemoryStream ms = (MemoryStream)read.GetStream(0);
                    bmp = new Bitmap(ms);
                    if (bmp != null)
                    {
                        pic[i].Image = bmp;
                    }
                }
                read.Close();
                lab[i] = new Label();
                lab[i].Location = new Point(25,155);
                lab[i].Size = new Size(150, 40);
                lab[i].Font = new Font("Masiva", 10);
                lab[i].BackColor = Color.MintCream;
                lab[i].ForeColor = Color.MediumSeaGreen;
                command = new OleDbCommand("SELECT potolok.name FROM potolok WHERE (id = " + Id[i] + ") AND (cvet = '" + ceiling_color + "') AND (material = '" + ceiling_mat + "')", conn);
                reader = command.ExecuteReader();
                while (reader.Read() != false)
                {
                    lab[i].Text = reader["name"].ToString();
                }
                reader.Close();
                pic[i].Name = Id[i].ToString();
                pan[i].Controls.Add(pic[i]);
                pan[i].Controls.Add(lab[i]);

                x+=203;
            }
        }

        public Panel[] pane = new Panel[30];
        public PictureBox[] pich = new PictureBox[30];
        public Label[] labl = new Label[30];

        List<Int32> Idi = new List<Int32>();

        public void load_fromBd_toWall()
        {
            OleDbCommand com = conn.CreateCommand();
            com.CommandText = "SELECT count(*) FROM stena WHERE (cvet = '" + wall_color + "') AND (material = '" + wall_mat + "')";
            OleDbCommand comm;
            OleDbDataReader reade;
            OleDbCommand cmd;
            OleDbDataReader read;
            OleDbCommand command;
            OleDbDataReader reader;
            int count = System.Convert.ToInt32(com.ExecuteScalar());
            int x = 3, y = 0;
            for (int i = 0; i <= count - 1; i++)
            {
                pane[i] = new Panel();
                pane[i].Location = new Point(x, y);
                pane[i].Size = new Size(200, 200);
                pane[i].BackColor = Color.MintCream;
                pane[i].BorderStyle = BorderStyle.Fixed3D;
                panel_walls.Controls.Add(pane[i]);
                pich[i] = new PictureBox();
                pich[i].Location = new Point(0, 0);
                pich[i].Size = new Size(200, 150);
                pich[i].BackColor = Color.MintCream;
                pich[i].SizeMode = PictureBoxSizeMode.StretchImage;
                pich[i].Click += Description_w;
                comm = new OleDbCommand("SELECT id FROM stena WHERE (cvet = '" + wall_color + "') AND (material = '" + wall_mat + "')", conn);
                reade = comm.ExecuteReader();
                while (reade.Read())
                {
                    Idi.Add(reade.GetInt32(0));
                }
                reade.Close();
                cmd = new OleDbCommand("SELECT stena.photo FROM stena WHERE (id = " + Idi[i] + ") AND (cvet = '" + wall_color + "') AND (material = '" + wall_mat + "')", conn);
                read = cmd.ExecuteReader();
                while (read.Read())
                {
                    MemoryStream ms = (MemoryStream)read.GetStream(0);
                    bmp = new Bitmap(ms);
                    if (bmp != null)
                    {
                        pich[i].Image = bmp;
                    }
                }
                read.Close();
                labl[i] = new Label();
                labl[i].Location = new Point(25, 155);
                labl[i].Size = new Size(150, 40);
                labl[i].Font = new Font("Masiva", 10);
                labl[i].BackColor = Color.MintCream;
                labl[i].ForeColor = Color.MediumSeaGreen;
                command = new OleDbCommand("SELECT stena.name FROM stena WHERE (id = " + Idi[i] + ") AND (cvet = '" + wall_color + "') AND (material = '" + wall_mat + "')", conn);
                reader = command.ExecuteReader();
                while (reader.Read() != false)
                {
                    labl[i].Text = reader["name"].ToString();
                }
                reader.Close();
                pich[i].Name = Idi[i].ToString();
                pane[i].Controls.Add(pich[i]);
                pane[i].Controls.Add(labl[i]);

                x += 203;
            }
        }

        public Panel[] panel = new Panel[30];
        public PictureBox[] piche = new PictureBox[30];
        public Label[] lable = new Label[30];
        List<Int32> IDi = new List<Int32>();

        public void load_fromBd_toFloor()
        {
            OleDbCommand com = conn.CreateCommand();
            com.CommandText = "SELECT count(*) FROM pol WHERE (cvet = '" + floor_color + "') AND (material = '" + floor_mat + "')";
            OleDbCommand comm;
            OleDbDataReader reade;
            OleDbCommand cmd;
            OleDbDataReader read;
            OleDbCommand command;
            OleDbDataReader reader;
            int count = System.Convert.ToInt32(com.ExecuteScalar());
            int x = 3, y = 0;
            for (int i = 0; i <= count - 1; i++)
            {
                panel[i] = new Panel();
                panel[i].Location = new Point(x, y);
                panel[i].Size = new Size(200, 200);
                panel[i].BackColor = Color.MintCream;
                panel[i].BorderStyle = BorderStyle.Fixed3D;
                panel_floors.Controls.Add(panel[i]);
                piche[i] = new PictureBox();
                piche[i].Location = new Point(0, 0);
                piche[i].Size = new Size(200, 150);
                piche[i].BackColor = Color.MintCream;
                piche[i].SizeMode = PictureBoxSizeMode.StretchImage;
                piche[i].Click += Description_fl;
                comm = new OleDbCommand("SELECT id FROM pol WHERE (cvet = '" + floor_color + "') AND (material = '" + floor_mat + "')", conn);
                reade = comm.ExecuteReader();
                while (reade.Read())
                {
                    IDi.Add(reade.GetInt32(0));
                }
                reade.Close();
                cmd = new OleDbCommand("SELECT pol.photo FROM pol WHERE (id = " + IDi[i] + ") AND (cvet = '" + floor_color + "') AND (material = '" + floor_mat + "')", conn);
                read = cmd.ExecuteReader();
                while (read.Read())
                {
                    MemoryStream ms = (MemoryStream)read.GetStream(0);
                    bmp = new Bitmap(ms);
                    if (bmp != null)
                    {
                        piche[i].Image = bmp;
                    }
                }
                read.Close();
                lable[i] = new Label();
                lable[i].Location = new Point(25, 155);
                lable[i].Size = new Size(150, 40);
                lable[i].Font = new Font("Masiva", 10);
                lable[i].BackColor = Color.MintCream;
                lable[i].ForeColor = Color.MediumSeaGreen;
                command = new OleDbCommand("SELECT pol.name FROM pol WHERE (id = " + IDi[i] + ") AND (cvet = '" + floor_color + "') AND (material = '" + floor_mat + "')", conn);
                reader = command.ExecuteReader();
                while (reader.Read() != false)
                {
                    lable[i].Text = reader["name"].ToString();
                }
                reader.Close();
                piche[i].Name = IDi[i].ToString();
                panel[i].Controls.Add(piche[i]);
                panel[i].Controls.Add(lable[i]);

                x += 203;
            }
        }

        void Description_ce(object sender, EventArgs e)
        {
            OleDbCommand command = new OleDbCommand("SELECT name, description, rashod FROM potolok WHERE id = " + ((PictureBox)sender).Name + "", conn);
            OleDbDataReader reader = command.ExecuteReader();
            while (reader.Read() != false)
            {
                label_u_name.Text = reader["name"].ToString();
                richTextBox_u_description.Text = reader["description"].ToString();
                //textBox_r_rashod.Text = reader["rashod"].ToString();
            }
            reader.Close();
            OleDbCommand cmd = new OleDbCommand("SELECT potolok.photo FROM potolok WHERE id = " + ((PictureBox)sender).Name + "", conn);
            OleDbDataReader read = cmd.ExecuteReader();
            while (read.Read())
            {
                MemoryStream ms = (MemoryStream)read.GetStream(0);
                bmp = new Bitmap(ms);
                if (bmp != null)
                {
                    pictureBox_u_photo.Image = bmp;
                }
            }
            tabControl1.SelectTab(tabPage7);
        }

        void Description_w(object sender, EventArgs e)
        {
            OleDbCommand command = new OleDbCommand("SELECT name, description, rashod FROM stena WHERE id = " + ((PictureBox)sender).Name + "", conn);
            OleDbDataReader reader = command.ExecuteReader();
            while (reader.Read() != false)
            {
                label_u_name.Text = reader["name"].ToString();
                richTextBox_u_description.Text = reader["description"].ToString();
                //textBox_r_rashod.Text = reader["rashod"].ToString();
            }
            reader.Close();
            OleDbCommand cmd = new OleDbCommand("SELECT stena.photo FROM stena WHERE id = " + ((PictureBox)sender).Name + "", conn);
            OleDbDataReader read = cmd.ExecuteReader();
            while (read.Read())
            {
                MemoryStream ms = (MemoryStream)read.GetStream(0);
                bmp = new Bitmap(ms);
                if (bmp != null)
                {
                    pictureBox_u_photo.Image = bmp;
                }
            }
            tabControl1.SelectTab(tabPage7);
        }
        void Description_fl(object sender, EventArgs e)
        {
            OleDbCommand command = new OleDbCommand("SELECT name, description, rashod FROM pol WHERE id = " + ((PictureBox)sender).Name + "", conn);
            OleDbDataReader reader = command.ExecuteReader();
            while (reader.Read() != false)
            {
                label_u_name.Text = reader["name"].ToString();
                richTextBox_u_description.Text = reader["description"].ToString();
                //textBox_r_rashod.Text = reader["rashod"].ToString();
            }
            reader.Close();
            OleDbCommand cmd = new OleDbCommand("SELECT pol.photo FROM pol WHERE id = " + ((PictureBox)sender).Name + "", conn);
            OleDbDataReader read = cmd.ExecuteReader();
            while (read.Read())
            {
                MemoryStream ms = (MemoryStream)read.GetStream(0);
                bmp = new Bitmap(ms);
                if (bmp != null)
                {
                    pictureBox_u_photo.Image = bmp;
                }
            }
            tabControl1.SelectTab(tabPage7);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            tabControl1.SelectTab(tabPage6);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.startPage = null;
            Properties.Settings.Default.Save();
            Application.Restart();
        }

        private void button_u_color_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.startPage = "Цвет";
            Properties.Settings.Default.Save();
            Application.Restart();
        }

        private void button_u_material_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.Color_ceiling = ceiling;
            Properties.Settings.Default.Color_wall = wall;
            Properties.Settings.Default.Color_floor = floor;
            Properties.Settings.Default.startPage = "Материал";
            Properties.Settings.Default.Save();
            Application.Restart();
        }
    }
}

