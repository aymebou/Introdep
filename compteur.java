public class Compteur {

	private int valeur = 0;
	
	public void increment() {
		this.valeur += 1;
	}	
	public int getValeur {
		return this.valeur;
	}
	
	public static String disBonjour() { //static: fonction non rattachee à un objet.
	
	return "Bonjour";
}

}



public class Main {

	public static void main(String[] args) { 	//void: on retourne rien. Permets un appel direct de la fonction, 
							//static : la fonction n'est rattachée à rien
	for(int i = 0; i < args.length ; i++) {
		System.out.println("args[" + i + "] = " + args[i]);
	}
	
	Foo c = new Foo();
	c.increment();
	
	System.out.println(c.getValeur());
	
	System.out.println(Foo.disBonjour());
	
	System.out.println(Math.PI);
	}
}

/*mot clé final: personne ne peut dire un objet est un maths
heritage: un cheval est un animal. les objets d'une certaines classe sont des objets d'une autre classe
final: personne ne peut modifier

*/

// On fait attention à la syntaxe (;,{}) sinon java pas content

