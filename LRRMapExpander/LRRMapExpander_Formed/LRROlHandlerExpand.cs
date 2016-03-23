using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.IO;

namespace LRRMapExpander_Formed
{
    class LRROlHandlerExpand
    {
        TextBox textBox_output;
        StreamReader reader;
        StreamWriter writer;
        int x_expand;           //Thickness of vertical border. Distance to expand x-dimension.
        int y_expand;           //Thickness of horizontal border. Distance to expand y-dimension.

        //Default Constructor
        //Initializes a new instance of LRROlHandlerExpand
        public LRROlHandlerExpand(TextBox _textBox_output)
        {
            Directory.CreateDirectory("Output//");
            textBox_output = _textBox_output;
        }

        //Sets the size by which to increase values.
        public void SetBorder(int _x_expand, int _y_expand)
        {
            x_expand = _x_expand;
            y_expand = _y_expand;
        }

        //Sets the I/O locations.
        public bool SetIO(string _inName, string _outName)
        {
            if (reader != null)
                reader.Close();

            if (writer != null)
                writer.Close();

            try
            {
                reader = new StreamReader(File.Open(_inName, FileMode.Open));
                writer = new StreamWriter(File.Open("Output//" + _outName, FileMode.Create));
            }
            catch (Exception ex)
            {
                textBox_output.AppendText(ex.Message + Environment.NewLine);
                return false;
            }
            return true;
        }

        //Expand the size of the OL file
        public void ExpandContent()
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
                            textBox_output.AppendText("ERROR. Failed to parse coordinate for an xPos!" + Environment.NewLine);
                    }
                    //If yPos line.
                    if (String.Equals(olsplit[i], "yPos"))
                    {
                        yExpandOccured = true;
                        if (Double.TryParse(olsplit[i + 1], out newPos))
                            newPos = newPos + y_expand;
                        else
                            textBox_output.AppendText("ERROR. Failed to parse coordinate for a yPos!" + Environment.NewLine);
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
