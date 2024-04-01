import {useParams} from "react-router-dom";
import {useEffect} from "react";

const RestaurantPage = () => {
    
    const { id } = useParams();
    
    const saveToLocalStorage = () => {
        const existingList = localStorage.getItem('restaurants');

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
    }, []);
    
    useEffect(() => {
        saveToLocalStorage();
    }, [id]);
    
    return (
        <>
            <h1>Restaurant {id}</h1>
        </>
    )
}

export default RestaurantPage