using System;
using System.Collections.Generic;
using System.Text;

namespace FileServer
{
    public class FileServerConfig
    {
        public PathItem Root { get; set; }
        public List<PathItem> PathList { get; set; }
    }

    public class PathItem
    {
        public string LocalPath { get; set; }

        public string Url { get; set; }
    }
}
