public class Bioms {
	private static MultiKeyDictionary<string, Biom> bioms;

	//private static Biom biom;
	public void Load() {
		bioms = new MultiKeyDictionary<string, Biom>();
		NewBiom(
			new Biom("forest") {
				HexColors = new string[] {}
			},
			new Biom("water") {
				HexColors = new string[] {}
			}
		);
	}

	private void NewBiom(params Biom[] b) {
		foreach(Biom biom in b) {
			bioms.Add(biom, biom.HexColors);
		}
	}

	public static Biom GetBiom(string color) {
		return bioms.Get(color);
	}
}
