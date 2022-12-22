using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace _3inrowKurs
{
	public partial class MainWindow : Window
	{
        int bSize = 45;
        int hod = 0;


        BitmapImage[] typedpic = new BitmapImage[]
        {
            new BitmapImage(new Uri(@"pack://application:,,,/img/01.png", UriKind.Absolute)),
            new BitmapImage(new Uri(@"pack://application:,,,/img/02.png", UriKind.Absolute)),
            new BitmapImage(new Uri(@"pack://application:,,,/img/03.png", UriKind.Absolute)),
            new BitmapImage(new Uri(@"pack://application:,,,/img/04.png", UriKind.Absolute)),
        };

        Elem[,] elfield = new Elem[w, w];
        glogic GameLog;
        Random rng = new Random();
        const int w = 12;
        const int nulltipe = -99;
        const int blocktype = -88;
        public MainWindow()
		{
			InitializeComponent();
            unigrid.Rows = w;
            unigrid.Columns = w;

            unigrid.Width = w * (bSize + 4);
            unigrid.Height = w * (bSize + 4);

            unigrid.Margin = new Thickness(5, 5, 5, 5);

            GameLog = new glogic(elfield);

            for (int i = 0; i < w; i++)
                for (int j = 0; j < w; j++)
                {
                    elfield[i, j] = new Elem(nulltipe, i + j * w);
                    StackPanel stackPanel = new StackPanel();
                    stackPanel.Margin = new Thickness(1);
                    elfield[i, j].b.Click += Btn_Click;
                    unigrid.Children.Add(elfield[i, j].b);
                }

        }

        private void Falled(object sender, EventArgs args)
        {
            Application.Current.Dispatcher.Invoke(delegate
            {
                Update();
            });
        }

        void Update()
        {
            for (int i = 0; i < w; i++)
                for (int j = 0; j < w; j++)
                {
                    StackPanel stack = new StackPanel();

                    int typeel = elfield[i, j].typeofpic;
                    if (typeel != nulltipe)
                    {
                        if (typeel == blocktype)
                            elfield[i, j].b.IsEnabled = false;
                        else
                        {
                            BitmapImage image = typedpic[typeel];
                            stack = getPanel(image);
                        }

                    }

                    elfield[i, j].b.Content = stack;
                }

            gamescore.Content = Convert.ToString(GameLog.getScore() - 3600);
            hodi.Content = "Ходы: " + GameLog.movesleft;
        }

        StackPanel getPanel(BitmapImage picture)
        {
            StackPanel stackPanel = new StackPanel();
            Image image = new Image();
            image.Source = picture;
            stackPanel.Children.Add(image);
            stackPanel.Margin = new Thickness(1);

            return stackPanel;
        }

        private void Btn_Click(object sender, RoutedEventArgs e)
        {
            int index = (int)(((Button)sender).Tag);

            int i = index % w;
            int j = index / w;

            GameLog.moveCell(i, j);

            gamescore.Content = Convert.ToString(GameLog.getScore() - 3600);


            Update();
        }

		private void Button_Click_1(object sender, RoutedEventArgs e)
		{
          hod = 5;
		}
		private void Button_Click_2(object sender, RoutedEventArgs e)
		{
            hod = 10;
		}
		private void Button_Click(object sender, RoutedEventArgs e)
		{
            GameLog.GameSetScore(0);
            GameLog.GameSetDif(hod);
            GameLog.Falled += Falled;
            Update();
            GameLog.StartFall();
        }
	}
}
