import {Routes as Router, Route} from "react-router-dom";
import LoginPage from "./pages/LoginPage.tsx";

const Routes = () => {
    return (
        <>
            <Router>
                <Route path="/login" element={<LoginPage />} />
            </Router>
        </>
    )
}

export default Routes;