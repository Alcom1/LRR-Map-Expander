using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.IO;

namespace LRRMapExpander
{
    class LRRMapHandlerExpand
    {
        BinaryReader reader;
        BinaryWriter writer;
        int x_expand;                       //Thickness of vertical border. Distance to expand x-dimension.
        int y_expand;                       //Thickness of horizontal border. Distance to expand y-dimension.
        int x_res;                          //Width of old map file.
        int y_res;                          //Height of old map file.
        byte[] header = new byte[16];       //Header of old map file.
        ushort block;                       //Blocks that compose the map border.
        ushort[,] content;                  //Content of old map file.
        ushort[,] newContent;               //Content of new map file.

        //Default Constructor
        //Initializes a new instance of LRRMapHandlerExpand
        public LRRMapHandlerExpand()
        {
            Directory.CreateDirectory("Output//");
        }

        //Sets the size by which to increase values.
        public void SetBorder(int _x_expand, int _y_expand)
        {
            x_expand = _x_expand;
            y_expand = _y_expand;
        }

        //Sets the I/O locations and block content.
        public void SetIOB(string _inName, string _outName, ushort _block)
        {
            if(reader != null)
                reader.Close();

            if (writer != null)
                writer.Close();

            try
            {
                reader = new BinaryReader(File.Open(_inName, FileMode.Open));
                writer = new BinaryWriter(File.Open("Output//" + _outName, FileMode.Create));
            }
            catch (FileNotFoundException ex)
            {
                Console.WriteLine(ex.Message);
            }
            block = _block;
        }

        //Gets the header of the orignal .map file.
        //Gets the width (x_res) and height (y_res) of the original .map file.
        public bool GrabHeader()
        {
            //Read .map file.
            for (int i = 0; i < header.GetLength(0); ++i)
            {
                header[i] = reader.ReadByte();
                //Console.Write("[{0:X}]", header[i]); //Displays Header
            }

            //Validate .map file.
            String MAP = "MAP";
            for (int i = 0; i < MAP.Length; ++i)
            {
                if ((char)header[i] != MAP[i])
                {
                    for (int j = 0; j < header.GetLength(0); ++j)
                    {
                        header[i] = 0;
                    }
                    return false;
                }
            }

            //establish .map file width and height.
            x_res = header[8];
            y_res = header[12];
            return true;
        }

        //Resizes the content matrix based on the width and height of the .map file.
        //Puts the contents of the .map file into the content matrix.
        public void GrabContent()
        {
            content = new ushort[y_res, x_res];
            for (int j = 0; j < y_res; ++j)
            {
                for (int i = 0; i < x_res; ++i)
                {
                    content[j, i] = reader.ReadUInt16();
                    //Console.Write("[{0:D}]", content[j, i]); //Displays Map Content.
                }
                //Console.Write("\n"); //Organizes rows of Map Content.
            }
        }

        //Resizes the newContent matrix based on the width and height of the .map file and the size of the border expansion.
        //Fills the newContent matrix.
        public void ModContent()
        {
            newContent = new ushort[y_res + y_expand * 2, x_res + x_expand * 2];                //2-dimensional content.
            ushort[] newContentX = new ushort[(y_res + y_expand * 2) * (x_res + x_expand * 2)]; //1-dimensional content.
            int contCounter = 0;   //Counter to track filling and pouring between 1 and 2-dimensional content.

            //Adds upper border
            for (int j = 0; j < y_expand; ++j)
            {
                for (int i = 0; i < x_res + x_expand * 2; ++i)
                {
                    newContentX[contCounter] = block;
                    contCounter++;
                }
            }

            //For loop, looping each row added. Neglects bottom excess.
            for (int j = 0; j < y_res - 1; ++j)
            {
                //Adds left borders.
                for (int i = 0; i < x_expand; ++i)
                {
                    newContentX[contCounter] = block;
                    contCounter++;
                }

                //Copies and adds center content. Neglects left excess.
                for (int i = 0; i < x_res - 1; ++i)
                {
                    newContentX[contCounter] = content[j, i];
                    contCounter++;
                }

                //Adds right borders and right excess.
                for (int i = 0; i < x_expand + 1; ++i)
                {
                    newContentX[contCounter] = block;
                    contCounter++;
                }
            }

            //Adds lower bock of void characters and bottom excess.
            for (int j = 0; j < y_expand + 1; ++j)
            {
                for (int i = 0; i < x_res + x_expand * 2; ++i)
                {
                    newContentX[contCounter] = block;
                    contCounter++;
                }
            }

            //Puts 1 dimensional new content into 2 dimensionall new content.
            for (int j = 0; j < newContent.GetLength(0); ++j)
            {
                for (int i = 0; i < newContent.GetLength(1); ++i)
                {
                    newContent[j, i] = newContentX[newContentX.GetLength(0) - contCounter];
                    contCounter--;
                }
            }
        }

        //Heightmatch.
        public void ModContentHeightMatch()
        {
            // HeightMatch top section.
            for (int j = 0; j < y_expand; ++j)
            {
                for (int i = 0; i < x_res - 1 + x_expand * 2; ++i)
                {
                    newContent[j, i] = newContent[y_expand, i];
                }
            }

            // HeightMatch bottom section.
            for (int j = y_res - 1 + y_expand; j < y_res - 1 + y_expand * 2; ++j)
            {
                for (int i = 0; i < x_res - 1 + x_expand * 2; ++i)
                {
                    newContent[j, i] = newContent[y_res - 1 + y_expand - 1, i];
                }
            }

            // HeightMatch left section.
            for (int j = 0; j < y_res - 1 + y_expand * 2; ++j)
            {
                for (int i = 0; i < x_expand; ++i)
                {
                    newContent[j, i] = newContent[j, x_expand];
                }
            }

            // HeightMatch right section.
            for (int j = 0; j < y_res - 1 + y_expand * 2; ++j)
            {
                for (int i = x_res - 1 + x_expand; i < x_res - 1 + x_expand * 2; ++i)
                {
                    newContent[j, i] = newContent[j, x_res - 1 + x_expand - 1];
                }
            }
        }

        //Outputs the contents of newContents as a new .map file.
        public void SpitContent()
        {
            //Write header.
            header[8] = (byte)(header[8] + x_expand * 2);
            header[12] = (byte)(header[12] + y_expand * 2);
            for (int j = 0; j < header.GetLength(0); ++j)
            {
                writer.Write(header[j]);
            }
            header[8] = (byte)x_res;
            header[12] = (byte)y_res;

            //Write content.
            for (int j = 0; j < newContent.GetLength(0); ++j)
            {
                for (int i = 0; i < newContent.GetLength(1); ++i)
                {
                    writer.Write(newContent[j, i]);
                }
            }
            writer.Close();
        }
    }
}
