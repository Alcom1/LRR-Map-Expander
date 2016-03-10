using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.IO;

namespace LRRMapExpander
{
    class LRROlHandlerExpand
    {
        StreamReader reader;
        StreamWriter writer;
        //int x_expand;                       //Thickness of vertical border. Distance to expand x-dimension.
        //int y_expand;                       //Thickness of horizontal border. Distance to expand y-dimension.

        public LRROlHandlerExpand(string _inPlace, string _outPlace, string _inName)
        {
            Directory.CreateDirectory(_outPlace);
            reader = new StreamReader(File.Open(_inPlace + _inName, FileMode.Open));
            writer = new StreamWriter(File.Open(_outPlace + _inName, FileMode.Create));
        }

        public void Action(int x_expand, int y_expand)
        {
            String inquiry;                         //Line of Ol file being read.
            Regex whiteSpace = new Regex("[ \t]");  //WhiteSpace regex for 
            bool xExpandOccured = false;            //If xPos was expanded.
            bool yExpandOccured = false;            //If yPos was expanded.
            double newPos = 0;                      //Modified xPos or yPos

            while (reader.Peek() >= 0)
            {
                inquiry = reader.ReadLine();
                string[] olsplit = whiteSpace.Split(inquiry);
                for (int i = 0; i < olsplit.Length; ++i)
                {
                    //If xPos line.
                    if (String.Equals(olsplit[i], "xPos"))
                    {
                        xExpandOccured = true;
                        if (Double.TryParse(olsplit[i + 1], out newPos))
                            newPos = newPos + x_expand;
                        else
                            Console.WriteLine("ERROR. Failed to parse coordinate for an xPos!");
                    }
                    //If yPos line.
                    if (String.Equals(olsplit[i], "yPos"))
                    {
                        yExpandOccured = true;
                        if (Double.TryParse(olsplit[i + 1], out newPos))
                            newPos = newPos + y_expand;
                        else
                            Console.WriteLine("ERROR. Failed to parse coordinate for a yPos!");
                    }
                }
                //Write xPos line.
                if (xExpandOccured)
                {
                    writer.WriteLine("\t\txPos\t" + "{0:0.000000}", newPos);
                    xExpandOccured = false;
                }
                //Write yPos line.
                else if (yExpandOccured)
                {
                    writer.WriteLine("\t\tyPos\t" + "{0:0.000000}", newPos);
                    yExpandOccured = false;
                }
                //Write other lines.
                else
                    writer.WriteLine(inquiry);
            }
            reader.Close();
            writer.Close();
        }
    }
}
