global assert_not_null
global assert_null
global parse_uint64
global get_modulus
global get_string_len
global get_random_value_by_seed
global exit

extern print


;------------------------------------------
; Assert the input is not null.
;
; @param rdi
;   Value to check is not null.
;
; @param rsi
;   Pointer to error message string.
;
assert_not_null:
    push rbp
    mov rbp, rsp

    cmp rdi, 0
    jne assert_not_null_end

    mov rdi, rsi
    call print

    mov rdi, 1
    call exit

assert_not_null_end:

    pop rbp
    ret

;------------------------------------------
; Assert the input is null.
;
; @param rdi
;   Value to check is null.
;
; @param rsi
;   Pointer to error message string.
;
assert_null:
    push rbp
    mov rbp, rsp

    cmp rdi, 0
    je assert_null_end

    mov rdi, rsi
    call print

    mov rdi, 1
    call exit

assert_null_end:

    leave
    ;pop rbp
    ret

;------------------------------------------
; Parse string to unsigned 64bit integer (ulong ulong)
; @param rdi
;   Pointer to string to parse.
; rax = result
parse_uint64:
    push rbp
    mov rbp, rsp

    xor rax, rax
    _nextChar:
        movzx rcx, byte [rdi]
        inc rdi
        cmp rcx, '0' ; is valid??
        jb _end
        cmp rcx, '9' ; is valid??
        ja _end
        sub rcx, '0'
        imul rax, 10
        add rax, rcx
        jmp _nextChar

    _end:
        leave
        ret


;------------------------------------------
; compute modulus
; @param rdi
;   dividend
; @param rsi
;   divisor
; @return rax = rdi % rsi
get_modulus:
    push rbp
    mov rbp, rsp

    mov rax, rdi
    mov rcx, rsi
    xor rdx, rdx
    div rcx
    mov rax, rdx

    leave
    ret

;------------------------------------------
; String length calculation function
; @param rdi
;   Pointer to string to calculate length of.
; @return rax = length of string
get_string_len:
    push rbp
    mov rbp, rsp

    xor rax, rax
    _nextCharacter:
        movzx rcx, byte [rdi]
        inc rdi
        cmp rcx, 0
        je _slen_finished
        inc rax
        jmp _nextCharacter

    _slen_finished:
    leave
    ret

;------------------------------------------
; return random number by seed value
; @param rdi seed value (unsigned 64bit integer)
; @return rax random number (64bit unsigned)
get_random_value_by_seed:
    push rbp
    mov rbp, rsp

    ; allocate space on stack for seed value
    sub rsp, 16
    mov [ss:rbp], qword rdi

    ;
    ;	P2|P1|P0 := (S1|S0) * A
    ;
    mov	eax, dword [ss:rbp-8] ; load low order word of seed
    mov	ebx, 16807 ; ¯\_(ツ)_/¯
    mul	ebx
    mov	esi, edx ;	si := pp01  (pp = partial product)
    mov	edi, eax ;	di := pp00 = P0
    mov	eax, dword [ss:rbp-4] ; load high order word of seed
    mul	ebx ;	ax := pp11
    add	eax, esi ;	ax := pp11 + pp01 = P1
    adc	edx, 0 ;	dx := pp12 + carry = P2

    ;
    ;	P2|P1:P0 = p * 2**31 + q, 0 <= q < 2**31
    ;
    ;	p = P2 SHL 1 + sign bit of P1 --> dx
    ;		(P2:P1|P0 < 2**46 so p fits in a single word)
    ;	q = (P1 AND 7FFFh)|P0 = (ax AND 7fffh) | di
    ;
    shl	eax, 1
    rcl	edx, 1
    shr	eax, 1
    ;
    ;	dx:ax := p + q
    ;
    add	edx, edi ;	dx := p0 + q0
    adc	eax, 0 ;	ax := q1 + carry
    xchg eax, edx
    ;
    ;	if p+q < 2**63 then p+q is the new seed; otherwise whack
    ;	  off the sign bit and add 1 and THATs the new seed
    ;
    test edx, 80000000h
    jz	Store
    and	edx, 7fffffffh ;	whack off the sign bit
    add	eax, 1 ;		inc doesn't set carry bit
    adc	edx, 0

    Store:

	mov	dword [ss:rbp-4], edx
	mov	dword [ss:rbp-8], eax

	mov	rax, qword [ss:rbp-8] ; load new random value into rax

    ; free space on stack
    add rsp, 16
leave
ret

;------------------------------------------
; Exit the program.
;
; @param rdi
;    Exit code.
exit:
    mov rax, 0x3c
    syscall