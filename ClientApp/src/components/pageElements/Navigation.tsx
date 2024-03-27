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

    return (
        <>
            {searchModalOpen && <SearchPanel setSearchModalOpen={setSearchModalOpen} searchModalOpen={searchModalOpen}/>}
            <nav className={`nav ${isNavOpen && (document.body.clientWidth < 1700) ? 'open' : ''}`}>
                <div className="menu-top">
                    <Link to="/"><h1>TableSpot</h1></Link>
                    <MenuIcon onClick={toggleNav}/>
                </div>
                <ul>
                    <li><Link to="">Home</Link></li>
                    <li><Link to="">Browse</Link></li>
                    <li><Link to="">Third option</Link></li>
                </ul>
                <SearchBarPlaceholder onClick={() => {setSearchModalOpen(true)}}/>
                <span>
                {user ?
                    <Link to=""><Button variant="contained">Dashboard</Button></Link>
                    :
                    <Link to="/login"><Button variant="outlined">Login</Button></Link>}
                    <Link to=""><Button variant="contained">Add your business</Button></Link>
                </span>
            </nav>
        </>
    );
};

export default Navigation;
