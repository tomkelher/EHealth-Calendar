public class Event {
	public int id {get; set; }
	public string titel {get; set; }
	public string omschrijving {get; set; }
	public string locatie {get; set; }
	public DateTime van {get; set; }
	public DateTime tot {get; set; }

	public Event(int id, string titel, string omschrijving, string locatie, DateTime van, DateTime tot){
		this.id = id;
		this.titel = titel;
		this.omschrijving = omschrijving;
		this.locatie = locatie;
		this.van = van;
		this.tot = tot;
	}
}