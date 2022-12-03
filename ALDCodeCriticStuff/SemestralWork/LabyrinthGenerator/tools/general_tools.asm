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
; Exit the program.
;
; @param rdi
;    Exit code.
exit:
    mov rax, 0x3c
    syscall