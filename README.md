# Séance 2 : La reprise d’un projet Legacy

## I. Les difficultés liées à la validation

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

### 4. Métriques de maintenabilité

#### 4.1. Complexité cyclomatique, Couplage et Cohésion

La complexité cyclomatique de la plupart des méthodes est juste astronomique. Il n'y a qu'a regarder les `switch` imbriqués qui contiennent eux-mêmes des `if` pour s'en rendre compte.

De plus, le code actuel est très couplé et peu cohésif.

### 3. Code smells

Voici quelques exemples de code smells dans le projet :

#### 3.1. Dead code

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

#### 3.2. Duplicate code

Il y a énormément de code dupliqué dans le projet. Les classes `Morpion` et `PuissanceQuatre` sont quasi-identiques.

#### 3.3. Long method & Long class

Les classes `Morpion` et `PuissanceQuatre` font plus de 250 lignes chacune, et contiennent des méthodes faisant facilement plus de 50 lignes.

#### 3.4. Primitive obsession

Le code utilise des types primitifs pour représenter des concepts métier. Par exemple, la grille est un tableau de chaînes de caractères.

#### 3.5. Switch statements
 
Comme mentionné précédemment, le code contient de nombreux `switch` imbriqués qui contiennent eux-mêmes des `if`. Cela rend le code difficile à comprendre et à maintenir.

