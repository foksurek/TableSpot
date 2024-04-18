import {Routes as Router, Route} from "react-router-dom";
import LoginPage from "./pages/LoginPage.tsx";
import MainPage from "./pages/MainPage.tsx";
import RestaurantPage from "./pages/RestaurantPage.tsx";
import RegisterPage from "./pages/RegisterPage.tsx";
import DashboardPage from "./pages/dashboard/DashboardPage.tsx";
import RestaurantsPage from "./pages/dashboard/RestaurantsPage.tsx";

const Routes = () => {
    return (
        <>
            <Router>
                <Route path="/login" element={<LoginPage />} />
                <Route path="/" element={<MainPage />} />
                <Route path="/restaurant/:id" element={<RestaurantPage />} />
                <Route path="/register" element={<RegisterPage/>} />
                <Route path="/dashboard" element={<DashboardPage/>}/>
                <Route path="/dashboard/employees" element={<DashboardPage/>} />
                <Route path="/dashboard/restaurants" element={<RestaurantsPage/>} />
                <Route path="*" element={<h1>404 - Not Found</h1>} />
            </Router>
        </>
    )
}

export default Routes;