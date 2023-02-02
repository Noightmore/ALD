; LabyrinthGenerator

extern print
extern print_num
extern parse_uint64
extern exit
extern get_random_value_by_seed
extern allocate_grid
extern populate_grid_with_tile_ids

section .rodata

section .data
    ;seed: dq 7843

section .bss

section .text
    global _start

_start:

    times 3 pop rdi ; pops argument (size of the grid) ; 2D grid is size x size big
    ; is popped 3 times bcs first arg is always number of args which is not cared about for now
    ; second one is the name of the program which is not cared about
    ; any other garbage on stack is to not be cared about
    pop rsi ; pops the seed

    ; allocate space for local variables
    sub rsp, 16
    ; rsp = grid size local variable
    ; rsp+8 = seed

    ; general_functions.parse_uint64(rdi)
    call parse_uint64
    mov [ss:rbp], rax

    mov rdi, rsi
    ; general_functions.parse_uint64(rdi)
    call parse_uint64
    mov [ss:rbp-8], rax

    mov rdi, [ss:rbp] ; load grid size
    ; grid.allocate_grid(rdi)
    ;call allocate_grid ; allocate grid

    mov rdi, [ss:rbp-8] ; load seed
    ; grid.populate_grid(rdi)
   ; call populate_grid_with_tile_ids ; populate grid

    mov rsi, 20
    _loop:

    push rsi
    mov rdi, [ss:rbp-8]
    call get_random_value_by_seed
    mov [ss:rbp-8], rax
    ; mov lower 32 bits of rax to rdi and zero extend to 64 bits
    mov rdi, rax
    call print_num

    pop rsi
    dec rsi
    jnz _loop


    mov rdi, 0
    call exit