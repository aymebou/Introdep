public class Main {


public static void main(String[] args) {
  Point p1 = new Point();

  System.out.println("Nous venons de créer le point : p1 = "+p1.versChaine());

  p1.deplacer(2,3);
  assert p1.getX() == 2;
  assert p1.getY() == 3;


}
}
