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
        const string input = "Input\\";
        const string output = "Output\\";

        const string cror = "cror.map";
        const string dugg = "dugg.map";
        const string emrg = "emrg.map";
        const string erod = "erod.map";
        const string fall = "fall.map";
        const string high = "high.map";
        const string path = "path.map";
        const string surf = "surf.map";
        const string tuto = "tuto.map";

        public static void Main(string[] args)
        {
            int x_expand = 8;
            int y_expand = 4;

            //Expand map files.
            //Dictionary<string, string> maps = new Dictionary<string, string>

            string[] maps = { cror, dugg, emrg, erod, fall, high, path, surf, tuto };
            LRRMapHandlerExpand testMap = new LRRMapHandlerExpand();
            testMap.setBorder(x_expand, y_expand);
            for (int i = 0; i < maps.GetLength(0); ++i)
            {
                //Set block for surf.map
                if (i == 7)
                    testMap.setIOPB(input, output, maps[i], 1);
                //Set block for high.map
                else if (i == 5)
                    testMap.setIOPB(input, output, maps[i], 8);
                //Set block for other file.map's
                else
                    testMap.setIOPB(input, output, maps[i], 0);
                //Expand
                testMap.grabHeader();
                testMap.grabContent();
                testMap.modContent();
                //Hight Match
                if (i == 5)
                    testMap.modContentHeightMatch();
                //Output
                testMap.spitContent();
            }

            //Expand Ol file.
            LRROlHandlerExpand testOl = new LRROlHandlerExpand(input, output, "ObjectList.ol");
            testOl.Action(x_expand, y_expand);

            Console.ReadKey();
        }
    }
}
