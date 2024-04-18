export type restaurant = {
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
}

export type RESTAURANT__GET_BY_NAME = {
    code: number;
    data: Array<restaurant>
}

export type ACCOUNT__PARSE_RECENTLY_SEARCHED_RESTAURANTS = {
    code: number;
    data: Array<restaurant>

}

export type ACCOUNT__GET_MY_RESTAURANTS = {
    code: number;
    data: Array<restaurant>

}