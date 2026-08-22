# Two Slot Big Mags

Mod serveur pour SPT 4.1.x. Il réduit de 3 à 2 cases la hauteur de tous les chargeurs de 40 coups.

La détection est faite au démarrage à partir de la capacité réelle du chargeur. Les chargeurs de 40 coups ajoutés par d'autres mods sont donc également pris en charge s'ils font 3 cases de haut. Les chargeurs qui font déjà 2 cases ne sont pas modifiés.

## Installation

1. Compilez le projet avec `dotnet build -c Release`.
2. Créez un dossier `TwoSlotBigMags` dans `SPT_Runtime/user/mods`.
3. Copiez-y `bin/Release/TwoSlotBigMags.dll`.
4. Redémarrez le serveur SPT.

Le serveur affiche au démarrage le nombre de chargeurs détectés et modifiés.

## Compatibilité

- SPT 4.1.x
- Testé avec SPT 4.1.2
- Aucun nouveau profil n'est nécessaire
- Le mod peut être ajouté ou retiré sans modifier définitivement la sauvegarde
