/// Date: 11/4/2022
/// Author: Travis Slade
/// Notes:
///         This class is and was only used as a means to learn selection algorithms.
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scoring
{
    public class Score
    {
        static void Main(string[] args)
        {
            Frame[] game = new Frame[11];

            game[1] = new Frame(1, null, null);
            game[2] = new Frame(2, game[1], null);

            for (int i = 3; i <= 10; i++)
            {
                game[i] = new Frame(i, game[i - 1], game[i - 2]);
            }

            for (int i = 1; i < 10; i++)
            {
                if(i % 2 == 0)
                    game[i].FirstBall = 10;
                else
                {
                    game[i].FirstBall = 9;
                    game[i].SecondBall = 1;
                }

            }
            game[10].FirstBall = 10;
            game[10].SecondBall = 9;
            game[10].ThirdBall = 1;


            Console.WriteLine(game[1].FrameTotal);
            Console.WriteLine(game[2].GameTotal);
            Console.Read();
        }
    }
}
