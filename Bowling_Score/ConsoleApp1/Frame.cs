using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Scoring
{
    public class Frame
    {
        public int frameTotal;
        public int gameTotal;
        private int firstBall;
        private int secondBall;
        public int frameNumber;
        public bool IsStrike;
        public bool IsSpare;
        public bool IsOpen;
        public bool IsTenth;
        public int thirdBall;
        public Frame last;
        public Frame secondLast;

        public int FrameTotal
        {
            get { return frameTotal; }
            private set { frameTotal = value; }
        }

        public int GameTotal
        {
            get { return gameTotal; }
            private set { gameTotal = value; }
        }

        public int FirstBall
        {
            get { return firstBall; }
            set
            {
                firstBall = value;
                if (firstBall == 10) IsStrike = true;

                if (last != null)
                {
                    // Last shot was a spare or strike, add current shot to last frame and game total.
                    if (last.IsStrike || last.IsSpare)
                    {
                        if (secondLast != null && last.IsStrike && secondLast.IsStrike)
                        {
                            secondLast.frameTotal += firstBall;
                            secondLast.gameTotal += firstBall;
                            last.frameTotal += firstBall;
                            last.gameTotal = secondLast.gameTotal + last.frameTotal;
                        }
                        else
                        {
                            last.frameTotal += firstBall;
                            last.gameTotal += firstBall;
                        }

                    }

                    this.frameTotal = firstBall;
                    this.gameTotal = last.gameTotal + firstBall;
                }
                else // First frame instance.
                {
                    frameTotal = firstBall;
                    gameTotal = firstBall;
                }
            }
        }

        public int SecondBall
        {
            get { return secondBall; }
            set
            {
                secondBall = value;

                if(IsTenth)
                {
                    SecondBallTenth(secondBall);
                    return;
                }

                if (IsStrike)
                {
                    secondBall = 0;
                    return;
                }
                else if (last != null && last.IsStrike)
                {
                    last.frameTotal += secondBall;
                    last.gameTotal += secondBall;
                }

                frameTotal = secondBall + firstBall;
                if (last != null) gameTotal = last.gameTotal + frameTotal;
                else
                    gameTotal = frameTotal;
                if (frameTotal == 10)
                    IsSpare = true;
                else
                    IsOpen = true;
            }
        }

        public void SecondBallTenth(int value)
        {
            secondBall = value;

            if (last.IsStrike)
            {
                last.frameTotal += secondBall;
                last.gameTotal += secondBall;
                this.frameTotal += secondBall;
                this.gameTotal = last.gameTotal + frameTotal;
                return;
            }
            else if(firstBall + secondBall != 10 && firstBall != 10) IsOpen = true;
            
            frameTotal = firstBall + secondBall;
            gameTotal += secondBall;
        }

        public int ThirdBall
        {
            get { return thirdBall; }
            set
            {
                thirdBall = value;

                if (IsOpen)
                    return;
                
                frameTotal += thirdBall;
                gameTotal += thirdBall;
            }
        }

        public Frame(int number, Frame last, Frame secondLast)
        {
            this.last = last;
            this.secondLast = secondLast;
            frameNumber = number;
            GameTotal = 0;
            frameTotal = 0;
            firstBall = 0;
            secondBall = 0;
            IsSpare = false;
            IsStrike = false;
            IsOpen = false;

            if (frameNumber == 10) IsTenth = true;
        }
    }
}
