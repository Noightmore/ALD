global allocate_grid
global populate_grid_with_tile_ids

extern print_num
extern simple_malloc
extern get_random_value_by_seed
extern get_tile_data_len
extern get_tile_data_by_id
extern get_modulus


section .bss
    grid_size: resq 1 ; an actual 64-bit value
    grid: resq 1 ; pointer to the start of the grid 2D array

section .text

;---------------------------------------------------------------------------
; method that allocates memory for the grid
; param
;   rdi - value - size of the grid
allocate_grid:
    push rbp
    mov rbp, rsp

    sub rsp, 16 ; allocate space for local variables
    ; rsp = loop counter
    ; rbp + 8 = pointer to the current row data

    mov [grid_size], rdi ; store the size of the grid in the grid_size variable
    shl rdi, 3 ; multiply by 8 to get the size in bytes
    call simple_malloc ; call the simple_malloc function
    mov [grid], rax ; store the pointer to the grid in the grid variable

    xor rax, rax ; set the loop counter to 0
    mov [rsp], qword rax ; store 0 in the stack

    grid_allocating_loop:
        mov rdi, [grid_size] ; get the size of each column
        call simple_malloc ; call the simple_malloc function
        mov [rsp+8], rax ; store the pointer to the column in the stack

        mov rax, [rsp] ; get the row index
        shl rax, 3 ; multiply by 8 to get the row index in bytes
        add rax, [grid] ; add the row index to the grid pointer
        mov rbx, [rsp+8]
        mov [rax], rbx ; store the row pointer to the array of row pointers

        inc qword [rsp] ; increment the row index
        mov rax, [grid_size] ; get the size of the grid
        cmp [rsp], rax ; compare the row index to the size of the grid
        jne grid_allocating_loop ; if the row index is less than the size of the grid, jump to the grid_allocating_loop

    add rsp, 16 ; deallocate space for local variables
    leave
    ret

;---------------------------------------------------------------------------
; method that sets the value of all cells in the grid
; also checks each value if its compatible with other cells around it
; params
;   rdi - uint64 a seed for the random number generator
populate_grid_with_tile_ids:

    push rbp
    mov rbp, rsp

    ; try normal push/pop
    sub rsp, 64 ; allocate space for local variables
    ; rbp - 8 = 64bit seed
    ; rsp - 8 = is tile_data_len
    ; rsp - 9 = cell value (its unique binary representation)
    ; rsp - 10 = testing index
    ; rsp - 18 =
    mov [rsp], rdi ; store the seed in the stack

    call get_tile_data_len ; call the get_tile_data_len function
    mov [rsp+8], rax ; store the tile_data_len in the stack

    ; loop
    mov r12, 20

    _loop:

    ;mov qword [rsp-24], r12 ; store the testing index in the stack

    mov rdi, [rsp] ; get the seed
    call get_random_value_by_seed ; call the get_random_value_by_seed function
    mov [rsp], rax ; store the random value in the stack

    ; get seed id from the random value
    mov rdi, qword [rsp] ; get the random value
    mov rsi, qword [rsp + 8] ; get the tile_data_len
    shr rsi, 3 ; divide by 8 to get the number of tiles
    call get_modulus ; modulus gets us the tile id
;
    mov [rsp + 16], rax
    movzx rdi, byte [rsp + 16]
    call get_tile_data_by_id
    mov [rsp + 16], rax ; store the tile data in the stack
;
    movzx rdi, byte [rsp + 16] ; get the tile data
    call print_num ; print the tile data

    ;mov r12, qword [rsp-24]
    dec r12
    jnz _loop

    add rsp, 64 ; deallocate space for local variables
    leave
    ret

;---------------------------------------------------------------------------
; method that returns a pointer to the grid into rax
get_pointer_to_the_grid:
    mov rax, [grid]
    ret