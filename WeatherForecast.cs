namespace ContactManager;

public class WeatherForecast
{
     public int Id { get; set; }
    public string Salutation { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string DisplayName { get; set; }
    public DateTime? BirthDate { get; set; }
    public DateTime CreationTimestamp { get; set; }
    public DateTime LastChangeTimestamp { get; set; }
    public bool NotifyHasBirthdaySoon { get; set; }
}
