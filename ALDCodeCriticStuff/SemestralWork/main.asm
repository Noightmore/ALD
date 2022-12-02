

%include "./tools/interface_tools.asm"
%include "./tools/general_tools.asm"
%include "./tools/print_tools.asm"
%include "./tools/memory_tools.asm"

section .bss
    grid_size: resq 1 ; an actual 64-bit value
    grid: resq 1 ; pointer to the start of the grid 2D array

section .text
    global _start

_start:
    times 3 pop rax ; pops argument (size of the grid) ; 2D grid is size x size big
    ; is popped 3 times bcs first arg is always number of args which is not cared about for now
    ; second one is the name of the program which is not cared about
    ; any other garbage on stack is to not be cared about
    mov rdi, rax
    call print ; todo: write some generic string to ulong ulong parser
    ;mov [grid_size], rax


;    mov rdi, [grid_size] ; rdi = size
;    call print_num
;    ; allocate memory for the grid
;    mov rax, [grid_size] ; grid row count
;    xor rdi, rdi ; rdi = 0
;    mov rdi, 64
;    mul rdi ; rax = rax * rdi
;    mov rdi, rax
;    call print_num
    ;call simple_malloc ; rax contains the address of the allocated memory
    ;mov [grid], rax ; store the address of the allocated memory in grid

    call exit


