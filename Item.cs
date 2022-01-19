using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Globalization;
using System.Drawing;

namespace D3
{
    using Img3D = List<Triangle>; // картинка одного состояния объекта   
    using AnimationSet = List<Animation>; // набор анимаций для каждого треугольника

    public class Item
    {
        public string name; // имя прдмета
        public int state = 0; // номер состояния объекта
        public AnimationSet animationSet; // набор анимация для каждого состояния
        
        public Animation CurrentAnimation { get { return animationSet[state]; } } // анимация текущего состояния
        public Img3D CurrentImg3D { get { return CurrentAnimation.CurrentImg3D; } } // текущая картинка анимации

        public Item(string fileName)
        {
            SetAnimationSet(fileName);
        }

        private void SetAnimationSet(string setFile)
        {
            animationSet = new AnimationSet();

            StreamReader sr = new StreamReader(setFile, System.Text.Encoding.Default);

            this.name = sr.ReadLine(); // первая строка в файле - имя предмета

            int state = 0;
            string aniFile;
            while ((aniFile = sr.ReadLine()) != null) // чтение файлов анимаций
            {
                if (aniFile == "") break;
                SetAnimation(aniFile, state);
                state++;
            }

            sr.Close();
        }

        private void SetAnimation(string aniFile, int state)
        {
            animationSet.Add(new Animation());

            StreamReader sr = new StreamReader(aniFile, System.Text.Encoding.Default);

            int period = Convert.ToInt32(sr.ReadLine());
            animationSet[state].period = period;

            int num = 0; // номер картинки в анимации
            string imgFile;
            while ((imgFile = sr.ReadLine()) != null) // чтение файлов картинок для анимаций
            {
                if (imgFile == "") break;

                SetImage(imgFile, state, num);
                num++;
            }

            sr.Close();
        }

        //           имя файла с картинкой . номер состояния . номер картинки в анимации
        private void SetImage(string imgFile, int state, int num)
        {
            animationSet[state].images.Add(new Img3D());

            StreamReader sr = new StreamReader(imgFile, System.Text.Encoding.Default);

            string line;
            while ((line = sr.ReadLine()) != null) // чтение треугольников
            {
                if (line == "") break;

                string[] elements = line.Split(' '); // чисда в строковом виде

                float[] numbers = new float[10]; 
                for (int i = 0; i < 10; i++) // преобразование строк во флоты
                    numbers[i] = float.Parse(elements[i], CultureInfo.InvariantCulture.NumberFormat);

                var point1 = new Point3D(numbers[0], numbers[1], numbers[2]);
                var point2 = new Point3D(numbers[3], numbers[4], numbers[5]);
                var point3 = new Point3D(numbers[6], numbers[7], numbers[8]);
                var color = Color.FromArgb((int)numbers[9]);
                var triangle = new Triangle(point1, point2, point3, color);

                animationSet[state].images[num].Add(triangle);
            }

            sr.Close();
        }
    }
}
