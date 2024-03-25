import {useEffect, useState} from "react";
import { Button } from "@mui/material";
import { Link } from "react-router-dom";
import SearchBar from "../SearchBar.tsx";
import MenuIcon from '@mui/icons-material/Menu';

const Navigation = () => {
    const [isNavOpen, setIsNavOpen] = useState(true);

    const toggleNav = () => {
        setIsNavOpen(!isNavOpen);
    };



    return (
        <nav className={`nav-full ${isNavOpen && (document.body.clientWidth < 1700) ? 'open' : ''}`}>
            <div className="menu-top">
                <Link to="/"><h1>TableSpot</h1></Link>
                <MenuIcon onClick={toggleNav} />
            </div>
            <>
                <>
                    <ul>
                        <li><Link to="">Home</Link></li>
                        <li><Link to="">Browse</Link></li>
                        <li><Link to="">Third option</Link></li>
                    </ul>
                    <SearchBar/>
                    <span>
                <Button variant="outlined">Log In</Button>
                <Button variant="contained">Add your business</Button>
                </span>
                </>
            </>
        </nav>
    );
};

export default Navigation;
