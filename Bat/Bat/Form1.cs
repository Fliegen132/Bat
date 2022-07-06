using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Media;

namespace Bat
{
    public partial class Form1 : Form
    {
        public SoundPlayer sound;
        public SoundPlayer sound1;
        PlayerController player; //ссылка на класс персонажа
        owl stog; //ссылка на класс препятствия
        stal stal; //ссылка на класс препятствия
        public bool secImage = false; // булевая переменна изменяющая спрайт персонажа
        public bool isSpace = false; //булевая переменная для кнопки пробел, чтобы мы не могли зажать пробел в игре
        Random rand = new Random(); //рандомная переменная для спавна преград
        public int score = 0;
        
        public Form1()
        {
            sound = new SoundPlayer("E:\\c#\\Bat\\Bat\\Sound\\defeat.wav");              //создание плеера для музыки/звуков
            sound1 = new SoundPlayer("E:\\c#\\Bat\\Bat\\Sound\\Win.wav");              //создание плеера для музыки/звуков
            InitializeComponent();
            Int(); //вызов функции которая ниже ниже
            Invalidate();
            timer1.Interval = 1;
            timer1.Tick += new EventHandler(update);//создаём новую функцию update где будет меняться гравитация каждый кадр из-за таймера
        }
        public void Int()
        {
            
            player = new PlayerController(100, 300); //указываем изначальные коордионаты персонажа
            timer1.Start();
            stog = new owl(600, -30); //коордионаты препятствия
            stal = new stal(600, 400); //коордионаты препятствия
        }

        private void update(object? sender, EventArgs e) //функция update 
        {
            
            player.gravityValue += 0.1f;   //скорость падения персонажа
            player.y += player.gravityValue;

            stog.x += -stog.speed; //изменение положения препятствий по оси X за счет скорости
            stal.x += -stal.speed; //изменение положения препятствий по оси X за счет скорости
            if (player.y < 0)               //если персонаж высоко, то проигрыш 
            {
                player.isAlive = false;                                         
            }
            if (player.y > 580)             //если персонаж низко, то проигрыш 
            {
                player.isAlive = false;
            }
            if (Collide(player, stog) || Collide2(player, stal))              //если персонаж докасается до одного из препятствий, то проигрыш
            {
                player.isAlive = false;
            }
            if (player.isAlive == false)                            //если проиграли
            {                
                timer1.Stop();
                sound.Play();
                label2.Visible = true;
                label2.Text = "Вы проиграли, ваш рекорд " + score;
                label3.Visible = true;
            }

            if(score == 51) {
                
                sound1.Play();

            }

            //если коордионата совы X = 0 то она возвращается на x+600 и на рандом по оси Y 
            if (stog.x <= -50) {
                score++;
                stog.x = 600;
                stal.x = 600;
                stog.y = rand.Next(-130, -30);
                stal.y = rand.Next(300, 400);
            }
            label1.Text = "Очков: " + score;

            Invalidate();
        }

        //функция для отрисовки персонажа и препятствий
        private void OnPaint(object sender, PaintEventArgs e)
        {
            Graphics graph = e.Graphics;
            graph.DrawImage(stog.owlImage, stog.x, stog.y, stog.scaleX, stog.ScaleY);
            graph.DrawImage(stal.stalImage, stal.x, stal.y, stal.scaleX, stal.ScaleY);

            if (secImage == false) //если эта переменная false то первая картинка персонажа
            {
                graph.DrawImage(player.batImage1, player.x, player.y, player.scale, player.scale);
            }
            if (secImage == true)//если эта переменная true то вторая картинка персонажа
            {
                graph.DrawImage(player.batImage2, player.x, player.y, player.scale, player.scale);
            }
        }

        //event который срабатывает при нажатии на Space
        void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            
            if (e.KeyCode == Keys.Space && isSpace == false) //срабатывает в том случае если нажат пробел и если он еще не был нажат, вчитайтесь внимательнее   
                                                             //это нужно чтобы нельзя было зажать пробел
            {
                secImage = true; //здесь меняется булевая переменная для изменения картинки
                player.gravityValue = -4f;
                isSpace = true; //мы уже нажали пробел переменная становится true, следовательно если мы зажмём пробел ничего не будет

                if (player.isAlive == false) {                      //если мы проиграли и нажали пробел то  игра перезапустится
                    player.isAlive = true;
                    score = 0;
                    label2.Visible = false;                 
                    label3.Visible = false;
                    Int();
                }
            }
        }

        //event который срабатывает при отпускании Space
        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Space)
            {
                secImage = false;   //здесь меняется булевая переменная для изменения картинки
                isSpace = false;    //мы отпустили пробел переменная становится false

            }
        }

        //столкновение с верхним препятствием
        private bool Collide(PlayerController player, owl stog)
        {
            PointF delta = new PointF();
            delta.X = (player.x + player.scale / 2) - (stog.x + stog.scaleX / 2) ;
            delta.Y = (player.y + player.scale / 2) - (stog.y + stog.ScaleY / 2);

            if (Math.Abs(delta.X) <= player.scale / 2 + stog.scaleX / 2.5)                  
            {
                if (Math.Abs(delta.Y) <= player.scale / 2 + stog.ScaleY / 2.5)
                {
                    return true;
                }
            }
            return false;

        }
        //столкновение с нижним препятствием
        private bool  Collide2(PlayerController player, stal stal)
        {
            PointF delta = new PointF();
            delta.X = (player.x + player.scale / 2) - (stal.x + stal.scaleX / 2);
            delta.Y = (player.y + player.scale / 2) - (stal.y + stal.ScaleY / 2);

            if (Math.Abs(delta.X) <= player.scale / 2 + stal.scaleX / 2.5)
            {
                if (Math.Abs(delta.Y) <= player.scale / 5 + stal.ScaleY / 2.5)
                {
                    return true;
                }
                
            }
            return false;

        }

        
    }
}
