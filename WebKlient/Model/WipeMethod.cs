﻿using System.ComponentModel.DataAnnotations;

namespace WebKlient.Model
{
    public class WipeMethod
    {
        [Key]
        public int MethodID { get; set; }  // Primary Key

        [Required]
        public string Name { get; set; }   // Navn på slettemetoden

        public string Description { get; set; }   // Beskrivelse af slettemetoden

        public int OverwritePass { get; set; }   // Antal overskrivninger
    }
}