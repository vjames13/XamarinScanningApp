﻿using System;
using System.Collections.Generic;
using System.Text;

namespace MonoAndroidDemo.Models.Shared
{
    public class ZenFile
    {
        public string FileName { get; set; }
        public string ContentType { get; set; }
        public byte[] FileData { get; set; }
    }
}
