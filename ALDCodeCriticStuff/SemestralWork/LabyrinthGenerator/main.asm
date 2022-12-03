

%include "./LabyrinthGenerator/user_interface/interface_tools.asm"
;%include "./LabyrinthGenerator/user_interface/tile_data.asm"
%include "./LabyrinthGenerator/user_interface/print_tools.asm"
%include "./LabyrinthGenerator/tools/memory_tools.asm"
%include "./LabyrinthGenerator/tools/general_tools.asm"

section .rodata
    x86_64_ptr_byte_size equ 8 ; 64-bit pointers are 8 bytes
    newline db 0x0A, 0 ; newline character
section .data

section .bss
    grid_size: resq 1 ; an actual 64-bit value
    grid: resq 1 ; pointer to the start of the grid 2D array
    seed: resq 1 ; seed for the random number generator

section .text
    global _start

_start:
    times 3 pop rdi ; pops argument (size of the grid) ; 2D grid is size x size big
    ; is popped 3 times bcs first arg is always number of args which is not cared about for now
    ; second one is the name of the program which is not cared about
    ; any other garbage on stack is to not be cared about
    call parse_uint64
    mov [grid_size], rax

    pop rdi ; pops argument (seed for the random number generator)
    call parse_uint64
    mov [seed], rax

    ; allocate memory for the grid
    mov rax, [grid_size] ; grid row count
    mov rdi, 8
    mul rdi ; rax = rax * rdi
    mov rdi, rax
    call simple_malloc ; rax contains the address of the allocated memory
    mov [grid], rax ; store the address of the allocated memory in grid

    ; allocate each row
    xor rdx, rdx ; initialize row index

    _grid_allocator:
    push rdx ; push row index

    mov rdi, [grid_size] ; grid column count
    call simple_malloc ; rax contains the address of the allocated memory
    push rax ; temporarily store the address of the allocated memory

    ; compute position in the grid
    mov rax, rdx ; get the pointer offset in the grid (index of the row)
    mov rcx, x86_64_ptr_byte_size
    mul rcx ; rax = rax * x86_64_ptr_byte_size
    add rax, [grid] ; rax = rax + grid (starting address of the grid)

;    push rax
;    mov rdi, rax
;    call print_num
;    pop rax

    pop rcx
    mov [rax], rcx ; store the address of the allocated memory in grid ; rax = pointer to the data

    pop rdx ; pop row index
    inc rdx
    cmp rdx, qword [grid_size] ; check if row index is less than grid row count
    jnz _grid_allocator

    ; grid allocated

    call exit