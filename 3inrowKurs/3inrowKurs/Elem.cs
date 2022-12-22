using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace _3inrowKurs
{
	internal class Elem
	{
        public BitmapImage pic { get; set; }
        public int typeofpic { get; set; }

        public Button b = new Button();

        int bSize = 45;

        Random rng = new Random();

        public Elem(int typeofpic, int tag)
        {
            this.typeofpic = typeofpic;


            b = new Button();
            b.Tag = tag;
            b.Width = bSize;
            b.Height = bSize;
            b.Content = "";
            b.Margin = new Thickness(2);
        }
    }
}
