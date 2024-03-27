export type RESTAURANT__GET_BY_NAME = {
    code: number;
    data: Array<{
        id: number;
        name: string
        address: string
        description: string
        imageUrl: string
        category: {
            id: number
            name: string
        }
        email: string
        website: string
        phoneNumber: string
    }>
}