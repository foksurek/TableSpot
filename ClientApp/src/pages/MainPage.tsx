import {useEffect, useRef, useState} from "react";
import {ACCOUNT__PARSE_RECENTLY_SEARCHED_RESTAURANTS} from "ApiConst/ApiResponse.ts";
import axios from "axios";
import API_URLS from "ApiConst/ApiUrls.ts";
import {useAuth} from "contexts/AuthContext.tsx";
import { Link } from "react-router-dom";
import ArrowBackIosIcon from '@mui/icons-material/ArrowBackIos';
import ArrowForwardIosIcon from '@mui/icons-material/ArrowForwardIos';
import BusinessCard from "components/BusinessCard";
const MainPage = () => {

    const {user} = useAuth();
    const [recentlySearched, setRecentlySearched] = useState<ACCOUNT__PARSE_RECENTLY_SEARCHED_RESTAURANTS | string>("")

    const scrollRef = useRef<HTMLDivElement>(null);
    
    const fetchRecentlySearched = async () => {
        const restaurants = localStorage.getItem("restaurants");
        const jsonObject = JSON.parse(restaurants as unknown as string);
        await axios.post<ACCOUNT__PARSE_RECENTLY_SEARCHED_RESTAURANTS>(API_URLS.ACCOUNT.PARSE_RECENTLY_SEARCHED_RESTAURANTS, {
            ids: jsonObject
        }, {withCredentials: true}).then((response) => {
            setRecentlySearched(response.data);
        }).catch((error) => {
            if (error.response.status === 404) {
                setRecentlySearched("No recently searched restaurants");
            }
        });
    }
    
    const handleScrollRight = () => {
        
        if (scrollRef.current) {
            scrollRef.current.scrollBy({
                left: 400,
                behavior: 'smooth'
            });

        }
    };
    const handleScrollLeft = () => {
        
        if (scrollRef.current) {
            scrollRef.current.scrollBy({
                left: -400,
                behavior: 'smooth'
            });

        }
    };

    useEffect(() => {
        fetchRecentlySearched();
    }, []);

    if (user)
        return(
            <>
                <main className="pageContainer">
                    <section id="recentlySearched">
                        <h2>Recently searched</h2>
                        <div className="recentlySearchedContainer">
                            <ArrowBackIosIcon onClick={handleScrollLeft}/>
                            <div className="recentlySearched" ref={scrollRef}>
                                {typeof recentlySearched !== "string" ?
                                    (recentlySearched.data.slice(0, 10).reverse().map((restaurant) => {
                                        return (
                                            <Link to={`/restaurant/${restaurant.id}`}><BusinessCard
                                                restaurant={restaurant}/></Link>
                                        )
                                    }))
                                    :
                                    (<span>{recentlySearched}</span>)
                                }
                            </div>
                            <ArrowForwardIosIcon onClick={handleScrollRight}/>
                        </div>
                    </section>
                </main>
            </>
        )

    return (
        <>
            <main className="pageContainer">

            </main>
        </>
    )
}

export default MainPage
