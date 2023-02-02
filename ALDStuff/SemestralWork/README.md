# LabyrinthGenerator

popis práce je popsán v commitech...

## Závislosti
- qt5 framework
- cmake, gcc, ninja, glibc, ld... (celý balíček nástrojů pro kompilaci, linkování a spouštění c++ projektů)


## aplikaci lze spustit následovně

### ./LabyrinthGenerator grid_width grid_height seed zoom

- grid_width = šířka pole
- grid_height = výška pole
- seed = seed, libovolné číslo
- zoom = přiblížení pole (čím je číslo větší, tím vetší budou jednotlivé obrázky)

Algoritmus je spíše generátor náhodných organizovaných patternů než li přímo bludiště...

![image](https://user-images.githubusercontent.com/91696187/210182310-3ae5bea2-bd2d-4eed-ab72-bfa42b8caa4d.png)

![image](https://user-images.githubusercontent.com/91696187/210183022-72e83b60-af98-49c5-a326-4584fec6792b.png)
