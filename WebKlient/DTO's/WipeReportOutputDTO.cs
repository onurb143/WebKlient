namespace WebKlient.DTO_s
{
    /// DTO til læsning af oplysninger om en sletterapport.
    /// Indeholder detaljer om det udførte slettejob, disken og slettemetoden.
    /// Data Transfer Object (DTO) til at overføre oplysninger om en sletterapport.
    /// Denne klasse bruges til at hente og overføre data mellem API'et og klienten
    /// uden at eksponere hele domænemodellen.

    public class WipeReportReadDto
    {
        /// ID for slettejobbet. Entydig identifikation af jobbet.
        public int WipeJobId { get; set; }
        /// Starttidspunkt for sletningsprocessen.
        public DateTime StartTime { get; set; }
        /// Sluttidspunkt for sletningsprocessen.
        /// Kan være null, hvis jobbet ikke er afsluttet.

        public DateTime? EndTime { get; set; }

        /// Status for slettejobbet, fx "Completed", "Failed", eller "In Progress".
  
        public string? Status { get; set; }

        /// Diskens type, fx "HDD" eller "SSD".

        public string? DiskType { get; set; }

    
        /// Diskens kapacitet i GB. 
        /// Standardværdien 0 kan bruges, hvis kapacitet ikke er oplyst.

        public int Capacity { get; set; }

        /// Diskens serienummer. Bruges til entydig identifikation af disken.
  
        public string? SerialNumber { get; set; }

   
        /// Navnet på producenten, fx "Samsung" eller "Western Digital".
        /// Kan være null, hvis producent ikke er angivet.
 
        public string? Manufacturer { get; set; }


        /// Navnet på den anvendte slettemetode, fx "Secure Erase".
        /// Kan være null, hvis metode ikke er angivet.

        public string? WipeMethodName { get; set; }


        /// Antallet af overskrivningspasser, der blev udført.

        public int OverwritePasses { get; set; }

        /// Navnet på den bruger, der udførte sletningen. 
        /// Kan bruges til sporbarhed og dokumentation.
        public string? PerformedBy { get; set; }
    }
}
