import SideNavigation from "components/pageElements/SideNavigation.tsx";
import {useEffect, useState } from "react";
import {ACCOUNT__GET_MY_RESTAURANTS} from "ApiConst/ApiResponse.ts";
import BusinessCard from "components/dashboard/BusinessCard.tsx";
import axios from "axios";
import API_URLS from "ApiConst/ApiUrls.ts";
import DivButton from "../../components/divButton.tsx";

const restaurantsPage = () => {

    // eslint-disable-next-line react-hooks/rules-of-hooks
    const [restaurants, setRestaurants] = useState<ACCOUNT__GET_MY_RESTAURANTS | undefined>();

    // eslint-disable-next-line react-hooks/rules-of-hooks
    useEffect(() => {
        const fetchData = async () => {
            await axios.get(API_URLS.ACCOUNT.GET_MY_RESTAURANTS, {withCredentials: true}).then()
                .then((response) => {
                    setRestaurants(response.data);
                });
        }
        fetchData();
    }, []);

    return (
        <>
            <SideNavigation/>
            <main className="DashboardRestaurantsContainer">
                <div className="header">
                    <h1>My businesses</h1>
                    <DivButton>Add New Business</DivButton>
                </div>
                <div className="content">
                    {restaurants?.data.map((restaurant) => {
                        return (
                            <BusinessCard restaurant={restaurant} key={restaurant.id}/>
                        );
                    })}
                </div>
            </main>
        </>
    )
}
export default restaurantsPage;