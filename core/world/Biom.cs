public class Biom {
	
	private string name;
	public string[] HexColors {get; set;}
	

	public Biom(string name) {
		this.name = name;
	}

	public string[] GetColors() {
		return HexColors;
	}

	public Biom Add(MultiKeyDictionary<string, Biom> bioms){
		bioms.Add(this, HexColors);
		return this;
	}
}
