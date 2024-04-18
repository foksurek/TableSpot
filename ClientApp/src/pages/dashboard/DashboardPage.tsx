import SideNavigation from "components/pageElements/SideNavigation.tsx";
import { useAuth } from "contexts/AuthContext";
import { useNavigate } from "react-router-dom";

const DashboardPage = () => {

    const navigate = useNavigate();
    const { user } = useAuth();
    if (!user) return navigate("/login");
    if (user.accountType === 1) return navigate("/");
    
    return (
        <>
            <SideNavigation/>
        </>
    )
    
}

export default DashboardPage