using System;
using System.Collections.Generic;
using System.Text;

namespace Models
{
    public class Company
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public int TelephoneNr { get; set; }
        public string Location { get; set; }
        public int Employee { get; set; }
        public string Link { get; set; }
        public string Info { get; set; }
    }
}
