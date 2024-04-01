import {Routes as Router, Route} from "react-router-dom";
import LoginPage from "./pages/LoginPage.tsx";
import MainPage from "./pages/MainPage.tsx";
import RestaurantPage from "./pages/RestaurantPage.tsx";

const Routes = () => {
    return (
        <>
            <Router>
                <Route path="/login" element={<LoginPage />} />
                <Route path="/" element={<MainPage />} />
                <Route path="/restaurant/:id" element={<RestaurantPage />} />
                <Route path="*" element={<h1>404 - Not Found</h1>} />
            </Router>
        </>
    )
}

export default Routes;