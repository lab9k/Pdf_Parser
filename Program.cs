using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using PDFParser_Core;

namespace PDFParser
{
    class Program
    {
        static void Main(string[] args)
        {
            List<string> arg = args.ToList();
            if (arg.Count == 0)
            {
                Console.WriteLine("Invalid parameters, expected: inputfile [-o outputfile] [-f outputformat]");
            }
            else if (arg.Count == 1 && File.Exists(args[0]))
            {
                WriteCSV(args[0], "output.csv");
            }
            else if (arg.Count >= 3)
            {
                string outputFile = "output";
                string inputFile = "";
                bool asCSV = true;
                bool error = false;

                while (!error && arg.Count > 0)
                {
                    if (arg[0] == "-o" && arg.Count > 1)
                    {
                        outputFile = arg[1];
                        arg.RemoveRange(0, 2);
                    }
                    else if (arg[0] == "-f" && arg.Count > 1)
                    {
                        string format = arg[1];
                        if (format.ToLower() == "json")
                        {
                            asCSV = false;
                            arg.RemoveRange(0, 2);
                        }
                        else if (format.ToLower() == "csv")
                        {
                            arg.RemoveRange(0, 2);
                        }
                        else
                        {
                            Console.WriteLine("Invalid format, expected: json / csv");
                            error = true;
                        }
                    }
                    else if (File.Exists(arg[0]))
                    {
                        inputFile = arg[0];
                        arg.RemoveAt(0);
                    }
                    else
                    {
                        Console.WriteLine("Invalid parameters, expected: inputfile [-o outputfile] [-f outputformat]");
                        error = true;
                    }

                    
                }
                if (arg.Count == 0)
                {
                
                    Console.WriteLine("Parsing...");
                    if (asCSV)
                    {
                        WriteCSV(inputFile, outputFile);
                    }
                    else
                    {
                        WriteJson(inputFile, outputFile);
                    }
                }

            }
            else
            {
                Console.WriteLine("Invalid parameters, expected: inputfile [-o outputfile] [-f outputformat]");
            }
            Console.WriteLine("Finished, press any key to exit.");
            Console.ReadKey();
        }


        static void WriteCSV(string input, string output)
            {
                if (!output.EndsWith(".csv"))
                {
                    output += ".csv";
                }
                List<string> res = new DataFilter().FilterLines(new PDFReadercs().GetPdfAsString(input));
                List<DataModel> validModels = new List<DataModel>();
                foreach (string line in res)
                {
                    DataModel currModel = new DataModel(line);
                    if (currModel.Transponder != 0) //Model is not valid without transponder
                    {
                        validModels.Add(currModel);
                    }
                }

                System.IO.File.WriteAllText(output, ""); //clear file
                System.IO.File.AppendAllText(output, new DataModel("").CSVHeader + "\n");

                foreach (DataModel model in validModels)
                {
                    System.IO.File.AppendAllText(output, model.CSV + "\n");
                }
            }

            static void WriteJson(string input, string output)
            {
                if (!output.EndsWith(".json"))
                {
                    output += ".json";
                }
            List<string> res = new DataFilter().FilterLines(new PDFReadercs().GetPdfAsString(input));


                System.IO.File.WriteAllText(output, ""); //clear file
                System.IO.File.AppendAllText(output, "[\n");

                List<DataModel> validModels = new List<DataModel>();
                foreach (string line in res)
                {
                    DataModel currModel = new DataModel(line);
                    if (currModel.Transponder != 0) //Model is not valid without transponder
                    {
                        validModels.Add(currModel);
                    }
                }


                for (int i = 0; i < validModels.Count; i++)
                {
                    System.IO.File.AppendAllText(output, validModels[i].JSON);
                    if (i < validModels.Count - 1)
                    {
                        System.IO.File.AppendAllText(output, ",\n");
                    }
                }
                System.IO.File.AppendAllText(output, "\n]");
            }
        }
    }