import {useState} from "react";
import { Button } from "@mui/material";
import { Link } from "react-router-dom";
import MenuIcon from '@mui/icons-material/Menu';
import SearchBarPlaceholder from "../SearchBarPlaceholder.tsx";
import SearchPanel from "./SearchPanel.tsx";
import {useAuth} from "../../contexts/AuthContext.tsx";

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
                {user ?
                    <Link to="" onClick={closeNav}><Button variant="contained">Dashboard</Button></Link>
                    :
                    <Link to="/login" onClick={closeNav}><Button variant="outlined">Login</Button></Link>}
                    <Link to="" onClick={closeNav}><Button variant="contained">Add your business</Button></Link>
                </span>
            </nav>
        </>
    );
};

export default Navigation;
