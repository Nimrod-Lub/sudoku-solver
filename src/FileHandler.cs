using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace src
{
    public class FileHandler : IInputHandler, IOutputHandler
    {
        private string[] lines;
        private int lineIndex;
        private string outputPath;
        public FileHandler(string filePath)
        {
            if (File.Exists(filePath))
            {
                lines = File.ReadAllLines(filePath);
                lineIndex = 0;
                outputPath = Path.Combine(Path.GetDirectoryName(filePath), SudokuConstants.OUTPUT_FILENAME);
                File.Delete(outputPath);
            }
            else
                throw new IOException("No file with such a name was found");
        }
        public string GetInput()
        {
            return lineIndex == lines.Length ? null : lines[lineIndex++];
        }
        public void Output(string str)
        {
            File.AppendAllText(outputPath, str + Environment.NewLine);
        }
    }
}
