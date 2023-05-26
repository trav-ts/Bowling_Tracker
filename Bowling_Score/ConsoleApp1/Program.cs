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

        }
    }

    public class Frame
    {
        private int total;
        private int firstBall;
        private int secondBall;
        private int frameNumber;
        private bool IsStrike;
        private bool IsSpare;
        private List<Frame> Dependants = new List<Frame>();
        private List<Frame> Dependees = new List<Frame>();
        private Frame next;

        public int Total {
            get { return total; }
            private set { total = value; }
        }

        public int FirstBall
        {
            get { return firstBall; }
            set
            {
                if (value == 10)
                {
                    firstBall = value;
                    IsStrike = true;
                    Dependants.Add(next);
                    Total = 10;

                    List<Frame> completedDEP = new List<Frame>();
                    foreach(Frame f in Dependees)
                    {
                        f.AddToTotal(10);
                        completedDEP.Add(f);
                    }

                    foreach(Frame f in completedDEP)
                    {
                        Dependees.Remove(f);
                    }
                }
                else
                {
                    Total = value;
                    List<Frame> completedDEP = new List<Frame>();
                    foreach (Frame f in Dependees)
                    {
                        if (f.IsSpare)
                        {
                            f.AddToTotal(value);
                            completedDEP.Add(f);
                        }
                        else 
                            f.AddToTotal(10);
                    }
                }
            }
        }

        public Frame(int number, Frame nextFrame)
        {
            next = nextFrame;
            frameNumber = number;
        }

        public void AddToTotal(int val)
        {
            if (val < 0 || val > 10)
                throw new ArgumentException();
            total += val;
        }
    }
}
