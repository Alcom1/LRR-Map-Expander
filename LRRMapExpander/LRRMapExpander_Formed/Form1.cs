using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace LRRMapExpander_Formed
{
    public partial class Form1 : Form
    {
        int x_expand;
        int y_expand;
        string[] args;
        LRRMapHandlerExpand mapHandler;
        LRROlHandlerExpand olHandler;

        public Form1(string[] _args)
        {
            InitializeComponent();
            mapHandler = new LRRMapHandlerExpand(textBox_output);  //Expander for map files
            olHandler = new LRROlHandlerExpand(textBox_output);    //Expander for ol file(s)
            args = _args;

            textBox_output.AppendText("Finding files." + Environment.NewLine);
            for (int i = 0; i < args.Length; i++)
            {
                if(File.Exists(args[i]))
                    textBox_output.AppendText("Found: " + Path.GetFileName(args[i]) + Environment.NewLine);
            }
            textBox_output.AppendText(Environment.NewLine);
            textBox_output.AppendText("Enter values and press the 'Expand' button to proceed." + Environment.NewLine);
            textBox_output.AppendText(Environment.NewLine);
        }

        //Perform map expansion exporting.
        private void Expand(object sender, EventArgs e)
        {
            try
            {
                x_expand = int.Parse(textBox_x.Text);
                if (x_expand < 0 || x_expand > 20)
                {
                    textBox_output.AppendText("Invalid value for 'Horizontal Expansion.'" + Environment.NewLine);
                    textBox_output.AppendText("Value must be between 0 and 20." + Environment.NewLine);
                    textBox_output.AppendText(Environment.NewLine);
                    return;
                }
            }
            catch
            {
                textBox_output.AppendText("Invalid value for 'Horizontal Expansion.'" + Environment.NewLine);
                textBox_output.AppendText("Value is not an integer." + Environment.NewLine);
                textBox_output.AppendText(Environment.NewLine);
                return;
            }

            try
            {
                y_expand = int.Parse(textBox_y.Text);
                if (y_expand < 0 || y_expand > 20)
                {
                    textBox_output.AppendText("Invalid value for 'Vertical Expansion.'" + Environment.NewLine);
                    textBox_output.AppendText("Value must be between 0 and 20." + Environment.NewLine);
                    textBox_output.AppendText(Environment.NewLine);
                    return;
                }
            }
            catch
            {
                textBox_output.AppendText("Invalid value for 'Vertical Expansion.'" + Environment.NewLine);
                textBox_output.AppendText("Value is not an integer." + Environment.NewLine);
                textBox_output.AppendText(Environment.NewLine);
                return;
            }

            textBox_output.AppendText("Performing Expansion." + Environment.NewLine);

            //Set borders for handlers.
            mapHandler.SetBorder(x_expand, y_expand);
            olHandler.SetBorder(x_expand, y_expand);

            //For each file presented.
            for (int i = 0; i < args.Length; ++i)
            {
                //Different actions for different extensions
                switch (Path.GetExtension(args[i]))
                {
                    case ".map":
                        if(!mapHandler.SetIO(args[i], Path.GetFileName(args[i])))
                        {
                            textBox_output.AppendText("Failed to handle file: " + Path.GetFileName(args[i]) + Environment.NewLine);
                            textBox_output.AppendText(Environment.NewLine);
                            break;
                        }

                        //Special default blocks.
                        switch (Path.GetFileName(args[i]))
                        {
                            case "high.map":
                                mapHandler.SetB(8); //Special default block fo height maps.
                                break;
                            case "surf.map":
                                mapHandler.SetB(1); //Special default block fo surface maps.
                                break;
                            default:
                                mapHandler.SetB(0); //Default default.
                                break;
                        }

                        //Expand
                        mapHandler.GrabHeader();
                        mapHandler.GrabContent();
                        mapHandler.ModContent();

                        //Hight Match
                        if (Path.GetFileName(args[i]) == "surf.map")
                            mapHandler.ModContentHeightMatch();

                        //Output
                        mapHandler.SpitContent();
                        break;

                    case ".ol":
                        if(!olHandler.SetIO(args[i], Path.GetFileName(args[i])))
                        {
                            textBox_output.AppendText("Failed to handle file: " + Path.GetFileName(args[i]) + Environment.NewLine);
                            textBox_output.AppendText(Environment.NewLine);
                            break;
                        }
                        olHandler.ExpandContent();
                        break;

                    default:
                        if(!Pass(args[i], Path.GetFileName(args[i]), textBox_output))
                        {
                            textBox_output.AppendText("Failed to handle file: " + Path.GetFileName(args[i]) + Environment.NewLine);
                            textBox_output.AppendText(Environment.NewLine);
                            break;
                        }
                        break;
                }
            }

            textBox_output.AppendText("Expansion complete." + Environment.NewLine);
            textBox_output.AppendText(Environment.NewLine);
        }

        //Pass a file to the output without any modifications.
        private static bool Pass(string _inName, string _outName, TextBox textBox_output)
        {
            try
            {
                BinaryReader reader = new BinaryReader(File.Open(_inName, FileMode.Open));
                BinaryWriter writer = new BinaryWriter(File.Open("Output//" + _outName, FileMode.Create));

                while (reader.PeekChar() >= 0)
                {
                    char item = reader.ReadChar();
                    writer.Write(item);
                }

                reader.Close();
                writer.Close();
            }
            catch (Exception ex)
            {
                textBox_output.AppendText(ex.Message + Environment.NewLine);
                return false;
            }
            return true;
        }
    }
}
