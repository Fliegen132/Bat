﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
namespace Bat
{
    internal class owl
    {
        //коордионаты препятствия
        public float x, y;
        //спрайт препятствия
        public Image owlImage;
        //размеры персонажа
        public float scaleX, ScaleY;
        public float speed;
        public owl(float x, float y)
        {
            this.x = x;
            this.y = y;
            owlImage = new Bitmap("E:\\c#\\Bat\\Bat\\Image\\stog.png"); //указываем путь картинки
            scaleX = 60;//инициализируем размеры
            ScaleY = 300;
            speed = 3f; //инициализируем скорость совы
        }


    }
}
