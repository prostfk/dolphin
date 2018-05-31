using System;
using System.Drawing;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

namespace Circles
{
    //аннотация, которая помечает объект сериализуемым(дает возможность записывать его в файл, базу данных и т.д.)
    [Serializable]
    public class Circle
    {
        private int x;//параметр по x 
        private int y;//параметр по y
        private int diameter;//диамтер окружности 
        private int windowWidth;//ширина окна
        private int windowHeight;//длина окна 
        private Color color;

        //конструктор класса
        public Circle(int windowWidth, int windowHeight, int diameter = 50)
        {
            this.windowWidth = windowWidth;
            this.windowHeight = windowHeight;
            this.diameter = diameter;
            this.color = Color.Black;
        }

        public Color PenColor
        {
            set
            {
                color = value;
            }
            get { return color; }
        }

        public int Diameter
        {
            set
            {
                diameter = value;
            }
            get { return diameter; }
        }

        //getter, setter для x
        public int X
        {
            set
            {
                x = value + windowWidth / 2;
            }

            get { return x; }
        }
        //getter,setter для y
        public int Y
        {
            set
            {
                y = value + windowHeight / 2;
            }

            get { return y; }
        }

        //метод отрисовки окружности 
        public void Draw(Graphics gr, Color color)
        {
            this.color = color;
            gr.DrawEllipse(new Pen(color, 5f),
                x - diameter / 2,
                y - diameter / 2,
                this.diameter,
                this.diameter);
        }

        //метод сравнения объектов 
        public override bool Equals(object obj) => this.GetHashCode() == obj.GetHashCode() && this.GetType() == obj.GetType();

        //метод нахождения хеша объекта 
        public override int GetHashCode()
        {
            const int hashconst = 31;
            int result = 1;
            result = result * hashconst + x;
            result = result * hashconst + y;
            result = result * hashconst + windowHeight;
            result = result * hashconst + windowWidth;
            return result;
        }

        //Сериализация массива объектов в файл
        public void SerializeObjects(string path, Circle[] circles)
        {
            BinaryFormatter serializer = new BinaryFormatter();

            using (FileStream fs = new FileStream(path + ".ini", FileMode.OpenOrCreate))
            {
                serializer.Serialize(fs, circles);
            }
        }

        //Метод копирование координат 
        public void CloneParams(Circle newCircle)
        {
            this.x = newCircle.X;
            this.y = newCircle.Y;
        }

        //Десериализация из файла в массива в объект 
        public Circle[] DeserializeObjects(string path)
        {
            BinaryFormatter serializer = new BinaryFormatter();

            using (FileStream fs = File.OpenRead(path))
            {
                return (Circle[])serializer.Deserialize(fs);
            }
        }

        //Генерация строки для записи в excel 
        public string[] StringForExcel()
        {
            return ToString().Split(',');
        }

        //строковое представление объекта(word)
        public override string ToString() => String.Format("X: {0}, Y: {1}, Диаметр: {2}, Цвет кисти: {3}", this.X, this.Y, this.diameter, String.Format("R: {0}, G: {1}, B: {2}", color.R, color.G, color.B));

    }
}
