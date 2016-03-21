using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.IO;

namespace LRRMapExpander
{
    class Program
    {
        public static void Main(string[] args)
        {
            int x_expand = 8;   //Width to expand.
            int y_expand = 4;   //Height to expand.

            LRRMapHandlerExpand mapHandler = new LRRMapHandlerExpand(); //Expander for map files
            LRROlHandlerExpand olHandler = new LRROlHandlerExpand();    //Expander for ol file(s)
            mapHandler.SetBorder(x_expand, y_expand);                   //Set the border for the map handler.
            olHandler.SetBorder(x_expand, y_expand);                    //Set the border for the ol handler.

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

            Console.ReadKey();
        }

        //Pass a file to the output without any modifications.
        private static void Pass(string _inName, string _outName)
        {
            try
            {
                BinaryReader reader = new BinaryReader(File.Open(_inName, FileMode.Open));
                BinaryWriter writer = new BinaryWriter(File.Open("Output//" + _outName, FileMode.Create));

                while(reader.PeekChar() >= 0)
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
