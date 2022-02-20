# lucca-devises
Problème sur la gestion des devises pour test technique entreprise LUCCA

Le lien de ce projet devrait vous être fourni avec .exe qui correspond à l'exécutable publié de la dernière version de l'application.

Si vous n'avez pas reçu l'executablen, vous pouvez publier vous même l'executable.
Pour publier la solution depuis Visual Studio, clic droit sur le projet et publier.

Généralement, l'executable se troupe dans bin/release/net6.0/publish et s'appelera LuccaDevises.exe

Vous pouvez le lancer depuis une commande LuccaDevises <chemin du fichier> ou LuccaDevises.exe <chemin du fichier>

Fonctionnalités prise en compte:
- Gestion des entrées comme spécifié
- Tableau de devises de taille variable
- Gestion des erreurs si aucun fichier spécifié et si aucun chemin possible entre les deux devises
- Gestion des montants avec virgule et points
- Arrondis à la 4ème décimale pour calcule intermédiaire et affichage retour en nomber entier
- Optimisation de chemin BFS

L'algorithme d'optimisation de chemin est grandement inspiré de cet article : 
https://www.koderdojo.com/blog/breadth-first-search-and-shortest-path-in-csharp-and-net-core
  
Optimisations envisageables :
- Refactoring du Program
- Utilisation de type plus adapté ( HashSet,...)
- Test unitaire sur plusieurs jeux de données
- Gestion des erreurs: Si deuxième ligne est incohérente avec nombre de lignes du ficher
- Verification des calculs à 4ème décimale 
- Optimisation de la complexité des calculs ( multiplier les taux de changes entre eux puis multiplier le résultats au montant initial ou bien multiplier a chaque fois les taux de changes avec le montant en traitement ? Quel est le plsu précis ? )
  

