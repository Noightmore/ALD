

%include "./LabyrinthGenerator/user_interface/interface_tools.asm"
%include "./LabyrinthGenerator/user_interface/tile_data.asm"
%include "./LabyrinthGenerator/user_interface/print_tools.asm"
%include "./LabyrinthGenerator/tools/memory_tools.asm"
%include "./LabyrinthGenerator/tools/general_tools.asm"


section .rodata
    x86_64_ptr_byte_size equ 8 ; 64-bit pointers are 8 bytes
    digits_vec: dq 1, 10, 100, 1000, 10000, 100000, 1000000, 10000000, 100000000, 1000000000
section .data

section .bss
    grid_size: resq 1 ; an actual 64-bit value
    grid: resq 1 ; pointer to the start of the grid 2D array
    seed: resq 1 ; seed for the random number generator
    seed_len: resq 1 ; length of the seed

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
    push rdi ; pushes it back on the stack
    call parse_uint64
    mov [seed], rax

    pop rdi ; pops argument (seed length)
    ; calculate the len of the seed
    call get_string_len
    mov [seed_len], rax

    ; allocation of memory for the grid --------------------------------------------------------------------------------
    ;
    mov rax, [grid_size] ; grid row count
    mov rdi, 8
    mul rdi ; rax = rax * rdi (8)
    mov rdi, rax
    call simple_malloc ; rax contains the address of the allocated memory
    mov [grid], rax ; store the address of the allocated memory in grid

    ; for loop that mallocs memory for each row of the grid
    xor rdx, rdx ; initialize row index

    _grid_allocator:
    push rdx ; push row index

    ; allocates grid_size amount of bytes and stores it onto the stack
    mov rdi, [grid_size] ; grid column count
    shr rdi, 1 ; rdi = rdi / 2 ; mem optimisation
    call simple_malloc ; rax contains the address of the allocated memory
    push rax ; temporarily store the address of the allocated memory

    ; compute position in the grid (pointer to the position to be new allocated row stored at)
    ; saves it in rax
    mov rax, rdx ; get the pointer offset in the grid (index of the row)
    mov rcx, x86_64_ptr_byte_size
    mul rcx ; rax = rax * x86_64_ptr_byte_size
    add rax, [grid] ; rax = rax + grid (starting address of the grid)
    pop rcx
    mov [rax], rcx ; store the address of the allocated memory in grid ; rax = pointer to the data row

    pop rdx ; pop row index
    inc rdx
    cmp rdx, qword [grid_size] ; check if row index is less than grid row count
    jnz _grid_allocator

    ; grid allocation done ---------------------------------------------------------------------------------------------
    ;

; print row pointers
;    mov rax, [grid] ; get the address of the grid
;    mov rcx, 0
;    _printLoop:
;        push rcx
;        push rax
;        mov rdi, [rax]
;        call print_num
;        pop rax
;        add rax, x86_64_ptr_byte_size
;        pop rcx
;        inc rcx
;        cmp rcx, [grid_size]
;        jnz _printLoop

; TODO: fill the grid with random values corresponding to the tile types
; check whether they are valid one by one, if not, generate a new one and repeat this until a valid one is found
; maybe add the filling to the generation part? Do everything in one loop?

; compute first row
mov rax, [grid] ; get the address of the grid
mov rdi, [rax] ; get the address of the first row
mov rsi, [grid_size] ; get the size of the grid
_gen_first_row:
    push rsi
    push rdi
    call gen_tile
    ; is this first tile in the row?
    pop rdi
    pop rsi
    add rdi, x86_64_ptr_byte_size
    dec rsi
    jnz _gen_first_row

; TODO: print the grid, create own method of doing so, pain, but lesser pain than learning a graphics library
; printing could also be added to the one loop cycle, rather not tho

    call exit

; generates a random tile
; returns the tile type in a duo, 8 bits. each duo is always compatible with each other
gen_tile:
    push rbp
    mov rbp, rsp

    ; generate a random number
    call get_random_value_by_seed

    ; converts the seed to id of the tile data
;    mov rdi, [seed]
;    mov rsi, tile_data_len
;    shr rsi, 3 ; rsi = rsi / 8
;    call get_modulus ; rax contains the modulus == position of the tile in the tile_data array

    ; print the tile id
;    push rax
;    mov rdi, [seed];rax -- DEBUG
;    call print_num
;    pop rax

    mov [seed], rax
    leave
    ret

; get random value from tile_data by seed
; returns the a random value based on previous input seed (stores it in seed variable)
; this subroutine just replaces the contents of seed with the new pseudo random value
get_random_value_by_seed:
    push rbp
    mov rbp, rsp

    ; middle square method for generating random number by seed

    ; square seed
    mov rax, [seed]
    mul rax
    push rax ; back up squared seed on stack

    ; loads trim divider-----------------
    mov rax, [seed_len]
    shr rax, 1 ; rax = rax / 2
    dec rax
    shl rax, 3 ; rdx = rdx * 8
    add rax, digits_vec
    mov rcx, [rax] ; get digit count by the amount of digits in the seed 1, 10, 100, 1000
    ;-------------------------------------
;    push rcx
;    push rax
;
;    mov rdi, rcx
;    call print_num ; DEBUG
;
;    pop rax
;    pop rcx

    pop rax ;  load squared seed
    div rcx ; rax = rax / rcx ; digits[trim]

    push rcx
    push rax

    mov rdi, rax
    call print_num ; DEBUG

    pop rax
    pop rcx

    xor rbx, rbx ; clear rdx
    xor r10, r10 ; clear r10 ; index of loop
    _foreachDigit:
        push rcx ; backup seed digit count
        push rbx ; backup rbx
        push rax ; backup squared seed

        mov rdi, rax
        mov rsi, rcx
        call get_modulus
        push rax ; backup the modulus product

        ; compute the digit id by loop iteration
        mov rax, r10
        shl rax, 3 ; rax = rax * 8
        add rax, digits_vec
        mov rcx, [rax] ; digit count for current iteration

        pop rax ; modulus product
        mul rcx ; rax = rax * rcx
        mov rcx, rax

        pop rax ; squared seed
        pop rbx ; final product
        ; rbx = rbx + rcx ; final_val + (squared seed % digit count of seed) * digit count of current iteration
        add rbx, rcx
        pop rcx ; restore seed digit count

        push rbx ; backup final product

;        push rax
;        push r10
;        push r11
;        push rcx
;        mov rdi, rax
;        call print_num
;        pop rcx
;        pop r11
;        pop r10
;        pop rax

        mov rbx, 10
        div rbx ; rax = rax / r11 == rax = rax / 10
        pop rbx ; load back final product


    inc r10 ; increment index of loop
    cmp r10, [seed_len]
    jnz _foreachDigit

    leave
    ret