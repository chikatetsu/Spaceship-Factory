# Spaceship Factory

Ce projet vise à automatiser et simplifier la production et l'assemblage de vaisseaux spatiaux dans une usine. L'application gère la gestion des stocks, le traitement des commandes, et la génération des instructions d'assemblage pour divers modèles de vaisseaux.

Le Trello est disponible [ici](https://trello.com/invite/b/661cd3eaa62e4c2489c1dddf/ATTI5cb63d2a3da022dcdb32d86e72ea2a12B182F092/spaceship-factory).

## Vue d'ensemble du projet

Le projet Spaceship Factory est conçu pour gérer l'assemblage final des vaisseaux en utilisant des pièces pré-produites. Le système permet de recevoir de nouveaux stocks, de générer des instructions d'assemblage, de vérifier et de produire des commandes, et de modifier les attributs et les designs des vaisseaux. Le projet utilise plusieurs design patterns pour assurer la modularité, l'évolutivité et la maintenabilité de la base de code.

## Modules obligatoires

* **Recevoir du stock (Commande RECEIVE) :** Gère l'ajout de nouvelles pièces et des assemblages de vaisseaux dans l'inventaire de l'usine.
* **Générer la liste d'instructions pour produire des vaisseaux (Commande INSTRUCTIONS) :** Génère une liste détaillée des instructions nécessaires pour assembler un nombre spécifié de vaisseaux.
* **Stock nécessaire à la production de vaisseaux (Commande NEEDED_STOCKS) :** Calcule les pièces nécessaires de l'inventaire pour répondre à une commande de production.
* **Gérer le stock de vaisseaux et de pièces (Commande STOCKS) :** Gère la gestion globale de l'inventaire des pièces et des vaisseaux assemblés.
* **Créer un vaisseau (Commande PRODUCE) :** Exécute la production d'un vaisseau en fonction d'une commande donnée, en mettant à jour l'inventaire en conséquence.
* **Vérifier une commande (Commande VERIFY) :** Valide une commande de production pour s'assurer qu'elle est correcte et réalisable avec l'inventaire actuel.
* **Séparer les éléments dans les commandes par des virgules :** Analyse les commandes utilisateur en séparant les éléments avec des virgules.
* **Changer les attributs des vaisseaux :** Permet la modification des attributs des vaisseaux, en s'assurant qu'ils respectent les contraintes de construction.

## Modules complémentaires

* **Création de vaisseaux customisés :** Permet la création de templates de vaisseaux personnalisés, en respectant les contraintes de construction spécifiées.
* **Modification de vaisseaux :** Fournit des fonctionnalités pour ajouter, retirer ou remplacer des pièces dans les designs de vaisseaux existants.

## Design Patterns

* **Factory Method / Abstract Factory :** Utilisés pour créer des pièces de vaisseaux et des assemblages de manière modulaire et évolutive.
* **Builder pour les vaisseaux :** Employé pour construire des objets vaisseaux complexes étape par étape, assurant que le processus de construction est flexible et réutilisable.
* **Stock Singleton :** Assure une instance unique du système de gestion des stocks, fournissant un point d'accès global.
* **Command pattern pour gérer le parsing des commandes de l'utilisateur :** Utilisé pour encapsuler les commandes utilisateur en tant qu'objets, permettant une modification, une extension et une journalisation faciles des commandes.

Ce projet fournit un cadre robuste pour gérer l'assemblage des vaisseaux dans un environnement industriel, avec un fort accent sur la conception modulaire et la gestion efficace des stocks.

## Commandes possibles

* `STOCKS` : Affiche l'inventaire actuel des pièces et des vaisseaux.
* `NEEDED_STOCKS 1 Cargo` : Calcule les pièces nécessaires pour produire un vaisseau de type Cargo.
* `PRODUCE 1 Speeder, 2 Cargo` : Produit un vaisseau de type Speeder et deux vaisseaux de type Cargo.
* `VERIFY PRODUCE 1 Speeder, 2 Cargo` : Vérifie si la production de 1 Speeder et 2 Cargo est possible.
* `INSTRUCTIONS 1 Speeder` : Génère les instructions pour assembler un vaisseau de type Speeder.
* `ADD_TEMPLATE Speed Hull_HS1 Engine_ES1 Wings_WS1 Wings_WS1 Thruster_TS1 Thruster_TS1` : Crée un template pour un vaisseau de type Speeder.
* `MODIFY Speed REPLACE 1 Engine_ES1, 2 Wings_WS1 WITH 2 Engine_EE1, 2 Wings_WE1` : Modifie un vaisseau de type Speeder en remplaçant les pièces spécifiées.
* `RECEIVE S 2 Speeder` : Ajoute deux vaisseaux de type Speeder à l'inventaire.
* `RECEIVE S 2 Speeder A 3 Engine_ES1 A 3 Hull_HS1 P 4 Hull_HE1` : Ajoute deux vaisseaux de type Speeder, trois moteurs Engine_ES1, trois coques Hull_HS1, et quatre coques Hull_HE1 à l'inventaire.