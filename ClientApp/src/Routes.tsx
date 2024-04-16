import {Routes as Router, Route} from "react-router-dom";
import LoginPage from "./pages/LoginPage.tsx";
import MainPage from "./pages/MainPage.tsx";
import RestaurantPage from "./pages/RestaurantPage.tsx";
import RegisterPage from "./pages/RegisterPage.tsx";
import DashboardPage from "./pages/DashboardPage.tsx";

const Routes = () => {
    return (
        <>
            <Router>
                <Route path="/login" element={<LoginPage />} />
                <Route path="/" element={<MainPage />} />
                <Route path="/restaurant/:id" element={<RestaurantPage />} />
                <Route path="/register" element={<RegisterPage/>} />
                <Route path="/dashboard" element={<DashboardPage/>} />
                <Route path="*" element={<h1>404 - Not Found</h1>} />
            </Router>
        </>
    )
}

export default Routes;