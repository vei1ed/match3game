using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace _3inrowKurs
{
	internal class glogic
	{
        Elem[,] gamefield = new Elem[w, w];
        int X = -1;
        int Y = -1;


        const int w = 12;
        const int nulltipe = -99;
        const int blocktype = -88;

        //const int moves = 5;
        //public int movesleft = moves;

        public bool endgame { get; set; }

        public int movesleft { get; set; }

        int gamefield1 = -1;
        int gamefield2 = -1;
        bool zamena;
        public int score { get; set; }
        public int finalscore { get; set; }

        public void GameSetScore(int score)
        {
            this.score = score;
        }

        public void GameSetDif(int movesleft)
        {
            this.movesleft = movesleft;
        }

        List<Elem> SovpadEl = new List<Elem>();

        Random rng = new Random();

        public EventHandler Falled;

        private void FallCellsss()
        {
            TriVRyad();
            if (Hasnullpic())
            {
                while (Hasnullpic())
                {
                    FallCells();
                    Falled(this, null);
                    Thread.Sleep(50);
                }
                StartFall();
            }
            else
            {
                if (movesleft == 0)
                {
                    MessageBox.Show("Игра завершена \n вы набрали: " + (score - 3600) + " очков");

                    score = 0;

                    for (int x = 0; x < w; x++)
                    {
                        for (int y = 0; y < w; y++)
                        {
                            gamefield[x, y].typeofpic = blocktype;
                        }
                    }
                    //movesleft = moves;
                }
            }
        }

        public void StartFall()
        {
            Thread newThread = new Thread(new ThreadStart(FallCellsss));
            newThread.Start();
        }

        public glogic(Elem[,] gamefield)
        {
            this.gamefield = gamefield;
        }

        public int getScore()
        {
            return score;
        }

        public int getMovesLeft()
        {
            return movesleft;
        }

        public void moveCell(int i, int j)
        {

            SovpadEl.Clear();

            if ((X == -1) && (Y == -1))
            {
                X = i;
                Y = j;
                gamefield1 = gamefield[i, j].typeofpic;
            }
            else
            {
                if (((X == i) && (Math.Abs(Y - j) == 1)) || ((Y == j) && (Math.Abs(X - i) == 1)))
                {
                    gamefield2 = gamefield[i, j].typeofpic;

                    gamefield[X, Y].typeofpic = gamefield2;
                    gamefield[i, j].typeofpic = gamefield1;

                    List<Elem> row = TriVRyad();
                    if (row.Count() != 0)
                    {
                        TriVRyad();

                    }
                    movesleft--;
                    zamena = true;
                }
                else
                {
                    zamena = false;
                }

                if (zamena == true)
                {

                    X = -1;
                    Y = -1;

                    gamefield1 = -1;
                    gamefield2 = -1;

                    StartFall();
                }
            }
        }

        public List<Elem> TriVRyad()
        {
            SovpadEl.Clear();

            int count;

            Elem el = gamefield[0, 0];
            Elem el1 = gamefield[0, 0];

            for (int i = 0; i < w; i++)
            {
                count = 0;
                for (int j = 0; j < w; j++)
                {
                    if (count == 0)
                    {
                        el = gamefield[i, j];
                        count = 1;
                    }
                    else if (count == 1)
                    {
                        if (gamefield[i, j].typeofpic == el.typeofpic)
                        {
                            el1 = gamefield[i, j];
                            count++;
                        }
                        else
                        {
                            el = gamefield[i, j];
                            count = 1;
                        }
                    }
                    else if (count > 1)
                    {
                        if (gamefield[i, j].typeofpic == el1.typeofpic)
                        {
                            SovpadEl.Add(el);
                            SovpadEl.Add(el1);
                            SovpadEl.Add(gamefield[i, j]);
                        }
                        else
                        {
                            el = gamefield[i, j];
                            count = 1;
                        }

                    }
                }
            }
            for (int j = 0; j < w; j++)
            {
                count = 0;
                for (int i = 0; i < w; i++)
                {
                    if (count == 0)
                    {
                        el = gamefield[i, j];
                        count = 1;
                    }
                    else if (count == 1)
                    {
                        if (gamefield[i, j].typeofpic == el.typeofpic)
                        {
                            el1 = gamefield[i, j];
                            count++;
                        }
                        else
                        {
                            el = gamefield[i, j];
                            count = 1;
                        }
                    }

                    else if (count > 1)
                    {
                        if (gamefield[i, j].typeofpic == el1.typeofpic)
                        {
                            SovpadEl.Add(el);
                            SovpadEl.Add(el1);
                            SovpadEl.Add(gamefield[i, j]);
                        }
                        else
                        {
                            el = gamefield[i, j];
                            count = 1;
                        }

                    }
                }
            }
            foreach (Elem elem in SovpadEl)
            {
                elem.typeofpic = nulltipe;
            }

            score += SovpadEl.Count() * 5;

            return SovpadEl;
        }

        public bool Hasnullpic()
        {
            for (int j = 0; j < w; j++)
            {
                for (int i = 0; i < w; i++)
                {
                    if (gamefield[i, j].typeofpic == nulltipe) return true;
                }
            }
            return false;
        }

        public void FallCells()
        {
            int typeel = -1;

            for (int j = w - 1; j >= 0; j--)
            {
                for (int i = w - 2; i >= 0; i--)
                {
                    if (gamefield[i + 1, j].typeofpic == nulltipe)
                    {
                        typeel = gamefield[i, j].typeofpic;

                        gamefield[i + 1, j].typeofpic = typeel;
                        gamefield[i, j].typeofpic = nulltipe;

                    }
                }
            }
            for (int j = 0; j < w; j++)
                for (int i = 0; i < w; i++)
                {
                    if (gamefield[i, j].typeofpic == nulltipe)
                    {
                        gamefield[i, j].typeofpic = rng.Next(0, 4);
                    }
                    break;
                }
        }
    }
}
