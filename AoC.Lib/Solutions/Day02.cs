using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AoC.Lib.Solutions
{
    public class Day02 : ISolution
    {

        public object Part1(string filename)
        {
            int score = 0;
            foreach (String line in Helpers.ReadFile(filename))
            {
                string[] parts = line.Split(' ');
                                
                switch(parts[1])
                {
                    case "X": // rock 
                        score += 1;

                        if(parts[0] == "A") // r
                        {
                            score += 3;
                        }
                        else if (parts[0] == "B")
                        {
                            score += 0;
                        }
                        else if (parts[0] == "C")
                        {
                            score += 6;
                        }
                        break;
                    case "Y": // p
                        score += 2;

                        if (parts[0] == "A")
                        {
                            score += 6;
                        }
                        else if (parts[0] == "B") // p
                        {
                            score += 3;
                        }
                        else if (parts[0] == "C")
                        {
                            score += 0;
                        }
                        break;
                    case "Z": // s
                        score += 3;

                        if (parts[0] == "A")
                        {
                            score += 0;
                        }
                        else if (parts[0] == "B")
                        {
                            score += 6;
                        }
                        else if (parts[0] == "C")
                        {
                            score += 3;
                        }
                        break;

                }
            }

            return score;
        }

        public object Part2(string filename)
        {
            int score = 0;
            int rock = 1;
            int paper = 2;
            int scissors = 3;

            foreach (String line in Helpers.ReadFile(filename))
            {
                string[] parts = line.Split(' ');

                switch (parts[0])
                {
                    case "A": // rock
                        switch (parts[1])
                        {
                            case "X": // l
                                score += scissors + 0;
                                break;
                            case "Y": // t
                                score += rock + 3;
                                break;
                            case "Z": // w
                                score += paper + 6;
                                break;
                        }
                      
                        break;
                    case "B": // p
                        switch (parts[1])
                        {
                            case "X": // l
                                score += rock + 0;
                                break;
                            case "Y": // t
                                score += paper + 3;
                                break;
                            case "Z": // w
                                score += scissors + 6;
                                break;
                        }
                        break;
                    case "C": // s
                        switch (parts[1])
                        {
                            case "X": // l
                                score += paper + 0;
                                break;
                            case "Y": // t
                                score += scissors + 3;
                                break;
                            case "Z": // w
                                score += rock + 6;
                                break;
                        }
                        break;

                }
            }


            return score;
        }
    }
}
