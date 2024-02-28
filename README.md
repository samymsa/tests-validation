# Séance 2 : La reprise d’un projet Legacy

## I. Les difficultés liées à la validation

Bien que le jeu soit relativement fonctionnel, les choix de design du prestataire initial vont poser des problèmes pour la validation du projet. En effet, le code fourni ne respecte pas les principes SOLID et contient de nombreux code smells. Par conséquent, il est difficilement maintenable et testable. Automatiser les tests va donc être un réel défi et nécessitera des modifications du code.

Ci-dessous, une liste des problèmes rencontrés dans le code fourni.

### 1. Manque de tests automatisés

Il n'y a pas de tests automatisés pour valider le bon fonctionnement du jeu. Il va donc falloir en mettre en place pour éviter les régressions.

### 2. Violation des principes SOLID

Le code ne respecte pas les principes SOLID. Il va falloir refactoriser le code pour le rendre plus maintenable.

#### 2.1. Violation du principe de responsabilité unique

Dans le code fourni, les classes `Morpion`, `PuissanceQuatre`, et `Program` ont des responsabilités multiples. Par exemple, elles gèrent à la fois la logique du jeu, l'interaction avec l'utilisateur et le contrôle de flux.

#### 2.2. Violation du principe ouvert/fermé

Le code actuel nécessiterait des modifications directes pour ajouter de nouvelles fonctionnalités ou jeux, plutôt que d'utiliser des mécanismes d'extension. Par exemple, pour ajouter un nouveau jeu, il faudrait modifier la classe `Program` et ajouter une nouvelle condition dans les instructions switch, ce qui viole le principe OCP.

#### 2.3. Violation du principe d'inversion de dépendance

Dans le code actuel, les classes `Morpion` et `PuissanceQuatre` dépendent directement de la console pour l'interaction utilisateur, ce qui les rend difficiles à tester et à réutiliser dans d'autres contextes. Cela va constituer l'un des principaux défis de l'automatisation des tests.
En inversant cette dépendance et en introduisant des abstractions pour l'interaction utilisateur, le code serait plus flexible et conforme au principe DIP.

### 3. Métriques de maintenabilité

#### 3.1. Complexité cyclomatique, Couplage et Cohésion

La complexité cyclomatique de la plupart des méthodes est juste astronomique. Il n'y a qu'a regarder les `switch` imbriqués qui contiennent eux-mêmes des `if` pour s'en rendre compte.

De plus, le code actuel est très couplé et peu cohésif.

#### 3.2 Code smells

Voici quelques exemples de code smells dans le projet :

##### Dead code

Il y a du code mort dans le projet, par exemple dans la classe `PuissanceQuatre`, pour désactiver les flèches haut et bas :

```csharp
                    //case ConsoleKey.UpArrow:
                    //    if (row <= 0)
                    //    {
                    //        row = 3;
                    //    }
                    //    else
                    //    {
                    //        row = row - 1;
                    //    }
                    //    break;

                    //case ConsoleKey.DownArrow:
                    //    if (row >= 3)
                    //    {
                    //        row = 0;
                    //    }
                    //    else
                    //    {
                    //        row = row + 1;
                    //    }
                    //    break;
```

##### Duplicate code

Il y a énormément de code dupliqué dans le projet. Les classes `Morpion` et `PuissanceQuatre` sont quasi-identiques.

##### Long method & Long class

Les classes `Morpion` et `PuissanceQuatre` font plus de 250 lignes chacune, et contiennent des méthodes faisant facilement plus de 50 lignes.

##### Primitive obsession

Le code utilise des types primitifs pour représenter des concepts métier. Par exemple, la grille est un tableau de chaînes de caractères (en plus d'avoir une taille fixe) :

```csharp
grille = new char[3, 3]
    {
        { ' ', ' ', ' '},
        { ' ', ' ', ' '},
        { ' ', ' ', ' '},
    };
```

##### Switch statements

Comme mentionné précédemment, le code contient de nombreux `switch` imbriqués qui contiennent eux-mêmes des `if`. Cela rend le code difficile à comprendre et à maintenir.

```csharp
public void tourJoueur()
    {
        ...
        while (!quiterJeu && !moved)
        {
            ...
            switch (Console.ReadKey(true).Key)
            {
                ...
                case ConsoleKey.RightArrow:
                    if (column >= 2)
                    {
                        column = 0;
                    }
                    else
                    {
                        column = column + 1;
                    }
                    break;
...
```

##### Autres

- Le français se mélange à l'anglais dans le code :

  ```csharp
  while (!quiterJeu && !moved)
  ```

- Les méthodes `verifEgalite` et `verifVictoire` n'utilisent pas de boucles mais une combinaison gigantesque de `&&` et de `||` pour vérifier les conditions de victoire ou d'égalité.

- La classe `Program` contient des instructions `goto`, ce qui est considéré comme une mauvaise pratique et peut rendre difficile le débogage du code :

  ```csharp
  default:
      goto GetKey;
  ```

- Comme dit plus haut, les entrées proviennent systématiquement de la console. Il n'y a pas de paramètres pour les méthodes, ce qui va nécessiter de modifier le code pour pouvoir écrire des tests unitaires.

## II. Les méthodes de résolution de ces problèmes

### 1. Refactorisation du code

Avant de pouvoir mettre en place des tests automatisés, il va falloir refactoriser le code pour le rendre plus maintenable et testable. Nous allons essayer de supprimer un maximum de code smells et de violations des principes SOLID.

Dans un premier temps, nous allons supprimer la duplication de code en créant une classe abstraite `BaseGame` que les classes `Morpion` et `PuissanceQuatre` vont étendre.

Ensuite, nous allons introduire des abstractions pour l'interaction utilisateur, afin de rendre le code plus flexible et conforme au principe d'inversion de dépendance.

Enfin, nous allons essayer de réduire la complexité cyclomatique des méthodes, en les découpant en méthodes plus petites et plus cohésives.

### 2. Mise en place de tests automatisés

Pour valider le bon fonctionnement du jeu, il va falloir mettre en place des tests automatisés. Pour cela, nous allons utiliser xUnit afin de mettre en place des tests unitaires.

### 3. Correction de bugs eventuels

Une fois les tests automatisés en place, il sera plus facile d'identifier et de corriger les éventuels bugs dans le code. En cas de bugs, nous allons les corriger et mettre à jour si besoin les tests automatisés.
