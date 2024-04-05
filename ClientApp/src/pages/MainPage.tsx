import {useEffect, useState} from "react";
import {ACCOUNT__PARSE_RECENTLY_SEARCHED_RESTAURANTS} from "../ApiConst/ApiResponse.ts";
import axios from "axios";
import API_URLS from "../ApiConst/ApiUrls.ts";

const MainPage = () => {

    const [recentlySearched, setRecentlySearched] = useState<ACCOUNT__PARSE_RECENTLY_SEARCHED_RESTAURANTS | string>("")

    const fetchRecentlySearched = async () => {
        const restaurants = localStorage.getItem("restaurants");
        const jsonObject = JSON.parse(restaurants as unknown as string);
        await axios.post<ACCOUNT__PARSE_RECENTLY_SEARCHED_RESTAURANTS>(API_URLS.ACCOUNT.PARSE_RECENTLY_SEARCHED_RESTAURANTS, {
            ids: jsonObject
        }, {withCredentials: true}).then((response) => {
            setRecentlySearched(response.data);
        }).catch((error) => {
            setRecentlySearched(error.response.data.message)
        });
    }

    useEffect(() => {
        fetchRecentlySearched();
    }, []);

    return(
        <>
            <main className="pageContainer">
                <section id="recentlySearched">
                    {/*TODO: Think about how good is it*/}
                    <h2>Recently searched</h2>
                    {typeof recentlySearched !== "string" ?
                        (recentlySearched.data.map((restaurant) => {
                            return (
                                <div key={restaurant.id}>
                                    <a href={`/restaurant/${restaurant}`}>{restaurant.id}</a>
                                </div>
                            )
                        }))
                        :
                        (<span>{recentlySearched}</span>)
                    }
                </section>
            </main>
        </>
    )
}

export default MainPage
