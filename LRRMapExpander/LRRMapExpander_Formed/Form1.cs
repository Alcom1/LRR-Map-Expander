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
                    textBox_output.AppendText(Path.GetFileName(args[i]) + " found" + Environment.NewLine);
            }
            textBox_output.AppendText(Environment.NewLine);
            textBox_output.AppendText("Enter values and press the 'Expand' button to proceed." + Environment.NewLine);
            textBox_output.AppendText(Environment.NewLine);
        }

        //Perform map expansion exporting.
        private void Expand(object sender, EventArgs e)
        {
            textBox_output.AppendText("Performing Expansion." + Environment.NewLine);
            textBox_output.AppendText(Environment.NewLine);

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
                        switch (Path.GetFileName(args[i]))
                        {
                            case "high.map":
                                mapHandler.SetIOB(args[i], Path.GetFileName(args[i]), 8); //Special default block fo height maps.
                                break;
                            case "surf.map":
                                mapHandler.SetIOB(args[i], Path.GetFileName(args[i]), 1); //Special default block fo surface maps.
                                break;
                            default:
                                mapHandler.SetIOB(args[i], Path.GetFileName(args[i]), 0); //Default default.
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
                        olHandler.SetIO(args[i], Path.GetFileName(args[i]));
                        olHandler.ExpandContent();
                        break;

                    default:
                        Pass(args[i], Path.GetFileName(args[i]));
                        break;
                }
            }

            textBox_output.AppendText("Expansion complete." + Environment.NewLine);
            textBox_output.AppendText(Environment.NewLine);
        }

        //Set the x_expand to the current x_expand boxes's value.
        private void Change_X_expand(object sender, EventArgs e)
        {
            //Parse and set expansion value.
            try
            {
                int tempX_expand = int.Parse(textBox_x.Text);
                if(tempX_expand < 0 || tempX_expand > 20)
                {
                    textBox_output.AppendText("Invalid value for 'Horizontal Expansion.'" + Environment.NewLine);
                    textBox_output.AppendText("Value must be between 0 and 20." + Environment.NewLine);
                    textBox_output.AppendText(Environment.NewLine);
                }
                else
                {
                    textBox_output.AppendText("Valid value for 'Horizontal Expansion.'" + Environment.NewLine);
                    textBox_output.AppendText(Environment.NewLine);
                    x_expand = tempX_expand;
                }
            }
            catch
            {
                //textBox_output.AppendText("Invalid value for 'Horizontal Expansion.'" + Environment.NewLine);
                //textBox_output.AppendText("Value is not an integer." + Environment.NewLine);
                //textBox_output.AppendText(Environment.NewLine);
                textBox_x.Text = "0";
            }
        }

        //Set the y_expand to the current y_expand boxes's value.
        private void Change_Y_expand(object sender, EventArgs e)
        {
            //Parse and set expansion value.
            try
            {
                int tempY_expand = int.Parse(textBox_y.Text);
                if (tempY_expand < 0 || tempY_expand > 20)
                {
                    textBox_output.AppendText("Invalid value for 'Vertical Expansion.'" + Environment.NewLine);
                    textBox_output.AppendText("Value must be between 0 and 20." + Environment.NewLine);
                    textBox_output.AppendText(Environment.NewLine);
                }
                else
                {
                    textBox_output.AppendText("Valid value for 'Vertical Expansion.'" + Environment.NewLine);
                    textBox_output.AppendText(Environment.NewLine);
                    y_expand = tempY_expand;
                }
            }
            catch
            {
                //textBox_output.AppendText("Invalid value for 'Vertical Expansion.'" + Environment.NewLine);
                //textBox_output.AppendText("Value is not an integer." + Environment.NewLine);
                //textBox_output.AppendText(Environment.NewLine);
                textBox_y.Text = "0";
            }
        }

        //Pass a file to the output without any modifications.
        private static void Pass(string _inName, string _outName)
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
            catch (FileNotFoundException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
