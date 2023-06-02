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
        private int firstThrow;
        private int secondThrow;
        public int frameNumber;
        public bool IsStrike;
        public bool IsSpare;
        public bool IsOpen;
        public bool IsTenth;
        public int thirdThrow;
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

        public int FirstThrow
        {
            get { return firstThrow; }
            set
            {
                firstThrow = value;
                if (firstThrow == 10) IsStrike = true;

                if (last != null)
                {
                    // Last shot was a spare or strike, add current shot to last frame and game total.
                    if (last.IsStrike || last.IsSpare)
                    {
                        if (secondLast != null && last.IsStrike && secondLast.IsStrike)
                        {
                            secondLast.frameTotal += firstThrow;
                            secondLast.gameTotal += firstThrow;
                            last.frameTotal += firstThrow;
                            last.gameTotal = secondLast.gameTotal + last.frameTotal;
                        }
                        else
                        {
                            last.frameTotal += firstThrow;
                            last.gameTotal += firstThrow;
                        }

                    }

                    this.frameTotal = firstThrow;
                    this.gameTotal = last.gameTotal + firstThrow;
                }
                else // First frame instance.
                {
                    frameTotal = firstThrow;
                    gameTotal = firstThrow;
                }
            }
        }

        public int SecondThrow
        {
            get { return secondThrow; }
            set
            {
                secondThrow = value;

                if(IsTenth)
                {
                    SecondThrowTenth(secondThrow);
                    return;
                }

                if (IsStrike)
                {
                    secondThrow = 0;
                    return;
                }
                else if (last != null && last.IsStrike)
                {
                    last.frameTotal += secondThrow;
                    last.gameTotal += secondThrow;
                }

                frameTotal = secondThrow + firstThrow;
                if (last != null) gameTotal = last.gameTotal + frameTotal;
                else
                    gameTotal = frameTotal;
                if (frameTotal == 10)
                    IsSpare = true;
                else
                    IsOpen = true;
            }
        }

        public void SecondThrowTenth(int value)
        {
            secondThrow = value;

            if (last.IsStrike)
            {
                last.frameTotal += secondThrow;
                last.gameTotal += secondThrow;
                this.frameTotal += secondThrow;
                this.gameTotal = last.gameTotal + frameTotal;
                return;
            }
            else if(firstThrow + secondThrow != 10 && firstThrow != 10) IsOpen = true;
            
            frameTotal = firstThrow + secondThrow;
            gameTotal += secondThrow;
        }

        public int ThirdThrow
        {
            get { return thirdThrow; }
            set
            {
                thirdThrow = value;

                if (IsOpen)
                    return;
                
                frameTotal += thirdThrow;
                gameTotal += thirdThrow;
            }
        }

        public Frame(int number, Frame last, Frame secondLast)
        {
            this.last = last;
            this.secondLast = secondLast;
            frameNumber = number;
            GameTotal = 0;
            frameTotal = 0;
            firstThrow = 0;
            secondThrow = 0;
            IsSpare = false;
            IsStrike = false;
            IsOpen = false;

            if (frameNumber == 10) IsTenth = true;
        }
    }
}
