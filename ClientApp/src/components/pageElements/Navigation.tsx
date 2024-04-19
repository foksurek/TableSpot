import {useState} from "react";
import { Link } from "react-router-dom";
import MenuIcon from '@mui/icons-material/Menu';
import SearchBarPlaceholder from "../SearchBarPlaceholder.tsx";
import SearchPanel from "./SearchPanel.tsx";
import {useAuth} from "../../contexts/AuthContext.tsx";
import axios from "axios";
import API_URLS from "../../ApiConst/ApiUrls.ts";
import DivButton from "../divButton.tsx";

const Navigation = () => {
    const [isNavOpen, setIsNavOpen] = useState(false);
    const [searchModalOpen, setSearchModalOpen] = useState(false);

    const {user} = useAuth();

    const toggleNav = () => {
        setIsNavOpen(!isNavOpen);
    };

    const closeNav = () => {
        setIsNavOpen(false);
    }

    const logout = async () => {
        await axios.get(API_URLS.AUTHORIZATION.LOGOUT, {withCredentials: true}).then(() => {
            window.location.href = '/';
        });
    }

    return (
        <>
            {searchModalOpen && <SearchPanel setSearchModalOpen={setSearchModalOpen} searchModalOpen={searchModalOpen}/>}
            <nav className={`nav ${isNavOpen && (document.body.clientWidth < 1700) ? 'open' : ''}`}>
                <div className="menu-top">
                    <Link to="/" onClick={closeNav}><h1>TableSpot</h1></Link>
                    <MenuIcon onClick={toggleNav}/>
                </div>
                <ul>
                    <li><Link to="" onClick={closeNav}>Home</Link></li>
                    <li><Link to="" onClick={closeNav}>Browse</Link></li>
                    <li><Link to="" onClick={closeNav}>Third option</Link></li>
                </ul>
                <SearchBarPlaceholder onClick={() => {setSearchModalOpen(true); closeNav()}}/>
                <span>
                    {user &&
                        <DivButton onClick={logout} outline={true}>Logout</DivButton>
                    }
                    {user && user.accountType === 3 && 
                        <Link to="/dashboard" onClick={closeNav}><DivButton>Dashboard</DivButton></Link>
                    }
                    {!user &&    
                        <Link to="/login" onClick={closeNav}><DivButton outline={true}>Login</DivButton></Link>
                    }
                    {user?.accountType == 1 || !user && 
                        <Link to="" onClick={closeNav}><DivButton>Add your business</DivButton></Link>
                    }
                </span>
            </nav>
        </>
    );
};

export default Navigation;
