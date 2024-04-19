import {Routes as Router, Route} from "react-router-dom";
import LoginPage from "pages/LoginPage.tsx";
import MainPage from "pages/MainPage.tsx";
import RestaurantPage from "pages/RestaurantPage.tsx";
import RegisterPage from "pages/RegisterPage.tsx";
import DashboardPage from "pages/dashboard/DashboardPage.tsx";
import RestaurantsPage from "pages/dashboard/RestaurantsPage.tsx";
import {useAuth} from "contexts/AuthContext.tsx";


const Routes = () => {
    const {user} = useAuth();
    return (
        <>
            <Router>
                <Route path="/login" element={<LoginPage />} />
                <Route path="/" element={<MainPage />} />
                <Route path="/restaurant/:id" element={<RestaurantPage />} />
                <Route path="/register" element={<RegisterPage/>} />
                <Route path="*" element={<h1>404 - Not Found</h1>} />
                {/*    type 3 pages*/}
                {user && user.accountType === 3 &&
                    <>
                        <Route path="/dashboard" element={<DashboardPage/>}/>
                        <Route path="/dashboard/employees" element={<DashboardPage/>} />
                        <Route path="/dashboard/restaurants" element={<RestaurantsPage/>} />
                    </>
                }
                
            </Router>
        </>
    )
}

export default Routes;