const API_BASE_URL = "http://localhost:5115/api";

const API_URLS = {
    RESTAURANT: {
        GET_BY_NAME: (name: string, limit: number, offset: number) => `${API_BASE_URL}/Restaurant/GetByName?name=${name}&limit=${limit}&offset=${offset}`
    }
}

export default API_URLS;