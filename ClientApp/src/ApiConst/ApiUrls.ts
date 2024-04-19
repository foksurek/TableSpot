const API_BASE_URL = "http://localhost:5000/Api";

const API_URLS = {
    ACCOUNT: {
        GET_ACCOUNT_DATA: `${API_BASE_URL}/Account/GetAccountData`,
        PARSE_RECENTLY_SEARCHED_RESTAURANTS: `${API_BASE_URL}/Account/ParseRecentlySeenRestaurants`,
        GET_MY_RESTAURANTS: `${API_BASE_URL}/Account/GetMyRestaurants`
    },
    AUTHORIZATION: {
        LOGIN: `${API_BASE_URL}/Auth/Login`,
        CREATE_ACCOUNT: `${API_BASE_URL}/Auth/CreateAccount`,
        CHECK_SESSION:  `${API_BASE_URL}/Auth/CheckSession`,
        LOGOUT: `${API_BASE_URL}/Auth/Logout`
    },
    RESTAURANT: {
        SEARCH: (query: string, limit: number, offset: number) => `${API_BASE_URL}/Restaurant/Search?query=${query}&limit=${limit}&offset=${offset}`,
        GET_BY_ID: (id: string) => `${API_BASE_URL}/Restaurant/GetById?id=${id}`,
        CREATE: `${API_BASE_URL}/Restaurant/Create`
    }
}

export default API_URLS;