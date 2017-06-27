public class Main {

	public static void main(String[] args) { 	//void: on retourne rien. Permets un appel direct de la fonction,
							//static : la fonction n'est rattachée à rien
	for(int i = 0; i < 10 ; i++) {
		System.out.println("Hello");
	}

	Foo c = new Foo();
	c.increment();

	System.out.println(c.getValeur());

	System.out.println(Foo.disBonjour());

	System.out.println(Math.PI);
	}
}
