project(LabyrinthGenerator)
enable_language(ASM_NASM)
set(CMAKE_ASM_NASM_OBJECT_FORMAT elf64)
set(CMAKE_ASM_NASM_LINK_EXECUTABLE "ld <CMAKE_ASM_NASM_LINK_FLAGS> <LINK_FLAGS> <OBJECTS> -o <TARGET> <LINK_LIBRARIES>")
set(CMAKE_ASM_NASM_FLAGS_DEBUG "-g -Fdwarf")

set(SOURCE_FILES
        main.asm
        tools/general_functions.asm
        tools/memory_functions.asm
        user_interface/print_functions.asm
        data/tiles.asm
        data/grid.asm
        )

set_source_files_properties(${SOURCE_FILES} PROPERTIES LANGUAGE ASM_NASM)

add_executable(LabyrinthGenerator ${SOURCE_FILES})

target_link_libraries(LabyrinthGenerator X11)

target_link_options(LabyrinthGenerator PRIVATE --dynamic-linker /lib64/ld-linux-x86-64.so.2)
cmake_minimum_required(VERSION 3.23)
