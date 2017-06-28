import java.util.*;

public class Hand {

  private int number;
  private List <Card> cards ;

public void Hand(int n , List <Card> c) {
  this.number = n;
  this.cards = c;
}

public void addCard (Card c){
  this.number++;
  this.cards.add(c);
  }
public void removeCard(Card c){
  if(this.cards.contains(c)) { this.cards.remove(c); this.number--;}

}
}
