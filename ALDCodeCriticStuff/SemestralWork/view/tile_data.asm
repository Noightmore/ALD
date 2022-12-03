section .rodata

    ; text representation of the first tile
    ; nibble representation of the first tile: 0b1111
    tile0: dq tile00, tile01, tile02
    tile00 db " | ",0
    tile01 db "-|-",0
    tile02 db " | ",0

    ; text representation of the second tile
    ; nibble representation of the second tile: 0b0010
    tile1: dq tile10, tile11, tile12
    tile10 db "   ",0
    tile11 db " x-",0
    tile12 db "   ",0

    ; text representation of the third tile
    ; nibble representation of the third tile: 0b0110
    tile2: dq tile20, tile21, tile22
    tile20 db " | ",0
    tile21 db " |-",0
    tile22 db "   ",0

    ; text representation of the fourth tile
    ; nibble representation of the fourth tile: 0b1100
    tile3: dq tile30, tile31, tile32
    tile30 db " | ",0
    tile31 db "-| ",0
    tile32 db "   ",0

    ; text representation of the fifth tile
    ; nibble representation of the fifth tile: 0b1001
    tile4: dq tile40, tile41, tile42
    tile40 db "   ",0
    tile41 db "-| ",0
    tile42 db " | ",0

    ; text representation of the sixth tile
    ; nibble representation of the sixth tile: 0b0101
    tile5: dq tile50, tile51, tile52
    tile50 db " | ",0
    tile51 db " | ",0
    tile52 db " | ",0

    ; text representation of the seventh tile
    ; nibble representation of the seventh tile: 0b1010
    tile6: dq tile60, tile61, tile62
    tile60 db "   ",0
    tile61 db "---",0
    tile62 db "   ",0
