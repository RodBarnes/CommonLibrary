using System;
using System.IO;
using System.Text;

namespace Common
{
    /// <summary>
    /// Based upon the Microsoft.VisualBasic.FileIO.TextFieldParser
    /// Since the above appears to be incompatible with ASP.NET Core 2.0
    /// this class was developed to replace that functionality.
    /// </summary>
    public class TextFieldParser : StreamReader, IDisposable
    {
        private char[] delimiters;

        public TextFieldParser(string path) : base(path) { }

        public TextFieldParser(Stream stream) : base(stream) { }

        #region Properties

        public bool HasFieldsEnclosedInQuotes { get; set; } = false;

        public bool EndOfData => EndOfStream;

        public long Length => base.BaseStream.Length;

        #endregion

        #region Methods

        public string[] ReadFields() => GetFields(base.ReadLine());

        public void SetDelimiters(string delim) => delimiters = delim.ToCharArray();

        public string[] GetFields(string curLine)
        {
            var quoted = false;
            var iToken = 1;

            if (delimiters == null || delimiters.Length == 0)
                throw new Exception($"{GetType().Name} requires delimiters be defined to identify fields.");

            if (!HasFieldsEnclosedInQuotes)
            {
                return curLine.Split(delimiters);
            }
            else
            {
                var token = (char)iToken;
                var sb = new StringBuilder();

                // Go through the string and change delimiters to token
                // ignoring them if within quotes if indicated
                for (int c = 0; c < curLine.Length; c++)
                {
                    var qc = curLine[c];

                    if (HasFieldsEnclosedInQuotes && qc == '"')
                    {
                        quoted = !quoted;
                        continue;
                    }
                    else if (!quoted)
                    {
                        // Replace the delimiters with token
                        for (int d = 0; d < delimiters.Length; d++)
                        {
                            if (qc == delimiters[d])
                            {
                                qc = token;
                                break;
                            }
                        }
                    }

                    sb.Append(qc);
                }

                return sb.ToString().Split(token);
            }
        }

        #endregion
    }
}
