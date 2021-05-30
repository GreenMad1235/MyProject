using BookingPlace.Context;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace BookingPlace
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            Draw_Button();


        }

        /*________________________________________________________DRAW_________________________________________*/
        private void Draw_Button()
        {
            const int x = 15, y = 25;
            Room r = new Room();
            Button[,] buttons = new Button[x, y];
            for (int i = 0; i < x; i++)
            {
                for (int j = 0; j < y; j++)
                {
                    buttons[i, j] = new Button()
                    {
                           
                        Size = new Size(15, 15),
                        Location = new Point(18 + j * 14, 5 + i * 14),
                        BackColor = Color.Green,
                        Tag = new Point(i, j)
                    };
                    buttons[i,j].Click += new EventHandler(buttons_click);
                    this.Controls.Add(buttons[i, j]);
        
                }

            }
        }
        
        /*_____________________________________________________OUTPUT/INPUT________________________________________________*/
        private void SetInDb(Button b)
        {
            using(var db = new RoomContext())
            {
                Room r = new Room()
                {
                    Number = b.TabIndex.ToString(),
                    Name = textBox1.Text,
                    Surname = textBox2.Text,
                    Status = false,
                    Date = dateTimePicker1.Value,
                    LastTime = dateTimePicker2.Value

                };

                

                if(r.Name.Length == 0 && r.Surname.Length == 0)
                {
                    MessageBox.Show("Enter name or surname");
                }
                else
                {
                    b.BackColor = Color.Red;
                    db.Rooms.Add(r);
                    db.SaveChanges();
                    richTextBox1.Text += "Номер: " + r.Number + " Имя "+ r.Name + " Фамилия " + r.Surname + " Статус комнаты: " + r.Status + " Дата бронирования: " + r.Date
                        + "Дата окончания " + r.LastTime;
                }
                
            }

        }
        /*____________________________________________________________________________________________________________*/
        private void Form1_Load(object sender, EventArgs e)
        {
            

        }

        private void buttons_click(object sender, EventArgs e) 
        {
            Button b = sender as Button;
            SetInDb(b);
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {
            
        }
    }
}
