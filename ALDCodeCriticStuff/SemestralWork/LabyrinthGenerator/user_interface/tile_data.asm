section .rodata

    ; text representation of the first tile
    ; bit representation of the first tile: 0b0000_1111
    tile0_bin: db 0x0F ; in dec it is 15
    tile0: dq tile00, tile01, tile02
    tile00 db " | ",0
    tile01 db "-|-",0
    tile02 db " | ",0

    ; text representation of the second tile
    ; bit representation of the second tile: 0b0000_0010
    tile1_bin: db 0x02 ; in dec it is 2
    tile1: dq tile10, tile11, tile12
    tile10 db "   ",0
    tile11 db " x-",0
    tile12 db "   ",0

    ; text representation of the third tile
    ; bit representation of the third tile: 0b0000_0110
    tile2_bin: db 0x06 ; in dec it is 6
    tile2: dq tile20, tile21, tile22
    tile20 db " | ",0
    tile21 db " |-",0
    tile22 db "   ",0

    ; text representation of the fourth tile
    ; bit representation of the fourth tile: 0b0000_1100
    tile3_bin: db 0x0C ; in dec it is 12
    tile3: dq tile30, tile31, tile32
    tile30 db " | ",0
    tile31 db "-| ",0
    tile32 db "   ",0

    ; text representation of the fifth tile
    ; bit representation of the fifth tile: 0b0000_1001
    tile4_bin: db 0x09 ; in dec it is 9
    tile4: dq tile40, tile41, tile42
    tile40 db "   ",0
    tile41 db "-| ",0
    tile42 db " | ",0

    ; text representation of the sixth tile
    ; bit representation of the sixth tile: 0b0000_0101
    tile5_bin: db 0x05 ; in dec it is 5
    tile5: dq tile50, tile51, tile52
    tile50 db " | ",0
    tile51 db " | ",0
    tile52 db " | ",0

    ; text representation of the seventh tile
    ; bit representation of the seventh tile: 0b0000_1010
    tile6_bin: db 0x0A ; in dec it is 10
    tile6: dq tile60, tile61, tile62
    tile60 db "   ",0
    tile61 db "---",0
    tile62 db "   ",0

    tile_data: dq tile0_bin, tile1_bin, tile2_bin, tile3_bin, tile4_bin, tile5_bin, tile6_bin
    tile_data_len equ $ - tile_data

section .text

ret