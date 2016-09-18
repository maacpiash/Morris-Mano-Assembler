using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {
            string sourceAddress = @"C:\SourceProgram.txt";
            string astAddress = @"C:\AddressSymbolTable.txt";
            firstPass(sourceAddress, astAddress);
        }

        static string chartohex(char x)
        {
            int dec = Convert.ToInt32(x);
            string hex = String.Format("{0:X}", dec);
            return hex;
        }

        static char hextochar(string hex)
        {
            int dec = Convert.ToInt32(hex);
            char ch = (char)dec;
            return ch;
        }

        static int getLC(string line)
        {
            int lc = 0;
            foreach (char c in line)
            {
                if ((int)c >= 0x30 && (int)c <= 0x39)
                    lc = lc * 0x10 + ((int)c - 0x30);
            }
            return lc;
        }

        static string hextobin(string bin)
        {
            string hex = "";
            foreach(char c in bin)
            {
                if (c == '0')
                    hex += "0000";
                else if (c == '1')
                    hex += "0001";
                else if (c == '2')
                    hex += "0010";
                else if (c == '3')
                    hex += "0011";
                else if (c == '4')
                    hex += "0100";
                else if (c == '5')
                    hex += "0101";
                else if (c == '6')
                    hex += "0110";
                else if (c == '7')
                    hex += "0111";
                else if (c == '8')
                    hex += "1000";
                else if (c == '9')
                    hex += "1001";
                else if (c == 'A')
                    hex += "1010";
                else if (c == 'B')
                    hex += "1011";
                else if (c == 'C')
                    hex += "1100";
                else if (c == 'D')
                    hex += "1101";
                else if (c == 'E')
                    hex += "1110";
                else if (c == 'F')
                    hex += "1111";
                else
                    hex += "EROR";
            }
            return hex;
        }

        static string[] initializeAST(string address)
        {
            string[] lines = System.IO.File.ReadAllLines(address);
            int labels = (lines.Length - 1) / 3;
            string[] temp = new string[labels];
            string hex = "";
            int ctr = 0;
            foreach(string t in temp)
            {
                
            }
            string[] ret = new string[labels];
            char[] line = new char[6];
            for(int i = 0; i < labels; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    
                }
            }
            

        }
        
        static void firstPass(string sourceAddress, string astAddress)
        {
            string[] lines = System.IO.File.ReadAllLines(sourceAddress);
            int lc = 0x0;
            string locctr = "";
            using (System.IO.StreamWriter astFile = new System.IO.StreamWriter(@astAddress))
            {
                foreach (string line in lines)
                {
                    if (line.Contains(","))
                    {
                        astFile.WriteLine(chartohex(line[0]) + " " + chartohex(line[1]));
                        astFile.WriteLine(chartohex(line[2]) + " " + chartohex(line[3]));
                        locctr = String.Format("{0:X}", lc);
                        if (lc <= 0xFFF)
                            astFile.WriteLine("0" + locctr[0] + " " + locctr[1] + locctr[2]);
                        else
                            astFile.WriteLine(locctr[0] + locctr[1] + " " + locctr[2] + locctr[3]);
                    }
                    else if (line.Contains("ORG"))
                        lc = getLC(line) - 1;
                    else if (line.Contains("END"))
                        break;
                    lc++;
                }
            }
        }

        static void secondPass(string sourceAddress, string astAddress, string objectAddress)
        {
            string[] lines = System.IO.File.ReadAllLines(sourceAddress);
            int lc = 0x0;
            string locctr = "";
            string whattowrrite = "IOPCXXXXXXXXXXXX";
            using (System.IO.StreamWriter objFile = new System.IO.StreamWriter(@objectAddress))
            {
                foreach (string line in lines)
                {
                    if (!line.Contains("ORG") && !line.Contains("END") && !line.Contains("DEC") && !line.Contains("HEX"))
                    {
                        if (line == "CLA")
                            objFile.WriteLine(hextobin("7800"));
                        else if (line == "CLE")
                            objFile.WriteLine(hextobin("7400"));
                        else if (line == "CMA")
                            objFile.WriteLine(hextobin("7200"));
                        else if (line == "CME")
                            objFile.WriteLine(hextobin("7100"));
                        else if (line == "CIR")
                            objFile.WriteLine(hextobin("7080"));
                        else if (line == "CIL")
                            objFile.WriteLine(hextobin("7040"));
                        else if (line == "INC")
                            objFile.WriteLine(hextobin("7020"));
                        else if (line == "SPA")
                            objFile.WriteLine(hextobin("7010"));
                        else if (line == "SNA")
                            objFile.WriteLine(hextobin("7008"));
                        else if (line == "SZA")
                            objFile.WriteLine(hextobin("7004"));
                        else if (line == "SZE")
                            objFile.WriteLine(hextobin("7002"));
                        else if (line == "HLT")
                            objFile.WriteLine(hextobin("7001"));
                        else
                        {
                            if (line.Contains("AND"))
                                whattowrrite.Replace("OPC", "000");
                            else if (line.Contains("ADD"))
                                whattowrrite.Replace("OPC", "010");
                            else if (line.Contains("LDA"))
                                whattowrrite.Replace("OPC", "010");
                            else if (line.Contains("STA"))
                                whattowrrite.Replace("OPC", "011");
                            else if (line.Contains("BUN"))
                                whattowrrite.Replace("OPC", "100");
                            else if (line.Contains("BSA"))
                                whattowrrite.Replace("OPC", "101");
                            else if (line.Contains("ISZ"))
                                whattowrrite.Replace("OPC", "110");
                            

                        }
                    }
                }
            }
                
            
        }
    }
}
