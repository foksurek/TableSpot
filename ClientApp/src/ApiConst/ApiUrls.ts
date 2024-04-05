const API_BASE_URL = "http://localhost:5115/Api";

const API_URLS = {
    ACCOUNT: {
        GET_ACCOUNT_DATA: `${API_BASE_URL}/Api/Account/GetAccountData`,
        PARSE_RECENTLY_SEARCHED_RESTAURANTS: `${API_BASE_URL}/Account/ParseRecentlySeenRestaurants`
    },
    AUTHORIZATION: {
        LOGIN: `${API_BASE_URL}/Api/Auth/Login`,
        CREATE_ACCOUNT: `${API_BASE_URL}/Api/Auth/CreateAccount`,
        CHECK_SESSION:  `${API_BASE_URL}/Api/Auth/CheckSession`
    },
    RESTAURANT: {
        SEARCH: (query: string, limit: number, offset: number) => `${API_BASE_URL}/Restaurant/Search?query=${query}&limit=${limit}&offset=${offset}`
    }
}

export default API_URLS;