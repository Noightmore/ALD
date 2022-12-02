section .data
    malloc_init: dq 0
    malloc_memory: dq 0

section .rodata
    mmap_failed: db "mmap failed", 10, 0

section .text

;----------------------------------------
; Map new pages into the process.
;
; @param rdi
;   Number of bytes to map.
;
; @returns
;   Address of allocated memory.
;
mmap:
    push rbp
    mov rbp, rsp

    push rdi
    ; map shared memory instead or 32, 4
    mov rax, 0x9 ; mmap kernel code
    mov rdi, 0x0 ; kernel chooses address
    pop rsi ; number of bytes to map
    mov rdx, 0x3 ; PROT_READ | PROT_WRITE
    mov r10, 0x22 ; MAP_PRIVATE | MAP_ANONYMOUS ; note maybe map shared for async
    mov r8, 0x0 ; no backing file
    mov r9, 0x0 ; no offset

    syscall

    push rax

    mov rdi, rax
    sub rdi, 0xffffffffffffffff ; check MAP_FAILED was not returned
    lea rsi, [mmap_failed]
    call assert_not_null
    pop rax

    leave
    ret


;------------------------------------------------------------------
; Simple implementation of malloc.
;
; Note that there is no equivalent of free, this just allocates memory from a large static buffer.
;
; @param rdi
;   Number of bytes to allocate.
;
; @returns
;   Address of allocated memory in rax
;
simple_malloc:
    push rbp
    mov rbp, rsp

    push rdi

    mov rax, [malloc_init]
    cmp rax, 0x0
    jne malloc_do_init

    ; calculates the total amount of memory based on the size of the 2D array
    ; rdi * rdi/8 (bit shift right 3, dont do div) * 1 (1 byte per cell)
    ; so basically rdi * rdi/8

    ; example 1000x1000 grid will take 8 megabytes of virtual memory (8000 * 1000) rounded up to the nearest page
    ; so in total this will require 1953.125 pages (1954 pages rounded up) so 8003584 bytes

    mov rax, rdi
    shr rax, 3
    mul rdi

    mov rdi, rax
    push rdi
    call print_num
    pop rdi
    ;mov rdi, 409600 ; 400 KB = 100 pages
    call mmap

    mov [malloc_memory], rax
    mov rax, 0x1
    mov [malloc_init], rax

malloc_do_init:

    pop rdi

    mov rax, [malloc_memory] ; get address from head of buffer
    mov rbx, rax
    add rbx, rdi ; advance buffer by requested size
    mov [malloc_memory], rbx

    leave
    ret

; free:
;     neumime