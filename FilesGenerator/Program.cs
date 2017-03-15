using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using FilesGenerator.Logic;

namespace FilesGenerator 
{
  class Program
  {
    static void Main(string[] args)
    {
      var generator = new Generator();
      var rootFolder = @"C:\logs\generated";
      generator.Generate(rootFolder, 2, 2, 2, 2, 2);
    }
  }
}
