using System;
using System.IO;

namespace Morris_Mano_Assembler
{
    class Program
    {
        static void Main(string[] args)
        {
            string sourceAddress = @"input.txt";
            string astAddress = @"AddressSymbolTable.txt";
            firstPass(sourceAddress, astAddress);
            string objectAddress = @"ObjectProgram.txt";
            secondPass(sourceAddress, astAddress, objectAddress);
        }

        static string chartohex(char x)
        {
            int dec = Convert.ToInt32(x);
            string hex = String.Format("{0:X}", dec);
            return hex;
        }

        static char hextochar(string hex)
        {
            if (hex == "41")
                return 'A';
            if (hex == "42")
                return 'B';
            if (hex == "43")
                return 'C';
            if (hex == "44")
                return 'D';
            if (hex == "45")
                return 'E';
            if (hex == "46")
                return 'F';
            if (hex == "47")
                return 'G';
            if (hex == "48")
                return 'H';
            if (hex == "49")
                return 'I';
            if (hex == "4A")
                return 'J';
            if (hex == "4B")
                return 'K';
            if (hex == "4C")
                return 'L';
            if (hex == "4D")
                return 'M';
            if (hex == "4E")
                return 'N';
            if (hex == "4F")
                return 'O';
            if (hex == "50")
                return 'P';
            if (hex == "51")
                return 'Q';
            if (hex == "52")
                return 'R';
            if (hex == "53")
                return 'S';
            if (hex == "54")
                return 'T';
            if (hex == "55")
                return 'U';
            if (hex == "56")
                return 'V';
            if (hex == "57")
                return 'W';
            if (hex == "58")
                return 'X';
            if (hex == "59")
                return 'Y';
            if (hex == "5A")
                return 'Z';
            else
                return ',';
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

        static string getBinAdd(string a)
        {
            string x = "";
            for (int i = 0; i < 3; i++)
                x += hextobin(a[i].ToString());
            return x;
        }

        static string[] initializeAST(string address)
        {
            string[] lines = File.ReadAllLines(address);
            int labels = (lines.Length) / 3;
            string[] ret = new string[labels];
            string item = "";
            int j;
            for (int i = 0; i < labels; i++)
            {
                j = 0;
                for (; j < 2; j++)
                {
                    item += hextochar(lines[i * 3 + j].ToCharArray()[0].ToString() + lines[i * 3 + j].ToCharArray()[1].ToString());
                    item += hextochar(lines[i * 3 + j].ToCharArray()[3].ToString() + lines[i * 3 + j].ToCharArray()[4].ToString());
                }
                item += lines[i * 3 + j][0].ToString() + lines[i * 3 + j][1].ToString() + lines[i * 3 + j][3].ToString() + lines[i * 3 + j][4].ToString();

                ret[i] = item;
                item = "";
            }

            return ret;
        }

        static string getMRI(string astAddress, string line)
        {
            string code1 = "";
            string code2 = "";
            if (line.EndsWith(" I"))
                code1 += "1";
            else
                code1 += "0";
            if (line.Contains("AND"))
                code1 += "000";
            else if (line.Contains("ADD"))
                code1 += "001";
            else if (line.Contains("LDA"))
                code1 += "010";
            else if (line.Contains("STA"))
                code1 += "011";
            else if (line.Contains("BUN"))
                code1 += "100";
            else if (line.Contains("BSA"))
                code1 += "101";
            else if (line.Contains("ISZ"))
                code1 += "110";
            string[] labels = initializeAST(astAddress);
            string label = line.Substring(5, 3);
            string l = "";
            string a = "";
            foreach (string check in labels)
            {
                l = check.Substring(0, 3);
                a = check.Substring(5, 3);
                if (l == label)
                {
                    string binAdd = getBinAdd(a);
                    code2 += binAdd;
                    break;
                }
            }
            return code1 + code2;
        }

        static string getBinary(string b, int value, bool isPositive)
        {
            string bin = "";
            string hex = "";
            int digit;
            if (b == "DEC")
            {
                while (value != 0)
                {
                    digit = value % 16;
                    if (value == 10)
                        hex = "A" + hex;
                    else if (value == 11)
                        hex = "B" + hex;
                    else if (value == 12)
                        hex = "C" + hex;
                    else if (value == 13)
                        hex = "D" + hex;
                    else if (value == 14)
                        hex = "E" + hex;
                    else if (value == 15)
                        hex = "F" + hex;
                    else
                        hex = digit.ToString() + hex;
                    value /= 16;
                }
            }
            else
            {
                hex = value.ToString();
            }

            if (isPositive)
                bin = "0000";
            else
                bin = "1000";
            if (hex.Length == 1)
                hex = "00" + hex;
            else if (hex.Length == 2)
                hex = "0" + hex;

            bin = bin + hextobin(hex);
            return bin;
        }

        static void firstPass(string sourceAddress, string astAddress)
        {
            Console.WriteLine("Initiating first pass...");
            string[] lines = null;
            try
            {
                lines = File.ReadAllLines(sourceAddress);
            }
            catch (Exception x)
            {
                Console.WriteLine(x.ToString());
            }
            int lc = 0x0;
            string locctr = "";
            using (StreamWriter astFile = new StreamWriter(@astAddress))
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
                    {
                        lc = getLC(line) - 1;
                        locctr = String.Format("{0:X}", lc);
                    }
                    else if (line.Contains("END"))
                        break;
                    lc++;
                }
            }
            Console.WriteLine("First pass executed successfully.");
        }

        static void secondPass(string sourceAddress, string astAddress, string objectAddress)
        {
            Console.WriteLine("Initiating second pass...");
            string[] lines = File.ReadAllLines(sourceAddress);
            int lc = 0x0;
            using (StreamWriter objFile = new StreamWriter(@objectAddress))
            {   
                foreach (string line in lines)
                {
                    if (line.Contains("END"))
                        break;
                    else if (!line.Contains("ORG") && !line.Contains("END") && !line.Contains("DEC") && !line.Contains("HEX"))
                    {
                        if (line.Contains("CLA"))
                            objFile.WriteLine(String.Format("{0:X}", lc) + "\t" + hextobin("7800"));
                        else if (line.Contains("CLE"))
                            objFile.WriteLine(String.Format("{0:X}", lc) + "\t" + hextobin("7400"));
                        else if (line.Contains("CMA"))
                            objFile.WriteLine(String.Format("{0:X}", lc) + "\t" + hextobin("7200"));
                        else if (line.Contains("CME"))
                            objFile.WriteLine(String.Format("{0:X}", lc) + "\t" + hextobin("7100"));
                        else if (line.Contains("CIR"))
                            objFile.WriteLine(String.Format("{0:X}", lc) + "\t" + hextobin("7080"));
                        else if (line.Contains("CIL"))
                            objFile.WriteLine(String.Format("{0:X}", lc) + "\t" + hextobin("7040"));
                        else if (line.Contains("INC"))
                            objFile.WriteLine(String.Format("{0:X}", lc) + "\t" + hextobin("7020"));
                        else if (line.Contains("SPA"))
                            objFile.WriteLine(String.Format("{0:X}", lc) + "\t" + hextobin("7010"));
                        else if (line.Contains("SNA"))
                            objFile.WriteLine(String.Format("{0:X}", lc) + "\t" + hextobin("7008"));
                        else if (line.Contains("SZA"))
                            objFile.WriteLine(String.Format("{0:X}", lc) + "\t" + hextobin("7004"));
                        else if (line.Contains("SZE"))
                            objFile.WriteLine(String.Format("{0:X}", lc) + "\t" + hextobin("7002"));
                        else if (line.Contains("HLT"))
                            objFile.WriteLine(String.Format("{0:X}", lc) + "\t" + hextobin("7001"));
                        else if (line.Contains("INP"))
                            objFile.WriteLine(String.Format("{0:X}", lc) + "\t" + hextobin("F800"));
                        else if (line.Contains("OUT"))
                            objFile.WriteLine(String.Format("{0:X}", lc) + "\t" + hextobin("F400"));
                        else if (line.Contains("SKI"))
                            objFile.WriteLine(String.Format("{0:X}", lc) + "\t" + hextobin("F200"));
                        else if (line.Contains("SKO"))
                            objFile.WriteLine(String.Format("{0:X}", lc) + "\t" + hextobin("F100"));
                        else if (line.Contains("ION"))
                            objFile.WriteLine(String.Format("{0:X}", lc) + "\t" + hextobin("F080"));
                        else if (line.Contains("IOF"))
                            objFile.WriteLine(String.Format("{0:X}", lc) + "\t" + hextobin("F040"));
                        else
                        {
                            objFile.WriteLine(String.Format("{0:X}", lc) + "\t" + getMRI(astAddress, line));
                        }
                        lc++;
                    }
                    else if (line.Contains("ORG"))
                    {
                        lc = getLC(line);
                    }   
                    else if (line.Contains(","))
                    {
                        string code = line;
                        string val = "";
                        for (int i = 9; i < code.Length; i++)
                            val += code[i];
                        int v;
                        if (val.Contains("-"))
                            v = Convert.ToInt32(val) * (-1);
                        else
                            v = Convert.ToInt32(val);
                        String baze = code.Substring(5, 3);
                        objFile.WriteLine(String.Format("{0:X}", lc) + "\t" + getBinary(baze, v, !code.Contains("-")));
                        lc++;   
                    }
                }
            }
            Console.WriteLine("Second pass executed successfully.");
        }
    }
}