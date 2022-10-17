﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace addressbook.Services
{
    internal interface IFileService
    {
        public void Save(string filePath, string content);
        public string Read(string filePath);
    }
    internal class FileService : IFileService
    {
        public void Save(string filePath, string content)
        {
            using var sw = new StreamWriter(filePath);
            sw.WriteLine(content);
        }
        public string Read(string filePath)
        {
            using var sr = new StreamReader(filePath);
            return sr.ReadToEnd();
        }
    }

}
