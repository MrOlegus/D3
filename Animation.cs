using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D3
{
    using Img3D = List<Triangle>; // картинка одного состояния объекта
    using Img3DSet = List<List<Triangle>>; // набор картинок

    public class Animation
    {
        public Img3DSet images = new Img3DSet(); // картинки анимации
        public int period; // период анимации в миллисекундах

        public Img3D CurrentImg3D
        {
            get
            {
                var time = DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond;
                long moment = time % period; // текущее время анимации
                int imgTime = period / images.Count; // время отображения одной картинки
                return images[(int)moment / imgTime];
            }
        }
    }
}
