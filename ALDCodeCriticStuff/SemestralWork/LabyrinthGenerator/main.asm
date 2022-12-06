

%include "./LabyrinthGenerator/user_interface/interface_tools.asm"
%include "./LabyrinthGenerator/user_interface/tile_data.asm"
%include "./LabyrinthGenerator/user_interface/print_tools.asm"
%include "./LabyrinthGenerator/tools/memory_tools.asm"
%include "./LabyrinthGenerator/tools/general_tools.asm"


section .rodata
    x86_64_ptr_byte_size equ 8 ; 64-bit pointers are 8 bytes

    A  equ	16807 ; ¯\_(ツ)_/¯
    S0 equ seed ;	Low order word of seed
    S1 equ seed + 2 ;	High order word of seed
section .data

section .bss
    grid_size: resq 1 ; an actual 64-bit value
    grid: resq 1 ; pointer to the start of the grid 2D array
    seed: resq 1 ; seed for the random number generator
    ;seed_len: resq 1 ; length of the seed

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

;    pop rdi ; pops argument (seed length)
;    ; calculate the len of the seed
;    call get_string_len
;    mov [seed_len], rax

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
    mov rdi, [seed]
    mov rsi, tile_data_len
    shr rsi, 3 ; rsi = rsi / 8 ; rsi = tile_data_len / 8 ; convert pointer len to array len
    call get_modulus ; compute the id of the tile
    mov rdi, rax ; rdi = id of the tile
    call print_num
    ; ------



    leave
    ret

; directly manipulates with the seed data, internal method...
get_random_value_by_seed:
push rbp
mov rbp, rsp

;
;	P2|P1|P0 := (S1|S0) * A
;
mov	ax, [S0]
mov	bx, A
mul	bx
mov	si, dx ;	si := pp01  (pp = partial product)
mov	di, ax ;	di := pp00 = P0
mov	ax, [S1]
mul	bx ;	ax := pp11
add	ax, si ;	ax := pp11 + pp01 = P1
adc	dx, 0 ;	dx := pp12 + carry = P2

;
;	P2|P1:P0 = p * 2**31 + q, 0 <= q < 2**31
;
;	p = P2 SHL 1 + sign bit of P1 --> dx
;		(P2:P1|P0 < 2**46 so p fits in a single word)
;	q = (P1 AND 7FFFh)|P0 = (ax AND 7fffh) | di
;
shl	ax, 1
rcl	dx, 1
shr	ax, 1
;
;	dx:ax := p + q
;
add	dx, di ;	dx := p0 + q0
adc	ax, 0 ;	ax := q1 + carry
xchg ax, dx
;
;	if p+q < 2**31 then p+q is the new seed; otherwise whack
;	  off the sign bit and add 1 and THATs the new seed
;
test	dx, 8000h
jz	Store
and	dx, 7fffh ; ¯\_(ツ)_/¯
add	ax, 1 ;		inc doesn't set carry bit
adc	dx, 0

Store:
	mov	[S1], dx
	mov	[S0], ax

leave
ret
