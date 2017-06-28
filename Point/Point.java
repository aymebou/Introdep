public class Point {


 private int x;
 private int y;

public Point() {
this.x = 0;
this.y = 0;}

 public int getX(){
   return this.x;
 }
 public int getY(){
   return this.y; }
 public String versChaine() {
  return "(" + this.x + "," + this.y + ")";
}

 public void deplacer(int dx, int dy) {
  this.x = dx;
  this.y = dy;
 }

}
