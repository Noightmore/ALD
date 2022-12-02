gridGenerator: gridGenerator.o
	ld ./build/gridGenerator.o -o	./build/gridGenerator

gridGenerator.o: main.asm
	nasm -f elf64 main.asm -o ./build/gridGenerator.o

clean:
	rm ./build/*