import {useParams} from "react-router-dom";
import {useEffect} from "react";
import axios from "axios";
import API_URLS from "../ApiConst/ApiUrls.ts";

const RestaurantPage = () => {
    
    const { id } = useParams();
    
    const saveToLocalStorage = async () => {
        const existingList = localStorage.getItem('restaurants');
        
        const response = await axios.get(API_URLS.RESTAURANT.GET_BY_ID(id as unknown as string));
        
        if (response.status !== 200) {
            return;
        }

        if (existingList) {
            let listArray = JSON.parse(existingList) as string[];

            if (!listArray.includes(id as unknown as string)) {
                listArray.push(id as unknown as string);

                if (listArray.length > 10) {
                    listArray = listArray.slice(1);
                }
                
                localStorage.setItem('restaurants', JSON.stringify(listArray));
            }
        } else {
            localStorage.setItem('restaurants', JSON.stringify([id as unknown as string]));
        }
    };

    useEffect(() => {
        saveToLocalStorage();
    }, [, id]);
    
    return (
        <>
            <h1>Restaurant {id}</h1>
        </>
    )
}

export default RestaurantPage