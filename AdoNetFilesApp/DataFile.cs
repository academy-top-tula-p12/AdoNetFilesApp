using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdoNetFilesApp
{
    public class DataFile
    {
        public int Id { get; set; }
        public string? Title { set; get; }
        public string? FileName { set; get; }
        public byte[] BinaryData { set; get; }

        public DataFile(int id, string? title, string? fileName, byte[] binaryData)
        {
            Id = id;
            Title = title;
            FileName = fileName;
            BinaryData = binaryData;
        }
    }
}
