using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using src.Constants;

namespace src.IO
{
    public class FileHandler : IInputHandler, IOutputHandler // Input/output boards to file
    {
        private string[] lines;
        private int lineIndex;
        private string outputPath;
        public FileHandler(string filePath)
        {
            if (File.Exists(filePath))
            {
                lines = File.ReadAllLines(filePath); // Throws an IOException if something goes wrong
                lineIndex = 0;
                outputPath = Path.Combine(Path.GetDirectoryName(filePath), IOConstants.OUTPUT_FILENAME); // Set output file to output.txt
                File.Delete(outputPath); // Insure output.txt is empty
            }
            else // No such file
                throw new IOException("No file with such a name was found");
        }
        public string GetInput() // Get a sudoku board from input file
        {
            return lineIndex == lines.Length ? null : lines[lineIndex++];
        }
        public void Output(string str) // Write a sudoku board to output file
        {
            File.AppendAllText(outputPath, str + Environment.NewLine);
        }
    }
}
