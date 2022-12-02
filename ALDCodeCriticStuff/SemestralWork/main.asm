

%include "./tools/interface_tools.asm"
%include "./tools/general_tools.asm"
%include "./tools/print_tools.asm"
%include "./tools/memory_tools.asm"

section .rodata
    x86_64_ptr_byte_size equ 8 ; 64-bit pointers are 8 bytes

section .bss
    page_size: resq 1 ; an actual 64-bit value
    grid: resq 1 ; pointer to the start of the grid 2D array
    seed: resq 1 ; seed for the random number generator
    page_id: resq 1 ; page id for the random number generator

section .text
    global _start

_start:
    times 3 pop rdi ; pops argument (size of the grid) ; 2D grid is size x size big
    ; is popped 3 times bcs first arg is always number of args which is not cared about for now
    ; second one is the name of the program which is not cared about
    ; any other garbage on stack is to not be cared about
    call parse_uint64
    mov [page_size], rax

    ; allocate memory for the grid
    mov rax, [page_size] ; grid row count
    mov rdi, 8
    mul rdi ; rax = rax * rdi
    mov rdi, rax
    call simple_malloc ; rax contains the address of the allocated memory
    mov [grid], rax ; store the address of the allocated memory in grid

    call exit


