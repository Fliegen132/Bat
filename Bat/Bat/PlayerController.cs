using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
namespace Bat
{
    internal class PlayerController
    {
        //коордионаты персонажа
        public float x, y; 
        //два спрайта персонажа
        public Image batImage1;
        public Image batImage2;
        //размеры персонажа
        public float scale = 6;
        //гравитация персонажа
        public float gravityValue;

        public bool isAlive;   
        public PlayerController(float x, float y) {
            this.x = x;
            this.y = y;
            batImage1 = new Bitmap("E:\\c#\\Bat\\Bat\\Image\\bat1.png"); //указываем путь картинок персонажа
            batImage2 = new Bitmap("E:\\c#\\Bat\\Bat\\Image\\bat2.png");
            scale = 60;//инициализируем размеры
            gravityValue = 0.1f; //инициализируем гравитацию
            isAlive = true;
        }

        
    }
}
