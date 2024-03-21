namespace Bataille {
    internal partial class Program {
        static void Main(string[] args) {

            void print(string s) {
                Console.WriteLine(s);
            }
            double comp = 0, bat = 0, maxComp = 0, maxBat = 0, minComp = double.MaxValue, minBat = double.MaxValue, totBat = 0, totComp = 0;
            const int ITERATIONS = 100;
            for (int rep = 0; rep < ITERATIONS; rep++) {
                Carte[] deck = new Carte[52];
                for (int i = 0; i < 4; i++) {
                    for (int j = 0; j < 13; j++) {
                        Carte c = new Carte { suite = (Suite)i, valeur = (Valeur)j + 2 };
                        deck[i * 13 + j] = c;
                    }
                }
                suffle(deck, 1_000_000);
                Queue<Carte> deck1 = new Queue<Carte>();
                Queue<Carte> deck2 = new Queue<Carte>();
                Stack<Carte> played1 = new Stack<Carte>();
                Stack<Carte> played2 = new Stack<Carte>();
                for (int i = 0; i < deck.Length; i++) {
                    if (i % 2 == 0) {
                        deck1.Enqueue(deck[i]);
                    } else {
                        deck2.Enqueue(deck[i]);
                    }
                }
                bool bataille = false;
                while (deck1.Count > 0 && deck2.Count > 0) {
                    played1.Push(deck1.Dequeue());
                    played2.Push(deck2.Dequeue());
                    if (bataille) {
                        bataille = false;
                    } else {
                        comp++;
                        print($"joueur 1 joue {played1.Peek()}, joueur 2 joue {played2.Peek()}");
                        int win = played1.Peek().CompareTo(played2.Peek());
                        if (win == 0) {
                            print("Bataille !");
                            bataille = true;
                            bat++;
                        } else if (win > 1) {
                            print("Joueur 1 l'emporte.");
                            while (played1.Count > 0) {
                                deck1.Enqueue(played1.Pop());
                            }
                            while (played2.Count > 0) {
                                deck1.Enqueue(played2.Pop());
                            }
                        } else {
                            print("Joueur 2 l'emporte.");
                            while (played2.Count > 0) {
                                deck2.Enqueue(played2.Pop());
                            }
                            while (played1.Count > 0) {
                                deck2.Enqueue(played1.Pop());
                            }
                        }
                    }
                }

                totComp += comp;
                totBat += bat;
                minBat = minBat < bat ? minBat : bat;
                minComp = minComp < comp ? minComp : comp;
                maxBat = maxBat > bat ? maxBat : bat;
                maxComp = maxComp > comp ? maxComp : comp;
                comp = 0;
                bat = 0;

                if (deck1.Count > 0) {
                    print("Joueur 1 gagne la partie !");
                } else {
                    print("Joueur 2 gagne la partie !");
                }
                print($"{deck1.Count} cartes dans le deck du joueur 1, {played1.Count} cartes en jeu; {deck2.Count} cartes dans le deck du joueur 2, {played2.Count} cartes en jeu.");
                print($"{52 - deck1.Count - deck2.Count - played1.Count - played2.Count} cartes ont disparu durant la partie.");
            }
            print($"moyenne de comparaisons : {totComp / ITERATIONS}\nmoyenne de batailles : {totBat / ITERATIONS}");
            print($"extrèmes en {ITERATIONS} parties : [{minComp}, {maxComp}] comparaisons et [{minBat}, {maxBat}] batailles");
        }


        static void suffle(Carte[] tab, long shuffles) {
            if (tab.Length > 1) {
                Random rnd = new Random();
                for (long i = 0; i < shuffles; i++) {
                    if (i % (shuffles / 10) == 0) Console.WriteLine($"Shuffling... {10 * i / shuffles}/10");
                    int x = rnd.Next(tab.Length);
                    int y;
                    do {
                        y = rnd.Next(tab.Length);
                    } while (x == y);
                    Carte temp = tab[x];
                    tab[x] = tab[y];
                    tab[y] = temp;
                }
            }
        }
    }
}
