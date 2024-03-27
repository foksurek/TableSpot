import {Modal} from "@mui/material";
import SearchBar from "../SearchBar.tsx";
import {useEffect, useState} from "react";
import {RESTAURANT__GET_BY_NAME} from "../../ApiConst/ApiResponse.ts";
import axios from "axios";
import API_URLS from "../../ApiConst/ApiUrls.ts";

type Props = {
    setSearchModalOpen: (arg0: boolean) => void;
    searchModalOpen: boolean;
}

const SearchPanel = (props: Props) => {

    const [searchResults, setSearchResults] = useState<RESTAURANT__GET_BY_NAME | null>(null);
    const [searchQuery, setSearchQuery] = useState<string>("");
    
    const fetchSearchResults = async () => {
        await axios.get<RESTAURANT__GET_BY_NAME>(API_URLS.RESTAURANT.GET_BY_NAME(searchQuery, 10, 0)).then((response) => {
            setSearchResults(response.data);
        })
    }
    
    const handleImageError = (e: any) => {
        e.target.remove()
    }

    useEffect(() => {
        fetchSearchResults();
    }, []);
    
    useEffect(() => {
        const delayDebounceFn = setTimeout(() => {
            fetchSearchResults();
        }, 500);
        return () => clearTimeout(delayDebounceFn);
    }, [searchQuery]);
    
    return (
        <>
            <Modal open={props.searchModalOpen} onClose={() => props.setSearchModalOpen(false)}>
                <div className="searchPanel">
                    <SearchBar onChange={(e) => {setSearchQuery(e.target.value)}}/>
                    <div className="searchResults">
                        <h2>Search results</h2>
                        {searchResults?.data.map((restaurant) => {
                            return (
                                <div className="searchResultItem" key={restaurant.id}>
                                    <img onError={handleImageError} className="searchResultImage" src={restaurant.imageUrl} alt="restaurnt cover"/>
                                    <span className="searchResultName">
                                        {restaurant.name}
                                    </span>
                                    {/*TODO: Add rating*/}
                                    <span className="searchResultRating">★★★★★</span> 
                                    <span className="searchResultDescription">
                                        {restaurant.description}
                                    </span>
                                </div>
                            )
                        })}
                    </div>
                </div>
            </Modal>
        </>
    );
}

export default SearchPanel;