import SideNavigation from "components/pageElements/SideNavigation.tsx";
import {useEffect, useState } from "react";
import {ACCOUNT__GET_MY_RESTAURANTS} from "ApiConst/ApiResponse.ts";
import BusinessCard from "components/dashboard/BusinessCard.tsx";
import axios from "axios";
import API_URLS from "ApiConst/ApiUrls.ts";
import {useAuth} from "contexts/AuthContext.tsx";
import { useNavigate } from "react-router-dom";

const restaurantsPage = () => {
    
    const [restaurants, setRestaurants] = useState<ACCOUNT__GET_MY_RESTAURANTS | undefined>();

    const navigate = useNavigate();
    const { user } = useAuth();
    if (!user) {
        navigate("/login");
        return;
    }
    if (user.accountType === 1) navigate("/");
    
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
                    <div className="divButton">Add New Business</div>
                </div>
                <div className="content">
                    {restaurants?.data.map((restaurant) => {
                        return (
                            <BusinessCard restaurant={restaurant}/>
                        )
                    })}
                </div>
            </main>
        </>
    )
}
export default restaurantsPage;