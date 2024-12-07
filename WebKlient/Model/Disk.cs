﻿namespace WebKlient.Model
{
    public class Disk
    {

        public int DiskID { get; set; }


        public string Type { get; set; } = string.Empty;


        public int Capacity { get; set; }


        public string Status { get; set; } = string.Empty;


        public string? Path { get; set; }


        public string? SerialNumber { get; set; }


        public string? Manufacturer { get; set; }
    }
}