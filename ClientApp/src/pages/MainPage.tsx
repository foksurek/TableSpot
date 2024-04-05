import {useEffect, useState} from "react";
import {ACCOUNT__PARSE_RECENTLY_SEARCHED_RESTAURANTS} from "../ApiConst/ApiResponse.ts";
import axios from "axios";
import API_URLS from "../ApiConst/ApiUrls.ts";

const MainPage = () => {

    const [recentlySearched, setRecentlySearched] = useState<ACCOUNT__PARSE_RECENTLY_SEARCHED_RESTAURANTS | null>(null)

    const fetchRecentlySearched = async () => {
        const restaurants = localStorage.getItem("restaurants");
        const jsonObject = JSON.parse(restaurants as unknown as string);
        await axios.post<ACCOUNT__PARSE_RECENTLY_SEARCHED_RESTAURANTS>(API_URLS.ACCOUNT.PARSE_RECENTLY_SEARCHED_RESTAURANTS, {
            data: jsonObject
        })
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
                    {localStorage.getItem("restaurants") ?
                        (JSON.parse(localStorage.getItem("restaurants") as unknown as string).map((restaurant: string) => {
                            return (
                                <div key={restaurant}>
                                    <a href={`/restaurant/${restaurant}`}>{restaurant}</a>
                                </div>
                            )
                        }))
                        :
                        (<span>Nothing</span>)
                    }
                </section>
            </main>
        </>
    )
}

export default MainPage
