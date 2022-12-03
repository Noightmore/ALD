gridGenerator: gridGenerator.o
	ld ./LabyrinthGenerator/build/gridGenerator.o -o	./LabyrinthGenerator/build/gridGenerator

gridGenerator.o: LabyrinthGenerator/main.asm
	nasm -f elf64 ./LabyrinthGenerator/main.asm -o ./LabyrinthGenerator/build/gridGenerator.o

clean:
	rm ./LabyrinthGenerator/build/*