export interface ItemResponseDto {
    id: number;
    name: string;
    category: string;
    rarity: string;
    basePrice: number;
    imageUrl?: string;
}

export interface CreateItemRequestDto {
    name: string;
    category: string;
    rarity: string;
    basePrice: number;
    imageUrl?: string;
}
