using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ContactManager.Models
{
    public class TBLContact
    {
    [Key]
    public int id { get; set; }
    public string salutation { get; set; }
    public string firstname { get; set; }
    public string lastname { get; set; }
    public string displayname { get; set; }
    public DateTime? birthdate { get; set; }
    public DateTime creationtimestamp { get; set; }
    public DateTime lastchangetimestamp { get; set; }
    public bool notifyhasbirthdaysoon { get; set; }
    public string email { get; set; }
    public string phonenumber { get; set; } 
    }
}