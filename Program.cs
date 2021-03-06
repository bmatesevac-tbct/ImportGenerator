﻿using CommandLine;
using System;
using System.IO;

namespace ImportGenerator
{

   class Program
   {
      /*
       * Load the options.
       * Make sure they good. If not, report and exit.
       * Create an ImportGenerator and run it.
       */
      static void Main(params string[] args)
      {

         var help = new StringWriter();
         var parser = new Parser(config =>
         {
            config.HelpWriter = help;
            config.MaximumDisplayWidth = 80;
         });

         Options options = new Options();
         parser.ParseArguments<Options>(args)
            .WithParsed(o => options = o);

         var anyErrors = Options.Validate(options);
         if (anyErrors != String.Empty)
            help.Write(anyErrors);

         var result = help.ToString();
         if (result.Length != 0)
         {
            Console.WriteLine(result);
            Environment.Exit(1);
         }

         var generator = new ImportGenerator(options);
         generator.Run();
      }
  }
}