const API_BASE_URL = "http://localhost:5115/api";

const API_URLS = {
    RESTAURANT: {
        SEARCH: (query: string, limit: number, offset: number) => `${API_BASE_URL}/Restaurant/Search?query=${query}&limit=${limit}&offset=${offset}`
    }
}

export default API_URLS;